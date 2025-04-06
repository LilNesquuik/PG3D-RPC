using DiscordRPC;
using PG3D_RPC.Enums;

namespace PG3D_RPC.Worker;

public class DiscordRpcStartup : IDisposable
{
    private readonly DiscordRpcClient _discordRpcClient;
    private readonly Timestamps _timestamps;
    private readonly GameMode _gameMode;
    private readonly Random _random;
    
    public DiscordRpcStartup(GameMode gameMode)
    {
        _discordRpcClient = new DiscordRpcClient("1358173889712361702");
        _timestamps = Timestamps.Now;
        _random = new Random();
        _gameMode = gameMode;
        _discordRpcClient.Initialize();
        
        Task.Run(async () => await StartAsync());
    }
    
    public void Dispose()
    {
        _discordRpcClient.Dispose();
    }

    public async Task StartAsync()
    {

        switch (_gameMode)
        {
            case GameMode.CaptureTheFlag:
                await CaptureTheFlag();
                break;
            case GameMode.DeathMatch:
                await DeathMatch();
                break;
            case GameMode.Survival:
                await Survival();
                break;
        }
    }
    
    private async Task CaptureTheFlag()
    {
        while (true)
        {
            int flagsCaptured = _random.Next(0, 5);
            int flagsCapturedEnemy = _random.Next(0, 5);
            
            _discordRpcClient.SetPresence(new RichPresence()
            {
                Details = "Capture the flags - In Game",
                State = $"Blue Team ({flagsCaptured} - {flagsCapturedEnemy}) ",
                Timestamps = _timestamps,
                Assets = new Assets()
                {
                    LargeImageKey = "pg3d",
                    LargeImageText = "It was just an old memory",
                },
                Party = new Party()
                {
                    ID = "PG3D-RPC",
                    Size = _random.Next(9, 10),
                    Max = 10
                },
                Buttons = new []
                {
                    new Button
                    {
                        Label = "Ask to join",
                        Url = "https://guns.lol/gr86"
                    }
                }
            });

            await Task.Delay(TimeSpan.FromSeconds(30));
        }
    }

    private async Task DeathMatch()
    {
        while (true)
        {
            int kills = _random.Next(5, 30);
            int deaths = _random.Next(0, 15);

            _discordRpcClient.SetPresence(new RichPresence()
            {
                Details = "Deathmatch - In Game",
                State = $"Kills: {kills} | Deaths: {deaths}",
                Timestamps = _timestamps,
                Assets = new Assets()
                {
                    LargeImageKey = "pg3d",
                    LargeImageText = "It was just an old memory",
                },
                Party = new Party()
                {
                    ID = "PG3D-RPC",
                    Size = _random.Next(1, 10),
                    Max = 10
                },
                Buttons = new []
                {
                    new Button
                    {
                        Label = "Ask to join",
                        Url = "https://guns.lol/gr86"
                    }
                }
            });

            await Task.Delay(TimeSpan.FromSeconds(30));
        }
    }
    
    private async Task Survival()
    {
        int wave = 1;
        
        while (true)
        {
            int teammates = _random.Next(1, 4);
            int zombies = _random.Next(1, 20) * wave;
            
            _discordRpcClient.SetPresence(new RichPresence()
            {
                Details = "Co-op Survival - Zombie Mode",
                State = $"Wave: {wave} | Zombies: {zombies} alive ",
                Timestamps = _timestamps,
                Assets = new Assets()
                {
                    LargeImageKey = "pg3d",
                    LargeImageText = "It was just an old memory",
                },
                Party = new Party()
                {
                    ID = "PG3D-RPC",
                    Size = teammates,
                    Max = 4
                },
                Buttons = new []
                {
                    new Button
                    {
                        Label = "Ask to join",
                        Url = "https://guns.lol/gr86"
                    }
                }
            });
            
            wave++;
            
            await Task.Delay(TimeSpan.FromSeconds(30));
        }
    }
}