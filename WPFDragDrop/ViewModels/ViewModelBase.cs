using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModelEditor.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ViewModelBase()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
