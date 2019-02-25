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
            if (TypBekommen(Player.Vehicle) == 2)
            {
                if(NameBekommen(Player.Vehicle) == "faggio")
                {
                    Player.TriggerEvent("RollerSpeed");
                }

                //Chat weg
                Player.TriggerEvent("Chathiden");

                Player.TriggerEvent("rollermietenpopupoeffnen", Funktionen.GeldFormatieren(MietpreisBekommen(Player.Vehicle)));
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

                //Die eigentliche Erstellung des Fahrzeuges
                auto.Fahrzeug = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Fahrzeuge.FahrzeugX, Fahrzeuge.FahrzeugY, Fahrzeuge.FahrzeugZ), Fahrzeuge.FahrzeugRot, Fahrzeuge.FahrzeugFarbe1, Fahrzeuge.FahrzeugFarbe2);

                //ID Lokal setzen damit man das Fahrzeug erkennen kann
                auto.Fahrzeug.SetData("Id", Fahrzeuge.Id);

                //Zuweisungen für das Auto
                auto.Fahrzeug.NumberPlate = Fahrzeuge.FahrzeugBeschreibung;
                auto.Fahrzeug.EngineStatus = false;
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

                //Nur lokal
                auto.FahrzeugAltePositionX = Fahrzeuge.FahrzeugX;
                auto.FahrzeugAltePositionY = Fahrzeuge.FahrzeugY;
                auto.FahrzeugAltePositionZ = Fahrzeuge.FahrzeugZ;
                auto.FahrzeugNeuePositionX = 0;
                auto.FahrzeugNeuePositionY = 0;
                auto.FahrzeugNeuePositionZ = 0;

                AutoListe.Add(auto);
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

        public static long MietpreisBekommen(Vehicle Auto)
        {
            //Benötigte Definitionen
            long Mietpreis = 0;

            foreach (AutoLokal auto in Funktionen.AutoListe)
            {
                if (Auto.GetData("Id") == auto.Id)
                {
                    Mietpreis = auto.FahrzeugMietpreis;
                }
            }
            return Mietpreis;
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
                if(Player.IsInVehicle && Player.GetData("Eingeloggt") == 1 && NAPI.Player.GetPlayerVehicleSeat(Player) == -1)
                {
                    //Benötigte Definitionen
                    float Distanz = 0.0f;
                    float nX, nY, nZ, aX, aY, aZ;

                    //Fahrzeug abprüfen
                    AutoLokal auto = new AutoLokal();
                    auto = Funktionen.AutoBekommen(Player);

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
