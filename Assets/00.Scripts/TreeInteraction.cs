using UnityEngine;

public class TreeInteraction : MonoBehaviour, IInteractable
{
    //[SerializeField] private GameObject[] branches;
    //[SerializeField] private float fallDistance = 2f;
    public void Interact()
    {
        Destroy(gameObject);
    }
}
