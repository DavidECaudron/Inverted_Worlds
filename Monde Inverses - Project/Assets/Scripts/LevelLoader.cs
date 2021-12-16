using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject _loadingScreenUI;
    [SerializeField] float _fadingTime = 2f;

    public static LevelLoader instance;

    [SerializeField] CanvasGroup _canvasGroup;

    public delegate void LevelLoading();

    public event LevelLoading OnStartLoadLevel;
    public event LevelLoading OnLoadLevelCompleted;
    public event LevelLoading OnSceneIsLoaded;

    private bool _onLoadingScene = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public bool InMainMenu()
    {
        return SceneManager.GetActiveScene().name == "Menu";
    }

    public void LoadScene(string sceneName)
    {
        if (_onLoadingScene) return;
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        _onLoadingScene = true;
        OnStartLoadLevel?.Invoke();

        //Fondu vers le noir
        _canvasGroup.alpha = 0;
        _loadingScreenUI.SetActive(true);

        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += Time.deltaTime / _fadingTime;
            yield return null;
        }

        //Chrager la scene
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        //Attendre que la scene soit chargée pour continuer
        while (!asyncOperation.isDone)
        {
            yield return new WaitForSeconds(.1f);
        }

        OnSceneIsLoaded?.Invoke();

        //Une fois la scene chargee, faire fondu inverse
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= Time.deltaTime / _fadingTime;
            yield return null;
        }

        _loadingScreenUI.SetActive(false);

        OnLoadLevelCompleted?.Invoke();
        _onLoadingScene = false;
    }
}
