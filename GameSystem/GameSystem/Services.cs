using UnityEngine;
using System.IO;

namespace GameSystem
{
    public static class Services
    {
        // Список всех возможных видов скобок в которые можно заключить текст.
        public enum Brackets
        {
            None = 0,        // Без скобок
            Round = 1,      // Круглые скобки ()
            Square = 2,    // Квадратные скобки []
            Angle = 3,    // Скобки больше-меньше <>
            Braces = 4   // фигурные скобки {}
        }

        /*
            SignRange - несколько методов, различающихся лишь типом входящих значений. 
            Возвращает 1 если текущее значение больше указанного максимального.
            Возвращает -1 если текущее значение меньше указанного минимального
            Возвращает 0 если текущее значение лежит в диапазоне между минимальным и максимальным значением.            
        */

        #region SignRange
        public static sbyte SignRange(float value, float minRange, float maxRange)
        {
            if (value > maxRange)
                return 1;
            else if (value < minRange)
                return -1;
            else return 0;
        }

        public static sbyte SignRange(int value, int minRange, int maxRange)
        {
            if (value > maxRange)
                return 1;
            else if (value < minRange)
                return -1;
            else return 0;
        }

        public static sbyte SignRange(sbyte value, sbyte minRange, sbyte maxRange)
        {
            if (value > maxRange)
                return 1;
            else if (value < minRange)
                return -1;
            else return 0;
        }

        public static sbyte SignRange(short value, short minRange, short maxRange)
        {
            if (value > maxRange)
                return 1;
            else if (value < minRange)
                return -1;
            else return 0;
        }


        public static sbyte SignRange(float value, Vector2 minMaxRange)
        {
            if (value > minMaxRange.y)
                return 1;
            else if (value < minMaxRange.x)
                return -1;
            else return 0;
        }

        public static sbyte SignRange(int value, Vector2 minMaxRange)
        {
            if (value > (int)minMaxRange.y)
                return 1;
            else if (value < (int)minMaxRange.x)
                return -1;
            else return 0;
        }

        public static sbyte SignRange(sbyte value, Vector2 minMaxRange)
        {
            if (value > (sbyte)minMaxRange.y)
                return 1;
            else if (value < (sbyte)minMaxRange.x)
                return -1;
            else return 0;
        }

        public static sbyte SignRange(short value, Vector2 minMaxRange)
        {
            if (value > (short)minMaxRange.y)
                return 1;
            else if (value < (short)minMaxRange.x)
                return -1;
            else return 0;
        }
        #endregion

        /*
            Mean - несколько методов, различающихся типом входящих значений. Но все входящие значения - массивы.
            Вовзращает среднее арифметическое всех чисел массива.
        */

        #region Mean
        public static float Mean(float[] array)
        {
            float sum = 0;

            for (int i = 0; i < array.Length; i++)
                sum += array[i];

            float result;

            if (array.Length != 0)
                result = sum / array.Length;
            else result = 0;

            return result;
        }

        public static float Mean(int[] array)
        {
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
                sum += array[i];

            int result;

            if (array.Length != 0)
                result = sum / array.Length;
            else result = 0;

            return result;
        }

        public static float Mean(ushort[] array)
        {
            ushort sum = 0;

            for (int i = 0; i < array.Length; i++)
                sum += array[i];

            ushort result;

            if (array.Length != 0)
                result = (ushort)(sum / array.Length);
            else result = 0;

            return result;
        }

        public static float Mean(short[] array)
        {
            short sum = 0;

            for (int i = 0; i < array.Length; i++)
                sum += array[i];

            short result;

            if (array.Length != 0)
                result = (short)(sum / array.Length);
            else result = 0;

            return result;
        }

        public static float Mean(byte[] array)
        {
            byte sum = 0;

            for (int i = 0; i < array.Length; i++)
                sum += array[i];

            byte result;

            if (array.Length != 0)
                result = (byte)(sum / array.Length);
            else result = 0;

            return result;
        }

        public static float Mean(sbyte[] array)
        {
            sbyte sum = 0;

            for (int i = 0; i < array.Length; i++)
                sum += array[i];

            sbyte result;

            if (array.Length != 0)
                result = (sbyte)(sum / array.Length);
            else result = 0;

            return result;
        }

        #endregion

        // 
        public static Vector2 ScreenBounds()
        {
            if (Camera.main)
            {
                // Получаем текущий размер экрана.
                return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
            }
            else return Vector2.zero;
        }

        // Устанавливает Position и Rotation трансформа равными нолю, а значение размера - равным 1.
        public static void ToDefaultTransform(Transform t)
        {
            t.position = Vector3.zero;
            t.eulerAngles = Vector3.zero;
            t.localScale = Vector3.one;
        }

        // Размещает объект на какой либо поверхности на определенной высоте от неё. Работает только со стандартной гравитацией Unity.
        public static void PlacingOnSurface(Transform t, float height)
        {
            RaycastHit hit;

            if (Physics.Raycast(t.position, Physics.gravity, out hit))
            {
                if (hit.distance != height)
                    t.position = hit.point + -Physics.gravity.normalized * height;
            }
        }

        // Размещает объект на какой либо поверхности на определенной высоте от неё. Может работать с любой сторонней гравитацией.
        public static void PlacingOnSurface(Transform t, float height, Vector3 gravity)
        {
            RaycastHit hit;

            if (Physics.Raycast(t.position, gravity, out hit))
            {
                if (hit.distance != height)
                    t.position = hit.point + -gravity.normalized * height;
            }
        }

          // Функция принимает в себя целое число и параметр длинны. Если длинна задана 5, а число двухзначное, то пространство перед числом
         // будет заполнено нолями. Т.е. число 56 будет выглядеть как 00056. При длинне 3, 056, при длинне 8 - 00000056 и т.д. Возвращает 
        // не число, а текст в виде string
        public static string GenerationIndex(int value, int length)
        {
            string index = "";
            int zerosNumber = Mathf.Clamp(length - value.ToString().Length, 0, int.MaxValue);

            for (int i = 0; i < zerosNumber; i++)
                index += "0";

            return index += value;
        }

        // Заключает любой текст в разные виды скобочек, с двух сторон. Доступные скобочки - () [] <> {}
        public static string EncloseTextInBrackets(string text, Brackets brackets)
        {
            if (brackets == Brackets.Round)
                return "(" + text + ")";
            else if (brackets == Brackets.Square)
                return "[" + text + "]";
            else if (brackets == Brackets.Angle)
                return "<" + text + ">";
            else if (brackets == Brackets.Braces)
                return "{" + text + "}";
            return text;
        }

         // Чистит каталог по указанному пути от файлов типа *.meta. Нужен для поддержания чистоты например в папке со скриншотами, в которой
        // желательно, чтобы лежали только файлы с изображениями и никакие другие.
        public static void DeleteMetaFiles(string path)
        {
            int count = Directory.GetFiles(path, "*.meta", SearchOption.TopDirectoryOnly).Length;

            if (count > 0)
            {
                string[] files = Directory.GetFiles(path, "*.meta");

                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length - 1; i++)
                        File.Delete(files[i]);
                }
            }
        }

        // 
        public static byte Lottery(byte[] exceptions, byte arraySize)
        {
            byte x = 0;

            if (exceptions != null && exceptions.Length > 0)
            {
                if (exceptions.Length < arraySize)
                {
                    bool coincidence = true;

                    while (coincidence)
                    {
                        coincidence = false;
                        x = (byte)UnityEngine.Random.Range(0, arraySize);

                        for (byte i = 0; i < exceptions.Length; i++)
                        {
                            if (exceptions[i] == x)
                            {
                                coincidence = true;
                                break;
                            }
                        }
                    }
                }
                else x = Lottery(exceptions[exceptions.Length - 1], arraySize);
            }
            else Debug.LogError("Common/RandomGenerator: Пустой массив исключений!");

            return x;
        }

        // Простой локатрон. Принимает в себя число вариантов и число, совпадение с которым будет означать победу.
        // Если аргумент exception = 9, a arraySize = 50, то рандом генератор 
        public static byte Lottery(byte exception, byte arraySize)
        {
            byte x = 0;

            while (x == exception)
                x = (byte)UnityEngine.Random.Range(0, arraySize);

            return x;
        }
    }
}
