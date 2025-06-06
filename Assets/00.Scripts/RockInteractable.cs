using UnityEngine;

public class RockInteractable : MonoBehaviour, IInteractable
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

        ItemPoolManager.Instance.Spawn("Stone", "Material", 6, spawnPos);

        Destroy(gameObject); 
    }
}
