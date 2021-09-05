using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class  Observer : MonoBehaviour {
    public abstract void OnNotify(GameObject observable);
        
}


public abstract class Observable : MonoBehaviour
{
    private List<Observer> _observers = new List<Observer>();

    public void Register(Observer observer) {
        //Debug.Log("register to "+observer.name);
        _observers.Add(observer);
    }
    
    public void UnRegister(Observer observer) {
        //Debug.Log("Unreg to "+observer.name);
        _observers.RemoveAt(0); 
        //_observers.Remove(observer);
    }   
    
    public void Notify(GameObject value) {
        
        for (int i = 0; i < _observers.Count; i++) {
            _observers[i].OnNotify(value);
        }
        
    }
}
