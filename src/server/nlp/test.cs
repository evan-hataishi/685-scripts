/*
 * Created by SharpDevelop.
 * User: Deeps
 * Date: 2019/12/07
 * Time: 2:29 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace HelloWorld
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World the day has come!");
			
			System.Diagnostics.Process.Start("C:\\Users\\lava\\Documents\\Moseli\\hello.bat");
			Console.ReadKey(true);
		}
	}
}