/*
 * Created by SharpDevelop.
 * User: kroll_lars
 * Date: 02.04.2019
 * Time: 09:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AntsSim
{
	class Program
	{
		
		public class ant
		{
			protected int posX;
			protected int posY;
			
			public void Move()
			{
				Console.SetCursorPosition(posX, posY);
				Console.WriteLine(" ");
				Random r = new Random();
				int stepX = r.Next(-1,2);
				int stepY = r.Next(-1,2);
				
				posX = posX + stepX;
				posY = posY + stepY;
				
				Console.SetCursorPosition(posX, posY);
			}
			
			public void Spawn()
			{
				Random r = new Random();
				posX = r.Next(0,80);
				posY = r.Next(0,25);
				
			}
			
			public int GetPosX()
			{
				return posX;
			}
			
			public int GetPosY()
			{
				return posY;
			}
			
			public void ShowAnt()
			{
				Console.WriteLine("A");
			}
			
		}
		public static void Main(string[] args)
		{
			int borderX = 80;
			int borderY = 25;
			
//			Console.SetWindowSize(borderX, borderY);
//	        Console.SetCursorPosition(10, 10);
//	        Console.WriteLine("A");
//	        System.Threading.Thread.Sleep(500);
//	        Console.Clear();
//	        //System.Threading.Thread.Sleep(500);
//	        Console.SetCursorPosition(11, 10);
//	        Console.WriteLine("A");
	        
//	        for (int i = 0; i < borderX; i++) 				// Move from left to right
//	        {
//		        Console.SetCursorPosition(i, 10);
//		        Console.WriteLine("A");
//		        System.Threading.Thread.Sleep(200);
//		        Console.Clear();
//	        }
			
			//ant Ameise01 = new ant();
			ant Ameise02 = new ant();
			//Ameise01.Spawn();
			Ameise02.Spawn();
			//Console.SetCursorPosition(Ameise01.GetPosX(), Ameise01.GetPosY());
			//Ameise01.ShowAnt();
			Console.SetCursorPosition(Ameise02.GetPosX(), Ameise02.GetPosY());
			Ameise02.ShowAnt();
			
			
	        
	        while (true) 
	        {
	        	//Ameise01.Move();
	        	Ameise02.Move();
	        	//Ameise01.ShowAnt();
	        	Ameise02.ShowAnt();
	        	System.Threading.Thread.Sleep(200);
	        }
	        
	        
	        //Console.ResetColor();
			
			
			// TODO: Implement Functionality Here
			
			//Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}