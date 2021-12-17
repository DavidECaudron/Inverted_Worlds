using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioClip _clickSound;

    public void StartGame()
    {
        AudioManager.instance.PlayClipAt(_clickSound, transform.position);
        LevelLoader.instance.LoadScene("Level03");
    }

    public void Option()
    {
        AudioManager.instance.PlayClipAt(_clickSound, transform.position);
        PlayerUI.instance.HandleOptionMenu();
    }

    public void Quit()
    {
        AudioManager.instance.PlayClipAt(_clickSound, transform.position);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();
#endif
    }

}
