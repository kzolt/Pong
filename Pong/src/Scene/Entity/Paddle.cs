using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

namespace Pong
{
	class Paddle : Entity
	{
		float m_PaddleSpeed = 0.1f;

		public Paddle(Vector3 position, Vector2 size, Vector4 color)
		{
			Position = position;
			Size = size;
			Color = color;
		}

		public void OnUpdate(GameWindow window)
		{
			if (window.KeyboardState.IsKeyDown(Keys.A))
				Position.X -= m_PaddleSpeed;

			if (window.KeyboardState.IsKeyDown(Keys.D))
				Position.X += m_PaddleSpeed;

			Console.WriteLine("X: " + Position.X);
			if (Position.X <= -2.7f)
				Position.X = -2.7f;
			if (Position.X >= 2.7f)
				Position.X = 2.7f;
		}
	}
}
