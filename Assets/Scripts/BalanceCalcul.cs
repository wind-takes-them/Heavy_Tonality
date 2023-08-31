using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BalanceCalcul : MonoBehaviour
{
    
    

    private int poidspresent;
    public List<GameObject> PlateauxAssociees= new List<GameObject>();

    public UnityEvent EvenementUpdate;
   
    private List<GameObject> ObjetsPresents = new List<GameObject>();
    private XRGrabInteractable interactable = null;
    public bool resolu = false;
    int[] etats = new int[] {0,0};
   


    public int[] getEtats()
    {
        return etats;
    } 


    //En contact avec un des objets faisant partie du puzzle, premdre sa
    //composante XRGrab et l'ajouter dans la liste des objets presents.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objet"))
        {
            interactable= other.GetComponent<XRGrabInteractable>();
            interactable.selectExited.AddListener(OnSelectExited);

            ObjetsPresents.Add(other.gameObject);

            Material material= other.GetComponent<Renderer>().material;
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", Color.cyan);

        }

    }

    //Quand l'objet est relache, verifier le puzzle et enlever la composante XR referencee.
    //Jouer le son de succes si le puzzle est resolu
    public void OnSelectExited(SelectExitEventArgs arg0)
    {
        
        foreach (GameObject obj in PlateauxAssociees)
        {
                //Mettre a jour les balances associees
                obj.GetComponent<BalanceCalcul>().CheckBalance();
        }

        CheckBalance();

        if (etats[0] == 3) {
            this.gameObject.GetComponent<AudioSource>().Play();
        }

        interactable = null;


    }

    //En enlevant un objet du plateau, l'enlever de la liste des objets presents
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Objet"))
        {
            Material material = other.GetComponent<Renderer>().material;
            material.SetColor("_EmissionColor", Color.black);

            ObjetsPresents.Remove(other.gameObject);
        }
    }

    //Comparer le poids des objets presents avec le poids des balances associees
    public void CheckBalance()
    {
        int i = 0;
       

            foreach (GameObject obj in PlateauxAssociees)
            {

                if (GetPoidsPresent() < obj.GetComponent<BalanceCalcul>().GetPoidsPresent()) 
                { 
                    etats[i] = 1;

                    //Envoyer un signal de changement de balance
                    EvenementUpdate.Invoke();
                }
                else if (GetPoidsPresent() > obj.GetComponent<BalanceCalcul>().GetPoidsPresent()) 
                { 
                    etats[i] = 2;

                    //Envoyer un signal de changement de balance
                    EvenementUpdate.Invoke();
                }
                else if (GetPoidsPresent() == obj.GetComponent<BalanceCalcul>().GetPoidsPresent() && GetPoidsPresent() != 0) 
                { 
                    etats[i] = 3;


                }
                if (etats[i] == 3) 
                { 
                    resolu = true;

                    //Envoyer un signal de changement de balance
                    EvenementUpdate.Invoke();
                }

                i++;

            }

        
    }


    //Calculer le poids des objets presents sur la plate
    public int GetPoidsPresent()
    {
        poidspresent = 0;
        foreach(GameObject obj in ObjetsPresents)
        {
            poidspresent = poidspresent + obj.GetComponent<Poids>().poids;
        }

        return poidspresent;
    }




}
