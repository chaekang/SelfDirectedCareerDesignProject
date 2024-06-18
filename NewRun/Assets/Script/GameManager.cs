using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public _Player player;
    public PoolManager pool;
    public Spawner spawner;
    public SynapseBar SynapseBar;
    public SynapsePlayer synapsePlayer;
    public DendriteManager dendriteManager;
    public ChangeScene changeScene;
    public TutorialManager tutorialManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
