using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AudioClip _deathSound;

    [HideInInspector] public bool haveKeyForNextLevel = false;

    public void Die()
    {
        PlayerController pc = GetComponent <PlayerController>();
        pc.TriggerAnimation("Death");
        pc.canMove = false;
        AudioManager.instance.PlayClipAt(_deathSound,transform.position);        
        LevelLoader.instance.LoadScene(LevelLoader.instance.GetCurrentSceneName());
    }
}
