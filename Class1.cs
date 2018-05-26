#region Get Rounds Methods
private static Team[] GetRoundByIndex(Team[,] fixture, int index)
{
    Team[] teams = new Team[MAX_TEAMS];

    for (int i = 0; i < MAX_TEAMS; i++)
    {
        teams[i] = fixture[index, i];
    }

    return teams;
}

private static Team[] GetRoundOne(Team[,] fixture)
{
    return GetRoundByIndex(fixture, 0);
}

private static Team[] GetRoundTwo(Team[,] fixture)
{
    return GetRoundByIndex(fixture, 1);
}

private static Team[] GetRoundThree(Team[,] fixture)
{
    return GetRoundByIndex(fixture, 2);
}

private static Team[] GetRoundFour(Team[,] fixture)
{
    return GetRoundByIndex(fixture, 3);
}

private static Team[] GetRoundFive(Team[,] fixture)
{
    return GetRoundByIndex(fixture, 4);
}

private static Team[] GetRoundSix(Team[,] fixture)
{
    return GetRoundByIndex(fixture, 5);
}

private static Team[] GetRoundSeven(Team[,] fixture)
{
    return GetRoundByIndex(fixture, 6);
}

#endregion



private static void CreatePopulation(List<Team[,]> population)
{
    int i = 0;
    for (i = 0; i < POPULATION; i++)
    {
        Team[,] fixture = new Team[MAX_ROUNDS, MAX_TEAMS];
        CreateRandomFixture(fixture);
        population.Add(fixture);
    }
}

private static void CreateRandomFixture(Team[,] fixture)
{
    int i = 0, j = 0;
    Random random = new Random(DateTime.Now.Millisecond);
    for (i = 0; i < MAX_ROUNDS; i++)
    {
        for (j = 0; j < MAX_TEAMS; j++)
        {
            var nextTeam = (Team)random.Next(MIN_VALUE, MAX_VALUE);

            while (RoundContainsTeam(GetRoundByIndex(fixture, i), nextTeam))
                nextTeam = (Team)random.Next(MIN_VALUE, MAX_VALUE);

            fixture[i, j] = nextTeam;
        }

    }

}

private static bool BocaAndRiverRoundSix(Team[,] fixture)
{
    var roundSix = GetRoundSix(fixture);
    return RoundContainsMatch(roundSix, Team.Boca, Team.River);
}

private static bool MatchContainsTeams(Team[] match, Team teamA, Team teamB)
{
    return (match[0] == teamA && match[1] == teamB) ||
            (match[0] == teamB && match[1] == teamA);
}

private static bool RoundContainsMatch(Team[] round, Team teamA, Team teamB)
{
    return MatchContainsTeams(GetFirstMatch(round), teamA, teamB)
    || MatchContainsTeams(GetSecondMatch(round), teamA, teamB)
    || MatchContainsTeams(GetThirdMatch(round), teamA, teamB)
    || MatchContainsTeams(GetFourthMatch(round), teamA, teamB);
}

private static bool RoundContainsTeam(Team[] round, Team team)
{
    int index = 0;

    while (index < round.Length)
    {
        if (round[index] == team)
            return true;

        index++;
    }

    return false;
}

#region Get Matches Methods
private static Team[] GetFirstMatch(Team[] round)
{
    return new Team[] { round[0], round[1] };
}

private static Team[] GetSecondMatch(Team[] fixture)
{
    return new Team[] { fixture[2], fixture[3] };
}

private static Team[] GetThirdMatch(Team[] fixture)
{
    return new Team[] { fixture[4], fixture[5] };
}

private static Team[] GetFourthMatch(Team[] fixture)
{
    return new Team[] { fixture[6], fixture[7] };
}
#endregion

private static bool FixtureIsValid(Team[,] fixture)
{
    for (int i = 0; i < MAX_ROUNDS; i++)
    {
        var round = GetRoundByIndex(fixture, i);
        if (!RoundIsValid(round))
        {
            return false;
        }
    }

    return true;
    for (int i = 0; i < fixture.Length; i += 2)
    {

    }
}

private static Team[] GetNextMatch(Team[,] fixture, int indexI, int indexJ)
{
    return new Team[] { fixture[indexI, indexJ], fixture[indexI, indexJ] };
}

private static bool RoundIsValid(Team[] round)
{
    var x = round.Distinct().ToList();
    return x.Count == MAX_TEAMS;
}

private static bool MatchesAreEqual(Team[] firstMatch, Team[] secondMatch)
{
    return firstMatch[0] == secondMatch[0] || firstMatch[0] == secondMatch[1] &&
    firstMatch[1] == secondMatch[0] || firstMatch[1] == secondMatch[1];
}
    }


    public class FixtureList
{
    public Team[] RoundOne { get; set; }
    public Team[] RoundTwo { get; set; }
    public Team[] RoundThree { get; set; }
    public Team[] RoundFour { get; set; }
    public Team[] RoundFive { get; set; }
    public Team[] RoundSix { get; set; }
    public Team[] RoundSeven { get; set; }
}

//Round round = RoundGenerator.GenerateRound();

//List<Team[,]> population = new List<Team[,]>();

//CreatePopulation(population);

//var roundOne = GetRoundOne(population[0]);

//var isFixtureValid = FixtureIsValid(population[0]);
//var isValid = RoundIsValid(roundOne);

//var firstMatch = GetFirstMatch(roundOne);
//var second = GetSecondMatch(roundOne);
//var t = GetThirdMatch(roundOne);
//var f = GetFourthMatch(roundOne);

//var br = BocaAndRiverRoundSix(population[0]);
