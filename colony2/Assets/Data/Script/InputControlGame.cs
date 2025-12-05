using System;
using UnityEngine;

public class InputControlGame : MonoBehaviour
{
    public event Action<float> MoveAxisX;
    public event Action<float> MoveAxisZ;
    public event Action ChoosedBase;
    public event Action BuildedBase;
    public event Action Scaning;

    private enum AxisType
    {
        Horizontal,
        Vertical
    }

    [SerializeField] private KeyCode _scanButton;
    [SerializeField] private KeyCode _chooseBaseButton;
    [SerializeField] private KeyCode _buildBaseButton;

    private AxisType _horizontal = AxisType.Horizontal;
    private AxisType _vertical = AxisType.Vertical;
    private float _directionX = 0;
    private float _directionZ = 0;

    private void Update()
    {
        MoveX();
        MoveZ();
        Scan();
        ChooseBase();
        BuildBase();
    }

    private void MoveX()
    {
        _directionX = Input.GetAxis(_horizontal.ToString());

        if (_directionX != 0)
        {
            MoveAxisX?.Invoke(_directionX);
        }
        else
        {
            _directionX = 0;
        }
    }

    private void MoveZ()
    {
        _directionZ = Input.GetAxis(_vertical.ToString());

        if (_directionZ != 0)
        {
            MoveAxisZ?.Invoke(_directionZ);
        }
        else
        {
            _directionZ = 0;
        }
    }

    private void Scan()
    {
        if(Input.GetKeyDown(_scanButton))
        {
            Scaning?.Invoke();
        }
    }

    private void ChooseBase()
    {
        if(Input.GetKeyDown(_chooseBaseButton))
        {
            ChoosedBase?.Invoke();
        }
    }

    private void BuildBase()
    {
        if (Input.GetKeyDown(_buildBaseButton))
        {
            BuildedBase?.Invoke();
        }
    }
}
