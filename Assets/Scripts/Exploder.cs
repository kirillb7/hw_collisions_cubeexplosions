using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.CubesSpawned += Explode;
    }

    private void OnDisable()
    {
        _spawner.CubesSpawned -= Explode;
    }

    private void Explode(Cube source, List<Rigidbody> affectedBodies)
    {
        foreach (Rigidbody body in affectedBodies)
        {
            body.AddExplosionForce(source.ExplosionForce, source.transform.position, source.ExplosionRadius);
        }
    }
}
