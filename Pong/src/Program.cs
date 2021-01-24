using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace Pong
{
	class Program
	{
		static void Main(string[] args)
		{
			var nativeWindowSettings = new NativeWindowSettings()
			{
				Size = new Vector2i(1280, 720),
				Title = "Pong"
			};

			var app = new Application(GameWindowSettings.Default, nativeWindowSettings);
			app.Run();
		}
	}
}
