using System;
using System.Collections.Generic;

namespace RusakovLaba
{
    class Result
    {
        public string result;
        public int ii, jj;
    }

    class Program
    {
        static int Minimum(int i, int j, ref int[,] vs)
        {
            int result;
            int n = vs.GetLength(1);
            int north = int.MaxValue;
            int east = int.MaxValue;
            int ne = int.MaxValue;

            int z = i - 1;
            if (z > 0 && z < n)
            {
                north = vs[z, j];
            }
            z = j + 1;
            if (z > 0 && z < n)
            {
                east = vs[i, z];
            }
            int zi = i - 1;
            int zj = j + 1;
            if (zi > 0 && zj > 0 && zi < n && zj < n)
            {
                ne = vs[zi, zj];
            }

            z = i - 2;
            if (z >= 0 && z < n && north != int.MaxValue)
            {
                north = north + vs[z, j];
            }

            z = j + 2;
            if (z >= 0 && z < n && east != int.MaxValue)
            {
                east = east + vs[i, z];
            }

            zi = i - 2;
            zj = j + 2;
            if (zi >= 0 && zj >= 0 && zi < n && zj < n && ne != int.MaxValue)
            {
                ne = ne + vs[zi, zj];
            }
            result = Math.Min(north, Math.Min(east, ne));

            if (result.Equals(int.MaxValue))
            {
                result = 0;
            }

            return result;
        }

        static Result pathij(int i, int j, ref int[,] vs)
        {
            Result result = new Result();
            int n = vs.GetLength(1);
            int north = int.MaxValue;
            int east = int.MaxValue;
            int ne = int.MaxValue;
            int z;

            z = i - 2;
            if (z >= 0 && z < n)
            {
                if (vs[i, j] - vs[i - 1, j] == vs[z, j])
                {
                    north = vs[z, j];
                }
            }

            z = j + 2;
            if (z >= 0 && z < n)
            {
                if (vs[i, j] - vs[i, j + 1] == vs[i, z])
                {
                    east = vs[i, z];
                }
            }

            int zi = i - 2;
            int zj = j + 2;
            if (zi >= 0 && zj >= 0 && zi < n && zj < n)
            {
                if (vs[i, j] - vs[i - 1, j + 1] == vs[zi, zj])
                {
                    ne = vs[zi, zj];
                }
            }

            int min = Math.Min(north, Math.Min(east, ne));
            if (north == min)
            {
                result.result = "n e ";
            }
            else if (east == min)
            {
                result.result = "e n ";
            }
            else
                result.result = "ne ";

            result.ii = i - 2;
            result.jj = j + 2;
            return result;
        }

        static void Main(string[] args)
        {
            int[,] vs = new int[,] { 
                { 00, 18, 00, 22, 00, 24, 00, 20, 00, 12, 00 },
                { 29, 30, 18, 36, 22, 22, 13, 46, 17, 36, 13 },
                { 00, 22, 00, 12, 00, 13, 00, 12, 00, 13, 00 },
                { 020, 048, 026, 054, 021, 034, 016, 046, 016, 032, 019 },
                { 0, 013, 0, 014, 0, 016, 0, 016, 0, 014, 0 },
                { 018, 036, 012, 034, 010, 042, 016, 048, 016, 036, 022 },
                { 0, 015, 0, 027, 0, 023, 0, 021, 0, 014, 0 },
                { 024, 056, 024, 056, 024, 038, 012, 030, 023, 050, 019 },
                { 0, 014, 0, 011, 0, 027, 0, 023, 0, 021, 0 },
                { 023, 028, 027, 024, 020, 052, 023, 026, 013, 026, 028 },
                { 0, 016, 0, 013, 0, 022, 0, 029, 0, 013, 0 } };
            //answer  Стоимость минимального маршрута: 143
            //        Маршрут прокладки маршрута за минимальную стоимость: sv v s s v s v v s
            //                                                             ne e n n e n e e n

            int m = vs.GetLength(0);
            int n = vs.GetLength(1);

            for (int i = 0; i <= n - 1; i+=2)
            {
                for (int j = 0; j < i; j+=2)
                {
                    int z = n - i + j +1;
                    vs[j, z] = Minimum(j, z, ref vs);
                }
            }

            for (int i = 0; i <= m - 1; i+=2)
            {
                for (int j = n - 1; i <= j; j-=2)
                {
                    int z = j - i;
                    vs[j, z] = Minimum(j, z, ref vs);
                }
            }
            vs[n-1, 0] = Minimum(n-1, 0, ref vs);
            int min_cost = vs[n-1, 0];

            //int am = 1;
            string path = string.Empty;
            int i1 = n - 1 ;
            int j1 = 0;

            while (i1 > 0 && j1 < n)
            {
                Result res = pathij(i1, j1, ref vs);
                path += res.result;
                i1 = res.ii;
                j1 = res.jj;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(vs[i, j].ToString() + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(min_cost);
            Console.WriteLine(path);
        }
    }
}
