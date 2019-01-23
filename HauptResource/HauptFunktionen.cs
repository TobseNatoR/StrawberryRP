using GTANetworkAPI;
using Datenbank;
using Fahrzeug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haupt
{
    public class Funktionen
    {
        //Globale Variablen
        //public const String Verbindung = "Server=127.0.0.1; Database=strawberryrp; Uid=strawberryserver; Pwd=tUGFHQfy3NEChUtt";
        public const String Verbindung = "Server=localhost;Database=strawberryrp;Uid=root;Pwd=";
        
        public enum AdminLevel : int
        {
            Supporter = 1,
            Administrator = 2,
            HeadAdministrator = 3,
            Projektleitung = 4,
            Entwickler = 5
        }

        public static void AllesStarten()
        {
            //Server Settings
            NAPI.World.SetTime(19, 15, 0);

            //Wichtige Dinge die geladen werden müssen
            Fahrzeuge.FahrzeugeSpawnen();
            TextLabelsLaden();

            //Statistiken für die Log
            var Accounts = ContextFactory.Instance.srp_accounts.Count();
            var Log = ContextFactory.Instance.srp_log.Count();
            var Fahzeuge = ContextFactory.Instance.srp_fahrzeuge.Count();
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Accounts + " Accounts wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Log + " Log einträge wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Fahzeuge + " Fahrzeuge wurden geladen.");
        }

        public static void SpielerLaden(Client player)
        {
            foreach (var Account in ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == player.SocialClubName).ToList())
            {
                //Daten für den Spieler lokal setzen
                NAPI.Data.SetEntitySharedData(player, "Eingeloggt", 1);
                NAPI.Data.SetEntitySharedData(player, "ID", Account.Id);
                NAPI.Data.SetEntitySharedData(player, "Nickname", Account.NickName);
                NAPI.Data.SetEntitySharedData(player, "AdminLevel", Account.AdminLevel);
            }
        }

        public static void LogEintrag(Client player, string Aktion)
        {
            var Log = new Log
            {
                Aktion = Aktion,
                SocialClub = player.SocialClubName,
                Wann = DateTime.Now
            };
            ContextFactory.Instance.srp_log.Add(Log);
            ContextFactory.Instance.SaveChanges();
        }

        public static void TextLabelsLaden()
        {
            NAPI.TextLabel.CreateTextLabel("~r~StrawberryRP~w~~n~/rollermieten", new Vector3(34.85226, -1495.312, 29.32524), 20.0f, 0.75f, 4, new Color(255, 255, 255));
        }

        public static void SpawnManager(Client player)
        {
            player.Position = new Vector3(22.06838, -1506.167, 31.85011);
            player.Rotation.Z = 226.9566f;
        }
    }
}
