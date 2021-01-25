using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace Pong
{

	///////////////////////////////////////////////
	// Shader Types
	///////////////////////////////////////////////

	enum ShaderDataType
	{
		None = 0, Float, Float2, Float3, Float4, Mat3, Mat4
	}

	///////////////////////////////////////////////
	// Buffer Layouts And Elements
	///////////////////////////////////////////////
	class BufferElement
	{
		public string Name;
		public ShaderDataType Type;
		public int Size;
		public int Offset;
		public bool Normalized;

		public BufferElement(ShaderDataType type, string name, bool normalized = false)
		{
			Name = name;
			Type = type;
			Size = ShaderDataTypeSize(type);
			Offset = 0;
			Normalized = normalized;
		}
		static int ShaderDataTypeSize(ShaderDataType type)
		{
			switch (type)
			{
				case ShaderDataType.Float: return sizeof(float) * 1;
				case ShaderDataType.Float2: return sizeof(float) * 2;
				case ShaderDataType.Float3: return sizeof(float) * 3;
				case ShaderDataType.Float4: return sizeof(float) * 4;
				case ShaderDataType.Mat3: return sizeof(float) * 3 * 3;
				case ShaderDataType.Mat4: return sizeof(float) * 4 * 4;
			}

			Console.WriteLine("Invalid Shader Data Type!");
			return 0;
		}

		public int GetComponentCount()
		{
			switch(Type)
			{
				case ShaderDataType.Float:		return 1;
				case ShaderDataType.Float2:		return 2;
				case ShaderDataType.Float3:		return 3;
				case ShaderDataType.Float4:		return 4;
				case ShaderDataType.Mat3:		return 3 * 3;
				case ShaderDataType.Mat4:		return 4 * 4;
			}

			Console.WriteLine("Invalid Shader Data Type!");
			return 0;
		}
	}

	class BufferLayout
	{
		List<BufferElement> m_Elements;
		int m_Stride = 0;

		public BufferLayout(List<BufferElement> elements)
		{
			m_Elements = elements;

			CalculateOffsetsAndStride();
		}

		void CalculateOffsetsAndStride()
		{
			int offset = 0;
			m_Stride = 0;
			for (int i = 0; i < m_Elements.Count; i++)
			{

				m_Elements[i].Offset = offset;
				offset += m_Elements[i].Size;
				m_Stride += m_Elements[i].Size;
			}
		}

		public List<BufferElement> Elements() => m_Elements;
		public int GetStride()
		{
			return m_Stride;
		}
	}

	///////////////////////////////////////////////
	// Vertex Buffer
	///////////////////////////////////////////////

	class VertexBuffer
	{
		int m_RendererID;
		int m_Size;
		BufferLayout m_Layout;

		public VertexBuffer(float[] data, int size)
		{
			GL.CreateBuffers(1, out m_RendererID);
			GL.BindBuffer(BufferTarget.ArrayBuffer, m_RendererID);

			GL.NamedBufferData(m_RendererID, size, data, BufferUsageHint.DynamicDraw);
		}

		public VertexBuffer(int size)
		{
			GL.CreateBuffers(1, out m_RendererID);
			GL.BindBuffer(BufferTarget.ArrayBuffer, m_RendererID);
		}

		~VertexBuffer()
		{
			GL.DeleteBuffers(1, ref m_RendererID);
		}

		public void Bind()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, m_RendererID);
		}

		public void SetData(float[] data, int size)
		{
			m_Size = size;

			GL.NamedBufferSubData(m_RendererID, IntPtr.Zero, m_Size, data);
		}

		public void SetLayout(BufferLayout layout)
		{
			m_Layout = layout;
		}

		public BufferLayout Layout => m_Layout;

	}

	///////////////////////////////////////////////
	// Index Buffer
	///////////////////////////////////////////////
	
	class IndexBuffer
	{
		int m_RendererID;
		int m_Count;

		public IndexBuffer(int[] data, int count)
		{
			GL.CreateBuffers(1, out m_RendererID);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_RendererID);
			GL.NamedBufferData(m_RendererID, sizeof(uint) * count, data, BufferUsageHint.StaticDraw);
		}

		~IndexBuffer()
		{
			GL.DeleteBuffers(1, ref m_RendererID);
		}

		public void Bind()
		{
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_RendererID);
		}
	}

}
