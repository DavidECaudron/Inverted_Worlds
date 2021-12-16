using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _respawnPoint;

    public static GameManager instance;

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
        LevelLoader.instance.OnSceneIsLoaded += SetupLevel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (FindObjectOfType<Menu>() == null)
            {
                HandlePauseMenu();
            }
        }
    }

    private void HandlePauseMenu()
    {
        PlayerUI.instance.HandlePauseMenu();
    }

    private void SetupLevel()
    {
        if (LevelLoader.instance.InMainMenu()) return;

        GetRespawnPointInScene();
        SpawnPlayer();
    }

    private void GetRespawnPointInScene()
    {
        _respawnPoint = FindObjectOfType<RespawnPoint>().gameObject;
    }

    public GameObject GetRespawnPoint()
    {
        return _respawnPoint;
    }

    private void SpawnPlayer()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.canMove = false;

        RespawnPoint respawn = FindObjectOfType<RespawnPoint>();
        player.transform.position = respawn.transform.position;
    }
}
