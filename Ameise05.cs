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
		public class AmeisenSimulation 
		{
			ant[] ameisen;
			food essen;
			home zuHause;
			
			public AmeisenSimulation(int grenzeX = 120, int grenzeY = 40, int ameisenAnzahl = 20)
			{
				ameisen = new ant[ameisenAnzahl];
				essen = new food(grenzeX, grenzeY);
				zuHause = new home(grenzeX, grenzeY);
				Console.SetWindowSize(grenzeX+10, grenzeY+10);
				
				for (int i = 0; i < ameisen.Length; i++) 
				{
					ameisen[i] = new ant(grenzeX, grenzeY);
				}
				
				while (true) 
		        {
		    		
		        	foreach (ant ameise in ameisen) 
		        	{
		        		ameise.Seek(essen, zuHause);	
		        	}
		        	
		        	if (essen.foodLeft > 0) 
		        	{
		        		essen.Draw("F");
		        	}
		        	else
		        	{
		        		essen = new food(grenzeX, grenzeY);
		        	}
		        	
		        	zuHause.Draw("H");
		        	Console.SetCursorPosition(0, grenzeY+5);
		        	Console.WriteLine("food: " + zuHause.foodCount);
		        	System.Threading.Thread.Sleep(100);
		        }
			}
		}
		public class BildschirmObjekt 
		{
			public int posX;
			public int posY;
			public int limitX { get; set; }
			public int limitY { get; set; }
			
			public void Draw(string Objekt)
			{
				if (Objekt == "H") 
				{
					Console.SetCursorPosition(posX, posY);
					Console.WriteLine("H");
					Console.SetCursorPosition(posX+1, posY);
					Console.WriteLine("H");
					Console.SetCursorPosition(posX, posY+1);
					Console.WriteLine("H");
					Console.SetCursorPosition(posX+1, posY+1);
					Console.WriteLine("H");
				} 
				
				else
				{
				Console.SetCursorPosition(posX, posY); // cleans up after itself
				Console.WriteLine(Objekt);
				}
			}
			private static readonly Random random = new Random(); 		//needs to be static, otherwise it won't generate different numbers for each ant
			private static readonly object syncLock = new object();
			public static int RandomNumber(int min, int max)
			{
			    lock(syncLock) // here it locks out other threads to use random at the same time (so it won't generate same numbers)
			    { 
			        return random.Next(min, max);
			    }
			}
			
		}
		
		
		public class ant : BildschirmObjekt
		{
			//TODO: Engergie, Ameise verrecken
			public bool hasFood;
			public ant(int grenzeX, int grenzeY)
			{
				limitX = grenzeX;
				limitY = grenzeY;
				posX = RandomNumber(0,limitX);
				posY = RandomNumber(0,limitY);
				hasFood = false;
			}
		
			public void Move()
			{	
				Draw(" ");
				
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
				
				Draw("A");
			}
			
			public void Show()
			{
				Console.SetCursorPosition(posX, posY);
				Console.WriteLine("A");
			}
			
			public void Seek(food futter, home haus)
			{
				Draw(" ");
				
				if ((posX != futter.posX || posY != futter.posY) && hasFood == false)
				{
					int whereIsMyFoodX = futter.posX - posX;
					int whereIsMyFoodY = futter.posY - posY;
					
					if (whereIsMyFoodX == 0 && whereIsMyFoodY == 0)
						hasFood = true;
					
					if (whereIsMyFoodY > 0)
						posY += 1;
					
					if (whereIsMyFoodX < 0) 
						posX -= 1;
					
					if (whereIsMyFoodY < 0) 
						posY -= 1;
					
					if (whereIsMyFoodX > 0) 
						posX += 1;
				}
				
				if ((posX != haus.posX || posY != haus.posY) && hasFood == true)
				{
					int whereIsMyHomeX = haus.posX - posX;
					int whereIsMyHomeY = haus.posY - posY;
					
					if (whereIsMyHomeY > 0) 
						posY += 1;
					
					if (whereIsMyHomeX < 0) 
						posX -= 1;
					
					if (whereIsMyHomeY < 0) 
						posY -= 1;
					
					if (whereIsMyHomeX > 0) 
						posX += 1;
				}
				
				if (posY == futter.posY)
				{
					if (posX == futter.posX) 
					{
						hasFood = true;
						futter.foodLeft -= 1;
					}
				}
				
				if (posY == haus.posY) 
				{
					if (posX == haus.posX) 
					{
						hasFood = false;
						haus.foodCount += 1;
					}
				}
				Move();
			}
	}
		
		
	public class food : BildschirmObjekt
	{
		public int foodLeft;
		
		public food(int grenzeX, int grenzeY)
		{
			limitX = grenzeX;
			limitY = grenzeY;
			posX = RandomNumber(0,limitX);
			posY = RandomNumber(0,limitY);
			foodLeft = 15;
		}
	}
	
	
	public class home : BildschirmObjekt
	{
		public int foodCount;
		public home(int grenzeX, int grenzeY)
			{
				limitX = grenzeX;
				limitY = grenzeY;
				posX = RandomNumber(0,limitX);
				posY = RandomNumber(0,limitY);
			}
	}
	
	
	class Program
	{
		
		public static void Main(string[] args)
		{
			AmeisenSimulation Simu = new AmeisenSimulation(120, 50, 20);

		}
	}
}