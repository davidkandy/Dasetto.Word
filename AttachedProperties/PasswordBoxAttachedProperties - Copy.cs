using Dasetto.Word.AttachedProperties;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Dasetto.Word
{

    /// <summary>
    /// The MonitorPassword attached property for a <see cref="PasswordBox"/>
    /// </summary>
    public class MonitorPasswordProperty : BaseAttachedProperty<HasTextProperty, bool> 
    {
        public override void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Get the caller
            PasswordBox passwordBox = d as PasswordBox;

            //Make sure it is a password box
            if (passwordBox == null)
            return;

            //Remove any previous events
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            //if the caller sets MonitorPassword to true...
            if ((bool)e.NewValue)
            {

                HasTextProperty.SetValue(passwordBox, passwordBox.SecurePassword.Length > 0);

                //start listening out for password changes
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }

        }

        
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;

            HasTextProperty.SetValue((PasswordBox)sender, passwordBox.SecurePassword.Length > 0);
        }
    };


    /// <summary>
    /// The HasText attached property for a <see cref="PasswordBox"/>
    /// </summary>
    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool> { };
}
