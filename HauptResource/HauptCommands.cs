using GTANetworkAPI;
using Datenbank;
using Fahrzeug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Haupt
{
    //Hallo djdjdjdjdjdjdjd
    
    public class Commands : Script
    {
        [Command("fahrzeugerstellen", "Nutze: /fahrzeugerstellen [Name] [Typ] [Farbe 1] [Farbe 2]")]
        public void FahrzeugErstellen(Client player, string Name, int Typ, int Farbe1, int Farbe2)
        {
            if (Typ < 0 || Typ > 2) { player.SendChatMessage("~w~[~r~StrawberryRP~w~] Ungültiger Typ."); return; }
            uint AutoCode = NAPI.Util.GetHashKey(Name);
            bool AutoCheck = Enum.IsDefined(typeof(VehicleHash), AutoCode);
            if(AutoCheck == true)
            {
                string Beschreibung = null;
                switch (Typ)
                {
                    case 0:
                        Beschreibung = "Admin";
                        break;
                    case 1:
                        Beschreibung = "Job";
                        break;
                    case 2:
                        Beschreibung = "Miet";
                        break;
                    default:
                        Beschreibung = "Unbekannt";
                        break;
                }
                var Fahrzeug = new Auto
                {
                    FahrzeugBeschreibung = Beschreibung,
                    FahrzeugName = Name,
                    FahrzeugTyp = Typ,
                    FahrzeugX = player.Position.X,
                    FahrzeugY = player.Position.Y,
                    FahrzeugZ = player.Position.Z,
                    FahrzeugRot = player.Rotation.Z,
                    FahrzeugFarbe1 = Farbe1,
                    FahrzeugFarbe2 = Farbe2,
                    TankVolumen = 60,
                    TankInhalt = 60,
                    Kilometerstand = 0
                };
                ContextFactory.Instance.srp_fahrzeuge.Add(Fahrzeug);
                ContextFactory.Instance.SaveChanges();
                Vehicle Auto = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(player.Position.X, player.Position.Y, player.Position.Z), player.Rotation.Z, Farbe1, Farbe2, numberPlate: Beschreibung);
                player.SetIntoVehicle(Auto, -1);
                Auto.NumberPlate = Beschreibung;
            }
            else
            {
                player.SendChatMessage("~w~[~r~StrawberryRP~w~] Dieses Fahrzeug kennen wir leider nicht.");
            }
        }
        [Command("test", "Nutze: /test")]
        public void test(Client player)
        {
            player.SendChatMessage("~w~[~r~StrawberryRP~w~] Auto ID: " + player.Vehicle.GetData("ID"));
        }
        [Command("save", "Use /save [Position Name]", GreedyArg = true)]
        public void CMD_SavePosition(Client player, string PosName = "No Set")
        {
            var pos = (player.IsInVehicle) ? player.Vehicle.Position : player.Position;
            var rot = (player.IsInVehicle) ? player.Vehicle.Rotation : player.Rotation;

            using (var stream = File.AppendText("SavePos.txt"))
            {
                if (player.IsInVehicle)
                {
                    NAPI.Notification.SendNotificationToPlayer(player, "~g~In car ~w~postion saved with name ~r~" + PosName, true);
                    stream.WriteLine("IN VEH || " + PosName + ":" + pos.X + ", " + pos.Y + ", " + pos.Z + "    Rot:    " + rot.Z);
                    stream.Close();
                }
                else
                {
                    NAPI.Notification.SendNotificationToPlayer(player, "~g~On foot ~w~position saved with name ~r~" + PosName, true);
                    stream.WriteLine("ON FOOT|| " + PosName + ":" + pos.X + ", " + pos.Y + ", " + pos.Z + "    Rot:    " + rot.Z);
                    stream.Close();
                }
            }
        }
    }
}
