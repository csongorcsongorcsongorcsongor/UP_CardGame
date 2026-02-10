using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.Model;
using static CardGame.Model.Card;

namespace CardGame.ViewModel
{
    public abstract class Entity:ViewModelBase
    {
        Random r = new Random();
        public string Name { get; set; }
        public string Health { get { return $"{_health}/{_maxHealth}+{_shield}"; } }
        public bool Dead { get { return _dead; }  }
        public Card NextCard
        {
            get { return _nextCard; }
        }
        public string NextMove { get {
                if (_nextCard.Action == Actions.Empty)
                {
                    return "";
                }
                else {
                    return $"{_nextCard.Action}:{_nextCard.Value}";                    
                }
                    ; } }

        public int MaxHealth { get {return _maxHealth ; } }

        protected Random random;
        protected string _name;
        protected int _health;
        protected int _shield;
        protected Card _nextCard;
        protected Card[] _cards;
        protected int _maxHealth;
        protected bool _dead;

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

    }
}
