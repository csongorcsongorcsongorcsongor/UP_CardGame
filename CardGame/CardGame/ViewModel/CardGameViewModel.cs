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
        public RelayCommand<int> UseCardCommand { get; set; }
        public RelayCommand NextRoundCommand { get; set; }

        public CardGameViewModel(CardGameModel model) 
        {
            _model = model;
            Enabled1 = true;
            Enabled2 = true;
            Enabled3 = true;
            _model.CardUseEvent += model_CardUse;
            _model.NextRoundEvent += model_NextRoundEvent;
            UseCardCommand = new RelayCommand<int>(_model.PlayerCardUse);
            NextRoundCommand = new RelayCommand(_model.NextRound);

        }
        private bool _enabled1;
        private bool _enabled2;
        private bool _enabled3;

        public bool Enabled1
        {
            get { return _enabled1; }
            set
            {
                _enabled1 = value;
                OnPropertyChanged(nameof(Enabled1));
            }
        }

        public bool Enabled2
        {
            get { return _enabled1; }
            set
            {
                _enabled1 = value;
                OnPropertyChanged(nameof(Enabled2));
            }
        }
        public bool Enabled3
        {
            get { return _enabled1; }
            set
            {
                _enabled1 = value;
                OnPropertyChanged(nameof(Enabled3));
            }
        }

        public Player Player { get { return _model.player; } }
        public Minion Enemy { get { return _model.minion; } }

        public Card Card1 { get { return _model.player.Card1; } }
        public Card Card2 { get { return _model.player.Card2; } }
        public Card Card3 { get { return _model.player.Card3; } }

        

        private void model_NextRoundEvent() { 
            Enabled1 = true;
            Enabled2 = true;
            Enabled3 = true;
        }
        private void model_CardUse() {
            Enabled1 = false;
            Enabled2 = false;
            Enabled3 = false;
            OnPropertyChanged(nameof(Enabled1));
            OnPropertyChanged(nameof(Enabled2));
            OnPropertyChanged(nameof(Enabled3));
        }
    }
}
