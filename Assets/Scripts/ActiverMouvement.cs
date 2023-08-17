using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActiverMouvement : MonoBehaviour
{
    private GameObject square;
    private int awake;

    void Start()
    {
        square = GameObject.Find("MovingSquare");

        //Enlever la possibilite de se teleporter sur cette area
        this.gameObject.GetComponent<MeshCollider>().enabled = false;
    }

    void Update()
    {
        //Tant que le carre en mouvement n'a pas termine sa transition, recuperer la valeur d'awake
        if (awake < 2)
        {
            awake = square.GetComponent<AwakeReaction>().GetAwake();
        }

        //Quand le carre a termine sa transition, reactiver la possibilite de se teleporter
        if (awake == 2)
        {
            this.gameObject.GetComponent<MeshCollider>().enabled = true;
            awake++;
        }
    }
}
