using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        
    }
        
    void Update()
    {

    }

    public void Restart()
    {
        StartCoroutine(DisableAndReactivatePlayerHealth());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Função genérica para desativar um script do tipo T
    private void DisableScriptOnReset<T>() where T : MonoBehaviour
    {
        T script = FindObjectOfType<T>();
        if (script != null)
        {
            script.enabled = false;
        }
    }

    // Coroutine para desativar e reativar o PlayerHealth
    private IEnumerator DisableAndReactivatePlayerHealth()
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.enabled = false;
            yield return new WaitForSeconds(2f);
            playerHealth.enabled = true;
        }
    }
}