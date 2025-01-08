using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public bool savedAlready;
    public GameObject mainPanel; // Varsayýlan panel
    public GameObject continuePanel; // Kayýtlý veriyi gösteren panel

    void Start()
    {
     
        if (ES3.KeyExists("SelectedCharacter"))
        {
            
            mainPanel.SetActive(false);
            continuePanel.SetActive(true);
         
        }
        else
        {
        
            mainPanel.SetActive(true);
            continuePanel.SetActive(false);
         
        }



    }
 

    public void ConfirmSelection(int selectedCharacterIndex)
    {
        ES3.Save("SelectedCharacter", selectedCharacterIndex); 
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void StartSavedGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
