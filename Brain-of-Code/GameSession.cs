public class GameSession
{  
    public string PlayerID {get; set;}
    public int Dimension {get; set;}
    public List<Player> OnlinePlayers { get; set; }=new List<Player>();
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}