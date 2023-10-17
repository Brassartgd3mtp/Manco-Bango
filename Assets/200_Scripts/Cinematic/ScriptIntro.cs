using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptIntro : MonoBehaviour


{
    public GameObject Player;
    public GameObject PlayerCam;
    public GameObject Ennemi;
    public GameObject ui;



    // Start is called before the first frame update
    void Start()
    {


        Player.SetActive(false);
        PlayerCam.SetActive(false);
        Ennemi.SetActive(false);
        ui.SetActive(false);


    }




    public void FinAnim()
    {
        Player.SetActive(true);
        PlayerCam.SetActive(true);
        Ennemi.SetActive(true);
        ui.SetActive(true);
        GetComponent<Camera>().depth = -1;
    }




}