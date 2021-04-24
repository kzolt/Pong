using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace Pong
{
	class Scene
	{
		Paddle m_Paddle;
		//Ball m_Ball;
		//List<Brick> m_Bricks;

		Camera m_Camera;

		public Scene()
		{
			m_Camera = new Camera(Matrix4.CreateOrthographicOffCenter(-16.0f, 16.0f, -9.0f, 9.0f, -1.0f, 1.0f));
			
			m_Paddle = new Paddle(new Vector3(0.0f, -7.0f, 0.0f), new Vector2(5.0f, 1.0f), new Vector4(1.0f));
			//m_Ball = new Ball(new Vector3(0.0f, -1.0f, 0.0f), new Vector2(1.0f, 1.0f), new Vector4(1.0f));

		}

		~Scene()
		{

		}

		public void OnUpdate(GameWindow window)
		{
			// Paddle Movement
			m_Paddle.OnUpdate(window);


		}

		public void OnRender()
		{
			Renderer.Clear();

			Renderer.BeginScene(m_Camera.GetViewProjection());
			Renderer.DrawQuad(m_Paddle.GetPosition(), m_Paddle.GetSize(), m_Paddle.GetColor());


			Renderer.EndScene();
		}
	}
}
