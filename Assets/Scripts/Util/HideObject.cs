using System;
using UnityEngine;
using UniRx;

// 自動で消える
public class HideObject : MonoBehaviour
{
    public float _hideTime = 0.2f;

    void OnEnable()
    {
        Observable.Timer(TimeSpan.FromSeconds(_hideTime)).Subscribe(_ =>
        {
            this.gameObject.SetActive(false);
        }).AddTo(this);
    }
}