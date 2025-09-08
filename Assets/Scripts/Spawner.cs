using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField, Min(0.1f)] private float _delay = 1f;
    [SerializeField, Min(0.1f)] private float _spawnWidth = 5f;
    [SerializeField, Min(0.1f)] private float _spawnHeight = 1f;
    [SerializeField, Min(0)] private int _minSpawnCount = 1;
    [SerializeField, Min(1)] private int _maxSpawnCount = 5;

    private int _defaultCapasity = 40;
    private int _maxSize = 40;

    private Coroutine _coroutine;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.ResetParameters(),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _defaultCapasity,
            maxSize: _maxSize
        );
    }

    private void ActionOnGet(Cube cube)
    {
        cube.gameObject.SetActive(true);
        cube.transform.position = transform.position + RandonPosition();
        cube.transform.rotation = Random.rotation;

        cube.ItemDumped += CubeRelease;
    }

    private Vector3 RandonPosition()
    {
        float x = Random.Range(-_spawnWidth, _spawnWidth);
        float y = Random.Range(-_spawnHeight, _spawnHeight);
        float z = Random.Range(-_spawnWidth, _spawnWidth);

        return new Vector3(x, y, z);
    }

    private void CubeRelease(Cube cube)
    {
        cube.ItemDumped -= CubeRelease;

        _pool.Release(cube);
    }


    private void OnEnable()
    {
        _coroutine = StartCoroutine(Routine(_delay));
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator Routine(float _delay)
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount);

            for (int i = 0; i < spawnCount; i++)
                _pool.Get();

            yield return wait;
        }
    }
}
