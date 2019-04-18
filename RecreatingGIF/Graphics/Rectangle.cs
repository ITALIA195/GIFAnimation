using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace RecreatingGIF.Graphics
{
    public class Rectangle
    {
        private readonly Shader _shader;
        private readonly Buffers _buffers;
        
        public Rectangle(Shader shader)
        {
            _shader = shader;
            _buffers = new Buffers(Buffer.VBO | Buffer.VAO);
            
            BindData();
        }

        private void BindData()
        {
            // =================
            // == Create Data ==
            // =================

            var vertices = new[]
            {
                0, 0, 0
            };
            
            // ==================
            // == Bind Buffers ==
            // ==================
            
            // VBO (Vertices)
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffers.VertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            
            // ================
            // == Create VAO ==
            // ================
            
            _shader.Use();
            
            GL.BindVertexArray(_buffers.VertexArray);
            
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffers.VertexBuffer);
        }

        public void Draw()
        {
            _shader.Use();

            var view = _shader.GetUniformLocation("view");
            Matrix4.CreateRotationX(MathHelper.DegreesToRadians(45f), out var m4);
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(45f), out var m3);
            var m2 = m4 * m3;
            _shader.SetUniformValue(view, ref m2);
            
            GL.BindVertexArray(_buffers.VertexArray);
            GL.DrawArrays(PrimitiveType.Points, 0, 3);
        }
    }
}