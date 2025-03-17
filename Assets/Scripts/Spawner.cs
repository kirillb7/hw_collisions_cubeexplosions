using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _splitRangeMin = 2;
    [SerializeField] private int _splitRangeMax = 6;
    [SerializeField] private InputReader _inputReader;

    private void OnEnable()
    {
        _inputReader.CubeClicked += SpawnCubes;
    }

    private void OnDisable()
    {
        _inputReader.CubeClicked -= SpawnCubes;
    }

    private void SpawnCubes(Cube cube)
    {
        if (cube.TrySplit())
        {
            int splitCount = Random.Range(_splitRangeMin, _splitRangeMax);

            cube.ReduceParameters();

            for (int i = 0; i < splitCount; i++)
            {
                GameObject copy = Instantiate(cube.gameObject);
                copy.GetComponent<Renderer>().material.color = Random.ColorHSV();
            }
        }
    }
}
