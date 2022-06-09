class Proram 
{ 
    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Random random = new Random();
        bool isPlaying = true;
        bool isAlive = true;
        int enemyPositionX;
        int enemyPositionY;
        int enemyDirectionX = 0;
        int enemyDirectionY = -1;
        int dotsCount = 0;
        int playerDotsCount = 0;
        int sleepTime = 150;
        int playerPositionX;
        int playerPositionY;
        int directionX = 0;
        int directionY = 1;
        string mapName = "map1";
        char[,] map = ReadMap(mapName, out playerPositionX, out playerPositionY,out enemyPositionX, out enemyPositionY, ref dotsCount);
        DrawMap(map);

        while (isPlaying)
        {
            Console.SetCursorPosition(0, 30);
            Console.WriteLine($"Собрано {playerDotsCount}/{dotsCount}");

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                ChangeDirection(key, ref directionX, ref directionY);
            }

            if (map[playerPositionX + directionX, playerPositionY + directionY] != '#')
            {
                Move(directionX, directionY, ref playerPositionX, ref playerPositionY, '@', map);
                CollectDots(playerPositionX, playerPositionY, map, ref playerDotsCount);
            }

            if (map[enemyPositionX + enemyDirectionX, enemyPositionY + enemyDirectionY] != '#')
            {
                Move(enemyDirectionX, enemyDirectionY, ref enemyPositionX, ref enemyPositionY, '$', map);
            }

            else
            {
                ChangeDirection(random, ref enemyDirectionX, ref enemyDirectionY);
            }

            System.Threading.Thread.Sleep(sleepTime);

            if (enemyPositionX == playerPositionX && enemyPositionY == playerPositionY)
            {
                isAlive = false;
            }

            if (playerDotsCount == dotsCount && isAlive)
            {
                isPlaying = false;
                Console.Clear();
                Console.WriteLine("Вы победили");
            }

            else if (isAlive == false)
            {
                isPlaying = false;
                Console.Clear();
                Console.WriteLine("Вы проиграли");
            }
            
        }
    }
    
    static void CollectDots(int playerX, int playerY, char [,] map, ref int playerDotsCount)
    {
        if (map[playerX, playerY] == '.')
        {
            playerDotsCount++;
            map[playerX, playerY] = ' ';
        }
    }

    static char[,] ReadMap(string mapName, out int playerX, out int playerY,out int enemyX, out int enemyY, ref int dotsCount)
    {
        playerY = 0;
        playerX = 0;
        enemyX = 0;
        enemyY  = 0;
        string[] newFile = File.ReadAllLines($"maps/{mapName}.txt");
        char[,] map = new char[newFile.Length, newFile[0].Length];

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = newFile[i][j];

                if (map[i, j] == '@')
                {
                    playerX = i;
                    playerY = j;
                    map[i, j] = '.';
                }

                else if (map[i, j] == '$')
                {
                    enemyX = i;
                    enemyY = j;
                    map[i, j] = '.';
                }

                else if (map[i, j] == ' ')
                {
                    map[i,j] = '.';
                    dotsCount++;
                }
            }
        }
        return map;
    }

    static void DrawMap(char[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write(array[i, j]);
            }
            Console.WriteLine();
        }
    }

    static void Move(int directionX, int directionY,ref int x,ref int y, char symbol, char[,] map)
    {
            Console.SetCursorPosition(y, x);
            Console.Write(map[x, y]);
            x += directionX;
            y += directionY;
            Console.SetCursorPosition(y, x);
            Console.Write(symbol);
    }

    static void ChangeDirection(ConsoleKeyInfo key ,ref int directionX, ref int directionY)
    {
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                 directionX = -1;
                 directionY = 0;
                  break;
            case ConsoleKey.DownArrow:
                 directionX = 1;
                 directionY = 0;
                 break;
            case ConsoleKey.LeftArrow:
                 directionX = 0;
                 directionY = -1;
                 break;
            case ConsoleKey.RightArrow:
                 directionX = 0;
                 directionY = 1;
                 break;
        }
        }

    static void ChangeDirection(Random random, ref int directionX, ref int directionY)
    {
        int minRandValue = 1;
        int maxRandValue = 5;
        int enemyDirection = random.Next(minRandValue, maxRandValue);

        switch (enemyDirection)
        {
            case 1:
                directionX = -1;
                directionY = 0;
                break;
            case 2:
                directionX = 1;
                directionY = 0;
                break;
            case 3:
                directionX = 0;
                directionY = -1;
                break;
            case 4:
                directionX = 0;
                directionY = 1;
                break;
        }
    }
}
