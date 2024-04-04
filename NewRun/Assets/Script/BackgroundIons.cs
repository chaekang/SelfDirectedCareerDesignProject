using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundIons : MonoBehaviour
{
    public GameObject backgroundIon;

    public GameObject ionSpace;
    public Vector3[] ionSpace_verfics;

    public int numOfIon;


    void Start()
    {
        ionSpace_verfics = GetVertics_IonSpace(ionSpace);
        for (int i = 0; i < numOfIon; i++)
        {
            Vector3 randomSpawnPosition = GetRandomPosition_Ion(ionSpace_verfics[0], ionSpace_verfics[1], ionSpace_verfics[2], ionSpace_verfics[3]);
            Instantiate(backgroundIon, randomSpawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition_Ion(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        float randomX = Random.Range(Mathf.Min(v1.x, v2.x, v3.x, v4.x), Mathf.Max(v1.x, v2.x, v3.x, v4.x));
        float randomY = Random.Range(Mathf.Min(v1.y, v2.y, v3.y, v4.y), Mathf.Max(v1.y, v2.y, v3.y, v4.y));
        return new Vector3(randomX, randomY, 0);
    }

    Vector3[] GetVertics_IonSpace(GameObject ionspace)
    {
        Bounds bounds = ionspace.GetComponent<Renderer>().bounds;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(bounds.min.x, bounds.min.y, bounds.center.z); 
        vertices[1] = new Vector3(bounds.min.x, bounds.max.y, bounds.center.z); 
        vertices[2] = new Vector3(bounds.max.x, bounds.max.y, bounds.center.z); 
        vertices[3] = new Vector3(bounds.max.x, bounds.min.y, bounds.center.z);

        return vertices;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
