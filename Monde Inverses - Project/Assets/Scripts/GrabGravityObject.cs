using UnityEngine;

public class GrabGravityObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GravityObject gvt = other.gameObject.GetComponent<GravityObject>();
        if(gvt != null)
        {
            gvt.ForceDisableGravity();
        }
    }
}
