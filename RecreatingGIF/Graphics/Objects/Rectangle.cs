using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace RecreatingGIF.Graphics.Objects
{
    public class Rectangle
    {
        private const int NumberOfBoxes = 15;
        private readonly Shader _shader;
        private readonly Buffers _buffers;

        private readonly float[] _vertices = new float[3 * NumberOfBoxes * NumberOfBoxes];
        
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

            int i = 0;
            for (float x = -NumberOfBoxes / 2f; x < NumberOfBoxes / 2f; x++)
            {
                for (float y = -NumberOfBoxes / 2f; y < NumberOfBoxes / 2f; y++)
                {
                    _vertices[i + 0] = x * 2;
                    _vertices[i + 1] = 0;
                    _vertices[i + 2] = y * 2;
                    i += 3;
                }
            }
            
            // ==================
            // == Bind Buffers ==
            // ==================
            
            // VBO (Vertices)
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffers.VertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            
            // ================
            // == Create VAO ==
            // ================
            
            _shader.Use();
            
            GL.BindVertexArray(_buffers.VertexArray);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffers.VertexBuffer);
        }
        
        private float anim;
        public void Draw()
        {
            _shader.Use();

            anim += 0.03f;
            var projection = Matrix4.Identity;
            var view = Matrix4.Identity;
            var model = Matrix4.Identity;

            const float scale = 0.04f;
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
            GL.DrawArrays(PrimitiveType.Points, 0, NumberOfBoxes * NumberOfBoxes);
        }
    }
}