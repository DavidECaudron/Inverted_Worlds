using UnityEngine;
using UnityEngine.EventSystems;

public class CoreObject : MonoBehaviour
{
    [SerializeField] GameObject[] _prefabToSpawn;
    [SerializeField] GameObject _eventSystemPrefab;
    private bool _hasSpawned = false;
    private void Awake()
    {
        if (_hasSpawned)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
        _hasSpawned = true;
    }

    private void Start()
    {
        if(FindObjectOfType<EventSystem>() == null)
        {
            Instantiate(_eventSystemPrefab);
        }

        foreach (GameObject prefab in _prefabToSpawn)
        {
            GameObject persistentObject = Instantiate(prefab);
            DontDestroyOnLoad(persistentObject);
        }
    }

}
