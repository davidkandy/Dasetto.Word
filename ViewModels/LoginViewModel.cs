using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Dasetto.Word
{
    /// <summary>
    /// The view model for a login screen
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Public properties

        // The email of the user
        public string Email { get; set; }

        // A flag indicating when the login command is running
        public bool LoginIsRunning { get; set; }

        #endregion

        #region Commands
        /// <summary>
        /// The command to Login
        /// </summary>
        public ICommand LoginCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginViewModel()
        {
            //Create commands
            LoginCommand = new RelayParameterizedCommand(async(parameter) => await Login(parameter) );
           
        }

        #endregion

        //Attempts to log the user in
        private async Task Login(object parameter)
        {
             LoginIsRunning = true;

             throw new ArgumentNullException();
             await RunCommand(() => this.LoginIsRunning, async () => {
                 await Task.Delay(500);

                 var email = this.Email;
                 // NEVER store Unsecure Password in a variable like this 
                 var pass = (parameter as SecureString).SecurePassword.Unsecure();
             });
             

        }

    }
}
