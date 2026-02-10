using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGame.Model;
using CommunityToolkit.Mvvm.Input;


namespace CardGame.ViewModel
{
    public class CardGameViewModel:ViewModelBase
    {
        private CardGameModel _model;
        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                OnPropertyChanged(nameof(Enabled));
            }
        }

        public Player Player { get { return _model.player; } }
        public Entity Enemy { get { return _model.minion; } }

        public Card Card1 { get { return _model.player.Card1; } }
        public Card Card2 { get { return _model.player.Card2; } }
        public Card Card3 { get { return _model.player.Card3; } }
        public RelayCommand<object> UseCardCommand { get; }
        public RelayCommand NextRoundCommand { get; }

        public string UntilBoss { get {return (5 - _model.Rounds).ToString() ; } set; }

        public CardGameViewModel(CardGameModel model) 
        {
            _model = model;
            Enabled = true;
            _model.CardUseEvent += model_CardUse;
            _model.NextRoundEvent += model_NextRoundEvent;
            UseCardCommand = new RelayCommand<object>(_model.PlayerCardUse);
            NextRoundCommand = new RelayCommand(_model.NextRound);

        }
      

        private void model_NextRoundEvent(object? s, EventArgs e) {
            Enabled = true;
        }
        private void model_CardUse(object? s ,EventArgs e) {
            Enabled = true;
            OnPropertyChanged(nameof(Card1));
            OnPropertyChanged(nameof(Card2));
            OnPropertyChanged(nameof(Card3));
        }
    }
}
