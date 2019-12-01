using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class BgView : MonoBehaviour
{
    private Image _bgImage = null;

    // Start is called before the first frame update
    void Start()
    {
        _bgImage = this.GetComponent<Image>();

        ChangeColor(Color.red);
    }

    void ChangeColor(Color color)
    {
//        _bgImage.material.SetColor("Color_D0409D15", color); //とりあえずEmissionで真っ赤にしてみる
    }

}