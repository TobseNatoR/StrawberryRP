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
        public void FahrzeugErstellen(Client Player, string Name, int Typ, int Farbe1, int Farbe2)
        {
            //Definitionen
            uint AutoCode = NAPI.Util.GetHashKey(Name);
            string Beschreibung = null;

            //Benötigte Abfragen
            if(NAPI.Data.GetEntitySharedData(Player, "Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~w~[~r~StrawberryRP~w~] Du musst dafür angemeldet sein!"); return; } 
            if (NAPI.Data.GetEntitySharedData(Player, "AdminLevel") < 5) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Deine Rechte reichen nicht aus."); return; }
            if (Typ < 0 || Typ > 3) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Ungültiger Typ."); return; }
            if(Enum.IsDefined(typeof(VehicleHash), AutoCode) == true) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Dieses Fahrzeug kennen wir leider nicht."); return; }

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
                FahrzeugX = Player.Position.X,
                FahrzeugY = Player.Position.Y,
                FahrzeugZ = Player.Position.Z,
                FahrzeugRot = Player.Rotation.Z,
                FahrzeugFarbe1 = Farbe1,
                FahrzeugFarbe2 = Farbe2,
                TankVolumen = 60,
                TankInhalt = 60,
                Kilometerstand = 0
            };
            ContextFactory.Instance.srp_fahrzeuge.Add(Fahrzeug);
            ContextFactory.Instance.SaveChanges();

            Vehicle Auto = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), Player.Rotation.Z, Farbe1, Farbe2, numberPlate: Beschreibung);
            Auto.NumberPlate = Beschreibung;

            //Dem Fahrzeug die Werte übergeben
            Auto.SetData("Id", ContextFactory.Instance.srp_fahrzeuge.Max(x => x.Id));
            Auto.SetData("FahrzeugBeschreibung", Beschreibung);
            Auto.SetData("FahrzeugName", Name);
            Auto.SetData("FahrzeugTyp", Typ);
            Auto.SetData("FahrzeugFraktion", 0);
            Auto.SetData("FahrzeugJob", 0);
            Auto.SetData("FahrzeugSpieler", 0);
            Auto.SetData("FahrzeugMietpreis", 0);
            Auto.SetData("FahrzeugX", Player.Position.X);
            Auto.SetData("FahrzeugY", Player.Position.Y);
            Auto.SetData("FahrzeugZ", Player.Position.Z);
            Auto.SetData("FahrzeugRot", Player.Rotation.Z);
            Auto.SetData("FahrzeugFarbe1", Farbe1);
            Auto.SetData("FahrzeugFarbe2", Farbe2);
            Auto.SetData("TankVolumen", 60);
            Auto.SetData("TankInhalt", 60);
            Auto.SetData("Kilometerstand", 0);

            Player.SetIntoVehicle(Auto, -1);

            if (Typ == 2) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Das Fahrzeug muss nun noch einem Job zugewiesen werden."); }
            if (Typ == 3) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Das Fahrzeug muss nun noch einer Fraktion zugewiesen werden."); }
            if (Typ == 4) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Das Fahrzeug muss nun noch einem Besitzer zugewiesen werden."); }

            Funktionen.LogEintrag(Player, Beschreibung + " Fahrzeug erstellt");
        }

        [Command("teleporten", "Nutze: /teleporten [Spielername] [1 = Zu ihm, 2 = Zu mir]")]
        public void Teleport(Client Player, String Spieler, int Typ)
        {
            //Benötigte Abfragen
            if (NAPI.Data.GetEntitySharedData(Player, "Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~w~[~r~StrawberryRP~w~] Du musst dafür angemeldet sein!"); return; }
            if (NAPI.Data.GetEntitySharedData(Player, "AdminLevel") < 1) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Deine Rechte reichen nicht aus."); return; }
            if (Typ < 0 || Typ > 2) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Ungültige Eingabe."); return; }

            Client Spieler1 = NAPI.Player.GetPlayerFromName(Spieler);
            if(Spieler1 == null) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Dieser Spieler konnte nicht gefunden werden."); return; }

            if(Typ == 1)
            {
                Player.Position = Spieler1.Position;
                NAPI.Notification.SendNotificationToPlayer(Player, "~w~[~r~StrawberryRP~w~] Du hast dich zu " + Spieler1.Name + " teleportiert!");
                NAPI.Notification.SendNotificationToPlayer(Spieler1, "~w~[~r~StrawberryRP~w~] " + Player.Name + " hat sich zu dir teleportiert!");
            }
            else if(Typ == 2)
            {
                Spieler1.Position = Player.Position;
                NAPI.Notification.SendNotificationToPlayer(Player, "~w~[~r~StrawberryRP~w~] Du hast " + Spieler1.Name + " zu dit teleportiert!");
                NAPI.Notification.SendNotificationToPlayer(Spieler1, "~w~[~r~StrawberryRP~w~] " + Player.Name + " hat dich zu sich teleportiert!");
            }
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
