using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;

namespace Pong
{
	abstract class Entity
	{
		protected Vector3 Position;
		protected Vector2 Size;
		protected Vector4 Color;

		public Vector3 GetPosition() { return Position; }
		public Vector2 GetSize() { return Size; }
		public Vector4 GetColor() { return Color; }
	}
}
