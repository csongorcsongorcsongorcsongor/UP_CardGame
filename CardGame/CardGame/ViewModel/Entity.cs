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
        public string Name { get; set; }
        public string Health { get{ return Convert.ToString(_health) + "/"+ Convert.ToString(_maxHealth) + Convert.ToString(_shield); }}
        public bool Dead { get { return _dead; }  }
        public Card NextCard
        {
            get { return _nextCard; }
        }
        public Actions NextMove { get { return _nextCard.Action ; } }

        protected Random random;
        protected string _name;
        protected int _health;
        protected int _shield;
        protected Card _nextCard;
        protected Card[] _cards;
        protected int _maxHealth;
        protected bool _dead;
    }
}
