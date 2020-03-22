using System;

namespace _2048
{
    class Program
    {
        static Random random = new Random();
        static int[,] field = new int[4, 4];

        static int RandomX()
        {
            return random.Next(0, 4);
        }
        static int RandomY()
        {
            return random.Next(0, 4);
        }
        static void Lose(ref bool lose, bool[] ps)
        {
            int k = 0;
            for (int i = 0; i < ps.Length; i++)
            {
                if (ps[i] == true)
                    k++;
            }

            if (k == 4)
            {
                lose = true;
                Console.WriteLine("\tПоражение!\t");

            }
        }

        static void Win(ref bool p)
        {

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (field[i, j] == 2048)
                    {
                        p = true;
                        Console.WriteLine("Поебда!");
                    }


                }
            }

        }

        static void AddRandEl(bool pas)
        {
            if (pas)
            {
                int x; int y;
                do
                {
                    x = RandomX();
                    y = RandomY();
                } while (field[x, y] != 0);
                field[x, y] = 2;

            }
        }

        static void ShowField(int[,] a)
        {

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("\t" + a[i, j] + "\t");
                }
                Console.WriteLine("");
            }
        }

        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            bool loose = false;
            bool win = false;
            bool pas = false;
            bool[] los = new bool[4] { false, false, false, false };


            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    field[i, j] = 0;
                }
            }
            field[RandomX(), RandomY()] = 2;
            field[RandomX(), RandomY()] = 2;
            while (!win && !loose)
            {
                ShowField(field);

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        los[0] = true;
                        for (int row = 1; row < 4; row++)
                        {
                            for (int column = 0; column < 4; column++)
                            {
                                if (field[row, column] != 0)
                                {
                                    int row1 = row;
                                    for (int zz = row - 1; zz >= 0; zz--)
                                    {

                                        if (field[row1, column] != 0 && (field[zz, column] == field[row1, column] || field[zz, column] == 0) && Math.Abs(zz - row1) == 1)
                                        {
                                            los[0] = false;
                                            pas = true;
                                            field[zz, column] += field[row1, column];
                                            field[row1, column] = 0;
                                            row1 = zz;
                                        }
                                    }
                                }
                            }

                        }
                        AddRandEl(pas);
                        pas = false;

                        break;

                    case ConsoleKey.DownArrow:
                        los[1] = true;
                        for (int row = 3; row >= 0; row--)
                        {
                            for (int column = 0; column < 4; column++)
                            {
                                if (field[row, column] != 0)
                                {
                                    int row1 = row;
                                    for (int zz = row + 1; zz < 4; zz++)
                                    {

                                        if (field[row1, column] != 0 && (field[zz, column] == field[row1, column] || field[zz, column] == 0) && Math.Abs(zz - row1) == 1)
                                        {
                                            los[1] = false;
                                            pas = true;
                                            field[zz, column] += field[row1, column];
                                            field[row1, column] = 0;
                                            row1 = zz;
                                        }
                                    }
                                }
                            }

                        }
                        AddRandEl(pas);
                        pas = false;
                        break;

                    case ConsoleKey.LeftArrow:
                        los[2] = true;
                        for (int row = 1; row < 4; row++)
                        {
                            for (int column = 0; column < 4; column++)
                            {
                                if (field[column, row] != 0)
                                {
                                    int row1 = row;
                                    for (int zz = row - 1; zz >= 0; zz--)
                                    {

                                        if (field[column, row1] != 0 && (field[column, zz] == field[column, row1] || field[column, zz] == 0) && Math.Abs(zz - row1) == 1)
                                        {
                                            los[2] = false;
                                            pas = true;
                                            field[column, zz] += field[column, row1];
                                            field[column, row1] = 0;
                                            row1 = zz;
                                        }
                                    }
                                }
                            }
                        }
                        AddRandEl(pas);
                        pas = false;
                        break;

                    case ConsoleKey.RightArrow:
                        los[3] = true;
                        for (int row = 3; row >= 0; row--)
                        {
                            for (int column = 0; column < 4; column++)
                            {
                                if (field[column, row] != 0)
                                {
                                    int row1 = row;
                                    for (int zz = row + 1; zz < 4; zz++)
                                    {

                                        if (field[column, row1] != 0 && (field[column, zz] == field[column, row1] || field[column, zz] == 0) && Math.Abs(zz - row1) == 1)
                                        {
                                            los[3] = false;
                                            pas = true;
                                            field[column, zz] += field[column, row1];
                                            field[column, row1] = 0;
                                            row1 = zz;
                                        }
                                    }
                                }
                            }
                        }
                        AddRandEl(pas);
                        los[3] = true;
                        pas = false;
                        break;

                }
                Console.Clear();
                Win(ref win);
                Lose(ref loose, los);
            }
            Console.Read();
        }
    }
}
