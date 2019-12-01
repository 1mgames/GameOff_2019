using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class RotateWall : BaseWall
{
    [SerializeField] private float _speed = 0.0f;
    private void Awake()
    {
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                this.transform.Rotate(0, 0, _speed*Time.deltaTime);
            });
    }
}
