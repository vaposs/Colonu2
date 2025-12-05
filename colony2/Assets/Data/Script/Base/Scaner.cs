using UnityEngine;
using System.Collections.Generic;

public class Scaner : MonoBehaviour
{
    [SerializeField] private float _scanRadiys;

    private LayerMask _maskScan = 9;
    private Collider[] _hits;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, _scanRadiys);
    }

    public Queue<Resours> Scan(Queue<Resours> queueResours)
    {
        _hits = Physics.OverlapSphere(transform.position, _scanRadiys, _maskScan);

        for (int i = 0; i < _hits.Length; i++)
        {
            if(_hits[i].TryGetComponent(out Resours resours) && queueResours.Contains(resours) == false && resours.IsScan == false)
            {
                resours.GetInPool();
                queueResours.Enqueue(resours);
            }
        }

        return queueResours;
    }
}
