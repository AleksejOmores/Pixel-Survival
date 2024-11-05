using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Memento
{
    public class GameState 
    {
        public Vector3 PlayerPosition { get; set; }
        public float PlayerHealth { get; set; }
        public int CurrentWave { get; set; }
        public List<Vector3> EnemyPosition { get; set; }

        public GameState(Vector3 playerPosition, float playerHealth, int currentWave, List<Vector3> enemyPosition)
        {
            PlayerPosition = playerPosition;
            PlayerHealth = playerHealth;
            CurrentWave = currentWave;
            EnemyPosition = enemyPosition;
        }
    }
}
