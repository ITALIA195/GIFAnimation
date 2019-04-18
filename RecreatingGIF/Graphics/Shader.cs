using System;
using System.IO;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace RecreatingGIF.Graphics
{
    public class Shader : IDisposable
    {
        private const string Path = @"./Graphics/Shaders/";
        private const string VertexFileFormat = ".vert";
        private const string FragmentFileFormat = ".frag";
        private const string GeometryFileFormat = ".geom";
        
        private readonly int _handle;
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

            _handle = GL.CreateProgram();
            
            GL.AttachShader(_handle, vertexShader);
            GL.AttachShader(_handle, fragmentShader);
            GL.AttachShader(_handle, geometryShader);
            
            LinkProgram(_handle);

            GL.DetachShader(_handle, vertexShader);
            GL.DetachShader(_handle, fragmentShader);
            GL.DetachShader(_handle, geometryShader);
            
            // =============
            // == Cleanup ==
            // =============
            
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(geometryShader);
        }

        public int Handle => _handle;
        public int Program => _handle;

        public void Use() => UseProgram();
        public void Bind() => UseProgram();
        public void UseProgram()
        {
            GL.UseProgram(_handle);
        }

        public int GetAttribLocation(string attrib)
        {
            return GL.GetAttribLocation(_handle, attrib);
        }

        public void SetUniformValue(int location, int data)
        {
            GL.Uniform1(location, data);
        }

        public void SetUniformValue(int location, ref Matrix4 data)
        {
            GL.UniformMatrix4(location, true, ref data);
        }

        public int GetUniformLocation(string uniform)
        {
            return GL.GetUniformLocation(_handle, uniform);
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
            
            Console.WriteLine(GL.GetShaderInfoLog(shader));
            
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int) All.True)
                throw new Exception($"Error occurred whilst compiling Shader({shader})");
        }

        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);
            
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int) All.True)
                throw new Exception($"Error occurred whilst linking Program({program})");
        }

        private static string ReadFile(string file)
        {
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
                return sr.ReadToEnd();
        }

        public void Dispose()
        {
            if (!_disposed)
                GL.DeleteProgram(_handle);
            
            _disposed = true;
        }
    }
}