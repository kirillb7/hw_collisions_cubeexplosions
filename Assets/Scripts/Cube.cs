using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _scaleMultiplier = 0.5f;
    [SerializeField] private float _splitChanceMultiplier = 0.5f;
    [SerializeField] private float _splitChance = 100f;
    [SerializeField] private int _splitRangeMin = 2;
    [SerializeField] private int _splitRangeMax = 6;

    public float ExplosionForce => _explosionForce;
    public float ExplosionRadius => _explosionRadius;
    public bool CanSplit { get; private set; }
    public int SplitCount { get; private set; }

    public event Action<Cube> Clicked;

    private void Awake()
    {
        Initiate();
    }

    private void OnMouseUpAsButton()
    {
        Clicked(this);
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return null;
        Destroy(gameObject);
    }

    private void Initiate()
    {
        int minChance = 0;
        int maxChance = 99;

        CanSplit = UnityEngine.Random.Range(minChance, maxChance) < _splitChance;
        SplitCount = UnityEngine.Random.Range(_splitRangeMin, _splitRangeMax);

        if (GetComponent(nameof(Renderer)))
        {
            GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
        }
    }

    public void InitiateCopy(Cube cube)
    {
        cube.GetSplitParameters(out float splitChance, out Vector3 scale);
        _splitChance = splitChance;
        transform.localScale = scale;

        Initiate();
    }

    public void GetSplitParameters(out float splitChance, out Vector3 scale)
    {
        splitChance = _splitChance * _splitChanceMultiplier;
        scale = transform.localScale * _scaleMultiplier;
    }
}
