using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private AudioClip _destructionSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<GravityObject>() != null)
        {
            AudioManager.instance.PlayClipAt(_destructionSound, transform.position);
            //TODO animation ou effet visuel de destruction du crystal

            Destroy(gameObject);
        }
    }
}
