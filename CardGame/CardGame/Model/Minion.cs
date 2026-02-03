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

        private string[] enemyType = { "Slime", "Skeleton", "Cultist", "Golem", "Zombie"};

        public Minion()
        {
            _cards = new Card[3];

            GenerateStats();
            GenerateDeck();
            PickNextCard();
        }
        private void GenerateStats() 
        {
            _name = r.Next(attributes.Length)+ " " + r.Next(enemyType.Length);
            _health = r.Next(20, 50);
            int which = r.Next(0, 1);
            if (which == 0)
            {
                _shield = 0;
            }
            else { _shield = 5; }
        }

        private void GenerateDeck()
        {
            _cards[0] = new Card("Basic Attack", Card.Actions.Attack, 5);
            _cards[1] = new Card("Basic Heal", Card.Actions.Heal, 3);
            _cards[2] = new Card("Basic Shield", Card.Actions.Shield, 3);
        }

        private void PickNextCard()
        {
            _nextCard = _cards[r.Next(_cards.Length)];

            OnPropertyChanged(nameof(NextCard));
            OnPropertyChanged(nameof(NextMove));
        }

        public void UseCard()
        {
            if(_nextCard.Action == Card.Actions.Heal)
            {
                _health += _nextCard.Value;
            }
            else if(_nextCard.Action == Card.Actions.Shield)
            {
                _shield += _nextCard.Value;
            }

            OnPropertyChanged(nameof(Health));
            PickNextCard();

        }

        public void Damage(int damage)
        {
            
                int dmgleft = 0;
                if (_shield >= damage) {
                    
                    _shield -= damage;
                }
                else
                {
                    dmgleft = damage - _shield;
                    _shield = 0;
                    _health -= dmgleft;
                    if (_health < 0) _health = 0;
                }
            
            OnPropertyChanged(nameof(Health));
        }

    }
}
