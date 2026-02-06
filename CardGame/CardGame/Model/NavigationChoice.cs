using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static CardGame.Model.Card;

namespace CardGame.Model
{
    public class NavigationChoice
    {
        public Random r = new Random();
        public string Name { get; set; }
        public string Description { get; set; }

        private int _choice;
        private Actions _action;
        private int _value;
        private Card _card;
        private Player _player;

        private void GenerateChoice()
        {
            _choice = r.Next(0, 3);
            switch (_choice)
            {
                case 0:
                    if (r.Next(0, 2) == 0)
                    {
                        _action = Actions.Heal;
                    }
                    else
                    {
                        _action = Actions.Shield;
                    }
                    _value = r.Next(5, 11);
                    Name = $"Add {_action}";
                    Description = $"Adds {_value} {_action}";

                    break;
                case 1:
                    int random = r.Next(1, 4);

                    switch (random)
                    {
                        case 1:
                            _action = Actions.Attack;
                            _value = r.Next(10, 26);
                            break;
                        case 2:
                            _action = Actions.Heal;
                            _value = r.Next(5, 16);
                            break;
                        case 3:
                            _action = Actions.Attack;
                            _value = r.Next(3, 13);
                            break;
                    }
                    _card = new Card("Navigation card", _action, _value);

                    Name = "Adds Navigation Card";
                    Description = $"Adds Card:\n{_action} : {_value}";
                    break;
                case 2:
                    _value = r.Next(2, 6);
                    Name = "Increase Max health";
                    Description = $"Increase your Max health by {_value}";
                    break;
            }
        }
        public void ApplyChoice()
        {
            switch (_choice)
            {
                case 0:
                    if (_action == Actions.Heal)
                    {
                        _player.AddHealth(_value);
                    }
                    else if (_action == Actions.Shield)
                    {
                        _player.AddShield(_value);
                    }
                    break;
                case 1:
                    _player.AddCardToDeck(_card);
                    break;
                case 2:
                    _player.IncreaseMaxHealth(_value);
                    break;
            }
        }

        public NavigationChoice(Player player)
        {
            _player = player;
            GenerateChoice();
        }
    }
}
