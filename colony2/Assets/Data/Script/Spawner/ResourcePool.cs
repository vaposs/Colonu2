using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Transform _conteiner;
    [SerializeField] private Transform _minPosition;
    [SerializeField] private Transform _maxPosition;
    [SerializeField] private Resours _resources;

    private Queue<Resours> _poolResources;
    private Resours _tempResources;
    private Vector3 _spawnPosition;
    private float _spawnPosotionY = -0.25f;

    private void Awake()
    {
        _poolResources = new Queue<Resours>();
    }

    public void PutResours(Resours resours)
    {
        resours.gameObject.SetActive(false);
        _poolResources.Enqueue(resours);
        resours.transform.SetParent(_conteiner);
        resours.Destroed -= PutResours;
    }

    public void GetResource()
    {
        _spawnPosition = new Vector3(
                    Random.RandomRange(_minPosition.position.x,_maxPosition.position.z),
                    _spawnPosotionY,
                    Random.RandomRange(_minPosition.position.z, _maxPosition.position.z));

        if(_poolResources.Count == 0)
        {
            _tempResources = Instantiate(_resources, _spawnPosition, Quaternion.identity, _conteiner);
        }
        else
        {
            _tempResources = _poolResources.Dequeue();
            _tempResources.transform.position = _spawnPosition;
            _tempResources.gameObject.SetActive(true);
        }

        _tempResources.Destroed += PutResours;
    }
}
