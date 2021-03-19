using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Pong
{

	struct QuadVertex
	{
		public Vector3 Position;
		public Vector4 Color;

		public static int SizeInBytes()
		{
			return Vector3.SizeInBytes + Vector4.SizeInBytes;
		}
	}

	struct RendererData
	{
		public readonly static int MaxQuads = 10000;
		public readonly static int MaxVertices = MaxQuads * 4;
		public readonly static int MaxIndices = MaxQuads * 6;

		public Shader Shader;

		public VertexArray QuadVertexArray;
		public VertexBuffer QuadVertexBuffer;

		public int QuadIndexCount;
		public int QuadVertexBufferIndex;
		public QuadVertex[] QuadVertexBufferBase;

		public Vector3[] QuadVertexPosition;

		public Matrix4 CameraViewProj;
	}

	class Renderer
	{
		RendererData m_Data;

		public Renderer()
		{
			m_Data.QuadIndexCount = 0;
			m_Data.QuadVertexBufferIndex = 0;

			m_Data.QuadVertexArray = new VertexArray();
			m_Data.QuadVertexBuffer = new VertexBuffer(RendererData.MaxVertices * QuadVertex.SizeInBytes());
			m_Data.QuadVertexBuffer.SetLayout(new BufferLayout(new List<BufferElement> {
				new BufferElement(ShaderDataType.Float3, "a_Position"),
				new BufferElement(ShaderDataType.Float4, "a_Color")
			}));
			m_Data.QuadVertexArray.AddVertexBuffer(m_Data.QuadVertexBuffer);
			m_Data.QuadVertexBufferBase = new QuadVertex[RendererData.MaxVertices];

			uint[] quadIndices = new uint[RendererData.MaxIndices];

			uint offset = 0;
			for (int i = 0; i < RendererData.MaxIndices; i += 6)
			{
				quadIndices[i + 0] = offset + 0;
				quadIndices[i + 1] = offset + 1;
				quadIndices[i + 2] = offset + 2;

				quadIndices[i + 3] = offset + 2;
				quadIndices[i + 4] = offset + 3;
				quadIndices[i + 5] = offset + 0;

				offset += 4;
			}

			IndexBuffer quadIB = new IndexBuffer(quadIndices, RendererData.MaxIndices);
			m_Data.QuadVertexArray.SetIndexBuffer(quadIB);

			m_Data.Shader = new Shader("res/shaders/basic.shader");

			m_Data.QuadVertexPosition = new Vector3[4];
			m_Data.QuadVertexPosition[0] = new Vector3(-0.5f, -0.5f, 0.0f);
			m_Data.QuadVertexPosition[1] = new Vector3( 0.5f, -0.5f, 0.0f);
			m_Data.QuadVertexPosition[2] = new Vector3( 0.5f,  0.5f, 0.0f);
			m_Data.QuadVertexPosition[3] = new Vector3(-0.5f,  0.5f, 0.0f);
		}

		public void BeginScene(Matrix4 viewProj)
		{
			m_Data.CameraViewProj = viewProj;

			m_Data.Shader.Bind();
			m_Data.Shader.UploadMatrix4("u_ViewProjection", viewProj);

			m_Data.QuadIndexCount = 0;
		}

		public void EndScene()
		{

		}

		public void Clear()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
		}

		public void DrawQuad(Vector3 position, Vector2 size, Vector4 color)
		{
			const int quadVertexCount = 4;

			Matrix4 transform = Matrix4.CreateTranslation(position) * Matrix4.CreateScale(new Vector3(size.X, size.Y, 0.0f));

			for (int i = 0; i < quadVertexCount; i++)
			{
				m_Data.QuadVertexBufferBase[i].Position = m_Data.QuadVertexPosition[i];
				m_Data.QuadVertexBufferBase[i].Color = color;
				m_Data.QuadVertexBufferIndex++;
			}

			m_Data.QuadIndexCount += 6;
		}
	}
}
