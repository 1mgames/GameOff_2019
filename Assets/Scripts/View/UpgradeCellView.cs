using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using UniRx.Triggers;

public class UpgradeCellView : MonoBehaviour
{
    private Animator _selfAnimator = null;
    private Button _selfButton = null;

    [SerializeField] private TextMeshProUGUI _nameText = null;
    [SerializeField] private TextMeshProUGUI _descriptionText = null;

    private Action _action = null;

    private void Awake()
    {
        _selfAnimator = GetComponent<Animator>();
        _selfButton = GetComponent<Button>();

        _selfButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                SetAnimationBool("IsDecide", true);
                _action();
            });
    }

    public void Setup(Action action, string name, string description)
    {
        _action = action;

        _nameText.text = name;
        _descriptionText.text = description;
    }

    public void SetAnimationBool(string name, bool bUpgrade)
    {
        if(_selfAnimator != null)
        {
            _selfAnimator.SetBool(name, bUpgrade);
        }
    }
}
