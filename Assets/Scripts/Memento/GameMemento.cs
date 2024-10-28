using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Memento
{
    public class GameMemento
    {
        public GameState State { get; set; }
        public GameMemento(GameState state)
        {
            State = new GameState(state.PlayerPosition, state.PlayerHealth, state.CurrentWave, state.EnemyPosition);
        }
    }
}
