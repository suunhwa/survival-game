using UnityEngine;

public class TreeInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Vector3 pos = transform.position + Vector3.up * 0.5f;

        Vector3 randomOffset = new Vector3(
            Random.Range(-0.5f, 0.5f),
            0,
            Random.Range(-0.5f, 0.5f)
        );

        Vector3 spawnPos = pos + randomOffset;

        ItemPoolManager.Instance.Spawn("Wood", "Material", 4, spawnPos); 

        Destroy(gameObject);
    }
}
