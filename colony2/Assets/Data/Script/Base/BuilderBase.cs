using UnityEngine;

public class BuilderBase : MonoBehaviour
{
    [SerializeField] private CommandCentre _commandCentrePrefab;
    [SerializeField] private PlaceForBase _placeForBase;
    [SerializeField] private Transform _conteinerCommandBase;

    private InputControlGame _input;
    private Ray _ray;
    private Camera _camera;
    private RaycastHit _raycastHit;
    private Counter _tempCommandCentre = null;
    private PlaceForBase _flagBuildCommandCentre = null;

    private void Awake()
    {
        _camera = Camera.main;
        _input = GameObject.FindAnyObjectByType<InputControlGame>();
    }

    private void Update()
    {
        if(_flagBuildCommandCentre != null)
        {
            _flagBuildCommandCentre.transform.position = SetPositionOnGround();
        }
    }

    private void OnEnable()
    {
        _input.BuildedBase += BuldNewBase;
        _input.ChoosedBase += ChooseBase;
    }

    private void OnDisable()
    {
        _input.BuildedBase -= BuldNewBase;
        _input.ChoosedBase -= ChooseBase;
    }

    private void ChooseBase()
    {
        SendRayCast();

        if (_raycastHit.transform.TryGetComponent<Counter>(out _tempCommandCentre))
        {
            if(_flagBuildCommandCentre == null)
            {
                _flagBuildCommandCentre = Instantiate(_placeForBase, SetPositionOnGround(), Quaternion.identity, _conteinerCommandBase);
            }
            else
            {
                _flagBuildCommandCentre.gameObject.SetActive(true);
            }
        }
        else
        {
            if(_flagBuildCommandCentre != null)
            {
                _flagBuildCommandCentre.gameObject.SetActive(false);
            }
        }
    }

    private void BuldNewBase()
    {
        if(_tempCommandCentre != null)
        {
            if(_flagBuildCommandCentre.IsMakeConstract == true)
            {
                _tempCommandCentre.TakePositionNewBase(_flagBuildCommandCentre.transform);
                _tempCommandCentre = null;
                _flagBuildCommandCentre.OffTriggerStatys();
                _flagBuildCommandCentre = null;
            }
        }
    }

    private void SendRayCast()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_ray.origin, _ray.direction * 1000, Color.red);
        Physics.Raycast(_ray, out _raycastHit);
    }

    private Vector3 SetPositionOnGround()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 20f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        return worldPosition;
    }
}
