using UnityEngine;

public class DronLoadUnloadCargo : MonoBehaviour
{
    [SerializeField] private Transform _cargoPlace;

    private Resours _resours;
    private Vector3 _standartSize;

    public bool IsHaveCommand { get; private set; } = false;
    public bool IsHaveResource { get; private set; } = false;
    public Transform Target { get; private set; } = null;

    public void LoadCargo(Resours resours)
    {
        _standartSize = resours.transform.localScale;
        resours.transform.SetParent(this.transform);
        resours.transform.position = _cargoPlace.position;
        IsHaveResource = true;
        IsHaveCommand = false;
        resours.transform.localScale = _standartSize;
        resours.transform.rotation = this.transform.rotation;
        _resours = resours;
    }

    public void UnloadCargo()
    {
        _resours.transform.parent = null;
        IsHaveResource = false;
        Target = null;
        _resours.DestroyResours();
    }

    public void TakeCommand(Transform target)
    {
        Target = target;
        IsHaveCommand = true;
    }
}
