using System.Collections;
using UnityEngine;

public class TreeInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        StartCoroutine(DestroyAfterDelay(3.5f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 pos = transform.position + Vector3.up * 0.5f;

        Vector3 randomOffset = new Vector3(
            Random.Range(-0.5f, 0.5f),
            0,
            Random.Range(-0.5f, 0.5f)
        );

        Vector3 spawnPos = pos + randomOffset;

        ItemPoolManager.Instance.Spawn("Wood", "Material", 4, spawnPos);

        Vector3 appleOffset = new Vector3(
            Random.Range(-0.8f, 0.8f),
            0,
            Random.Range(-0.8f, 0.8f)
        );

        Vector3 applePos = pos + appleOffset;

        ItemPoolManager.Instance.Spawn("Apple", "Food", 1, spawnPos);
        Debug.Log("»ç°ú µå¶øµÊ!");
        Destroy(gameObject);
    }
}
