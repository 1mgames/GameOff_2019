using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private CameraView _cameraView = null;
    [SerializeField] private GameOverController _gameOverController = null;
    [SerializeField] private GameClearController _gameClearController = null;
    [SerializeField] private UpgradeController _upgradController = null;

    public enum GameState : int
    {
        Title,
        Initialize,
        Game,
        Retry,
        FloorUp,
        Upgrade,
        GameOver,
        GameClear,
        Restart,
    }

    [SerializeField] private GameState _state = GameState.Initialize;
    public GameState State { get { return _state; }  }

    [SerializeField] private Player _player = null;

    private void Start()
    {
        // 状態が変わった時にだけ呼び出される
        this.UpdateAsObservable()
            .Select(_ => _state)
            .DistinctUntilChanged()
            .Subscribe(_ =>
            {
                switch(_)
                {
                    case GameState.Title:
                        {
                            break;
                        }
                    case GameState.Initialize:
                        {
                            _cameraView.ChangeBloom(1, 1.25f);
                            _cameraView.ChangeDepthOfField(5, 1.5f);
                            _player.Initialize();
                            break;
                        }
                    case GameState.Game:
                        {
                            break;
                        }
                    case GameState.Retry:
                        {
                            _cameraView.ChangeDepthOfField(3, .5f);
                            FloorManager.Instance.Retry();
                            _player.JumpUp();
                            _player.Restore();
                            break;
                        }
                    case GameState.FloorUp:
                        {
                            FloorManager.Instance.FloorUp();
                            _player.JumpUp();
                            break;
                        }
                    case GameState.Upgrade:
                        {
                            _upgradController.Setup();
                            break;
                        }
                    case GameState.GameOver:
                        {
                            if (Time.timeScale != 1)
                            {
                                Time.timeScale = 1;
                                CameraVignette(0.25f, 0.7f);
                            }

                            _cameraView.ChangeDepthOfField(2, .5f);
                            _gameOverController.GameOver();
                            break;
                        }
                    case GameState.GameClear:
                        {
                            if (Time.timeScale != 1)
                            {
                                Time.timeScale = 1;
                                CameraVignette(0.25f, 0.7f);
                            }

                            _cameraView.ChangeBloom(5, 0.5f);
                            _cameraView.ChangeDepthOfField(1, 1f);
                            _gameClearController.GameClear(_player.GetTotalTime(), _player.GetCoin());
                            break;
                        }
                    case GameState.Restart:
                        {
                            _cameraView.ChangeBloom(50, 0.25f);
                            _cameraView.ChangeDepthOfField(5, 2f);

                            FloorManager.Instance.Restart();
                            _player.JumpUp();
                            _player.Restart();

                            break;
                        }
                }
            });
    }

    public void FloorRetry()
    {
        _state = GameState.Retry;
    }
    public void RestartGame()
    {
        _state = GameState.Restart;
    }
    public void FloorUp()
    {
        _state = GameState.FloorUp;
    }
    public void ResumeGame()
    {
        _state = GameState.Game;
    }
    public void Initialize()
    {
        _state = GameState.Initialize;
    }
    public void Upgrade()
    {
        _state = GameState.Upgrade;
    }
    public void UpgradeComplete()
    {
        _state = GameState.Initialize;
    }

    public void GameOver()
    {
        _state = GameState.GameOver;
    }

    public void GameClear()
    {
        _state = GameState.GameClear;
    }

    public void UpgradeHp(int value)
    {
        _player.AddHp(value);
    }
    public void UpgradeAttack(int value)
    {
        _player.AddAttack(value);
    }
    public void UpgradePenetration(int value)
    {
        _player.AddPenetration(value);
    }
    public void UpgradeEnergyRecharge(float value)
    {
        _player.AddEnergyRecharge(value);
    }
    public void UpgradeEnergyCapacity(float value)
    {
        _player.AddEnergyCapacity(value);
    }
    public void UpgradeAddPoint(float value)
    {
        _player.AddPoint(value);
    }

    public void CameraShake(float duration, float strength)
    {
        _cameraView.Shake(duration, strength);
    }

    public void CameraColor(float duration, Color color)
    {
        _cameraView.ChangeColor(duration, color);
    }

    public void CameraVignette(float duration, float value)
    {
        _cameraView.ChangeVignette(value, duration);
    }

    public float GetCoin()
    {
        return _player.Coin;
    }
    public void SpendCoin(float value)
    {
        _player.SpendCoin(value);
    }
}
