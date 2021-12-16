using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    public GameObject pauseMenu;
    public GameObject interactUI;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void HandlePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
