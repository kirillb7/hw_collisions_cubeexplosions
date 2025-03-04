using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _scaleMultiplier = 0.5f;
    [SerializeField] private float _splitChanceMultiplier = 0.5f;
    [SerializeField] private float _splitChance = 100f;
    [SerializeField] private int _splitRangeMin = 2;
    [SerializeField] private int _splitRangeMax = 6;

    private void OnMouseUpAsButton()
    {
        Explode();
    }

    private void Explode()
    {
        if (TrySplit())
        {
            Split();
        }

        foreach (Rigidbody affectedObject in GetObjectsInRange())
        {
            affectedObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Destroy(gameObject);
    }

    private List<Rigidbody> GetObjectsInRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);
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

    private bool TrySplit()
    {
        int minValue = 0;
        int maxValue = 99;

        return Random.Range(minValue, maxValue) < _splitChance;
    }

    private void Split()
    {
        int splitCount = Random.Range(_splitRangeMin, _splitRangeMax);

        _splitChance *= _splitChanceMultiplier;
        gameObject.transform.localScale *= _scaleMultiplier;

        for (int i = 0; i < splitCount; i++)
        {
            GameObject copy = Instantiate(gameObject);
        }
    }
}
