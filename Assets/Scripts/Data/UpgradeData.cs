using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeData
{
    public enum UPGRADE_TYPE : int
    {
        ATTACK = 0,
//        PENETRATION,
        ENERGY_CAPACITY,
        ENERGY_RECHARGE,
        HP,
        ADD_POINT,

        UPGRADE_MAX
    }

    public static readonly Dictionary<UPGRADE_TYPE, string> upgradeName = new Dictionary<UPGRADE_TYPE, string>()
    {
        { UPGRADE_TYPE.ATTACK, "Attack" },
//        { UPGRADE_TYPE.PENETRATION, "Penetration" },
        { UPGRADE_TYPE.ENERGY_CAPACITY, "EnergyCapacity" },
        { UPGRADE_TYPE.ENERGY_RECHARGE, "EnergyRecharge" },
        { UPGRADE_TYPE.HP, "Hp" },
        { UPGRADE_TYPE.ADD_POINT, "AddPoint" },
    };

    public static readonly Dictionary<UPGRADE_TYPE, string> upgradeDescription = new Dictionary<UPGRADE_TYPE, string>()
    {
        { UPGRADE_TYPE.ATTACK, "For Enemy/HardBlock"},
//        { UPGRADE_TYPE.PENETRATION, "Add To Penetration" },
        { UPGRADE_TYPE.ENERGY_CAPACITY, "Drag longer" },
        { UPGRADE_TYPE.ENERGY_RECHARGE, "Faster Recovery" },
        { UPGRADE_TYPE.HP, "More Tolerable" },
        { UPGRADE_TYPE.ADD_POINT, "Get More Bonus" },
    };

    // アップグレードした時に上昇するパラメータ値
    public static readonly Dictionary<UPGRADE_TYPE, float> upgradeValue = new Dictionary<UPGRADE_TYPE, float>()
    {
        { UPGRADE_TYPE.ATTACK, 1},
//        { UPGRADE_TYPE.PENETRATION, 1 },
        { UPGRADE_TYPE.ENERGY_CAPACITY, 10 },
        { UPGRADE_TYPE.ENERGY_RECHARGE, 2 },
        { UPGRADE_TYPE.HP, 1 },
        { UPGRADE_TYPE.ADD_POINT, 0.1f },
    };
}
