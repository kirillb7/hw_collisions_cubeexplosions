using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Cube _firstCube;
    [SerializeField] private Spawner _spawner;

    private List<Cube> _subscribedCubes = new List<Cube>();

    private void OnEnable()
    {
        _spawner.CubesSpawned += ExplodeSpawnedCubes;
        SubscribeToCube(_firstCube);
    }

    private void OnDisable()
    {
        _spawner.CubesSpawned -= ExplodeSpawnedCubes;

        for (int i = _subscribedCubes.Count; i > 0; i--)
        {
            UnsubscribeFromCube(_subscribedCubes[i - 1]);
        }
    }

    private void TryExplode(Cube cube, bool canSplit)
    {
        UnsubscribeFromCube(cube);

        if (canSplit == false)
        {
            Explode(cube, GetBodiesInRange(cube));
        }
    }

    private void ExplodeSpawnedCubes(Cube cube, List<Cube> spawnedCubes)
    {
        List<Rigidbody> affectedBodies = new();

        foreach (Cube spawnedCube in spawnedCubes)
        {
            SubscribeToCube(spawnedCube);
            affectedBodies.Add(spawnedCube.Rigidbody);
        }

        Explode(cube, affectedBodies);
    }

    private void Explode(Cube cube, List<Rigidbody> affectedBodies)
    {
        foreach (Rigidbody body in affectedBodies)
        {
            body.AddExplosionForce(cube.ExplosionForce, cube.transform.position, cube.ExplosionRadius);
        }
    }

    private List<Rigidbody> GetBodiesInRange(Cube cube)
    {
        Collider[] hits = Physics.OverlapSphere(cube.transform.position, cube.ExplosionRadius);
        List<Rigidbody> affectedBodies = new();

        foreach (Collider hit in hits)
        {
            Rigidbody body = hit.attachedRigidbody;

            if (body != null)
            {
                affectedBodies.Add(body);
            }
        }

        return affectedBodies;
    }

    private void SubscribeToCube(Cube cube)
    {
        cube.Clicked += TryExplode;
        _subscribedCubes.Add(cube);
    }

    private void UnsubscribeFromCube(Cube cube)
    {
        cube.Clicked -= TryExplode;
        _subscribedCubes.Remove(cube);
    }
}
