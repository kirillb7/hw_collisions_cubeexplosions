using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _firstCube;
    [SerializeField] private Cube _cubePrefab;

    private List<Cube> _subscribedCubes = new List<Cube>();

    public event Action<Cube, List<Cube>> CubesSpawned;

    private void OnEnable()
    {
        SubscribeToCube(_firstCube);
    }

    private void OnDisable()
    {
        for (int i = _subscribedCubes.Count; i > 0; i--)
        {
            UnsubscribeFromCube(_subscribedCubes[i - 1]);
        }
    }

    private void TrySpawnCubes(Cube cube, bool canSplit)
    {
        UnsubscribeFromCube(cube);

        if (canSplit)
        {
            SpawnCubes(cube);
        }
    }

    private void SpawnCubes(Cube cube)
    {
        List<Cube> spawnedCubes = new List<Cube>();

        for (int i = 0; i < cube.SplitCount; i++)
        {
            Cube copy = Instantiate(_cubePrefab.gameObject, cube.transform.position, cube.transform.rotation).GetComponent<Cube>();

            copy.InitiateCopy(cube);
            spawnedCubes.Add(copy);
            SubscribeToCube(copy);
        }

        CubesSpawned?.Invoke(cube, spawnedCubes);
    }

    private void SubscribeToCube(Cube cube)
    {
        cube.Clicked += TrySpawnCubes;
        _subscribedCubes.Add(cube);
    }

    private void UnsubscribeFromCube(Cube cube)
    {
        cube.Clicked -= TrySpawnCubes;
        _subscribedCubes.Remove(cube);
    }
}
