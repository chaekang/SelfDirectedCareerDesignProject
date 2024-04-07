using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Soma : MonoBehaviour
{
    public Material SomaMaterial;

    private void Start()
    {
        if (SomaMaterial != null)
        {
            Color color = SomaMaterial.color;
            color.a = 155f / 255f;
            SomaMaterial.color = color;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Dendrite"))
        {
            IncreaseTransparency(20);
        }
    }

    void IncreaseTransparency(float increaseAmount)
    {
        if (SomaMaterial != null)
        {
            Color color = SomaMaterial.color;
            color.a = Mathf.Clamp01(color.a + increaseAmount / 255f);
            SomaMaterial.color = color;
        }
    }
}
