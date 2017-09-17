using System;

namespace AI.MathMod
{
    public static class RandomUtilities
    {
        private static readonly Random random = new Random();
        private static double val;
        private static bool returnVal = false;

        public static int Seed
        {
            get { return (int) DateTime.Now.Ticks; }
        }

        public static double GaussianRandom()
        {
           if (returnVal)
            {
                returnVal = false;
                return val;
            }

            var u = 2*random.NextDouble() - 1;
            var v = 2*random.NextDouble() - 1;
            var r = u*u + v*v;

            if (r == 0 || r > 1)
            {
                return GaussianRandom();
            }

            var c = Math.Sqrt(-2*Math.Log(r)/r);
            val = v*c; 
            returnVal = true;

            return u*c;
        }

        public static double Randn(double mu, double std)
        {
        	return mu +GaussianRandom()*std;
        }
    }
}