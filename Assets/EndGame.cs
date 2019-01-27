using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndGame : MonoBehaviour
{

    public Canvas victory;
    // Start is called before the first frame update
    void Start()
    {
        victory.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision colission)
    {
        if (colission.gameObject.GetComponent<PlayerGame>() != null)
        {
            victory.enabled = true;
            victory.worldCamera = colission.gameObject.GetComponent<PlayerGame>().camera;
            GameManager.instance.state = GameManager.GameState.finished;
            //SceneManager.LoadScene("MenuJogoNovo", LoadSceneMode.Single);
        }

    }
}
