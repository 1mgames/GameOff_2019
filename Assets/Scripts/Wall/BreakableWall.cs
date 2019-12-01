using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : BaseWall
{
    [SerializeField] private float _hp = 0.0f;
    [SerializeField] private GameObject _breakParticle = null;
    [SerializeField] private GameObject _hitParticle = null;

    [SerializeField] private int _bonus = 0;


    public int Damage(float value)
    {
        _hp -= value;
        if(_hp <= 0)
        {
            Destroy(gameObject);
            GameObject b_obj = GameObject.Instantiate(_breakParticle, null);
            b_obj.transform.position = this.transform.position;
            b_obj.transform.localScale *= 10;

            return _bonus;
        }

        if(_hitParticle != null)
        {
            GameObject h_obj = GameObject.Instantiate(_hitParticle, null);
            h_obj.transform.position = this.transform.position;
            h_obj.transform.localScale *= 10;
        }

        return 0;
    }
}
