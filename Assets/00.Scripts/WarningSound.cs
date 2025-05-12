using UnityEngine;

public class WarningSound : MonoBehaviour, IObserver<int>
{
    public void OnNotify(int newHealth)
    {
        if (newHealth <= 30)
            Debug.Log("체력 낮음! 경고 사운드");
    }
}
