using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour
{
    public string nameOfTheLevel1;
    public string nameOfTheLevel2;
    public string nameOfTheLevel3;
    public string nameOfTheLevel4;
    public string nameOfTheLevel5;
    public GameObject MainMenuPanel;
    public GameObject MenuPanelOptions;
    public GameObject SelectLevelPanel;
    public GameObject EventSystemMainMenu;
    public GameObject EventSystemOptions;
    public GameObject EventSystemMenuLevelSelect;
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
        SceneManager.LoadScene(nameOfTheLevel1);
    }

    public void SelectLevel2()
    {
        SceneManager.LoadScene(nameOfTheLevel2);
    }

    public void SelectLevel3()
    {
        SceneManager.LoadScene(nameOfTheLevel3);
    }

    public void SelectLevel4()
    {
        SceneManager.LoadScene(nameOfTheLevel4);
    }

    public void SelectLevel5()
    {
        SceneManager.LoadScene(nameOfTheLevel5);
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