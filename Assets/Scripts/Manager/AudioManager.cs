using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType : int
{
    HIT,
    GOAL,
    DAMAGE,
    DEAD,
    BREAK,

    UPGRADE_BEGIN,
    UPGRADE_IN,
    UPGRADE_SELECT,
    UPGRADE_COMPLETE,
}

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    // 音
    [SerializeField] private AudioSource _hit = null;
    [SerializeField] private AudioSource _goal = null;
    [SerializeField] private AudioSource _damage = null;
    [SerializeField] private AudioSource _dead = null;

    [SerializeField] private AudioSource _break = null;

    // アップグレード関連
    [SerializeField] private AudioSource _upgradeBegin = null;
    [SerializeField] private AudioSource _upgradeCellIn = null;
    [SerializeField] private AudioSource _upgradeCellSelect = null;
    [SerializeField] private AudioSource _upgradeComplete = null;

    // Start is called before the first frame update
    public void PlaySe(AudioType type)
    {
        switch(type)
        {
            case AudioType.HIT:
                {
                    _hit.Play();
                    break;
                }
            case AudioType.GOAL:
                {
                    _goal.Play();
                    break;
                }
            case AudioType.DAMAGE:
                {
                    _damage.Play();
                    break;
                }
            case AudioType.DEAD:
                {
                    _dead.Play();
                    break;
                }
            case AudioType.BREAK:
                {
                    _break.Play();
                    break;
                }
            case AudioType.UPGRADE_BEGIN:
                {
                    _upgradeBegin.Play();
                    break;
                }
            case AudioType.UPGRADE_IN:
                {
                    _upgradeCellIn.Play();
                    break;
                }
            case AudioType.UPGRADE_SELECT:
                {
                    _upgradeCellSelect.Play();
                    break;
                }
            case AudioType.UPGRADE_COMPLETE:
                {
                    _upgradeComplete.Play();
                    break;
                }
        }
    }
}
