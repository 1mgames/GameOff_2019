using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class GeneratableWall : BaseWall
{
    [SerializeField] private BaseWall _generatePrefab = null;

    private List<BaseWall> _generatedPool = new List<BaseWall>();

    [SerializeField] private float _GENERATE_INTERVAL = 2.0f;
    [SerializeField] private float _elapsedTime = 0;

    [SerializeField] private int _DEFAULT_POOL_SIZE = 5;

    private RectTransform _rectTransform = null;

    private void Awake()
    {
        for(int i=0; i<_DEFAULT_POOL_SIZE; i++)
        {
            Generate();
        }

        _rectTransform = this.transform.GetComponent<RectTransform>();

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                _elapsedTime += Time.deltaTime;
                if(_elapsedTime >= _GENERATE_INTERVAL)
                {
                    _elapsedTime = 0;
                    BaseWall obj = Search();
                    if(obj == null)
                    {
                        obj = Generate();
                    }
                    obj.gameObject.SetActive(true);
                    obj.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - _rectTransform.sizeDelta.y / 2, this.transform.localPosition.z);
                }
            });
    }

    private BaseWall Generate()
    {
        var obj = GameObject.Instantiate(_generatePrefab, transform.parent);
        obj.gameObject.SetActive(false);
        _generatedPool.Add(obj);

        return obj;
    }

    private BaseWall Search()
    {
        foreach(var obj in _generatedPool)
        {
            if(obj.gameObject.activeSelf == false)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        return null;
    }
}
