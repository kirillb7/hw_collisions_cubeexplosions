using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action<Cube> CubeClicked;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.TryGetComponent(out Cube cube))
                {
                    CubeClicked(cube);
                }
            }
        }
    }
}
