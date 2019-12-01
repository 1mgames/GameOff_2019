using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;


public class GameClearController : MonoBehaviour
{
    private Animator _selfAnimator = null;
    [SerializeField] private GameClearView _view = null;

    [SerializeField] private Button _restartButton = null;
    [SerializeField] private Button _tweetButton = null;

    private float _totalTime = 0;
    private float _totalCoin = 0;
    private void Awake()
    {
        _selfAnimator = this.GetComponent<Animator>();

        _restartButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                _selfAnimator.SetBool("GameClear", false);
                GameManager.Instance.RestartGame();
            });

        _tweetButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                string tweetUrl = "https://twitter.com/intent/tweet?text=" + WWW.EscapeURL("Clear time : " + (_totalTime - _totalCoin * 0.01f) + " #BBBounDDD");
                Application.ExternalEval("var F = 0;if (screen.height > 500) {F = Math.round((screen.height / 2) - (250));}window.open('" + tweetUrl + "','intent','left='+Math.round((screen.width/2)-(250))+',top='+F+',width=500,height=260,personalbar=no,toolbar=no,resizable=no,scrollbars=yes');");

            });
    }


    public void GameClear(float total_time, float total_coin)
    {
        _totalTime = total_time;
        _totalCoin = total_coin;
        _view.Initialize(total_time, total_coin);
        _selfAnimator.SetBool("GameClear", true);
    }
}
