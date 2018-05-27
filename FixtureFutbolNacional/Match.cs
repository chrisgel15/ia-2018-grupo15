using System;
using System.Collections.Generic;

namespace FixtureFutbolNacional
{
    partial class Program
    {
        public class Match : IEquatable<Match>
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

            public bool Equals(Match otherMatch)
            {
                var returnValue = (this.Local == otherMatch.Local || this.Local == otherMatch.Visitante) &&
                    (this.Visitante == otherMatch.Local || this.Visitante == otherMatch.Visitante);

                return returnValue;
            }
            
        }
    }
}
