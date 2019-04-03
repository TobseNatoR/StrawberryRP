/************************************************************************************************************************************************************************************************
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        @@ Dieser Gamemode wurde von Toby Gallenkamp Wohnhaft in der Fontanestraße 35 in Hatten programmiert.                                                                   @@
        @@ Die Entwicklung dieses Gamemodes wurde im Januar 2019 aufgenommen.                                                                                                   @@
        @@ Es dürfen nur von Toby Gallenkamp bestimmte Entwickler an diesem Gamemode arbeiten.                                                                                  @@
        @@ Alle Arbeiten an diesem Gamemode gehören automatisch Strawberry Roleplay und dürfen auch nur von Strawberry Roleplay genutzt werden.                                 @@
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        @@ Sollte dieser Gamemode in Hände dritter gelangen, so ist Toby Gallenkamp unter folgender Telefonnummer zu kontaktieren: 0160/1144521                                 @@
        @@ Sollte Toby Gallenkamp in diesem Fall nicht kontaktiert werden, so macht sich die Person nach § 106 Urheberrechtsgesetz strafbar.                                    @@
        @@ In einem solchen Fall wird nicht gezögert mit einem Anwalt gegen die Person vor zu gehen.                                                                            @@
        @@ Sollte Toby Gallenkamp durch einen Unfall oder sonstige Umstände sterben, so gehört dieser Gamemode Jakob Pritschmann wohnhaft in der Straße Hunteaue 1 in Hatten.   @@
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
************************************************************************************************************************************************************************************************/

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
                    Funktionen.AlleBlipsWeg(Player);
                    Player.SendChatMessage("~r~Unser Server ist noch in Entwicklung. Melde dich auf www.strawberry-rp.de");
                    NAPI.Notification.SendNotificationToPlayer(Player, "~r~Unser Server ist noch in Entwicklung. Melde dich auf www.strawberry-rp.de");
                    Timer.SetTimer(() => Funktionen.SpielerKickenBlipsNeuladen(Player), 2000, 1);

                    //Log Eintrag
                    Funktionen.LogEintrag(Player, "Nicht auf der Whitelist");
                    return;
                }
            }

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Verbunden");

            //Consolen Nachricht
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Player.SocialClubName + " hat sich mit dem Server verbunden. [" + DateTime.Now + "]", ConsoleColor.Red);

            //Chat entfernen
            Player.TriggerEvent("Chathiden");

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
            Player.SetData("Pferderennen", 0);
            Player.SetData("NachträglicherNickname", 0);
            Player.SetData("HeiratsAntrag", 0);
            Player.SetData("HeiratsId", 0);
            Player.SetData("HeiratenId", 0);
            Player.SetData("HeiratenBrowser", 0);
            Player.SetData("GruppenEinladungId", 0);
            Player.SetData("StadthalleInt", 0);

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

