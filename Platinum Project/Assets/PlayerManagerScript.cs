using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerScript : MonoBehaviour
{
    public static PlayerManagerScript Instance;
    public GameObject[] players;

    public const int RUNNER1 = 0;
    public const int RUNNER2 = 1;
    public const int TRAPPER1 = 2;
    public const int TRAPPER2 = 3;

    private int RoundNumber = 1;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

            Instance = this;
            DontDestroyOnLoad(gameObject);
            players = new GameObject[4];
    }

    public void InitPlayerGame()
    {
        
        if (RoundNumber == 1)
        {
            for(int i = 0; i<4; i++)
            {
                if (players[i])
                {
                    players[i].GetComponent<neutralcontroller>().InitPlayer();
                }

            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (players[i])
                {
                    players[i].GetComponent<neutralcontroller>().limit = 1;
                    if (i <= 1)
                    {
                        players[i].GetComponent<neutralcontroller>()._state = neutralcontroller.STATE.TRAPPER;
                    }
                    else
                    {
                        players[i].GetComponent<neutralcontroller>()._state = neutralcontroller.STATE.RUNNER;
                    }
                    players[i].GetComponent<neutralcontroller>().InitPlayer();
                }

            }            
        }

    }

    public void ResetPlayerArray()
    {
        for (int i = 0; i < 4; i++)
        {
            if (players[i])
            {
                players[i] = null;
            }

        }

        RoundNumber = 1;
    }
}
