using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synapse : MonoBehaviour
{
    public int clearGauge;
    public GameObject GaugeBar;

    public void ClearAstrocyte()
    {
        GaugeBar.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
}
