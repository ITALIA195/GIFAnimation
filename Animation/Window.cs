using System;
using Animation.Graphics;
using Animation.Graphics.Objects;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Animation
{
    public class Window : GameWindow
    {
        private Rectangles _rectangles;
        
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
            4,
            3,
            GraphicsContextFlags.Default)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.98f, 0.98f, 0.98f, 1f);
            GL.Enable(EnableCap.DepthTest);

            _rectangles = new Rectangles(new Shader("main"));
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard[Key.Escape] || keyboard[Key.Q] || keyboard[Key.LAlt] && keyboard[Key.F4])
                Exit();

            _rectangles.Update();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            _rectangles.Draw();
            
            GL.Flush();
            SwapBuffers();
        }

        public static float AspectRatio => 1280f / 720f;
    }
}
