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
	private static bool isreload = true; // перезарядка
	private static int ax; // кординаты пули по X
	private static int ay; // кординаты пули по Y
	private static char pk; 
	private static bool isS,isW,isA,isD; // переменные для стрельбы
	private static Random r = new Random(); // переменная для рандома
	private static int score = 0;
	private static int zx = 10; // кординаты зомби по X
	private static int zy = 15; // кординаты зомби по Y
	private static bool isXY = true;

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
			Thread.Sleep(150);
			Console.Clear();
		}
		Show();
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
				}
				else if(i == zy && j == zx)
				{
					Console.Write("Z");
				}
				else // ставит пропуски если нечего не совпало
				{
					Console.Write(" ");
				}
			}
			Console.WriteLine();
		}
		Console.WriteLine("Score : {0}",score);
	}

	private static void TShoot()
	{
		  if(isW)
			{
				ay--;
			}
			else if(isS)
			{
				ay++;
			}
			else if(isA)
			{
				ax--;
			}
			else if(isD)
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
			Shoot();
			isreload = false;
			   break;
		}
	 }
	}

	private static void Shoot()
	{
		if(pk == 'w' && isreload)
			{
				Console.Beep();
				ay = py++;
				ax = px;
				isW = true;
				isA = false;
				isS = false;
				isD = false;
			}
			else if(pk == 's' && isreload)
			{
				Console.Beep();
				ay = py--;
				ax = px;
				isW = false;
				isA = false;
				isS = true;
				isD = false;
			}
			else if(pk == 'a' && isreload)
			{
				Console.Beep();
				ay = py;
				ax = px++;
				isW = false;
				isA = true;
				isS = false;
				isD = false;
			}
			else if(pk == 'd' && isreload)
			{
				Console.Beep();
				ay = py;
				ax = px--;
				isW = false;
				isA = false;
				isS = false;
				isD = true;
			}
	}

	private static void SpawnZ() // генерация зомби
	{
		zx = r.Next(0,w);
		zy = r.Next(0,h);
	}

	private static void ZMove() // движениеaaa зомби
	{
		bool isX = zx > px ? true : false;
		bool isY = zy> py ? true : false;
		if(zy == py)
		{
			if(isX)
			{
				zx--;
			}
			else
			{
				zx++;
			}
		}
		else if(zx == px)
		{
			if(isY)
			{
				zy--;
			}
			else
			{
				zy++;
			}
		}
		else
		{
			if(isXY)
			{
				zx++;
			}
			else
			{
				zx--;
			}
		}
	}

	private static void Logic()
	{
		if(px == w-1 || py == h-1 || px == 0 || py == 0) // если персонаж зашел за карту
		{
			isalive = false;
		}
		if(ax == zx && ay == zy) // если пуля задела зомби
		{
			Console.Beep();
			SpawnZ();
			score++;
		}
		if(zx == px && zy == py) // если персонаж задел зомби
		{
			isalive = false;
		}
		if(zx == w-1 || zy == h-1)
		{
			isXY = false;
		}
		if(zx == 0 || zy == 0)
		{
			isXY = true;
		}
		if(ax == w-1 || ay == h-1 || ax == 0 || ay == 0) // перезарядка
		{
			isreload = true;
		}
		TShoot();
		ZMove();
	}
}