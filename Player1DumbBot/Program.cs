// See https://aka.ms/new-console-template for more information

using Core.Network;
using Core.Network.ExternalShared.Enums;
using Core.Network.ExternalShared.Interfaces;
using Player1DumbBot;

Console.WriteLine("Press Enter to close bot client");

using var client = new Client();
Console.ReadLine();