using System;

namespace CardGame.Model
{
    public class Card
    {
        public enum Actions
        {
            Attack,
            Heal,
            Shield,
            Empty
        }

        private string _name;
        private Actions _action;
        private int _value;

        public string Name { get { return _name; }}

        public Actions Action { get { return _action; }}

        public int Value { get { return _value; }}

        public Card(string name, Actions action, int value)
        {
            _name = name;
            _action = action;
            _value = value;
        }
    }
}
