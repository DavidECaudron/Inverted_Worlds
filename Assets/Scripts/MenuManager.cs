using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject _mainMenu, _inputsMenu;
    public static MenuManager MenuManagerInstance;
    public static MenuManager Instance
    {
        get
        {
            if (MenuManagerInstance == null)
            {
                MenuManagerInstance = FindObjectOfType(typeof(MenuManager)) as MenuManager;
            }
            return MenuManagerInstance;
        }
    }

    private void Awake()
    {
        ShowMenu(TYPESMENU.MAIN_MENU);
    }


    public void ShowMenu(TYPESMENU localTypeMenu)
    {
        switch (localTypeMenu)
        {
            case TYPESMENU.MAIN_MENU:
                _mainMenu.SetActive(true);
                _inputsMenu.SetActive(false);
                break;
            case TYPESMENU.INPUTS_MENU:
                _mainMenu.SetActive(false);
                _inputsMenu.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void OnClickInputsMenuOpen()
    {
        ShowMenu(TYPESMENU.INPUTS_MENU);
    }
    public void OnClickMainMenuOpen()
    {
        ShowMenu(TYPESMENU.MAIN_MENU);
    }

}
public enum TYPESMENU
{
    NONES,
    MAIN_MENU,
    INPUTS_MENU,

}
