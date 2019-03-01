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
            auto = Funktionen.AutoBekommen(Player);

            if(Funktionen.AccountGeldBekommen(Player) < auto.FahrzeugMietpreis)
            {
                Player.SendChatMessage("~y~Info~w~: Du hast nicht genug Geld.");
                Player.TriggerEvent("rollermietenpopupschliessen");

                //Chat an
                Player.TriggerEvent("Chatzeigen");
            }
            else
            {
                long GeldAbzug = Funktionen.AccountGeldBekommen(Player) - auto.FahrzeugMietpreis;

                Player.SendChatMessage("~y~Info~w~: Du hast das Fahrzeug gemietet.");
                Funktionen.AccountGeldSetzen(Player, GeldAbzug);
                Player.TriggerEvent("rollermietenpopupschliessen");

                //Chat an
                Player.TriggerEvent("Chatzeigen");
            }
        }

        [RemoteEvent("RollerNichtMieten")]
        public void RollerNichtMieten(Client Player)
        {
            Player.TriggerEvent("rollermietenpopupschliessen");

            //Chat an
            Player.TriggerEvent("Chatzeigen");
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client Player, Vehicle vehicle, sbyte seatID)
        {
            //Motor Sachen
            if(FahrzeugMotorStatus(Player.Vehicle) == 1)
            {
                Player.SendChatMessage("Motor war an");
                Player.Vehicle.EngineStatus = true;
            }
            else
            {
                Player.SendChatMessage("Motor war aus");
                Player.Vehicle.EngineStatus = false;
            }

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

                if(Player.GetData("VerwaltungsModus") == 0)
                {
                    //Chat weg
                    Player.TriggerEvent("Chathiden");

                    Player.TriggerEvent("rollermietenpopupoeffnen", Funktionen.GeldFormatieren(MietpreisBekommen(Player.Vehicle)));
                }
            }
            //Autohausfahrzeug
            else if (TypBekommen(Player.Vehicle) == 5 && AutoHausBekommen(Player.Vehicle) > 0)
            {
                if (Player.GetData("VerwaltungsModus") == 0)
                {
                    if (Player.GetData("KaufenTyp") == 5) { Player.SendChatMessage("~y~Info~w~: Schließe erst das aktuelle Fenster."); return; }
                    Player.SetData("KaufenTyp", 5);
                    Player.SetData("KaufenId", IdBekommen(Player.Vehicle));
                    Player.SetData("KaufenPreis", KaufPreisBekommen(Player.Vehicle));

                    //Freezen
                    Funktionen.Freeze(Player);

                    //Chat weg
                    Player.TriggerEvent("Chathiden");

                    Player.TriggerEvent("Kaufen", 5, Funktionen.GeldFormatieren(KaufPreisBekommen(Player.Vehicle)));
                }
            }
            //Manufaktur Fahrzeuge
            else if (AutoHausBekommen(Player.Vehicle) == -1 && Funktionen.AccountHatAutohaus(Player) == 1)
            {
                if (Player.GetData("VerwaltungsModus") == 0)
                {
                    if (Player.GetData("KaufenTyp") == 6) { Player.SendChatMessage("~y~Info~w~: Schließe erst das aktuelle Fenster."); return; }
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

                    //Chat weg
                    Player.TriggerEvent("Chathiden");

                    Player.TriggerEvent("Kaufen", 6, Funktionen.GeldFormatieren(KaufPreisBekommen(Player.Vehicle)), Funktionen.GeldFormatieren(Rechnen1));
                }
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

                //Zuweisungen für das Auto
                auto.Fahrzeug.NumberPlate = Fahrzeuge.FahrzeugBeschreibung;
                if(auto.FahrzeugMotor == 0)
                {
                    auto.Fahrzeug.EngineStatus = false;
                }
                else
                {
                    auto.Fahrzeug.EngineStatus = true;
                }
                auto.Fahrzeug.Dimension = NAPI.GlobalDimension;

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

                //Schauen ob es ein Verkaufs Auto ist
                if (auto.FahrzeugAutohaus > 0)
                {
                    VerkaufsText = "~r~[~w~Zu verkaufen~r~]~n~";
                    VerkaufsText += "Name~w~: " + auto.FahrzeugName + "~n~~r~";
                    VerkaufsText += "Autohaus~w~: " + auto.FahrzeugAutohaus + "~n~~r~";
                    VerkaufsText += "Preis:~w~ " + Funktionen.GeldFormatieren(auto.FahrzeugKaufpreis);

                    auto.AutohausTextLabel = NAPI.TextLabel.CreateTextLabel(VerkaufsText, new Vector3(Fahrzeuge.FahrzeugX, Fahrzeuge.FahrzeugY, Fahrzeuge.FahrzeugZ), 18.0f, 1.00f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
                }
                if (auto.FahrzeugAutohaus < 0)
                {
                    VerkaufsText = "~r~[~w~Manufaktur~r~]~n~";
                    VerkaufsText += "Name~w~: " + auto.FahrzeugName + "~n~~r~";
                    VerkaufsText += "Preis:~w~ " + Funktionen.GeldFormatieren(auto.FahrzeugKaufpreis);

                    auto.AutohausTextLabel = NAPI.TextLabel.CreateTextLabel(VerkaufsText, new Vector3(Fahrzeuge.FahrzeugX, Fahrzeuge.FahrzeugY, Fahrzeuge.FahrzeugZ), 18.0f, 1.00f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
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

                    auto.AutohausTextLabel = NAPI.TextLabel.CreateTextLabel(VerkaufsText, new Vector3(auto.FahrzeugX, auto.FahrzeugY, auto.FahrzeugZ), 18.0f, 1.00f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);

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

        public static float XBekommen(Vehicle Auto)
        {
            //Benötigte Definitionen
            float X = 0.0f;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    X = auto.FahrzeugX;
                }
            }
            return X;
        }

        public static float YBekommen(Vehicle Auto)
        {
            //Benötigte Definitionen
            float Y = 0.0f;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    Y = auto.FahrzeugX;
                }
            }
            return Y;
        }

        public static float ZBekommen(Vehicle Auto)
        {
            //Benötigte Definitionen
            float Z = 0.0f;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    Z = auto.FahrzeugX;
                }
            }
            return Z;
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

                    auto.AutohausTextLabel = NAPI.TextLabel.CreateTextLabel(VerkaufsText, new Vector3(Auto.Position.X, Auto.Position.Y, Auto.Position.Z), 18.0f, 1.00f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
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
                    auto = Funktionen.AutoBekommen(Player);

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

                    //Kilometer und Tank aufsummieren
                    float Kilometer = auto.Kilometerstand + Distanz;

                    //Verbrauch errechnen
                    float Verbraucht = Distanz * GlobaleSachen.Verbrauch;

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
