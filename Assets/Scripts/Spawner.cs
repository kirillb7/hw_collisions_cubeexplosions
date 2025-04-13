using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _firstCube;
    [SerializeField] private Cube _cubePrefab;

    private List<Cube> _subscribedCubes = new List<Cube>();

    public event Action<Cube, List<Rigidbody>> CubesSpawned;

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

    private void SpawnCubes(Cube cube)
    {
        UnsubscribeFromCube(cube);

        if (cube.CanSplit)
        {
            List<Rigidbody> spawnedBodies = new List<Rigidbody>();

            for (int i = 0; i < cube.SplitCount; i++)
            {
                Cube copy = Instantiate(_cubePrefab.gameObject, cube.transform.position, cube.transform.rotation).GetComponent<Cube>();

                copy.InitiateCopy(cube);
                spawnedBodies.Add(copy.Rigidbody);
                SubscribeToCube(copy);
            }

            CubesSpawned(cube, spawnedBodies);
        }
    }

    private void SubscribeToCube(Cube cube)
    {
        cube.Clicked += SpawnCubes;
        _subscribedCubes.Add(cube);
    }

    private void UnsubscribeFromCube(Cube cube)
    {
        cube.Clicked -= SpawnCubes;
        _subscribedCubes.Remove(cube);
    }
}
