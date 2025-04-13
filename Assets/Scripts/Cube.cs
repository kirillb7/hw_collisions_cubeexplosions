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

    private Renderer _renderer;

    public float ExplosionForce => _explosionForce;
    public float ExplosionRadius => _explosionRadius;
    public bool CanSplit { get; private set; }
    public int SplitCount { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public event Action<Cube> Clicked;

    private IEnumerator DestroySelf()
    {
        yield return null;
        Destroy(gameObject);
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        Rigidbody = GetComponent<Rigidbody>();

        Initiate();
    }

    private void OnMouseUpAsButton()
    {
        Clicked(this);
        StartCoroutine(DestroySelf());
    }

    private void Initiate()
    {
        int minChance = 0;
        int maxChance = 99;

        CanSplit = UnityEngine.Random.Range(minChance, maxChance) < _splitChance;
        SplitCount = UnityEngine.Random.Range(_splitRangeMin, _splitRangeMax);
        _renderer.material.color = UnityEngine.Random.ColorHSV();
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
