using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using System.ComponentModel;

namespace Sens{

    /// <summary>
    /// Base view model that fires Property Changed events as needed
    /// </summary>

    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged{
 
        // The event that is fired when any child property changes its value
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        // Call this to fire a <see cref="PropertyChanged"/> event
        // <param name="name"></param>
        public void OnPropertyChanged(string name){
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        //fires PropertyChanged event and fires the name parameter
        public void onPropertyChanged(String name){
            PropertyChanged(this, new PropertyChangedEventArgs(name));
         }



    }
}