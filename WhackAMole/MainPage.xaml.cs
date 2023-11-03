namespace WhackAMole;

public partial class MainPage : ContentPage
{
    private Random random;
    private int currentscore = 0;
    private int countdown = 0;
    private bool gridfive = false;
    private int maxrow = 3;
    private int topscore = 0;
    private string player = "";
    private bool gamestop = true;
    private List<Score> thescores;
    private Image whichimg;

    System.Timers.Timer timer, moletimer;
    private Image mole2;

    public MainPage(List<Score> thescores)
    {
        InitializeComponent();
        random = new Random();
        SetUpMyTimers();
        topscore = Preferences.Default.Get("topscore", 0);
        topscorelbl.Text = topscore.ToString();
        thescores = new List<Score>();
        whichimg = mole;
        ReadScoreBoardFile();
        this.thescores = thescores;
    }

    private void ReadScoreBoardFile()
    {
        throw new NotImplementedException();
    }

    private void SetUpMyTimers() {

        countdown = 10;
        timer = new System.Timers.Timer
        {
            Interval = 1000
        };
        timer.Elapsed += timer_Elapsed;
        moletimer = new System.Timers.Timer
        {
            Interval = 800
        };
        moletimer.Elapsed += timer_Elapsed;
        countdown_lbl.Text = countdown.ToString();
    }
    private void moletimer_Elapsed(object sender, EventArgs e) {
        Dispatcher.Dispatch(() =>
        {
            MoleTimerFunctions();
        });
    }
    private void timer_Elapsed(object sender, EventArgs e) {
        Dispatcher.Dispatch(() =>
        {
            TimerFunctions();
        });
    }
    private void MoleTimerFunctions() {
        moletimer.Stop();
        MovetheMole(whichimg);
        moletimer.Start();
    }

    private void MovetheMole(Image whichimg)
    {
        throw new NotImplementedException();
    }

    private void TimerFunctions() {
        countdown--;
        countdown_lbl.Text = countdown.ToString();
        if (countdown == 0) {
            StopGame();
        }
    }

    private void StopGame() 
    {
        gamestop = true;
        moletimer.Stop();
        timer.Stop();
        whichimg.IsVisible = false;
        thescores.Add(new Score(player, currentscore));
        thescores.Sort(Score.sorter);
        thescores.Reverse();
        if (currentscore > topscore)
        {
            topscore = currentscore;
            topscorelbl.Text = topscore.ToString();
            Preferences.Default.Set("topscore", topscore);
        }
    }
    private async void Start_Btn_Clicked(object sender, EventArgs e)
    {
        if (gamestop)
        {
            string result = await DisplayPromptAsync("Start Game", "What's your name ?");
            if (result != null)
            {
                gamestop = false;
                player = result;
                currentscore = 0;
                countdown = 5;
                countdown_lbl.Text = countdown.ToString();
                currentscore_lbl.Text = currentscore.ToString();
                timer.Start();
                MovetheMole(whichimg);
                moletimer.Start();
            }
        }
        else
        {
            await DisplayAlert("Already running", "Game Already Running", "ok");
        }

    }

    private Image GetGridMole4()
    {
        return GridMole4;
    }

    private Image GetGridMole4(Image gridMole4)
    {
        return gridMole4;
    }

    private void Switch_Grid_Clicked(object sender, EventArgs e, Image gridMole4)
    {
        if (gridfive)
        {
            GridMole4.IsVisible = true;
            GridMole4.IsEnabled = true;
            GridMole5.IsVisible = false;
            GridMole5.IsEnabled = false;
            gridfive = false;
            maxrow = 4;
            whichimg = gridMole4;
        }
        else
        {
            GridMole5.IsVisible = true;
            GridMole5.IsEnabled = true;
            GridMole4.IsVisible = false;
            GridMole4.IsEnabled = false;
            gridfive = true;
            whichimg = GridMole5;
            maxrow = 5;
        }
    }
    private async void Scoreboard_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Scoreboard(thescores));
    }
    private void ImageTapped (object sender, EventArgs e) 
    {
        currentscore += 10;
        moletimer.Stop();
        MoveTheMole((Image) sender);
        moletimer.Start();
        currentscore_lbl.Text = currentscore.ToString();
    }
    private async void MoveTheMole(Image img) 
    {
        if(!gamestop) 
        { 
            int row, col;
            row = random.Next(0, maxrow);
            col = random.Next(0, maxrow);
            img.Scale = 0;
            img.SetValue(Grid.RowProperty, row);
            img.SetValue(Grid.ColumnProperty, col);
            await img.ScaleTo(1, 400);
            img.IsVisible=true;
        }
        else
        {
            moletimer.Stop();
        }
    }
}

