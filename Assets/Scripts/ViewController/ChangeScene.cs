using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene(string nameScene) {
        Application.LoadLevel(nameScene);
    }
}
