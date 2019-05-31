/*
 * Created by SharpDevelop.
 * User: kroll_lars
 * Date: 02.04.2019
 * Time: 09:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace AntsSim
{
	public class AmeisenSimulation 
		{
			
			List<ant> ameisen;
			List<food> essen;
			home zuHause;
			
			public AmeisenSimulation(int grenzeX = 120, int grenzeY = 40, int ameisenAnzahl = 20, int essensAnzahl = 15)
			{
				ameisen = new List<ant>();
				essen = new List<food>();
				zuHause = new home(grenzeX, grenzeY);
				Console.SetWindowSize(grenzeX+10, grenzeY+10);
				
				for (int i = 0; i < ameisenAnzahl; i++) 
				{
					ameisen.Add(new ant(grenzeX, grenzeY));
					ameisen[i].sim = this;
				}
				
				for (int i = 0; i < essensAnzahl; i++) 
				{
					essen.Add(new food(grenzeX, grenzeY));
					essen[i].sim = this;
				}
				
				while (true) 
		        {
		        	for (int i = 0; i < ameisen.Count; i++) 
		        	{
		        		for (int j = 0; j < essen.Count; j++) 
		        		{
		        			if (essen[j].foodLeft <= 0)
		        			{
		        				CleanUp(essen[j].posX, essen[j].posY);
		        				essen.RemoveAt(j);
		        				essen.Add(new food(grenzeX, grenzeY));
		        			}
		        			if (essen[j].foodLeft > 0)
		        			{
		        				essen[j].Draw("F");
		        			}
		        		}
		        		
		        		
		        		
		        		if (ameisen[i].currentFoodGoal == false) 
		        		{
		        			ameisen[i].SetFood(essen[ameisen[i].LocateNearestFood(essen)]);
		        			ameisen[i].currentFoodGoal = true;
		        		}
		        		
		        		if (ameisen[i].currentFood.foodLeft <= 0) 
		        		{
		        			ameisen[i].currentFoodGoal = false;
		        		}
		        		
		        		ameisen[i].Seek(ameisen[i].currentFood, zuHause);
		        		
		        		ameisen[i].energie -= 1;
		        		if (ameisen[i].CheckIfDead())
		        		{
		        			CleanUp(ameisen[i].posX, ameisen[i].posY);
		        			ameisen.RemoveAt(i);
		        		}
		        	}        	
		        	zuHause.Draw("H");
		        	Console.SetCursorPosition(0, grenzeY+5);
		        	Console.WriteLine("food: " + zuHause.foodCount);
		        	System.Threading.Thread.Sleep(10);
		        }
			}
			
			public void CleanUp(int posX, int posY)
			{
				Console.SetCursorPosition(posX, posY);
				Console.WriteLine(" ");
			}
			
			public bool PositionEqual(int firstObjectPosX, int firstObjectPosY, int secondObjectPosX, int secondObjectPosY) 
			{
				bool retVal = false;
				if (firstObjectPosX != secondObjectPosX || firstObjectPosY != secondObjectPosY)
				{
					
					int distanceBetweenX = Math.Abs(firstObjectPosX - secondObjectPosX);
					int distanceBetweenY =  Math.Abs(firstObjectPosY - secondObjectPosY);
					
					if (distanceBetweenX <= 1)
					{
						if (distanceBetweenY <= 1) 
						{
							retVal = true;
						}
					}
					else
					{
					    retVal = false;
					}
				}
				return retVal;
			}
		}
		
	public class BildschirmObjekt 
		{
			public int posX;
			public int posY;
			public int limitX { get; set; }
			public int limitY { get; set; }
			public AmeisenSimulation sim;
			
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
			public int energie = RandomNumber(5000, 9000);
			public bool hasFood;
			public bool currentFoodGoal;
			public food currentFood;
			
			public ant(int grenzeX, int grenzeY)
			{
				limitX = grenzeX;
				limitY = grenzeY;
				posX = RandomNumber(0,limitX);
				posY = RandomNumber(0,limitY);
				hasFood = false;
				currentFoodGoal = false;
			}
			
			public void SetFood(food futter)
			{
				currentFood = futter;
			}
			
			public bool CheckIfDead()
			{
				bool retVal = false;
				if (energie <= 0) 
				{
					retVal = true;
				}
				return retVal;
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
			
				if (hasFood == false) 
				{
					if (sim.PositionEqual(this.posX, this.posY, futter.posX, futter.posY)) 
					{
						hasFood = true;
						futter.foodLeft -= 1;
					}
					else
					{
						int distanceToMyFoodX = futter.posX - posX;
						int distanceToMyFoodY = futter.posY - posY;
						
						if (distanceToMyFoodY > 0)
							posY += 1;
						
						if (distanceToMyFoodX < 0) 
							posX -= 1;
						
						if (distanceToMyFoodY < 0) 
							posY -= 1;
						
						if (distanceToMyFoodX > 0) 
							posX += 1;
					}
				}
				if (hasFood == true)
				{
					if (sim.PositionEqual(this.posX, this.posY, haus.posX, haus.posY)) 
					{
						hasFood = false;
						haus.foodCount += 1;
					}
					else
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
				}
				
				Move();
			}
			
			public int LocateNearestFood(List<food> essen)
			{
				double abstand = 10000;
				double zwischenErgebnis;
				int index = 0;
				for (int i = 0; i < essen.Count; i++) 
				{
					double xValue = Math.Pow(essen[i].posX - this.posX, 2);
					double yValue = Math.Pow(essen[i].posY - this.posY, 2);
					zwischenErgebnis = Math.Sqrt(xValue + yValue);
						
			  		if (abstand > zwischenErgebnis) 
					{
						abstand = zwischenErgebnis;
						index = i;
			  		}
				}
				return index;
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
			AmeisenSimulation Simu = new AmeisenSimulation(150, 50);
		}
	}
}