﻿/************************************************************************************************************
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        @@ Strawberry Roleplay Gamemode                                                                   @@
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
*************************************************************************************************************/

using GTANetworkAPI;
using Datenbank;
using Fahrzeug;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;



namespace Haupt
{    
    public class Commands : Script
    {
        //Test CMD
        [Command("test", "Nutze: /test")]
        public void test(Client Player)
        {
			NAPI.Player.SetPlayerClothes(Player, 11, 15, 0);
			NAPI.Player.SetPlayerClothes(Player, 3, 15, 0);
			NAPI.Player.SetPlayerClothes(Player, 8, 15, 0);
		}

        //Test1 CMD
        [Command("test1", "Nutze: /test")]
        public void test1(Client Player)
        {
            
        }

        [Command("delallacars", "Nutze: /delallacars")]
        public void AlleAdminFahrzeugeLöschen(Client Player)
        {
            //Definitionen
            int i = 0;
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AlleAdminFahrzeugeLöschen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            foreach (var Fahrzeuge in NAPI.Pools.GetAllVehicles())
            {

                foreach (AutoLokal auto in Funktionen.AutoListe)
                {
                    if (auto.FahrzeugTyp == 0)
                    {
                        auto.Fahrzeug.Delete();
                        Funktionen.AutoListe.Remove(auto);
                        i += 1;
                    }
                }
            }

            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurden " + i + " Admin Fahrzeug(e) gelöscht.");
        }
        [Command("acarmod", "Nutze: /modcar [acarmod]")]
        public void ModCarErstellen(Client Player, String Name)
        {
            //Definitionen
            uint AutoCode = NAPI.Util.GetHashKey(Name);
            Name = Funktionen.ErsterBuchstabeGroß(Name);

            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AdminModFahrzeugErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Enum.IsDefined(typeof(VehicleHash), AutoCode) == false && Funktionen.FahrzeugModVorhanden(Name) == false) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieses Fahrzeug kennen wir leider nicht."); return; }

            //Objekt für die Liste erzeugen
            AutoLokal auto = new AutoLokal();

            //Das Fahrzeug spawnen
            auto.Fahrzeug = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), Player.Rotation.Z, 0, 0, numberPlate: Funktionen.FahrzeugTypNamen(0));

            auto.Fahrzeug.NumberPlate = Funktionen.FahrzeugTypNamen(0);
            auto.Fahrzeug.Dimension = 0;

            auto.Id = -1;
            auto.FahrzeugBeschreibung = Funktionen.FahrzeugTypNamen(0);
            auto.FahrzeugName = Name;
            auto.FahrzeugTyp = 0;
            auto.FahrzeugFraktion = 0;
            auto.FahrzeugJob = 0;
            auto.FahrzeugSpieler = 0;
            auto.FahrzeugMietpreis = 0;
            auto.FahrzeugKaufpreis = 0;
            auto.FahrzeugAutohaus = 0;
            auto.FahrzeugMaxMietzeit = 0;
            auto.FahrzeugMietzeit = 0;
            auto.FahrzeugX = Player.Position.X;
            auto.FahrzeugY = Player.Position.Y;
            auto.FahrzeugZ = Player.Position.Z;
            auto.FahrzeugRot = Player.Rotation.Z;
            auto.FahrzeugFarbe1 = 0;
            auto.FahrzeugFarbe2 = 0;
            auto.TankVolumen = Funktionen.TankVolumenBerechnen(Name);
            auto.TankInhalt = Funktionen.TankVolumenBerechnen(Name) * 10 * 100;
            auto.Kilometerstand = 0;
            auto.KraftstoffArt = 3;
            auto.FahrzeugHU = DateTime.Now.AddMonths(+1);
            auto.FahrzeugAbgeschlossen = 0;
            auto.FahrzeugMotor = 1;
            auto.FahrzeugGespawnt = 1;

            //Diese Sachen nur lokal
            auto.FahrzeugAltePositionX = Player.Position.X;
            auto.FahrzeugAltePositionY = Player.Position.Y;
            auto.FahrzeugAltePositionZ = Player.Position.Z;
            auto.FahrzeugNeuePositionX = 0;
            auto.FahrzeugNeuePositionY = 0;
            auto.FahrzeugNeuePositionZ = 0;

            //Fahrzeug in der Liste ablegen
            Funktionen.AutoListe.Add(auto);

            //Den Spieler in das Auto setzen
            Player.SetIntoVehicle(auto.Fahrzeug, -1);

            //ID Setzen
            auto.Fahrzeug.SetData("Id", -1);

            Funktionen.LogEintrag(Player, "Admin Mod Fahrzeug erstellt");
        }

        [Command("ferstellen", "Nutze: /ferstellen [Name] [Typ 0 = Admin, 1 = Job, 2 = Miet, 3 = Fraktion, 4 = Privat, 5 = Autohaus] [Farbe 1] [Farbe 2]")]
        public void FahrzeugErstellen(Client Player, String Name, int Typ, int Farbe1, int Farbe2)
        {
            //Definitionen
            
            uint AutoCode = NAPI.Util.GetHashKey(Name);
            Name = Funktionen.ErsterBuchstabeGroß(Name);

            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Typ < 0 || Typ > 5) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Ungültiger Typ."); return; }
            if(Enum.IsDefined(typeof(VehicleHash), AutoCode) == false && Funktionen.FahrzeugModVorhanden(Name) == false) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieses Fahrzeug kennen wir leider nicht."); return; }
            if (Typ == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Job Fahrzeuge werden an der jeweiligen Base gespawnt. Sie können nicht mehr per /ferstellen erstellt werden."); return; }

            if(Typ != 0)
            {
                //Ein neues Objekt erzeugen
                var veh = new Auto
                {
                    FahrzeugBeschreibung = Funktionen.FahrzeugTypNamen(Typ),
                    FahrzeugName = Name,
                    FahrzeugTyp = Typ,
                    FahrzeugFraktion = 0,
                    FahrzeugJob = 0,
                    FahrzeugSpieler = 0,
                    FahrzeugMietpreis = 0,
                    FahrzeugKaufpreis = 0,
                    FahrzeugAutohaus = 0,
                    FahrzeugMaxMietzeit = 0,
                    FahrzeugMietzeit = 0,
                    FahrzeugX = Player.Position.X,
                    FahrzeugY = Player.Position.Y,
                    FahrzeugZ = Player.Position.Z,
                    FahrzeugRot = Player.Rotation.Z,
                    FahrzeugFarbe1 = Farbe1,
                    FahrzeugFarbe2 = Farbe2,
                    TankVolumen = Funktionen.TankVolumenBerechnen(Name),
                    TankInhalt = Funktionen.TankVolumenBerechnen(Name) * 10 * 100,
                    Kilometerstand = 0.0f,
                    KraftstoffArt = 3,
                    FahrzeugHU = DateTime.Now.AddMonths(+1),
                    FahrzeugAbgeschlossen = 0,
                    FahrzeugMotor = 1,
                    FahrzeugGespawnt = 1
                };

                //Query absenden
                ContextFactory.Instance.srp_fahrzeuge.Add(veh);
                ContextFactory.Instance.SaveChanges();
            }

            //Objekt für die Liste erzeugen
            AutoLokal auto = new AutoLokal();

            //Das Fahrzeug spawnen
            auto.Fahrzeug = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), Player.Rotation.Z, Farbe1, Farbe2, numberPlate: Funktionen.FahrzeugTypNamen(Typ));

            auto.Fahrzeug.NumberPlate = Funktionen.FahrzeugTypNamen(Typ);
            auto.Fahrzeug.Dimension = 0;

            //Dem Fahrzeug die Werte lokal übergeben
            if(Typ != 0)
            {
                auto.Id = ContextFactory.Instance.srp_fahrzeuge.Max(x => x.Id);
            }
            else
            {
                auto.Id = -1;
            }
            auto.FahrzeugBeschreibung = Funktionen.FahrzeugTypNamen(Typ);
            auto.FahrzeugName = Name;
            auto.FahrzeugTyp = Typ;
            auto.FahrzeugFraktion = 0;
            auto.FahrzeugJob = 0;
            auto.FahrzeugSpieler = 0;
            auto.FahrzeugMietpreis = 0;
            auto.FahrzeugKaufpreis = 0;
            auto.FahrzeugAutohaus = 0;
            auto.FahrzeugMaxMietzeit = 0;
            auto.FahrzeugMietzeit = 0;
            auto.FahrzeugX = Player.Position.X;
            auto.FahrzeugY = Player.Position.Y;
            auto.FahrzeugZ = Player.Position.Z;
            auto.FahrzeugRot = Player.Rotation.Z;
            auto.FahrzeugFarbe1 = Farbe1;
            auto.FahrzeugFarbe2 = Farbe2;
            auto.TankVolumen = Funktionen.TankVolumenBerechnen(Name);
            auto.TankInhalt = Funktionen.TankVolumenBerechnen(Name) * 10 * 100;
            auto.Kilometerstand = 0;
            auto.KraftstoffArt = 3;
            auto.FahrzeugHU = DateTime.Now.AddMonths(+1);
            auto.FahrzeugAbgeschlossen = 0;
            auto.FahrzeugMotor = 1;
            auto.FahrzeugGespawnt = 1;

            //Diese Sachen nur lokal
            auto.FahrzeugAltePositionX = Player.Position.X;
            auto.FahrzeugAltePositionY = Player.Position.Y;
            auto.FahrzeugAltePositionZ = Player.Position.Z;
            auto.FahrzeugNeuePositionX = 0;
            auto.FahrzeugNeuePositionY = 0;
            auto.FahrzeugNeuePositionZ = 0;

            //Fahrzeug in der Liste ablegen
            Funktionen.AutoListe.Add(auto);

            //Dem Auto die DB Id lokal geben
            if (Typ != 0)
            {
                auto.Fahrzeug.SetData("Id", ContextFactory.Instance.srp_fahrzeuge.Max(x => x.Id));
            }
            else
            {
                auto.Fahrzeug.SetData("Id", -1);
            }

            //Den Spieler in das Auto setzen
            Player.SetIntoVehicle(auto.Fahrzeug, -1);

            //Mitteilung das das Fahrzeug noch zugewiesen werden muss
            if (Typ == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Fahrzeug muss nun noch einem Job zugewiesen werden."); }
            if (Typ == 3) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Fahrzeug muss nun noch einer Fraktion zugewiesen werden."); }
            if (Typ == 4) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Fahrzeug muss nun noch einem Besitzer zugewiesen werden."); }
            if (Typ == 5) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Fahrzeug muss nun noch einem Autohaus zugewiesen werden."); }

            Funktionen.LogEintrag(Player, Funktionen.FahrzeugTypNamen(Typ) + "(s) Fahrzeug erstellt");

            auto.Fahrzeug.EngineStatus = true;
        }

        [Command("acar", "Nutze: /acar")]
        public void AdminCar(Client Player)
        {
            //Definitionen
            uint AutoCode = NAPI.Util.GetHashKey("oppressor2");

            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AdminFahrzeugErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            
            //Objekt für die Liste erzeugen
            AutoLokal auto = new AutoLokal();

            //Das Fahrzeug spawnen
            auto.Fahrzeug = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), Player.Rotation.Z, 0, 0, numberPlate: Funktionen.FahrzeugTypNamen(0));

            auto.Fahrzeug.NumberPlate = Funktionen.FahrzeugTypNamen(0);
            auto.Fahrzeug.Dimension = 0;

            
            auto.Id = -1;
            auto.FahrzeugBeschreibung = Funktionen.FahrzeugTypNamen(0);
            auto.FahrzeugName = "Oppressor2";
            auto.FahrzeugTyp = 0;
            auto.FahrzeugFraktion = 0;
            auto.FahrzeugJob = 0;
            auto.FahrzeugSpieler = 0;
            auto.FahrzeugMietpreis = 0;
            auto.FahrzeugKaufpreis = 0;
            auto.FahrzeugAutohaus = 0;
            auto.FahrzeugMaxMietzeit = 0;
            auto.FahrzeugMietzeit = 0;
            auto.FahrzeugX = Player.Position.X;
            auto.FahrzeugY = Player.Position.Y;
            auto.FahrzeugZ = Player.Position.Z;
            auto.FahrzeugRot = Player.Rotation.Z;
            auto.FahrzeugFarbe1 = 0;
            auto.FahrzeugFarbe2 = 0;
            auto.TankVolumen = Funktionen.TankVolumenBerechnen("Oppressor2");
            auto.TankInhalt = Funktionen.TankVolumenBerechnen("Oppressor2") * 10 * 100;
            auto.Kilometerstand = 0;
            auto.KraftstoffArt = 3;
            auto.FahrzeugHU = DateTime.Now.AddMonths(+1);
            auto.FahrzeugAbgeschlossen = 0;
            auto.FahrzeugMotor = 1;
            auto.FahrzeugGespawnt = 1;

            //Diese Sachen nur lokal
            auto.FahrzeugAltePositionX = Player.Position.X;
            auto.FahrzeugAltePositionY = Player.Position.Y;
            auto.FahrzeugAltePositionZ = Player.Position.Z;
            auto.FahrzeugNeuePositionX = 0;
            auto.FahrzeugNeuePositionY = 0;
            auto.FahrzeugNeuePositionZ = 0;

            //Fahrzeug in der Liste ablegen
            Funktionen.AutoListe.Add(auto);

            //Den Spieler in das Auto setzen
            Player.SetIntoVehicle(auto.Fahrzeug, -1);

            //ID Setzen
            auto.Fahrzeug.SetData("Id", -1);

            Funktionen.LogEintrag(Player, "Admin Fahrzeug erstellen");
        }

        [Command("interior", "Nutze: /interior")]
        public void Interior(Client Player)
        {
            NAPI.Notification.SendNotificationToPlayer(Player, "Dein Interior: " + Player.GetData("InteriorName"));
            NAPI.Notification.SendNotificationToPlayer(Player, "Dein Dimension: " + Player.Dimension);
            Player.Dimension = 0;
        }

        [Command("mfferstellen", "Nutze: /mfferstellen [Name]")]
        public void FahrzeugErstellenManufaktur(Client Player, String Name)
        {
            //Definitionen
            uint AutoCode = NAPI.Util.GetHashKey(Name);
            Name = Funktionen.ErsterBuchstabeGroß(Name);
            Random random = new Random();
            int Farbe1 = random.Next(0, 159);
            int Farbe2 = random.Next(0, 159);

            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.ManufakturFahrzeugErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Enum.IsDefined(typeof(VehicleHash), AutoCode) == false) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieses Fahrzeug kennen wir leider nicht."); return; }

            //Ein neues Objekt erzeugen
            var veh = new Auto
            {
                FahrzeugBeschreibung = "Neuwagen",
                FahrzeugName = Name,
                FahrzeugTyp = 5,
                FahrzeugFraktion = 0,
                FahrzeugJob = 0,
                FahrzeugSpieler = 0,
                FahrzeugMietpreis = 0,
                FahrzeugKaufpreis = 0,
                FahrzeugAutohaus = -1,
                FahrzeugMaxMietzeit = 0,
                FahrzeugMietzeit = 0,
                FahrzeugX = Player.Position.X,
                FahrzeugY = Player.Position.Y,
                FahrzeugZ = Player.Position.Z,
                FahrzeugRot = Player.Rotation.Z,
                FahrzeugFarbe1 = Farbe1,
                FahrzeugFarbe2 = Farbe2,
                TankVolumen = Funktionen.TankVolumenBerechnen(Name),
                TankInhalt = Funktionen.TankVolumenBerechnen(Name) * 10 * 100,
                Kilometerstand = 0.0f,
                KraftstoffArt = 3,
                FahrzeugHU = DateTime.Now.AddMonths(+3),
                FahrzeugAbgeschlossen = 0,
                FahrzeugMotor = 1,
                FahrzeugGespawnt = 1
            };

            //Query absenden
            ContextFactory.Instance.srp_fahrzeuge.Add(veh);
            ContextFactory.Instance.SaveChanges();

            //Objekt für die Liste erzeugen
            AutoLokal auto = new AutoLokal();

            //Das Fahrzeug spawnen
            auto.Fahrzeug = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), Player.Rotation.Z, Farbe1, Farbe2, numberPlate: "Neuwagen");

            auto.Fahrzeug.NumberPlate = "Neuwagen";
            auto.Fahrzeug.Dimension = 0;

            //Dem Fahrzeug die Werte lokal übergeben
            auto.Id = ContextFactory.Instance.srp_fahrzeuge.Max(x => x.Id);
            auto.FahrzeugBeschreibung = "Neuwagen";
            auto.FahrzeugName = Name;
            auto.FahrzeugTyp = 5;
            auto.FahrzeugFraktion = 0;
            auto.FahrzeugJob = 0;
            auto.FahrzeugSpieler = 0;
            auto.FahrzeugMietpreis = 0;
            auto.FahrzeugKaufpreis = 0;
            auto.FahrzeugAutohaus = -1;
            auto.FahrzeugMaxMietzeit = 0;
            auto.FahrzeugMietzeit = 0;
            auto.FahrzeugX = Player.Position.X;
            auto.FahrzeugY = Player.Position.Y;
            auto.FahrzeugZ = Player.Position.Z;
            auto.FahrzeugRot = Player.Rotation.Z;
            auto.FahrzeugFarbe1 = Farbe1;
            auto.FahrzeugFarbe2 = Farbe2;
            auto.TankVolumen = Funktionen.TankVolumenBerechnen(Name);
            auto.TankInhalt = Funktionen.TankVolumenBerechnen(Name) * 10 * 100;
            auto.Kilometerstand = 0;
            auto.KraftstoffArt = 3;
            auto.FahrzeugHU = DateTime.Now.AddMonths(+3);
            auto.FahrzeugAbgeschlossen = 0;
            auto.FahrzeugMotor = 1;
            auto.FahrzeugGespawnt = 1;

            //Diese Sachen nur lokal
            auto.FahrzeugAltePositionX = Player.Position.X;
            auto.FahrzeugAltePositionY = Player.Position.Y;
            auto.FahrzeugAltePositionZ = Player.Position.Z;
            auto.FahrzeugNeuePositionX = 0;
            auto.FahrzeugNeuePositionY = 0;
            auto.FahrzeugNeuePositionZ = 0;

            //Fahrzeug in der Liste ablegen
            Funktionen.AutoListe.Add(auto);

            //Dem Auto die DB Id lokal geben
            auto.Fahrzeug.SetData("Id", ContextFactory.Instance.srp_fahrzeuge.Max(x => x.Id));

            //Den Spieler in das Auto setzen
            Player.SetIntoVehicle(auto.Fahrzeug, -1);

            //Auto als kaufbar markieren
            Fahrzeuge.AutohausSetzen(Player.Vehicle, -1);

            Funktionen.LogEintrag(Player, "Manufaktur Fahrzeug erstellt");

            auto.Fahrzeug.EngineStatus = true;
        }

        [Command("herstellen", "Nutze: /herstellen [Interior 1 - 25] [Kaufpreis]")]
        public void ImmobilieErstellen(Client Player, int Interior, long Kaufpreis)
        {
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.HausErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if(Interior <= 0 || Interior > 25) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieses Interior ist uns nicht bekannt."); return; } 

            //Benötigte Definitionen
            String InteriorName = null;
            float EingangX = 0.0f, EingangY = 0.0f, EingangZ = 0.0f;

            //Interior Liste
            if(Interior == 1) { InteriorName = "apa_v_mp_h_01_a"; EingangX = -786.8663f; EingangY = 315.7642f; EingangZ = 217.6385f; }
            else if (Interior == 2) { InteriorName = "apa_v_mp_h_01_c";	EingangX = -786.9563f; EingangY = 315.6229f; EingangZ = 187.9136f; }
            else if (Interior == 3) { InteriorName = "apa_v_mp_h_01_b"; EingangX = -774.0126f; EingangY = 342.0428f; EingangZ = 196.6864f; }
            else if (Interior == 4) { InteriorName = "apa_v_mp_h_02_a"; EingangX = -787.0749f; EingangY = 315.8198f; EingangZ = 217.6386f; }
            else if (Interior == 5) { InteriorName = "apa_v_mp_h_02_c"; EingangX = -786.8195f; EingangY = 315.5634f; EingangZ = 187.9137f; }
            else if (Interior == 6) { InteriorName = "apa_v_mp_h_02_b"; EingangX = -774.1382f; EingangY = 342.0316f; EingangZ = 196.6864f; }
            else if (Interior == 7) { InteriorName = "apa_v_mp_h_03_a"; EingangX = -786.6245f; EingangY = 315.6175f; EingangZ = 217.6385f; }
            else if (Interior == 8) { InteriorName = "apa_v_mp_h_03_c"; EingangX = -786.9584f; EingangY = 315.7974f; EingangZ = 187.9135f; }
            else if (Interior == 9) { InteriorName = "apa_v_mp_h_03_b"; EingangX = -774.0223f; EingangY = 342.1718f; EingangZ = 196.6863f; }
            else if (Interior == 10) { InteriorName = "apa_v_mp_h_04_a"; EingangX = -787.0902f; EingangY = 315.7039f; EingangZ = 217.6384f; }
            else if (Interior == 11) { InteriorName = "apa_v_mp_h_04_c"; EingangX = -787.0155f; EingangY = 315.7071f; EingangZ = 187.9135f; }
            else if (Interior == 12) { InteriorName = "apa_v_mp_h_04_b"; EingangX = -773.8976f; EingangY = 342.1525f; EingangZ = 196.6863f; }
            else if (Interior == 13) { InteriorName = "apa_v_mp_h_05_a"; EingangX = -786.9887f; EingangY = 315.7393f; EingangZ = 217.6386f; }
            else if (Interior == 14) { InteriorName = "apa_v_mp_h_05_c"; EingangX = -786.8809f; EingangY = 315.6634f; EingangZ = 187.9136f; }
            else if (Interior == 15) { InteriorName = "apa_v_mp_h_05_b"; EingangX = -774.0675f; EingangY = 342.0773f; EingangZ = 196.6864f; }
            else if (Interior == 16) { InteriorName = "apa_v_mp_h_06_a"; EingangX = -787.1423f; EingangY = 315.6943f; EingangZ = 217.6384f; }
            else if (Interior == 17) { InteriorName = "apa_v_mp_h_06_c"; EingangX = -787.0961f; EingangY = 315.815f; EingangZ = 187.9135f; }
            else if (Interior == 18) { InteriorName = "apa_v_mp_h_06_b"; EingangX = -773.9552f; EingangY = 341.9892f; EingangZ = 196.6862f; }
            else if (Interior == 19) { InteriorName = "apa_v_mp_h_07_a"; EingangX = -787.029f; EingangY = 315.7113f; EingangZ = 217.6385f; }
            else if (Interior == 20) { InteriorName = "apa_v_mp_h_07_c"; EingangX = -787.0574f; EingangY = 315.6567f; EingangZ = 187.9135f; }
            else if (Interior == 21) { InteriorName = "apa_v_mp_h_07_b"; EingangX = -774.0109f; EingangY = 342.0965f; EingangZ = 196.6863f; }
            else if (Interior == 22) { InteriorName = "apa_v_mp_h_08_a"; EingangX = -786.9469f; EingangY = 315.5655f; EingangZ = 217.6383f; }
            else if (Interior == 23) { InteriorName = "apa_v_mp_h_08_c"; EingangX = -786.9756f; EingangY = 315.723f; EingangZ = 187.9134f; }
            else if (Interior == 24) { InteriorName = "apa_v_mp_h_08_b"; EingangX = -774.0349f; EingangY = 342.0296f; EingangZ = 196.6862f; }
            else if (Interior == 25) { InteriorName = "low_budged"; EingangX = 318.2038f; EingangY = -2041.698f; EingangZ = -4.74618f; }


            //Ein neues Objekt erzeugen
            var Haus = new Immobilien
            {
                ImmobilienBeschreibung = "Zu verkaufen",
                ImmobilienBesitzer = 0,
                ImmobilienGeld = 0,
                ImmobilienAbgeschlossen = 0,
                ImmobilienKaufpreis = Kaufpreis,
                ImmobilienInteriorName = InteriorName,
                ImmobilienEingangX = EingangX,
                ImmobilienEingangY = EingangY,
                ImmobilienEingangZ = EingangZ,
                ImmobilienX = Player.Position.X,
                ImmobilienY = Player.Position.Y,
                ImmobilienZ = Player.Position.Z
            };

            //Query absenden
            ContextFactory.Instance.srp_immobilien.Add(Haus);
            ContextFactory.Instance.SaveChanges();


            //Immobilie zu der Lokalen Liste hinzufügen
            ImmobilienLokal haus = new ImmobilienLokal();

            haus.Id = ContextFactory.Instance.srp_immobilien.Max(x => x.Id);
            haus.ImmobilienBeschreibung = "Zu verkaufen";
            haus.ImmobilienBesitzer = 0;
            haus.ImmobilienGeld = 0;
            haus.ImmobilienAbgeschlossen = 0;
            haus.ImmobilienKaufpreis = Kaufpreis;
            haus.ImmobilienInteriorName = InteriorName;
            haus.ImmobilienEingangX = EingangX;
            haus.ImmobilienEingangY = EingangY;
            haus.ImmobilienEingangZ = EingangZ;
            haus.ImmobilienX = Player.Position.X;
            haus.ImmobilienY = Player.Position.Y;
            haus.ImmobilienZ = Player.Position.Z;


            String ImmobilienText = "~g~[~w~Hausnummer: " + haus.Id + "~g~]~n~";
            ImmobilienText += "~w~Kaufpreis: " + Funktionen.GeldFormatieren(haus.ImmobilienKaufpreis) + "~n~";
            ImmobilienText += "~w~Beschreibung: " + haus.ImmobilienBeschreibung;


            //TextLabel, Marker, Blip erstellen
            haus.ImmobilienLabel = NAPI.TextLabel.CreateTextLabel(ImmobilienText, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            haus.ImmobilienMarker = NAPI.Marker.CreateMarker(GlobaleSachen.ImmobilienMarker, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            haus.ImmobilienBlip = NAPI.Blip.CreateBlip(new Vector3(haus.ImmobilienX, haus.ImmobilienY, haus.ImmobilienZ));
            haus.ImmobilienBlip.Name = haus.ImmobilienBeschreibung;
            haus.ImmobilienBlip.ShortRange = true;
            haus.ImmobilienBlip.Sprite = 40;
            haus.ImmobilienBlip.Color = 2;

            //Immobilie in der Liste Lokal speichern
            Funktionen.ImmobilienListe.Add(haus);

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Haus erstellt");
        }

        [Command("247erstellen", "Nutze: /247erstellen [Kaufpreis]")]
        public void SupermarktErstellen(Client Player, long Kaufpreis)
        {
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.SupermarktErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Ein neues Objekt erzeugen
            var Supermarkt = new Supermarkt
            {
                SupermarktBeschreibung = "Zu verkaufen",
                SupermarktBesitzer = 0,
                SupermarktGeld = 0,
                SupermarktKaufpreis = Kaufpreis,
                SupermarktX = Player.Position.X,
                SupermarktY = Player.Position.Y,
                SupermarktZ = Player.Position.Z
            };

            //Query absenden
            ContextFactory.Instance.srp_supermärkte.Add(Supermarkt);
            ContextFactory.Instance.SaveChanges();


            //Supermarkt zu der Lokalen Liste hinzufügen
            SupermarktLokal supermarkt = new SupermarktLokal();

            supermarkt.Id = ContextFactory.Instance.srp_supermärkte.Max(x => x.Id);
            supermarkt.SupermarktBeschreibung = "Zu verkaufen";
            supermarkt.SupermarktBesitzer = 0;
            supermarkt.SupermarktGeld = 0;
            supermarkt.SupermarktKaufpreis = Kaufpreis;
            supermarkt.SupermarktX = Player.Position.X;
            supermarkt.SupermarktY = Player.Position.Y;
            supermarkt.SupermarktZ = Player.Position.Z;


            String SupermarktText = "~g~[~w~24/7 ID: " + supermarkt.Id + "~g~]~n~";
            SupermarktText += "~w~Kaufpreis: " + Funktionen.GeldFormatieren(supermarkt.SupermarktKaufpreis) + "~n~";
            SupermarktText += "~w~Beschreibung: " + supermarkt.SupermarktBeschreibung;


            //TextLabel, Marker, Blip erstellen
            supermarkt.SupermarktLabel = NAPI.TextLabel.CreateTextLabel(SupermarktText, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            supermarkt.SupermarktMarker = NAPI.Marker.CreateMarker(GlobaleSachen.SupermarktMarker, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            supermarkt.SupermarktBlip = NAPI.Blip.CreateBlip(new Vector3(supermarkt.SupermarktX, supermarkt.SupermarktY, supermarkt.SupermarktZ));
            supermarkt.SupermarktBlip.Name = supermarkt.SupermarktBeschreibung;
            supermarkt.SupermarktBlip.ShortRange = true;
            supermarkt.SupermarktBlip.Sprite = 52;
            supermarkt.SupermarktBlip.Color = 2;

            //Supermarkt in der Liste Lokal speichern
            Funktionen.SupermarktListe.Add(supermarkt);

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Supermarkt erstellt");
        }

        [Command("aherstellen", "Nutze: /aherstellen [Beschreibung] [Kaufpreis]")]
        public void AutohausErstellen(Client Player, String Beschreibung, long Kaufpreis)
        {
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AutohausErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Ein neues Objekt erzeugen
            var Autohaus = new Autohaus
            {
                AutohausBeschreibung = Beschreibung,
                AutohausBesitzer = 0,
                AutohausGeld = 0,
                AutohausKaufpreis = Kaufpreis,
                AutohausX = Player.Position.X,
                AutohausY = Player.Position.Y,
                AutohausZ = Player.Position.Z
            };

            //Query absenden
            ContextFactory.Instance.srp_autohäuser.Add(Autohaus);
            ContextFactory.Instance.SaveChanges();


            //Autohaus zu der Lokalen Liste hinzufügen
            AutohausLokal autohaus = new AutohausLokal();

            autohaus.Id = ContextFactory.Instance.srp_autohäuser.Max(x => x.Id);
            autohaus.AutohausBeschreibung = Beschreibung;
            autohaus.AutohausBesitzer = 0;
            autohaus.AutohausGeld = 0;
            autohaus.AutohausKaufpreis = Kaufpreis;
            autohaus.AutohausX = Player.Position.X;
            autohaus.AutohausY = Player.Position.Y;
            autohaus.AutohausZ = Player.Position.Z;


            String AutohausText = "~g~[~w~Autohaus ID: " + autohaus.Id + "~g~]~n~";
            AutohausText += "~w~Kaufpreis: " + Funktionen.GeldFormatieren(autohaus.AutohausKaufpreis) + "~n~";
            AutohausText += "~w~Beschreibung: " + autohaus.AutohausBeschreibung;


            //TextLabel, Marker, Blip erstellen
            autohaus.AutohausLabel = NAPI.TextLabel.CreateTextLabel(AutohausText, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            autohaus.AutohausMarker = NAPI.Marker.CreateMarker(GlobaleSachen.SupermarktMarker, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            autohaus.AutohausBlip = NAPI.Blip.CreateBlip(new Vector3(autohaus.AutohausX, autohaus.AutohausY, autohaus.AutohausZ));
            autohaus.AutohausBlip.Name = autohaus.AutohausBeschreibung;
            autohaus.AutohausBlip.ShortRange = true;
            autohaus.AutohausBlip.Sprite = 523;
            autohaus.AutohausBlip.Color = 2;

            //Autohaus in der Liste Lokal speichern
            Funktionen.AutohausListe.Add(autohaus);

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Autohaus erstellt");
        }

        [Command("tbeschreibung", "Nutze: /tbeschreibung [Id] [Text]", GreedyArg = true)]
        public void TankstelleBeschreibung(Client Player, int Id, String Beschreibung)
        {
            //Benötigte Abfragen
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankeBeschreibung) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Id < 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Id kann nicht kleiner als 0 sein."); return; }
            if(Beschreibung.Length > 20) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Beschreibung ist zu lang."); return; }

            TankstelleLokal tanke = new TankstelleLokal();
            tanke = Funktionen.TankeVonIdBekommen(Id);

            if(tanke != null)
            {
                tanke.TankstelleBeschreibung = Beschreibung;
                tanke.TankstelleGeändert = true;
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Tankstelle konnte nicht gefunden werden.");
            }

            //Erfolgreich Nachricht
            NAPI.Notification.SendNotificationToPlayer(Player, "~g~Info~w~: Die Beschreibung der Tankstelle " + Id + " wurde erfolgreich geändert.");

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Tankstellen Beschreibung geändert");
        }

        [Command("ahbeschreibung", "Nutze: /ahbeschreibung [Id] [Text]", GreedyArg = true)]
        public void AutohausBeschreibung(Client Player, int Id, String Beschreibung)
        {
            //Benötigte Abfragen
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AutohausBeschreibung) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Id < 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Id kann nicht kleiner als 0 sein."); return; }
            if (Beschreibung.Length > 20) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Beschreibung ist zu lang."); return; }

            AutohausLokal autohaus = new AutohausLokal();
            autohaus = Funktionen.AutohausVonIdBekommen(Id);

            if (autohaus != null)
            {
                autohaus.AutohausBeschreibung = Beschreibung;
                autohaus.AutohausGeändert = true;
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Autohaus konnte nicht gefunden werden.");
            }

            //Erfolgreich Nachricht
            NAPI.Notification.SendNotificationToPlayer(Player, "~g~Info~w~: Die Beschreibung des Autohauses " + Id + " wurde erfolgreich geändert.");

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Autohaus Beschreibung geändert");
        }

        [Command("tkaufpreis", "Nutze: /tkaufpreis [Id] [Kaufpreis]")]
        public void TankstellenKaufPreis(Client Player, int Id, long Kaufpreis)
        {
            //Benötigte Abfragen
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankeKaufpreis) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Id < Kaufpreis) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Kaufpreis kann nicht kleiner als 0 sein."); return; }
            

            TankstelleLokal tanke = new TankstelleLokal();
            tanke = Funktionen.TankeVonIdBekommen(Id);

            if (tanke != null)
            {
                tanke.TankstelleKaufpreis = Kaufpreis;
                tanke.TankstelleGeändert = true;
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Tankstelle konnte nicht gefunden werden.");
            }

            //Erfolgreich Nachricht
            NAPI.Notification.SendNotificationToPlayer(Player, "~g~Info~w~: Die Beschreibung der Tankstelle " + Id + " wurde erfolgreich geändert.");

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Tankstellen Kaufpreis geändert");
        }

        [Command("testdlc", "Nutze: /testdlc")]
        public void testdlc(Client Player)
        {
            Player.TriggerEvent("TestDLC");
        }

        [Command("terstellen", "Nutze: /terstellen [Kaufpreis]")]
        public void TankstelleErstellen(Client Player, long Kaufpreis)
        {
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankeErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Ein neues Objekt erzeugen
            var Tankstelle = new Tankstelle
            {
                TankstelleBeschreibung = "Zu verkaufen",
                TankstelleBesitzer = 0,
                TankstelleGeld = 0,
                TankstelleKaufpreis = Kaufpreis,
                TankstelleDiesel = 2000,
                TankstelleE10 = 2000,
                TankstelleSuper = 2000,
                TankstelleDieselPreis = 2,
                TankstelleE10Preis = 2,
                TankstelleSuperPreis = 2,
                TankstelleX = Player.Position.X,
                TankstelleY = Player.Position.Y,
                TankstelleZ = Player.Position.Z
            };

            //Query absenden
            ContextFactory.Instance.srp_tankstellen.Add(Tankstelle);
            ContextFactory.Instance.SaveChanges();

            //Tanke zu der Lokalen Liste hinzufügen
            TankstelleLokal tanke = new TankstelleLokal();

            tanke.Id = ContextFactory.Instance.srp_tankstellen.Max(x => x.Id);
            tanke.TankstelleBeschreibung = "Zu verkaufen";
            tanke.TankstelleBesitzer = 0;
            tanke.TankstelleGeld = 0;
            tanke.TankstelleKaufpreis = Kaufpreis;
            tanke.TankstelleDiesel = 2000;
            tanke.TankstelleE10 = 2000;
            tanke.TankstelleSuper = 2000;
            tanke.TankstelleDieselPreis = 2;
            tanke.TankstelleE10Preis = 2;
            tanke.TankstelleSuperPreis = 2;
            tanke.TankstelleX = Player.Position.X;
            tanke.TankstelleY = Player.Position.Y;
            tanke.TankstelleZ = Player.Position.Z;


            String TankstellenText = "~g~[~w~Tankstelle ID: " + tanke.Id + "~g~]~n~";
            TankstellenText += "~w~Kaufpreis: " + Funktionen.GeldFormatieren(Kaufpreis) + "~n~";
            TankstellenText += "Beschreibung: Zu verkaufen";


            //TextLabel, Marker, Blip erstellen
            tanke.TankstellenLabel = NAPI.TextLabel.CreateTextLabel(TankstellenText, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            tanke.TankstellenMarker = NAPI.Marker.CreateMarker(GlobaleSachen.TankstellenMarker, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            tanke.TankstellenBlip = NAPI.Blip.CreateBlip(new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z));
            tanke.TankstellenBlip.Name = "Zu verkaufen";
            tanke.TankstellenBlip.ShortRange = true;
            tanke.TankstellenBlip.Sprite = 361;
            tanke.TankstellenBlip.Color = 2;

            //Tanke in der Liste Lokal speichern
            Funktionen.TankenListe.Add(tanke);

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Tankstelle erstellt");
        }
        
        [Command("perstellen", "Nutze: /perstellen [Name] [Beschreibung]")]
        public void PedErstellen(Client Player, String Name, String Beschreibung)
        {
            int Gesperrt = 1;
            if(Gesperrt == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Befehl ist derzeit leider gesperrt."); return; }

            //Definitionen
            uint BotHash = NAPI.Util.GetHashKey(Name);

            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.PedErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Enum.IsDefined(typeof(PedHash), BotHash) == false) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieses Ped kennen wir leider nicht."); return; }

            //Ein neues Objekt erzeugen
            var Bot = new Bot
            {
                BotName = Name,
                BotBeschreibung = Beschreibung,
                BotX = Player.Position.X,
                BotY = Player.Position.Y,
                BotZ = Player.Position.X,
                BotKopf = Player.Rotation.Z,
                BotDimension = Player.Dimension
            };

            //Query absenden
            ContextFactory.Instance.srp_bots.Add(Bot);
            ContextFactory.Instance.SaveChanges();

            //Tanke zu der Lokalen Liste hinzufügen
            BotLokal bot = new BotLokal();

            bot.Id = ContextFactory.Instance.srp_bots.Max(x => x.Id);
            bot.BotName = Name;
            bot.BotBeschreibung = Beschreibung;
            bot.BotX = Player.Position.X;
            bot.BotY = Player.Position.Y;
            bot.BotZ = Player.Position.Z;
            bot.BotKopf = Player.Rotation.Z;
            bot.BotDimension = Player.Dimension;

            //Bot erstellen
            //bot.Bot = NAPI.Ped.CreatePed(NAPI.Util.PedNameToModel(Name), new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), Player.Rotation.Z, Player.Dimension);
            var SpielerPosition = new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z);
            var SpielerRotZ = new Vector3(0.0, 0.0, Player.Rotation.Z);
            NAPI.ClientEvent.TriggerClientEventForAll("boterstellen", Name, SpielerPosition.X, SpielerPosition.Y, SpielerPosition.Z, SpielerRotZ.Z);

            //Tanke in der Liste Lokal speichern
            Funktionen.BotListe.Add(bot);

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Bot erstellt");
        }

        [Command("plöschen", "Nutze: /plöschen")]
        public void PedLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.PedLöschen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            BotLokal bot = new BotLokal();
            bot = Funktionen.NahePedBekommen(Player);

            if (bot != null)
            {
                var Bot = ContextFactory.Instance.srp_bots.Where(x => x.Id == bot.Id).FirstOrDefault();

                Funktionen.BotListe.Remove(bot);
                bot.Bot.Delete();

                //Query absenden
                ContextFactory.Instance.srp_bots.Remove(Bot);

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Tankstelle gelöscht");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde kein Ped gefunden.");
            }
        }

        [Command("fvermerstellen", "Nutze: /fvermerstellen")]
        public void FahrzeugvermietungErstellen(Client Player)
        {
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FVermietungErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Ein neues Objekt erzeugen
            var Fverm = new Fahrzeugvermietungen
            {
                PositionX = Player.Position.X,
                PositionY = Player.Position.Y,
                PositionZ = Player.Position.Z
            };

            //Query absenden
            ContextFactory.Instance.srp_fahrzeugvermietungen.Add(Fverm);
            ContextFactory.Instance.SaveChanges();

            //Fahrzeugvermietung zu der Lokalen Liste hinzufügen
            FahrzeugvermietungenLokal fverm = new FahrzeugvermietungenLokal();

            fverm.Id = ContextFactory.Instance.srp_fahrzeugvermietungen.Max(x => x.Id);
            fverm.PositionX = Player.Position.X;
            fverm.PositionY = Player.Position.Y;
            fverm.PositionZ = Player.Position.Z;

            String fVermText = null;
            fVermText = "~g~[~w~Fahrzeugvermietung~g~]~n~";
            fverm.FVermietungBlip = NAPI.Blip.CreateBlip(new Vector3(fverm.PositionX, fverm.PositionY, fverm.PositionZ));
            fverm.FVermietungBlip.Name = "Fahrzeugvermietung";
            fverm.FVermietungBlip.ShortRange = true;
            fverm.FVermietungBlip.Sprite = 81;
            fverm.FVermietungBlip.Color = 17;

            //TextLabel und Marker erstellen
            fverm.FVermietungText = NAPI.TextLabel.CreateTextLabel(fVermText, new Vector3(fverm.PositionX, fverm.PositionY, fverm.PositionZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            fverm.FVermietungMarker = NAPI.Marker.CreateMarker(GlobaleSachen.ATMMarker, new Vector3(fverm.PositionX, fverm.PositionY, fverm.PositionZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Tanke in der Liste Lokal speichern
            Funktionen.FVermietungListe.Add(fverm);

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Fahrzeugvermietung erstellt");
        }

        [Command("fvermlöschen", "Nutze: /fvermlöschen")]
        public void FahrzeugvermietungLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FVermietungLöschen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            FahrzeugvermietungenLokal fverm = new FahrzeugvermietungenLokal();
            fverm = Funktionen.NaheFahrzeugvermietungBekommen(Player);

            if (fverm != null)
            {
                var FVerm = ContextFactory.Instance.srp_bankautomaten.Where(x => x.Id == fverm.Id).FirstOrDefault();

                Funktionen.FVermietungListe.Remove(fverm);
                fverm.FVermietungMarker.Delete();
                fverm.FVermietungBlip.Delete();
                fverm.FVermietungText.Delete();

                //Query absenden
                ContextFactory.Instance.srp_bankautomaten.Remove(FVerm);

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Fahrzeugvermietung gelöscht");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde keine Fahrzeugvermietung gefunden.");
            }
        }

        [Command("atmerstellen", "Nutze: /atmerstellen")]
        public void ATMErstellen(Client Player)
        {
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.ATMErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Ein neues Objekt erzeugen
            var Atm = new Bankautomaten
            {
                PositionX = Player.Position.X,
                PositionY = Player.Position.Y,
                PositionZ = Player.Position.Z
            };

            //Query absenden
            ContextFactory.Instance.srp_bankautomaten.Add(Atm);
            ContextFactory.Instance.SaveChanges();

            //Tanke zu der Lokalen Liste hinzufügen
            BankautomatenLokal atm = new BankautomatenLokal();

            atm.Id = ContextFactory.Instance.srp_bankautomaten.Max(x => x.Id);
            atm.PositionX = Player.Position.X;
            atm.PositionY = Player.Position.Y;
            atm.PositionZ = Player.Position.Z;

            String ATMText = null;
            ATMText = "~g~[~w~ATM~g~]~n~";
            atm.ATMBlip = NAPI.Blip.CreateBlip(new Vector3(atm.PositionX, atm.PositionY, atm.PositionZ));
            atm.ATMBlip.Name = "ATM";
            atm.ATMBlip.ShortRange = true;
            atm.ATMBlip.Sprite = 277;

            //TextLabel und Marker erstellen
            atm.ATMText = NAPI.TextLabel.CreateTextLabel(ATMText, new Vector3(atm.PositionX, atm.PositionY, atm.PositionZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            atm.ATMMarker = NAPI.Marker.CreateMarker(GlobaleSachen.ATMMarker, new Vector3(atm.PositionX, atm.PositionY, atm.PositionZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Tanke in der Liste Lokal speichern
            Funktionen.ATMListe.Add(atm);

            //Log Eintrag
            Funktionen.LogEintrag(Player, "ATM erstellt");
        }

        [Command("rserstellen", "Nutze: /rserstellen")]
        public void ATMErstellen(Client Player, String rsName)
        {
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.RandomSpawnErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Ein neues Objekt erzeugen
            var Rs = new RandomSpawns
            {
                Name = rsName,
                PosX = Player.Vehicle.Position.X,
                PosY = Player.Vehicle.Position.Y,
                PosZ = Player.Vehicle.Position.Z,
                RotZ = Player.Vehicle.Position.Z
            };

            //Query absenden
            ContextFactory.Instance.srp_randomspawns.Add(Rs);
            ContextFactory.Instance.SaveChanges();

            //Tanke zu der Lokalen Liste hinzufügen
            RandomSpawns rs = new RandomSpawns();

            rs.Id = ContextFactory.Instance.srp_randomspawns.Max(x => x.Id);
            rs.Name = rsName;
            rs.PosX = Player.Vehicle.Position.X;
            rs.PosY = Player.Vehicle.Position.Y;
            rs.PosZ = Player.Vehicle.Position.Z;
            rs.RotZ = Player.Vehicle.Position.Z;

            //Tanke in der Liste Lokal speichern
            Funktionen.RandomSpawnListe.Add(rs);

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Randomspawn erstellt");
        }

        [Command("atmlöschen", "Nutze: /atmlöschen")]
        public void ATMLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.ATMLöschen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            BankautomatenLokal atm = new BankautomatenLokal();
            atm = Funktionen.NaheBankautomatenBekommen(Player);

            if (atm != null)
            {
                var Atm = ContextFactory.Instance.srp_bankautomaten.Where(x => x.Id == atm.Id).FirstOrDefault();

                Funktionen.ATMListe.Remove(atm);
                atm.ATMMarker.Delete();
                atm.ATMBlip.Delete();
                atm.ATMText.Delete();

                //Query absenden
                ContextFactory.Instance.srp_bankautomaten.Remove(Atm);

                //Log Eintrag
                Funktionen.LogEintrag(Player, "ATM gelöscht");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde kein ATM gefunden.");
            }
        }

        [Command("tpunkterstellen", "Nutze: /tpunkterstellen [TankstellenID]")]
        public void TankstellenPunktErstellen(Client Player, int Id)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankPunktErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Query durch die Tankstellen
            var Check = ContextFactory.Instance.srp_tankstellen.Count(x => x.Id == Id);

            //Prüfen ob die Tankstelle existiert
            if (Check == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Tankstelle ist uns nicht bekannt."); return; }

            //Ein neues Objekt erzeugen
            var Tankpunkt = new TankstellenPunkt
            {
                TankstellenId = Id,
                TankstellenPunktX = Player.Position.X,
                TankstellenPunktY = Player.Position.Y,
                TankstellenPunktZ = Player.Position.Z
            };

            //Query absenden
            ContextFactory.Instance.srp_tankstellenpunkte.Add(Tankpunkt);
            ContextFactory.Instance.SaveChanges();

            //Tanke zu der Lokalen Liste hinzufügen
            TankstellenPunktLokal tankstellenpunkt = new TankstellenPunktLokal();

            tankstellenpunkt.Id = ContextFactory.Instance.srp_tankstellenpunkte.Max(x => x.Id);
            tankstellenpunkt.TankstellenId = Id;
            tankstellenpunkt.TankstellenPunktX = Player.Position.X;
            tankstellenpunkt.TankstellenPunktY = Player.Position.Y;
            tankstellenpunkt.TankstellenPunktZ = Player.Position.Z;


            String TankstellenPunktText = "~g~[~w~Zapfsäule ID: " + tankstellenpunkt.Id + "~g~]~n~";
            TankstellenPunktText += "~w~Tankstelle: ~g~" + tankstellenpunkt.TankstellenId;


            //TextLabel und Marker erstellen
            tankstellenpunkt.TankstellenPunktLabel = NAPI.TextLabel.CreateTextLabel(TankstellenPunktText, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            tankstellenpunkt.TankstellenPunktMarker = NAPI.Marker.CreateMarker(GlobaleSachen.TankstellenZapfsäuleMarker, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Tanke in der Liste Lokal speichern
            Funktionen.TankenPunktListe.Add(tankstellenpunkt);

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Tankstellen Punkt erstellt");
        }

        [Command("tinfoerstellen", "Nutze: /tinfoerstellen [TankstellenID]")]
        public void TankstellenInfoErstellen(Client Player, int Id)
        {
            //Benötigte Definitionen
            float Diesel = 0.0f, E10 = 0.0f, Super = 0.0f;

            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankInfoErstellen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Query durch die Tankstellen
            var Check = ContextFactory.Instance.srp_tankstellen.Count(x => x.Id == Id);

            //Prüfen ob die Tankstelle existiert
            if (Check == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Tankstelle ist uns nicht bekannt."); return; }

            //Ein neues Objekt erzeugen
            var TankInfo = new TankstellenInfo
            {
                TankstellenInfoId = Id,
                TankstellenInfoX = Player.Position.X,
                TankstellenInfoY = Player.Position.Y,
                TankstellenInfoZ = Player.Position.Z
            };

            //Query absenden
            ContextFactory.Instance.srp_tankstelleninfo.Add(TankInfo);
            ContextFactory.Instance.SaveChanges();

            //Tanke zu der Lokalen Liste hinzufügen
            TankstellenInfoLokal tankinfo = new TankstellenInfoLokal();

            tankinfo.Id = ContextFactory.Instance.srp_tankstelleninfo.Max(x => x.Id);
            tankinfo.TankstellenInfoId = Id;
            tankinfo.TankstellenInfoX = Player.Position.X;
            tankinfo.TankstellenInfoY = Player.Position.Y;
            tankinfo.TankstellenInfoZ = Player.Position.Z;

            foreach (TankstelleLokal tankstelle in Funktionen.TankenListe)
            {
                if(tankstelle.Id == Id)
                {
                    Diesel = tankstelle.TankstelleDiesel;
                    E10 = tankstelle.TankstelleE10;
                    Super = tankstelle.TankstelleSuper;
                }
            }

            String TankstellenInfoText = "~g~[~w~Für ID: " + Id + "~g~]~n~";
            TankstellenInfoText += "~w~Diesel: ~g~" + Diesel + "~w~L~n~";
            TankstellenInfoText += "~w~E10: ~g~" + E10 + "~w~L~n~";
            TankstellenInfoText += "~w~Super: ~g~" + Super + "~w~L";


            //TextLabel und Marker erstellen
            tankinfo.TankstellenInfoLabel = NAPI.TextLabel.CreateTextLabel(TankstellenInfoText, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            tankinfo.TankstellenInfoMarker = NAPI.Marker.CreateMarker(GlobaleSachen.TankstellenInfoMarker, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Tanke in der Liste Lokal speichern
            Funktionen.TankenInfoListe.Add(tankinfo);

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Tankstellen Info Punkt erstellt");
        }

        [Command("tlöschen", "Nutze: /tlöschen")]
        public void TankstelleLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankeLöschen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            TankstelleLokal tanke = new TankstelleLokal();
            tanke = Funktionen.NaheTankeBekommen(Player);

            if(tanke != null)
            {
                var Tankstelle = ContextFactory.Instance.srp_tankstellen.Where(x => x.Id == tanke.Id).FirstOrDefault();
                tanke.TankstellenLabel.Delete();
                tanke.TankstellenMarker.Delete();
                tanke.TankstellenBlip.Delete();
                Funktionen.TankenListe.Remove(tanke);

                //Query absenden
                ContextFactory.Instance.srp_tankstellen.Remove(Tankstelle);

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Tankstelle gelöscht");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde keine Tankstelle gefunden.");
            }
        }

        [Command("247löschen", "Nutze: /247löschen")]
        public void SupermarktLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.SupermarktLöschen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            SupermarktLokal supermarkt = new SupermarktLokal();
            supermarkt = Funktionen.NaheSupermarktBekommen(Player);

            if (supermarkt != null)
            {
                var Supermarkt = ContextFactory.Instance.srp_supermärkte.Where(x => x.Id == supermarkt.Id).FirstOrDefault();
                supermarkt.SupermarktLabel.Delete();
                supermarkt.SupermarktMarker.Delete();
                supermarkt.SupermarktBlip.Delete();
                Funktionen.SupermarktListe.Remove(supermarkt);

                //Query absenden
                ContextFactory.Instance.srp_supermärkte.Remove(Supermarkt);

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Supermarkt gelöscht");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde kein 24/7 gefunden.");
            }
        }

        [Command("ahlöschen", "Nutze: /ahlöschen")]
        public void AutohausLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AutohausLöschen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            AutohausLokal autohaus = new AutohausLokal();
            autohaus = Funktionen.NaheAutohausBekommen(Player);

            if (autohaus != null)
            {
                var Autohaus = ContextFactory.Instance.srp_autohäuser.Where(x => x.Id == autohaus.Id).FirstOrDefault();
                autohaus.AutohausLabel.Delete();
                autohaus.AutohausMarker.Delete();
                autohaus.AutohausBlip.Delete();
                Funktionen.AutohausListe.Remove(autohaus);

                //Query absenden
                ContextFactory.Instance.srp_autohäuser.Remove(Autohaus);

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Autohaus gelöscht");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde kein Autohaus gefunden.");
            }
        }

        [Command("hlöschen", "Nutze: /hlöschen")]
        public void ImmobilieLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.HausLöschen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            ImmobilienLokal haus = new ImmobilienLokal();
            haus = Funktionen.NaheImmobilieBekommen(Player);

            if (haus != null)
            {
                var Immobilie = ContextFactory.Instance.srp_immobilien.Where(x => x.Id == haus.Id).FirstOrDefault();
                haus.ImmobilienLabel.Delete();
                haus.ImmobilienMarker.Delete();
                haus.ImmobilienBlip.Delete();
                Funktionen.ImmobilienListe.Remove(haus);

                //Query absenden
                ContextFactory.Instance.srp_immobilien.Remove(Immobilie);

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Immobilie gelöscht");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde keine Immobilie gefunden.");
            }
        }

        [Command("hint", "Nutze: /hint [Interior 1 - 24]")]
        public void ImmobilieInterior(Client Player, int Interior)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.HausInterior) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Interior <= 0 || Interior > 24) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieses Interior ist uns nicht bekannt."); return; }

            ImmobilienLokal haus = new ImmobilienLokal();
            haus = Funktionen.NaheImmobilieBekommen(Player);

            if (haus != null)
            {
                //Benötigte Definitionen
                String InteriorName = null;
                float EingangX = 0.0f, EingangY = 0.0f, EingangZ = 0.0f;

                //Interior Liste
                if (Interior == 1) { InteriorName = "apa_v_mp_h_01_a"; EingangX = -786.8663f; EingangY = 315.7642f; EingangZ = 217.6385f; }
                else if (Interior == 2) { InteriorName = "apa_v_mp_h_01_c"; EingangX = -786.9563f; EingangY = 315.6229f; EingangZ = 187.9136f; }
                else if (Interior == 3) { InteriorName = "apa_v_mp_h_01_b"; EingangX = -774.0126f; EingangY = 342.0428f; EingangZ = 196.6864f; }
                else if (Interior == 4) { InteriorName = "apa_v_mp_h_02_a"; EingangX = -787.0749f; EingangY = 315.8198f; EingangZ = 217.6386f; }
                else if (Interior == 5) { InteriorName = "apa_v_mp_h_02_c"; EingangX = -786.8195f; EingangY = 315.5634f; EingangZ = 187.9137f; }
                else if (Interior == 6) { InteriorName = "apa_v_mp_h_02_b"; EingangX = -774.1382f; EingangY = 342.0316f; EingangZ = 196.6864f; }
                else if (Interior == 7) { InteriorName = "apa_v_mp_h_03_a"; EingangX = -786.6245f; EingangY = 315.6175f; EingangZ = 217.6385f; }
                else if (Interior == 8) { InteriorName = "apa_v_mp_h_03_c"; EingangX = -786.9584f; EingangY = 315.7974f; EingangZ = 187.9135f; }
                else if (Interior == 9) { InteriorName = "apa_v_mp_h_03_b"; EingangX = -774.0223f; EingangY = 342.1718f; EingangZ = 196.6863f; }
                else if (Interior == 10) { InteriorName = "apa_v_mp_h_04_a"; EingangX = -787.0902f; EingangY = 315.7039f; EingangZ = 217.6384f; }
                else if (Interior == 11) { InteriorName = "apa_v_mp_h_04_c"; EingangX = -787.0155f; EingangY = 315.7071f; EingangZ = 187.9135f; }
                else if (Interior == 12) { InteriorName = "apa_v_mp_h_04_b"; EingangX = -773.8976f; EingangY = 342.1525f; EingangZ = 196.6863f; }
                else if (Interior == 13) { InteriorName = "apa_v_mp_h_05_a"; EingangX = -786.9887f; EingangY = 315.7393f; EingangZ = 217.6386f; }
                else if (Interior == 14) { InteriorName = "apa_v_mp_h_05_c"; EingangX = -786.8809f; EingangY = 315.6634f; EingangZ = 187.9136f; }
                else if (Interior == 15) { InteriorName = "apa_v_mp_h_05_b"; EingangX = -774.0675f; EingangY = 342.0773f; EingangZ = 196.6864f; }
                else if (Interior == 16) { InteriorName = "apa_v_mp_h_06_a"; EingangX = -787.1423f; EingangY = 315.6943f; EingangZ = 217.6384f; }
                else if (Interior == 17) { InteriorName = "apa_v_mp_h_06_c"; EingangX = -787.0961f; EingangY = 315.815f; EingangZ = 187.9135f; }
                else if (Interior == 18) { InteriorName = "apa_v_mp_h_06_b"; EingangX = -773.9552f; EingangY = 341.9892f; EingangZ = 196.6862f; }
                else if (Interior == 19) { InteriorName = "apa_v_mp_h_07_a"; EingangX = -787.029f; EingangY = 315.7113f; EingangZ = 217.6385f; }
                else if (Interior == 20) { InteriorName = "apa_v_mp_h_07_c"; EingangX = -787.0574f; EingangY = 315.6567f; EingangZ = 187.9135f; }
                else if (Interior == 21) { InteriorName = "apa_v_mp_h_07_b"; EingangX = -774.0109f; EingangY = 342.0965f; EingangZ = 196.6863f; }
                else if (Interior == 22) { InteriorName = "apa_v_mp_h_08_a"; EingangX = -786.9469f; EingangY = 315.5655f; EingangZ = 217.6383f; }
                else if (Interior == 23) { InteriorName = "apa_v_mp_h_08_c"; EingangX = -786.9756f; EingangY = 315.723f; EingangZ = 187.9134f; }
                else if (Interior == 24) { InteriorName = "apa_v_mp_h_08_b"; EingangX = -774.0349f; EingangY = 342.0296f; EingangZ = 196.6862f; }

                haus.ImmobilienEingangX = EingangX;
                haus.ImmobilienEingangY = EingangY;
                haus.ImmobilienEingangZ = EingangZ;
                haus.ImmobilienInteriorName = InteriorName;

                haus.ImmobilieGeändert = true;

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Immobilie Interior geändert");

                //Nachricht
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Interior wurde angepasst.");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde keine Immobilie gefunden.");
            }
        }

        [Command("hkaufpreis", "Nutze: /hkaufpreis [Preis]")]
        public void ImmobilieKaufpreis(Client Player, long Preis)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.HausKaufPreis) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Preis < 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~:Das ist zu wenig Geld."); return; }

            ImmobilienLokal haus = new ImmobilienLokal();
            haus = Funktionen.NaheImmobilieBekommen(Player);

            if (haus != null)
            {
                haus.ImmobilienKaufpreis = Preis;
                haus.ImmobilieGeändert = true;

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Immobilie Kaufpreis geändert");

                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Kaufpreis wurde auf " + Funktionen.GeldFormatieren(Preis) + " geändert!");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde keine Immobilie gefunden.");
            }
        }

        [Command("htp", "Nutze: /htp [Hausnummer]")]
        public void ImmobilieTeleport(Client Player, int Hausnummer)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.HausTeleport) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Benötigte Definitionen
            int i = 0;

            foreach (ImmobilienLokal hauslocal in Funktionen.ImmobilienListe)
            {
                if(hauslocal.Id == Hausnummer)
                {
                    Player.Position = new Vector3(hauslocal.ImmobilienX, hauslocal.ImmobilienY, hauslocal.ImmobilienZ);
                    i = 1;
                }
            }

            if(i == 0)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieses Haus konnte nicht gefunden werden.");
            }
        }

        [Command("tpunktlöschen", "Nutze: /tpunktlöschen")]
        public void TankstellenPunktLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankPunktLöschen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            TankstellenPunktLokal tankenpunkt = new TankstellenPunktLokal();
            tankenpunkt = Funktionen.NaheTankePunktBekommen(Player);

            if (tankenpunkt != null)
            {
                var TankstellenPunkt = ContextFactory.Instance.srp_tankstellenpunkte.Where(x => x.Id == tankenpunkt.Id).FirstOrDefault();
                tankenpunkt.TankstellenPunktLabel.Delete();
                tankenpunkt.TankstellenPunktMarker.Delete();
                Funktionen.TankenPunktListe.Remove(tankenpunkt);

                //Query absenden
                ContextFactory.Instance.srp_tankstellenpunkte.Remove(TankstellenPunkt);

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Tankstellen Punkt gelöscht");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde keine Zapfsäule gefunden.");
            }
        }

        [Command("beenden", "Nutze: /beenden")]
        public void JobBeenden(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }

            //Benötigte Definitionen
            int Job = Funktionen.AccountJobBekommen(Player);

            if(Player.IsInVehicle == false)
            {
                if (Job == 1)
                {
                    if (Player.GetData("BerufskraftfahrerFahrzeug") == 1)
                    {
                        Fahrzeuge.JobFahrzeugLöschen(Player, Funktionen.AccountJobFahrzeugBekommen(Player));
                    }
                }
                if (Job == 2)
                {
                    if (Player.GetData("BusfahrerFahrzeug") == 1)
                    {
                        Fahrzeuge.JobFahrzeugLöschen(Player, Funktionen.AccountJobFahrzeugBekommen(Player));
                    }
                }
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das geht nur außerhalb eines Fahrzeugs.");
            }
        }

        [Command("tinfolöschen", "Nutze: /tinfolöschen")]
        public void TankstellenInfoLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankInfoLöschen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            TankstellenInfoLokal tankeninfo = new TankstellenInfoLokal();
            tankeninfo = Funktionen.NaheTankeInfoBekommen(Player);

            if (tankeninfo != null)
            {
                var TankstellenPunkt = ContextFactory.Instance.srp_tankstelleninfo.Where(x => x.Id == tankeninfo.Id).FirstOrDefault();
                tankeninfo.TankstellenInfoLabel.Delete();
                tankeninfo.TankstellenInfoMarker.Delete();
                Funktionen.TankenInfoListe.Remove(tankeninfo);

                //Query absenden
                ContextFactory.Instance.srp_tankstelleninfo.Remove(TankstellenPunkt);

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Tankstellen Info gelöscht");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde kein Info Punkt gefunden.");
            }
        }

        [Command("waffe")]
        public void AdminWaffe(Client Player, String weaponName, int ammo)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.Waffe) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Anhand des eingegeben Namens den WeaponHash ermitteln
            WeaponHash weapon = NAPI.Util.WeaponNameToModel(weaponName);

            if (weapon == 0)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Diese Waffe ist uns nicht bekannt.");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Waffe erhalten.");

                //Dem Spieler die gewünschte Waffe geben
                Player.GiveWeapon(weapon, ammo);

                //Log Eintrag
                Funktionen.LogEintrag(Player, weaponName + "gegeben");
            }
        }

        [Command("stats")]
        public void SpielerStats(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }

            NAPI.Notification.SendNotificationToPlayer(Player, "~w~[~r~Stats für " + Player.Name + "~w~]");
            NAPI.Notification.SendNotificationToPlayer(Player, "Social-Club: " + Funktionen.AccountSocialClubBekommen(Player));
            NAPI.Notification.SendNotificationToPlayer(Player, "Admin Level: " + Funktionen.AccountAdminLevelBekommen(Player));
            NAPI.Notification.SendNotificationToPlayer(Player, "Spielzeit: " + Funktionen.AccountSpielzeitBerechnen(Player));
            NAPI.Notification.SendNotificationToPlayer(Player, "Geld: " + Funktionen.GeldFormatieren(Funktionen.AccountGeldBekommen(Player)));
            NAPI.Notification.SendNotificationToPlayer(Player, "Bank Geld: " + Funktionen.GeldFormatieren(Funktionen.AccountBankGeldBekommen(Player)));
            NAPI.Notification.SendNotificationToPlayer(Player, "Fahrzeug Schlüssel: " + Funktionen.AccountFahrzeugSchlüsselBekommen(Player));
            NAPI.Notification.SendNotificationToPlayer(Player, "Job: " + Funktionen.AccountJobInNamen(Player));
        }

        [Command("fparken", "Nutze /fparken")]
        public void FahrzeugParken(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugParken) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Überprüfen ob der Spieler in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                //Die Datenbank ID des Fahrzeuges bekommen
                int FahrzeugID = Player.Vehicle.GetData("Id");

                AutoLokal auto = new AutoLokal();
                auto = Funktionen.AutoBekommen(Player.Vehicle);

                auto.FahrzeugX = Player.Vehicle.Position.X;
                auto.FahrzeugY = Player.Vehicle.Position.Y;
                auto.FahrzeugZ = Player.Vehicle.Position.Z;
                auto.FahrzeugRot = Player.Vehicle.Rotation.Z;

                auto.FahrzeugGeändert = true;
       
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Fahrzeug mit der ID: ~g~" + FahrzeugID + " ~w~wurde erfolgreich geparkt.");

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Fahrzeug ID: " + FahrzeugID + " geparkt");

                //Query absenden
                ContextFactory.Instance.SaveChanges();
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("heiraten", "Nutze /heiraten [Name]")]
        public void Heiraten(Client Player, String Spieler)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }

            if (Funktionen.AccountVerheiratetBekommen(Player) != "Nein") { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du bist bereits verheiratet."); return; }

            if (Player.Position.DistanceTo(new Vector3(-329.9244, 6150.168, 32.31319)) < 10.0f)
            {
                if(GlobaleSachen.Heiraten == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es heiratet bereits jemand."); return; }

                if (Funktionen.AccountGeldBekommen(Player) < GlobaleSachen.HeiratenPreis)
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast nicht genug Geld. (" + Funktionen.GeldFormatieren(GlobaleSachen.HeiratenPreis) + ")");
                    return;
                }

                //Den Spieler über den Namen ermitteln
                Client Spieler1 = NAPI.Player.GetPlayerFromName(Spieler);
                if (Spieler1 == null)
                {
                    if (Funktionen.SpielerSuchen(Spieler) == null)
                    {
                        NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieser Spieler konnte nicht gefunden werden.");
                        return;
                    }
                    else
                    {
                        Spieler1 = Funktionen.SpielerSuchen(Spieler);
                    }
                }
                if(Spieler1 != null)
                {
                    Spieler1 = Funktionen.SpielerSuchen(Spieler);
                    if (Spieler1 == Player) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du kannst dich nicht selbst heiraten!"); return; }
                    if (Funktionen.AccountVerheiratetBekommen(Spieler1) != "Nein") { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Spieler ist bereits verheiratet."); return; }
                    if (Spieler1.GetData("HeiratsId") != 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Spieler ist bereits am heiraten."); return; }
                    if (Spieler1.GetData("HeiratenId") != 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Spieler ist bereits am heiraten."); return; }

                    if (Spieler1.Position.DistanceTo(new Vector3(-329.9244, 6150.168, 32.31319)) < 10.0f)
                    {
                        GlobaleSachen.Heiraten = 1;
                        NAPI.Notification.SendNotificationToPlayer(Spieler1, "~y~Info~w~: Möchtest du " + Player.Name + " heiraten? Nutze /ja oder /nein");
                        Spieler1.SetData("HeiratsAntrag", 1);
                        Spieler1.SetData("HeiratsId", Player.GetData("Id"));
                        Player.SetData("HeiratenId", Spieler1.GetData("Id"));
                        Timer.SetTimer(() => Funktionen.HeiratenBeenden(Player), 80000, 1);

                        Funktionen.LogEintrag(Player, Spieler1 + " einen Antrag gemacht");

                        Funktionen.AccountGeldSetzen(Player, 2, GlobaleSachen.HeiratenPreis);
                    }
                    else
                    {
                        NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Spieler ist nicht am Heiraten Punkt.");
                    }
                }
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du bist nicht am Heiraten Punkt.");
            }

        }

        [Command("ja", "Nutze /ja")]
        public void HeiratenJa(Client Player)
        {
            if(Player.GetData("HeiratsAntrag") == 1)
            {
                //Benötigte Definitionen
                String HeiratsName = null;

                foreach (AccountLokal account in Funktionen.AccountListe)
                {
                    if (Player.GetData("HeiratsId") == account.Id)
                    {
                        account.Verheiratet = Player.Name;
                        HeiratsName = account.NickName;
                        account.AccountGeändert = true;
                    }
                }
                foreach (AccountLokal account in Funktionen.AccountListe)
                {
                    if (Player.GetData("Id") == account.Id)
                    {
                        account.Verheiratet = HeiratsName;
                        account.AccountGeändert = true;
                    }
                }

                foreach (var Spieler in NAPI.Pools.GetAllPlayers())
                {
                    if(Spieler.GetData("HeiratenId") == Player.GetData("Id"))
                    {
                        Spieler.SetData("HeiratenId", 0);
                        Player.SetData("HeiratsId", 0);
                        Player.SetData("HeiratsAntrag", 0);
                        Spieler.SetData("HeiratsAntrag", 0);
                    }
                }

                Client Spieler1 = NAPI.Player.GetPlayerFromName(HeiratsName);
                Funktionen.HeiratenSound();
                NAPI.Notification.SendNotificationToAll("~r~GLÜCKWUNSCH~w~: " + Spieler1.Name + " und " + Player.Name + " haben sich das Ja Wort gegeben!");
                Funktionen.LogEintrag(Player, "Antrag von " + Spieler1.Name + " angenommen");

            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast keinen Antrag erhalten.");
            }
            
        }
        
        [Command("nein", "Nutze /nein")]
        public void HeiratenNein(Client Player)
        {
            if (Player.GetData("HeiratsAntrag") == 1)
            {
                //Benötigte Definitionen
                String HeiratsName = null;

                foreach (AccountLokal account in Funktionen.AccountListe)
                {
                    if (Player.GetData("HeiratsId") == account.Id)
                    {
                        HeiratsName = account.NickName;
                    }
                }

                Client Spieler1 = NAPI.Player.GetPlayerFromName(HeiratsName);

                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast den Antrag von " + HeiratsName + " abgelehnt.");
                NAPI.Notification.SendNotificationToPlayer(Spieler1, "~y~Info~w~: " + Player.Name + " hat deinen Antrag abgelehnt.");

                Player.SetData("HeiratsAntrag", 0);
                Player.SetData("HeiratsId", 0);

                GlobaleSachen.Heiraten = 0;

            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast keinen Antrag erhalten.");
            }
        }

        [Command("fmietpreis", "Nutze /fmietpreis")]
        public void FahrzeugMietPreis(Client Player, long Mietpreis)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugMietPreis) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Mietpreis < 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Mietpreis muss mehr als 0 sein."); return; }

            //Überprüfen ob der Spieler in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                //Die Datenbank ID des Fahrzeuges bekommen
                int FahrzeugID = Player.Vehicle.GetData("Id");

                AutoLokal auto = new AutoLokal();
                auto = Funktionen.AutoBekommen(Player.Vehicle);

                if(auto.FahrzeugTyp != 2) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das ist kein Mietfahrzeug."); return; }

                auto.FahrzeugMietpreis = Mietpreis;
                
                auto.FahrzeugGeändert = true;
       
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Mietpreis für dieses Fahrzeug wurde auf " + Funktionen.GeldFormatieren(Mietpreis) + " gesetzt.");

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Fahrzeug ID: " + FahrzeugID + " Mietpreis geändert");

                //Query absenden
                ContextFactory.Instance.SaveChanges();
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("fkaufpreis", "Nutze /fkaufpreis")]
        public void FahrzeugKaufpreis(Client Player, long Kaufpreis)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugKaufPreis) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Kaufpreis < 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Kaufpreis muss mehr als 0 sein."); return; }

            //Überprüfen ob der Spieler in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                //Die Datenbank ID des Fahrzeuges bekommen
                int FahrzeugID = Player.Vehicle.GetData("Id");

                AutoLokal auto = new AutoLokal();
                auto = Funktionen.AutoBekommen(Player.Vehicle);

                if (auto.FahrzeugTyp != 5) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das ist kein verkaufbares Fahrzeug."); return; }

                auto.FahrzeugKaufpreis = Kaufpreis;

                auto.FahrzeugGeändert = true;

                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Kaufpreis für dieses Fahrzeug wurde auf " + Funktionen.GeldFormatieren(Kaufpreis) + " gesetzt.");

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Fahrzeug ID: " + FahrzeugID + " Kaufpreis geändert");

                //Query absenden
                ContextFactory.Instance.SaveChanges();
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("flöschen", "Nutze /flöschen")]
        public void FahrzeugLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugLöschen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Überprüfen ob der Spieler in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                AutoLokal auto = new AutoLokal();
                auto = Funktionen.AutoBekommen(Player.Vehicle);

                //Falls es ein Admin Fahrzeug ist
                if(auto.FahrzeugTyp == 0) { Funktionen.AdminFahrzeugLöschen(Player, Player.Vehicle); return; }

                //Damit es nicht wieder gespeichert wird
                auto.Fahrzeug.SetData("Id", 0);

                var Fahrzeug = ContextFactory.Instance.srp_fahrzeuge.Where(x => x.Id == auto.Id).FirstOrDefault();

                //Query absenden
                ContextFactory.Instance.srp_fahrzeuge.Remove(Fahrzeug);

                //Query absenden
                ContextFactory.Instance.SaveChanges();
                
                //Auto zerstören
                auto.Fahrzeug.Delete();

                //Von der lokalen Liste entfernen
                Funktionen.AutoListe.Remove(auto);

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Fahrzeug ID: " + auto.Id + " gelöscht");

            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("a", "Nutze /a [Text]", GreedyArg = true)]
        public void AdminChat(Client Player, String Text)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AdminChat) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            foreach (var Spieler in NAPI.Pools.GetAllPlayers())
            {
                if (Funktionen.AccountAdminLevelBekommen(Spieler) > 0)
                {
                    Spieler.SendChatMessage("~y~Adminchat ~w~" + Funktionen.AdminLevelName(Funktionen.AccountAdminLevelBekommen(Player)) + " " + Player.Name + ": " + Text);
                }
            }
        }

        [Command("tp", "Nutze: /tp [Spielername] [1 = Zu ihm, 2 = Zu mir]")]
        public void AdminTeleport(Client Player, String Spieler, int Typ)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.Teleporten) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Typ < 0 || Typ > 2) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Ungültige Eingabe."); return; }

            //Den Spieler über den Namen ermitteln
            Client Spieler1 = NAPI.Player.GetPlayerFromName(Spieler);
            if(Spieler1 == null)
            {
                if(Funktionen.SpielerSuchen(Spieler) == null)
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieser Spieler konnte nicht gefunden werden.");
                    return;
                }
                else
                {
                    Spieler1 = Funktionen.SpielerSuchen(Spieler);
                }
            }

            if(Typ == 1)
            {
                //Dem Spieler die Position setzen
                Player.Position = Spieler1.Position;

                //Nachricht an beide Spieler
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast dich zu " + Spieler1.Name + " teleportiert!");
                NAPI.Notification.SendNotificationToPlayer(Spieler1, "~y~Info~w~: " + Player.Name + " hat sich zu dir teleportiert!");

                //Log eintrag
                Funktionen.LogEintrag(Player, "Zu " + Spieler1.Name + " teleportiert");
            }
            else if(Typ == 2)
            {
                //Dem Spieler die Position setzen
                Spieler1.Position = Player.Position;

                //Nachricht an beide Spieler
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast " + Spieler1.Name + " zu dir teleportiert!");
                NAPI.Notification.SendNotificationToPlayer(Spieler1, "~y~Info~w~: " + Player.Name + " hat dich zu sich teleportiert!");

                //Log eintrag
                Funktionen.LogEintrag(Player, Spieler1.Name + " zu sich teleportiert");
            }
        }

        [Command("perso", "Nutze: /perso")]
        public void Perso(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountPersoBekommen(Player) == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast keinen Personalausweis."); return; }

            //Checken ob er gerade schon einen Perso sieht
            if (Player.GetData("SiehtPerso") == 1) { Player.TriggerEvent("persoschliessen"); Player.SetData("SiehtPerso", 0); }

            //Datum vom Spieler Abrufen und formatieren
            DateTime Datum = Funktionen.AccountEinreiseDatumBekommen(Player);
            String EinreiseDatumString = Datum.ToString("dd.MM.yyyy");

            DateTime Datum1 = Funktionen.AccountGeburtstagBekommen(Player);
            String GeburtstagString = Datum1.ToString("dd.MM.yyyy");

            //Den Perso anzeigen
            Player.TriggerEvent("persozeigen", Player.Name, GeburtstagString, EinreiseDatumString, Player.GetData("Id"));

            //Dem Spieler zuweisen das er gerade einen Perso sieht
            Player.SetData("SiehtPerso", 1);
        }

        [Command("tport", "Nutze: /tport [Name]")]
        public void AdminTeleports(Client Player, String Name)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.Teleporten) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Die Teleport Orte mit Abfrage
            if(Name == "Stadthalle" || Name == "stadthalle")
            {
                Player.Position = new Vector3(337.126, -1562.91, 30.298);
            }
            else if (Name == "Noobspawn" || Name == "noobspawn")
            {
                Player.Position = new Vector3(-3245.707, 967.6216, 12.73052);
            }
            else if (Name == "Arbeitsamt" || Name == "arbeitsamt")
            {
                Player.Position = new Vector3(250.553, -1594.62, 31.5322);
            }
            else if (Name == "Berufskraftfahrer" || Name == "berufskraftfahrer")
            {
                Player.Position = new Vector3(-1546.57, 1367.763, 126.1016);
            }
            else if(Name == "Kirche" || Name == "kirche")
            {
                Player.Position = new Vector3(-329.9244, 6150.168, 32.31319);
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieser Ort konnte nicht gefunden werden.");
            }
        }

        [Command("admingeben", "Nutze: /admingeben [Spielername] [AdminLevel]")]
        public void AdminGeben(Client Player, String Spieler, int Level)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            //if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AdminGeben) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Level < 0 || Level > 5) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Ungültige Eingabe."); return; }

            //Spieler über den Namen ermitteln
            Client Spieler1 = NAPI.Player.GetPlayerFromName(Spieler);
            if (Spieler1 == null)
            {
                if (Funktionen.SpielerSuchen(Spieler) == null)
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieser Spieler konnte nicht gefunden werden.");
                    return;
                }
                else
                {
                    Spieler1 = Funktionen.SpielerSuchen(Spieler);
                }
            }

            //Dem Spieler das Admin Level setzen
            Funktionen.AccountAdminLevelSetzen(Spieler1, Level);

            //Beiden eine Nachricht senden
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast " + Spieler1.Name + " Admin Level " + Level + " gegeben!");
            NAPI.Notification.SendNotificationToPlayer(Spieler1, "~y~Info~w~: Du hast von " + Player.Name + " Admin Level " + Level + " erhalten!");

            //Log eintrag
            Funktionen.LogEintrag(Player, Spieler1.Name + " Admin Level " + Level + " gegeben");
        }

        [Command("fraktionsetzen", "Nutze: /fraktionsetzen [Spielername] [FraktionsID]")]
        public void FraktionSetzen(Client Player, String Spieler, int Fraktion)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FraktionSetzen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Spieler über den Namen ermitteln
            Client Spieler1 = NAPI.Player.GetPlayerFromName(Spieler);
            if (Spieler1 == null)
            {
                if (Funktionen.SpielerSuchen(Spieler) == null)
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieser Spieler konnte nicht gefunden werden.");
                    return;
                }
                else
                {
                    Spieler1 = Funktionen.SpielerSuchen(Spieler);
                }
            }

            var Check = ContextFactory.Instance.srp_fraktionen.Count(x => x.Id == Fraktion);

            if(Check != 0)
            {
                var fraki = ContextFactory.Instance.srp_fraktionen.Where(x => x.Id == Fraktion).FirstOrDefault();

                //Beiden eine Nachricht senden
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast " + Spieler1.Name + " in die Fraktion " + fraki.FraktionName + " gesetzt.");
                NAPI.Notification.SendNotificationToPlayer(Spieler1, "~y~Info~w~: Du wurdest von " + Player.Name + " in die Fraktion " + fraki.FraktionName + " gesetzt.");

                //Dem Spieler die Fraktion setzen
                Funktionen.AccountFraktionSetzen(Spieler1, Fraktion);

                //Log eintrag
                Funktionen.LogEintrag(Player, Spieler1.Name + " in Fraktion " + fraki.FraktionName + " gesetzt.");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Diese Fraktion ist uns nicht bekannt.");
            }
        }

        [Command("geldsetzen", "Nutze: /geldsetzen [Spielername] [Geld]")]
        public void GeldSetzen(Client Player, String Spieler, long Geld)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.GeldGeben) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Den Spieler über den Namen ermitteln
            Client Spieler1 = NAPI.Player.GetPlayerFromName(Spieler);
            if (Spieler1 == null)
            {
                if (Funktionen.SpielerSuchen(Spieler) == null)
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieser Spieler konnte nicht gefunden werden.");
                    return;
                }
                else
                {
                    Spieler1 = Funktionen.SpielerSuchen(Spieler);
                }
            }

            //Dem Spieler das Admin Level setzen
            Funktionen.AccountGeldSetzen(Spieler1, 1, Geld);

            //Beiden eine Nachricht senden
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast " + Spieler1.Name + " " + Funktionen.GeldFormatieren(Geld) + " gegeben!");
            NAPI.Notification.SendNotificationToPlayer(Spieler1, "~y~Info~w~: Du hast von " + Funktionen.AdminLevelName(Funktionen.AccountAdminLevelBekommen(Player)) + " " + Player.Name + " " + Funktionen.GeldFormatieren(Geld) + " erhalten!");

            //Log eintrag
            Funktionen.LogEintrag(Player, Spieler1.Name + " " + Funktionen.GeldFormatieren(Geld) + " gegeben!");
        }

        [Command("freparieren", "Nutze: /freparieren")]
        public void FahrzeugReparieren(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugReparieren) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Checken ob er in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                //Log eintrag
                Funktionen.LogEintrag(Player, "Fahrzeug repariert");

                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Fahrzeug mit der ID: ~g~" + Player.Vehicle.GetData("Id") + " ~w~wurde erfolgreich repariert.");
                Player.Vehicle.Repair();
                Player.TriggerEvent("FahrzeugReparieren");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("fzuweisen", "Nutze: /fzuweisen [1 = Job, 2 = Fraktion, 3 = Autohaus] [Fraktions ID / Job ID / Autohaus ID]")]
        public void FahrzeugZuweisen(Client Player, int Typ, int Id = 0)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugZuweisen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Typ < 1 || Typ > 3) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Ungültiger Typ."); return; }

            //Checken ob er in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                //[Typ 0 = Admin, 1 = Job, 2 = Miet, 3 = Fraktion, 4 = Privat, 5 = Autohaus]
                if (Fahrzeuge.TypBekommen(Player.Vehicle) == 1 && Typ == 1)
                {
                    //Schauen ob der Job überhaupt existiert
                    if (Id < 0|| Id > Zahlen.Jobs) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Ungültiger Job."); return; }

                    //Dem Fahrzeug lokal die Werte mitgeben
                    Fahrzeuge.JobSetzen(Player.Vehicle, Id);

                    //Nachricht an den Spieler
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dem Fahrzeug wurde der Job zugewiesen.");

                    //Log eintrag
                    Funktionen.LogEintrag(Player, "Fahrzeug Job zugewiesen");
                }
                else if (Fahrzeuge.TypBekommen(Player.Vehicle) == 3 && Typ == 2)
                {
                    //Schauen ob die Fraktion überhaupt existiert
                    if (Id < 1 || Id > Zahlen.Fraktionen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Ungültige Fraktion."); return; }

                    //Dem Fahrzeug lokal die Werte mitgeben
                    Fahrzeuge.FraktionSetzen(Player.Vehicle, Id);

                    //Nachricht an den Spieler
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dem Fahrzeug wurde die Fraktion zugewiesen.");

                    //Log eintrag
                    Funktionen.LogEintrag(Player, "Fahrzeug Fraktion zugewiesen");
                }
                else if (Fahrzeuge.TypBekommen(Player.Vehicle) == 5 && Typ == 3)
                {
                    //Query durch die Autohäuser
                    var Check = ContextFactory.Instance.srp_autohäuser.Count(x => x.Id == Id);

                    //Prüfen ob das Autohaus existiert
                    if (Check == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieses Autohaus ist uns nicht bekannt."); return; }

                    //Dem Fahrzeug lokal die Werte mitgeben
                    Fahrzeuge.AutohausSetzen(Player.Vehicle, Id);

                    //Nachricht an den Spieler
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dem Fahrzeug wurde das Autohaus zugewiesen.");

                    //Log eintrag
                    Funktionen.LogEintrag(Player, "Fahrzeug Autohaus zugewiesen");
                }
                else
                {
                    //Fehlermessage falls der Typ der Verändert werden soll nicht zum Fahrzeug passt
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Da hat was nicht geklappt. Schaue auf das Kennzeichen um zu sehen was es für ein Fahrzeug ist.");
                }
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("fzuweisenprivat", "Nutze: /fzuweisenprivat [Name]")]
        public void FahrzeugZuweisenPrivat(Client Player, String Spieler)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugZuweisen) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Checken ob er in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                //Fahrzeug aus der Datenbank greifen
                int FahrzeugID = Player.Vehicle.GetData("Id");
                var Fahrzeug = ContextFactory.Instance.srp_fahrzeuge.Where(x => x.Id == FahrzeugID).FirstOrDefault();

                
                if (Fahrzeuge.TypBekommen(Player.Vehicle) == 4)
                {
                    //Schauen ob der Spieler überhaupt existiert
                    Client Spieler1 = NAPI.Player.GetPlayerFromName(Spieler);
                    if (Spieler1 == null)
                    {
                        if (Funktionen.SpielerSuchen(Spieler) == null)
                        {
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieser Spieler konnte nicht gefunden werden.");
                            return;
                        }
                        else
                        {
                            Spieler1 = Funktionen.SpielerSuchen(Spieler);
                        }
                    }

                    //Dem Fahrzeug den Job zuweisen
                    Fahrzeug.FahrzeugSpieler = Spieler1.GetData("Id");

                    //Dem Spieler und dem Sender eine Nachricht senden
                    NAPI.Notification.SendNotificationToPlayer(Spieler1, "~y~Info~w~: Der " + Funktionen.AdminLevelName(Funktionen.AccountAdminLevelBekommen(Player)) + " " + Player.Name + " hat dir das Fahrzeug " + Fahrzeuge.NameBekommen(Player.Vehicle) + " als Privatfahrzeug zugewiesen.");
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast " + Spieler1.Name + " das Fahrzeug " + Fahrzeuge.NameBekommen(Player.Vehicle) + " als Privatfahrzeug zugewiesen.");

                    //Dem Fahrzeug lokal die Werte mitgeben
                    Fahrzeuge.BesitzerSetzen(Player.Vehicle, Spieler1.GetData("Id"));

                    //Log eintrag
                    Funktionen.LogEintrag(Player, "Privat Fahrzeug zugewiesen");
                }
                else
                {
                    //Fehlermessage falls der Typ der Verändert werden soll nicht zum Fahrzeug passt
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Da hat was nicht geklappt. Schaue auf das Kennzeichen um zu sehen was es für ein Fahrzeug ist.");
                }

                //Query absenden
                ContextFactory.Instance.SaveChanges();
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }
        

        [Command("fporten", "Nutze: /fporten [ID]")]
        public void FahrzeugPorten(Client Player, int Id)
        {
            //Definierungen
            int i = 0;

            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugPorten) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Id < 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Ungültige ID."); return; }

            //Schleife durch die Fahrzeuge die neugespawnt werden sollen
            foreach (var Fahrzeuge in NAPI.Pools.GetAllVehicles())
            {
                if (Fahrzeuge.GetData("Id") == Id)
                {
                    i += 1;
                    Fahrzeuge.Position = new Vector3(Player.Position.X + 5, Player.Position.Y, Player.Position.Z);
                    Fahrzeuge.Rotation = new Vector3(0.0, 0.0, Player.Rotation.Z);
                }
            }

            //Log eintrag
            Funktionen.LogEintrag(Player, "Fahrzeug teleportiert");

            if (i == 0)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Fahrzeug konnte nicht gefunden werden.");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Fahrzeug wurde zu dir teleportiert.");
            }
        }

        [Command("speichern", "Nutze: /speichern")]
        public void Speichern(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.Speichern) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            NAPI.Notification.SendNotificationToAll("~g~Info~w~: Speicherung wurde eingeleitet.");
            NAPI.Notification.SendNotificationToAll("~g~Info~w~: Dies kann zu Verzögerungen führen!");

            //Stoppuhr starten
            var Uhr = System.Diagnostics.Stopwatch.StartNew();

            //Alles speichern
            Funktionen.AllesSpeichern();

            //StoppUhr anhalten
            Uhr.Stop();
            var MS = Uhr.ElapsedMilliseconds;

            //Ausgabe
            NAPI.Notification.SendNotificationToAll("~g~Info~w~: Der Gesamte Server wurde gespeichert. [" + MS + "ms]");
        }

        [Command("frespawn", "Nutze: /frespawn [Typ 0 = Admin, 1 = Job, 2 = Miet, 3 = Fraktion, 4 = Privat, 5 = Autohaus]")]
        public void FahrzeugRespawn(Client Player, int Typ)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugRespawn) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Typ < 0 || Typ > 5) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Ungültiger Typ."); return; }

            //Schleife durch die Fahrzeuge die neugespawnt werden sollen
            foreach(var Fahrzeuge in NAPI.Pools.GetAllVehicles())
            {

                foreach (AutoLokal auto in Funktionen.AutoListe)
                {
                    if (auto.FahrzeugTyp == Typ && Fahrzeuge.GetData("Id") == auto.Id && auto.FahrzeugGespawnt == 1)
                    {
                        Fahrzeuge.Position = new Vector3(auto.FahrzeugX, auto.FahrzeugY, auto.FahrzeugZ);
                        Fahrzeuge.Rotation = new Vector3(0.0, 0.0, auto.FahrzeugRot);
                    }
                }
            }

            //Nachricht an alle Spieler
            NAPI.Notification.SendNotificationToAll("~y~Info~w~: Der " + Funktionen.AdminLevelName(Funktionen.AccountAdminLevelBekommen(Player)) + " " + Player.Name + " hat alle " + Funktionen.FahrzeugTypNamen(Typ) + " Fahrzeuge neu gespawnt.");

            //Log eintrag
            Funktionen.LogEintrag(Player, "Fahrzeuge respawnt");
        }

        [Command("vmodus", "Nutze: /vmodus")]
        public void VerwaltungsModus(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.VerwaltungsModus) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            if(Player.GetData("VerwaltungsModus") == 1)
            {
                Player.SetData("VerwaltungsModus", 0);
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast den Verwaltungsmodus verlassen.");

                //Log eintrag
                Funktionen.LogEintrag(Player, "Verwaltungsmodus verlassen");
            }
            else
            {
                Player.SetData("VerwaltungsModus", 1);
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast den Verwaltungsmodus betreten.");

                //Log eintrag
                Funktionen.LogEintrag(Player, "Verwaltungsmodus betreten");
            }
        }

        [Command("fid")]
        public void FahrzeugID(Client Player)
        {
            //Überprüfen ob der Spieler in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die ID dieses Fahrzeuges ist " + Player.Vehicle.GetData("Id"));
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("tpcoord", "Nutze /tpcoord")]
        public void CoordinatenTeleport(Client Player, float posX, float posY, float posZ)
        {
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.CoordinatenTeleport) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            Player.Position = new Vector3(posX, posY, posZ);
        }

        [Command("save", "Nutze /save [Beschreibung]", GreedyArg = true)]
        public void SavePosition(Client Player, String PosBeschreibung = "Nicht gesetzt")
        {
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.PositionSpeichern) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            var Save = new Save
            {
                Beschreibung = PosBeschreibung,
                Von = Player.Name, 
                PositionX = Player.Position.X,
                PositionY = Player.Position.Y,
                PositionZ = Player.Position.Z,
                PositionRot = Player.Rotation.Z
            };

            //Query absenden
            ContextFactory.Instance.srp_saves.Add(Save);
            ContextFactory.Instance.SaveChanges();

            SaveLokal save = new SaveLokal();

            save.Id = ContextFactory.Instance.srp_saves.Max(x => x.Id);
            save.Beschreibung = PosBeschreibung;
            save.Von = Player.Name;
            save.PositionX = Player.Position.X;
            save.PositionY = Player.Position.Y;
            save.PositionZ = Player.Position.Z;
            save.PositionRot = Player.Rotation.Z;

            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Position wurde mit dem Namen ~r~" + PosBeschreibung + " ~w~gespeichert!");

            Funktionen.SaveListe.Add(save);
        }

        [Command("saveliste", "Nutze /saveliste")]
        public void SavePositionListe(Client Player, String PosBeschreibung = "Nicht gesetzt")
        {
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.SaveListe) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            Player.TriggerEvent("savelisteoeffnen");

            foreach (SaveLokal save in Funktionen.SaveListe.OrderByDescending(y => y.Id))
            {
                String SaveX = NAPI.Util.ToJson(Math.Round(save.PositionX, 2));
                String SaveY = NAPI.Util.ToJson(Math.Round(save.PositionY, 2));
                String SaveZ = NAPI.Util.ToJson(Math.Round(save.PositionZ, 2));
                String SaveRot = NAPI.Util.ToJson(Math.Round(save.PositionRot, 2));

                Player.TriggerEvent("Saveliste_Eintragen", save.Id, save.Beschreibung, save.Von, SaveX, SaveY, SaveZ);
            }
        }

        /*[Command("save", "Use /save [Position Name]", GreedyArg = true)]
        public void CMD_SavePosition(Client player, String PosName = "No Set")
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
        }*/
    }
}


