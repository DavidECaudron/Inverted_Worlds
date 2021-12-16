using UnityEngine;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        LevelLoader.instance.LoadScene("Level03");
    }

    public void Option()
    {
        PlayerUI.instance.HandleOptionMenu();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif
    }

}
