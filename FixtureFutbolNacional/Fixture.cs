using System;
using System.Collections.Generic;
using System.Linq;

namespace FixtureFutbolNacional
{
    partial class Program
    {
        public class Fixture : IComparable<Fixture>
        {
            public List<Round> Rounds { get; set; }
            public int BocaAndRiverSixRoundAptitude { get; set; }
            public int BocaAndRiverBothHomeAptitude { get; set; }
            public int OnlyOneMatchForBigTeamOnSaturdayAptitude { get; set; }
            public int NoDerbiesOnSaturdayAptitude { get; set; }
            public int OneDerbyPerRoundAptitude { get; set; }
            public int DistinctMatchesAptitude { get; set; }

            public int AptitudeValue
            {
                get
                {
                    return BocaAndRiverSixRoundAptitude +
                   BocaAndRiverBothHomeAptitude +
                   OnlyOneMatchForBigTeamOnSaturdayAptitude +
                   NoDerbiesOnSaturdayAptitude +
                   OneDerbyPerRoundAptitude +
                   DistinctMatchesAptitude;
                }
            }

            int AttemptsToGenerate;         

            public Fixture()
            {
                this.Rounds = new List<Round>();
            }

            public bool IsValid()
            {
                return ValidValue() == 28;
            }

            internal void ResetAptitudeValue()
            {
                BocaAndRiverBothHomeAptitude = 0;
                BocaAndRiverSixRoundAptitude = 0;
                OnlyOneMatchForBigTeamOnSaturdayAptitude = 0;
                NoDerbiesOnSaturdayAptitude = 0;
                OneDerbyPerRoundAptitude = 0;
                DistinctMatchesAptitude = 0;
            }

            internal int ValidValue()
            {
                List<Match> matches = new List<Match>();
                foreach (Round r in Rounds)
                    matches.AddRange(r.Matches);

                return matches.Distinct(new MatchComparer()).Count();
            }

            internal void ValidateDistinctMatchesAptitude()
            {
                if (ValidValue() == 28) this.DistinctMatchesAptitude += 20;
                if (ValidValue() >= 20 && ValidValue() < 28) this.DistinctMatchesAptitude += 15;
                if (ValidValue() >= 12 && ValidValue() < 20) this.DistinctMatchesAptitude += 10;
                if (ValidValue() >= 0 && ValidValue() < 12) this.DistinctMatchesAptitude += 0;
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

            #region Aptitude Methods

            internal void ValidateBocaRiverSixRound()
            {
                if (this.Rounds[5].ContainsMatch(new Match(Team.Boca, Team.River)))
                    this.BocaAndRiverSixRoundAptitude += 15;
                else
                    this.BocaAndRiverSixRoundAptitude -= 5;
            }

            internal void ValidateBocaRiverNotBothHomeTeam()
            {
                foreach (Round r in Rounds)
                {
                    int bothHome = 0;
                    foreach (Match m in r.Matches)
                    {
                        if (m.Local == Team.Boca || m.Local == Team.River)
                            bothHome++;
                    }

                    if (bothHome == 2)
                        this.BocaAndRiverBothHomeAptitude -= 3;
                }
            }

            internal void ValidateOnlyOneMatchForBigTeamOnSaturday()
            {
                foreach (Round r in Rounds)
                {
                    var count = 0;

                    foreach (var sm in r.SaturdayMatches())
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
                    foreach (Match m in r.SundayMatches())
                    {
                        if (m.IsDerby())
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

            #endregion           

            public int CompareTo(Fixture other)
            {
                if (this.AptitudeValue > other.AptitudeValue)
                    return -1;
                else
                    return 1;
            }

            public void MixWithOther(Fixture other)
            {
                // 1 con 2
                Round aux1 = other.GetRound1();
                Round aux2 = GetRound2();                
                other.AddRound1(aux2);
                AddRound2(aux1);
                //

                // 5 con 6
                aux1 = other.GetRound5();
                aux2 = GetRound6();
                other.AddRound5(aux2);
                AddRound6(aux1);
                //
            }

            private Round GetRoundByIndex(int index)
            {
                return Rounds[index];
            }

            public Round GetRound1() { return GetRoundByIndex(0); }
            public Round GetRound2() { return GetRoundByIndex(1); }
            public Round GetRound3() { return GetRoundByIndex(2); }
            public Round GetRound4() { return GetRoundByIndex(3); }
            public Round GetRound5() { return GetRoundByIndex(4); }
            public Round GetRound6() { return GetRoundByIndex(5); }
            public Round GetRound7() { return GetRoundByIndex(6); }

            private void AddRoundByIndex(Round r, int index)
            {
                Rounds[index] = r;
            }

            public void AddRound1(Round r) { AddRoundByIndex(r, 0); }
            public void AddRound2(Round r) { AddRoundByIndex(r, 1); }
            public void AddRound3(Round r) { AddRoundByIndex(r, 2); }
            public void AddRound4(Round r) { AddRoundByIndex(r, 3); }
            public void AddRound5(Round r) { AddRoundByIndex(r, 4); }
            public void AddRound6(Round r) { AddRoundByIndex(r, 5); }
            public void AddRound7(Round r) { AddRoundByIndex(r, 6); }            


            #region Print methods
            public void PrintFixture()
            {
                foreach (Round r in Rounds)
                    this.PrintsRound(r);
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

            internal void PrintAptitude()
            {
                Console.WriteLine("\n # Aptitudes Summary #");
                Console.WriteLine(" BocaAndRiverSixRoundAptitude: " + this.BocaAndRiverSixRoundAptitude);
                Console.WriteLine(" BocaAndRiverBothHomeAptitude: " + this.BocaAndRiverBothHomeAptitude);
                Console.WriteLine(" OnlyOneMatchForBigTeamOnSaturdayAptitude: " + this.OnlyOneMatchForBigTeamOnSaturdayAptitude);
                Console.WriteLine(" OneDerbyPerRoundAptitude: " + this.OneDerbyPerRoundAptitude);
                Console.WriteLine(" NoDerbiesOnSaturdayAptitude: " + this.NoDerbiesOnSaturdayAptitude);
                Console.WriteLine(" DistinctMatchesAptitude: " + this.DistinctMatchesAptitude);
                Console.WriteLine("\n Total Aptitude: " + this.AptitudeValue);
            }

            #endregion

        }
    }
}
