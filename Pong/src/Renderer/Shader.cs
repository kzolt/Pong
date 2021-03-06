﻿using System;
using System.Collections.Generic;
using System.IO;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Pong
{
	enum ShaderSourceType
	{
		Vertex = 0, Fragment = 1
	};

	class Shader
	{
		int m_RendererID = 0;
		Dictionary<string, int> m_UniformLocations = new Dictionary<string, int>();

		public Shader(string filepath)
		{
			string[] source = File.ReadAllLines(filepath);
			if (source.Length != 0)
			{
				var shaderSources = PreProcess(source);
				Compile(shaderSources);
			}
		}

		~Shader()
		{
			GL.DeleteProgram(m_RendererID);
		}

		public void Bind()
		{
			GL.UseProgram(m_RendererID);
		}

		public void UploadUniform(string name, float value)
		{
			GL.Uniform1(GetUniformLocation(name), value);
		}

		public void UploadUniform2(string name, Vector2 value)
		{
			GL.Uniform2(GetUniformLocation(name), value);
		}

		public void UploadUniform3(string name, Vector3 value)
		{
			GL.Uniform3(GetUniformLocation(name), value);
		}

		public void UploadUniform4(string name, Vector4 value)
		{
			GL.Uniform4(GetUniformLocation(name), value);
		}

		public void UploadMatrix4(string name, Matrix4 matrix)
		{
			GL.UniformMatrix4(GetUniformLocation(name), false, ref matrix);
		}

		Dictionary<int, string> PreProcess(string[] source)
		{
			Dictionary<int, string> shaderSources = new Dictionary<int, string>();
			string type = "";
			string shader = "";

			string tokenType = "#type ";
			for (int i = 0; i < source.Length; i++)
			{
				if (source[i].Length != 0 && source[i].Length != 1)
				{
					if (source[i].Substring(0, tokenType.Length) == tokenType)
					{
						if (i != 0)
						{
							shaderSources.Add((int)ShaderTypeFromString(type), shader);
							shader = "";
						}

						int length = source[i].Length - tokenType.Length;
						type = source[i].Substring(tokenType.Length, length);
						
						continue;
					}
				}

				shader += source[i] + "\r\n";

				if (i + 1 == source.Length)
					shaderSources.Add((int)ShaderTypeFromString(type), shader);
			}

			return shaderSources;
		}

		void Compile(Dictionary<int, string> shaderSources)
		{
			int program = GL.CreateProgram();

			int[] glShaderIDs = new int[2];
			int glShaderIDIndex = 0;

			foreach (var kv in shaderSources)
			{
				ShaderType type = (ShaderType)kv.Key;
				string source = kv.Value;

				int shader = GL.CreateShader(type);
				GL.ShaderSource(shader, source);
				GL.CompileShader(shader);

				GL.AttachShader(program, shader);
				glShaderIDs[glShaderIDIndex++] = shader;
			}

			GL.LinkProgram(program);

			foreach (int id in glShaderIDs)
				GL.DeleteShader(id);

			m_RendererID = program;
		}
		
		int GetUniformLocation(string name)
		{
			if (!m_UniformLocations.ContainsKey(name))
				m_UniformLocations[name] = GL.GetUniformLocation(m_RendererID, name);

			return m_UniformLocations[name];
		}

		static ShaderType ShaderTypeFromString(string type)
		{
			if (type == "vertex")
				return ShaderType.VertexShader;
			if (type == "fragment")
				return ShaderType.FragmentShader;

			Console.WriteLine("Invalid Shader Type!");
			return 0;
		}

		
	}
}
