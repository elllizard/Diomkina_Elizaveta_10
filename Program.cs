using System;
using System.Drawing;


namespace game
{
    class MainClass
    {
        static void Pause()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\n\n\t\t\t\tНажмите любую клавишу, чтобы продолжить...");
            Console.ResetColor();
            Console.ReadKey();
        }

        public static void Main(string[] args)
        {
            char choice = char.MinValue;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\t\t\t----- ПЯТНАШКИ -----");
                Console.WriteLine("\n\n\t\t\t'y' - Начать игру\n\t\t\t'n' - Выход");
                choice = (char)Console.Read();
                switch (choice)
                {
                    case 'y':
                        Console.Clear();
                        
                        GameField field = new GameField();
                        field.FillOutField();
                        field.ShowField();
                        bool flag = true;

                        while (true)
                        {
                            Console.WriteLine("\nНажите ESC, что бы выйти...");
                            ConsoleKeyInfo keyinfo = Console.ReadKey();

                            switch (keyinfo.Key)
                            {
                                case ConsoleKey.UpArrow:
                                    field.MoveUp();
                                    break;
                                case ConsoleKey.DownArrow:
                                    field.MoveDown();

                                    break;
                                case ConsoleKey.LeftArrow:
                                    field.MoveLeft();

                                    break;
                                case ConsoleKey.RightArrow:
                                    field.MoveRight();
                                    break;
                                case ConsoleKey.Escape:
                                    flag = false;
                                    break;

                            }
                            if (!flag)
                            {
                                break;
                            }
                            else field.ShowField();
                        }
                        break;
                    case 'n':
                        Console.Clear();
                        Console.WriteLine("\n\t\t\t----- ПЯТНАШКИ -----");
                        Console.WriteLine("\n\t\tИгру окончено...");
                        Pause();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\n\t\t\t----- ПЯТНАШКИ -----");
                        Console.WriteLine("\nТакого пункта в меню нет! Попробуйте снова...");
                        Pause();
                        break;
                }
            }
            while (choice != 'n');
        }
    }
   
    class GameField
    {
        string[,] field = new string[4, 4];
        Random ran = new Random();
        Point emptyField;

        bool IsRepeat(string elem)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == elem)
                        return true;
                }
            }
            return false;
        }


        void Swap(ref string a, ref string b)
        {
            string temp = a;
            a = b;
            b = temp;
        }

        public string[,] FillOutField()
        {
            string check;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    check = Convert.ToString(ran.Next(0, 16));
                    while (IsRepeat(check) == true)
                    {
                        check = Convert.ToString(ran.Next(0, 16));
                    }
                    field[i, j] = check;
                    if (field[i, j] == "0") emptyField = new Point(i, j);
                }
            }
            Console.WriteLine();
            return field;
        }

        public void ShowField()
        {
            Console.Clear();
            Console.WriteLine("\n\t\t\t----- ПЯТНАШКИ -----");
            Console.WriteLine("Передвигайте пустую клеточку чтобы расположить числа по порядку от 1 до 15: \n");
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if ((i == emptyField.X) && (j == emptyField.Y))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        field[i, j] = "[ ]";
                        Console.ResetColor();
                    }

                    Console.Write("{0,5}", field[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void MoveRight()
        {
            if (emptyField.Y < field.GetLength(1) - 1)
            {
                Swap(ref field[emptyField.X, emptyField.Y], ref field[emptyField.X, emptyField.Y + 1]);
                emptyField.Y++;
            }
        }
        public void MoveLeft()
        {
            if (emptyField.Y > 0)
            {
                Swap(ref field[emptyField.X, emptyField.Y], ref field[emptyField.X, emptyField.Y - 1]);
                emptyField.Y--;
            }
        }

        public void MoveUp()
        {
            if (emptyField.X > 0)
            {
                Swap(ref field[emptyField.X - 1, emptyField.Y], ref field[emptyField.X, emptyField.Y]);
                emptyField.X--;
            }
        }

        public void MoveDown()
        {
            if (emptyField.X < field.GetLength(0) - 1)
            {
                Swap(ref field[emptyField.X + 1, emptyField.Y], ref field[emptyField.X, emptyField.Y]);
                emptyField.X++;
            }
        }
    }
}
