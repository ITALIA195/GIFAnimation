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
            _buffers = new Buffers(Buffer.VBO | Buffer.EBO | Buffer.CBO | Buffer.VAO);
            
            BindData();
            EnableAttribArrays();
        }

        private void BindData()
        {
            // =================
            // == Create Data ==
            // =================
            
            var vertices = new[]
            {
                //X   Y    Z
                +1f, +1f, +1f, // 0
                +1f, -1f, +1f, // 1
                -1f, -1f, +1f, // 2
                -1f, +1f, +1f, // 3
                -1f, +1f, -1f, // 4
                -1f, -1f, -1f, // 5
                +1f, -1f, -1f, // 6
                +1f, +1f, -1f  // 7
            };

            var indices = new uint[]
            {
                // Top face
                0, 3, 4, 7,
                
                // Bottom face
                1, 2, 5, 6,                
                
                // Front face
                0, 1, 2, 3,
                
                // Right face
                0, 1, 6, 7,

                // Back face
                4, 5, 6, 7,
                
                // Left face
                2, 3, 4, 5
            };

            var colors = new[]
            {
                // Top Color RGB(137, 186, 183)
                137/255f, 186/255f, 183/255f,
                
                // Bottom Color RGB(137, 186, 183)
                137/255f, 186/255f, 183/255f,
                
                // Front Color RGB(65, 84, 132)
                65/255f, 84/255f, 132/255f,
                
                // Back Color RGB(65, 84, 132)
                65/255f, 84/255f, 132/255f,
                
                // Left Color RGB(230, 226, 177)
                230/255f, 226/255f, 177/255f,
                
                // Right Color RGB(230, 226, 177)
                230/255f, 226/255f, 177/255f
            };
            
            // ==================
            // == Bind Buffers ==
            // ==================
            
            // VBO (Vertices)
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffers.VertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            
            // EBO (Indices)
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _buffers.ElementBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            
            // CBO (Colors)
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffers.ColorBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, colors.Length * sizeof(float), colors, BufferUsageHint.StaticDraw);
            
            // ================
            // == Create VAO ==
            // ================
            
            GL.BindVertexArray(_buffers.VertexArray);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffers.VertexBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _buffers.ElementBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffers.ColorBuffer);
        }

        private void EnableAttribArrays()
        {
            _shader.Use();
            
            // Vertex Attrib
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            
            // Color Attrib
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);
        }

        public void Draw()
        {
            _shader.Use();
                
            GL.BindVertexArray(_buffers.VertexArray);
            GL.DrawElements(PrimitiveType.Quads, 4 * 6, DrawElementsType.UnsignedInt, 0);
        }
    }
}