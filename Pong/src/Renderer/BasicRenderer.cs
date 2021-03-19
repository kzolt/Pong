using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Pong
{
	class BasicRenderer
	{
		
		public BasicRenderer()
		{
		}

		public void BeginScene(Camera camera, Shader shader)
		{
			shader.Bind();
			shader.UploadMatrix4("u_ViewProjection", camera.GetViewProjection());
		}

		public void Submit(VertexArray vao)
		{
			vao.Bind();
			GL.DrawElements(PrimitiveType.Triangles, vao.GetIndexBuffer().GetCount(), DrawElementsType.UnsignedInt, IntPtr.Zero);
		}

		public void EndScene()
		{

		}

		public void Flush()
		{

		}
		
		public void Clear()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
		}

	}
}
