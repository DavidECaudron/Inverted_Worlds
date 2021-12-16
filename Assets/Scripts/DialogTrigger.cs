using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private Dialog _dialog;

    private bool _inRange = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _inRange)
        {
            if (!DialogManager.instance.OnDialog)
            {
                DialogManager.instance.StartDialog(_dialog);
            }
            else
            {
                DialogManager.instance.NextLine();
            }            
        }

        //TODO faire apparaitre dialog ui, dans player UI
        //Y afficher le text de la ligne avec l'index actuelle
        //Quand appui sur une touche passer à ligne suivante/fermer le dialogue
        //afficher les lettre une par une avec coroutine pour effet visuel
        //Pourvoir afficher tout la phrase d'un seul coup si interaction
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _inRange = false;

            if (DialogManager.instance.OnDialog)
            {
                DialogManager.instance.CloseDialogUI();
            }
        }
    }
}
