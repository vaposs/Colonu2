using System;
using UnityEngine;

public class Resours : MonoBehaviour
{
    public event Action<Resours> Destroed;

    public bool IsScan { get; private set; } = false;

    private Vector3 _standartSize;

    private void Awake()
    {
        _standartSize = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = _standartSize;
        IsScan = false;
    }

    public void DestroyResours()
    {
        Destroed?.Invoke(this);
    }

    public void GetInPool()
    {
        IsScan = true;
    }
}
