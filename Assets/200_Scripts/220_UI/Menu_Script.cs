using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    // Nom du niveau que vous souhaitez relancer
    public string Level_1 = "Level_1";
    public string Main_Menu = "Main_Menu";


    // Méthode appelée lorsqu'on appuie sur le bouton
    public void RelancerLeNiveau()
    {
        // Charger à nouveau le même niveau
        SceneManager.LoadScene(Level_1);
    } 
    
    public void RetourMenu()
    {
        // Charger à nouveau le même niveau
        SceneManager.LoadScene(Main_Menu);
    }


}