using Xamarin.Forms;

namespace XamaRead.Views
{
    public partial class StartPage : MasterDetailPage
    {
        public StartPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }
    }
}