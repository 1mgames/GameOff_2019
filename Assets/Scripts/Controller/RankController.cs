using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankController : MonoBehaviour
{
    private Animator _selfAnimator = null;

    private void Awake()
    {
        _selfAnimator = this.GetComponent<Animator>();
    }

    public void Play(string trigger_name)
    {
        _selfAnimator.SetTrigger(trigger_name);
    }
}
