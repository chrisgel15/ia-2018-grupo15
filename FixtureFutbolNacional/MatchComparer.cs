using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FixtureFutbolNacional.Program;

namespace FixtureFutbolNacional
{
    partial class Program
    {
        public class MatchComparer : IEqualityComparer<Match>
        {
            public bool Equals(Match x, Match y)
            {
                var returnValue = (x.Local == y.Local || x.Local == y.Visitante) &&
                    (x.Visitante == y.Local || x.Visitante == y.Visitante);

                return returnValue;

            }

            public int GetHashCode(Match obj)
            {           
                return base.GetHashCode();  
            }
        }

    }
    
}
