﻿using PropertyChanged;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dasetto.Word
{
    /// <summary>
    /// A base view model that fires Property Changed events as needed
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged (string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #region Command helpers
        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            // Check if the flag property is true (meaning the function is already running)
            if (updatingFlag.GetPropertyValue())
                return;

            //Set the property flag to true to indicate we are running
            updatingFlag.SetPropertyValue(true);

            try
            {
                await action();
            }

            finally
            {
                // set the property flag to false now that it has finished
                updatingFlag.SetPropertyValue(false);
            }
        }
        #endregion
    }
}
