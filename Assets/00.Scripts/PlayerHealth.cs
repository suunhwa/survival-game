using System.Collections.Generic;
using UnityEngine;

public interface IObserver<T>
{
    void OnNotify(T data);
}
public interface ISubject<T>
{
    void RegisterObserver(IObserver<T> observer);
    void UnregisterObserver(IObserver<T> observer);
    void NotifyObservers(T data);
}

public class PlayerHealth : MonoBehaviour, ISubject<int>
{
    private List<IObserver<int>> observers = new();
    private int currentHealth = 100;
    void Start()
    {
        RegisterObserver(FindFirstObjectByType<HealthBarUI>());
        RegisterObserver(FindFirstObjectByType<WarningSound>());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H키 누름");
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        NotifyObservers(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("사망");
            // 게임 오버 처리
        }
    }

    public void RegisterObserver(IObserver<int> observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(IObserver<int> observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(int currentHp)
    {
        foreach (var obs in observers)
            obs.OnNotify(currentHp);
    }
}

