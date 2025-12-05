using System;
using UnityEngine;

public class Resours : MonoBehaviour
{
    public event Action<Resours> Destroed;

    private Vector3 _standartSize;

    private void Awake()
    {
        _standartSize = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = _standartSize;
    }

    public void DestroyResours()
    {
        Destroed?.Invoke(this);
    }
}
