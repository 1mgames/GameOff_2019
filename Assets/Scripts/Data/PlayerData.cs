using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public enum RANK_TYPE : int
    {
        GOOD = 0,
        GREAT,
        EXELENT,
        FANTASTIC,
        AWESOME,

        RANK_MAX
    }

    public static readonly Dictionary<RANK_TYPE, int> rankBonus = new Dictionary<RANK_TYPE, int>()
    {
        { RANK_TYPE.GOOD, 0 },
        { RANK_TYPE.GREAT, 10 },
        { RANK_TYPE.EXELENT, 30 },
        { RANK_TYPE.FANTASTIC, 50 },
        { RANK_TYPE.AWESOME, 100 },
    };

    public static readonly Dictionary<RANK_TYPE, string> rankString = new Dictionary<RANK_TYPE, string>()
    {
        { RANK_TYPE.GOOD, "Good" },
        { RANK_TYPE.GREAT, "Great" },
        { RANK_TYPE.EXELENT, "Execelt!" },
        { RANK_TYPE.FANTASTIC, "Fantactic!!" },
        { RANK_TYPE.AWESOME, "Awesome!!!" },
    };

    public static readonly Dictionary<RANK_TYPE, string> animationString = new Dictionary<RANK_TYPE, string>()
    {
        { RANK_TYPE.GOOD, "PlayGood" },
        { RANK_TYPE.GREAT, "PlayGreat" },
        { RANK_TYPE.EXELENT, "PlayExelent" },
        { RANK_TYPE.FANTASTIC, "PlayFantastic" },
        { RANK_TYPE.AWESOME, "PlayAwesome" },
    };
}
