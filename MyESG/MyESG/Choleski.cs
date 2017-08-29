using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyESG
{
    class Cholesky
    {
        public static double[,] CholeskyDecomp(double[,] matrix)
        {
            int size = matrix.Length;

            double[,] L = new double[size, size];

            L[0, 0] = Math.Sqrt(matrix[0, 0]);

            for (int i = 0; i<size;i++)
            {
                for (int j=0;j<size;j++)
                {
                    L[i, j] = 0.0;
                }
            }

            double sumk = 0.0;
            double sumj = 0.0;

            for (int i=0; i<size; i++)
            {
                sumk = 0;
                for (int k=0; k<i; k++)
                {
                    sumk += Math.Pow(L[i, k], 2.0);
                }

                L[i, i] = Math.Sqrt(matrix[i, i] - sumk);

                for (int j=i+1; j<size; j++)
                {
                    sumj = 0.0;
                    for (int k=0; k < i; k++)
                    {
                        sumj += L[i, k] * L[j, k];
                    }

                    L[j, i] = (matrix[j, i] - sumj) / L[i, i];
                }
            }

            return L;
        }
    }
}
