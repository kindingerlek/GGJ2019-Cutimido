using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitscreen : MonoBehaviour
{

    public Camera[] cameras;

    public GameObject[] players;

    private int lastPlayerCount = 0;

    private GameObject[] playersHud;

    private GameObject[] playersToFit =  new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        UpdateRefs();
    }

    void UpdateRefs()
    {
        for (int i = 0; i < 4; i++)
        {
            this.cameras[i].GetComponent<CameraControl>().SetObjectToFollow(players[i].transform);
            this.players[i].GetComponent<PlayerGame>().camera = this.cameras[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRefs();
        
        int playerCount = 0;
        foreach(GameObject player in players){
            PlayerGame player2 = player.GetComponent(typeof(PlayerGame)) as PlayerGame;

            if (player2.player != null) {
                playersToFit[playerCount] = player;
                playerCount++;
            }
        }

        if (playerCount == this.lastPlayerCount) {
            return;
        }
        this.lastPlayerCount = playerCount;

        this.resetCameras();
        switch (playerCount) {
           default:
            case 1:

                this.cameras[0].rect = new Rect(0, 0, 1, 1);
                this.cameras[0].enabled = true;
                break;
            case 2:                
                this.cameras[0].rect = new Rect(0, 0, 0.5f, 1);
                this.cameras[1].rect = new Rect(0.5f, 0, 0.5f, 1);

                this.cameras[0].enabled = true;
                this.cameras[1].enabled = true;
                break;
            case 3:                
                this.cameras[0].rect = new Rect(0.25f, 0, 0.5f, 0.5f);
                this.cameras[1].rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                this.cameras[2].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);

                this.cameras[0].enabled = true;
                this.cameras[1].enabled = true;
                this.cameras[2].enabled = true;
                break;
            case 4:

                this.cameras[0].rect = new Rect(0, 0, 0.5f, 0.5f);
                this.cameras[1].rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                this.cameras[2].rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                this.cameras[3].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);

                this.cameras[0].enabled = true;
                this.cameras[1].enabled = true;
                this.cameras[2].enabled = true;
                this.cameras[3].enabled = true;
                break;

        }
    }

    void resetCameras() {
        this.cameras[0].enabled = false;
        this.cameras[1].enabled = false;
        this.cameras[2].enabled = false;
        this.cameras[3].enabled = false;
    }
}
