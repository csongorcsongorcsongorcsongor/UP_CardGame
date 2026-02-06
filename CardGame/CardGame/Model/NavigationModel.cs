using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Model
{
    public class NavigationModel
    {
        private Player _player;
        private NavigationChoice _choice1;
        private NavigationChoice _choice2;

        public Player Player { get { return _player; } }
        public NavigationChoice Choice1 { get { return _choice1; } }
        public NavigationChoice Choice2 { get { return _choice2; } }
        public NavigationModel(Player player)
        {
            _player = player;
        }
        public void PickChoice(int index)
        {
            if (index == 1)
            {
                _choice1.ApplyChoice();
            }
            else if (index == 2)
            {
                _choice2.ApplyChoice();
            }
        }
        public void GenerateNewNavigation()
        {
            //nem tudom igy jo e de nem irja a feladat a _ a choice elott de csak igy nem error
            _choice1 = new NavigationChoice(_player);
            _choice2 = new NavigationChoice(_player);
        }

    }
}
