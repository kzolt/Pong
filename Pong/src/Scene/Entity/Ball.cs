using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;

namespace Pong
{
	class Ball : Entity
	{
		public Ball(Vector3 position, Vector2 size, Vector4 color)
		{
			Position = position;
			Size = size;
			Color = color;
		}

		void OnUpdate(Paddle paddle)
		{

		}

	}
}
