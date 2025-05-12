using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour, IObserver<int>
{
    public Slider healthSlider;
    public Image fillImg;

    public void OnNotify(int newHealth)
    {
        healthSlider.value = newHealth;
        UpdateFillColor(newHealth);
        Debug.Log($"UI 업데이트: 체력 = {newHealth}");
    }

    private void UpdateFillColor(int hp)
    {
        if (hp > 60)
        {
            fillImg.color = Color.green;
        }
        else if (hp > 30) 
        {
             fillImg.color= Color.yellow;
        }
        else
        {
            fillImg.color= Color.red;
        }
    }
}
