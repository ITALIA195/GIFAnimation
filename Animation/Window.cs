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
            1280, 720,
            new GraphicsMode(new ColorFormat(24), 48, 8, 8),
            "GIF Animation",
            GameWindowFlags.Default,
            DisplayDevice.Default,
            4, 3,
            GraphicsContextFlags.Default)
        {
            VSync = VSyncMode.On;

            Load += OnLoad;
            UpdateFrame += OnUpdateFrame;
        }

        public static float AspectRatio => 1280f / 720f;

        private void OnLoad(object sender, EventArgs e)
        {
            GL.ClearColor(0.98f, 0.98f, 0.98f, 1f);
            GL.Enable(EnableCap.DepthTest);

            _rectangles = new Rectangles(new Shader("main"));
            this.RenderFrame += _rectangles.Render;
            this.UpdateFrame += _rectangles.Update;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            base.OnRenderFrame(e);
            
            GL.Flush();
            SwapBuffers();
        }

        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard[Key.Escape] || keyboard[Key.Q] || keyboard[Key.LAlt] && keyboard[Key.F4])
                Exit();
        }
    }
}
