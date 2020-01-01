using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KanbanBoard
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new[] {
                "SwipeView_Experimental",
                "IndicatorView_Experimental"
            });

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
