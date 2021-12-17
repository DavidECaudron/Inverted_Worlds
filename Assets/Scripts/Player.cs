using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AudioClip _deathSound;
    [SerializeField] GameObject _death_vfx;

    [HideInInspector] public bool haveKeyForNextLevel = false;
    public void Die()
    {
        PlayerController pc = GetComponent<PlayerController>();
        pc.TriggerAnimation("Death");
        pc.canMove = false;
        GameObject vfx = Instantiate(_death_vfx);
        vfx.transform.position = transform.position;
        AudioManager.instance.PlayClipAt(_deathSound, transform.position);

        StartCoroutine(WaitBeforeReload(1.5f));        
    }

    private IEnumerator WaitBeforeReload(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        LevelLoader.instance.LoadScene(LevelLoader.instance.GetCurrentSceneName());
    }
}
