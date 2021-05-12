using System;
using System.Threading;

class MainClass 
{
  public static void Main (string[] args) 
	{
    Game.Start();
  }
}

static class Game
{
	private static bool isalive = true;
	private const int h = 20; // высота карты
	private const int w = 30; // ширина карты
	private static int px = w/2; // кординаты героя по X
	private static int py = h/2; // кординаты героя по Y
	private static float Reload = 10; // перезарядка
	private static int ax; // кординаты пули по X
	private static int ay; // кординаты пули по Y
	private static char pk;

  public static void Start()
	{
		Menu();
	}

	private static void Menu()
	{
		while(isalive)
		{
		  Show();
			Input();
			Logic();
			Thread.Sleep(50);
			Console.Clear();
		}
		Console.WriteLine("вы проиграли...");
	}

	private static void Show() // выводит все объекты
	{
		for(int i = 0;i < h;i++)
		{
			for(int j = 0;j < w;j++)
			{
				if(i == 0 || i == h-1 || j == 0 || j == w-1) // отображает саму карту
				{
					Console.Write("#");
				}
				else if(i == py && j == px) // отображает персонажа
				{
					Console.Write("U");
				}
				else if(i == ay && j == ax) // отображает пулю
				{
					Console.Write("o");
					Shoot();
				}
				else // ставит пропуски если нечего не совпало
				{
					Console.Write(" ");
				}
			}
			Console.WriteLine();
		}
	}

	private static void Shoot()
	{
		  if(pk == 'w')
			{
				ay--;
			}
			else if(pk == 's')
			{
				ay++;
			}
			else if(pk == 'a')
			{
				ax--;
			}
			else if(pk == 'd')
			{
				ax++;
			}
	}

	private static void Input()
	{
		if(Console.KeyAvailable) 
	 {
		var key = Console.ReadKey();
		switch(key.KeyChar)
		{
			case 'w': // идет вверх
			py--;
			pk = 'w';
			   break;
			case 's': // идет вниз
			py++;
			pk = 's';
			   break;
			case 'a': // идет влево
			px--;
			pk = 'a';
			   break;
			case 'd': // идет вправо
			px++;
			pk = 'd';
			   break;
			case 'f': // выстреливает
			if(pk == 'w')
			{
				ay = py++;
				ax = px;
			}
			else if(pk == 's')
			{
				ay = py--;
				ax = px;
			}
			else if(pk == 'a')
			{
				ay = py;
				ax = px++;
			}
			else if(pk == 'd')
			{
				ay = py;
				ax = px--;
			}
			   break;
		}
	 }
	}

	private static void Logic()
	{
		if(px == w-1 || py == h-1 || px == 0 || py == 0) // если персонаж зашел за карту
		{
			isalive = false;
		}
	}
}