using UnityEngine;
using System;
using System.Collections.Generic;

public class Counter : MonoBehaviour
{
    public event Action CreatedNewDron;
    public event Action<int> AddResource;
    public event Action<Transform> CreatedNewBase;

    [SerializeField] private UploadPlace _uploadPlace;

    private int _countResours = 0;
    private int _countCostDrone = 3;
    private int _countCostBase = 5;
    private Queue<Transform> _queryConstractNewBese;

    private void Awake()
    {
        _queryConstractNewBese = new Queue<Transform>();
    }

    private void OnEnable()
    {
        _uploadPlace.PrintCountResource += AddCountResours;

    }

    private void OnDisable()
    {
        _uploadPlace.PrintCountResource -= AddCountResours;
    }

    private void AddCountResours()
    {
        _countResours++;

        if(_queryConstractNewBese.Count == 0 && _countResours >= _countCostDrone)
        {
            CreatedNewDron?.Invoke();
            _countResours -= _countCostDrone;
        }

        if(_queryConstractNewBese.Count > 0 && _countResours >= _countCostBase)
        {
            CreatedNewBase?.Invoke(_queryConstractNewBese.Dequeue());
            _countResours -= _countCostBase;
        }

        AddResource?.Invoke(_countResours);
    }

    public void TakePositionNewBase(Transform position)
    {
        _queryConstractNewBese.Enqueue(position);
    }
}
