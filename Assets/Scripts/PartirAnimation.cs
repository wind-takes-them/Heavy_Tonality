using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartirAnimation : MonoBehaviour
{
    private GameObject square;
    private int awake;

    void Start()
    {
        square = GameObject.Find("MovingSquare");

    }


    void Update()
    {
        if (awake < 2)
        {
            awake = square.GetComponent<AwakeReaction>().GetAwake();
        }

        if (awake == 2)
        {
            this.gameObject.GetComponent<Animator>().Play("Anim_Square");
            awake++;
        }
    }
}
