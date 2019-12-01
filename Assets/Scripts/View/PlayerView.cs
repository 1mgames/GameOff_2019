using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private TextMeshProUGUI _floorText = null;
    [SerializeField] private TextMeshProUGUI _coinText = null;
    [SerializeField] private TextMeshProUGUI _timeText = null;
    [SerializeField] private TextMeshProUGUI _totalTimeText = null;
    [SerializeField] private TextMeshProUGUI _rankText = null;
    [SerializeField] private TextMeshProUGUI _bonusText = null;

    // すまん…
    [SerializeField] private RankController _rankController = null;

    [SerializeField] private GameObject _hitParticlePrefab = null;
    [SerializeField] private const int _HITPARTICLE_MAX = 10;
    private GameObject[] _hitParticlePool = new GameObject[_HITPARTICLE_MAX];

    [SerializeField] private Image _energyGauge = null;
    [SerializeField] private Image _energyGauge2 = null;
    [SerializeField] private TextMeshProUGUI _energyGaugeText = null;

    [SerializeField] private Image _hpGauge = null;
    [SerializeField] private TextMeshProUGUI _hpGaugeText = null;

    [SerializeField] private GameObject _deathParticlePrefab = null;
    private GameObject _deathParticle = null;

    [SerializeField] private LineRenderer _lineRenderer = null;


    // 演出用
    private float prevCoin = 0;
    private float prevHp = 0;

    private void Awake()
    {
        _camera = Camera.main;

        for(int i=0; i< _HITPARTICLE_MAX; i++)
        {
            _hitParticlePool[i] = GameObject.Instantiate(_hitParticlePrefab);
            _hitParticlePool[i].SetActive(false);
        }

        _deathParticle = GameObject.Instantiate(_deathParticlePrefab);
        _deathParticle.SetActive(false);
    }

    public void UpdateFloorView(int floor)
    {
        _floorText.text = floor.ToString();
    }

    public void UpdateCoinView(float coin)
    {
        DOVirtual.Float(prevCoin, coin, 0.5f, value =>
        {
            _coinText.text = value.ToString("F2");
        });

        prevCoin = coin;
    }

    public void UpdateTimeView(float time)
    {
        _timeText.text = time.ToString("F2");
    }

    public void UpdateTotalTimeView(float total_time)
    {
        _totalTimeText.text = total_time.ToString("F2");
    }

    public void UpdateBonusView(PlayerData.RANK_TYPE type, float add_point)
    {
        _bonusText.text = (PlayerData.rankBonus[type] * add_point).ToString();
    }

    public void PlayRankView(PlayerData.RANK_TYPE type, float add_point)
    {
        UpdateBonusView(type, add_point);

        _rankText.text = PlayerData.rankString[type];
        _rankController.Play(PlayerData.animationString[type]);
    }

    public void PlayHitParticle(Vector3 position)
    {
        foreach(var particle in _hitParticlePool)
        {
            if (particle.activeSelf == false)
            {
                particle.SetActive(true);
                particle.transform.position = position;
                break;
            }
        }
    }

    public void PlayDeathEffect(Vector3 position)
    {
        _deathParticle.SetActive(true);
        _deathParticle.transform.position = position;

    }

    public void ViewHpGauge(float value, float max_value)
    {
        DOVirtual.Float(prevHp, value, 0.5f, ef_value =>
        {
            _hpGauge.fillAmount = ef_value / max_value;
        });

        prevHp = value;
        _hpGaugeText.text = value.ToString("0") + " / " + max_value.ToString("0");
    }

    public void ViewEnergyGauge(float value, float max_value)
    {
        _energyGauge.fillAmount = value / max_value;
        _energyGauge2.fillAmount = value / max_value;
        _energyGaugeText.text = value.ToString("0") + " / " + max_value.ToString("0");
    }

    public void SetPositionEnergyGauge(Vector3 pos)
    {
        _energyGauge2.transform.position = pos;
    }

    public void SetActiveEnergyGauge(bool bActive)
    {
        _energyGauge2.gameObject.SetActive(bActive);
    }

    public void ViewLineRenderer(Vector3 start_pos, Vector3 now_pos)
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, now_pos);
        _lineRenderer.SetPosition(1, now_pos + (start_pos - Input.mousePosition) / 10);
    }

    public void SetStartPosLineRenderer(Vector3 start_pos)
    {
        if (_lineRenderer != null)
        {
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, start_pos);
        }
    }

    public void InitializeLineRenderer()
    {
        if (_lineRenderer != null)
        {
            _lineRenderer.positionCount = 0;
        }
    }
}
