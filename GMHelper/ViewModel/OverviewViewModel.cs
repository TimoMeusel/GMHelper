﻿using System.Collections.Generic;
using System.Windows.Input;

namespace GM.ViewModel
{
    public abstract class OverviewViewModel<T>: ViewModelBase where T:PlayerViewModel
    {
        private IEnumerable<T> _players;
        private ICommand _setFirstPlayerCommand;
        private ICommand _setSecondPlayerCommand;
        private T _selectedPlayer;
        
        public IEnumerable<T> Players
        {
            get { return _players; }
            set
            {
                _players = value; 
                OnPropertyChanged();
            }
        }

        public ICommand SetFirstPlayerCommand
        {
            get { return _setFirstPlayerCommand; }
            protected set
            {
                _setFirstPlayerCommand = value; 
                OnPropertyChanged();
            }
        }

        public ICommand SetSecondPlayerCommand
        {
            get { return _setSecondPlayerCommand; }
            protected set
            {
                _setSecondPlayerCommand = value; 
                OnPropertyChanged();
            }
        }

        public ComparisonViewModel<T> ComparisonViewModel { get; protected set; }

        public T SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                _selectedPlayer = value; 
                OnPropertyChanged();
                OnPropertyChanged("Url");
            }
        }

        public string Url
        {
            get
            {
                if (SelectedPlayer?.EliteProspectsId != null)
                {
                    return $"http://www.eliteprospects.com/iframe_player_stats.php?player={SelectedPlayer.EliteProspectsId}";
                }

                return $"http://www.eliteprospects.com/iframe_player_stats.php";
            }
        }
    }
}
