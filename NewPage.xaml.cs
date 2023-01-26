namespace MauiBug;

public partial class NewPage : ContentPage
{
    private static System.Timers.Timer Timer;
    public NewPage()
	{
		InitializeComponent();

        Timer = new System.Timers.Timer();
        Timer.Interval = 1000;

        Timer.Elapsed += OnTimedEvent;
        Timer.AutoReset = false;
        Timer.Enabled = false;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        Duration.Text = $"Loading took {((int)DurationHelper.Duration)} ms.";

        Timer.Enabled = true;
    }

    private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
    {
        Timer.Enabled = false;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync("..");
        });
    }
}