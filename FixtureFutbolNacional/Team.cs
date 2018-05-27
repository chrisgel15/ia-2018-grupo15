using System.Collections.Generic;

namespace FixtureFutbolNacional
{
    partial class Program
    {
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

        public class PossibleMatches
        {
            public List<Match> Matches { get; set; }

            public PossibleMatches()
            {
                Matches = new List<Match>()
                {
                    new Match(Team.Velez, Team.Argentinos),
                    new Match(Team.River, Team.Belgrano),
                    new Match(Team.Boca, Team.Racing),
                    new Match(Team.Ferro, Team.Central),
                    new Match(Team.Racing, Team.Central),
                    new Match(Team.Velez, Team.Belgrano),
                    new Match(Team.Ferro, Team.River),
                    new Match(Team.Argentinos, Team.Boca),
                    new Match(Team.Racing, Team.Argentinos),
                    new Match(Team.Central, Team.River),
                    new Match(Team.Ferro, Team.Belgrano),
                    new Match(Team.Boca, Team.Velez),
                    new Match(Team.Ferro, Team.Boca),
                    new Match(Team.Racing, Team.River),
                    new Match(Team.Velez, Team.Central),
                    new Match(Team.Belgrano, Team.Argentinos),
                    new Match(Team.Velez, Team.River),
                    new Match(Team.Argentinos, Team.Ferro),
                    new Match(Team.Belgrano, Team.Racing),
                    new Match(Team.Boca, Team.Central),
                    new Match(Team.Boca, Team.River),
                    new Match(Team.Boca, Team.Velez),
                    new Match(Team.River, Team.Argentinos),
                    new Match(Team.Racing, Team.Velez),
                    new Match(Team.Racing, Team.Ferro),
                    new Match(Team.Argentinos, Team.Central),
                    new Match(Team.Ferro, Team.Velez),
                    new Match(Team.Central, Team.Belgrano)
                };
            }
        }
    }
}
