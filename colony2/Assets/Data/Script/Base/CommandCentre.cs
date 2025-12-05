using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Scaner))]
[RequireComponent(typeof(Counter))]

public class CommandCentre : MonoBehaviour
{
    [SerializeField] private DronMover _dron;
    [SerializeField] private Transform _spawn;
    [SerializeField] private Transform[] _points;
    [SerializeField] private Transform _dronConteiner;
 
    private InputControlGame _input;
    private Scaner _scaner;
    private Queue<Resours> _resource = new Queue<Resours>();
    private Queue<DronMover> _drons = new Queue<DronMover>();
    private Transform _target;
    private DronMover _tempDron;
    private Counter _counter;
    private bool _commandCreateNewBase = false;
    private Transform _positionNewBase;

    private void Awake()
    {
        _counter = GetComponent<Counter>();
        _input = GameObject.FindAnyObjectByType<InputControlGame>();
        _scaner = GetComponent<Scaner>();
        CreateNewDrone();
    }

    private void OnEnable()
    {
        _input.Scaning += Scaning;
        _counter.CreatedNewDron += CreateNewDrone;
        _counter.CreatedNewBase += TakePositionNewBase;
    }

    private void OnDisable()
    {
        _input.Scaning -= Scaning;
        _counter.CreatedNewDron -= CreateNewDrone;
        _counter.CreatedNewBase -= TakePositionNewBase;
    }

    private void Update()
    {
        if(_drons.Count > 0)
        {
            if(_commandCreateNewBase == false)
            {
                _target = _resource.Count > 0 ? _resource.Dequeue().transform : null;

                if (_target != null)
                {
                    SendDrone(_drons.Dequeue(), _target);
                }
            }
            else
            {
                CreateNewBaseCommasd(_drons.Dequeue(), _positionNewBase);
                _commandCreateNewBase = false;
            }
        }
    }

    public void AddDron(DronMover dron)
    {
        _drons.Enqueue(dron);
    }

    private void TakePositionNewBase(Transform position)
    {
        _positionNewBase = position;
        _commandCreateNewBase = true;
    }

    private void CreateNewDrone()
    {
        _tempDron = Instantiate(_dron, _spawn.position, Quaternion.identity, _dronConteiner);
        _tempDron.Initialization(transform, _points);
        _drons.Enqueue(_tempDron);
    }

    private void SendDrone(DronMover dron, Transform resours)
    {
        if (dron.TryGetComponent(out DronLoadUnloadCargo dronLoadUnloadCargo))
        {
            dronLoadUnloadCargo.TakeCommand(resours);
        }
    }

    private void CreateNewBaseCommasd(DronMover dron, Transform newBasePosition)
    {
        if (dron.TryGetComponent(out DronLoadUnloadCargo dronLoadUnloadCargo))
        {
            dronLoadUnloadCargo.TakeCommand(newBasePosition);
        }
    }

    private void Scaning()
    {
        _resource = _scaner.Scan(_resource);
    }
}
