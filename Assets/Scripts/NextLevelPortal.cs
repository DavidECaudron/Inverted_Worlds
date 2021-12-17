using UnityEngine;

public class NextLevelPortal : MonoBehaviour
{
    [SerializeField] private string _nextLevelName;
    [SerializeField] AudioClip _openDoorSound;
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if (collider.gameObject.GetComponent<Player>().haveKeyForNextLevel)
            {
                AudioManager.instance.PlayClipAt(_openDoorSound, transform.position);
                LevelLoader.instance.LoadScene(_nextLevelName);
            }
        }
    }

}
