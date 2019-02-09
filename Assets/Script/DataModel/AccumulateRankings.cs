using System;
using System.Collections.Generic;


[Serializable]
public class AccumulateRankings {
    public List<Ranking> ranking = new List<Ranking>();
}


[Serializable]
public class Ranking {
    public String nickname;
    public int score;
}