using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private AudioClip _destructionSound;
    [SerializeField] private GameObject _destruction_vfx;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<GravityObject>() != null)
        {
            AudioManager.instance.PlayClipAt(_destructionSound, transform.position);
            //TODO animation ou effet visuel de destruction du crystal
            GameObject go = Instantiate(_destruction_vfx);
            go.transform.position = transform.position;

            Destroy(gameObject);
        }
    }
}
