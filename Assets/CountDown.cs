using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class CountDown : MonoBehaviour
{
    public AudioClip countSound;
    public AudioClip initSound;

    private AudioSource source;
    private Text counter;
    void Start()
    {
        
        source = GetComponent<AudioSource>();
        counter = GetComponentInChildren<Text>();
        counter.enabled = false;
    }

    private void Update()
    {
        if(GameManager.instance.state == GameManager.GameState.waitingforplayers)
        {
            counter.text = "Aguardando Jogadores";
            counter.enabled = true;
        }

        if (GameManager.instance.state == GameManager.GameState.time) {
            GameManager.instance.state = GameManager.GameState.timeStarted;
            counter.enabled = true;
            StartCoroutine(Countdown(5));
        }
    }

    IEnumerator Countdown(int seconds)
    { 
        int count = seconds;

        while (count > 0)
        {
            count--;
            
            if (count == 0) {
                source.PlayOneShot(initSound);
                source.Play();
                counter.text = "RUUUN!!!";
            }
            else
            {
                //
                source.PlayOneShot(countSound);
                counter.text = count.ToString();

            }
            // display something...
            yield return new WaitForSeconds(1);
        }

        // count down is finished...
        StartGame();
    }

    void StartGame()
    {
        counter.enabled = false;
        GameManager.instance.state = GameManager.GameState.init;
        // do something...
    }
}


