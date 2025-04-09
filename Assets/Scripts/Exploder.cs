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

    private void Explode(Cube source, List<Cube> affectedCubes)
    {
        foreach (Cube cube in affectedCubes)
        {
            if (cube.GetComponent(nameof(Rigidbody)))
            {
                cube.GetComponent<Rigidbody>().AddExplosionForce(source.ExplosionForce, source.transform.position, source.ExplosionRadius);
            }
        }
    }
}
