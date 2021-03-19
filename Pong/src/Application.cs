using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

namespace Pong
{
	class Application : GameWindow
	{
		//float[] m_Verticies =
		//{
		//	-0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f,
		//	 0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
		//	 0.0f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f
		//};

		float[] m_Verticies =
		{
			-5.0f, -5.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f,
			 5.0f, -5.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
			 0.0f,  5.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f
		};

		VertexBuffer m_VBO;
		VertexArray m_VAO;

		Shader m_Shader;
		Renderer m_Renderer;
		Camera m_Camera;

		public Application(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
			: base(gameWindowSettings, nativeWindowSettings)
		{
			m_Renderer = new Renderer();
			m_Camera = new Camera(OpenTK.Mathematics.Matrix4.CreateOrthographicOffCenter(-16.0f, 16.0f, -9.0f, 9.0f, -1.0f, 1.0f));
		}

		protected override void OnLoad()
		{
			m_VAO = new VertexArray();
			m_VBO = new VertexBuffer(m_Verticies, m_Verticies.Length * sizeof(float));
			m_VBO.SetLayout(new BufferLayout(new List<BufferElement> { 
				new BufferElement(ShaderDataType.Float3, "a_Position"),
				new BufferElement(ShaderDataType.Float4, "a_Color")
			}));

			m_VAO.AddVertexBuffer(m_VBO);

			m_Shader = new Shader("res/shaders/basic.shader");
			m_Shader.Bind();

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
			m_Renderer.Clear();

			m_Shader.Bind();
			m_Shader.UploadMatrix4("u_ViewProjection", m_Camera.GetViewProjection());
			m_VAO.Bind();

			GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

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
