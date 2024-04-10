using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public _Player player;
    public PoolManager pool;
    public SynapseBar SynapseBar;
    public Spawner spawner;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
