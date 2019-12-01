using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MoveWall : BaseWall
{
    [SerializeField] public Vector3 _position1;
    [SerializeField] public Vector3 _position2;

    [SerializeField] private float _speed = 0.0f;

    // 二点間の距離を入れる
    private float _distanceTwo = 0;
    // 移動時間
    private float _elapsedTime = 0;

    // 動く向き
    private bool _to1 = false;

    // 往復するかどうか
    private bool _bTrip = true;

    private void Awake()
    {
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                _distanceTwo = Vector3.Distance(_position1, _position2);

                // 現在の位置
                _elapsedTime += (Time.deltaTime * _speed) / _distanceTwo;

                // オブジェクトの移動
                if(_to1)
                {
                    transform.localPosition = Vector3.Lerp(_position1, _position2, _elapsedTime);
                    if(transform.localPosition == _position2)
                    {
                        if(_bTrip)
                        {
                            _to1 = !_to1;
                            _elapsedTime = 0;
                        }
                    }
                }
                else
                {
                    transform.localPosition = Vector3.Lerp(_position2, _position1, _elapsedTime);
                    if (transform.localPosition == _position1)
                    {
                        if (_bTrip)
                        {
                            _to1 = !_to1;
                            _elapsedTime = 0;
                        }
                    }
                }

            });
    }

    public void Setup(Vector3 position1, Vector3 position2, bool is_trip)
    {
        _position1 = position1;
        _position2 = position2;
        _bTrip = is_trip;
    }
}
