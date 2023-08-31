using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;

public class PartirParticules : MonoBehaviour
{

    public GameObject balanceAssociee;
    public GameObject directionalLight;
    private ParticleSystem particules;
    public Material skyMaterial;


    // Start is called before the first frame update
    void Start()
    {
        balanceAssociee.GetComponent<BalanceCalcul>().EvenementUpdate.AddListener(BalanceChangee);
        particules= GetComponent<ParticleSystem>();
        var emission = particules.emission;
        emission.enabled = false;

    }



    private void BalanceChangee()
    {
        var emission = particules.emission;

        //sur changement dans les balances, verifier leur egalite
        if (balanceAssociee.GetComponent<BalanceCalcul>().resolu)
        {

            int i = 1;
            foreach (GameObject obj in balanceAssociee.GetComponent<BalanceCalcul>().PlateauxAssociees)
            {
                obj.GetComponent<BalanceCalcul>().CheckBalance();
                if (!obj.GetComponent<BalanceCalcul>().resolu)
                {
                    break;

                }
                if (i == 2)
                {
                    //Partir l'emission des particules
                    emission.enabled = true;

                    //modifier la lumiere directionelle et changer le materiel du skybox
                    directionalLight.GetComponent<Light>().intensity = 0.1f;
                    directionalLight.GetComponent<Light>().color = Color.gray;
                    RenderSettings.skybox = skyMaterial;
                }
                i++;

            }
        }
    }



}
