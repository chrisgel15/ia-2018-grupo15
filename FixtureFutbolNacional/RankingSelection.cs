using System.Collections.Generic;
using System.Linq;

namespace FixtureFutbolNacional
{
    partial class Program
    {
        public static class RankingSelection
        {
            public static List<Fixture> Select(List<Fixture> allFixtures)
            {
                List<Fixture> selectedFixtures = new List<Fixture>();

                foreach(Fixture f in allFixtures)
                {
                    if (f.IsValid())
                        selectedFixtures.Add(f);
                }

                selectedFixtures.Sort();

                int best = 0;
                int last = 0;
                int cut = 0;
                if (selectedFixtures.Count > 0)
                {
                    best = selectedFixtures.First().AptitudeValue;
                    last = selectedFixtures.Last().AptitudeValue;
                    cut = (best + last) / 2;
                }
                else
                    cut = -1000;

                

                return selectedFixtures.Where(f => f.AptitudeValue >= cut).ToList();                
            }
        }
            
    }
}
