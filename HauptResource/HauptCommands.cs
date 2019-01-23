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
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~w~[~r~StrawberryRP~w~] Du musst dafür angemeldet sein!"); return; }
            if (Player.GetData("AdminLevel") < 5) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Deine Rechte reichen nicht aus."); return; }
            if (Typ < 0 || Typ > 4) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Ungültiger Typ."); return; }
            if(Enum.IsDefined(typeof(VehicleHash), AutoCode) == false) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Dieses Fahrzeug kennen wir leider nicht."); return; }

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

            Vehicle Auto = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), Player.Rotation.Z, Farbe1, Farbe2, numberPlate: Beschreibung);
            Auto.NumberPlate = Beschreibung;

            Player.SetIntoVehicle(Auto, -1);

            var Fahrzeug = new Auto
            {
                FahrzeugBeschreibung = Beschreibung,
                FahrzeugName = Name,
                FahrzeugTyp = Typ,
                FahrzeugFraktion = 0,
                FahrzeugJob = 0,
                FahrzeugSpieler = 0,
                FahrzeugMietpreis = 0,
                FahrzeugX = Player.Vehicle.Position.X,
                FahrzeugY = Player.Vehicle.Position.Y,
                FahrzeugZ = Player.Vehicle.Position.Z,
                FahrzeugRot = Player.Vehicle.Rotation.Z,
                FahrzeugFarbe1 = Farbe1,
                FahrzeugFarbe2 = Farbe2,
                TankVolumen = 60,
                TankInhalt = 60,
                Kilometerstand = 0
            };
            ContextFactory.Instance.srp_fahrzeuge.Add(Fahrzeug);
            ContextFactory.Instance.SaveChanges();

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

            if (Typ == 1) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Das Fahrzeug muss nun noch einem Job zugewiesen werden."); }
            if (Typ == 3) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Das Fahrzeug muss nun noch einer Fraktion zugewiesen werden."); }
            if (Typ == 4) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Das Fahrzeug muss nun noch einem Besitzer zugewiesen werden."); }

            Funktionen.LogEintrag(Player, Beschreibung + " Fahrzeug erstellt");
        }

        [Command("/waffe")]
        public void Waffe(Client Player, string weaponName, int ammo)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~w~[~r~StrawberryRP~w~] Du musst dafür angemeldet sein!"); return; }
            if (Player.GetData("AdminLevel") < 5) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Deine Rechte reichen nicht aus."); return; }

            WeaponHash weapon = NAPI.Util.WeaponNameToModel(weaponName);

            if (weapon == 0)
            {
                Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Diese Waffe ist uns nicht bekannt.");
            }
            else
            {
                Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Du hast die Waffe erhalten.");
                Player.GiveWeapon(weapon, ammo);
            }
        }

        [Command("/parken", "Nutze /parken")]
        public void Parken(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~w~[~r~StrawberryRP~w~] Du musst dafür angemeldet sein!"); return; }
            if (Player.GetData("AdminLevel") < 3) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Deine Rechte reichen nicht aus."); return; }

            if (Player.IsInVehicle)
            {
                int FahrzeugID = Player.Vehicle.GetData("Id");
                var Fahrzeug = ContextFactory.Instance.srp_fahrzeuge.Where(x => x.Id == FahrzeugID).FirstOrDefault();

                Fahrzeug.FahrzeugX = Player.Position.X;
                Fahrzeug.FahrzeugY = Player.Position.Y;
                Fahrzeug.FahrzeugZ = Player.Position.Z;
                Fahrzeug.FahrzeugRot = Player.Vehicle.Rotation.Z;

                Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Das Fahrzeug mit der ID: ~g~" + FahrzeugID + " ~w~wurde erfolgreich geparkt.");

                ContextFactory.Instance.SaveChanges();
            }
            else
            {
                Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("teleporten", "Nutze: /teleporten [Spielername] [1 = Zu ihm, 2 = Zu mir]")]
        public void Teleport(Client Player, String Spieler, int Typ)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~w~[~r~StrawberryRP~w~] Du musst dafür angemeldet sein!"); return; }
            if (Player.GetData("AdminLevel") < 1) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Deine Rechte reichen nicht aus."); return; }
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

        [Command("admingeben", "Nutze: /admingeben [Spielername] [AdminLevel]")]
        public void Admingeben(Client Player, String Spieler, int Level)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~w~[~r~StrawberryRP~w~] Du musst dafür angemeldet sein!"); return; }
            if (Player.GetData("AdminLevel") < 5) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Deine Rechte reichen nicht aus."); return; }
            if (Level < 0 || Level > 5) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Ungültige Eingabe."); return; }

            Client Spieler1 = NAPI.Player.GetPlayerFromName(Spieler);
            if (Spieler1 == null) { Player.SendChatMessage("~w~[~r~StrawberryRP~w~] Dieser Spieler konnte nicht gefunden werden."); return; }

            Spieler1.SetData("AdminLevel", Level);

            NAPI.Notification.SendNotificationToPlayer(Player, "~w~[~r~StrawberryRP~w~] Du hast " + Spieler1.Name + " Admin Level " + Level + " gegeben!");
            NAPI.Notification.SendNotificationToPlayer(Spieler1, "~w~[~r~StrawberryRP~w~] Du hast von " + Player.Name + " Admin Level " + Level + " erhalten!");
        }

        [Command("hilfe")]
        public void Hilfe(Client player)
        {
            //Benötigte Abfragen
            if (player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(player, "~w~[~r~StrawberryRP~w~] Du musst dafür angemeldet sein!"); return; }

            //Clientseitiges Event Triggern und ihm das AdminLevel mitgeben
            player.TriggerEvent("hilfebrowseroeffnen", player.GetData("AdminLevel"));
        }

        [Command("test")]
        public void Test(Client player)
        {
            player.SendChatMessage("~w~[~r~StrawberryRP~w~] Rot: " + player.Vehicle.Rotation.Z);
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
