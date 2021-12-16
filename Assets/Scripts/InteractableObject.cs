using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private GameObject _objectInstance;
    private Vector3 _offset = new Vector3(0, 2f, 0);

    public void InstantiateUI(Transform player)
    {
        _objectInstance = Instantiate(PlayerUI.instance.interactUI, player.position + _offset, Quaternion.identity, player);
    }

    public void DestroyInstance()
    {
        Destroy(_objectInstance);
    }
}
