using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
    class Program
    {
    //    const int MAX_ROUNDS = 7;
    //    const int MAX_TEAMS = 8;
    //    const int MIN_VALUE = 1;
    //    const int MAX_VALUE = 9;
    //    const int POPULATION = 5;
        static void Main(string[] args)
        {
            #region Generation

            List<Fixture> population = new List<Fixture>();
            Fixture f = FixtureGenerator.GenerateFixture();
            population.Add(f);

            Console.WriteLine("TOTAL FIXTURES: " + population.Count);

            f = FixtureGenerator.GenerateFixture();
            population.Add(f);

            Console.WriteLine("TOTAL FIXTURES: " + population.Count);


            f = FixtureGenerator.GenerateFixture();
            population.Add(f);

            Console.WriteLine("TOTAL FIXTURES: " + population.Count);


            f = FixtureGenerator.GenerateFixture();
            population.Add(f);

            Console.WriteLine("TOTAL FIXTURES: " + population.Count);


            f = FixtureGenerator.GenerateFixture();
            population.Add(f);

            Console.WriteLine("TOTAL FIXTURES: " + population.Count);


            f = FixtureGenerator.GenerateFixture();
            population.Add(f);

            Console.WriteLine("TOTAL FIXTURES: " + population.Count);

            
            #endregion

            foreach (Fixture fix in population)
            {
                fix.ValidateBocaRiverSixRound();
                fix.ValidateBocaRiverNotBothHomeTeam();
                fix.ValidateOnlyOneMatchForBigTeamOnSaturday();
                fix.ValidateNoDerbiesOnSaturday();
                fix.ValidateOneDerbyPerRound();

                fix.PrintAptitude();
            }

            Console.ReadKey();


            //while (population.Count < POPULATION)
            //{
            //    Fixture f = FixtureGenerator.GenerateFixture();
            //    population.Add(f);

            //    Console.WriteLine("TOTAL FIXTURES: " + population.Count);
            //}
        }

        

        public enum Team
        {
            Boca = 1,
            River = 2,
            Racing = 3,
            Argentinos = 4,
            Velez = 5,
            Ferro = 6,
            Central = 7,
            Belgrano = 8
        }

        public class Round
        {
            const int MAX_MATCHES = 8;
            public List<Match> Matches { get; set; }

            public Round(Match firstMatch, Match secondMatch, Match thirdMatch, Match fourthMatch)
            {
                Matches = new List<Match>();
                Matches.Add(firstMatch);
                Matches.Add(secondMatch);
                Matches.Add(thirdMatch);
                Matches.Add(fourthMatch);
            }

            public bool IsValid()
            {
                return Teams().Distinct().Count() == MAX_MATCHES;
            }

            private List<Team> Teams()
            {
                List<Team> teams = new List<Team>();

                foreach (Match m in Matches)
                {
                    teams.Add(m.Local);
                    teams.Add(m.Visitante);
                }

                return teams;
            }

            private Match GetMatchByIndex(int index)
            {
                return Matches[index];
            }

            public Match GetFirstMatch()
            {
                return GetMatchByIndex(0);
            }

            public Match GetSecondMatch()
            {
                return GetMatchByIndex(1);
            }

            public Match GetThirdMatch()
            {
                return GetMatchByIndex(2);
            }

            public Match GetFourthMatch()
            {
                return GetMatchByIndex(2);
            }

            public bool ContainsMatch(Match match)
            {
                return Matches.Contains(match);
            }

            public List<Match> SaturdayMatches()
            {
                return new List<Match>() { this.GetFirstMatch(), this.GetSecondMatch() };
            }

            internal IEnumerable<Match> SundayMatches()
            {
                return new List<Match>() { this.GetThirdMatch(), this.GetFourthMatch() };
            }
        }

        public class Match
        {
            private static List<Match> Derbies = new List<Match>()
            {
                new Match(Team.Boca, Team.River),
                new Match(Team.Boca, Team.Racing),
                new Match(Team.River, Team.Racing),
                new Match(Team.Argentinos, Team.Velez),
                new Match(Team.Argentinos, Team.Ferro),
                new Match(Team.Velez, Team.Ferro),
                new Match(Team.Central, Team.Belgrano)
            };
            public Team Local { get; private set; }
            public Team Visitante { get; private set; }

            public Match(Team local, Team visitante)
            {
                this.Local = local;
                this.Visitante = visitante;
            }

            public override bool Equals(object obj)
            {
                Match otherMatch;
                try { otherMatch = (Match)obj; }
                catch (Exception ex) { return false; }

                var returnValue = (this.Local == otherMatch.Local || this.Local == otherMatch.Visitante) &&
                    (this.Visitante == otherMatch.Local || this.Visitante == otherMatch.Visitante);

                return returnValue;
            }

            internal void PrintMatch()
            {
                Console.Write(" ~" +
                    this.Local.ToString() + " vs " + this.Visitante.ToString() + "~ ");
            }

            internal bool IsDerby()
            {
                return Derbies.Contains(this);
            }
        }

        public class MatchAndCount
        {
            public Match match { get; set; }
            public int amount { get; set; }
        }

        public class RoundGenerator
        {
            // static Random random = new Random(DateTime.Now.Millisecond * DateTime.Now.Second);

            public static List<MatchAndCount> matchesInRound = new List<MatchAndCount>();
            

            public static Round GenerateRound()
            {
                List<Team> teams = new List<Team> { Team.Argentinos, Team.Belgrano, Team.Boca, Team.Central, Team.Ferro, Team.Racing,
                                Team.River, Team.Velez };

                Team local = GetRandomTeamFromList(teams);
                Team away = GetRandomTeamFromList(teams);

                Match firstMatch = new Match(local, away);

                AddMatchIfExist(firstMatch);

                local = GetRandomTeamFromList(teams);
                away = GetRandomTeamFromList(teams);

                Match secondMatch = new Match(local, away);

                AddMatchIfExist(secondMatch);

                local = GetRandomTeamFromList(teams);
                away = GetRandomTeamFromList(teams);

                Match thirdMatch = new Match(local, away);

                AddMatchIfExist(thirdMatch);

                local = GetRandomTeamFromList(teams);
                away = GetRandomTeamFromList(teams);

                Match fourthMatch = new Match(local, away);

                AddMatchIfExist(fourthMatch);

                return new Round(firstMatch, secondMatch, thirdMatch, fourthMatch);
            }

            private static void AddMatchIfExist(Match match)
            {
                foreach(MatchAndCount mc in matchesInRound)
                {
                    if (mc.match.Equals(match))
                    {
                        mc.amount++;
                        return;
                    }                       
                }
                MatchAndCount mac = new MatchAndCount();
                mac.match = match;
                mac.amount = 0;
                matchesInRound.Add(mac);
            }

            private static Team GetRandomTeamFromList(List<Team> teams)
            {
                const int MIN_VALUE = 100000;
                const int MAX_VALUE = 900000;
                
                
                //var possibleTeam = (Team)DivideValue(RandomValue.randomValue.Next(MIN_VALUE, MAX_VALUE));
                var possibleTeam = (Team)DivideValue(RandomValue.GenerateRandomNumber(MIN_VALUE, MAX_VALUE));

                while (!teams.Contains(possibleTeam))
                {
                    //possibleTeam = (Team)DivideValue(RandomValue.randomValue.Next(MIN_VALUE, MAX_VALUE));
                    possibleTeam = (Team)DivideValue(RandomValue.GenerateRandomNumber(MIN_VALUE, MAX_VALUE));
                }

                teams.Remove(possibleTeam);

                return possibleTeam;
            }

            public static Team DivideValue(int v)
            {
                const int DIVIDE_VALUE = 100000;
                int div = (v / DIVIDE_VALUE);
                return (Team) div;
            }

            internal static void CleanList()
            {
                matchesInRound.Clear();
            }
        }

        public class Fixture
        {
            public List<Round> Rounds { get; set; }

            public int BocaAndRiverSixRoundAptitude { get; set; }
            public int BocaAndRiverBothHomeAptitude { get; set; }
            public int OnlyOneMatchForBigTeamOnSaturdayAptitude { get; set; }
            public int NoDerbiesOnSaturdayAptitude { get; set; }
            public int OneDerbyPerRoundAptitude { get; set; }


            public int AptitudeValue { get {
                    return BocaAndRiverBothHomeAptitude +
                   BocaAndRiverBothHomeAptitude +
                   OnlyOneMatchForBigTeamOnSaturdayAptitude +
                   NoDerbiesOnSaturdayAptitude +
                   OneDerbyPerRoundAptitude; } }

            int AttemptsToGenerate;

            public Fixture()
            {
                this.Rounds = new List<Round>();
            }

            internal bool ContainsRound(Round possibleRound)
            {
                foreach (Match m in possibleRound.Matches)
                {
                    foreach (Round r in this.Rounds)
                    {
                        if (r.ContainsMatch(m))
                        {
                            //PrintContainingMatch(m);
                            AttemptsToGenerate++;
                            if (AttemptsToGenerate > 1000)
                            {
                                AttemptsToGenerate = 0;
                                RandomValue.ReNew();

                            }
                            return true;
                        }

                    }
                }

                return false;
            }

            private static void PrintContainingMatch(Match m)
            {
                Console.Write("\t Round already contains: ");
                m.PrintMatch();
            }

            internal void PrintsRound(Round firstRound)
            {
                //Console.Write("\nRound:" + this.Rounds.Count + "\n");
                Console.Write("\n");
                foreach (Match m in firstRound.Matches)
                {
                    m.PrintMatch();
                }

            }

            internal void ValidateBocaRiverSixRound()
            {
                if (this.Rounds[5].ContainsMatch(new Match(Team.Boca, Team.River)))
                    this.BocaAndRiverSixRoundAptitude += 15;
                else
                    this.BocaAndRiverSixRoundAptitude -= 5;
            }

            internal void ValidateBocaRiverNotBothHomeTeam()
            {
                foreach(Round r in Rounds)
                {
                    int bothHome = 0;
                    foreach(Match m in r.Matches)
                    {
                        if (m.Local == Team.Boca || m.Local == Team.River)
                            bothHome++;
                    }

                    if(bothHome == 2)
                        this.BocaAndRiverBothHomeAptitude -= 3;                   
                }
            }

            internal void ValidateOnlyOneMatchForBigTeamOnSaturday()
            {
                foreach (Round r in Rounds)
                {
                    var count = 0;

                    foreach(var sm in r.SaturdayMatches())
                    {
                        if (sm.Local.Equals(Team.Boca) || sm.Local.Equals(Team.Racing) || sm.Local.Equals(Team.River))
                            count++;
                        if (sm.Visitante.Equals(Team.Boca) || sm.Visitante.Equals(Team.Racing) || sm.Visitante.Equals(Team.River))
                            count++;
                    }

                    if (count == 0)
                        this.OnlyOneMatchForBigTeamOnSaturdayAptitude += 3;
                    if (count == 1)
                        this.OnlyOneMatchForBigTeamOnSaturdayAptitude += 3;
                    if (count == 2)
                        this.OnlyOneMatchForBigTeamOnSaturdayAptitude -= 5;
                    if (count > 2)
                        this.OnlyOneMatchForBigTeamOnSaturdayAptitude -= 15;
                   
                }
                              
            }

            internal void ValidateNoDerbiesOnSaturday()
            {
                foreach (Round r in Rounds)
                {
                    int derbiesCount = 0;
                    foreach (Match m in r.SaturdayMatches())
                    {
                        if (m.IsDerby())
                            derbiesCount++;
                    }

                    if (derbiesCount == 0)
                        this.NoDerbiesOnSaturdayAptitude += 3;
                    if (derbiesCount == 1)
                        this.NoDerbiesOnSaturdayAptitude -= 5;
                    if (derbiesCount == 2)
                        this.NoDerbiesOnSaturdayAptitude -= 10;
                }
            }

            internal void ValidateOneDerbyPerRound()
            {
                int countDerbies = 0;
                foreach (Round r in Rounds)
                {                    
                    foreach(Match m in r.SundayMatches())
                    {
                        if(m.IsDerby())
                        {
                            countDerbies++;
                            break;
                        }
                    }
                }
                if (countDerbies == 0) { this.OneDerbyPerRoundAptitude -= 15; return; }
                if (countDerbies == 1) { this.OneDerbyPerRoundAptitude -= 12; return; }
                if (countDerbies == 2) { this.OneDerbyPerRoundAptitude -= 8; return; }
                if (countDerbies == 3) { this.OneDerbyPerRoundAptitude -= 5; return; }
                if (countDerbies == 4) { this.OneDerbyPerRoundAptitude -= 0; return; }
                if (countDerbies == 5) { this.OneDerbyPerRoundAptitude += 15; return; }
                if (countDerbies == 6) { this.OneDerbyPerRoundAptitude += 25; return; }
                if (countDerbies == 7) { this.OneDerbyPerRoundAptitude += 35; return; }
                if (countDerbies == 8) { this.OneDerbyPerRoundAptitude += 50; return; }
            }

            internal void PrintAptitude()
            {
                Console.WriteLine();
                Console.WriteLine("BocaAndRiverSixRoundAptitude: " + this.BocaAndRiverSixRoundAptitude);
                Console.WriteLine("BocaAndRiverBothHomeAptitude: " + this.BocaAndRiverBothHomeAptitude);
                Console.WriteLine("OnlyOneMatchForBigTeamOnSaturdayAptitude: " + this.OnlyOneMatchForBigTeamOnSaturdayAptitude);
                Console.WriteLine("OneDerbyPerRoundAptitude: " + this.OneDerbyPerRoundAptitude);
                Console.WriteLine("NoDerbiesOnSaturdayAptitude: " + this.NoDerbiesOnSaturdayAptitude);
                Console.WriteLine("Aptitud: " + this.AptitudeValue);
            }
        }

        public static class FixtureGenerator
        {
            const int MAX_ROUNDS = 7;
            public static Fixture GenerateFixture()
            {
                Fixture fixture = new Fixture();

                Round firstRound = RoundGenerator.GenerateRound();
                fixture.Rounds.Add(firstRound);
                fixture.PrintsRound(firstRound);

                while (fixture.Rounds.Count < MAX_ROUNDS)
                {
                    Round possibleRound = RoundGenerator.GenerateRound();

                    if (!fixture.ContainsRound(possibleRound))
                    {
                        fixture.Rounds.Add(possibleRound);
                        fixture.PrintsRound(possibleRound);                        
                    }

                }
                RoundGenerator.CleanList();
                return fixture;
            }
        }
    }
}
