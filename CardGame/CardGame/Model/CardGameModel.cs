using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.ViewModel;

namespace CardGame.Model
{
    public class CardGameModel
    {
        private Player _player;
        private Entity _enemy;
        private double _difficulty{ get; set; }
        private int _score{ get; set; }

        public int Rounds { get;private  set; }

        public Player player { get { return _player; } }
        public Entity Enemy { get { return _enemy; } }


        public event EventHandler CardUseEvent;
        public event EventHandler NextRoundEvent;
        public event EventHandler<GameEndEventArgs> GameEndEvent;

        
        public CardGameModel(Player player, double difficulty) 
        {
            _player = player;
            _difficulty = difficulty;
            Rounds = 1;
            _enemy = new Minion(_difficulty);
            
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
                _enemy.Damage(card.Value);
            }
            _player.UseCard(cardIndex);
            CardUseEvent?.Invoke(this, EventArgs.Empty);
            if (_player.Dead || _enemy.Dead)
            {
                GameEndEvent?.Invoke(this, new GameEndEventArgs(_player.Dead, _enemy.Dead, false));
                
                if (Rounds == 5)
                {
                    _enemy = new FinalBoss(_difficulty);
                }
                else if (Rounds != 5) {
                    _enemy = new Minion(_difficulty);
                    Rounds++;
                }
                
            }
            if (_enemy.Dead) {
                _score += _enemy.maxHP * 100 * _difficulty;
            }
        }

        public void NextRound()
        {
            Card savedCard = _enemy.NextCard;

            if(savedCard== null)
            {
                NextRoundEvent?.Invoke(this, EventArgs.Empty);
                return;
            }
            if (savedCard.Action == Card.Actions.Attack)
            {
                _player.Damage(savedCard.Value);
            }

            _enemy.UseCard();
            _player.GenerateCurrentHand();

            CardUseEvent?.Invoke(this, EventArgs.Empty);
            NextRoundEvent?.Invoke(this, EventArgs.Empty);
        }


    }
}
