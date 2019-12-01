using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private Vector3 _touchStartPos;
    private Vector3 _touchEndPos;

    [SerializeField] private PlayerView _view = null;

    private Rigidbody2D _rigidbody2D = null;
    [SerializeField] private Collider2D _collider2D = null;

    [SerializeField] private FloatReactiveProperty _hp = new FloatReactiveProperty(1);
    [SerializeField] private FloatReactiveProperty _hpMax = new FloatReactiveProperty(1);

    [SerializeField] private FloatReactiveProperty _energy = new FloatReactiveProperty(25);
    [SerializeField] private FloatReactiveProperty _energyCapacity = new FloatReactiveProperty(50);
    [SerializeField] private FloatReactiveProperty _energyRecharge = new FloatReactiveProperty(10);
    [SerializeField] private IntReactiveProperty _attack = new IntReactiveProperty(1);
    [SerializeField] private IntReactiveProperty _penetration = new IntReactiveProperty(1);

    // 係数
    [SerializeField] private FloatReactiveProperty _addPoint = new FloatReactiveProperty(1);
    [SerializeField] private FloatReactiveProperty _coin = new FloatReactiveProperty(0);
    public float Coin { get { return _coin.Value; } }

    [SerializeField] private IntReactiveProperty _special = new IntReactiveProperty(0);

    private FloatReactiveProperty _time = new FloatReactiveProperty(0);
    private FloatReactiveProperty _totalTime = new FloatReactiveProperty(0);

    private bool _bSwipe = false;

    private float FORCE_THRESHOLD = 100;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _collider2D.OnCollisionEnter2DAsObservable()
            .Subscribe(_ =>
            {
                HitObject(_);
            });

        this.LateUpdateAsObservable()
            .Subscribe(_ =>
            {
                _view.SetPositionEnergyGauge(this.transform.position);
            });

        this.UpdateAsObservable()
            .Where(_ => GameManager.Instance.State == GameManager.GameState.Game)
            .Subscribe(_ =>
            {
                if(FloorManager.Instance.GetNowFloor() != 1)
                {
                    _time.Value += Time.deltaTime;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    _touchStartPos = Input.mousePosition;

                    _view.SetStartPosLineRenderer(this.transform.position);

                    _bSwipe = true;
                }
                else if (_bSwipe == true && (Input.GetMouseButtonUp(0) || _energy.Value <= 0))
                {
                    _touchEndPos = Input.mousePosition;

                    _view.InitializeLineRenderer();

                    _rigidbody2D.velocity = Vector3.zero;
                    _rigidbody2D.angularVelocity = 0;
                    _rigidbody2D.AddForce((this.transform.position + (_touchStartPos - _touchEndPos) * 35));

                    _bSwipe = false;
                }

                if (_bSwipe == true)
                {
                    _view.ViewLineRenderer(_touchStartPos, this.transform.position);

                    if (Time.timeScale >= 0.1f)
                    {
                        Time.timeScale -= (Time.deltaTime * 3);
                    }

                    GameManager.Instance.CameraVignette(0, 0.7f + (1-Time.timeScale)*0.25f);

                    _energy.Value -= Time.deltaTime * 50;
                }
                else if(Time.timeScale != 1)
                {
                    Time.timeScale = 1;
                    GameManager.Instance.CameraVignette(0.25f, 0.7f);
                }
                else
                {
                    if (_energy.Value < _energyCapacity.Value)
                    {
                        _energy.Value += Time.deltaTime * _energyRecharge.Value;
                        if(_energy.Value >= _energyCapacity.Value)
                        {
                            _energy.Value = _energyCapacity.Value;
                        }
                    }                    
                }

                _view.ViewEnergyGauge(_energy.Value, _energyCapacity.Value);
            });

        // viewにフロア変更通知
        FloorManager.Instance.FloorChange.Subscribe(_ =>
        {
            _view.UpdateFloorView(_);
        });

        // viewにコイン変動通知
        _coin.Subscribe(_ =>
        {
            _view.UpdateCoinView(_);
        });

        // viewにタイム変動通知
        _time.Subscribe(_ =>
        {
            _view.UpdateTimeView(_);
        });

        // viewにトータルタイム変動通知
        _totalTime.Subscribe(_ =>
        {
            _view.UpdateTotalTimeView(_);
        });

        // viewにhp変動通知
        _hp.Subscribe(_ =>
        {
            _view.ViewHpGauge(_, _hpMax.Value);
        });
    }

    public void Initialize()
    {
        _totalTime.Value += _time.Value;
        _time.Value = 0;
//        _hp.Value = _hpMax.Value;
        _bSwipe = false;

        _view.InitializeLineRenderer();

        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
            GameManager.Instance.CameraVignette(0.25f, 0.7f);
        }

        InitializePosition();
    }

    public void Restart()
    {
        _totalTime.Value = 0;
        _time.Value = 0;
        _coin.Value = 0;
        _hpMax.Value = 1;
        _hp.Value = 1;

        _attack.Value = 1;
        _penetration.Value = 0;
        _energy.Value = 25;
        _energyCapacity.Value = 50;
        _energyRecharge.Value = 10;
        _special.Value = 0;
        _addPoint.Value = 1;
    }

    public void Restore()
    {
        _hp.Value = _hpMax.Value;
    }

    public void JumpUp()
    {
        _bSwipe = false;
        _rigidbody2D.velocity = Vector3.zero;
        _rigidbody2D.angularVelocity = 0;

        _collider2D.enabled = false;
        _rigidbody2D.simulated = false;

        this.transform.DOScale(3.0f, 1.0f);
        this.transform.DOLocalMove(Vector2.zero, 1.0f);

        _view.InitializeLineRenderer();

        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
            GameManager.Instance.CameraVignette(0.25f, 0.7f);
        }
    }

    private void DecideRank(float time)
    {
        if (time <= 1.0f)
        {
            _view.PlayRankView(PlayerData.RANK_TYPE.AWESOME, _addPoint.Value);
            AddCoin(PlayerData.rankBonus[PlayerData.RANK_TYPE.AWESOME]);
        }
        else if (time <= 2.0f)
        {
            _view.PlayRankView(PlayerData.RANK_TYPE.FANTASTIC, _addPoint.Value);
            AddCoin(PlayerData.rankBonus[PlayerData.RANK_TYPE.FANTASTIC]);
        }
        else if (time <= 5.0f)
        {
            _view.PlayRankView(PlayerData.RANK_TYPE.EXELENT, _addPoint.Value);
            AddCoin(PlayerData.rankBonus[PlayerData.RANK_TYPE.EXELENT]);
        }
        else if (time <= 10.0f)
        {
            _view.PlayRankView(PlayerData.RANK_TYPE.GREAT, _addPoint.Value);
            AddCoin(PlayerData.rankBonus[PlayerData.RANK_TYPE.GREAT]);
        }
        else
        {
            _view.PlayRankView(PlayerData.RANK_TYPE.GOOD, _addPoint.Value);
            AddCoin(PlayerData.rankBonus[PlayerData.RANK_TYPE.GOOD]);
        }
    }

    public void InitializePosition()
    {
        if(this.gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(true);
            _view.SetActiveEnergyGauge(true);
        }

        this.transform.DOScale(1.0f, 1.0f);
        this.GetComponent<RectTransform>().DOAnchorPos(FloorManager.Instance.GetStartPosition(), 1.0f)
            .OnComplete(()=> 
            {
                _collider2D.enabled = true;
                _rigidbody2D.simulated = true;
                GameManager.Instance.ResumeGame();
            });
    }

    public void AddHp(int value)
    {
        _hpMax.Value += value;
        _hp.Value += value;
    }
    public void AddAttack(int value)
    {
        _attack.Value += value;
    }
    public void AddPenetration(int value)
    {
        _penetration.Value += value;
    }
    public void AddEnergyRecharge(float value)
    {
        _energyRecharge.Value += value;
    }
    public void AddEnergyCapacity(float value)
    {
        _energyCapacity.Value += value;
    }
    public void AddPoint(float value)
    {
        _addPoint.Value += value;
    }

    public void AddCoin(float value)
    {
        // コインを増やす時は強化値も加味
        _coin.Value += value * _addPoint.Value;
    }
    public void SpendCoin(float value)
    {
        _coin.Value -= value;
    }

    public float GetTotalTime()
    {
        return _totalTime.Value;
    }

    public float GetCoin()
    {
        return _coin.Value;
    }

    private IEnumerator DelayMethod(int delayFrameCount, System.Action action)
    {
        for (var i = 0; i < delayFrameCount; i++)
        {
            yield return null;
        }
        action();
    }

    private void HitObject(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "GoalBlock":
                {
                    GameManager.Instance.FloorUp();
                    int bonus = collision.transform.GetComponent<BreakableWall>().Damage(_attack.Value);
                    AddCoin(bonus);
                    DecideRank(_time.Value);

                    GameManager.Instance.CameraShake(0.5f, 2f);
                    AudioManager.Instance.PlaySe(AudioType.HIT);
                    AudioManager.Instance.PlaySe(AudioType.BREAK);
                    AudioManager.Instance.PlaySe(AudioType.GOAL);
                    break;
                }

            case "BreakBlock":
                {
                    int bonus = collision.transform.GetComponent<BreakableWall>().Damage(_attack.Value);
                    AddCoin(bonus);

                    if (bonus > 0)
                    {
                        DamageTextManager.Instance.Setup(bonus * _addPoint.Value, collision.transform.position, true);
                    }

                    GameManager.Instance.CameraShake(0.25f, 1f);
                    AudioManager.Instance.PlaySe(AudioType.HIT);
                    AudioManager.Instance.PlaySe(AudioType.BREAK);
                    break;
                }

            case "DamageWall":
                {
                    float damage = collision.transform.GetComponent<DamageWall>().GetDamageValue();
                    _hp.Value -= damage;

                    if (_hp.Value <= 0.0f)
                    {
                        _view.PlayDeathEffect(this.transform.position);
                        this.gameObject.SetActive(false);
                        _view.SetActiveEnergyGauge(false);

                        _coin.Value -= 100;
                        if (_coin.Value >= 0)
                        {
                            GameManager.Instance.FloorRetry();
                        }
                        else
                        {
                            _coin.Value = 0;
                            GameManager.Instance.GameOver();
                        }

                        GameManager.Instance.CameraShake(0.75f, 2f);
                        GameManager.Instance.CameraColor(0.5f, ColorUtil.ryDarkRed);
                        AudioManager.Instance.PlaySe(AudioType.DEAD);

                        DamageTextManager.Instance.Setup(-100, collision.transform.position, false);
                    }
                    else
                    {
                        GameManager.Instance.CameraShake(0.25f, 1f);
                        GameManager.Instance.CameraColor(0.25f, ColorUtil.ryDarkRed);
                        AudioManager.Instance.PlaySe(AudioType.DAMAGE);
                    }

                    AudioManager.Instance.PlaySe(AudioType.HIT);

                    break;
                }

            case "SpecialWall":
                {
                    int bonus = collision.transform.GetComponent<BreakableWall>().Damage(_attack.Value);
                    _coin.Value += bonus * _addPoint.Value; ;

                    _special.Value++;

                    break;
                }

            default:
                {
                    if (collision.relativeVelocity.magnitude > FORCE_THRESHOLD)
                    {
                        GameManager.Instance.CameraShake(0.1f, 0.5f);
                        _view.PlayHitParticle(this.transform.position);

                        AudioManager.Instance.PlaySe(AudioType.HIT);
                    }
                    break;
                }
        }
    }
}
