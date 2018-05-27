using System.Collections.Generic;
using System.Linq;

namespace FixtureFutbolNacional
{
    partial class Program
    {
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
    }
}
