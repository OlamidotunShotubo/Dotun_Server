public interface GameControls
{
    Task Play(GameSession game);
    Task SendDisplay(Player game, Console color);
    Task UserJoin(string user);
    Task GetReady(GameSession game);
    Task CountDown();
    Task PickDimension(string user);
    Task SendGame(GameSession game);
    Task GameEnd(GameSession game);
}