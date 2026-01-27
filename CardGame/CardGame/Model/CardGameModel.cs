using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Model
{
    public class CardGameModel
    {
        private Player _player;
        private Minion _minion;

        public Player player { get { return _player; } }
        public Minion minion { get { return _minion; } }


        public event EventHandler CardUseEvent;
        public event EventHandler NextRoundEvent;

        public CardGameModel() 
        {
            _player = new Player();
            _minion = new Minion();
        }

        public void PlayerCardUse(object? index)
        {
            if (index is string s && int.TryParse(s, out int i))
            {
                Card card = _player.GetCard(i);

                if (card.Action == Card.Actions.Attack)
                {
                    _minion.Damage(card.Value);
                }

                _player.UseCard(i);
                CardUseEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public void NextRound()
        {
            Card savedCard = _minion.NextCard;

            if (savedCard.Action == Card.Actions.Attack)
            {
                _player.Damage(savedCard.Value);
            }

            _minion.UseCard();
            _player.GenerateCurrentHand();

            CardUseEvent?.Invoke(this, EventArgs.Empty);
            NextRoundEvent?.Invoke(this, EventArgs.Empty);
        }


    }
}
