using System.Configuration;
using System.Data;
using System.Windows;
using CardGame.Model;
using CardGame.View;
using CardGame.ViewModel;

namespace CardGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _window;
        private CardGameViewModel _viewModel;
        private CardGameModel _model;
        private CombatPage _combatPage;

        private NavigationModel _navigationModel;
        private NavigationViewModel _navigationViewModel;
        private NavigationPage _navigationPage;
        private Player _player;
        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _window = new MainWindow();

            _player = new Player();
            Minion minion = new Minion();

            _model = new CardGameModel(_player, minion);
            _viewModel = new CardGameViewModel(_model);
            _combatPage = new CombatPage();

            _navigationModel = new NavigationModel(_player);
            _navigationViewModel = new NavigationViewModel(_navigationModel);
            _navigationPage = new NavigationPage();

            _model.GameEndEvent += ChangeToNavigation;
            _navigationViewModel.ExitNavigationEvent += ChangeToCombat;

            _window.DataContext = _viewModel;
            _window.Content = _combatPage;
            _window.Show();
        }
        public void ChangeToCombat(object? sender, EventArgs e)
        {
            _viewModel.Enabled = true;
            _player.GenerateCurrentHand();
            _window.DataContext = _viewModel;
            _window.Content = _combatPage;
        }
        public void ChangeToNavigation(object? sender, GameEndEventArgs e)
        {
            if (e.EnemyDead)
            {
                _navigationViewModel.HasNotChosen = true;
                _navigationModel.GenerateNewNavigation();

                _window.DataContext = _navigationViewModel;
                _window.Content = _navigationPage;
            }
            else if (e.PlayerDead)
            {
                MessageBox.Show("Vege a jateknak meghaltal :(");
                _window.Close();
            }
        }
    }

}
