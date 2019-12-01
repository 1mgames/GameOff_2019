using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWall : BaseWall
{
    [SerializeField] private float _attack = 0.0f;

    public float GetDamageValue()
    {
        return _attack;
    }
}
