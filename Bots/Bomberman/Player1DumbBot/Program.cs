// See https://aka.ms/new-console-template for more information

using Core.Network;
using Core.Network.ExternalShared.Enums;
using Core.Network.ExternalShared.Interfaces;
using Player1DumbBot;

Console.WriteLine("Press Enter to close bot client");

var bot = new Bot();
using var client = new Client(bot);
Console.ReadLine();