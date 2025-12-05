using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(BoxCollider))]

public class PlaceForBase : MonoBehaviour
{
    [SerializeField] private Color _trueColor;
    [SerializeField] private Color _falseColor;
    [SerializeField] private Transform _conteiner;

    private CommandCentre _newCommandCentrePrefab;
    private MeshRenderer _meshRenderer;
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;
    private bool _isAlreadyConstruct = false;

    public bool IsMakeConstract { get; private set; } = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _conteiner = GameObject.FindAnyObjectByType<ConteinerForNewBase>().transform;
        _newCommandCentrePrefab = Resources.Load<CommandCentre>("Prefabs/Base");
    }

    public void OffTriggerStatys()
    {
        _boxCollider.isTrigger = false;
        Destroy(_rigidbody);
        _isAlreadyConstruct = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((other.transform.TryGetComponent(out CommandCentre commandCentre) || other.transform.TryGetComponent(out PlaceForBase placeForBase)) && _isAlreadyConstruct == false)
        {
            _meshRenderer.material.color = _falseColor;
            IsMakeConstract = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.transform.TryGetComponent(out CommandCentre commandCentre) || other.transform.TryGetComponent(out PlaceForBase placeForBase)) && _isAlreadyConstruct == false)
        {
            _meshRenderer.material.color = _trueColor;
            IsMakeConstract = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<DronLoadUnloadCargo>(out DronLoadUnloadCargo dronLoadUnloadCargo))
        {
            if (dronLoadUnloadCargo.Target != null && dronLoadUnloadCargo.Target.position == this.transform.position)
            {
                Instantiate(_newCommandCentrePrefab, transform.position, Quaternion.identity, _conteiner);
                Destroy(dronLoadUnloadCargo.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
