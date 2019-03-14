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
        [Command("fire", "")]
        public void Feuer(Client Player)
        {
            Player.TriggerEvent("StartFire", -3237.754f, 969.6091f, 12.94306f, 25, true);

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                Player.SendChatMessage(auto.Id + " | " + auto.FahrzeugTyp + " | " + auto.FahrzeugName);
            }

        }

        [Command("ferstellen", "Nutze: /ferstellen [Name] [Typ 0 = Admin, 1 = Job, 2 = Miet, 3 = Fraktion, 4 = Privat, 5 = Autohaus] [Farbe 1] [Farbe 2]")]
        public void FahrzeugErstellen(Client Player, String Name, int Typ, int Farbe1, int Farbe2)
        {
            //Definitionen
            uint AutoCode = NAPI.Util.GetHashKey(Name);
            Name = Funktionen.ErsterBuchstabeGroß(Name);

            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugErstellen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Typ < 0 || Typ > 5) { Player.SendChatMessage("~y~Info~w~: Ungültiger Typ."); return; }
            if(Enum.IsDefined(typeof(VehicleHash), AutoCode) == false) { Player.SendChatMessage("~y~Info~w~: Dieses Fahrzeug kennen wir leider nicht."); return; }
            if (Typ == 1) { Player.SendChatMessage("~y~Info~w~: Job Fahrzeuge werden an der jeweiligen Base gespawnt. Sie können nicht mehr per /ferstellen erstellt werden."); return; }

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
                    FahrzeugX = Player.Position.X,
                    FahrzeugY = Player.Position.Y,
                    FahrzeugZ = Player.Position.Z,
                    FahrzeugRot = Player.Rotation.Z,
                    FahrzeugFarbe1 = Farbe1,
                    FahrzeugFarbe2 = Farbe2,
                    TankVolumen = Funktionen.TankVolumenBerechnen(Name),
                    TankInhalt = Funktionen.TankVolumenBerechnen(Name) * 10 * 100,
                    Kilometerstand = 0.0f,
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
            auto.Fahrzeug.Dimension = NAPI.GlobalDimension;

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
            auto.FahrzeugX = Player.Position.X;
            auto.FahrzeugY = Player.Position.Y;
            auto.FahrzeugZ = Player.Position.Z;
            auto.FahrzeugRot = Player.Rotation.Z;
            auto.FahrzeugFarbe1 = Farbe1;
            auto.FahrzeugFarbe2 = Farbe2;
            auto.TankVolumen = Funktionen.TankVolumenBerechnen(Name);
            auto.TankInhalt = Funktionen.TankVolumenBerechnen(Name) * 10 * 100;
            auto.Kilometerstand = 0;
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
            if (Typ == 1) { Player.SendChatMessage("~y~Info~w~: Das Fahrzeug muss nun noch einem Job zugewiesen werden."); }
            if (Typ == 3) { Player.SendChatMessage("~y~Info~w~: Das Fahrzeug muss nun noch einer Fraktion zugewiesen werden."); }
            if (Typ == 4) { Player.SendChatMessage("~y~Info~w~: Das Fahrzeug muss nun noch einem Besitzer zugewiesen werden."); }
            if (Typ == 5) { Player.SendChatMessage("~y~Info~w~: Das Fahrzeug muss nun noch einem Autohaus zugewiesen werden."); }

            Funktionen.LogEintrag(Player, Funktionen.FahrzeugTypNamen(Typ) + "(s) Fahrzeug erstellt");
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
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.ManufakturFahrzeugErstellen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Enum.IsDefined(typeof(VehicleHash), AutoCode) == false) { Player.SendChatMessage("~y~Info~w~: Dieses Fahrzeug kennen wir leider nicht."); return; }

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
                FahrzeugX = Player.Position.X,
                FahrzeugY = Player.Position.Y,
                FahrzeugZ = Player.Position.Z,
                FahrzeugRot = Player.Rotation.Z,
                FahrzeugFarbe1 = Farbe1,
                FahrzeugFarbe2 = Farbe2,
                TankVolumen = Funktionen.TankVolumenBerechnen(Name),
                TankInhalt = Funktionen.TankVolumenBerechnen(Name) * 10 * 100,
                Kilometerstand = 0.0f,
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
            auto.Fahrzeug.Dimension = NAPI.GlobalDimension;

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
            auto.FahrzeugX = Player.Position.X;
            auto.FahrzeugY = Player.Position.Y;
            auto.FahrzeugZ = Player.Position.Z;
            auto.FahrzeugRot = Player.Rotation.Z;
            auto.FahrzeugFarbe1 = Farbe1;
            auto.FahrzeugFarbe2 = Farbe2;
            auto.TankVolumen = Funktionen.TankVolumenBerechnen(Name);
            auto.TankInhalt = Funktionen.TankVolumenBerechnen(Name) * 10 * 100;
            auto.Kilometerstand = 0;
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
        }

        [Command("herstellen", "Nutze: /herstellen [Interior 1 - 24] [Kaufpreis]")]
        public void ImmobilieErstellen(Client Player, int Interior, long Kaufpreis)
        {
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.HausErstellen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if(Interior <= 0 || Interior > 24) { Player.SendChatMessage("~y~Info~w~: Dieses Interior ist uns nicht bekannt."); return; } 

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
            haus.ImmobilienLabel = NAPI.TextLabel.CreateTextLabel(ImmobilienText, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
            haus.ImmobilienMarker = NAPI.Marker.CreateMarker(GlobaleSachen.ImmobilienMarker, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
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
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.SupermarktErstellen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
            supermarkt.SupermarktLabel = NAPI.TextLabel.CreateTextLabel(SupermarktText, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
            supermarkt.SupermarktMarker = NAPI.Marker.CreateMarker(GlobaleSachen.SupermarktMarker, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
            supermarkt.SupermarktBlip = NAPI.Blip.CreateBlip(new Vector3(supermarkt.SupermarktX, supermarkt.SupermarktY, supermarkt.SupermarktZ));
            supermarkt.SupermarktBlip.Name = supermarkt.SupermarktBeschreibung;
            supermarkt.SupermarktBlip.ShortRange = true;
            supermarkt.SupermarktBlip.Sprite = 590;
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
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AutohausErstellen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
            autohaus.AutohausLabel = NAPI.TextLabel.CreateTextLabel(AutohausText, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
            autohaus.AutohausMarker = NAPI.Marker.CreateMarker(GlobaleSachen.SupermarktMarker, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
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
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankeBeschreibung) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Id < 0) { Player.SendChatMessage("~y~Info~w~: Die Id kann nicht kleiner als 0 sein."); return; }
            if(Beschreibung.Length > 20) { Player.SendChatMessage("~y~Info~w~: Die Beschreibung ist zu lang."); return; }

            TankstelleLokal tanke = new TankstelleLokal();
            tanke = Funktionen.TankeVonIdBekommen(Id);

            if(tanke != null)
            {
                tanke.TankstelleBeschreibung = Beschreibung;
                tanke.TankstelleGeändert = true;
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Die Tankstelle konnte nicht gefunden werden.");
            }

            //Erfolgreich Nachricht
            Player.SendChatMessage("~g~Info~w~: Die Beschreibung der Tankstelle " + Id + " wurde erfolgreich geändert.");

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Tankstellen Beschreibung geändert");
        }

        [Command("ahbeschreibung", "Nutze: /ahbeschreibung [Id] [Text]", GreedyArg = true)]
        public void AutohausBeschreibung(Client Player, int Id, String Beschreibung)
        {
            //Benötigte Abfragen
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AutohausBeschreibung) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Id < 0) { Player.SendChatMessage("~y~Info~w~: Die Id kann nicht kleiner als 0 sein."); return; }
            if (Beschreibung.Length > 20) { Player.SendChatMessage("~y~Info~w~: Die Beschreibung ist zu lang."); return; }

            AutohausLokal autohaus = new AutohausLokal();
            autohaus = Funktionen.AutohausVonIdBekommen(Id);

            if (autohaus != null)
            {
                autohaus.AutohausBeschreibung = Beschreibung;
                autohaus.AutohausGeändert = true;
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Das Autohaus konnte nicht gefunden werden.");
            }

            //Erfolgreich Nachricht
            Player.SendChatMessage("~g~Info~w~: Die Beschreibung des Autohauses " + Id + " wurde erfolgreich geändert.");

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Autohaus Beschreibung geändert");
        }

        [Command("tkaufpreis", "Nutze: /tkaufpreis [Id] [Kaufpreis]")]
        public void TankstellenKaufPreis(Client Player, int Id, long Kaufpreis)
        {
            //Benötigte Abfragen
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankeKaufpreis) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Id < Kaufpreis) { Player.SendChatMessage("~y~Info~w~: Der Kaufpreis kann nicht kleiner als 0 sein."); return; }
            

            TankstelleLokal tanke = new TankstelleLokal();
            tanke = Funktionen.TankeVonIdBekommen(Id);

            if (tanke != null)
            {
                tanke.TankstelleKaufpreis = Kaufpreis;
                tanke.TankstelleGeändert = true;
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Die Tankstelle konnte nicht gefunden werden.");
            }

            //Erfolgreich Nachricht
            Player.SendChatMessage("~g~Info~w~: Die Beschreibung der Tankstelle " + Id + " wurde erfolgreich geändert.");

            //Log Eintrag
            Funktionen.LogEintrag(Player, "Tankstellen Kaufpreis geändert");
        }

        [Command("terstellen", "Nutze: /terstellen [Kaufpreis]")]
        public void TankstelleErstellen(Client Player, long Kaufpreis)
        {
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankeErstellen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
            tanke.TankstellenLabel = NAPI.TextLabel.CreateTextLabel(TankstellenText, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
            tanke.TankstellenMarker = NAPI.Marker.CreateMarker(GlobaleSachen.TankstellenMarker, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
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

            if(Gesperrt == 1) { Player.SendChatMessage("~y~Info~w~: Der Befehl ist derzeit leider gesperrt."); return; }

            //Definitionen
            uint BotHash = NAPI.Util.GetHashKey(Name);

            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.PedErstellen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Enum.IsDefined(typeof(PedHash), BotHash) == false) { Player.SendChatMessage("~y~Info~w~: Dieses Ped kennen wir leider nicht."); return; }

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
            bot.Bot = NAPI.Ped.CreatePed(NAPI.Util.PedNameToModel(Name), new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), Player.Rotation.Z, Player.Dimension);

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
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.PedLöschen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
                Player.SendChatMessage("~y~Info~w~: Es wurde kein Ped gefunden.");
            }
        }

        [Command("tpunkterstellen", "Nutze: /tpunkterstellen [TankstellenID]")]
        public void TankstellenPunktErstellen(Client Player, int Id)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankPunktErstellen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Query durch die Tankstellen
            var Check = ContextFactory.Instance.srp_tankstellen.Count(x => x.Id == Id);

            //Prüfen ob die Tankstelle existiert
            if (Check == 0) { Player.SendChatMessage("~y~Info~w~: Die Tankstelle ist uns nicht bekannt."); return; }

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
            tankstellenpunkt.TankstellenPunktLabel = NAPI.TextLabel.CreateTextLabel(TankstellenPunktText, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
            tankstellenpunkt.TankstellenPunktMarker = NAPI.Marker.CreateMarker(GlobaleSachen.TankstellenZapfsäuleMarker, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);

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
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankInfoErstellen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Query durch die Tankstellen
            var Check = ContextFactory.Instance.srp_tankstellen.Count(x => x.Id == Id);

            //Prüfen ob die Tankstelle existiert
            if (Check == 0) { Player.SendChatMessage("~y~Info~w~: Die Tankstelle ist uns nicht bekannt."); return; }

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
            tankinfo.TankstellenInfoLabel = NAPI.TextLabel.CreateTextLabel(TankstellenInfoText, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
            tankinfo.TankstellenInfoMarker = NAPI.Marker.CreateMarker(GlobaleSachen.TankstellenInfoMarker, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);

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
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankeLöschen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
                Player.SendChatMessage("~y~Info~w~: Es wurde keine Tankstelle gefunden.");
            }
        }

        [Command("247löschen", "Nutze: /247löschen")]
        public void SupermarktLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.SupermarktLöschen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
                Player.SendChatMessage("~y~Info~w~: Es wurde kein 24/7 gefunden.");
            }
        }

        [Command("ahlöschen", "Nutze: /ahlöschen")]
        public void AutohausLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AutohausLöschen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
                Player.SendChatMessage("~y~Info~w~: Es wurde kein Autohaus gefunden.");
            }
        }

        [Command("hlöschen", "Nutze: /hlöschen")]
        public void ImmobilieLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.HausLöschen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
                Player.SendChatMessage("~y~Info~w~: Es wurde keine Immobilie gefunden.");
            }
        }

        [Command("hint", "Nutze: /hint [Interior 1 - 24]")]
        public void ImmobilieInterior(Client Player, int Interior)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.HausInterior) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Interior <= 0 || Interior > 24) { Player.SendChatMessage("~y~Info~w~: Dieses Interior ist uns nicht bekannt."); return; }

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
                Player.SendChatMessage("~y~Info~w~: Das Interior wurde angepasst.");
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Es wurde keine Immobilie gefunden.");
            }
        }

        [Command("hkaufpreis", "Nutze: /hkaufpreis [Preis]")]
        public void ImmobilieKaufpreis(Client Player, long Preis)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.HausKaufPreis) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Preis < 0) { Player.SendChatMessage("~y~Info~w~:Das ist zu wenig Geld."); return; }

            ImmobilienLokal haus = new ImmobilienLokal();
            haus = Funktionen.NaheImmobilieBekommen(Player);

            if (haus != null)
            {
                haus.ImmobilienKaufpreis = Preis;
                haus.ImmobilieGeändert = true;

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Immobilie Kaufpreis geändert");

                Player.SendChatMessage("~y~Info~w~: Der Kaufpreis wurde auf " + Funktionen.GeldFormatieren(Preis) + " geändert!");
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Es wurde keine Immobilie gefunden.");
            }
        }

        [Command("htp", "Nutze: /htp [Hausnummer]")]
        public void ImmobilieTeleport(Client Player, int Hausnummer)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.HausTeleport) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
                Player.SendChatMessage("~y~Info~w~: Dieses Haus konnte nicht gefunden werden.");
            }
        }

        [Command("tpunktlöschen", "Nutze: /tpunktlöschen")]
        public void TankstellenPunktLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankPunktLöschen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
                Player.SendChatMessage("~y~Info~w~: Es wurde keine Zapfsäule gefunden.");
            }
        }

        [Command("tinfolöschen", "Nutze: /tinfolöschen")]
        public void TankstellenInfoLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.TankInfoLöschen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
                Player.SendChatMessage("~y~Info~w~: Es wurde kein Info Punkt gefunden.");
            }
        }

        [Command("waffe")]
        public void AdminWaffe(Client Player, String weaponName, int ammo)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.Waffe) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Anhand des eingegeben Namens den WeaponHash ermitteln
            WeaponHash weapon = NAPI.Util.WeaponNameToModel(weaponName);

            if (weapon == 0)
            {
                Player.SendChatMessage("~y~Info~w~: Diese Waffe ist uns nicht bekannt.");
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Du hast die Waffe erhalten.");

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

            Player.SendChatMessage("~w~[~r~Stats für " + Player.Name + "~w~]");
            Player.SendChatMessage("Social-Club: " + Funktionen.AccountSocialClubBekommen(Player));
            Player.SendChatMessage("Admin Level: " + Funktionen.AccountAdminLevelBekommen(Player));
            Player.SendChatMessage("Spielzeit: " + Funktionen.AccountSpielzeitBerechnen(Player));
            Player.SendChatMessage("Geld: " + Funktionen.GeldFormatieren(Funktionen.AccountGeldBekommen(Player)));
            Player.SendChatMessage("Bank Geld: " + Funktionen.GeldFormatieren(Funktionen.AccountBankGeldBekommen(Player)));
            Player.SendChatMessage("Fahrzeug Schlüssel: " + Funktionen.AccountFahrzeugSchlüsselBekommen(Player));
            Player.SendChatMessage("Job: " + Funktionen.AccountJobInNamen(Player));
        }

        [Command("fparken", "Nutze /fparken")]
        public void FahrzeugParken(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugParken) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
       
                Player.SendChatMessage("~y~Info~w~: Das Fahrzeug mit der ID: ~g~" + FahrzeugID + " ~w~wurde erfolgreich geparkt.");

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Fahrzeug ID: " + FahrzeugID + " geparkt");

                //Query absenden
                ContextFactory.Instance.SaveChanges();
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("fmietpreis", "Nutze /fmietpreis")]
        public void FahrzeugMietPreis(Client Player, long Mietpreis)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugMietPreis) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Mietpreis < 0) { Player.SendChatMessage("~y~Info~w~: Der Mietpreis muss mehr als 0 sein."); return; }

            //Überprüfen ob der Spieler in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                //Die Datenbank ID des Fahrzeuges bekommen
                int FahrzeugID = Player.Vehicle.GetData("Id");

                AutoLokal auto = new AutoLokal();
                auto = Funktionen.AutoBekommen(Player.Vehicle);

                if(auto.FahrzeugTyp != 2) { Player.SendChatMessage("~y~Info~w~: Das ist kein Mietfahrzeug."); return; }

                auto.FahrzeugMietpreis = Mietpreis;
                
                auto.FahrzeugGeändert = true;
       
                Player.SendChatMessage("~y~Info~w~: Der Mietpreis für dieses Fahrzeug wurde auf " + Funktionen.GeldFormatieren(Mietpreis) + " gesetzt.");

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Fahrzeug ID: " + FahrzeugID + " Mietpreis geändert");

                //Query absenden
                ContextFactory.Instance.SaveChanges();
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("fkaufpreis", "Nutze /fkaufpreis")]
        public void FahrzeugKaufpreis(Client Player, long Kaufpreis)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugKaufPreis) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Kaufpreis < 0) { Player.SendChatMessage("~y~Info~w~: Der Kaufpreis muss mehr als 0 sein."); return; }

            //Überprüfen ob der Spieler in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                //Die Datenbank ID des Fahrzeuges bekommen
                int FahrzeugID = Player.Vehicle.GetData("Id");

                AutoLokal auto = new AutoLokal();
                auto = Funktionen.AutoBekommen(Player.Vehicle);

                if (auto.FahrzeugTyp != 5) { Player.SendChatMessage("~y~Info~w~: Das ist kein verkaufbares Fahrzeug."); return; }

                auto.FahrzeugKaufpreis = Kaufpreis;

                auto.FahrzeugGeändert = true;

                Player.SendChatMessage("~y~Info~w~: Der Kaufpreis für dieses Fahrzeug wurde auf " + Funktionen.GeldFormatieren(Kaufpreis) + " gesetzt.");

                //Log Eintrag
                Funktionen.LogEintrag(Player, "Fahrzeug ID: " + FahrzeugID + " Kaufpreis geändert");

                //Query absenden
                ContextFactory.Instance.SaveChanges();
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("pferd1", "Nutze /pferd1")]
        public void PferdeRennen(Client Player)
        {
            Player.TriggerEvent("Pferderennenoeffnen");
            Player.SetData("Pferderennen", 1);
        }

        [Command("pferd2", "Nutze /pferd2")]
        public void PferdeRennen1(Client Player)
        {
            Player.TriggerEvent("Pferderennenschliessen");
            Player.SetData("Pferderennen", 0);
        }

        [Command("flöschen", "Nutze /flöschen")]
        public void FahrzeugLöschen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugLöschen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Überprüfen ob der Spieler in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                AutoLokal auto = new AutoLokal();
                auto = Funktionen.AutoBekommen(Player.Vehicle);

                //Falls es ein Admin Fahrzeug ist
                if(auto.FahrzeugTyp == 0) { Funktionen.AdminFahrzeugLöschen(Player, Player.Vehicle); return; }

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
                Player.SendChatMessage("~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("a", "Nutze /a [Text]", GreedyArg = true)]
        public void AdminChat(Client Player, String Text)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AdminChat) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            foreach (var Spieler in NAPI.Pools.GetAllPlayers())
            {
                if (Funktionen.AccountAdminLevelBekommen(Spieler) > 0)
                {
                    Spieler.SendChatMessage("~y~Adminchat ~w~" + Funktionen.AdminLevelName(Funktionen.AccountAdminLevelBekommen(Player)) + " " + Player.Name + ": " + Text);
                }
            }
        }

        [Command("teleporten", "Nutze: /teleporten [Spielername] [1 = Zu ihm, 2 = Zu mir]")]
        public void AdminTeleport(Client Player, String Spieler, int Typ)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.Teleporten) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Typ < 0 || Typ > 2) { Player.SendChatMessage("~y~Info~w~: Ungültige Eingabe."); return; }

            //Den Spieler über den Namen ermitteln
            Client Spieler1 = NAPI.Player.GetPlayerFromName(Spieler);
            if(Spieler1 == null)
            {
                if(Funktionen.SpielerSuchen(Spieler) == null)
                {
                    Player.SendChatMessage("~y~Info~w~: Dieser Spieler konnte nicht gefunden werden.");
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
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast " + Spieler1.Name + " zu dit teleportiert!");
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

        [Command("teleports", "Nutze: /teleports [Name]")]
        public void AdminTeleports(Client Player, String Name)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.Teleporten) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Die Teleport Orte mit Abfrage
            if(Name == "Stadthalle")
            {
                Player.Position = new Vector3(-1277.161, -560.1884, 30.22556);
            }
            else if (Name == "Noobspawn")
            {
                Player.Position = new Vector3(-3245.707, 967.6216, 12.73052);
            }
            else if (Name == "Arbeitsamt")
            {
                Player.Position = new Vector3(-837.6387, -272.0361, 38.72037);
            }
            else if (Name == "Berufskraftfahrer")
            {
                Player.Position = new Vector3(-1546.57, 1367.763, 126.1016);
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Dieser Ort konnte nicht gefunden werden.");
            }
        }

        [Command("admingeben", "Nutze: /admingeben [Spielername] [AdminLevel]")]
        public void AdminGeben(Client Player, String Spieler, int Level)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            //if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.AdminGeben) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Level < 0 || Level > 5) { Player.SendChatMessage("~y~Info~w~: Ungültige Eingabe."); return; }

            //Spieler über den Namen ermitteln
            Client Spieler1 = NAPI.Player.GetPlayerFromName(Spieler);
            if (Spieler1 == null) { Player.SendChatMessage("~y~Info~w~: Dieser Spieler konnte nicht gefunden werden."); return; }

            //Dem Spieler das Admin Level setzen
            Funktionen.AccountAdminLevelSetzen(Spieler1, Level);

            //Beiden eine Nachricht senden
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast " + Spieler1.Name + " Admin Level " + Level + " gegeben!");
            NAPI.Notification.SendNotificationToPlayer(Spieler1, "~y~Info~w~: Du hast von " + Player.Name + " Admin Level " + Level + " erhalten!");

            //Log eintrag
            Funktionen.LogEintrag(Player, Spieler1.Name + " Admin Level " + Level + " gegeben");
        }

        [Command("geldsetzen", "Nutze: /geldsetzen [Spielername] [Geld]")]
        public void GeldSetzen(Client Player, String Spieler, long Geld)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.GeldGeben) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Den Spieler über den Namen ermitteln
            Client Spieler1 = NAPI.Player.GetPlayerFromName(Spieler);
            if (Spieler1 == null)
            {
                if (Funktionen.SpielerSuchen(Spieler) == null)
                {
                    Player.SendChatMessage("~y~Info~w~: Dieser Spieler konnte nicht gefunden werden.");
                    return;
                }
                else
                {
                    Spieler1 = Funktionen.SpielerSuchen(Spieler);
                }
            }

            //Dem Spieler das Admin Level setzen
            Funktionen.AccountGeldSetzen(Spieler1, Geld);

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
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugReparieren) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            //Checken ob er in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                //Log eintrag
                Funktionen.LogEintrag(Player, "Fahrzeug repariert");

                Player.SendChatMessage("~y~Info~w~: Das Fahrzeug mit der ID: ~g~" + Player.Vehicle.GetData("Id") + " ~w~wurde erfolgreich repariert.");
                Player.Vehicle.Repair();
                Player.TriggerEvent("FahrzeugReparieren");
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("fzuweisen", "Nutze: /fzuweisen [1 = Job, 2 = Fraktion, 3 = Autohaus] [Fraktions ID / Job ID / Autohaus ID]")]
        public void FahrzeugZuweisen(Client Player, int Typ, int Id = 0)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugZuweisen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Typ < 1 || Typ > 3) { Player.SendChatMessage("~y~Info~w~: Ungültiger Typ."); return; }

            //Checken ob er in einem Fahrzeug ist
            if (Player.IsInVehicle)
            {
                //[Typ 0 = Admin, 1 = Job, 2 = Miet, 3 = Fraktion, 4 = Privat, 5 = Autohaus]
                if (Fahrzeuge.TypBekommen(Player.Vehicle) == 1 && Typ == 1)
                {
                    //Schauen ob der Job überhaupt existiert
                    if (Id < 0|| Id > Zahlen.Jobs) { Player.SendChatMessage("~y~Info~w~: Ungültiger Job."); return; }

                    //Dem Fahrzeug lokal die Werte mitgeben
                    Fahrzeuge.JobSetzen(Player.Vehicle, Id);

                    //Nachricht an den Spieler
                    Player.SendChatMessage("~y~Info~w~: Dem Fahrzeug wurde der Job zugewiesen.");

                    //Log eintrag
                    Funktionen.LogEintrag(Player, "Fahrzeug Job zugewiesen");
                }
                else if (Fahrzeuge.TypBekommen(Player.Vehicle) == 3 && Typ == 2)
                {
                    //Schauen ob die Fraktion überhaupt existiert
                    if (Id < 1 || Id > Zahlen.Fraktionen) { Player.SendChatMessage("~y~Info~w~: Ungültige Fraktion."); return; }

                    //Dem Fahrzeug lokal die Werte mitgeben
                    Fahrzeuge.FraktionSetzen(Player.Vehicle, Id);

                    //Nachricht an den Spieler
                    Player.SendChatMessage("~y~Info~w~: Dem Fahrzeug wurde die Fraktion zugewiesen.");

                    //Log eintrag
                    Funktionen.LogEintrag(Player, "Fahrzeug Fraktion zugewiesen");
                }
                else if (Fahrzeuge.TypBekommen(Player.Vehicle) == 5 && Typ == 3)
                {
                    //Query durch die Autohäuser
                    var Check = ContextFactory.Instance.srp_autohäuser.Count(x => x.Id == Id);

                    //Prüfen ob das Autohaus existiert
                    if (Check == 0) { Player.SendChatMessage("~y~Info~w~: Dieses Autohaus ist uns nicht bekannt."); return; }

                    //Dem Fahrzeug lokal die Werte mitgeben
                    Fahrzeuge.AutohausSetzen(Player.Vehicle, Id);

                    //Nachricht an den Spieler
                    Player.SendChatMessage("~y~Info~w~: Dem Fahrzeug wurde das Autohaus zugewiesen.");

                    //Log eintrag
                    Funktionen.LogEintrag(Player, "Fahrzeug Autohaus zugewiesen");
                }
                else
                {
                    //Fehlermessage falls der Typ der Verändert werden soll nicht zum Fahrzeug passt
                    Player.SendChatMessage("~y~Info~w~: Da hat was nicht geklappt. Schaue auf das Kennzeichen um zu sehen was es für ein Fahrzeug ist.");
                }
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("fzuweisenprivat", "Nutze: /fzuweisenprivat [Name]")]
        public void FahrzeugZuweisenPrivat(Client Player, String Spieler)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugZuweisen) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

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
                    if (Spieler1 == null) { Player.SendChatMessage("~y~Info~w~: Dieser Spieler konnte nicht gefunden werden."); return; }

                    //Dem Fahrzeug den Job zuweisen
                    Fahrzeug.FahrzeugSpieler = Spieler1.GetData("Id");

                    //Dem Spieler und dem Sender eine Nachricht senden
                    Spieler1.SendChatMessage("~y~Info~w~: Der " + Funktionen.AdminLevelName(Funktionen.AccountAdminLevelBekommen(Player)) + " " + Player.Name + " hat dir das Fahrzeug " + Fahrzeuge.NameBekommen(Player.Vehicle) + " als Privatfahrzeug zugewiesen.");
                    Player.SendChatMessage("~y~Info~w~: Du hast " + Spieler1.Name + " das Fahrzeug " + Fahrzeuge.NameBekommen(Player.Vehicle) + " als Privatfahrzeug zugewiesen.");

                    //Dem Fahrzeug lokal die Werte mitgeben
                    Fahrzeuge.BesitzerSetzen(Player.Vehicle, Spieler1.GetData("Id"));

                    //Log eintrag
                    Funktionen.LogEintrag(Player, "Privat Fahrzeug zugewiesen");
                }
                else
                {
                    //Fehlermessage falls der Typ der Verändert werden soll nicht zum Fahrzeug passt
                    Player.SendChatMessage("~y~Info~w~: Da hat was nicht geklappt. Schaue auf das Kennzeichen um zu sehen was es für ein Fahrzeug ist.");
                }

                //Query absenden
                ContextFactory.Instance.SaveChanges();
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }
        

        [Command("fporten", "Nutze: /fporten [ID]")]
        public void FahrzeugPorten(Client Player, int Id)
        {
            //Definierungen
            int i = 0;

            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugPorten) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Id < 0) { Player.SendChatMessage("~y~Info~w~: Ungültige ID."); return; }

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
                Player.SendChatMessage("~y~Info~w~: Das Fahrzeug konnte nicht gefunden werden.");
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Das Fahrzeug wurde zu dir teleportiert.");
            }
        }

        [Command("speichern", "Nutze: /speichern")]
        public void Speichern(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.Speichern) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            NAPI.Chat.SendChatMessageToAll("~g~Info~w~: Speicherung wurde eingeleitet.");
            NAPI.Chat.SendChatMessageToAll("~g~Info~w~: Dies kann zu Verzögerungen führen!");

            //Stoppuhr starten
            var Uhr = System.Diagnostics.Stopwatch.StartNew();

            //Alles speichern
            Funktionen.AllesSpeichern();

            //StoppUhr anhalten
            Uhr.Stop();
            var MS = Uhr.ElapsedMilliseconds;

            //Ausgabe
            NAPI.Chat.SendChatMessageToAll("~g~Info~w~: Der Gesamte Server wurde gespeichert. [" + MS + "ms]");
        }

        [Command("frespawn", "Nutze: /frespawn [Typ 0 = Admin, 1 = Job, 2 = Miet, 3 = Fraktion, 4 = Privat, 5 = Autohaus]")]
        public void FahrzeugRespawn(Client Player, int Typ)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.FahrzeugRespawn) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }
            if (Typ < 0 || Typ > 5) { Player.SendChatMessage("~y~Info~w~: Ungültiger Typ."); return; }

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
            NAPI.Chat.SendChatMessageToAll("~y~Info~w~: Der " + Funktionen.AdminLevelName(Funktionen.AccountAdminLevelBekommen(Player)) + " " + Player.Name + " hat alle " + Funktionen.FahrzeugTypNamen(Typ) + " Fahrzeuge neu gespawnt.");

            //Log eintrag
            Funktionen.LogEintrag(Player, "Fahrzeuge respawnt");
        }

        [Command("vmodus", "Nutze: /vmodus")]
        public void VerwaltungsModus(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("Eingeloggt") == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst dafür angemeldet sein!"); return; }
            if (Funktionen.AccountAdminLevelBekommen(Player) < AdminBefehle.VerwaltungsModus) { Player.SendChatMessage("~y~Info~w~: Deine Rechte reichen nicht aus."); return; }

            if(Player.GetData("VerwaltungsModus") == 1)
            {
                Player.SetData("VerwaltungsModus", 0);
                Player.SendChatMessage("~y~Info~w~: Du hast den Verwaltungsmodus verlassen.");

                //Log eintrag
                Funktionen.LogEintrag(Player, "Verwaltungsmodus verlassen");
            }
            else
            {
                Player.SetData("VerwaltungsModus", 1);
                Player.SendChatMessage("~y~Info~w~: Du hast den Verwaltungsmodus betreten.");

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
                Player.SendChatMessage("~y~Info~w~: Die ID dieses Fahrzeuges ist " + Player.Vehicle.GetData("Id"));
            }
            else
            {
                Player.SendChatMessage("~y~Info~w~: Du musst in einem Fahrzeug sein.");
            }
        }

        [Command("save", "Use /save [Position Name]", GreedyArg = true)]
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
        }
    }
}


