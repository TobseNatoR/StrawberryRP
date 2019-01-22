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
        public const String Verbindung = "Server=localhost;Database=strawberryrp;Uid=root;Pwd=";

        public static void AllesStarten()
        {
            NAPI.World.SetTime(19, 15, 0);
            Fahrzeuge.FahrzeugeSpawnen();
            TextLabelsLaden();
            var Accounts = ContextFactory.Instance.srp_accounts.Count();
            var Log = ContextFactory.Instance.srp_log.Count();
            var Fahzeuge = ContextFactory.Instance.srp_fahrzeuge.Count();
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Accounts + " Accounts wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Log + " Log einträge wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Fahzeuge + " Fahrzeuge wurden geladen.");
        }
        public static void SpielerLaden(Client player)
        {
            NAPI.Data.SetEntitySharedData(player, "Name", "Blahtan");
            NAPI.Data.GetEntitySharedData(player, "Name");
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
