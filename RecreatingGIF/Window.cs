using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using RecreatingGIF.Graphics;

namespace RecreatingGIF
{
    public class Window : GameWindow
    {
        private Rectangle _rectangle;
        private float _angle;
        private double _multiplier;
        
        public Window() : base(
            1280,
            720,
            GraphicsMode.Default,
            "GIF Animation",
            GameWindowFlags.Default,
            DisplayDevice.Default,
            3,
            3,
            GraphicsContextFlags.Default)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color4.Red);
            
            _rectangle = new Rectangle(new Shader("main"));
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            _angle += 0.1f;
            _multiplier = Math.Sin(_angle);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            _rectangle.Draw();
            
            GL.Flush();
            SwapBuffers();
        }
    }
}