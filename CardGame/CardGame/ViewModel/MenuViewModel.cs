using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace CardGame.ViewModel
{
    public class MenuViewModel:ViewModelBase
    {
        private bool _easy { get; set; }
        private bool _medium { get; set; }
        private bool _hard { get; set; }

        public bool Easy { get { return _easy; } set; }
        public bool Medium { get { return _medium; } set; }
        public bool Hard { get { return _hard; } set; }
        public double Difficulty { get ; private set; }

        public RelayCommand<string> SetDifficulty { get; }
        public RelayCommand PlayCommand { get; }

        public event EventHandler<EventArgs> StartGameEvent;

        public MenuViewModel() {
            ChangeDifficulty("Medium");
            SetDifficulty = new RelayCommand<string>(ChangeDifficulty);
            PlayCommand = new RelayCommand(() =>
                StartGameEvent?.Invoke(this, EventArgs.Empty)
            );
        }

        private void ChangeDifficulty(string difficulty) {
            if (difficulty == "Easy") {
                Easy = false;
                Medium = true;
                Hard = true;
                Difficulty = 0.7;
            }
            else if (difficulty == "Medium")
            {
                Easy = true;
                Medium = false;
                Hard = true;
                Difficulty = 1.0;
            }
            else if (difficulty == "Hard")
            {
                Easy = true;
                Medium = true;
                Hard = false;
                Difficulty = 1.3;
            }

        }
    }
}
