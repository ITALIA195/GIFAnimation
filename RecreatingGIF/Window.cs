using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using RecreatingGIF.Graphics;
using RecreatingGIF.Graphics.Objects;

namespace RecreatingGIF
{
    public class Window : GameWindow
    {
        private Rectangle _rectangle;
        private float _angle;
        
        public Window() : base(
            1280,
            720,
            new GraphicsMode(
                new ColorFormat(24),
                48,
                8,
                8),
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
            GL.ClearColor(0.98f, 0.98f, 0.98f, 1f);
            GL.Enable(EnableCap.DepthTest);

            _rectangle = new Rectangle(new Shader("main"));
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Key.Escape))
                Exit();
            if ((keyboard.IsKeyDown(Key.AltLeft) ||
                keyboard.IsKeyDown(Key.AltRight)) && keyboard.IsKeyDown(Key.F4))
                Exit();

            if (keyboard.IsKeyDown(Key.Q))
                Exit();

            _angle += 0.1f;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            _rectangle.Draw();
            
            GL.Flush();
            SwapBuffers();
        }

        public static float AspectRatio => 1280f / 720f;
    }
}