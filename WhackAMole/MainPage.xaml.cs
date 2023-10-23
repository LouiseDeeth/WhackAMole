namespace WhackAMole;

public partial class MainPage : ContentPage
{
    private Random random;
    private int currentscore = 0;
    private int countdown = 30;
    System.Timers.Timer timer;

	public MainPage() {
		InitializeComponent();
        random = new Random();
	}
    private void SetUpMyTimers() {
        /*Dispatcher.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
        {
            TimerFunctions();
            return true;
        });*/    // has no way to stop timer
        countdown = 10;
        timer = new System.Timers.Timer
        {
            Interval = 1000
        };
        timer.Elapsed += timer_Elapsed;
        countdown_lbl.Text = countdown.ToString();
        timer.Start();
    }
    private void timer_Elapsed(object sender, EventArgs e) {
        Dispatcher.Dispatch(() =>
        {
            TimerFunctions();
        });
    }    
    private void TimerFunctions() {
        countdown--;
        countdown_lbl.Text = countdown.ToString();
        if (countdown == 0)
            timer.Stop();
    }

    private void Start_Btn_Clicked(object sender, EventArgs e) {

    }
    private void Switch_Grid_Clicked(object sender, EventArgs e) {

    }
    private void ImageTapped (object sender, EventArgs e) {
        currentscore += 10;
        MoveTheMole();
        currentscore_lbl.Text = currentscore.ToString();
    }
    private void MoveTheMole() {
        int row, col;
        row = random.Next(0, 3);
        col = random.Next(0, 3);

        mole2.SetValue(Grid.RowProperty, row);
        mole2.SetValue(Grid.ColumnProperty, row);
        mole2.IsVisible=true;
    }
}

