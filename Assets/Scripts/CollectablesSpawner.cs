using UnityEngine;

public class CollectablesSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPointParent;
    [SerializeField] private Collectable _template;

    private Transform[] _spawnPoints;

    private void Awake()
    {
        _spawnPoints = new Transform[_spawnPointParent.childCount];
        for (int i = 0; i < _spawnPointParent.childCount; i++)
        {
            _spawnPoints[i] = _spawnPointParent.GetChild(i);
        }
    }

    private void Start()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            var createdCollectable = Instantiate(_template, _spawnPoints[i].position, Quaternion.identity);
        }
    }
}