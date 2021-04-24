using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;

namespace Pong
{
	class Brick : Entity
	{
		public Brick(Vector3 position, Vector2 size, Vector4 color)
		{
			Position = position;
			Size = size;
			Color = color;
		}

		void OnUpdate(Ball ball)
		{

		}
	}
}
