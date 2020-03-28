using System;using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Dasetto.Word
{


    public class BasePage<VM> : Page
        where VM : BaseViewModel, new()
    {

        #region Private members
        // The view model associated with this page 
        private VM mViewModel;

        #endregion

        #region Public properties
        //The animation to play when the page is first loaded 
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;
        //The animatioin to play when the page is unloaded
        public PageAnimation PageLoadUnAnimation { get; set; } = PageAnimation.SlideAndFadeInToLeft;
        //Time taken for the animation to slide in 
        public float SlideSeconds { get; set; } = 0.8f;
        //The view model associated with this page
        public VM ViewModel
        {
            get { return mViewModel; }
            set
            {
                //if nothing has changed, return
                if (mViewModel == value)
                    return;
                // Update the value
                mViewModel = value;
                // Set the data context for this page
                this.DataContext = mViewModel;
            }
        } 

        #endregion

        #region constructors

        public BasePage()
        {
            //If we are animating, hide to begin with
            // if (this.PageLoadAnimation != PageAnimation.None)
                // this.Visibility = Visibility.Collapsed;

            //Listen out for the page loading
            this.Loaded += BasePage_Loaded;

            // create a default viewmodel
            this.ViewModel = new VM();
        }


        #endregion

        #region Animation Load/Unload

        private async void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimateInAsync();
        }

        public async Task AnimateInAsync()
        {
            //make sure there is something to do
            if (this.PageLoadAnimation == PageAnimation.None)
                return;
            switch (this.PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:
                    var sb = new Storyboard();
                    var SlideAnimation = new ThicknessAnimation
                    {
                        Duration = new Duration(TimeSpan.FromSeconds(this.SlideSeconds)),
                        From = new Thickness(this.WindowWidth, 0, -this.WindowWidth, 0),
                        To = new Thickness(0),
                        DecelerationRatio = 0.9f,
                    };

                    Storyboard.SetTargetProperty(SlideAnimation, new PropertyPath("Margin"));
                    sb.Children.Add(SlideAnimation);

                    sb.Begin(this);

                    await Task.Delay((int)(this.SlideSeconds * 1000));

                    break;
            }
        }



        #endregion
    }
}
