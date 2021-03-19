using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL4;

namespace Pong
{
	class VertexArray
	{
		int m_RendererID;
		int m_VertexBufferIndex;

		List<VertexBuffer> m_VertexBuffers;
		IndexBuffer m_IndexBuffer;

		public VertexArray()
		{
			GL.CreateVertexArrays(1, out m_RendererID);

			m_VertexBuffers = new List<VertexBuffer>();
		}

		~VertexArray()
		{
			GL.DeleteVertexArrays(1, ref m_RendererID);
		}

		public List<VertexBuffer> GetVertexBuffers() { return m_VertexBuffers; }
		public IndexBuffer GetIndexBuffer() { return m_IndexBuffer; }

		public void Bind()
		{
			GL.BindVertexArray(m_RendererID);
		}

		public void AddVertexBuffer(VertexBuffer vbo)
		{
			Bind();
			vbo.Bind();

			var elements = vbo.Layout.Elements();
			for (int i = 0; i < elements.Count; i++)
			{
				var glBaseType = ShaderDataTypeToGLType(elements[i].Type);
				GL.EnableVertexAttribArray(m_VertexBufferIndex);
				GL.VertexAttribPointer(m_VertexBufferIndex, elements[i].GetComponentCount(), glBaseType, elements[i].Normalized, vbo.Layout.GetStride(), (IntPtr)elements[i].Offset);
				m_VertexBufferIndex++;
			}

			m_VertexBuffers.Add(vbo);
		}

		static VertexAttribPointerType ShaderDataTypeToGLType(ShaderDataType type)
		{
			switch (type)
			{
				case ShaderDataType.Float: return VertexAttribPointerType.Float;
				case ShaderDataType.Float2: return VertexAttribPointerType.Float;
				case ShaderDataType.Float3: return VertexAttribPointerType.Float;
				case ShaderDataType.Float4: return VertexAttribPointerType.Float;
				case ShaderDataType.Mat3: return VertexAttribPointerType.Float;
				case ShaderDataType.Mat4: return VertexAttribPointerType.Float;
			}

			Console.WriteLine("Invalid Shader Data Type!");
			return 0;
		}

		public void SetIndexBuffer(IndexBuffer ibo)
		{
			m_IndexBuffer = ibo;
		}
	}
}
