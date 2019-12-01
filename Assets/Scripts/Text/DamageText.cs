using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DamageText : MonoBehaviour
{
    private TextMeshProUGUI _text = null;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void Setup(float value, Vector3 pos, bool bPositive)
    {
        _text.color = bPositive ? Color.white : ColorUtil.ryRed;

        _text.alpha = 255;
        _text.text = value > 0 ? "+" + value.ToString("F0") : value.ToString("F0");

        transform.position = new Vector3(pos.x, pos.y, 90);

        _text.DOFade(0, 0.5f);
        transform.DOJump(
            new Vector3(pos.x, pos.y + 2, 90),   // 移動終了地点
            3,                     // ジャンプする力
            2,                     // 移動終了までにジャンプする回数
            0.5f).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
    }
}
