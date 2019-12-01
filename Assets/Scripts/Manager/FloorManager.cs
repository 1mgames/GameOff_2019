using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class FloorManager : SingletonMonoBehaviour<FloorManager>
{
    [SerializeField] private RectTransform _parent = null;
    [SerializeField] private Player _player = null;

    public List<GameObject> _floorList = new List<GameObject>();
    private List<GameObject> _createdFLoorList = new List<GameObject>();

    [SerializeField] private IntReactiveProperty _floorNow = new IntReactiveProperty(1);
    public ReadOnlyReactiveProperty<int> FloorChange;

    private new void Awake()
    {
        base.Awake();
        FloorChange = _floorNow.ToReadOnlyReactiveProperty();
    }

    private IEnumerator Start()
    {
        CreateFloor(1);

        yield return null;

        CreateFloor(2);

        yield break;
    }

    public void FloorUp()
    {
        _floorNow.Value++;

        if (_floorNow.Value >= _floorList.Count)
        {
            GameManager.Instance.GameClear();
            return;
        }

        CreateFloor(_floorNow.Value+1);
        FocusFloor(_floorNow.Value, () =>
        {
            if ((_floorNow.Value-1) % 5 == 0)
            {
                GameManager.Instance.Upgrade();
            }
            else
            {
                GameManager.Instance.Initialize();
            }
        });
    }

    private void CreateFloor(int floor)
    {
        if (floor-1 >= _floorList.Count) return;

        GameObject obj = GameObject.Instantiate(_floorList[floor - 1], _parent);
        obj.transform.localPosition = new Vector3(0, (floor-1) * 1280, 0);
        _createdFLoorList.Add(obj);
    }

    private void FocusFloor(int floor, Action complete_method)
    {
        _parent.DOAnchorPos(new Vector2(0, -(floor - 1) * 1280), 1.0f, true)
            .OnComplete(() =>
            {
                complete_method();
            });
    }

    public void Retry()
    {
        FocusFloor(_floorNow.Value, () => { GameManager.Instance.Initialize(); });
    }

    public void Restart()
    {
        _floorNow.Value = 1;
        FocusFloor(1, () => { GameManager.Instance.Initialize(); });

        StartCoroutine(ClearFloorList());
    }

    private IEnumerator ClearFloorList()
    {
        foreach(var obj in _createdFLoorList)
        {
            GameObject.Destroy(obj);
            yield return null;
        }

        _createdFLoorList.Clear();

        CreateFloor(1);

        yield return null;

        CreateFloor(2);

        yield break;
    }

    public Vector3 GetStartPosition()
    {
        return _floorList[_floorNow.Value - 1].GetComponent<Floor>().GetStartPosition();
    }

    public int GetNowFloor()
    {
        return _floorNow.Value;
    }
}
