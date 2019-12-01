using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameClearView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalTimeText = null;
    [SerializeField] private TextMeshProUGUI _totalCoinText = null;
    [SerializeField] private TextMeshProUGUI _resultText = null;

    // 計算用
    private float _totalTime = 0;
    private float _totalCoin = 0;

    public void Initialize(float total_time, float total_coin)
    {
        _totalTime = total_time;
        _totalCoin = total_coin;

        _totalTimeText.text = "0";
        _totalCoinText.text = "0";
        _resultText.text = "0";
    }

    // 以下の3種はアニメーションから呼び出し
    public void OnReflectTotalTime()
    {
        DOVirtual.Float(0, _totalTime, 0.25f, ef_value =>
        {
            _totalTimeText.text = ef_value.ToString("F2");
            _resultText.text = ef_value.ToString("F2");
        });
    }

    public void OnReflectTotalCoin()
    {
        DOVirtual.Float(0, _totalCoin, 0.25f, ef_value =>
        {
            _totalCoinText.text = ef_value.ToString("F2");
            _resultText.text = (_totalTime - ef_value*0.01f).ToString("F2");
        });
    }

    public void OnReflectResult()
    {
        // TODO: ランキング

    }
}
