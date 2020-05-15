using System;
using System.IO;
using System.Text;
using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Mathematics;

namespace Animation.Graphics
{
    public class Shader : IDisposable
    {
        private const string Path = @"./Graphics/Shaders/";
        private const string VertexFileFormat = ".vert";
        private const string FragmentFileFormat = ".frag";
        private const string GeometryFileFormat = ".geom";

        private bool _disposed;

        public Shader(string shaderName)
        {
            // =============
            // == Shaders ==
            // =============

            var vertexShader = CreateShader(
                ShaderType.VertexShader, 
                Path + shaderName + VertexFileFormat);
            
            var fragmentShader = CreateShader(
                ShaderType.FragmentShader, 
                Path + shaderName + FragmentFileFormat); 
            
            var geometryShader = CreateShader(
                ShaderType.GeometryShader, 
                Path + shaderName + GeometryFileFormat); 
            
            // =============
            // == Program ==
            // =============

            Program = GL.CreateProgram();
            
            GL.AttachShader(Program, vertexShader);
            GL.AttachShader(Program, fragmentShader);
            GL.AttachShader(Program, geometryShader);
            
            LinkProgram(Program);

            GL.DetachShader(Program, vertexShader);
            GL.DetachShader(Program, fragmentShader);
            GL.DetachShader(Program, geometryShader);
            
            // =============
            // == Cleanup ==
            // =============
            
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(geometryShader);
        }

        public int Program { get; }
        
        public void Use() => UseProgram();
        public void Bind() => UseProgram();
        public void UseProgram()
        {
            GL.UseProgram(Program);
        }

        public int GetAttribLocation(string attrib)
        {
            return GL.GetAttribLocation(Program, attrib);
        }

        public void SetUniformValue(string attrib, float data)
        {
            var loc = GetUniformLocation(attrib);
            if (loc < 0)
                throw new Exception();
            SetUniformValue(loc, data);
        }
        
        public void SetUniformValue(int location, float data)
        {
            GL.Uniform1(location, data);
        }

        public void SetUniformValue(string attrib, Vector3 data)
        {
            var loc = GetUniformLocation(attrib);
            if (loc < 0)
                throw new Exception();
            SetUniformValue(loc, data);
        }
        
        public void SetUniformValue(int location, Vector3 data)
        {
            GL.Uniform3(location, data);
        }

        public void SetUniformValue(string attrib, int data)
        {
            var loc = GetUniformLocation(attrib);
            if (loc < 0)
                throw new Exception();
            SetUniformValue(loc, data);
        }
        
        public void SetUniformValue(int location, int data)
        {
            GL.Uniform1(location, data);
        }
        
        public void SetUniformValue(string attrib, ref Matrix4 data)
        {
            var loc = GetUniformLocation(attrib);
            if (loc < 0)
                throw new Exception();
            SetUniformValue(loc, ref data);
        }
        
        public void SetUniformValue(int location, ref Matrix4 data)
        {
            GL.UniformMatrix4(location, false, ref data);
        }

        public int GetUniformLocation(string uniform)
        {
            return GL.GetUniformLocation(Program, uniform);
        }

        private static int CreateShader(ShaderType shaderType, string path)
        {
            var shader = GL.CreateShader(shaderType);
            GL.ShaderSource(shader, ReadFile(path));
            CompileShader(shader);

            return shader;
        }
        
        private static void CompileShader(int shader)
        {
            GL.CompileShader(shader);
            
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int) All.True)
            {
                Console.WriteLine(GL.GetShaderInfoLog(shader));
                throw new Exception($"Error occurred whilst compiling Shader({shader})");
            }
        }

        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);
            
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int) All.True)
            {
                Console.WriteLine(GL.GetProgramInfoLog(program));
                throw new Exception($"Error occurred whilst linking Program({program})");
            }
        }

        private static string ReadFile(string file)
        {
            using var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sr = new StreamReader(fs, Encoding.UTF8);
            
            return sr.ReadToEnd();
        }

        public void Dispose()
        {
            if (!_disposed)
                GL.DeleteProgram(Program);
            
            _disposed = true;
        }
    }
}
