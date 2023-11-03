namespace WhackAMole;
public class Score
{
    public string name { get; }

    public int amount { get; }

    public int difficulty { get; }

    public Score(string a, int b)
    {
        this.name = a;
        this.amount = b;
    }

    public string FormattedString()
    {
        return name + "|" + amount;
    }

    public static int sorter(Score obj1, Score obj2)
    {
        return obj1.amount.CompareTo(obj2.amount);
    }
}