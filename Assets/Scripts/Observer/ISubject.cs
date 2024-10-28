using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObject(IObserver observer);
    void NotifyObserver(string message);
    void NotifyHealthObserver(float health);
}
