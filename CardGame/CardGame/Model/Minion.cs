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

        public Minion()
        {
            _cards = new Card[3];

            GenerateStats();
            GenerateDeck();
            PickNextCard();
        }
        private void GenerateStats()
        {
            _name = r.Next(attributes.Length) + " " + r.Next(enemyType.Length);
            _health = r.Next(20, 50);
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
            _cards[0] = new Card("Basic Attack", Card.Actions.Attack, r.Next(4, 10));
            _cards[1] = new Card("Basic Heal", Card.Actions.Heal, r.Next(2, 5));
            _cards[2] = new Card("Basic Shield", Card.Actions.Shield, r.Next(2, 8));
        }

        private void PickNextCard()
        {
            _nextCard = _cards[r.Next(_cards.Length)];
            //ez allitolag ujra generalja ha a választott kártya „Heal” action és az Enemy "health” egyenlő a "maxHealth” –el
            do
            {
                _nextCard = _cards[r.Next(_cards.Length)];
            }
            while (_nextCard.Action == Card.Actions.Heal && _health == _maxHealth);

            OnPropertyChanged(nameof(NextCard));
            OnPropertyChanged(nameof(NextMove));
        }

        public void UseCard()
        {
            if (!_dead)
            {
                if (_nextCard.Action == Card.Actions.Heal)
                {
                    _health += _nextCard.Value;

                    if (_health > _maxHealth)
                    {
                        _health = _maxHealth;
                    }
                }
                else if (_nextCard.Action == Card.Actions.Shield)
                {
                    _shield += _nextCard.Value;
                }

                OnPropertyChanged(nameof(Health));
                PickNextCard();
            }
        }

        public void Damage(int damage)
        {
            if (!_dead)
            {
                int dmgleft = damage;

                if (_shield >= dmgleft)
                {
                    _shield -= dmgleft;
                    dmgleft = 0;
                }
                else
                {
                    dmgleft -= _shield;
                    _shield = 0;
                }

                if (dmgleft > 0)
                {
                    _health -= dmgleft;

                    if (_health <= 0)
                    {
                        _health = 0;
                        _dead = true;
                    }
                }

                OnPropertyChanged(nameof(Health));
            }

        }

    }
}
