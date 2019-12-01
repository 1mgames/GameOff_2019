using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : SingletonMonoBehaviour<DamageTextManager>
{
    [SerializeField] private DamageText _damageTextPrefab = null;

    [SerializeField] private const int _POOL_SIZE = 10;
    private List<DamageText> _damageTextPool = new List<DamageText>();

    private new void Awake()
    {
        base.Awake();

        for(int i=0;i< _POOL_SIZE; i++)
        {
            var obj = Create();
            obj.gameObject.SetActive(false);
        }
    }

    private DamageText Create()
    {
        GameObject obj = GameObject.Instantiate(_damageTextPrefab.gameObject);
        obj.transform.SetParent(transform, false);
        DamageText component = obj.GetComponent<DamageText>();
        _damageTextPool.Add(component);
        return component;
    }

    private DamageText GetObject()
    {
        foreach(var obj in _damageTextPool)
        {
            if (obj.gameObject.activeSelf == false)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        return Create();
    }

    public void Setup(float value, Vector2 pos, bool bPositive)
    {
        var obj = GetObject();
        obj.Setup(value, pos, bPositive);
    }
}