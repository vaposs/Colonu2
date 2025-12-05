using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowCounter : MonoBehaviour
{
    [SerializeField] private Text _countMoney;

    private Counter _counter;

    private void Awake()
    {
        _counter = GetComponent<Counter>();
    }

    private void OnEnable()
    {
        _counter.AddResource += Print;
    }

    private void OnDisable()
    {
        _counter.AddResource -= Print;
    }

    private void Print(int count)
    {
        _countMoney.text = Convert.ToString(count);
    }
}
