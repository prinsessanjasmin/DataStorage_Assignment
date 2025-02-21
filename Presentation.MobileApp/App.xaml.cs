namespace Presentation.MobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());

            int newWidth = 500;
            int newHeight = 800;

            window.Width = newWidth;
            window.Height = newHeight;

            

            return window;

        }
    }
}