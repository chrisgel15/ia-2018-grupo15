using System;

namespace FixtureFutbolNacional
{
    public static class RandomValue
    {
        //public static Random randomValue = new Random(DateTime.Now.Millisecond);

        private static Random random;
        private static object syncObj = new object();

        private static void InitRandomNumber(int seed)
        {
         //   Thread.Sleep(40);
            random = new Random(seed);
        }

        public static int GenerateRandomNumber(int min, int max)
        {
            lock(syncObj)
            {
                if (random == null)
                {
                 //   Thread.Sleep(30);
                    random = new Random(); // Or exception...

                }
               // Thread.Sleep(2);  
                return random.Next(min, max);
            }
        }

        public static void ReNew()
        {
            InitRandomNumber(DateTime.Now.Millisecond);
        }
    }
}
