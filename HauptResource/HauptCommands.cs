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
    public class Commands : Script
    {
        [Command("fahrzeugerstellen", "Nutze: /fahrzeugerstellen [Name] [Typ 0 = Admin, 1 = Job, 2 = Miet, 3 = Fraktion, 4 = Privat] [Farbe 1] [Farbe 2]")]
        public void FahrzeugErstellen(Client player, string Name, int Typ, int Farbe1, int Farbe2)
        {
            //Definitionen
            uint AutoCode = NAPI.Util.GetHashKey(Name);
            string Beschreibung = null;

            //Benötigte Abfragen
            if(NAPI.Data.GetEntitySharedData(player, "Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(player, "~w~[~r~StrawberryRP~w~] Du musst dafür angemeldet sein!"); return; } 
            if (NAPI.Data.GetEntitySharedData(player, "AdminLevel") < 5) { player.SendChatMessage("~w~[~r~StrawberryRP~w~] Deine Rechte reichen nicht aus."); return; }
            if (Typ < 0 || Typ > 3) { player.SendChatMessage("~w~[~r~StrawberryRP~w~] Ungültiger Typ."); return; }
            if(Enum.IsDefined(typeof(VehicleHash), AutoCode) == true) { player.SendChatMessage("~w~[~r~StrawberryRP~w~] Dieses Fahrzeug kennen wir leider nicht."); return; }

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
                case 3:
                    Beschreibung = "Fraktion";
                    break;
                case 4:
                    Beschreibung = "Privat";
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
                FahrzeugFraktion = 0,
                FahrzeugJob = 0,
                FahrzeugSpieler = 0,
                FahrzeugMietpreis = 0,
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
            Auto.NumberPlate = Beschreibung;

            player.SetIntoVehicle(Auto, -1);

            if (Typ == 2) { player.SendChatMessage("~w~[~r~StrawberryRP~w~] Das Fahrzeug muss nun noch einem Job zugewiesen werden."); }
            if (Typ == 3) { player.SendChatMessage("~w~[~r~StrawberryRP~w~] Das Fahrzeug muss nun noch einer Fraktion zugewiesen werden."); }
            if (Typ == 4) { player.SendChatMessage("~w~[~r~StrawberryRP~w~] Das Fahrzeug muss nun noch einem Besitzer zugewiesen werden."); }

            Funktionen.LogEintrag(player, Beschreibung + " Fahrzeug erstellt");
        }

        [Command("hilfe")]
        public void Hilfe(Client player)
        {
            //Benötigte Abfragen
            if (NAPI.Data.GetEntitySharedData(player, "Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(player, "~w~[~r~StrawberryRP~w~] Du musst dafür angemeldet sein!"); return; }

            //Clientseitiges Event Triggern und ihm das AdminLevel mitgeben
            player.TriggerEvent("hilfebrowseroeffnen", NAPI.Data.GetEntitySharedData(player, "AdminLevel"));
        }

        [Command("test")]
        public void Test(Client player)
        {
            if (player.IsInVehicle)
            {
                player.SendChatMessage("~w~[~r~StrawberryRP~w~] Auto ID: " + player.Vehicle.GetData("Id"));
            }
            else
            {
                player.SendChatMessage("~w~[~r~StrawberryRP~w~] Du musst in einem Fahrzeug sein");
            }
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
