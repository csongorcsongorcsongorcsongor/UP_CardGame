using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.ViewModel;

namespace CardGame.Model
{
    public class Player:Entity
    {
        private Card[] _currentHand;
        public Card Card1 { get { return _currentHand[0]; } }
        public Card Card2 { get { return _currentHand[1]; } }
        public Card Card3 { get { return _currentHand[2]; } }

        private void GenerateStats() {
            _name = "Hero";
            _health = 100;
            _shield = 10;
            _maxHealth = _health;
        }
        private void GenerateDeck() {
            _cards = new Card[10];
            int index = 0;
            for(int i = 0; i < 3; i++) { _cards[index++] = new Card("Basic Attack", Card.Actions.Attack, 10); }
            for(int i = 0; i < 2; i++) { _cards[index++] = new Card("Basic Heal", Card.Actions.Heal, 2); }
            for(int i = 0; i < 2; i++) { _cards[index++] = new Card("Basic Shield", Card.Actions.Shield, 4); }

            _cards[index++] = new Card("Basic Attack", Card.Actions.Attack, 20);
            _cards[index++] = new Card("Basic Heal", Card.Actions.Heal, 10);
            _cards[index++] = new Card("Basic Shield", Card.Actions.Shield, 8);

        }
        public void GenerateCurrentHand() {
            _currentHand = new Card[3];

            if (random == null) random = new Random();
            for (int i = 0; i < _currentHand.Length; i++)
            {
                int randomIndex = random.Next(_cards.Length);
                _currentHand[i] = _cards[randomIndex];
            }
        }

        public void UseCard(int index) {
            if (!_dead)
            {
                if (_currentHand[index].Action == Card.Actions.Heal)
                {
                    _health += _nextCard.Value;

                    if (_health > _maxHealth)
                    {
                        _health = _maxHealth;
                    }
                    OnPropertyChanged(nameof(_health));
                    _currentHand[index] = new Card("", Card.Actions.Empty, 0);
                }
                else if (_currentHand[index].Action == Card.Actions.Shield)
                {
                    _shield += _nextCard.Value;
                    OnPropertyChanged(nameof(_shield));
                    _currentHand[index] = new Card("", Card.Actions.Empty, 0);
                }
            }
        }
        public void Damage(int damage) {
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
        public Card GetCard(int index) {
            return _currentHand[index];
        }

        public Player() {
            _currentHand = new Card[3];
            _cards = new Card[10];
            _nextCard = new Card("", Card.Actions.Empty, 0);
            GenerateStats();
            GenerateDeck();
            GenerateCurrentHand();

        }
        public void AddCardToDeck(Card card)
        {
            Card[] newCards = new Card[_cards.Length + 1];

            for (int i = 0; i < _cards.Length; i++)
            {
                newCards[i] = _cards[i];
            }

            newCards[newCards.Length - 1] = card;
            _cards = newCards;
        }
        public void AddShield(int index) 
        {
            _shield += index;
            OnPropertyChanged(nameof(Health));
        }
        public void AddHealth(int index)
        {
            _health += index;
            if(_health > _maxHealth)
            {
                _health = _maxHealth;
            }
            OnPropertyChanged(nameof(Health));
        }
        public void IncreaseMaxHealth(int index)
        {
            _maxHealth += index;
            OnPropertyChanged(nameof(Health));
        }
    }
}
