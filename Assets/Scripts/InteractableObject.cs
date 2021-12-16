using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private GameObject _objectInstance;
    private Vector3 _offset = new Vector3(0, 2f, 0);

    //private void Start()
    //{
    //    _player = FindObjectOfType<PlayerController>().transform;
    //}

    public void InstantiateUI(Transform player)
    {
        _objectInstance = Instantiate(PlayerUI.instance.interactUI, player);
        _objectInstance.transform.position = player.position + _offset;
    }

    public void DestroyInstance()
    {
        Destroy(_objectInstance);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Player" && _objectInstance == null)
    //    {
    //        _objectInstance = Instantiate(PlayerUI.instance.interactUI, _player);
    //        _objectInstance.transform.position = _player.position + _offset;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Destroy(_objectInstance);
    //        _objectInstance = null;
    //    }
    //}
}
