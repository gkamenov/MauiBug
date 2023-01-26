namespace MauiBug;

public static class DurationHelper
{
    public static DateTime startTime = DateTime.Now;

    public static double Duration => (DateTime.Now - startTime).TotalMilliseconds;
}


public partial class MainPage : ContentPage
{
	int count = 0;


    System.Timers.Timer Timer;

    bool Started;

    public MainPage()
	{
		InitializeComponent();
        
        // Create a timer and set a 500ms interval.
        Timer = new System.Timers.Timer();
        Timer.Interval = 100;

        Timer.Elapsed += OnTimedEvent;
        Timer.AutoReset = false;
        Timer.Enabled = false;

        Started = false;
    }

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        Timer.Enabled = Started;
    }

    private void OnStartBtnClicked(object sender, EventArgs e)
    {
        Started = true;
        Timer.Enabled = true;
        StartBtn.IsEnabled = false;
    }
    private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
    {
        Timer.Enabled = false;

        DurationHelper.startTime = DateTime.Now;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync(nameof(NewPage), true);
        });
    }
}

