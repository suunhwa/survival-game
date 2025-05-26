using UnityEngine;

public class WarningSound : MonoBehaviour, IObserver<int>
{
    public void OnNotify(int newHealth)
    {
        if (newHealth <= 30)
            Debug.Log("ü�� ����! ��� ����");
    }
}
