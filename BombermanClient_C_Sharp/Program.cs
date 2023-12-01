// See https://aka.ms/new-console-template for more information

using BombermanClient_C_Sharp;

Console.WriteLine("Press Enter to close bot client");

var bot = new Bot();
using var client = new Client(bot);
Console.ReadLine();