using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Button _restartButton = null;
    private Animator _selfAnimator = null;

    private void Awake()
    {
        _selfAnimator = this.GetComponent<Animator>();

        _restartButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _selfAnimator.SetBool("GameOver", false);
                GameManager.Instance.RestartGame();
            });
    }

    public void GameOver()
    {
        _selfAnimator.SetBool("GameOver", true);
    }
}
