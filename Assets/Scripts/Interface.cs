using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interface : MonoBehaviour
{


    public GameObject BalanceAssociee;
    private bool troisplateaux;
    BalanceCalcul plateau;
    int[] etatsinterface = new int[] { 0, 0 };

    public GameObject FlecheDownG;
    public GameObject FlecheUpG;
    public GameObject EgaleG;
    public GameObject FlecheDownD;
    public GameObject FlecheUpD;
    public GameObject EgaleD;




    // Start is called before the first frame update
    void Start()
    {


        plateau = BalanceAssociee.GetComponent<BalanceCalcul>();

        //Identifier si l'interface est associee a une balance a trois ou deux plateaux
        if (plateau.PlateauxAssociees.Count == 1)
        {
            troisplateaux = false;
        }

        if (plateau.PlateauxAssociees.Count == 2)
        {
            troisplateaux = true;
        }

        //Desactiver tous les symboles
        FlecheDownD.SetActive(false);
        FlecheUpD.SetActive(false);
        EgaleD.SetActive(false);
        FlecheDownG.SetActive(false);
        FlecheUpG.SetActive(false);
        EgaleG.SetActive(false);

        //Ecouter pour une changement de balance dans tous les plateaux associes
        plateau.EvenementUpdate.AddListener(UpdateBalance);
        foreach( GameObject obj in plateau.PlateauxAssociees)
        {
            obj.GetComponent<BalanceCalcul>().EvenementUpdate.AddListener(UpdateBalance);
        }

    }

    //Montrer l'etat de la balance en activant le symbole approprie
    private void UpdateBalance()
    {

        FlecheDownD.SetActive(false);
        FlecheUpD.SetActive(false);
        EgaleD.SetActive(false);
        FlecheDownG.SetActive(false);
        FlecheUpG.SetActive(false);
        EgaleG.SetActive(false);

        etatsinterface[0] = plateau.getEtats()[0];
        if (plateau.getEtats()[0] == 1)
        {
            FlecheDownG.SetActive(true);
        }
        else if (plateau.getEtats()[0] == 2)
        {
            FlecheUpG.SetActive(true);
        }
        else if (plateau.getEtats()[0] == 3)
        {
            EgaleG.SetActive(true);
        }



        if (troisplateaux)
        {
            etatsinterface[1] = plateau.getEtats()[1];
            if (plateau.getEtats()[1] == 1)
            {
                FlecheDownD.SetActive(true);
            }
            else if (plateau.getEtats()[1] == 2)
            {
                FlecheUpD.SetActive(true);
            }
            else if (plateau.getEtats()[1] == 3)
            {
                EgaleD.SetActive(true);
            }
        }

    }


}
