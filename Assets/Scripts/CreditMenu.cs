using UnityEngine;

public class CreditMenu : MonoBehaviour
{
    void GoToMainMenu()
    {
        LevelLoader.instance.LoadScene("Menu");
    }
}
