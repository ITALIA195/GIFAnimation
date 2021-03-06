using Framework;
using Framework.Shaders;
using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;

namespace Animation.Graphics.Objects
{
    public class Rectangles
    {
        private const int RectanglesPerSide = 15;
        private readonly GLObjects _buffers = new GLObjects();
        private readonly Program _program;

        private readonly Vector2[] _vertices = new Vector2[RectanglesPerSide * RectanglesPerSide];
        private int _shaderTimeLocation;
        private float _time;

        public Rectangles(Program program)
        {
            _program = program;
            
            BindData();
            SendMatrices();
        }

        private void BindData()
        {
            // Create Data
            var i = 0;
            for (var x = -RectanglesPerSide; x < RectanglesPerSide; x += 2)
                for (var y = -RectanglesPerSide; y < RectanglesPerSide; y += 2)
                    _vertices[i++] = new Vector2(x, y);

            // Bind Buffers
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffers.VertexBuffer); // VBO (Vertices)
            GL.BufferData(BufferTarget.ArrayBuffer, 
                _vertices.Length * Vector2.SizeInBytes, _vertices, BufferUsageHint.StaticDraw);
            
            // Create VAO
            _program.Bind();
            
            GL.BindVertexArray(_buffers.VertexArray);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);
            GL.EnableVertexAttribArray(0);
        }

        private void SendMatrices()
        {
            _program.Bind();

            const float scale = 1 / 50f;
            var model = Matrix4.CreateScale(scale, scale, scale) *
                        Matrix4.CreateRotationY(-MathHelper.PiOver4) *
                        Matrix4.CreateRotationX(MathHelper.PiOver4);

            var view = Matrix4.CreateTranslation(0, 0, -2f);
            
            var projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, 
                Window.AspectRatio, 
                0.1f, 100f);
            
            _program.SetUniformValue("projection", ref projection);
            _program.SetUniformValue("view", ref view);
            _program.SetUniformValue("model", ref model);

            _shaderTimeLocation = _program.GetUniformLocation("animation");
        }

        public void Render(FrameEventArgs e)
        {
            GL.BindVertexArray(_buffers.VertexArray);
            GL.DrawArrays(PrimitiveType.Points, 0, RectanglesPerSide * RectanglesPerSide);
        }

        public void Update(FrameEventArgs e)
        {
            const float timeStep = 0.75f;
            _time = (_time + timeStep * (float)e.Time) % 2;

            _program.SetUniformValue(_shaderTimeLocation, _time);
        }
    }
}
