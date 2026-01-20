using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.ViewModel
{
    public abstract class Entity:ViewModelBase
    {
        public string Name { get; set; }
        public string Healt { get{ return Convert.ToString(_healt) + Convert.ToString(_shield); }}
        public string NextCard { get; set; }
        public Actions NextMove { get { return _nextCard.Action ; } }

        protected Random random;
        protected string _name;
        protected int _healt;
        protected int _shield;
        protected Card[] _nextCard;
        protected Card[] _cards;
    }
}
