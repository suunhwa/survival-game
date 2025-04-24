using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private float checkRadius = 5.0f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private float activationDistance = 3.0f;

    private Dictionary<Transform, GameObject> activeIcons = new Dictionary<Transform, GameObject>();
    void Start()
    {
        iconPrefab.SetActive(false);
    }
    void Update()
    {
        Collider[] nearByObjects = Physics.OverlapSphere(transform.position, checkRadius, interactableLayer);
        HashSet<Transform> currentObjects = new HashSet<Transform>();

        foreach (Collider collider in nearByObjects)
        {
            Transform targetTransform = collider.transform;
            float distance = Vector3.Distance(transform.position, targetTransform.position);

            if (distance <= activationDistance)
            {
                Debug.LogWarning("Found Obj: " + targetTransform.name);
                currentObjects.Add(targetTransform);
                showIcon(targetTransform);
            }
        }
        UpdateIconPositions();
        RemoveInactiveIcons(currentObjects);
    }
    private Vector3 GetIconPosition(Transform target)
    {
        Transform iconPoint = target.Find("IconPoint");
        return (iconPoint != null)
            ? iconPoint.position
            : target.position + Vector3.up * 2f;
    }

    private void showIcon(Transform target)
    {
        if (activeIcons.ContainsKey(target)) return;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(GetIconPosition(target));
        GameObject iconInstance = Instantiate(iconPrefab, uiCanvas.transform);
        iconInstance.transform.position = screenPosition;
        iconInstance.SetActive(true);

        activeIcons[target] = iconInstance;
    }
    private void UpdateIconPositions()
    {
        foreach (var pair in activeIcons)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(GetIconPosition(pair.Key));
            pair.Value.transform.position = screenPosition;
        }
    }

    private void RemoveInactiveIcons(HashSet<Transform> stillActive)
    {
        List<Transform> toRemove = new List<Transform>();

        foreach (var pair in activeIcons)
        {
            if (!stillActive.Contains(pair.Key))
            {
                Destroy(pair.Value);
                toRemove.Add(pair.Key);
            }
        }

        foreach (var key in toRemove)
        {
            activeIcons.Remove(key);
        }

    }
}

