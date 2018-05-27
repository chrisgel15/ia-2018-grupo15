using System;

namespace FixtureFutbolNacional
{
    partial class Program
    {
        public static bool enablePrint = false;
        public static class FixtureGenerator
        {
            const int MAX_ROUNDS = 7;
            public static Fixture GenerateFixture()
            {
                Fixture fixture = new Fixture();
                PossibleMatches possibleMatches = new PossibleMatches();

                Round firstRound = RoundGenerator.GenerateRound();
                fixture.Rounds.Add(firstRound);
                PrintRound(fixture, firstRound);
                

                foreach (Match m in firstRound.Matches) possibleMatches.Matches.Remove(m);

                int attemptsToGenerateRound = 0;

                while (fixture.Rounds.Count < MAX_ROUNDS)
                {
                    Round possibleRound = RoundGenerator.GenerateRound();

                    if (!fixture.ContainsRound(possibleRound))
                    {
                        attemptsToGenerateRound = 0;
                        fixture.Rounds.Add(possibleRound);
                        PrintRound(fixture, possibleRound);
                        foreach (Match m in possibleRound.Matches) possibleMatches.Matches.Remove(m);
                    }
                    else
                    {
                        attemptsToGenerateRound++;
                        if (attemptsToGenerateRound > 1000)
                        {
                            Round remainingRound = RoundGenerator.GenerateRoundFromList(possibleMatches.Matches);
                            fixture.Rounds.Add(remainingRound);
                            PrintRound(fixture, remainingRound, true);                            
                            attemptsToGenerateRound = 0;
                        }

                    }

                }
                RoundGenerator.CleanList();

                if (!fixture.IsValid())
                    PrintInvalidFixtureMessage();

                return fixture;
            }

            private static void PrintInvalidFixtureMessage()
            {
                if (enablePrint)
                    Console.WriteLine("\tFIXTURE IS NOT VALID!!!");
            }

            private static void PrintRound(Fixture fixture, Round remainingRound, bool v)
            {
                if (enablePrint)
                {
                    PrintRound(fixture, remainingRound);
                    if (v) Console.Write("***");
                }
                
            }

            private static void PrintRound(Fixture fixture, Round round)
            {
                if (enablePrint)
                    fixture.PrintsRound(round);
            }
        }            
    }
}
