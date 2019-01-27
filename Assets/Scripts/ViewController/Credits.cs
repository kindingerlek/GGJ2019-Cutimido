using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Credits: MonoBehaviour
{
    private float startTime;
    public int status;
    ChangeScene Cs;
    public void LoadCredits() {
        if (status == (int)EnumCredits.Designer)
        {
            Cs.LoadScene(7);
            return;
        }
        else if (status == (int) EnumCredits.Artist)
        {
            Cs.LoadScene(1);
            return;
        }
        else if (status == (int)EnumCredits.Programmer)
        {
            Cs.LoadScene(3);
            return;
        }
        else if (status == (int)EnumCredits.Music)
        {
            return;
        }
        else if (status == 5)
        {
            Cs.LoadScene(2);
            return;
        }


    }

    void Start()
    {
        startTime = Time.time;
        Cs = new ChangeScene();

    }

    private void Update()
    {
        if (Time.time - startTime  > 3)
        {
            LoadCredits();
        }
    }
}
