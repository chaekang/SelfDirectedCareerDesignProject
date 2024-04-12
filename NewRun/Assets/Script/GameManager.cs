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
    public DendriteManager dendriteManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public enum E_STATE
    {
        INTRO,
        START,
        HOWTOPLAY,
        INFO,
        PLAY,
        GAMEOVER,
        GAMEWINNING,
        END,
        RESTART
    }

    E_STATE gamestate;

    private void Update()
    {
        GameState();
    }

    void GameState()
    {

    }
}
