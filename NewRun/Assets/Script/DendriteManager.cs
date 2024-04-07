using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DendriteManager : MonoBehaviour
{
    public List<GameObject> dendrites;
    private int currentIndex = 0;

    void Start()
    {
        ActivateDendrite(currentIndex);
    }

    void Update()
    {
        if (!dendrites[currentIndex].gameObject.activeSelf)
        {
            currentIndex++;
            if (currentIndex < dendrites.Count)
                ActivateDendrite(currentIndex);
            if (currentIndex == dendrites.Count)
            {
                currentIndex = dendrites.Count - 1;
            }
        }
    }

    void ActivateDendrite(int index)
    {
        dendrites[index].gameObject.SetActive(true);
    }
}
