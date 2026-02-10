using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Model
{
    public class GameEndEventArgs :EventArgs
    {
        public bool PlayerDead { get; }
        public bool EnemyDead { get; }
        public int Score { get; set; }

        public GameEndEventArgs(bool playerDead, bool enemyDead, int score)
        {
            PlayerDead = playerDead;
            EnemyDead = enemyDead;
            Score = score;
        }
    }
}
