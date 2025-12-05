using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _pointUp;
    [SerializeField] private Transform _pointDown;
    [SerializeField] private float _speed;

    private InputControlGame _input;

    private void Awake()
    {
        _input = GameObject.FindAnyObjectByType<InputControlGame>();
    }

    private void OnEnable()
    {
        _input.MoveAxisX += MoveX;
        _input.MoveAxisZ += MoveZ;
    }

    private void OnDisable()
    {
        _input.MoveAxisX -= MoveX;
        _input.MoveAxisZ -= MoveZ;
    }

    private void MoveX(float directionX)
    {
        transform.position = new Vector3(directionX * _speed + transform.position.x, transform.position.y, transform.position.z);

        if (transform.position.x > _pointUp.position.x)
        {
            transform.position = new Vector3(_pointUp.position.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < _pointDown.position.x)
        {
            transform.position = new Vector3(_pointDown.position.x, transform.position.y, transform.position.z);
        }
    }

    private void MoveZ(float directionZ)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, directionZ * _speed + transform.position.z);

        if (transform.position.z > _pointUp.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _pointUp.position.z);
        }
        else if (transform.position.z < _pointDown.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _pointDown.position.z);
        }
    }
}
