using UnityEngine;

public class NextLevelPortal : MonoBehaviour
{
    [SerializeField] private string _nextLevelName;
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if (collider.gameObject.GetComponent<Player>().haveKeyForNextLevel)
            {
                LevelLoader.instance.LoadScene(_nextLevelName);
            }
        }
    }

}
