using System.Collections.Generic;
using UnityEngine;

public abstract class  Observer : MonoBehaviour {
    public abstract void OnNotify(GameObject observable);
    
}


public abstract class Observable : MonoBehaviour
{
    private List<Observer> _observers = new List<Observer>();

    public void Register(Observer observer) {
        _observers.Add(observer);
    }
    
    public void Notify(GameObject value) {
        foreach (var observer in _observers) {
            observer.OnNotify(value);
        }
    }
}
