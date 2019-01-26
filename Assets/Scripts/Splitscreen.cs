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
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl cam0;
            CameraControl cam1;
            CameraControl cam2;
            CameraControl cam3;

        ;
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
                 cam0 = this.cameras[0].GetComponent(typeof(CameraControl)) as CameraControl;
                cam0.SetObjectToFollow(playersToFit[0].transform);

                this.cameras[0].rect = new Rect(0, 0, 1, 1);
                this.cameras[0].enabled = true;
                break;
            case 2:

                 cam0 = this.cameras[0].GetComponent(typeof(CameraControl)) as CameraControl;
                cam0.SetObjectToFollow(playersToFit[0].transform);

                 cam1 = this.cameras[1].GetComponent(typeof(CameraControl)) as CameraControl;
                cam1.SetObjectToFollow(playersToFit[1].transform);

                this.cameras[0].rect = new Rect(0, 0, 0.5f, 1);
                this.cameras[1].rect = new Rect(0.5f, 0, 0.5f, 1);

                this.cameras[0].enabled = true;
                this.cameras[1].enabled = true;
                break;
            case 3:


                 cam0 = this.cameras[0].GetComponent(typeof(CameraControl)) as CameraControl;
                cam0.SetObjectToFollow(playersToFit[0].transform);

                 cam1 = this.cameras[1].GetComponent(typeof(CameraControl)) as CameraControl;
                cam1.SetObjectToFollow(playersToFit[1].transform);

                 cam2 = this.cameras[2].GetComponent(typeof(CameraControl)) as CameraControl;
                cam2.SetObjectToFollow(playersToFit[2].transform);

                
                this.cameras[0].rect = new Rect(0.25f, 0, 0.5f, 0.5f);
                this.cameras[1].rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                this.cameras[2].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);

                this.cameras[0].enabled = true;
                this.cameras[1].enabled = true;
                this.cameras[2].enabled = true;
                break;
            case 4:

                 cam0 = this.cameras[0].GetComponent(typeof(CameraControl)) as CameraControl;
                cam0.SetObjectToFollow(playersToFit[0].transform);

                 cam1 = this.cameras[1].GetComponent(typeof(CameraControl)) as CameraControl;
                cam1.SetObjectToFollow(playersToFit[1].transform);

                 cam2 = this.cameras[2].GetComponent(typeof(CameraControl)) as CameraControl;
                cam2.SetObjectToFollow(playersToFit[2].transform);

                 cam3 = this.cameras[3].GetComponent(typeof(CameraControl)) as CameraControl;
                cam3.SetObjectToFollow(playersToFit[3].transform);

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
