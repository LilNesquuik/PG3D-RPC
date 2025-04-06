// See https://aka.ms/new-console-template for more information

using DiscordRPC;
using PG3D_RPC.Enums;
using PG3D_RPC.Worker;

public class Program
{
    private static DiscordRpcStartup? _rpc;
    
    public static void Main(string[] args)
    {
        Console.WriteLine("PG3D-RPC - Discord Rich Presence");
        Console.WriteLine("Made by LilNesquuik");
        Console.WriteLine("=================================");
        Console.WriteLine("Enter the game mode you want to use:");
        
        foreach (var mode in Enum.GetValues(typeof(GameMode)))
            Console.WriteLine($"- {mode} ({(byte)mode})");
        
        string? game = Console.ReadLine();
        
        if (!Enum.TryParse(game, true, out GameMode gameMode))
        {
            Console.WriteLine("Invalid game mode. Please enter a valid game mode.");
            return;
        }
        
        Console.WriteLine($"Starting Discord Rich Presence for {gameMode}...");
        
        _rpc = new DiscordRpcStartup(gameMode);
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}