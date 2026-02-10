using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.ViewModel;
using CardGame.Model;

namespace CardGame.Model
{
    public class Minion : Entity
    {
        static Random r = new Random();
        private string[] attributes = { "Vicious", "Cursed", "Ravenous", "Corrupted", "Abyssal", "Infernal" };

        private string[] enemyType = { "Slime", "Skeleton", "Cultist", "Golem", "Zombie" };

        private double _difficulty;

        

        public Minion(double difficulty)
        {
            _cards = new Card[3];
            _difficulty = difficulty;
            GenerateStats();
            GenerateDeck();

        }
        private void GenerateStats()
        {
            _name = r.Next(attributes.Length) + " " + r.Next(enemyType.Length);
            _health = (int)(r.Next(20, 50) * _difficulty);
            int which = r.Next(0, 1);
            if (which == 0)
            {
                _shield = 0;
            }
            else { _shield = 5; }
            _maxHealth = _health;
        }

        private void GenerateDeck()
        {
            _cards[0] = new Card("Basic Attack", Card.Actions.Attack, (int)(r.Next(4, 10) * _difficulty));
            _cards[1] = new Card("Basic Heal", Card.Actions.Heal, (int)(r.Next(2, 5) * _difficulty));
            _cards[2] = new Card("Basic Shield", Card.Actions.Shield, (int)(r.Next(2, 8) * _difficulty));
        }

    }
}
