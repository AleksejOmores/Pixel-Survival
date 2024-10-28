using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Observer
{
    public class Ally : MonoBehaviour, IObserver
    {
        public void OnHealthUpdate(float health)
        {
            Debug.Log($"Здоровье игрока: {health}%");
        }

        public void OnNofity(string Message)
        {
            Debug.Log($"Уведомление: {Message}");
        }
    }
}
