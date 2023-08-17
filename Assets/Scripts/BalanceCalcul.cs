using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BalanceCalcul : MonoBehaviour
{
    //Permet de choisir les objets requis pour activer la balance
    public List<GameObject> ObjetsNecessaires = new List<GameObject>();
   
    private List<GameObject> ObjetsPresents = new List<GameObject>();
    private XRGrabInteractable interactable = null;
    public bool resolu = false;
    
    //En contact avec un des objets faisant partie du puzzle, premdre sa
    //composante XRGrab et l'ajouter dans la liste des objets presents.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objet"))
        {
            interactable= other.GetComponent<XRGrabInteractable>();
            interactable.selectExited.AddListener(OnSelectExited);

            ObjetsPresents.Add(other.gameObject);
        }

    }

    //Quand l'objet est relache, verifier le puzzle et enlever la composante XR referencee.
    //Jouer le son de succes si le puzzle est resolu
    private void OnSelectExited(SelectExitEventArgs arg0)
    {
        resolu = CheckBalance();
        if (resolu) { this.gameObject.GetComponent<AudioSource>().Play(); }
        interactable = null;
    }

    //En enlevant un objet du plateau, l'enlever de la liste des objets presents
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Objet"))
        {
            ObjetsPresents.Remove(other.gameObject);
        }
    }

    //Comparer la liste d'objets present avec la liste des objets requis
    private bool CheckBalance()
    {
        if (ObjetsPresents.Count != ObjetsNecessaires.Count) { return false; }
        foreach(GameObject obj in ObjetsNecessaires) { 
            if(!ObjetsPresents.Contains(obj)) { return false; }
        }
        return true;
    }


}
