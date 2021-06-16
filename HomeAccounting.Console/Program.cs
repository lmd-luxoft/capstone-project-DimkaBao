using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAccounting.Console
{
    class Program
    {
        private static List<int> Fill(int lenght)
        {
            var result = new List<int>();
            for (int i = lenght; i >= 0; i--)
                result.Add(i);
            return result;
        }

        static void Main(string[] args)
        {
            var mas = Fill(100);
            var sort = Sort(mas);

            sort.ForEach(x => System.Console.WriteLine(x));
            System.Console.ReadKey();
        }

        private static List<int> Sort(List<int> massiv)
        {
            if (massiv == null || !massiv.Any())
                throw new ArgumentException("пустой массив");
            if (massiv.Count() == 1)
                return massiv;
            if (massiv.Count() == 2)
                return Compare(massiv[0], massiv[1]);

            int i = massiv.Count() / 2;
            List<int> a = massiv.Take(i).ToList();
            List<int> b = massiv.Skip(i).ToList();

            Task[] tasks = new Task[]
            {
                Task.Run(() => { a = Sort(a); }),
                Task.Run(() => { b = Sort(b); })
            };

            Task.WaitAll(tasks);

            return Concat(a, b);
        }

        private static List<int> Compare(int a, int b)
        {
            if (a < b)
                return new List<int> { a, b };
            else
                return new List<int> { b, a };
        }

        private static List<int> Concat(List<int> a, List<int> b)
        {
            var aLenght = a.Count();
            var bLenght = b.Count();
            List<int> result = new List<int>();
            int i = 0;
            int j = 0;
            while (i < aLenght || j < bLenght)
            {
                int r;
                if (i == aLenght)
                {
                    r = b[j];
                    j++;
                }
                else if (j == bLenght)
                {
                    r = a[i];
                    i++;
                }
                else if (a[i] < b[j])
                {
                    r = a[i];
                    i++;
                }
                else
                {
                    r = b[j];
                    j++;
                }
                result.Add(r);
            }

            return result;
        }
    }
}
