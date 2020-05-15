using System;
using Animation.Graphics;
using Animation.Graphics.Objects;
using OpenToolkit.Graphics.OpenGL4;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Common.Input;
using OpenToolkit.Windowing.Desktop;

namespace Animation
{
    public class Window : GameWindow
    {
        private const int BaseWidth = 1280;
        private const int BaseHeight = 720;
        private Rectangles _rectangles;

        public Window() : base(GameWindowSettings.Default, WindowSettings)
        {
            Load += OnInitialized;
            UpdateFrame += OnUpdate;
        }

        private static NativeWindowSettings WindowSettings =>
            new NativeWindowSettings
            {
                Title = "GIF Animation",
                Size = new Vector2i(BaseWidth, BaseHeight),
                APIVersion = new Version(4, 6),
                Profile = ContextProfile.Core
            };

        public static float AspectRatio => 1280f / 720f;

        private void OnInitialized()
        {
            GL.ClearColor(0.98f, 0.98f, 0.98f, 1f);
            GL.Enable(EnableCap.DepthTest);

            _rectangles = new Rectangles(new Shader("main"));
            RenderFrame += _rectangles.Render;
            UpdateFrame += _rectangles.Update;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            base.OnRenderFrame(e);
            
            GL.Flush();
            SwapBuffers();
        }

        private void OnUpdate(FrameEventArgs e)
        {
            if (
                KeyboardState[Key.Escape] ||
                KeyboardState[Key.Q] ||
                KeyboardState[Key.LAlt] && KeyboardState[Key.F4]
            )
            {
                Close();
            }
        }
    }
}
