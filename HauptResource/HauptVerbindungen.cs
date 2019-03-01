using GTANetworkAPI;
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
        public void OnPlayerConnected(Client Player)
        {
            //Whitelist Check
            var Check = ContextFactory.Instance.srp_whitelist.Count(x => x.SocialClub == Player.SocialClubName);
            {
                if (Check == 0)
                {
                    Player.SendChatMessage("~r~Unser Server ist noch in Entwicklung. Melde dich auf www.strawberry-rp.de");
                    NAPI.Player.KickPlayer(Player, "Unser Server ist in Entwicklung!");
                }
            }

            //Dimension
            Player.Dimension = NAPI.GlobalDimension;

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Verbunden");

            //Consolen Nachricht
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Player.SocialClubName + " hat sich mit dem Server verbunden. [" + DateTime.Now + "]", ConsoleColor.Red);

            //Chat entfernen
            Player.TriggerEvent("Chathiden");

            //Eingeloggt auf 0 für Command Check
            Player.SetData("Eingeloggt", 0);

            //Nachrichten für den Spieler
            Player.SendChatMessage("~y~Info~w~: Hallo " + Player.SocialClubName + "!");
            Player.SendChatMessage("~y~Info~w~: Wir wünschen dir viel Spaß bei uns!");

            //Laden und danach Login/Register
            Player.TriggerEvent("Laden");
            Timer.SetTimer(() => Funktionen.LoginLadenBeenden(Player), 4000, 1);

            //An den eine Versteckte Position setzen damit er nirgendwo rumsteht
            Player.Position = new Vector3(-3245.781, 945.7014, 7.519356);

            //Freeze
            Funktionen.Freeze(Player);

            //Camera
            Player.TriggerEvent("moveSkyCamera", Player, "up", 1, false);
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Client Player, DisconnectionType type, string reason)
        {
            //Nur speichern wenn er Eingeloggt war
            if(Player.GetData("Eingeloggt") == 1)
            {
                Funktionen.SpielerSpeichern(Player);
            }

            //Daten zur Sicherheit zurücksetzen
            Player.SetData("SiehtPerso", 0);
            Player.SetData("AmTanken", 0);
            Player.SetData("TankenTankstellenId", 0);
            Player.SetData("TankRechnung", 0);
            Player.SetData("KaufenTyp", 0);
            Player.SetData("KaufenId", 0);
            Player.SetData("KaufenPreis", 0);
            Player.SetData("Cooldown", 0);
            Player.SetData("Verwaltungsmodus", 0);

            //Dialoge
            Player.SetData("StadthalleDialog", 0);
            Player.SetData("FahrzeugPrivatDialog", 0);

            Funktionen.LogEintrag(Player, "Verbindung getrennt");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Player.SocialClubName + " hat den Server verlassen.", ConsoleColor.Red);
        }
    }
}

