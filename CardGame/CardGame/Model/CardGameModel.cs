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

        
        public CardGameModel(Player player, Minion minion) 
        {
            _player = player;
            _minion = minion;
        }
        protected void OnCardUse()
        {
            CardUseEvent?.Invoke(this, EventArgs.Empty);
        }
        protected void OnNextRound()
        {
            NextRoundEvent?.Invoke(this, EventArgs.Empty);
        }
        public void PlayerCardUse(object index)
        {
            int cardIndex;
            if(index is string indexString)
            {
                cardIndex = int.Parse(indexString);
            }
            else
            {
                cardIndex = (int)index;
            }
            Card card = _player.GetCard(cardIndex);
            if(card.Action == Card.Actions.Attack)
            {
                _minion.Damage(card.Value);
            }
            _player.UseCard(cardIndex);
            CardUseEvent?.Invoke(this, EventArgs.Empty);
        }

        public void NextRound()
        {
            Card savedCard = _minion.NextCard;

            if(savedCard== null)
            {
                NextRoundEvent?.Invoke(this, EventArgs.Empty);
                return;
            }
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
