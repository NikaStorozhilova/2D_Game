using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCheckPointPos;
    public bool checkpoint = false;
    public bool restarted = false;
    public List<string> collectedCoins;
    public List<string> collectedKeys;
    public int countCoin;
    public int countKey;

    private void Start()
    {
        countCoin = 0;
        countKey = 0;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
