using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowCounter : MonoBehaviour
{
    [SerializeField] private Text _counter;

    public void Print(int count)
    {
        _counter.text = Convert.ToString(count);
    }
}
