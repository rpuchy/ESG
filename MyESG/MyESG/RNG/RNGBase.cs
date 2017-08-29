using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyESG
{
    abstract class RNGBase
    {

        abstract public double GenerateUniform();
        
        public double GenerateNormal()
        {
            double U, u, v, S;
            do
            {
                u = 2.0 * GenerateUniform() - 1.0;
                v = 2.0 * GenerateUniform() - 1.0;
                S = u * u + v * v;
            }
            while (S >= 1.0);

            double fac = Math.Sqrt(-2.0 * Math.Log(S) / S);
            return u * fac;
        }


        public int GeneratePoisson(double lambda)
        {
            return (lambda < 30.0) ? PoissonSmall(lambda) : PoissonLarge(lambda);
        }

        private int PoissonSmall(double lambda)
        {
            // Algorithm due to Donald Knuth, 1969.
            double p = 1.0, L = Math.Exp(-lambda);
            int k = 0;
            do
            {
                k++;
                p *= GenerateUniform();
            }
            while (p > L);
            return k - 1;
        }

        private int PoissonLarge(double lambda)
        {
            // "Rejection method PA" from "The Computer Generation of 
            // Poisson Random Variables" by A. C. Atkinson,
            // Journal of the Royal Statistical Society Series C 
            // (Applied Statistics) Vol. 28, No. 1. (1979)
            // The article is on pages 29-35. 
            // The algorithm given here is on page 32.

            double c = 0.767 - 3.36 / lambda;
            double beta = Math.PI / Math.Sqrt(3.0 * lambda);
            double alpha = beta * lambda;
            double k = Math.Log(c) - lambda - Math.Log(beta);

            for (;;)
            {
                double u = GenerateUniform();
                double x = (alpha - Math.Log((1.0 - u) / u)) / beta;
                int n = (int)Math.Floor(x + 0.5);
                if (n < 0)
                    continue;
                double v = GenerateUniform();
                double y = alpha - beta * x;
                double temp = 1.0 + Math.Exp(y);
                double lhs = y + Math.Log(v / (temp * temp));
                double rhs = k + n * Math.Log(lambda) - Utility.LogFactorial(n);
                if (lhs <= rhs)
                    return n;
            }
        }

    }
}
