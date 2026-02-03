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
        }
        private void GenerateDeck() {
            _cards[0] = new Card("Basic Attack", Card.Actions.Attack, 10);
            _cards[1] = new Card("Basic Attack", Card.Actions.Attack, 10);
            _cards[2] = new Card("Basic Attack", Card.Actions.Attack, 10);
            _cards[2] = new Card("Basic Heal", Card.Actions.Heal, 2);
            _cards[2] = new Card("Basic Heal", Card.Actions.Heal, 2);
            _cards[2] = new Card("Basic Shield", Card.Actions.Shield, 4);
            _cards[2] = new Card("Basic Shield", Card.Actions.Shield, 4);
            _cards[2] = new Card("Advanced Attack", Card.Actions.Attack, 20);
            _cards[2] = new Card("Advanced Heal", Card.Actions.Heal, 10);
            _cards[2] = new Card("Advanced Shield", Card.Actions.Shield, 18);


        }
        public void GenerateCurrentHand() {
            random = new Random();
            _currentHand[0] = _cards[random.Next(0, 10)];
            _currentHand[1] = _cards[random.Next(0, 10)];
            _currentHand[2] = _cards[random.Next(0, 10)];
        }

        public void UseCard(int index) {
            if (_currentHand[index].Action ==Card.Actions.Heal  )
            {
                _health += _nextCard.Value;
                OnPropertyChanged(nameof(_health));
                _currentHand[index] = new Card("", Card.Actions.Empty, 0);
            }
            else if (_currentHand[index].Action == Card.Actions.Shield) {
                _shield += _nextCard.Value;
                OnPropertyChanged(nameof(_shield));
                _currentHand[index] = new Card("", Card.Actions.Empty, 0);
            }

        }
        public void Damage(int damage) {
            
            int leftshield;
            if (_shield > 0) {
               leftshield = _shield -= damage;
                if (leftshield <= 0)
                {
                    _health += leftshield;
                    OnPropertyChanged(nameof(_shield));
                    OnPropertyChanged(nameof(_health));
                }
                
                OnPropertyChanged(nameof(_shield));
                OnPropertyChanged(nameof(_health));
            }
            
            OnPropertyChanged(nameof(_shield));
            OnPropertyChanged(nameof(_health));

        }
        public Card GetCard(int index) {
            return _currentHand[index];
        }

        public Player() {
            _cards = new Card[10];
            _nextCard = new Card("", Card.Actions.Empty, 0);
            GenerateStats();
            GenerateDeck();
            GenerateCurrentHand();
        }

    }
}
