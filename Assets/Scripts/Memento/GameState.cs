using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Memento
{
    public class GameState
    {
        public Vector3 PlayerPosition { get; set; }
        public int PlayerHealth { get; set; }
        public int CurrentWave { get; set; }
        public List<Vector3> EnemyPosition { get; set; }

        public GameState(Vector3 playerPosition, int playerHealth, int currentWave, List<Vector3> enemyPosition)
        {
            PlayerPosition = playerPosition;
            PlayerHealth = playerHealth;
            CurrentWave = currentWave;
            EnemyPosition = enemyPosition;
        }
    }
}
