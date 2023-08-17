using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AwakeReaction : MonoBehaviour
{

    private GameObject balanceDroite;
    private GameObject balanceGauche;
    private int awake = 0;
    private Color lerpedColor = Color.black;
    private float t = 0;
    private XRGrabInteractable interactable;

    private Material material;

    private void Start()
    {
        //recuperer le materiel de cet objet et allumer l'emissif
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        material.EnableKeyword("_EMISSION");

        //Recuperer et desactiver la composante Grab de cet objet
        interactable = this.gameObject.GetComponent<XRGrabInteractable>();
        interactable.enabled = false;

        //Avoir une reference vers la balance
        balanceDroite = GameObject.Find("Plate_Right");
        balanceGauche = GameObject.Find("Plate_Left");

    }

    private void Update()
    {
        //Verifier la resolution des deux balances tant qu'elles ne sont pas resolues
        if (awake == 0)
        {
            if (balanceDroite.GetComponent<BalanceCalcul>().resolu && balanceGauche.GetComponent<BalanceCalcul>().resolu)
            {
                awake = 1;

            }
        }

        //Une fois les balances resolues, graduellement changer la couleur de l'emission de cet objet
        if (awake == 1)
        {
            t += Time.deltaTime / 3.0f;
            lerpedColor = Color.Lerp(Color.black, Color.red, t);
            material.SetColor("_EmissionColor", lerpedColor);

                //Une fois transition terminee, activer la composante grab et partir l'audio
                if (t>= 1)
                {
                    interactable.enabled = true;
                    this.gameObject.GetComponent<AudioSource>().Play();
                    awake = 2;
                }

        }

    }

    public int GetAwake()
    {
        return awake;
    }

}
