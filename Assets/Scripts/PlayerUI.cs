using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    public GameObject pauseMenu;
    public GameObject interactUI;
    public GameObject keyUI;
    public GameObject missingKeyUI;
    public GameObject optionMenu;

    [SerializeField] Slider _soundSlider;
    [SerializeField] Slider _musicSlider;
    [SerializeField] private AudioMixerGroup _soundMixer;
    [SerializeField] private AudioMixerGroup _musicMixer;
    [SerializeField] AudioClip _clickSound;

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

        float soundVolumeValue;
        _soundMixer.audioMixer.GetFloat("EffectVolume", out soundVolumeValue);
        _soundSlider.value = Mathf.Clamp(soundVolumeValue, -80, 0);

        float musicVolumeValue;
        _musicMixer.audioMixer.GetFloat("MusicVolume", out musicVolumeValue);
        _musicSlider.value = Mathf.Clamp(musicVolumeValue, -80, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (LevelLoader.instance.GetCurrentSceneName() == "Menu") return;

            if(pauseMenu.activeInHierarchy && optionMenu.activeInHierarchy)
            {
                HandleOptionMenu();
            }
            else
            {
                HandlePauseMenu();
            }            
        }
    }

    public void HandlePauseMenu()
    {
        AudioManager.instance.PlayClipAt(_clickSound, transform.position);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void HandleOptionMenu()
    {
        AudioManager.instance.PlayClipAt(_clickSound, transform.position);
        optionMenu.SetActive(!optionMenu.activeSelf);
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

    public void OnSoundVolumeChanged(float volume)
    {
        _soundMixer.audioMixer.SetFloat("EffectVolume", volume);
    }

    public void OnMusicVolumeChanged(float volume)
    {
        _musicMixer.audioMixer.SetFloat("MusicVolume", volume);
    }

}
