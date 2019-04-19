using System;
using System.Drawing.Drawing2D;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace RecreatingGIF.Graphics.Objects
{
    public class Rectangles
    {
        private const int RectanglesPerSide = 15;
        private readonly Shader _shader;
        private readonly Buffers _buffers;

        private readonly Vector2[] _vertices = new Vector2[RectanglesPerSide * RectanglesPerSide];
        private int _shaderTimeLocation;
        private float _time;

        public Rectangles(Shader shader)
        {
            _shader = shader;
            _buffers = new Buffers(Buffer.VertexBuffer | Buffer.VertexArray);
            
            BindData();
            SendMatries();
        }

        private void BindData()
        {
            // =================
            // == Create Data ==
            // =================

            int i = 0;
            for (int x = -RectanglesPerSide; x < RectanglesPerSide; x += 2)
                for (int y = -RectanglesPerSide; y < RectanglesPerSide; y += 2)
                    _vertices[i++] = new Vector2(x, y);

            // ==================
            // == Bind Buffers ==
            // ==================
            
            // VBO (Vertices)
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffers.VertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * Vector2.SizeInBytes, _vertices, BufferUsageHint.StaticDraw);
            
            // ================
            // == Create VAO ==
            // ================
            
            _shader.Use();
            
            GL.BindVertexArray(_buffers.VertexArray);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);
            GL.EnableVertexAttribArray(0);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffers.VertexBuffer);
        }

        private void SendMatries()
        {
            _shader.Use();
            
            var projection = Matrix4.Identity;
            var view = Matrix4.Identity;
            var model = Matrix4.Identity;

            const float scale = 0.04f;
            model *= Matrix4.CreateScale(scale, scale, scale);

            view *= Matrix4.CreateScale(1f / Window.AspectRatio, 1, 1);
            view *= Matrix4.CreateRotationX(MathHelper.PiOver6);
            view *= Matrix4.CreateRotationY(-MathHelper.PiOver4);

//            projection *= Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, Window.AspectRatio, 1f, 50f);

            _shader.SetUniformValue("projection", ref projection);
            _shader.SetUniformValue("view", ref view);
            _shader.SetUniformValue("model", ref model);

            _shaderTimeLocation = _shader.GetUniformLocation("animation");
        }

        public void Draw()
        {
            GL.BindVertexArray(_buffers.VertexArray);
            GL.DrawArrays(PrimitiveType.Points, 0, RectanglesPerSide * RectanglesPerSide);
        }

        public void Update()
        {
            _shader.SetUniformValue("animation", _time);
            
            _time = (_time + 0.005f) % 2;
        }
    }
}