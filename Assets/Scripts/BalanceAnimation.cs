using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BalanceAnimation : MonoBehaviour
{

    public GameObject plateauAssocie;
    public GameObject cercleAssocie;

    Animator anim;
    Animator animcCercle;

    void Start()
    {
        //avoir les references vers les composantes animateur des parties de la balance et indiquer leur type
        anim = this.GetComponent<Animator>();
        animcCercle = cercleAssocie.GetComponent<Animator>();
        anim.SetBool("IsSpirale", true);
        animcCercle.SetBool("IsSpirale", false);

        //Ecouter pour un changement de balance
        plateauAssocie.GetComponent<BalanceCalcul>().EvenementUpdate.AddListener(BalanceUpdate);
        

    }


    //Sur changement, activer l'animation
    void BalanceUpdate()
    {
        if (plateauAssocie.GetComponent<BalanceCalcul>().resolu)
        {
            int i = 1;
            int count = plateauAssocie.GetComponent<BalanceCalcul>().PlateauxAssociees.Count;
            foreach (GameObject obj in plateauAssocie.GetComponent<BalanceCalcul>().PlateauxAssociees)
            {
                obj.GetComponent<BalanceCalcul>().CheckBalance();
                if (!obj.GetComponent<BalanceCalcul>().resolu)
                {
                    break;

                }
                if (i == count)
                {
                    anim.SetBool("IsActivated", true);
                    animcCercle.SetBool("IsActivated", true);
                }
                i++;

            }
        }
    }

}