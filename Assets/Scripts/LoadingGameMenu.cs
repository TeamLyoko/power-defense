using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingGameMenu : MonoBehaviour
{
    [SerializeField]
    public Image progressBar;
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(CheckStatus());
    }

     IEnumerator CheckStatus() 
    {
        float progress = 0f;
        progressBar.fillAmount = progress;

         while (progress < 1f)
        {
            progress += Time.deltaTime / 3; 
            progressBar.fillAmount = progress;
            yield return null;
        }
       
       if (APIRequestHandler.IsAuthenticated())
        {
            // Load game scene
            yield return new WaitForSeconds(1);
            Debug.Log("Game menu loads.");
            SceneManager.LoadScene("GameMenu");
        }
        else
        {
            Debug.LogError("Authentication failed!");
        }

       
    }

    

}
