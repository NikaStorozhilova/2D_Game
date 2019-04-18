using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private GameMaster gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        if (gm.checkpoint)
        {
            PreserveGameMaster();
        } else if (gm.restarted)
        {
            RefreshGameMaster();
        }
        UpdateGameState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gm.checkpoint = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void RefreshGameMaster()
    {
        gm.collectedKeys = new List<string>();
        gm.collectedCoins = new List<string>();
        gm.countKey = 0;
        gm.countCoin = 0;
        gm.lastCheckPointPos = transform.position;
    }

    public void PreserveGameMaster()
    {
        transform.position = gm.lastCheckPointPos;
        foreach (var coin in gm.collectedCoins)
        {
            GameObject.Find(coin).SetActive(false);
        }
        foreach (var key in gm.collectedKeys)
        {
            GameObject.Find(key).SetActive(false);
        }
    }

    public void UpdateGameState()
    {
        gm.restarted = false;
        gm.checkpoint = false;
        var cho = GetComponent<CharControllerScript>();
        cho.count = gm.countCoin;
        cho.countKey = gm.countKey;
        if (cho.countKey > 0)
        {
            cho.door.GetComponent<SpriteRenderer>().sprite = cho.doorOpened.GetComponent<SpriteRenderer>().sprite;
        }
        cho.SetCoinText();
    }
}
