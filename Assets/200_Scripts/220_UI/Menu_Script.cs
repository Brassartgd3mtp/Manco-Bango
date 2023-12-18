using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    // Méthode appelée lorsqu'on appuie sur le bouton
    public void RelancerLeNiveau()
    {
        // Charger à nouveau le même niveau
        SceneManager.LoadScene(1);
    } 
    
    public void RetourMenu()
    {
        // Charger à nouveau le même niveau
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}