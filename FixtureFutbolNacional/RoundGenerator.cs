using System;
using System.Collections.Generic;
using System.Linq;

namespace FixtureFutbolNacional
{
    partial class Program
    {
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

            internal static Round GenerateRoundFromList(List<Match> matches)
            {
                Match match1 = matches.First();
                matches.RemoveAt(0);

                Match match2 = matches.First();
                matches.RemoveAt(0);

                Match match3 = matches.First();
                matches.RemoveAt(0);

                Match match4 = matches.First();
                matches.RemoveAt(0);

                return new Round(match1, match2, match3, match4);

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
    }
}
