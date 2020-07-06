using System;

namespace LotteryGame
{
    public struct LotteryResult
    {
        public int targetValue;
        public int randomResult;
        public int randomRange;

        public LotteryResult(int targetValue, int randomResult, int randomRange)
        {
            this.targetValue = targetValue;
            this.randomResult = randomResult;
            this.randomRange = randomRange;
        }
    }

    public static class Lottery
    {
        public static byte BooleanToByte(bool value)
        {
            if (value)
                return 1;
            else return 0;
        }

        public static bool ByteToBoolean(sbyte value)
        {
            if (value >= 1)
                return true;
            else return false;
        }
        
        public static LotteryResult GetLotteryResult(int targetValue, int randomRange)
        {
            LotteryResult result = new LotteryResult();            

            if (randomRange > 0)
            {
                result.randomRange = randomRange;
                result.targetValue = targetValue;
                result.randomResult = new Random().Next(randomRange);
            }

            return result;
        }

        public static LotteryResult GetLotteryResult(int randomRange)
        {
            LotteryResult result = new LotteryResult();
            result.targetValue = 0;

            if (randomRange > 0)
            {
                result.randomRange = randomRange;
                result.randomResult = new Random().Next(randomRange);
            }

            return result;
        }

        public static bool LotteryResultBoolean(LotteryResult result)
        {
            return result.targetValue == result.randomResult;
        }

        public static byte LotteryResultBinary(LotteryResult result)
        {
            return BooleanToByte(LotteryResultBoolean(result));
        }
    }

    class Program
    {        
        static void Main()
        {
            Console.WriteLine("Please, select language / Пожалуйста, выберите язык.");
            Console.WriteLine("Default lingvo - english / По умолчанию будет выбран английский.");
            Console.WriteLine("Input ENGLISH or / или напечатайте РУССКИЙ");
            string inputLinguage = Console.ReadLine().ToLower();

            byte currentLinguage;

            if (inputLinguage == "русский")
                currentLinguage = 1;
            else if (inputLinguage == "english")
                currentLinguage = 0;
            else currentLinguage = 0;

            if (currentLinguage == 0)
                Console.WriteLine("Welcome in Lottery!");
            else if (currentLinguage == 1)
                Console.WriteLine("Добро пожаловать в лотерею!");

            bool isWork;
            do
            {
                Console.Clear();
                int range = 0, targetValue = 0;               

                if (currentLinguage == 0)
                    Console.WriteLine("Input random range: ");
                else if (currentLinguage == 1)
                    Console.WriteLine("Задайте количество случайных чисел: ");

                bool repeatInputRange = true;

                while (repeatInputRange)
                {
                    try
                    {
                        range = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {

                    }
                    finally
                    {
                        if (range > 1)
                            repeatInputRange = false;
                        else
                        {                            
                            if (currentLinguage == 0)
                                Console.WriteLine("It number must be greater than zero!");
                            else if (currentLinguage == 1)
                                Console.WriteLine("Это число должно быть больше нуля!");

                            repeatInputRange = true;
                        }  
                    }
                }

                if (currentLinguage == 0)
                    Console.WriteLine("Do you want to set a target value? By default, it will be 0.");
                else if (currentLinguage == 1)
                    Console.WriteLine("Желаете задать целевое число? По умолчанию оно равно 0.");

                try
                {
                    targetValue = Convert.ToInt32(Console.ReadLine());
                }
                catch { }

                LotteryResult result = Lottery.GetLotteryResult(targetValue, range);

                if (currentLinguage == 0)
                {
                    Console.WriteLine($"Result: {Lottery.LotteryResultBoolean(result)}; " +
                                      $"Value: {result.randomResult}; Target: {result.targetValue}; Range: {result.randomRange}");
                }
                else if (currentLinguage == 1)
                {
                    Console.WriteLine($"Результат: {Lottery.LotteryResultBoolean(result)}; " +
                                      $"Значение: {result.randomResult}; Цель: {result.targetValue}; Диапазон: {result.randomRange}");
                }

                if (currentLinguage == 0)
                    Console.WriteLine("Repeat program? Input: For close input: FALSE, EXIT or N");
                else if (currentLinguage == 1)
                    Console.WriteLine("Повторить программу? Для выхода введите: FALSE, EXIT или N");

                string exit = "" + Console.ReadLine().ToLower();

                exit.ToLower();

                if (exit == "exit" || exit == "false" || exit == "n")
                    isWork = false;
                else isWork = true;
            }
            while (isWork);            
        }
    }
}
