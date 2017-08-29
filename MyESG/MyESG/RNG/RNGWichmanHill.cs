using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyESG
{
    class RNGWichmanHill : RNGBase, IRandomNumberGenerator
    {
        int Seed1;
        int Seed2;
        int Seed3;

        int s1;
        int s2;
        int s3;

        public void Reset()
        {
            this.s1 = Seed1;
            this.s2 = Seed2;
            this.s3 = Seed3;
        }

        public RNGWichmanHill(int seed) : this(seed,seed+1,seed+2)
        {

        }


        public RNGWichmanHill(int Seed1, int Seed2, int Seed3)
        {
            this.Seed1 = Seed1;
            this.Seed2 = Seed2;
            this.Seed3 = Seed3;
            this.s1 = Seed1;
            this.s2 = Seed2;
            this.s3 = Seed3;
        }

        public override double GenerateUniform()
        {
            s1 = 171 * (s1 % 177) - 2 * (s1 / 177);
            if (s1 < 0) { s1 += 30269; }
            s2 = 172 * (s2 % 176) - 35 * (s2 / 176);
            if (s2 < 0) { s2 += 30307; }
            s3 = 170 * (s3 % 178) - 63 * (s3 / 178);
            if (s3 < 0) { s3 += 30323; }
            double r = (s1 * 1.0) / 30269 + (s2 * 1.0) / 30307 + (s3 * 1.0) / 30323;
            return r - Math.Truncate(r);  // orig uses % 1.0
        }
    }
}
