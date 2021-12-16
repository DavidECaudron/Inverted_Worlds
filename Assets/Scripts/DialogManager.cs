using System.Collections;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    [SerializeField] GameObject _dialogUI;
    [SerializeField] TMP_Text _dialogText;

    private Dialog _currentDialog;
    private int _currentIndex = 0;

    public bool OnDialog
    {
        get{ return _dialogUI.activeInHierarchy; }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void StartDialog(Dialog dialog)
    {
        HandleDialogUI();
        _currentIndex = 0;
        _currentDialog = dialog;

        StartCoroutine(DisplayDialog(_currentDialog.lines[_currentIndex]));
    }

    public void NextLine()
    {
        _currentIndex++;
        if(_currentIndex >= _currentDialog.lines.Length)
        {
            HandleDialogUI();
            return;
        }
        StopAllCoroutines();
        StartCoroutine(DisplayDialog(_currentDialog.lines[_currentIndex]));
    }

    private void HandleDialogUI()
    {
        _dialogUI.SetActive(!_dialogUI.activeSelf);
        if (!_dialogUI.activeInHierarchy)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator DisplayDialog(string lines)
    {
        _dialogText.text = string.Empty;
        for (int i = 0; i < lines.Length; i++)
        {
            _dialogText.text += lines[i];
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void CloseDialogUI()
    {
        _dialogUI.SetActive(false);
        StopAllCoroutines();
    }
}
