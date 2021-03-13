using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Mathematics;

namespace Pong
{
	class Camera
	{
		public Matrix4 ProjectionMatrix;
		public Matrix4 ViewMatrix;

		public Vector3 Position;

		public Camera(Matrix4 projectionMatrix)
		{
			ProjectionMatrix = projectionMatrix;
			Position = new Vector3(0.0f);

			RecalculateViewMatrix();
		}

		~Camera()
		{

		}

		void RecalculateViewMatrix()
		{
			Matrix4 translate = Matrix4.CreateTranslation(Position);
			ViewMatrix = translate.Inverted();
		}

		public Matrix4 GetViewProjection()
		{
			return ViewMatrix * ProjectionMatrix;
		}
	}
}
