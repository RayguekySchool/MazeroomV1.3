using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [Header("Scene Names (as in Build Settings)")]
    public string sceneA;
    public string sceneB;

    [Header("UI Elements")]
    public Slider progressBar;

    /// <summary>
    /// Chame este método para carregar a Scene A.
    /// </summary>
    public void LoadSceneA()
    {
        StartCoroutine(LoadSceneAsync(sceneA));
    }

    /// <summary>
    /// Chame este método para carregar a Scene B.
    /// </summary>
    public void LoadSceneB()
    {
        StartCoroutine(LoadSceneAsync(sceneB));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        if (progressBar != null)
            progressBar.value = 0f;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            if (progressBar != null)
                progressBar.value = progress;

            yield return null;
        }
    }
}