using System;
using System.Threading;

namespace RunningCatsTotalizator
{
    class Program
    {
        private const int Distance = 1000;
        private static int _winNumber;
        private static double _bestTime;
        private static int _playerPrediction;

        static void Main(string[] args)
        {
            Greetings();
            UserInteraction();
        }

        private static void Greetings()
        {
            Console.WriteLine("Добро пожаловать в \"Симулятор кошачьих бегов версии\" 1.0!");
            Console.WriteLine($"Правила очень просты: у нас есть {Cat.totalCatsInTheGame} котов," +
                              " участвующих в забеге!");
            Console.WriteLine("Каждый из котов имеет свой уникальный номер. Вам нужно угадать кто" +
                              "прибежит к финишу первым.");
            Console.WriteLine("Для этого достаточно просто ввести его номер с клавиатуры и узнать угадали или нет!");
            Console.WriteLine("Хотите сыграть? (введите \"да\" чтобы сыграть или \"нет\", чтобы завершить игру)");
        }


        private static void UserInteraction()
        {
            while (true)
            {
                var enter = Console.ReadLine();
                if (enter == "да")
                {
                    if (PositiveAnswerHandler()) break;
                }

                if (enter == "нет")
                {
                    NegativeAnswerHandler();
                    break;
                }

                UnknownAnswerHandler();
            }
        }

        
        private static bool PositiveAnswerHandler()
        {
            Console.Clear();
            Console.WriteLine(
                "Отлично!!!Забег вот вот начнется, какой номер по вашему мнению выиграет ? " +
                $"Укажите пожалуйста число от 1 до {Cat.totalCatsInTheGame}");

            if (!UserInputHandler()) return false;


            Console.WriteLine("Спортсмены готовы! Нажмите любую клавишу чтобы начать забег!");
            Console.ReadKey();
                
            CreateResults();

            PrintResults();

            return true;

        }

        private static void CreateResults()
        {
            for (var i = 1; i <= Cat.totalCatsInTheGame; i++)
            {
                var cat = new Cat {catNumber = i};

                var rnd = new Random();
                cat.catSpeed = rnd.Next(1, 11);

                var time = cat.CatTime(Distance);

                if (_bestTime == 0 || time <= _bestTime)
                {
                    _bestTime = time;
                    _winNumber = cat.catNumber;
                }
            }
        }

        private static void PrintResults()
        {
            Console.WriteLine(
                $"Победил кот под номером: {_winNumber},пробежав дистанцию за = {_bestTime} секунд!");

            if (_playerPrediction == _winNumber)
            {
                Console.WriteLine("ПОЗДРАВЛЯЕМ!!! Вы угадали!");
                Console.WriteLine("Спасибо заигру! До встречи");
                Console.WriteLine("Для выхода нажмите любую клавишу . . .");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Очень жаль, но вы проиграли!");
                Console.WriteLine("В следующий раз непременно повезет! До встречи!");
                Console.WriteLine("Для выхода нажмите любую клавишу . . .");
                Console.ReadKey();
            }
        }

        private static bool UserInputHandler() => int.TryParse(Console.ReadLine(), out var input) && input > 0 && input <=Cat.totalCatsInTheGame;

        private static void NegativeAnswerHandler()
        {
            Console.Clear();
            Console.WriteLine("Очень жаль! До встречи!");
            Thread.Sleep(1000);
        }

        private static void UnknownAnswerHandler()
        {
            Console.WriteLine("Вы ввели что-то не то! Давайте еще раз...");
        }
    }
}