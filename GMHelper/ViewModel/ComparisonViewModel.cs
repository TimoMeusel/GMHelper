using System.Collections.Generic;
using GM.Model;

namespace GM.ViewModel
{
    public class ComparisonViewModel<T>: ViewModelBase where T:PlayerViewModel
    {
        private T _firstPlayer;
        private T _secondPlayer;

        public T FirstPlayer
        {
            get { return _firstPlayer; }
            set
            {
                _firstPlayer = value; 
                OnPropertyChanged();
            }
        }

        public T SecondPlayer
        {
            get { return _secondPlayer; }
            set
            {
                _secondPlayer = value; 
                OnPropertyChanged();
            }
        }

        public List<string> Header { get; set; }
    }
}
