﻿using GTANetworkAPI;
using Datenbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Haupt
{
    public class Verbindungen : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            Funktionen.AllesStarten();
            NAPI.Server.SetAutoSpawnOnConnect(false);
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Client player)
        {
            Funktionen.LogEintrag(player, "Verbunden");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + player.SocialClubName + " hat sich mit dem Server verbunden. [" + DateTime.Now + "]", ConsoleColor.Red);
            player.SendChatMessage("~w~[~r~StrawberryRP~w~] Hallo " + player.SocialClubName + "!");
            player.SendChatMessage("~w~[~r~StrawberryRP~w~] Wir wünschen dir viel Spaß auf unserem Server!");
            player.TriggerEvent("browseroeffnen");
            player.TriggerEvent("kameraon");
            player.Position = new Vector3(0.0, 0.0, 0.0);
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            Funktionen.LogEintrag(player, "Verbindung getrennt");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + player.SocialClubName + " hat den Server verlassen.", ConsoleColor.Red);
        }
    }
}

