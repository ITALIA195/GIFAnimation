using System;
using OpenToolkit.Graphics.OpenGL4;

namespace Animation.Graphics
{
    public class Buffers
    {
        private readonly int _vertexBuffer;
        private readonly int _elementBuffer;
        private readonly int _colorBuffer;
        private readonly int _textureBuffer;
        private readonly int _vertexArray;

        public Buffers(Buffer buffer)
        {
            if ((buffer & Buffer.VBO) != 0)
                _vertexBuffer = GL.GenBuffer();
            if ((buffer & Buffer.EBO) != 0)
                _elementBuffer = GL.GenBuffer();
            if ((buffer & Buffer.CBO) != 0)
                _colorBuffer = GL.GenBuffer();
            if ((buffer & Buffer.TBO) != 0)
                _textureBuffer = GL.GenBuffer();
            if ((buffer & Buffer.VAO) != 0)
                _vertexArray = GL.GenVertexArray();
        }
        
        public int VertexBuffer => _vertexBuffer;
        public int ColorBuffer => _colorBuffer;
        public int ElementBuffer => _elementBuffer;
        public int TextureBuffer => _textureBuffer;
        public int VertexArray => _vertexArray;
    }

    [Flags]
    public enum Buffer
    {
        VertexBuffer = 1 << 1,
        ElementBuffer = 1 << 2,
        ColorBuffer = 1 << 3,
        TextureBuffer = 1 << 4,
        VertexArray = 1 << 5,

        VBO = VertexBuffer,
        EBO = ElementBuffer,
        CBO = ColorBuffer,
        TBO = TextureBuffer,
        VAO = VertexArray,

        VertexElement = VBO | EBO,
        VertElementsCol = VBO | EBO | CBO
}
}
