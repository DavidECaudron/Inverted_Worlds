using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] AudioClip _pickupSound;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            AudioManager.instance.PlayClipAt(_pickupSound, transform.position);
            collider.gameObject.GetComponent<Player>().haveKeyForNextLevel = true;
            PlayerUI.instance.keyUI.SetActive(true);
            PlayerUI.instance.missingKeyUI.SetActive(false);
            Destroy(gameObject);
        }
    }
}
