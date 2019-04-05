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
		public class ant
		{
		public int posX;
		public int posY;
		public int limitX { get; set; }
		public int limitY { get; set; }
		private static readonly Random random = new Random(); 		//needs to be static, otherwise it won't generate different numbers for each ant
		private static readonly object syncLock = new object();
		
		private static int RandomNumber(int min, int max)
		{
		    lock(syncLock) // here it locks out other threads to use random at the same time (so it won't generate same numbers)
		    { 
		        return random.Next(min, max);
		    }
		}
		
		public void Move()
		{
			Console.SetCursorPosition(posX, posY); // cleans up after itself
			Console.WriteLine(" ");
			
			int stepX = RandomNumber(-1,2);
			int stepY = RandomNumber(-1,2);
			
			posX = posX + stepX;
			posY = posY + stepY;
			
			if (posX < 0) {
    			posX += 1;
    		}
    		if (posX >= limitX) {
    			posX -= 1;
    		}
    		if (posY < 0) {
    			posY += 1;
    		}
    		if (posY >= limitY) {
    			posY -= 1;
    		}
			
			Console.SetCursorPosition(posX, posY);
			Console.WriteLine("A");
		}
		
		public void Spawn()
		{
			posX = RandomNumber(0,limitX);
			posY = RandomNumber(0,limitY);
		}
		
		public void Show()
		{
			Console.SetCursorPosition(posX, posY);
			Console.WriteLine("A");
		}
		
		public void SeekFood(food futter)
		{
			Console.SetCursorPosition(posX, posY); // cleans up after itself
			Console.WriteLine(" ");
			
			if (posX != futter.posX || posY != futter.posY) 
			{
				int whereIsMyFoodX = futter.posX - posX;
				int whereIsMyFoodY = futter.posY - posY;
				
				if (whereIsMyFoodY > 0) 
				{
					posY += 1;
				}
				
				if (whereIsMyFoodX < 0) 
				{
					posX -= 1;
				}
				
				if (whereIsMyFoodY < 0) 
				{
					posY -= 1;
				}
				
				if (whereIsMyFoodX > 0) 
				{
					posX += 1;
				}
			}
			
			Move();
			Console.SetCursorPosition(posX, posY);
			Console.WriteLine("A");
		}
		
	}
		
		
	public class food : ant
	{
		new public void Show()
		{
			Console.SetCursorPosition(posX, posY);
			Console.WriteLine("F");
			Console.SetCursorPosition(posX+1, posY);
			Console.WriteLine("F");
			Console.SetCursorPosition(posX, posY+1);
			Console.WriteLine("F");
			Console.SetCursorPosition(posX+1, posY+1);
			Console.WriteLine("F");
		}
	}
	
	public class RandomNumb
	{

	}
	
	
	class Program
	{
		
		public static void Main(string[] args)
		{
			int borderX = 120;
			int borderY = 40;
			Console.SetWindowSize(borderX+10, borderY+10);
			ant[] ameisen = new ant[20];
			food essen = new food(){limitX = borderX, limitY = borderY};
			
			

			
			for (int i = 0; i < ameisen.Length; i++) 
			{
				ameisen[i] = new ant(){limitX = borderX, limitY = borderY};
				ameisen[i].Spawn();
			}
			
			essen.Spawn();
			
			
	        while (true) 
	        {
	    		
	        	foreach (ant ameise in ameisen) 
	        	{
	        		ameise.SeekFood(essen);
	        		
	        	}
	        	essen.Show();

	        	System.Threading.Thread.Sleep(200);
	        }
	        
	        
	        //Console.ResetColor();
			
			
//			Here is the comment section for remembering stuff:
	        
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
		}
	}
}