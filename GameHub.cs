using Microsoft.AspNetCore.SignalR;

public class GameHub : Hub<GameControls>
{
    public GameHub(GameSession game)
    {
        this.game = game;
    }
    GameSession game;
    public Task UserJoin(string user)
    {
        if (game != null)
        {
            if (CheckPlayer(user) != true || SelectHost(user) == true)
            {
                Random rand = new Random();
                var color = rand.Next(1, 15);
                game.OnlinePlayers.Add(new Player() { Username = user, Color = (ConsoleColor)color , Host = user });
            }

        }
        return Clients.All.SendGame(game);
    }
    public bool CheckPlayer(string user)
    {
        for (int x = 0; x < game.OnlinePlayers.Count; x++)
        {
            if (user == game.OnlinePlayers[x].Username)
            {
                return true;
            }
        }
        return false;
    }
    public bool SelectHost(string user)
    {
        for (int x = 0; x < game.OnlinePlayers.Count; x++)
        {
            if (user == game.OnlinePlayers[1].Username)
            {
                return true;
            }
        }
        return false;
    }
    public Task PickDimension(int size)
    {
        game.Dimension = size;
        foreach (var player in game.OnlinePlayers)
        {
            player.Game = new Puzzle(size);
        }
        return Clients.All.GetReady(game);
    }
    public Task SendDisplay(Player gamer)
    {
        for (int x = 0; x < game.OnlinePlayers.Count; x++)
        {
            if (game.OnlinePlayers[x].Username == gamer.Username)
            {
                game.OnlinePlayers[x] = gamer;
                if (gamer.Game.Checkgane())
                {
                    gamer.GameWinner = true;
                    var oldgame = new GameSession();
                    oldgame.Dimension = game.Dimension;
                    oldgame.OnlinePlayers.AddRange(game.OnlinePlayers);
                    game.Dimension = 0;
                    game.OnlinePlayers.Clear();
                    return Clients.All.GameEnd(oldgame);
                }
            }
        }
        return Clients.All.Play(game);
    }
}