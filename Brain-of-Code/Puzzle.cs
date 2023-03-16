public class Puzzle
{
    public int Dimension { get; set; } = 0;
    public int x { get; set; } = 0;
    public int y { get; set; } = 0;
    public List<List<int>> input { get; set; } = new List<List<int>>();
    public Puzzle(int dimension)
    {
        this.Dimension = dimension;
    }

    internal string Display(int rownumber)
    {
        var display = "";
        var row = input[rownumber];
        for (int x = 0; x < Dimension; x++)
        {
            if (row[x] != Dimension * Dimension)
            {
                display += row[x] + "|";
            }
            else
            {
                display += " |";
            }
        }
        return display;
    }

    public bool Checkgane()
    {
        int prev = 0;
        for (int x = 0; x < Dimension; x++)
        {
            var current = input[x];
            for (int y = 0; y < Dimension; y++)
            {
                if (current[y] != prev + 1)
                {
                    return false;
                }
                prev = current[y];
            }
        }
        return true;
    }
    internal Location Checklast()
    {
        for (int x = 0; x < Dimension; x++)
        {
            var current = input[x];
            for (int y = 0; y < Dimension; y++)
            {
                if (current[y] == Dimension * Dimension)
                {
                    return new Location() { Row = x, Column = y };
                }
            }
        }
        return new Location() { Row = -1, Column = -1 };
    }

    public void Play(Direction direction)
    {
        var location = Checklast();
        x = location.Row;
        y = location.Column;
        int replace = 0;
        switch (direction)
        {
            case Direction.Upwards:
                if (x < Dimension - 1)
                {
                    replace = input[x][y];
                    input[x][y] = input[x + 1][y];
                    input[x + 1][y] = replace;
                }
                break;
            case Direction.Downwards:
                if (x != 0)
                {
                    replace = input[x][y];
                    input[x][y] = input[x - 1][y];
                    input[x - 1][y] = replace;
                }
                break;
            case Direction.Left:
                if (y < Dimension - 1)
                {
                    replace = input[x][y];
                    input[x][y] = input[x][y + 1];
                    input[x][y + 1] = replace;
                }
                break;
            case Direction.Right:
                if (y != 0)
                {
                    replace = input[x][y];
                    input[x][y] = input[x][y - 1];
                    input[x][y - 1] = replace;
                }
                break;
            default:
                throw new ArgumentException("Invalid direction specified");
        }
    }

}