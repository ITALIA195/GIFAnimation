using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace RecreatingGIF.Graphics.Objects
{
    public class Rectangle
    {
        private readonly Shader _shader;
        private readonly Buffers _buffers;
        
        public Rectangle(Shader shader)
        {
            _shader = shader;
            _buffers = new Buffers(Buffer.VertexBuffer | Buffer.VertexArray);
            
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
            
            GL.BindVertexArray(_buffers.VertexArray);
            
            _shader.Use();
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffers.VertexBuffer);
        }

        private float anim;
        public void Draw()
        {
            _shader.Use();

            anim += 0.05f;
            
            var projection = Matrix4.Identity;
            var view = Matrix4.Identity;
            var model = Matrix4.Identity;

            const float scale = 0.05f;
            model *= Matrix4.CreateScale(scale, scale, scale);

            view *= Matrix4.CreateScale(1f / Window.AspectRatio, 1, 1);
            view *= Matrix4.CreateRotationX(MathHelper.PiOver6);
            view *= Matrix4.CreateRotationY(-MathHelper.PiOver4);

//            projection *= Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, Window.AspectRatio, 1f, 50f);

            _shader.SetUniformValue("animation", anim);
            _shader.SetUniformValue("centerOfAnimation", new Vector3(0, 0, 0));
            _shader.SetUniformValue("projection", ref projection);
            _shader.SetUniformValue("view", ref view);
            _shader.SetUniformValue("model", ref model);
            
            GL.BindVertexArray(_buffers.VertexArray);
            GL.DrawArrays(PrimitiveType.Points, 0, 3);
        }
    }
}