using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    public string DoomLike;
    public GameObject MainMenuPanel;
    public GameObject MenuPanelOptions;
    public GameObject SelectLevelPanel;
    public GameObject EventSystemMainMenu;
    public GameObject EventSystemOptions;
    public GameObject EventSystemMenuLevelSelect;
    public Animator transitionAnimator;

    public void OpenPlay()
    {
        MainMenuPanel.SetActive(false);
        EventSystemMainMenu.SetActive(false);
        SelectLevelPanel.SetActive(true);
        EventSystemMenuLevelSelect.SetActive(true);
    }

    public void ClosePlay()
    {
        MainMenuPanel.SetActive(true);
        EventSystemMainMenu.SetActive(true);
        SelectLevelPanel.SetActive(false);
        EventSystemMenuLevelSelect.SetActive(false);
    }

    public void SelectLevel1()
    {
        StartCoroutine(PlayTransitionAndLoad());
    }

    private IEnumerator PlayTransitionAndLoad()
    {
        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Start");
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(DoomLike);
    }

    public void OpenOptions()
    {
        MainMenuPanel.SetActive(false);
        EventSystemMainMenu.SetActive(false);
        MenuPanelOptions.SetActive(true);
        EventSystemOptions.SetActive(true);
    }

    public void CloseOptions()
    {
        MainMenuPanel.SetActive(true);
        EventSystemMainMenu.SetActive(true);
        MenuPanelOptions.SetActive(false);
        EventSystemOptions.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quited");
        Application.Quit();
    }
}