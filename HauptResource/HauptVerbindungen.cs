/************************************************************************************************************
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        @@ Strawberry Roleplay Gamemode                                                                   @@
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
*************************************************************************************************************/

using GTANetworkAPI;
using Datenbank;
using System;
using System.Collections.Generic;
using System.Linq;
using Fahrzeug;
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
            //Eingeloggt auf 0 für Command Check
            Player.SetData("Eingeloggt", 0);

            //Whitelist Check
            var Check = ContextFactory.Instance.srp_whitelist.Count(x => x.SocialClub == Player.SocialClubName);
            {
                if (Check == 0)
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~r~Du bist nicht auf der Whitelist. Melde dich auf www.strawberry-rp.de");

                    //Log Eintrag
                    Funktionen.LogEintrag(Player, "Nicht auf der Whitelist");

                    NAPI.Player.KickPlayer(Player, "Nicht auf der Whitelist");
                    return;
                }
            }

            //DLCs Checken
            Player.TriggerEvent("checkDLC");

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Verbunden");

            //Consolen Nachricht
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Player.SocialClubName + " hat sich mit dem Server verbunden. [" + DateTime.Now + "]", ConsoleColor.Red);

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
                Fahrzeuge.JobFahrzeugLöschen(Player, Funktionen.AccountJobFahrzeugBekommen(Player));
                Funktionen.AccountPositionInteriorDimensionUpdaten(Player);
                Funktionen.SpielerSpeichernDisconnect(Player);

                //Spieler Online Status
                Funktionen.ServerSpielerGejoined(2);
            }

            //Daten zur Sicherheit zurücksetzen
            //Generelle Daten
            Player.SetData("InteriorName", 0);
            Player.SetData("Eingeloggt", 1);
            Player.SetData("BewegtSichMitFahrzeug", 0);
            Player.SetData("SiehtPerso", 0);
            Player.SetData("IBerry", 0);
            Player.SetData("Scoreboard", 0);
            Player.SetData("Freezed", 0);
            Player.SetData("AmTanken", 0);
            Player.SetData("TankenTankstellenId", 0);
            Player.SetData("TankRechnung", 0);
            Player.SetData("KaufenTyp", 0);
            Player.SetData("KaufenId", 0);
            Player.SetData("KaufenPreis", 0);
            Player.SetData("KeyCoolDown", 0);
            Player.SetData("MenuCoolDown", 0);
            Player.SetData("Verwaltungsmodus", 0);
            Player.SetData("NachträglicherNickname", 0);
            Player.SetData("HeiratsAntrag", 0);
            Player.SetData("HeiratsId", 0);
            Player.SetData("HeiratenId", 0);
            Player.SetData("HeiratenBrowser", 0);
            Player.SetData("GruppenEinladungId", 0);
            Player.SetData("StadthalleInt", 0);
            Player.SetData("Chat", 0);

            //Job Daten Berufskraftfahrer
            Player.SetData("BerufskraftfahrerFahrzeug", 0);
            Player.SetData("BerufskraftfahrerHolz", 0);
            Player.SetData("BerufskraftfahrerHolzGeladen", 0);
            Player.SetData("BerufskraftfahrerJobAngenommen", 0);
            Player.SetData("BerufskraftfahrerKraftstoffTyp", 0);
            Player.SetData("BerufskraftfahrerDieselTanke", 0);
            Player.SetData("BerufskraftfahrerE10Tanke", 0);
            Player.SetData("BerufskraftfahrerSuperTanke", 0);
            Player.SetData("BerufskraftfahrerVerdienst", 0);
            Player.SetData("BerufskraftfahrerAmAbladen", 0);

            //Job Daten Busfahrer
            Player.SetData("BusfahrerFahrzeug", 0);
            Player.SetData("BusfahrerJobAngenommen", 0);
            Player.SetData("BusfahrerRoute", 0);
            Player.SetData("BusfahrerRoutePosition", 0);

            //Dialoge
            Player.SetData("FahrzeugPrivatDialog", 0);

            Funktionen.LogEintrag(Player, "Verbindung getrennt");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Player.SocialClubName + " hat den Server verlassen.", ConsoleColor.Red);
        }
    }
}

