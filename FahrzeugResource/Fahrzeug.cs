/************************************************************************************************************
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        @@ Strawberry Roleplay Gamemode                                                                   @@
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
*************************************************************************************************************/

using GTANetworkAPI;
using Datenbank;
using Haupt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Fahrzeug
{
    public class Fahrzeuge : Script
    {
        [RemoteEvent("RollerMieten")]
        public void RollerMieten(Client Player)
        {
            //Das Fahrzeug greifen
            AutoLokal auto = new AutoLokal();
            auto = Funktionen.AutoBekommen(Player.Vehicle);

            if(Funktionen.AccountGeldBekommen(Player) < auto.FahrzeugMietpreis)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast nicht genug Geld.");
                Player.TriggerEvent("rollermietenpopupschliessen");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast das Fahrzeug gemietet.");
                Funktionen.AccountGeldSetzen(Player, 2, auto.FahrzeugMietpreis);
                Player.TriggerEvent("rollermietenpopupschliessen");
            }
        }

        [RemoteEvent("RollerNichtMieten")]
        public void RollerNichtMieten(Client Player)
        {
            Player.TriggerEvent("rollermietenpopupschliessen");
        }

        [RemoteEvent("LichtAn")]
        public void LichtAn(Client Player)
        {
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Licht eingeschaltet.");
        }

        [RemoteEvent("LichtAus")]
        public void LichtAus(Client Player)
        {
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Licht ausgeschaltet.");
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client Player, Vehicle vehicle, sbyte seatID)
        {
            //Abfragen damit niemand mit den autos rumfahren kann
            if (Player.GetData("VerwaltungsModus") == 0)
            {
                if (AutoHausBekommen(Player.Vehicle) == -1 && Funktionen.AccountHatAutohaus(Player) == 0) { Player.TriggerEvent("FahrzeugVerlassen"); return; }
            }
            
            //Mietfahrzeug
            if (TypBekommen(Player.Vehicle) == 2)
            {
                if(NameBekommen(Player.Vehicle) == "faggio")
                {
                    Player.TriggerEvent("RollerSpeed");
                }
                else if (NameBekommen(Player.Vehicle) == "pounder2")
                {
                    Player.TriggerEvent("LKWSpeed");
                }

                if (Player.GetData("VerwaltungsModus") == 0)
                {
                    Player.TriggerEvent("rollermietenpopupoeffnen", Funktionen.GeldFormatieren(MietpreisBekommen(Player.Vehicle)));
                }
            }
            //Autohausfahrzeug
            else if (TypBekommen(Player.Vehicle) == 5 && AutoHausBekommen(Player.Vehicle) > 0)
            {
                if (Player.GetData("VerwaltungsModus") == 0)
                {
                    if (Player.GetData("KaufenTyp") == 5) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Schließe erst das aktuelle Fenster."); return; }
                    Player.SetData("KaufenTyp", 5);
                    Player.SetData("KaufenId", IdBekommen(Player.Vehicle));
                    Player.SetData("KaufenPreis", KaufPreisBekommen(Player.Vehicle));

                    //Freezen
                    Funktionen.Freeze(Player);

                    Player.TriggerEvent("Kaufen", 5, Funktionen.GeldFormatieren(KaufPreisBekommen(Player.Vehicle)));
                }
            }
            //Manufaktur Fahrzeuge
            else if (AutoHausBekommen(Player.Vehicle) == -1 && Funktionen.AccountHatAutohaus(Player) == 1)
            {
                if (Player.GetData("VerwaltungsModus") == 0)
                {
                    if (Player.GetData("KaufenTyp") == 6) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Schließe erst das aktuelle Fenster."); return; }
                    Player.SetData("KaufenTyp", 6);
                    Player.SetData("KaufenId", IdBekommen(Player.Vehicle));
                    Player.SetData("KaufenPreis", KaufPreisBekommen(Player.Vehicle));

                    //Benötigte Definitionen
                    double Rechnen = 0.0;
                    long Rechnen1 = 0;
                    Rechnen = KaufPreisBekommen(Player.Vehicle) + KaufPreisBekommen(Player.Vehicle) * 0.2;
                    Rechnen1 = (long)Rechnen;

                    //Freezen
                    Funktionen.Freeze(Player);

                    Player.TriggerEvent("Kaufen", 6, Funktionen.GeldFormatieren(KaufPreisBekommen(Player.Vehicle)), Funktionen.GeldFormatieren(Rechnen1));
                }
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnPlayerExitVehicle(Client Player, Vehicle vehicle)
        {
            AutoLokal auto = new AutoLokal();
            auto = Funktionen.AutoBekommen(Player.Vehicle);

            if(auto.FahrzeugJob == 1 && vehicle.GetData("GeradeGespawnt") == 0)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Fertig mit jobben? /beenden");
            }
            else
            {
                Funktionen.FahrzeugSpeichern(vehicle);
            }
        }

        public static void JobFahrzeugLöschen(Client Player, Vehicle Fahrzeug)
        {
            if (Funktionen.AccountJobFahrzeugBekommen(Player) != null)
            {
                if (Player.GetData("BerufskraftfahrerDieselTanke") != 0 || Player.GetData("BerufskraftfahrerE10Tanke") != 0 || Player.GetData("BerufskraftfahrerSuperTanke") != 0)
                {
                    if (Player.GetData("BerufskraftfahrerDieselTanke") != 0)
                    {
                        foreach (TankstelleLokal tanke in Funktionen.TankenListe)
                        {
                            if (tanke.Id == Player.GetData("BerufskraftfahrerDieselTanke"))
                            {
                                tanke.TankstelleJobSpieler = 0;
                            }
                        }
                    }
                    else if (Player.GetData("BerufskraftfahrerE10Tanke") != 0)
                    {
                        foreach (TankstelleLokal tanke in Funktionen.TankenListe)
                        {
                            if (tanke.Id == Player.GetData("BerufskraftfahrerE10Tanke"))
                            {
                                tanke.TankstelleJobSpieler = 0;
                            }
                        }
                    }
                    else if (Player.GetData("BerufskraftfahrerSuperTanke") != 0)
                    {
                        foreach (TankstelleLokal tanke in Funktionen.TankenListe)
                        {
                            if (tanke.Id == Player.GetData("BerufskraftfahrerSuperTanke"))
                            {
                                tanke.TankstelleJobSpieler = 0;
                            }
                        }
                    }
                }

                AutoLokal auto = new AutoLokal();
                auto = Funktionen.AutoBekommen(Fahrzeug);
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dein Job wurde beendet.");
                //Berufskraftfahrer
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

                //Busfahrer
                Player.SetData("BusfahrerFahrzeug", 0);
                Player.SetData("BusfahrerJobAngenommen", 0);
                Player.SetData("BusfahrerRoute", 0);
                Player.SetData("BusfahrerRoutePosition", 0);

                auto.Fahrzeug.Delete();

                Funktionen.AutoListe.Remove(auto);

                Funktionen.AccountJobFahrzeugSetzen(Player, null);

                //Navi resetten
                var EigenerPunkt = new Vector3(Player.Position.X, Player.Position.Y, 0);
                Player.TriggerEvent("Navigation", EigenerPunkt.X, EigenerPunkt.Y);
            }
        }

        public static List<AutoLokal> AlleAutosLadenDB()
        {
            List<AutoLokal> AutoListe = new List<AutoLokal>();
            foreach (var Fahrzeuge in ContextFactory.Instance.srp_fahrzeuge.Where(x => x.Id > 0).ToList())
            {
                AutoLokal auto = new AutoLokal();

                //Definitionen
                uint AutoCode = NAPI.Util.GetHashKey(Fahrzeuge.FahrzeugName);
                String VerkaufsText = null;

                //Die eigentliche Erstellung des Fahrzeuges
                auto.Fahrzeug = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Fahrzeuge.FahrzeugX, Fahrzeuge.FahrzeugY, Fahrzeuge.FahrzeugZ), Fahrzeuge.FahrzeugRot, Fahrzeuge.FahrzeugFarbe1, Fahrzeuge.FahrzeugFarbe2);

                //ID Lokal setzen damit man das Fahrzeug erkennen kann
                auto.Fahrzeug.SetData("Id", Fahrzeuge.Id);

                auto.Fahrzeug.Dimension = 0;

                auto.Id = Fahrzeuge.Id;
                auto.FahrzeugBeschreibung = Fahrzeuge.FahrzeugBeschreibung;
                auto.FahrzeugName = Fahrzeuge.FahrzeugName;
                auto.FahrzeugTyp = Fahrzeuge.FahrzeugTyp;
                auto.FahrzeugFraktion = Fahrzeuge.FahrzeugFraktion;
                auto.FahrzeugJob = Fahrzeuge.FahrzeugJob;
                auto.FahrzeugSpieler = Fahrzeuge.FahrzeugSpieler;
                auto.FahrzeugMietpreis = Fahrzeuge.FahrzeugMietpreis;
                auto.FahrzeugKaufpreis = Fahrzeuge.FahrzeugKaufpreis;
                auto.FahrzeugAutohaus = Fahrzeuge.FahrzeugAutohaus;
                auto.FahrzeugX = Fahrzeuge.FahrzeugX;
                auto.FahrzeugY = Fahrzeuge.FahrzeugY;
                auto.FahrzeugZ = Fahrzeuge.FahrzeugZ;
                auto.FahrzeugRot = Fahrzeuge.FahrzeugRot;
                auto.FahrzeugFarbe1 = Fahrzeuge.FahrzeugFarbe1;
                auto.FahrzeugFarbe2 = Fahrzeuge.FahrzeugFarbe2;
                auto.TankVolumen = Fahrzeuge.TankVolumen;
                auto.TankInhalt = Fahrzeuge.TankInhalt * 10 * 100;
                auto.Kilometerstand = Fahrzeuge.Kilometerstand * 10 * 100;
                auto.KraftstoffArt = Fahrzeuge.KraftstoffArt;
                auto.FahrzeugHU = Fahrzeuge.FahrzeugHU;
                auto.FahrzeugAbgeschlossen = Fahrzeuge.FahrzeugAbgeschlossen;
                auto.FahrzeugMotor = Fahrzeuge.FahrzeugMotor;

                //Nur lokal
                auto.FahrzeugAltePositionX = Fahrzeuge.FahrzeugX;
                auto.FahrzeugAltePositionY = Fahrzeuge.FahrzeugY;
                auto.FahrzeugAltePositionZ = Fahrzeuge.FahrzeugZ;
                auto.FahrzeugNeuePositionX = 0;
                auto.FahrzeugNeuePositionY = 0;
                auto.FahrzeugNeuePositionZ = 0;

                //Zur Liste hinzufügen
                AutoListe.Add(auto);

                //Zuweisungen für das Auto
                auto.Fahrzeug.NumberPlate = Fahrzeuge.FahrzeugBeschreibung;

                if (auto.FahrzeugMotor == 0)
                {
                    auto.Fahrzeug.EngineStatus = false;
                }
                else
                {
                    auto.Fahrzeug.EngineStatus = true;
                }

                //Schauen ob es ein Verkaufs Auto ist
                if (auto.FahrzeugAutohaus > 0)
                {
                    VerkaufsText = "~r~[~w~Zu verkaufen~r~]~n~";
                    VerkaufsText += "Name~w~: " + auto.FahrzeugName + "~n~~r~";
                    VerkaufsText += "Autohaus~w~: " + auto.FahrzeugAutohaus + "~n~~r~";
                    VerkaufsText += "Preis:~w~ " + Funktionen.GeldFormatieren(auto.FahrzeugKaufpreis);

                    auto.AutohausTextLabel = NAPI.TextLabel.CreateTextLabel(VerkaufsText, new Vector3(Fahrzeuge.FahrzeugX, Fahrzeuge.FahrzeugY, Fahrzeuge.FahrzeugZ), 18.0f, 1.00f, 4, new Color(255, 255, 255), false, 0);
                }
                if (auto.FahrzeugAutohaus < 0)
                {
                    VerkaufsText = "~r~[~w~Manufaktur~r~]~n~";
                    VerkaufsText += "Name~w~: " + auto.FahrzeugName + "~n~~r~";
                    VerkaufsText += "Preis:~w~ " + Funktionen.GeldFormatieren(auto.FahrzeugKaufpreis);

                    auto.AutohausTextLabel = NAPI.TextLabel.CreateTextLabel(VerkaufsText, new Vector3(Fahrzeuge.FahrzeugX, Fahrzeuge.FahrzeugY, Fahrzeuge.FahrzeugZ), 18.0f, 1.00f, 4, new Color(255, 255, 255), false, 0);
                }
            }
            return AutoListe;
        }

        public static int TypBekommen(Vehicle Auto)
        {
            //Benötigte Definitionen
            int Typ = 0;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    Typ = auto.FahrzeugTyp;
                }
            }
            return Typ;
        }

        public static int AutoHausBekommen(Vehicle Auto)
        {
            //Benötigte Definitionen
            int Autohaus = 0;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    Autohaus = auto.FahrzeugAutohaus;
                }
            }
            return Autohaus;
        }

        public static void FahrzeugKaufen(Client Player, Vehicle Auto)
        {
            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    auto.FahrzeugSpieler = Player.GetData("Id");
                    auto.FahrzeugTyp = 4;
                    auto.FahrzeugAutohaus = 0;

                    auto.AutohausTextLabel.Delete();

                    auto.FahrzeugGeändert = true;

                    break;
                }
            }
        }

        public static void FahrzeugMotor(Vehicle Auto, int Motor)
        {
            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    auto.FahrzeugMotor = Motor;
                    auto.FahrzeugGeändert = true;
                    break;
                }
            }
        }

        public static int FahrzeugMotorStatus(Vehicle Auto)
        {
            //Benötigte Definitionen
            int Motor = 0;
            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    Motor = auto.FahrzeugMotor;
                    break;
                }
            }
            return Motor;
        }

        public static void FahrzeugKaufenAutohaus(Client Player, Vehicle Auto)
        {
            //Benötigte Definitionen
            String VerkaufsText = null;
            double Rechnen = 0.0;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    auto.FahrzeugAutohaus = Funktionen.AccountAutohausBekommen(Player);
                    auto.AutohausTextLabel.Delete();

                    Rechnen = auto.FahrzeugKaufpreis + auto.FahrzeugKaufpreis * 0.2;
                    auto.FahrzeugKaufpreis = (long)Rechnen;
                    auto.FahrzeugBeschreibung = "Autohaus";

                    VerkaufsText = "~r~[~w~Zu verkaufen~r~]~n~";
                    VerkaufsText += "Name~w~: " + auto.FahrzeugName + "~n~~r~";
                    VerkaufsText += "Autohaus~w~: " + auto.FahrzeugAutohaus + "~n~~r~";
                    VerkaufsText += "Preis:~w~ " + Funktionen.GeldFormatieren(auto.FahrzeugKaufpreis);

                    auto.AutohausTextLabel = NAPI.TextLabel.CreateTextLabel(VerkaufsText, new Vector3(auto.FahrzeugX, auto.FahrzeugY, auto.FahrzeugZ), 18.0f, 1.00f, 4, new Color(255, 255, 255), false, 0);

                    auto.FahrzeugGeändert = true;
                }
            }
        }

        public static long MietpreisBekommen(Vehicle Auto)
        {
            //Benötigte Definitionen
            long Mietpreis = 0;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    Mietpreis = auto.FahrzeugMietpreis;
                    break;
                }
            }
            return Mietpreis;
        }

        public static int IdBekommen(Vehicle Auto)
        {
            //Benötigte Definitionen
            int Id = 0;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    Id = auto.Id;
                    break;
                }
            }
            return Id;
        }

        public static long KaufPreisBekommen(Vehicle Auto)
        {
            //Benötigte Definitionen
            long KaufPreis = 0;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    KaufPreis = auto.FahrzeugKaufpreis;
                    break;
                }
            }
            return KaufPreis;
        }

        public static float TankInhaltBekommen(Vehicle Auto)
        {
            //Benötigte Definitionen
            float TankInhalt = 0;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    TankInhalt = auto.TankInhalt;
                    break;
                }
            }
            return TankInhalt;
        }

        public static float RotBekommen(Vehicle Auto)
        {
            //Benötigte Definitionen
            float Rot = 0.0f;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    Rot = auto.FahrzeugRot;
                }
            }
            return Rot;
        }

        public static String NameBekommen(Vehicle Auto)
        {
            //Benötigte Definitionen
            String Name = null;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    Name = auto.FahrzeugName;
                }
            }
            return Name;
        }

        public static void BesitzerSetzen(Vehicle Auto, int Besitzer)
        {
            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    auto.FahrzeugSpieler = Besitzer;
                    auto.FahrzeugGeändert = true;
                }
            }
        }

        public static void JobSetzen(Vehicle Auto, int Job)
        {
            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    auto.FahrzeugJob = Job;
                    auto.FahrzeugGeändert = true;
                }
            }
        }

        public static void AutohausSetzen(Vehicle Auto, int Autohaus)
        {
            String VerkaufsText = null;
            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    auto.FahrzeugAutohaus = Autohaus;
                    auto.FahrzeugGeändert = true;

                    if(Autohaus > 0)
                    {
                        VerkaufsText = "~r~[~w~Zu verkaufen~r~]~n~";
                        VerkaufsText += "Name~w~: " + auto.FahrzeugName + "~n~~r~";
                        VerkaufsText += "Autohaus~w~: " + Autohaus + "~n~~r~";
                        VerkaufsText += "Preis~w~: " + Funktionen.GeldFormatieren(auto.FahrzeugKaufpreis);
                    }
                    else if(Autohaus < 0)
                    {
                        VerkaufsText = "~r~[~w~Manufaktur~r~]~n~";
                        VerkaufsText += "Name~w~: " + auto.FahrzeugName + "~n~~r~";
                        VerkaufsText += "Preis~w~: " + Funktionen.GeldFormatieren(auto.FahrzeugKaufpreis);
                    }

                    auto.AutohausTextLabel = NAPI.TextLabel.CreateTextLabel(VerkaufsText, new Vector3(Auto.Position.X, Auto.Position.Y, Auto.Position.Z), 18.0f, 1.00f, 4, new Color(255, 255, 255), false, 0);
                }
            }
        }

        public static void FraktionSetzen(Vehicle Auto, int Fraktion)
        {
            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    auto.FahrzeugFraktion = Fraktion;
                    auto.FahrzeugGeändert = true;
                }
            }
        }

        public static void FahrzeugCheck()
        {
            //Benötigte Definitionen
            Boolean HatFahrer = false;
            foreach (var Fahrzeug in NAPI.Pools.GetAllVehicles())
            {
                if (Fahrzeug.EngineStatus == true)
                {
                    if (NAPI.Vehicle.GetVehicleDriver(Fahrzeug) == null)
                    {
                        foreach (var Spieler in NAPI.Pools.GetAllPlayers())
                        {
                            if(Spieler.Vehicle == Fahrzeug)
                            {
                                HatFahrer = true;
                                break;
                            }
                        }
                        if(HatFahrer == false)
                        {
                            foreach (AutoLokal auto in Funktionen.AutoListe)
                            {
                                if (Fahrzeug.GetData("Id") == auto.Id)
                                {
                                    if (auto.TankInhalt <= 0)
                                    {
                                        Fahrzeug.EngineStatus = false;
                                        FahrzeugMotor(Fahrzeug, 0);
                                        if (auto.TankInhalt < 0)
                                        {
                                            auto.TankInhalt = 0.0f;
                                        }
                                        return;
                                    }

                                    auto.TankInhalt -= 5;
                                    auto.FahrzeugGeändert = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void HelmutFahrzeugErstellen(Client Player)
        {
            //Definitionen
            uint AutoCode = NAPI.Util.GetHashKey("mk7");

            //Ein neues Objekt erzeugen
            var veh = new Auto
            {
                FahrzeugBeschreibung = "Nicos Fahrzeug",
                FahrzeugName = Funktionen.ErsterBuchstabeGroß("Golf mk7"),
                FahrzeugTyp = 2,
                FahrzeugFraktion = 0,
                FahrzeugJob = 0,
                FahrzeugSpieler = Player.GetData("Id"),
                FahrzeugMietpreis = 0,
                FahrzeugKaufpreis = 0,
                FahrzeugAutohaus = 0,
                FahrzeugX = Player.Position.X,
                FahrzeugY = Player.Position.Y,
                FahrzeugZ = Player.Position.Z,
                FahrzeugRot = Player.Rotation.Z,
                FahrzeugFarbe1 = 0,
                FahrzeugFarbe2 = 0,
                TankVolumen = Funktionen.TankVolumenBerechnen("Golf"),
                TankInhalt = Funktionen.TankVolumenBerechnen("Golf") * 10 * 100,
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
            

            //Objekt für die Liste erzeugen
            AutoLokal auto = new AutoLokal();

            //Das Fahrzeug spawnen
            auto.Fahrzeug = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), Player.Rotation.Z, 0, 0, numberPlate: "Nico");

            auto.Fahrzeug.NumberPlate = "Nico";
            auto.Fahrzeug.Dimension = 0;

            //Dem Fahrzeug die Werte lokal übergeben
            auto.Id = ContextFactory.Instance.srp_fahrzeuge.Max(x => x.Id);
            auto.FahrzeugBeschreibung = "Nicos Fahrzeug";
            auto.FahrzeugName = Funktionen.ErsterBuchstabeGroß("Golf mk7");
            auto.FahrzeugTyp = 2;
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
            auto.FahrzeugFarbe1 = 0;
            auto.FahrzeugFarbe2 = 0;
            auto.TankVolumen = Funktionen.TankVolumenBerechnen("Golf");
            auto.TankInhalt = Funktionen.TankVolumenBerechnen("Golf") * 10 * 100;
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
            auto.Fahrzeug.SetData("Id", ContextFactory.Instance.srp_fahrzeuge.Max(x => x.Id));
            auto.Fahrzeug.EngineStatus = true;
        }

        public static void TachoUpdaten()
        {
            //Schleife für alle Autos
            foreach (var Player in NAPI.Pools.GetAllPlayers())
            {
                //Abfrage ob eingeloggt und im Auto und auf dem Fahrersitz ist
                if(Player.IsInVehicle && Player.GetData("Eingeloggt") == 1 && NAPI.Player.GetPlayerVehicleSeat(Player) == -1 && Player.Vehicle.EngineStatus == true)
                {
                    //Benötigte Definitionen
                    float Distanz = 0.0f;
                    float nX, nY, nZ, aX, aY, aZ;

                    //Fahrzeug abprüfen
                    AutoLokal auto = new AutoLokal();
                    auto = Funktionen.AutoBekommen(Player.Vehicle);

                    if(auto.TankInhalt <= 0)
                    {
                        Player.Vehicle.EngineStatus = false;
                        FahrzeugMotor(Player.Vehicle, 0);
                        if(auto.TankInhalt < 0)
                        {
                            auto.TankInhalt = 0.0f;
                        }
                        return;
                    }

                    //Fahrzeug Position zuweisen
                    auto.FahrzeugNeuePositionX = Player.Vehicle.Position.X;
                    auto.FahrzeugNeuePositionY = Player.Vehicle.Position.Y;
                    auto.FahrzeugNeuePositionZ = Player.Vehicle.Position.Z;

                    //Neue Koordinaten an die Variablen geben
                    nX = Player.Vehicle.Position.X;
                    nY = Player.Vehicle.Position.Y;
                    nZ = Player.Vehicle.Position.Z;

                    //Alte Koordinaten an die Variablen geben
                    aX = auto.FahrzeugAltePositionX;
                    aY = auto.FahrzeugAltePositionY;
                    aZ = auto.FahrzeugAltePositionZ;

                    //Distanz mit der Vector3 Distance Funktion errechnen
                    Distanz = Vector3.Distance(new Vector3(nX, nY, nZ), new Vector3(aX, aY, aZ));

                    //Damit wir wissen ob der Spieler steht oder nicht
                    if(Distanz > 2)
                    {
                        Player.SetData("BewegtSichMitFahrzeug", 1);
                    }
                    else
                    {
                        Player.SetData("BewegtSichMitFahrzeug", 0);
                    }

                    //Kilometer und Tank aufsummieren
                    float Kilometer = auto.Kilometerstand + Distanz;

                    //Verbrauch errechnen
                    float Verbraucht = Distanz * GlobaleSachen.Verbrauch;

                    //Wenn er nicht fährt
                    if(Distanz == 0)
                    {
                        Verbraucht = 0.2f;
                    }

                    //Verbrauch subtrahieren
                    float Tank = auto.TankInhalt - Verbraucht;

                    //Dem Fahrzeug den Kilometerstand und Tank setzen
                    auto.Kilometerstand = Kilometer;
                    auto.TankInhalt = Tank;

                    //Für das Tacho den Kilometerstand runterteilen damit die Zahl lesbar ist
                    float KilometerFloat = auto.Kilometerstand / 10 / 100;
                    float TankFloat = auto.TankInhalt / 10 / 100;
                    float TankVolumenFloat = auto.TankVolumen;

                    //Client Event vom Tacho triggern (Tacho/index.js)
                    Player.TriggerEvent("TachoUpdaten", NAPI.Util.ToJson(Math.Round(KilometerFloat, 2)), NAPI.Util.ToJson(Math.Round(TankFloat, 2)), NAPI.Util.ToJson(Math.Round(TankVolumenFloat, 2)));

                    //Vom Fahrzeug die alte Position auf die neue updaten
                    auto.FahrzeugAltePositionX = auto.FahrzeugNeuePositionX;
                    auto.FahrzeugAltePositionY = auto.FahrzeugNeuePositionY;
                    auto.FahrzeugAltePositionZ = auto.FahrzeugNeuePositionZ;

                    auto.FahrzeugGeändert = true;
                }
            }
        }
    }
}
