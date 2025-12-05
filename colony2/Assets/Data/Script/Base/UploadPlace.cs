using System;
using UnityEngine;

public class UploadPlace : MonoBehaviour
{
    [SerializeField] private CommandCentre _commandCentre;

    public event Action PrintCountResource;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out DronLoadUnloadCargo dronLoadUnloadCargo))
        {
            if(dronLoadUnloadCargo.IsHaveResource == true)
            {
                PrintCountResource?.Invoke();
                dronLoadUnloadCargo.UnloadCargo();

                if(dronLoadUnloadCargo.TryGetComponent(out DronMover dronMover))
                {
                    _commandCentre.AddDron(dronMover);
                }
            }
        }
    }
}
