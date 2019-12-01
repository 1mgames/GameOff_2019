using System;
using UnityEngine;
using UniRx;

// 自動で消える
public class DestroyObject : MonoBehaviour
{
    public float _destroyTime = 0.2f;

    void OnEnable()
    {
        Observable.Timer(TimeSpan.FromSeconds(_destroyTime)).Subscribe(_ =>
        {
            Destroy(this.gameObject);
        }).AddTo(this);
    }
}
