using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorizer : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
