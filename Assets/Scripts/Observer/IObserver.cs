using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver 
{
    void OnNofity(string Message);
    void OnHealthUpdate(float health);
}
