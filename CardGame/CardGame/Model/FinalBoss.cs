using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.ViewModel;

namespace CardGame.Model
{
    public class FinalBoss :Entity
    {
        Random r = new Random();
        private string[] attributes = { "Slime King", "Skeleton King", "Cultist Leader", "Golem Prime", "Emperor" };
        private double _difficulty;

        public FinalBoss(double difficulty)
        {
            _cards = new Card[6];
            _difficulty = difficulty;
            GenerateStats();
            GenerateDeck();
            PickNextCard();

        }
        private void GenerateStats()
        {
            _name = $"{r.Next(attributes.Length)}";
            _health = (int)(r.Next(80, 150) * _difficulty);
            int which = r.Next(0, 1);
            if (which == 0)
            {
                _shield = 10;
            }
            else { _shield = 20; }
            _maxHealth = _health;
        }

        private void GenerateDeck()
        {
            _cards[0] = new Card("Basic Attack", Card.Actions.Attack, (int)(r.Next(7, 12) * _difficulty));
            _cards[1] = new Card("Heavy Attack", Card.Actions.Attack, (int)(r.Next(10, 15) * _difficulty));
            _cards[2] = new Card("Basic Heal", Card.Actions.Heal, (int)(r.Next(7, 10) * _difficulty));
            _cards[3] = new Card("Cursed Heal", Card.Actions.Heal, (int)(r.Next(10, 15) * _difficulty));
            _cards[4] = new Card("Basic Shield", Card.Actions.Shield, (int)(r.Next(7, 10) * _difficulty));
            _cards[5] = new Card("Heavy Shield", Card.Actions.Shield, (int)(r.Next(10, 15) * _difficulty));
        }
    }
}
