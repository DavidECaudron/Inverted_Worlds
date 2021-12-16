using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    public GameObject pauseMenu;
    public GameObject interactUI;
    public GameObject keyUI;
    public GameObject missingKeyUI;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        LevelLoader.instance.OnSceneIsLoaded += HideInMainMenu;
    }

    public void HandlePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    private void HideInMainMenu()
    {
        if(LevelLoader.instance.GetCurrentSceneName() == "Menu")
        {
            keyUI.SetActive(false);
            missingKeyUI.SetActive(false);
        }
        else
        {
            keyUI.SetActive(false);
            missingKeyUI.SetActive(true);
        }
    }

}
