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
		static RendererData s_Data;

		public static void Init()
		{
			s_Data.QuadIndexCount = 0;
			s_Data.QuadVertexBufferIndex = 0;

			s_Data.QuadVertexArray = new VertexArray();
			s_Data.QuadVertexBuffer = new VertexBuffer(RendererData.MaxVertices * QuadVertex.SizeInBytes());
			s_Data.QuadVertexBuffer.SetLayout(new BufferLayout(new List<BufferElement> {
				new BufferElement(ShaderDataType.Float3, "a_Position"),
				new BufferElement(ShaderDataType.Float4, "a_Color")
			}));
			s_Data.QuadVertexArray.AddVertexBuffer(s_Data.QuadVertexBuffer);
			s_Data.QuadVertexBufferBase = new QuadVertex[4];

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

			IndexBuffer quadIB = new IndexBuffer(quadIndices, quadIndices.Length);
			s_Data.QuadVertexArray.SetIndexBuffer(quadIB);

			s_Data.Shader = new Shader("res/shaders/basic.shader");

			s_Data.QuadVertexPosition = new Vector3[4];
			s_Data.QuadVertexPosition[0] = new Vector3(-0.5f, -0.5f, 0.0f);
			s_Data.QuadVertexPosition[1] = new Vector3( 0.5f, -0.5f, 0.0f);
			s_Data.QuadVertexPosition[2] = new Vector3( 0.5f,  0.5f, 0.0f);
			s_Data.QuadVertexPosition[3] = new Vector3(-0.5f,  0.5f, 0.0f);
		}

		public static void BeginScene(Matrix4 viewProj)
		{
			s_Data.CameraViewProj = viewProj;

			s_Data.Shader.Bind();
			s_Data.Shader.UploadMatrix4("u_ViewProjection", viewProj);

			s_Data.QuadIndexCount = 0;
		}

		public static void EndScene()
		{
			int dataSize = s_Data.QuadVertexBufferIndex * QuadVertex.SizeInBytes();
			s_Data.QuadVertexArray.Bind();
			s_Data.QuadVertexBuffer.SetData(s_Data.QuadVertexBufferBase, dataSize);

			GL.DrawElements(PrimitiveType.Triangles, s_Data.QuadIndexCount, DrawElementsType.UnsignedInt, IntPtr.Zero);
		}

		public static void Flush()
		{
			EndScene();


		}

		public static void Clear()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
		}

		public static void DrawQuad(Vector3 position, Vector2 size, Vector4 color)
		{
			const int quadVertexCount = 4;

			Matrix4 transform = Matrix4.CreateTranslation(position) * Matrix4.CreateScale(new Vector3(size.X, size.Y, 1.0f));

			for (int i = 0; i < quadVertexCount; i++)
			{
				s_Data.QuadVertexBufferBase[i].Position = Vector3.TransformPosition(s_Data.QuadVertexPosition[i], transform);
				s_Data.QuadVertexBufferBase[i].Color = color;
				s_Data.QuadVertexBufferIndex++;
			}

			s_Data.QuadIndexCount += 6;
		}
	}
}
