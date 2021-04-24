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

		Scene m_Scene;

		public Application(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
			: base(gameWindowSettings, nativeWindowSettings)
		{
			m_Scene = new Scene();
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
			m_Scene.OnUpdate(this);
			
			base.OnUpdateFrame(args);
		}

		protected override void OnRenderFrame(FrameEventArgs args)
		{
			m_Scene.OnRender();

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
