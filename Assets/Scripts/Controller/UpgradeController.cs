using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;


public class UpgradeController : MonoBehaviour
{
    private Animator _animator = null;

    [SerializeField] private UpgradeCellView[] _upgrageCellList;
    [SerializeField] private Button _shuffleButton = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _shuffleButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                AudioManager.Instance.PlaySe(AudioType.UPGRADE_IN);
                GameManager.Instance.SpendCoin(100);
                ShuffleUpgradeCell();
            });
    }

    public void Setup()
    {
        AudioManager.Instance.PlaySe(AudioType.UPGRADE_BEGIN);

        _animator.SetBool("IsUpgrade", true);

        ShuffleUpgradeCell();
    }

    private void ShuffleUpgradeCell()
    {
        if (GameManager.Instance.GetCoin() < 100)
        {
            _shuffleButton.interactable = false;
            return;
        }
        else if(_shuffleButton.interactable == false)
        {
            _shuffleButton.interactable = true;
        }

        foreach (var cell in _upgrageCellList)
        {
            int index = Random.Range(0, (int)UpgradeData.UPGRADE_TYPE.UPGRADE_MAX);
            UpgradeData.UPGRADE_TYPE type = (UpgradeData.UPGRADE_TYPE)index;

            cell.Setup(() =>
            {
                AudioManager.Instance.PlaySe(AudioType.UPGRADE_SELECT);
                AudioManager.Instance.PlaySe(AudioType.UPGRADE_COMPLETE);

                _animator.SetBool("IsUpgrade", false);
                foreach (var cell2 in _upgrageCellList)
                {
                    cell2.SetAnimationBool("IsUpgrade", false);
                }

                switch (type)
                {
                    case UpgradeData.UPGRADE_TYPE.HP:
                        {
                            GameManager.Instance.UpgradeHp((int)UpgradeData.upgradeValue[type]);
                            break;
                        }
                    case UpgradeData.UPGRADE_TYPE.ATTACK:
                        {
                            GameManager.Instance.UpgradeAttack((int)UpgradeData.upgradeValue[type]);
                            break;
                        }
                        /*
                    case UpgradeData.UPGRADE_TYPE.PENETRATION:
                        {
                            GameManager.Instance.UpgradePenetration(1);
                            break;
                        }
                        */
                    case UpgradeData.UPGRADE_TYPE.ENERGY_RECHARGE:
                        {
                            GameManager.Instance.UpgradeEnergyRecharge((int)UpgradeData.upgradeValue[type]);
                            break;
                        }
                    case UpgradeData.UPGRADE_TYPE.ENERGY_CAPACITY:
                        {
                            GameManager.Instance.UpgradeEnergyCapacity((int)UpgradeData.upgradeValue[type]);
                            break;
                        }
                    case UpgradeData.UPGRADE_TYPE.ADD_POINT:
                        {
                            GameManager.Instance.UpgradeAddPoint(UpgradeData.upgradeValue[type]);
                            break;
                        }
                }
            }, UpgradeData.upgradeName[type], UpgradeData.upgradeDescription[type]);
        }

        if (GameManager.Instance.GetCoin() < 100)
        {
            _shuffleButton.interactable = false;
            return;
        }
    }

    public void SetVisibleUpgradeCell1()
    {
        AudioManager.Instance.PlaySe(AudioType.UPGRADE_IN);
        _upgrageCellList[0].SetAnimationBool("IsUpgrade", true);
    }
    public void SetVisibleUpgradeCell2()
    {
        AudioManager.Instance.PlaySe(AudioType.UPGRADE_IN);
        _upgrageCellList[1].SetAnimationBool("IsUpgrade", true);
    }
    public void SetVisibleUpgradeCell3()
    {
        return;
        AudioManager.Instance.PlaySe(AudioType.UPGRADE_IN);
        _upgrageCellList[2].SetAnimationBool("IsUpgrade", true);
    }
    public void Complete()
    {
        foreach (var cell in _upgrageCellList)
        {
            cell.SetAnimationBool("IsDecide", false);
        }

        GameManager.Instance.UpgradeComplete();
    }
}
