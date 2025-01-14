using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RunnersVictory : MonoBehaviour
{
    public GameObject RunnersVictoryScreen;
    public ParticleSystem PS;

    private int limit = 1;

    private void Update()
    {
        if ((transform.position.x - Camera.main.transform.position.x) < 50 && limit == 1)
        {
            limit--;
            AudioManager.Instance.PlaySingleSound("End_Portal_Sound");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.name.Contains("Player"))
        {
            AudioManager.Instance.StopSingleSound("End_Portal_Sound");
            StartCoroutine(Victory());
            var emission = PS.emission;
            emission.rateOverTime = 60f;
            var Force = PS.forceOverLifetime;
            Force.enabled = true;
            emission.enabled = false;
            GameObject.Find("TrapManager").SetActive(false);
            Camera.main.GetComponent<TestCam>().enabled = false;
        }
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(1);
        var emission = PS.emission;
        emission.enabled = false;
        
        if (PlayerManagerScript.Instance.RoundNumberDone % 2 != 0)
        {
            PlayerManagerScript.Instance.Team1Score++;
        }
        else
        {
            PlayerManagerScript.Instance.Team2Score++;
        }
        RunnersVictoryScreen.SetActive(true);
        AudioManager.Instance.StopSingleSound("Music");
        AudioManager.Instance.PlaySingleSound("Runners_Victory_Jingle_Sound");
        GameObject.FindObjectOfType<EventSystem>().SetSelectedGameObject(RunnersVictoryScreen.transform.GetComponentInChildren<Button>().gameObject);
        Time.timeScale = 0f;
    }
}
