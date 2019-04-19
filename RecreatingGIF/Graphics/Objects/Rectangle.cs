using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace RecreatingGIF.Graphics.Objects
{
    public class Rectangle
    {
        private const int NumberOfBoxes = 15;
        private readonly Shader _shader;
        private readonly Buffers _buffers;

        private readonly float[] _vertices = new float[2 * NumberOfBoxes * NumberOfBoxes];
        private int _shaderTimeLocation;
        private float _time;

        public Rectangle(Shader shader)
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
            for (int x = -NumberOfBoxes; x < NumberOfBoxes; x += 2)
            {
                for (int y = -NumberOfBoxes; y < NumberOfBoxes; y += 2)
                {
                    _vertices[i + 0] = x;
                    _vertices[i + 1] = y;
                    i += 2;
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
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
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
            GL.DrawArrays(PrimitiveType.Points, 0, NumberOfBoxes * NumberOfBoxes);
        }

        public void Update()
        {
            _shader.SetUniformValue("animation", _time);
            
            _time = (_time + 0.005f) % 2;
        }
    }
}