using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundIons : MonoBehaviour
{
    public GameObject backgroundIon;

    public Tilemap ionSpace;
    private List<Vector3> ion_Positions = new List<Vector3>();

    public int numOfIon;


    void Start()
    {
        GetVertics_IonSpace();
        for (int i = 0; i < numOfIon; i++)
        {
            Vector3 randomSpawnPosition = GetRandomPosition();
            Instantiate(backgroundIon, randomSpawnPosition, Quaternion.identity);
        }
    }

    void GetVertics_IonSpace()
    {
        BoundsInt bounds = ionSpace.cellBounds;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x, y, 0);
                if (ionSpace.HasTile(cellPosition))
                {
                    ion_Positions.Add(ionSpace.GetCellCenterWorld(cellPosition));
                }
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        if (ion_Positions.Count == 0)
        {
            return Vector3.zero;
        }

        int randomIndex = Random.Range(0, ion_Positions.Count);
        return ion_Positions[randomIndex];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
