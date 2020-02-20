using Prism;
using Prism.Ioc;
using XamaRead.Interfaces;
using XamaRead.Services;
using XamaRead.ViewModels;
using XamaRead.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamaRead
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("StartPage/NavigationPage/WelcomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services
            containerRegistry.Register<IBookService, BookService>();
            containerRegistry.Register<IRestService, RestService>();
            containerRegistry.Register<IDataService, DataService>();
            containerRegistry.Register<IShareBooks, ShareBooks>();

            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>();
            containerRegistry.RegisterForNavigation<StartPage, StartPageViewModel>();
            containerRegistry.RegisterForNavigation<SearchPage, SearchPageViewModel>();
            containerRegistry.RegisterForNavigation<BookDetailsPage, BookDetailsPageViewModel>();
        }
    }
}
