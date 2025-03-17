using System;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _scaleMultiplier = 0.5f;
    [SerializeField] private float _splitChanceMultiplier = 0.5f;
    [SerializeField] private float _splitChance = 100f;

    public float ExplosionForce => _explosionForce;
    public float ExplosionRadius => _explosionRadius;

    public bool TrySplit()
    {
        int minValue = 0;
        int maxValue = 99;

        return UnityEngine.Random.Range(minValue, maxValue) < _splitChance;
    }

    public void ReduceParameters()
    {
        _splitChance *= _splitChanceMultiplier;
        gameObject.transform.localScale *= _scaleMultiplier;
    }
}
