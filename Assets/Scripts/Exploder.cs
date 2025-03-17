using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private void OnEnable()
    {
        _inputReader.CubeClicked += Explode;
    }

    private void OnDisable()
    {
        _inputReader.CubeClicked -= Explode;
    }

    private void Explode(Cube cube)
    {
        foreach (Rigidbody affectedObject in GetObjectsInRange(cube))
        {
            affectedObject.AddExplosionForce(cube.ExplosionForce, cube.transform.position, cube.ExplosionRadius);
        }

        Destroy(cube.gameObject);
    }

    private List<Rigidbody> GetObjectsInRange(Cube cube)
    {
        Collider[] hits = Physics.OverlapSphere(cube.transform.position, cube.ExplosionRadius);
        List<Rigidbody> objects = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                objects.Add(hit.attachedRigidbody);
            }
        }

        return objects;
    }
}
