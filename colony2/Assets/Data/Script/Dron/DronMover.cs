using UnityEngine;

[RequireComponent(typeof(DronLoadUnloadCargo))]

public class DronMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _commandCentre;
    private Transform[] _points;
    private DronLoadUnloadCargo _dronLoadUnloadCargo;
    private int _currentWayPoint = 0;

    private void Awake()
    {
        _dronLoadUnloadCargo = GetComponent<DronLoadUnloadCargo>();
    }

    private void Update()
    {
        if(_dronLoadUnloadCargo.IsHaveCommand == true)
        {
            MoveToTarget(_dronLoadUnloadCargo.Target);
        }
        else
        {
            if(_dronLoadUnloadCargo.IsHaveResource == true)
            {
                MoveToTarget(_commandCentre);
            }
            else
            {
                FreeMove();
            }
        }
    }

    public void Initialization(Transform commandCentre, Transform[] point)
    {
        _commandCentre = commandCentre;
        _points = point;
    }

    private void MoveToTarget(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed);
        transform.LookAt(target);
    }

    private void FreeMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, _points[_currentWayPoint].position, _speed);
        transform.LookAt(_points[_currentWayPoint]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Resours resours) && _dronLoadUnloadCargo.Target != null && resours.transform.position == _dronLoadUnloadCargo.Target.position)
        {
            _dronLoadUnloadCargo.LoadCargo(resours);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent(out PointPatrol point))
        {
            _currentWayPoint = ++_currentWayPoint % _points.Length;
        }
    }
}
