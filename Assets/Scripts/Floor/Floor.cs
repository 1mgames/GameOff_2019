using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private Transform _startPosition = null;

    public Vector3 GetStartPosition()
    {
        return _startPosition.localPosition;
    }
}
