using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private GameMaster gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    public void MainMenu()
    {
        gm.restarted = true;
        SceneManager.LoadScene(2);
    }
}
