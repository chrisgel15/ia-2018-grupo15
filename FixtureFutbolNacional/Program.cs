using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FixtureFutbolNacional
{
    partial class Program
    {
        const int POPULATION = 20000;
        const int ITERATIONS = 10;
        static void Main(string[] args)
        {

            List<Fixture> population = new List<Fixture>();

            while (population.Count < POPULATION)
            {
                Fixture f = FixtureGenerator.GenerateFixture();
                population.Add(f);
            }

            Console.WriteLine("\n TOTAL FIXTURES: " + population.Count);

            for (int j = 0; j < ITERATIONS; j++)
            {
                // Validate
                Validate(population);

                // Select
                population = RankingSelection.Select(population);

                PrintParcialInformation(population);

                if (population.Count < 200)
                    break;

                // Mix
                for (int i = 0; i < population.Count - 1; i += 2)
                {
                    population[i].MixWithOther(population[i + 1]);
                }

                for (int i = 0; i < population.Count; i++)
                    population[i].ResetAptitudeValue();
            }

            PrintFinalInformation(population);

            Console.ReadKey();
        }

        private static void PrintFinalInformation(List<Fixture> population)
        {
            Console.WriteLine("\n\n\n *** FINAL INFORMATION ***");
            Console.WriteLine("\n BEST FIXTURE ");
            PrintInfo(population);
        }

        private static void PrintInfo(List<Fixture> population)
        {
            population.First().PrintFixture();
            Console.WriteLine("\n");
            population.First().PrintAptitude();
        }

        private static void PrintParcialInformation(List<Fixture> population)
        {
            Console.WriteLine(" *** Partial Information ***");
            Console.WriteLine(" Population count:" + population.Count);
            PrintInfo(population);
        }

        private static void Validate(List<Fixture> population)
        {
            foreach (Fixture fix in population)
            {
                fix.ValidateBocaRiverSixRound();
                fix.ValidateBocaRiverNotBothHomeTeam();
                fix.ValidateOnlyOneMatchForBigTeamOnSaturday();
                fix.ValidateNoDerbiesOnSaturday();
                fix.ValidateOneDerbyPerRound();
                fix.ValidateDistinctMatchesAptitude();

                //fix.PrintAptitude();
            }
        }
    }
}
