namespace WhackAMole;
public partial class Scoreboard : ContentPage
{
    public Scoreboard(List<Score> scores)
    {
        InitializeComponent();
        foreach (Score score in scores)
        {
            Label lbl = new Label();
            lbl.Text = score.name + " " + score.amount;
            Listing.Add(lbl);
        }

    }
}