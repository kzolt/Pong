using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

using OpenTK.Mathematics;

namespace Pong
{
	class Application : GameWindow
	{
		
		float[] squareVerticies =
		{
			-2.0f, -2.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f,
			 2.0f, -2.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
			 2.0f,  2.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f,
			-2.0f,  2.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f
		};

		Camera m_Camera;

		public Application(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
			: base(gameWindowSettings, nativeWindowSettings)
		{
			m_Camera = new Camera(Matrix4.CreateOrthographicOffCenter(-16.0f, 16.0f, -9.0f, 9.0f, -1.0f, 1.0f));
		}

		protected override void OnLoad()
		{
			Renderer.Init();

			base.OnLoad();
		}

		protected override void OnUnload()
		{
			base.OnUnload();
		}

		protected override void OnUpdateFrame(FrameEventArgs args)
		{
			if (KeyboardState.IsKeyDown(Keys.Escape))
			{
				Close();
			}

			base.OnUpdateFrame(args);
		}

		protected override void OnRenderFrame(FrameEventArgs args)
		{
			Renderer.Clear();

			Renderer.BeginScene(m_Camera.GetViewProjection());
			Renderer.DrawQuad(new Vector3(0.0f), new Vector2(5.0f, 5.0f), new Vector4(0.8f, 0.2f, 0.3f, 1.0f));
			Renderer.EndScene();

			SwapBuffers();

			base.OnRenderFrame(args);
		}

		protected override void OnResize(ResizeEventArgs e)
		{
			GL.Viewport(0, 0, Size.X, Size.Y);
			base.OnResize(e);
		}
	}
}
