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
		
		float[] squareVerticies =
		{
			-2.0f, -2.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f,
			 2.0f, -2.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
			 2.0f,  2.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f,
			-2.0f,  2.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f
		};

		VertexBuffer m_VBO;
		VertexArray m_VAO;
		IndexBuffer m_IBO;

		Shader m_Shader;
		BasicRenderer m_Renderer;
		Camera m_Camera;

		public Application(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
			: base(gameWindowSettings, nativeWindowSettings)
		{
			m_Renderer = new BasicRenderer();
			m_Camera = new Camera(OpenTK.Mathematics.Matrix4.CreateOrthographicOffCenter(-16.0f, 16.0f, -9.0f, 9.0f, -1.0f, 1.0f));
		}

		protected override void OnLoad()
		{
			m_VAO = new VertexArray();
			m_VBO = new VertexBuffer(squareVerticies, squareVerticies.Length * sizeof(float));
			m_VBO.SetLayout(new BufferLayout(new List<BufferElement> { 
				new BufferElement(ShaderDataType.Float3, "a_Position"),
				new BufferElement(ShaderDataType.Float4, "a_Color")
			}));

			m_VAO.AddVertexBuffer(m_VBO);

			uint[] squareIndices = { 0, 1, 2, 2, 3, 0 };
			m_IBO = new IndexBuffer(squareIndices, squareIndices.Length);
			m_VAO.SetIndexBuffer(m_IBO);

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
			
			m_Renderer.BeginScene(m_Camera, m_Shader);
			m_Renderer.Submit(m_VAO);
			m_Renderer.EndScene();

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
