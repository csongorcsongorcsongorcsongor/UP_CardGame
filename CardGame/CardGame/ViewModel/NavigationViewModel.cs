using CardGame.Model;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        private bool _hasNotChosen;
        private NavigationModel _model;
        public string Name1
        {
            get { return _model.Choice1.Name; }
        }
        public string Name2
        { //a feladat Choice1-et ir de sztem Choice2 a helyes
            get { return _model.Choice2.Name; }
        }
        public string Description1
        {
            get { return _model.Choice1.Description; }
        }
        public string Description2
        { //a feladat Choice1-et ir de sztem Choice2 a helyes
            get { return _model.Choice2.Description; }
        }
        public Player Player { get { return _model.Player; } }
        public bool HasNotChosen
        {
            get { return _hasNotChosen; }
            set
            {
                _hasNotChosen = value;
                OnPropertyChanged(nameof(HasNotChosen));
            }
        }
        public RelayCommand<object> ChooseCommand { get; }
        public RelayCommand ExitNavigationCommand { get; }
        public event EventHandler ExitNavigationEvent;
        public void OnExitNavigation()
        {
            ExitNavigationEvent?.Invoke(this, EventArgs.Empty);
        }
        public NavigationViewModel(NavigationModel model)
        {
            _model = model;
            HasNotChosen = true;
            //ez nem tudom hogy jo e igy
            ChooseCommand = 
            new RelayCommand<object>((param) =>
            {
                if (param != null)
                {
                    int index = int.Parse(param.ToString());
                    _model.PickChoice(index);
                    HasNotChosen = false;
                }
            });

            ExitNavigationCommand = new RelayCommand(OnExitNavigation);
        }

    }
}
