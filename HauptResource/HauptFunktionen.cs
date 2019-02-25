using GTANetworkAPI;
using Datenbank;
using Fahrzeug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace Haupt
{
    public class GlobaleSachen
    {
        //Datenbankverbindung
        //public const String Verbindung = "Server=127.0.0.1; Database=strawberryrp_server; Uid=strawberryrpserver; Pwd=rElciatks7Pn7DpH";
        public const String Verbindung = "Server=localhost; Database=strawberryrp_server; Uid=root;Pwd=";

        //Verbrauch
        public const float Verbrauch = 0.7f;

        //Wetter
        public static int ServerWetter = 0;

        //Marker
        public const int TankstellenMarker = 0;
        public const int TankstellenZapfsäuleMarker = 20;
        public const int TankstellenInfoMarker = 20;
        public const int ImmobilienMarker = 0;
        public const int SupermarktMarker = 0;
    }

    public class AdminBefehle
    {
        //Hier kann man die Berechtigungen für die Befehle ändern
        public const int AdminGeben = 5;
        public const int GeldGeben = 5;
        public const int TankeBeschreibung = 4;
        public const int TankeKaufpreis = 4;
        public const int FahrzeugErstellen = 4;
        public const int FahrzeugLöschen = 4;
        public const int FahrzeugMietPreis = 4;
        public const int FahrzeugKaufPreis = 4;
        public const int HausErstellen = 4;
        public const int HausLöschen = 4;
        public const int TankeErstellen = 4;
        public const int SupermarktErstellen = 4;
        public const int SupermarktLöschen = 4;
        public const int TankPunktErstellen = 4;
        public const int TankeLöschen = 4;
        public const int TankPunktLöschen = 4;
        public const int TankInfoLöschen = 4;
        public const int TankInfoErstellen = 4;
        public const int Waffe = 4;
        public const int Parken = 3;
        public const int Fahrzeugporten = 3;
        public const int Teleporten = 1;
        public const int AdminChat = 1;
        public const int Fahrzeugrespawn = 3;
        public const int Fahrzeugreparieren = 3;
        public const int Fahrzeugzuweisen = 3;
    }

    public class Zahlen
    {
        //Globale Zahlen
        public const int Jobs = 3;
        public const int Fraktionen = 3;
    }

    public class Funktionen : Script
    {
        //Listen für Dynamische Systeme
        public static List<AccountLokal> AccountListe;
        public static List<ImmobilienLokal> ImmobilienListe;
        public static List<AutoLokal> AutoListe;
        public static List<TankstelleLokal> TankenListe;
        public static List<TankstellenPunktLokal> TankenPunktListe;
        public static List<TankstellenInfoLokal> TankenInfoListe;
        public static List<SupermarktLokal> SupermarktListe;

        public static void AllesStarten()
        {
            //Globaler Server Chat aus
            NAPI.Server.SetGlobalServerChat(false);

            //Wichtige Dinge die geladen werden müssen
            AccountsLadenLokal();
            FahrzeugeLadenLokal();
            TextLabelsLaden();
            BlipsLaden();
            MarkersLaden();
            InteriorsLaden();
            TankstellenLadenLokal();
            TankstellenPunkteLadenLokal();
            TankstellenInfoLadenLokal();
            ImmobilienLadenLokal();
            SupermärkteLadenLokal();

            //Speicherungs Timer
            Timer.SetTimer(AlleSpielerSpeichern, 10000, 0);
            Timer.SetTimer(FahrzeugeSpeichern, 10000, 0);
            Timer.SetTimer(AlleTankstellenSpeichern, 10000, 0);
            Timer.SetTimer(AlleSupermärkteSpeichern, 10000, 0);
            Timer.SetTimer(AlleImmobilienSpeichern, 10000, 0);

            //Update Timer
            Timer.SetTimer(AlleTankstellenUpdaten, 10000, 0);
            Timer.SetTimer(AlleSupermärkteUpdaten, 10000, 0);
            Timer.SetTimer(AlleImmobilienUpdaten, 10000, 0);

            //Spieler Timer
            Timer.SetTimer(Alle, 1000, 0);

            //Sonstige Timer
            Timer.SetTimer(WetterAendern, 3600000, 0);
            Timer.SetTimer(WetterStation, 900000, 0);
            Timer.SetTimer(Uhrzeit, 1000, 0);
            Timer.SetTimer(Fahrzeuge.TachoUpdaten, 200, 0);
            WetterAendern();

            //Daten aus den Datenbanken zählen
            var Accounts = ContextFactory.Instance.srp_accounts.Count();
            var Log = ContextFactory.Instance.srp_log.Count();
            var Fahzeuge = ContextFactory.Instance.srp_fahrzeuge.Count();
            var Tankstellen = ContextFactory.Instance.srp_tankstellen.Count();
            var TankstellenPunkte = ContextFactory.Instance.srp_tankstellenpunkte.Count();
            var TankstellenInfoPunkte = ContextFactory.Instance.srp_tankstelleninfo.Count();
            var Immobilien = ContextFactory.Instance.srp_immobilien.Count();
            var Supermärkte = ContextFactory.Instance.srp_supermärkte.Count();

            //Gezählte Werte in der Log ausgeben
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Accounts + " Accounts wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Log + " Log Einträge wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Fahzeuge + " Fahrzeuge wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Tankstellen + " Tankstellen wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + TankstellenPunkte + " Tankstellen Punkte wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + TankstellenInfoPunkte + " Tankstellen Info Punkte wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Immobilien + " Immobilien wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Supermärkte + " Supermärkte wurden geladen.");
        }

        public static void TankstellenLadenLokal()
        {
            TankenListe = AlleTankstellenLadenDB();

            foreach (TankstelleLokal tankstelle in TankenListe)
            {
                String TankstellenText = null;
                if (tankstelle.TankstelleBesitzer == 0)
                {
                    TankstellenText = "~g~[~w~Tankstelle ID: " + tankstelle.Id + "~g~]~n~";
                    TankstellenText += "~w~Kaufpreis: " + GeldFormatieren(tankstelle.TankstelleKaufpreis) + "~n~";
                    TankstellenText += "Beschreibung: " + tankstelle.TankstelleBeschreibung;
                    tankstelle.TankstellenBlip = NAPI.Blip.CreateBlip(new Vector3(tankstelle.TankstelleX, tankstelle.TankstelleY, tankstelle.TankstelleZ));
                    tankstelle.TankstellenBlip.Name = tankstelle.TankstelleBeschreibung;
                    tankstelle.TankstellenBlip.ShortRange = true;
                    tankstelle.TankstellenBlip.Sprite = 361;
                    tankstelle.TankstellenBlip.Color = 2;
                }
                else
                {
                    TankstellenText = "~g~[~w~Tankstelle ID: " + tankstelle.Id + "~g~]~n~";
                    TankstellenText += "~w~Beschreibung: " + tankstelle.TankstelleBeschreibung + "~n~";
                    TankstellenText += "Besitzer: " + BesitzerNamenBekommen(tankstelle.TankstelleBesitzer);
                    tankstelle.TankstellenBlip = NAPI.Blip.CreateBlip(new Vector3(tankstelle.TankstelleX, tankstelle.TankstelleY, tankstelle.TankstelleZ));
                    tankstelle.TankstellenBlip.Name = tankstelle.TankstelleBeschreibung;
                    tankstelle.TankstellenBlip.ShortRange = true;
                    tankstelle.TankstellenBlip.Sprite = 361;
                    tankstelle.TankstellenBlip.Color = 1;
                }

                //TextLabel und Marker erstellen
                tankstelle.TankstellenLabel = NAPI.TextLabel.CreateTextLabel(TankstellenText, new Vector3(tankstelle.TankstelleX, tankstelle.TankstelleY, tankstelle.TankstelleZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
                tankstelle.TankstellenMarker = NAPI.Marker.CreateMarker(GlobaleSachen.TankstellenMarker, new Vector3(tankstelle.TankstelleX, tankstelle.TankstelleY, tankstelle.TankstelleZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
            }
        }

        public static void ImmobilienLadenLokal()
        {
            ImmobilienListe = AlleImmobilienLadenDB();

            foreach (ImmobilienLokal haus in ImmobilienListe)
            {
                String ImmobilienText = null;
                if (haus.ImmobilienBesitzer == 0)
                {
                    ImmobilienText = "~g~[~w~Hausnummer: " + haus.Id + "~g~]~n~";
                    ImmobilienText += "~w~Kaufpreis: " + GeldFormatieren(haus.ImmobilienKaufpreis) + "~n~";
                    ImmobilienText += "~w~Beschreibung: " + haus.ImmobilienBeschreibung;
                    haus.ImmobilienBlip = NAPI.Blip.CreateBlip(new Vector3(haus.ImmobilienX, haus.ImmobilienY, haus.ImmobilienZ));
                    haus.ImmobilienBlip.Name = haus.ImmobilienBeschreibung;
                    haus.ImmobilienBlip.ShortRange = true;
                    haus.ImmobilienBlip.Sprite = 40;
                    haus.ImmobilienBlip.Color = 2;
                }
                else
                {
                    ImmobilienText = "~g~[~w~Hausnummer: " + haus.Id + "~g~]~n~";
                    ImmobilienText += "~w~Beschreibung: " + haus.ImmobilienBeschreibung + "~n~";
                    ImmobilienText += "~w~Besitzer: " + BesitzerNamenBekommen(haus.ImmobilienBesitzer);
                    haus.ImmobilienBlip = NAPI.Blip.CreateBlip(new Vector3(haus.ImmobilienX, haus.ImmobilienY, haus.ImmobilienZ));
                    haus.ImmobilienBlip.Name = haus.ImmobilienBeschreibung;
                    haus.ImmobilienBlip.ShortRange = true;
                    haus.ImmobilienBlip.Sprite = 40;
                    haus.ImmobilienBlip.Color = 1;
                }

                //TextLabel und Marker erstellen
                haus.ImmobilienLabel = NAPI.TextLabel.CreateTextLabel(ImmobilienText, new Vector3(haus.ImmobilienX, haus.ImmobilienY, haus.ImmobilienZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
                haus.ImmobilienMarker = NAPI.Marker.CreateMarker(GlobaleSachen.ImmobilienMarker, new Vector3(haus.ImmobilienX, haus.ImmobilienY, haus.ImmobilienZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
            }
        }

        public static void SupermärkteLadenLokal()
        {
            SupermarktListe = AlleSupermärkteLadenDB();

            foreach (SupermarktLokal supermarkt in SupermarktListe)
            {
                String SupermarktText = null;
                if (supermarkt.SupermarktBesitzer == 0)
                {
                    SupermarktText = "~g~[~w~24/7: " + supermarkt.Id + "~g~]~n~";
                    SupermarktText += "~w~Kaufpreis: " + GeldFormatieren(supermarkt.SupermarktKaufpreis) + "~n~";
                    SupermarktText += "~w~Beschreibung: " + supermarkt.SupermarktBeschreibung;
                    supermarkt.SupermarktBlip = NAPI.Blip.CreateBlip(new Vector3(supermarkt.SupermarktX, supermarkt.SupermarktY, supermarkt.SupermarktZ));
                    supermarkt.SupermarktBlip.Name = supermarkt.SupermarktBeschreibung;
                    supermarkt.SupermarktBlip.ShortRange = true;
                    supermarkt.SupermarktBlip.Sprite = 590;
                    supermarkt.SupermarktBlip.Color = 2;
                }
                else
                {
                    SupermarktText = "~g~[~w~24/7: " + supermarkt.Id + "~g~]~n~";
                    SupermarktText += "~w~Beschreibung: " + supermarkt.SupermarktBeschreibung + "~n~";
                    SupermarktText += "~w~Besitzer: " + BesitzerNamenBekommen(supermarkt.SupermarktBesitzer);
                    supermarkt.SupermarktBlip = NAPI.Blip.CreateBlip(new Vector3(supermarkt.SupermarktX, supermarkt.SupermarktY, supermarkt.SupermarktZ));
                    supermarkt.SupermarktBlip.Name = supermarkt.SupermarktBeschreibung;
                    supermarkt.SupermarktBlip.ShortRange = true;
                    supermarkt.SupermarktBlip.Sprite = 590;
                    supermarkt.SupermarktBlip.Color = 1;
                }

                //TextLabel und Marker erstellen
                supermarkt.SupermarktLabel = NAPI.TextLabel.CreateTextLabel(SupermarktText, new Vector3(supermarkt.SupermarktX, supermarkt.SupermarktY, supermarkt.SupermarktZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
                supermarkt.SupermarktMarker = NAPI.Marker.CreateMarker(GlobaleSachen.SupermarktMarker, new Vector3(supermarkt.SupermarktX, supermarkt.SupermarktY, supermarkt.SupermarktZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
            }
        }

        public static void FahrzeugeLadenLokal()
        {
            AutoListe = Fahrzeuge.AlleAutosLadenDB();
        }

        public static void AccountsLadenLokal()
        {
            AccountListe = AlleAccountsLadenDB();
        }

        public static void TankstellenPunkteLadenLokal()
        {
            TankenPunktListe = AlleTankstellenPunkteLadenDB();

            foreach (TankstellenPunktLokal tankpunkt in TankenPunktListe)
            {
                String TankstellenPunktText = "~g~[~w~Zapfsäule ID: " + tankpunkt.Id + "~g~]~n~";
                TankstellenPunktText += "~w~Tankstelle: ~g~" + tankpunkt.TankstellenId;

                //TextLabel und Marker erstellen
                tankpunkt.TankstellenPunktLabel = NAPI.TextLabel.CreateTextLabel(TankstellenPunktText, new Vector3(tankpunkt.TankstellenPunktX, tankpunkt.TankstellenPunktY, tankpunkt.TankstellenPunktZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
                tankpunkt.TankstellenPunktMarker = NAPI.Marker.CreateMarker(GlobaleSachen.TankstellenZapfsäuleMarker, new Vector3(tankpunkt.TankstellenPunktX, tankpunkt.TankstellenPunktY, tankpunkt.TankstellenPunktZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
            }
        }

        public static void TankstellenInfoLadenLokal()
        {
            //Benötigte Definitionen
            int Diesel = 0, E10 = 0, Super = 0;

            TankenInfoListe = AlleTankstellenInfoLadenDB();

            foreach (TankstellenInfoLokal tankinfo in TankenInfoListe)
            {
                foreach (TankstelleLokal tankstelle in TankenListe)
                {
                    if(tankinfo.TankstellenInfoId == tankstelle.Id)
                    {
                        Diesel = tankstelle.TankstelleDiesel;
                        E10 = tankstelle.TankstelleE10;
                        Super = tankstelle.TankstelleSuper;
                    }
                }

                String TankstellenInfoText = "~g~[~w~Für ID: " + tankinfo.TankstellenInfoId + "~g~]~n~";
                TankstellenInfoText += "~w~Diesel: ~g~" + Diesel + "~w~L~n~";
                TankstellenInfoText += "~w~E10: ~g~" + E10 + "~w~L~n~";
                TankstellenInfoText += "~w~Super: ~g~" + Super + "~w~L";

                //TextLabel und Marker erstellen
                tankinfo.TankstellenInfoLabel = NAPI.TextLabel.CreateTextLabel(TankstellenInfoText, new Vector3(tankinfo.TankstellenInfoX, tankinfo.TankstellenInfoY, tankinfo.TankstellenInfoZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
                tankinfo.TankstellenInfoMarker = NAPI.Marker.CreateMarker(GlobaleSachen.TankstellenInfoMarker, new Vector3(tankinfo.TankstellenInfoX, tankinfo.TankstellenInfoY, tankinfo.TankstellenInfoZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
            }
        }

        public static TankstelleLokal NaheTankeBekommen(Client Player, float distance = 2.0f)
        {
            TankstelleLokal Tankstelle = null;
            foreach (TankstelleLokal tankstellelocal in TankenListe)
            {
                if (Player.Position.DistanceTo(new Vector3(tankstellelocal.TankstelleX, tankstellelocal.TankstelleY, tankstellelocal.TankstelleZ)) < distance)
                {
                    Tankstelle = tankstellelocal;
                }
            }
            return Tankstelle;
        }

        public static SupermarktLokal NaheSupermarktBekommen(Client Player, float distance = 2.0f)
        {
            SupermarktLokal Supermarkt = null;
            foreach (SupermarktLokal supermarktlocal in SupermarktListe)
            {
                if (Player.Position.DistanceTo(new Vector3(supermarktlocal.SupermarktX, supermarktlocal.SupermarktY, supermarktlocal.SupermarktZ)) < distance)
                {
                    Supermarkt = supermarktlocal;
                }
            }
            return Supermarkt;
        }

        public static TankstellenPunktLokal NaheTankePunktBekommen(Client Player, float distance = 4.0f)
        {
            TankstellenPunktLokal TankstellenPunkt = null;
            foreach (TankstellenPunktLokal tankstellenpunktlocal in TankenPunktListe)
            {
                if (Player.Position.DistanceTo(new Vector3(tankstellenpunktlocal.TankstellenPunktX, tankstellenpunktlocal.TankstellenPunktY, tankstellenpunktlocal.TankstellenPunktZ)) < distance)
                {
                    TankstellenPunkt = tankstellenpunktlocal;
                }
            }
            return TankstellenPunkt;
        }

        public static AutoLokal NaheAutosBekommen(Client Player, float distance = 2.0f)
        {
            AutoLokal Auto = null;
            foreach (var Fahrzeuge in NAPI.Pools.GetAllVehicles())
            {
                if (Player.Position.DistanceTo(new Vector3(Fahrzeuge.Position.X, Fahrzeuge.Position.Y, Fahrzeuge.Position.Z)) < distance)
                {
                    //Benötigte Definitionen
                    int FahrzeugID = Fahrzeuge.GetData("Id");

                    foreach (AutoLokal autolocal in AutoListe)
                    {
                        if(FahrzeugID == autolocal.Id)
                        {
                            Auto = autolocal;
                        }
                    }
                }
            }
            return Auto;
        }

        public static AutoLokal NaheEigenesAutoBekommenPrivat(Client Player, float distance = 2.0f)
        {
            AutoLokal Auto = null;
            foreach (var Fahrzeuge in NAPI.Pools.GetAllVehicles())
            {
                if (Player.Position.DistanceTo(new Vector3(Fahrzeuge.Position.X, Fahrzeuge.Position.Y, Fahrzeuge.Position.Z)) < distance)
                {
                    //Benötigte Definitionen
                    int FahrzeugID = Fahrzeuge.GetData("Id");

                    foreach (AutoLokal autolocal in AutoListe)
                    {
                        if (FahrzeugID == autolocal.Id && autolocal.FahrzeugSpieler == Player.GetData("Id"))
                        {
                            Auto = autolocal;
                            break;
                        }
                    }
                }
            }
            return Auto;
        }

        public static ImmobilienLokal NaheImmobilieBekommen(Client Player, float distance = 2.0f)
        {
            ImmobilienLokal haus = null;
            foreach (ImmobilienLokal hauslocal in ImmobilienListe)
            {
                if (Player.Position.DistanceTo(new Vector3(hauslocal.ImmobilienX, hauslocal.ImmobilienY, hauslocal.ImmobilienZ)) < distance)
                {
                    haus = hauslocal;
                }
                else if (Player.Position.DistanceTo(new Vector3(hauslocal.ImmobilienEingangX, hauslocal.ImmobilienEingangY, hauslocal.ImmobilienEingangZ)) < distance && NAPI.Entity.GetEntityDimension(Player) == hauslocal.Id)
                {
                    haus = hauslocal;
                }
            }
            return haus;
        }

        public static AutoLokal AutoBekommen(Client Player)
        {
            AutoLokal Auto = null;
            foreach (AutoLokal auto in AutoListe)
            {
                if (Player.Vehicle.GetData("Id") == auto.Id)
                {
                    Auto = auto;
                }
            }
            return Auto;
        }

        public static AutoLokal AutoVonIdBekommen(Client Player, int Id)
        {
            AutoLokal Auto = null;
            foreach (AutoLokal auto in AutoListe)
            {
                if (auto.Id == Id)
                {
                    Auto = auto;
                }
            }
            return Auto;
        }

        public static TankstelleLokal TankeVonIdBekommen(Client Player, int Id)
        {
            TankstelleLokal Tanke = null;
            foreach (TankstelleLokal tanke in TankenListe)
            {
                if (tanke.Id == Id)
                {
                    Tanke = tanke;
                }
            }
            return Tanke;
        }

        public static AccountLokal AccountBekommen(Client Player)
        {
            AccountLokal acc = null;
            foreach (AccountLokal account in AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    acc = account;
                }
            }
            return acc;
        }

        public static int AccountAdminLevelBekommen(Client Player)
        {
            //Benötigte Definitionen
            int AdminLevel = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    AdminLevel = account.AdminLevel;
                }
            }
            return AdminLevel;
        }

        public static int AccountPersoBekommen(Client Player)
        {
            //Benötigte Definitionen
            int Perso = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    Perso = account.Perso;
                }
            }
            return Perso;
        }

        public static long AccountGeldBekommen(Client Player)
        {
            //Benötigte Definitionen
            long Geld = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    Geld = account.Geld;
                }
            }
            return Geld;
        }

        public static long AccountBankGeldBekommen(Client Player)
        {
            //Benötigte Definitionen
            long BankGeld = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    BankGeld = account.BankGeld;
                }
            }
            return BankGeld;
        }

        public static long AccountFahrzeugSchlüsselBekommen(Client Player)
        {
            //Benötigte Definitionen
            long FahrzeugSchlüssel = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    FahrzeugSchlüssel = account.FahrzeugSchlüssel;
                }
            }
            return FahrzeugSchlüssel;
        }

        public static void AccountGeldSetzen(Client Player, long Geld)
        {
            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    account.Geld = Geld;
                    account.AccountGeändert = true;
                }
            }
        }

        public static void AccountAdminLevelSetzen(Client Player, int AdminLevel)
        {
            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    account.AdminLevel = AdminLevel;
                    account.AccountGeändert = true;
                }
            }
        }

        public static String AccountSocialClubBekommen(Client Player)
        {
            //Benötigte Definitionen
            String SocialClub = null;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    SocialClub = account.SocialClub;
                }
            }
            return SocialClub;
        }

        public static DateTime AccountEinreiseDatumBekommen(Client Player)
        {
            //Benötigte Definitionen
            DateTime EinreiseDatum = DateTime.Now;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    EinreiseDatum = account.EinreiseDatum;
                }
            }
            return EinreiseDatum;
        }

        public static TankstellenInfoLokal NaheTankeInfoBekommen(Client Player, float distance = 2.0f)
        {
            TankstellenInfoLokal TankstellenInfoPunkt = null;
            foreach (TankstellenInfoLokal tankstelleninfolocal in TankenInfoListe)
            {
                if (Player.Position.DistanceTo(new Vector3(tankstelleninfolocal.TankstellenInfoX, tankstelleninfolocal.TankstellenInfoY, tankstelleninfolocal.TankstellenInfoZ)) < distance)
                {
                    TankstellenInfoPunkt = tankstelleninfolocal;
                }
            }
            return TankstellenInfoPunkt;
        }

        public static int FahrzeugeZählenPrivat(Client Player)
        {
            int i = 0;
            foreach (AutoLokal auto in AutoListe)
            {
                if (auto.FahrzeugSpieler == Player.GetData("Id"))
                {
                    i += 1;
                }
            }
            return i;
        }

        public static string BesitzerNamenBekommen(int Id)
        {
            //Benötigte Definitionen
            string Name = null;

            //Den Account des Besitzers finden
            var Account = ContextFactory.Instance.srp_accounts.Where(x => x.Id == Id).FirstOrDefault();

            //Den Nicknamen dem String zuweisen
            if (Id == 0)
            {
                Name = "Niemand";
            }
            else
            {
                Name = Account.NickName;
            }

            //Alles wieder zurück geben
            return Name;
        }

        public static void Uhrzeit()
        {
            //Benötigte Definitionen
            var Zeit = DateTime.Now;

            //Uhrzeit setzen
            NAPI.World.SetTime(Zeit.Hour, Zeit.Minute, 0);
        }

        public static void Alle()
        {
            foreach (var Spieler in NAPI.Pools.GetAllPlayers())
            {
                if(Spieler.GetData("AmTanken") == 1 && Spieler.GetData("TankRechnung") > 0)
                {
                    foreach (TankstelleLokal tankstellelocal in TankenListe)
                    {
                        if(tankstellelocal.Id == Spieler.GetData("TankenTankstellenId"))
                        {
                            if (Spieler.Position.DistanceTo(new Vector3(tankstellelocal.TankstelleX, tankstellelocal.TankstelleY, tankstellelocal.TankstelleZ)) > 50)
                            {
                                Spieler.SendChatMessage("~y~Info~w~: Die Polizei wurde darüber benachrichtigt das du deine Rechnung nicht bezahlt hast.");
                                Spieler.SetData("AmTanken", 0);
                                Spieler.SetData("TankenTankstellenId", 0);
                                Spieler.SetData("TankRechnung", 0);
                            }
                        }
                    }
                }
            }
        }

        public static void WetterAendern()
        {
            //Definitionen
            string MonatString = DateTime.Now.ToString("MM");
            int Monat = int.Parse(MonatString);
            Random random = new Random();
            String Wetter = null;

            //Zeit Abfragen
            var Zeit = DateTime.Now;

            //Random Funktion
            int Random = random.Next(1, 5);

            switch (Monat)
            {
                case 1:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 11; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 10; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 12; }
                    break;
                case 2:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 6; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 8; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 9; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 6; }
                    break;
                case 3:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 5; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 8; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 9; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 9; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 6; }
                    break;
                case 4:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 2; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 9; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 2; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 1; }
                    break;
                case 5:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 2; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 11; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 10; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 12; }
                    break;
                case 6:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 11; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 10; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 12; }
                    break;
                case 7:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 11; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 10; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 12; }
                    break;
                case 8:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 11; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 10; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 12; }
                    break;
                case 9:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 11; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 10; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 12; }
                    break;
                case 10:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 11; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 10; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 12; }
                    break;
                case 11:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 11; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 10; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 12; }
                    break;
                case 12:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 13; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 11; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 10; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 12; }
                    break;
            }

            switch (GlobaleSachen.ServerWetter)
            {
                case 0:
                    Wetter = "EXTRASUNNY";
                    break;
                case 1:
                    Wetter = "CLEAR";
                    break;
                case 2:
                    Wetter = "CLOUDS";
                    break;
                case 3:
                    Wetter = "SMOG";
                    break;
                case 4:
                    Wetter = "FOGGY";
                    break;
                case 5:
                    Wetter = "OVERCAST";
                    break;
                case 6:
                    Wetter = "RAIN";
                    break;
                case 7:
                    Wetter = "THUNDER";
                    break;
                case 8:
                    Wetter = "CLEARING";
                    break;
                case 9:
                    Wetter = "NEUTRAL";
                    break;
                case 10:
                    Wetter = "SNOW";
                    break;
                case 11:
                    Wetter = "BLIZZARD";
                    break;
                case 12:
                    Wetter = "SNOWLIGHT";
                    break;
                case 13:
                    Wetter = "XMAS";
                    break;
            }

            //Wetter setzen
            NAPI.World.SetWeather(Wetter);

            //Log Nachricht
            NAPI.Util.ConsoleOutput("[StrawberryRP] Das Wetter hat sich auf die ID: " + GlobaleSachen.ServerWetter + " geändert.");

            //Schauen ob der Eintrag schon da ist
            var Check = ContextFactory.Instance.srp_server.Count(x => x.Id == 1);

            //Falls der Eintrag nicht da ist soll er erstellt werden da der Server sonst einen Fehler kriegt
            if(Check == 0)
            {
                var Eintrag = new Server
                {
                    Wetter = 0,
                    WetterAktualisiert = DateTime.Now,
                    Staatskasse = 0,
                    OnlineRekord = 0
                };

                //Query absenden
                ContextFactory.Instance.srp_server.Add(Eintrag);
                ContextFactory.Instance.SaveChanges();
            }

            //Datenbank Updaten
            var Server = ContextFactory.Instance.srp_server.Where(x => x.Id == 1).FirstOrDefault();
            Server.Wetter = GlobaleSachen.ServerWetter;
            Server.WetterAktualisiert = DateTime.Now;

            //Datenbank speichern
            ContextFactory.Instance.SaveChanges();
        }

        public static void WetterStation()
        {
            //Datenbank Updaten
            var Server = ContextFactory.Instance.srp_server.Where(x => x.Id == 1).FirstOrDefault();
            Server.WetterAktualisiert = DateTime.Now;

            //Datenbank speichern
            ContextFactory.Instance.SaveChanges();
        }

        public static float TankVolumenBerechnen(string Name)
        {
            //Benötigte Definitionen
            float Volumen = 0.0f;

            switch (Name)
            {
                case "faggio":
                    Volumen = 10.0f;
                    break;
                case "faggio2":
                    Volumen = 10.0f;
                    break;
                case "faggio3":
                    Volumen = 10.0f;
                    break;
                default:
                    Volumen = 50.0f;
                    break;
            }
            return Volumen;
        }

        public static string AdminLevelName(int AdminLevel)
        {
            //Definitionen
            string AdminLevelName;

            switch (AdminLevel)
            {
                case 0:
                    AdminLevelName = "Kein Admin";
                    break;
                case 1:
                    AdminLevelName = "Supporter";
                    break;
                case 2:
                    AdminLevelName = "Entwickler";
                    break;
                case 3:
                    AdminLevelName = "Administrator";
                    break;
                case 4:
                    AdminLevelName = "Head-Administrator";
                    break;
                case 5:
                    AdminLevelName = "Projektleiter";
                    break;
                default:
                    AdminLevelName = "Unbekannter Adminrang";
                    break;
            }
            return AdminLevelName;
        }

        public static string FahrzeugTypNamen(int Typ)
        {
            //Definitionen
            string FahrzeugTypName;

            switch (Typ)
            {
                case 0:
                    FahrzeugTypName = "Admin";
                    break;
                case 1:
                    FahrzeugTypName = "Job";
                    break;
                case 2:
                    FahrzeugTypName = "Miet";
                    break;
                case 3:
                    FahrzeugTypName = "Fraktion";
                    break;
                case 4:
                    FahrzeugTypName = "Privat";
                    break;
                case 5:
                    FahrzeugTypName = "Autohaus";
                    break;
                default:
                    FahrzeugTypName = "Unbekannt";
                    break;
            }
            return FahrzeugTypName;
        }

        [RemoteEvent("EnterCheck")]
        public static void EnterCheck(Client Player)
        {
            //Benötigte Definitionen
            int Eingeloggt = Player.GetData("Eingeloggt");

            //Daten übergeben
            Player.TriggerEvent("Enter", Eingeloggt);
        }

        [RemoteEvent("Tanken")]
        public static void Tanken(Client Player, float Liter, int Diesel, int E10, int Super)
        {
            //Schauen ob er noch an einer Tanke ist
            TankstellenPunktLokal tankenpunkt = new TankstellenPunktLokal();
            tankenpunkt = NaheTankePunktBekommen(Player);

            //Falls Tankenpunkt nicht 0 ist ist er an einer Tankstelle
            if (tankenpunkt != null)
            {
                //Benötigte Definitionen
                int DieselPreis = 0, E10Preis = 0, SuperPreis = 0, TankRechnung = 0;

                //Das Auto bekommen
                AutoLokal auto = new AutoLokal();
                auto = AutoVonIdBekommen(Player, Player.GetData("TankenAutoId"));

                //Werte runden und rechnen
                float TankVolumen = auto.TankVolumen;
                float TankInhalt = (float)Math.Round(auto.TankInhalt, 0) / 10 / 100;

                //Liter runden
                float LiterGerundet = (float)Math.Round(Liter, 0);

                //Den TankInhalt nach dem Tanken berechnen
                float TankDanach = TankInhalt + LiterGerundet;

                //Prüfen ob der Tank danach zu voll ist
                if (TankDanach > TankVolumen + 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: So viel kannst du nicht tanken."); return; }

                //Spritpreise bekommen
                foreach (TankstelleLokal tankstelle in TankenListe)
                {
                    if (tankenpunkt.TankstellenId == tankstelle.Id)
                    {
                        Player.SetData("TankenTankstellenId", tankstelle.Id);
                        DieselPreis = tankstelle.TankstelleDieselPreis;
                        E10Preis = tankstelle.TankstelleE10Preis;
                        SuperPreis = tankstelle.TankstelleSuperPreis;
                    }
                }

                //Dem Spieler nun sagen das er bezahlen soll
                int iLiter = (int)Liter;
                if(Diesel == 1) { TankRechnung = DieselPreis * iLiter; }
                else if (E10 == 1) { TankRechnung = E10Preis * iLiter; }
                else if (Super == 1) { TankRechnung = SuperPreis * iLiter; }

                //Auto Tank setzen
                if(TankDanach >= TankVolumen + 1)
                {
                    auto.TankInhalt = TankVolumen * 10 * 100;
                }
                else
                {
                    auto.TankInhalt = TankDanach * 10 * 100;
                }

                //Nachrichten an den Spieler
                Player.SendChatMessage("~y~Info~w~: Dein Fahrzeug wurde getankt.");
                Player.SendChatMessage("~y~Info~w~: Bezahle nun an der Tankstelle " + GeldFormatieren(TankRechnung));
                Player.TriggerEvent("tankenschliessen");

                //Chat zeigen
                Player.TriggerEvent("Chatzeigen");

                //Rechnung setzen
                Player.SetData("TankRechnung", TankRechnung);
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst an einer Tankstelle sein.");
            }
        }

        [RemoteEvent("TankenAbbrechen")]
        public static void TankenAbbrechen(Client Player)
        {
            Player.SetData("AmTanken", 0);
            Player.SetData("TankenTankstellenId", 0);
            Player.SetData("TankRechnung", 0);

            //Chat zeigen
            Player.TriggerEvent("Chatzeigen");
        }

        [RemoteEvent("Fahrzeugverwaltung_Privat_Abbrechen")]
        public static void FahrzeugVerwaltungPrivatAbbrechen(Client Player)
        {
            Player.SetData("FahrzeugPrivatDialog", 0);

            //Chat zeigen
            Player.TriggerEvent("Chatzeigen");
        }

        [RemoteEvent("Fahrzeug_Privat_Abschließen")]
        public static void FahrzeugPrivatAbschließen(Client Player, int Status)
        {
            AutoLokal auto = new AutoLokal();
            auto = NaheEigenesAutoBekommenPrivat(Player);

            if (auto != null)
            {
                if(Status == 1)
                {
                    auto.FahrzeugAbgeschlossen = 1;
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast dein Fahrzeug abgeschlossen.");
                }
                else
                {
                    auto.FahrzeugAbgeschlossen = 0;
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast dein Fahrzeug aufgeschlossen.");
                }
            }
        }

        [RemoteEvent("BCheck")]
        public static void BCheck(Client Player)
        {
            //Wichtige Abfragen
            if (Player.GetData("Eingeloggt") == 0) { return; }

            //Alles was nicht mit Fahrzeugen zu tun hat
            if (!Player.IsInVehicle)
            {
                //Schauen ob ein Haus in der nähe ist
                ImmobilienLokal haus = new ImmobilienLokal();
                haus = NaheImmobilieBekommen(Player);

                if(haus != null)
                {
                    if (Player.Position.DistanceTo(new Vector3(haus.ImmobilienX, haus.ImmobilienY, haus.ImmobilienZ)) < 2.0f)
                    {
                        Player.TriggerEvent("InteriorLaden", haus.ImmobilienInteriorName);
                        Player.Position = new Vector3(haus.ImmobilienEingangX, haus.ImmobilienEingangY, haus.ImmobilienEingangZ);
                        uint Dimension = Convert.ToUInt32(haus.Id);
                        NAPI.Entity.SetEntityDimension(Player, Dimension);
                        Freeze(Player);
                        Timer.SetTimer(() => Unfreeze(Player), 3000, 1);
                    }
                    else if (Player.Position.DistanceTo(new Vector3(haus.ImmobilienEingangX, haus.ImmobilienEingangY, haus.ImmobilienEingangZ)) < 2.0f)
                    {
                        Player.Position = new Vector3(haus.ImmobilienX, haus.ImmobilienY, haus.ImmobilienZ);
                        NAPI.Entity.SetEntityDimension(Player, NAPI.GlobalDimension);
                        Freeze(Player);
                        Timer.SetTimer(() => Unfreeze(Player), 3000, 1);
                    }
                }
            }
        }

        [RemoteEvent("F2Check")]
        public static void F2Check(Client Player)
        {
            //Benötigte Definitionen

            //Benötigte Abfragen
            if(Player.GetData("FahrzeugPrivatDialog") == 1) { Player.TriggerEvent("Fahrzeugverwaltung_Privat_Abbrechen"); Player.SetData("FahrzeugPrivatDialog", 0); return; }

            if (FahrzeugeZählenPrivat(Player) != 0)
            {

                AutoLokal autoe = new AutoLokal();
                autoe = NaheEigenesAutoBekommenPrivat(Player);

                if(autoe != null)
                {
                    float TankFloat = autoe.TankInhalt / 10 / 100;
                    float Kilometerstand = autoe.Kilometerstand / 10 / 100;
                    float TankVolumenFloat = autoe.TankVolumen;
                   
                    String Tankstring = NAPI.Util.ToJson(Math.Round(TankFloat, 2)) + " / " + TankVolumenFloat + " Liter";
                    String Kilometerstring = NAPI.Util.ToJson(Math.Round(Kilometerstand, 1)) + " KM";
                    DateTime Datum = autoe.FahrzeugHU;

                    Player.TriggerEvent("Fahrzeugverwaltung_Privat", autoe.FahrzeugName, Tankstring, Kilometerstring, Datum.ToString("dd.MM.yyyy"), autoe.FahrzeugAbgeschlossen);
                }
                else
                {
                    Player.TriggerEvent("Fahrzeugverwaltung_Privat_Liste");
                    foreach (AutoLokal auto in AutoListe)
                    {
                        if (auto.FahrzeugSpieler == Player.GetData("Id"))
                        {
                            float TankFloat = auto.TankInhalt / 10 / 100;
                            float Kilometerstand = auto.Kilometerstand / 10 / 100;
                            float TankVolumenFloat = auto.TankVolumen;
                            String Abgeschlossen = null;
                            if (auto.FahrzeugAbgeschlossen == 1)
                            {
                                Abgeschlossen = "Ja";
                            }
                            else
                            {
                                Abgeschlossen = "Nein";
                            }
                            String Tankstring = NAPI.Util.ToJson(Math.Round(TankFloat, 2)) + " / " + TankVolumenFloat + " Liter";
                            String Kilometerstring = NAPI.Util.ToJson(Math.Round(Kilometerstand, 1)) + " KM";
                            DateTime Datum = auto.FahrzeugHU;

                            Player.TriggerEvent("Fahrzeugverwaltung_Privat_Liste_Add", auto.FahrzeugName, Tankstring, Kilometerstring, Datum.ToString("dd.MM.yyyy"), Abgeschlossen);
                        }
                    }
                }

                Player.SetData("FahrzeugPrivatDialog", 1);
            }
        }

        [RemoteEvent("KCheck")]
        public static void KCheck(Client Player)
        {
            //Wichtige Abfragen
            if (Player.GetData("Eingeloggt") == 0) { return; }

            //Alles was nicht mit Fahrzeugen zu tun hat
            if (!Player.IsInVehicle)
            {
                TankstelleLokal tanke = new TankstelleLokal();
                tanke = Funktionen.NaheTankeBekommen(Player);

                if (tanke != null)
                {
                    if (tanke.TankstelleBesitzer == 0)
                    {
                        if (Player.GetData("KaufenTyp") == 1) { Player.SendChatMessage("~y~Info~w~: Schließe erst das aktuelle Fenster."); return; }
                        Player.SetData("KaufenTyp", 1);
                        Player.SetData("KaufenId", tanke.Id);
                        Player.SetData("KaufenPreis", tanke.TankstelleKaufpreis);

                        //Chat weg
                        Player.TriggerEvent("Chathiden");

                        Player.TriggerEvent("Kaufen", 1, GeldFormatieren(tanke.TankstelleKaufpreis));

                        return;
                    }
                }

                ImmobilienLokal haus = new ImmobilienLokal();
                haus = Funktionen.NaheImmobilieBekommen(Player);

                if (haus != null)
                {
                    if (haus.ImmobilienBesitzer == 0)
                    {
                        if (Player.GetData("KaufenTyp") == 2) { Player.SendChatMessage("~y~Info~w~: Schließe erst das aktuelle Fenster."); return; }
                        Player.SetData("KaufenTyp", 2);
                        Player.SetData("KaufenId", haus.Id);
                        Player.SetData("KaufenPreis", haus.ImmobilienKaufpreis);

                        //Chat weg
                        Player.TriggerEvent("Chathiden");

                        Player.TriggerEvent("Kaufen", 2, GeldFormatieren(haus.ImmobilienKaufpreis));

                        return;
                    }
                }

                SupermarktLokal supermarkt = new SupermarktLokal();
                supermarkt = Funktionen.NaheSupermarktBekommen(Player);

                if (supermarkt != null)
                {
                    if (supermarkt.SupermarktBesitzer == 0)
                    {
                        if (Player.GetData("KaufenTyp") == 3) { Player.SendChatMessage("~y~Info~w~: Schließe erst das aktuelle Fenster."); return; }
                        Player.SetData("KaufenTyp", 3);
                        Player.SetData("KaufenId", supermarkt.Id);
                        Player.SetData("KaufenPreis", supermarkt.SupermarktKaufpreis);

                        //Chat weg
                        Player.TriggerEvent("Chathiden");

                        Player.TriggerEvent("Kaufen", 3, GeldFormatieren(supermarkt.SupermarktKaufpreis));

                        return;
                    }
                }
            }
        }

        [RemoteEvent("ECheck")]
        public static void ECheck(Client Player)
        {
            //Wichtige Abfragen
            if (Player.GetData("Eingeloggt") == 0) { return; }

            //Sieht er einen Perso? Falls ja schließen
            if (Player.GetData("SiehtPerso") == 1) { Player.TriggerEvent("persoschliessen"); return; }
            if(Player.GetData("StadthalleDialog") == 1) { Player.TriggerEvent("StadthalleWeg"); Player.SetData("StadthalleDialog", 0); return; }

            //Alles was mit Fahrzeugen zu tun hat
            if(Player.IsInVehicle)
            {
                if (NAPI.Player.GetPlayerVehicleSeat(Player) == -1)
                {

                }
            }
            //Alles ohne Fahrzeuge
            else
            {
                if (Player.Position.DistanceTo(new Vector3(-138.8336, -632.2155, 168.8204)) < 2.0f && Player.Dimension == NAPI.GlobalDimension)
                {
                    Player.TriggerEvent("Stadthalle");
                    Player.SetData("StadthalleDialog", 1);
                }

                //Schauen ob er an einer Tanke ist
                TankstellenPunktLokal tankenpunkt = new TankstellenPunktLokal();
                tankenpunkt = NaheTankePunktBekommen(Player);

                //Falls Tankenpunkt nicht 0 ist ist er an einer Tankstelle
                if (tankenpunkt != null && Player.GetData("AmTanken") == 0 && Player.GetData("TankRechnung") == 0)
                {
                    AutoLokal auto = new AutoLokal();
                    auto = NaheAutosBekommen(Player);

                    if(auto == null) { return; }
                    if (auto != null) { Player.SetData("TankenAutoId", auto.Id); }

                    //Schauen ob der Spieler schon am tanken ist wenn ja Browser schließen
                    if (Player.GetData("AmTanken") == 1) { Player.SendChatMessage("~y~Info~w~: Du bist bereits am tanken."); return; }

                    //Benötigte Definitionen
                    int Diesel = 0, E10 = 0, Super = 0;
                    float Volumen = 0.0f, Inhalt = 0.0f, InhaltGerundet = 0.0f, TankvollRechnung = 0.0f;

                    //Werte zuweisen
                    Volumen = auto.TankVolumen;
                    Inhalt = auto.TankInhalt / 10 / 100;

                    //Spritpreise bekommen
                    foreach (TankstelleLokal tankstelle in TankenListe)
                    {
                        if (tankenpunkt.TankstellenId == tankstelle.Id)
                        {
                            Diesel = tankstelle.TankstelleDieselPreis;
                            E10 = tankstelle.TankstelleE10Preis;
                            Super = tankstelle.TankstelleSuperPreis;
                        }
                    }

                    InhaltGerundet = (float)Math.Round(Inhalt, 0);
                    TankvollRechnung = Volumen - InhaltGerundet;

                    //Chat weg
                    Player.TriggerEvent("Chathiden");

                    //CEF Javascript Event triggern und die Prameter übergeben
                    Player.TriggerEvent("Tanken", Volumen, InhaltGerundet, TankvollRechnung, Diesel, E10, Super);

                    //Dem Spieler lokal setzen das er gerade an tanken ist
                    Player.SetData("AmTanken", 1);
                }
            

                //Falls er seine Rechnung bezahlen will
                if (Player.GetData("AmTanken") == 1 && Player.GetData("TankRechnung") > 0)
                    {
                    TankstelleLokal tanke = new TankstelleLokal();
                    tanke = Funktionen.NaheTankeBekommen(Player);

                    if (tanke != null && Player.GetData("TankenTankstellenId") == tanke.Id)
                    {
                        if (Funktionen.AccountGeldBekommen(Player) < Player.GetData("TankRechnung"))
                        {
                            Player.SendChatMessage("~y~Info~w~: Du hast nicht genug Geld.");
                        }
                        else
                        {
                            //Der Tankstelle das Geld geben
                            tanke.TankstelleGeld += Player.GetData("TankRechnung");

                            //Nachricht an den Spieler
                            Player.SendChatMessage("~y~Info~w~: Du hast deine Rechnung bezahlt.");
                            Player.SendChatMessage("~y~Info~w~: Eine angenehme Weiterfahrt.");

                            //Dem Spieler das Geld abziehen
                            long GeldAbzug = Funktionen.AccountGeldBekommen(Player) - Player.GetData("TankRechnung");
                            Funktionen.AccountGeldSetzen(Player, GeldAbzug);

                            //Werte wieder resetten
                            Player.SetData("AmTanken", 0);
                            Player.SetData("TankenTankstellenId", 0);
                            Player.SetData("TankRechnung", 0);

                            //Tankstelle wurde geändert
                            tanke.TankstelleGeändert = true;
                        }
                    }
                }
            }
        }

        [ServerEvent(Event.ChatMessage)]
        public void OnChatMessage(Client Player, string message)
        {
            //Liste mit Spielern im Umkreis erstellen
            List<Client> NaheSpieler = NAPI.Player.GetPlayersInRadiusOfPlayer(10.0, Player);

            //Den anderen Spielern im Umkreis die Nachricht senden
            foreach (Client AndererSpieler in NaheSpieler)
            {
                AndererSpieler.SendChatMessage("[Lokal] " + Player.Name + ": " + message);
            }
        }

        public static List<TankstelleLokal> AlleTankstellenLadenDB()
        {
            List<TankstelleLokal> TankstellenListe = new List<TankstelleLokal>();
            foreach (var Tankstelle in ContextFactory.Instance.srp_tankstellen.Where(x => x.Id > 0).ToList())
            {
                TankstelleLokal tanke = new TankstelleLokal();

                tanke.Id = Tankstelle.Id;
                tanke.TankstelleBeschreibung = Tankstelle.TankstelleBeschreibung;
                tanke.TankstelleBesitzer = Tankstelle.TankstelleBesitzer;
                tanke.TankstelleGeld = Tankstelle.TankstelleGeld;
                tanke.TankstelleKaufpreis = Tankstelle.TankstelleKaufpreis;
                tanke.TankstelleDiesel = Tankstelle.TankstelleDiesel;
                tanke.TankstelleE10 = Tankstelle.TankstelleE10;
                tanke.TankstelleSuper = Tankstelle.TankstelleSuper;
                tanke.TankstelleDieselPreis = Tankstelle.TankstelleDieselPreis;
                tanke.TankstelleE10Preis = Tankstelle.TankstelleE10Preis;
                tanke.TankstelleSuperPreis = Tankstelle.TankstelleSuperPreis;
                tanke.TankstelleX = Tankstelle.TankstelleX;
                tanke.TankstelleY = Tankstelle.TankstelleY;
                tanke.TankstelleZ = Tankstelle.TankstelleZ;
                tanke.TankstelleGeändert = false;

                TankstellenListe.Add(tanke);
            }
            return TankstellenListe;
        }

        public static List<ImmobilienLokal> AlleImmobilienLadenDB()
        {
            List<ImmobilienLokal> ImmobilienListe = new List<ImmobilienLokal>();
            foreach (var Immobilie in ContextFactory.Instance.srp_immobilien.Where(x => x.Id > 0).ToList())
            {
                ImmobilienLokal haus = new ImmobilienLokal();

                haus.Id = Immobilie.Id;
                haus.ImmobilienBeschreibung = Immobilie.ImmobilienBeschreibung;
                haus.ImmobilienBesitzer = Immobilie.ImmobilienBesitzer;
                haus.ImmobilienGeld = Immobilie.ImmobilienGeld;
                haus.ImmobilienAbgeschlossen = Immobilie.ImmobilienAbgeschlossen;
                haus.ImmobilienKaufpreis = Immobilie.ImmobilienKaufpreis;
                haus.ImmobilienInteriorName = Immobilie.ImmobilienInteriorName;
                haus.ImmobilienEingangX = Immobilie.ImmobilienEingangX;
                haus.ImmobilienEingangY = Immobilie.ImmobilienEingangY;
                haus.ImmobilienEingangZ = Immobilie.ImmobilienEingangZ;
                haus.ImmobilienX = Immobilie.ImmobilienX;
                haus.ImmobilienY = Immobilie.ImmobilienY;
                haus.ImmobilienZ = Immobilie.ImmobilienZ;
                haus.ImmobilieGeändert = false;

                ImmobilienListe.Add(haus);
            }
            return ImmobilienListe;
        }

        public static List<SupermarktLokal> AlleSupermärkteLadenDB()
        {
            List<SupermarktLokal> SupermarktListe = new List<SupermarktLokal>();
            foreach (var Supermarkt in ContextFactory.Instance.srp_supermärkte.Where(x => x.Id > 0).ToList())
            {
                SupermarktLokal supermarkt = new SupermarktLokal();

                supermarkt.Id = Supermarkt.Id;
                supermarkt.SupermarktBeschreibung = Supermarkt.SupermarktBeschreibung;
                supermarkt.SupermarktBesitzer = Supermarkt.SupermarktBesitzer;
                supermarkt.SupermarktGeld = Supermarkt.SupermarktGeld;
                supermarkt.SupermarktKaufpreis = Supermarkt.SupermarktKaufpreis;
                supermarkt.SupermarktX = Supermarkt.SupermarktX;
                supermarkt.SupermarktY = Supermarkt.SupermarktY;
                supermarkt.SupermarktZ = Supermarkt.SupermarktZ;
                supermarkt.SupermarktGeändert = false;

                SupermarktListe.Add(supermarkt);
            }
            return SupermarktListe;
        }

        public static List<TankstellenPunktLokal> AlleTankstellenPunkteLadenDB()
        {
            List<TankstellenPunktLokal> TankstellenPunkteListe = new List<TankstellenPunktLokal>();
            foreach (var TankstellenPunkt in ContextFactory.Instance.srp_tankstellenpunkte.Where(x => x.Id > 0).ToList())
            {
                TankstellenPunktLokal tankstellenpunkt = new TankstellenPunktLokal();

                tankstellenpunkt.Id = TankstellenPunkt.Id;
                tankstellenpunkt.TankstellenId = TankstellenPunkt.TankstellenId;
                tankstellenpunkt.TankstellenPunktX = TankstellenPunkt.TankstellenPunktX;
                tankstellenpunkt.TankstellenPunktY = TankstellenPunkt.TankstellenPunktY;
                tankstellenpunkt.TankstellenPunktZ = TankstellenPunkt.TankstellenPunktZ;

                TankstellenPunkteListe.Add(tankstellenpunkt);
            }
            return TankstellenPunkteListe;
        }

        public static List<TankstellenInfoLokal> AlleTankstellenInfoLadenDB()
        {
            List<TankstellenInfoLokal> TankstellenInfoListe = new List<TankstellenInfoLokal>();
            foreach (var TankstellenPunkt in ContextFactory.Instance.srp_tankstelleninfo.Where(x => x.Id > 0).ToList())
            {
                TankstellenInfoLokal tankstelleninfo = new TankstellenInfoLokal();

                tankstelleninfo.Id = TankstellenPunkt.Id;
                tankstelleninfo.TankstellenInfoId = TankstellenPunkt.TankstellenInfoId;
                tankstelleninfo.TankstellenInfoX = TankstellenPunkt.TankstellenInfoX;
                tankstelleninfo.TankstellenInfoY = TankstellenPunkt.TankstellenInfoY;
                tankstelleninfo.TankstellenInfoZ = TankstellenPunkt.TankstellenInfoZ;

                TankstellenInfoListe.Add(tankstelleninfo);
            }
            return TankstellenInfoListe;
        }

        public static List<AccountLokal> AlleAccountsLadenDB()
        {
            List<AccountLokal> AccountListe = new List<AccountLokal>();

            foreach (var Account in ContextFactory.Instance.srp_accounts.Where(x => x.Id > 0).ToList())
            {
                AccountLokal account = new AccountLokal();

                account.Id = Account.Id;
                account.SocialClub = Account.SocialClub;
                account.NickName = Account.NickName;
                account.Passwort = Account.Passwort;
                account.AdminLevel = Account.AdminLevel;
                account.Fraktion = Account.Fraktion;
                account.Job = Account.Job;
                account.Geld = Account.Geld;
                account.BankGeld = Account.BankGeld;
                account.Perso = Account.Perso;
                account.EinreiseDatum = Account.EinreiseDatum;
                account.AccountGeändert = false;

                AccountListe.Add(account);
            }
            return AccountListe;
        }

        public static String GeldFormatieren(long Geld)
        {
            //Benötigte Definitionen
            String GeldFormatiert = null;
            if(Geld < 1000)
            {
                GeldFormatiert = Geld + "$";
            }
            else
            {
                GeldFormatiert = Convert.ToDecimal(Geld).ToString("#,##0.") + "$";
            }
            return GeldFormatiert;
        }

        [RemoteEvent("KaufenAbbrechen")]
        public static void KaufenAbbrechen(Client Player)
        {
            Player.SetData("KaufenTyp", 0);
            Player.SetData("KaufenId", 0);
            Player.SetData("KaufenPreis", 0);

            //Chat an
            Player.TriggerEvent("Chatzeigen");
        }

        [RemoteEvent("Kaufen")]
        public static void Kaufen(Client Player)
        {
            if(Player.GetData("KaufenTyp") == 1)
            {
                //Nochmal die Tankstelle ermitteln
                TankstelleLokal tanke = new TankstelleLokal();
                tanke = Funktionen.NaheTankeBekommen(Player);

                //Schauen ob er noch an der Tanke ist
                if (tanke != null && tanke.Id == Player.GetData("KaufenId"))
                {
                    //Schauen ob die Tankstelle immer noch keinen Besitzer hat
                    if (tanke.TankstelleBesitzer == 0)
                    {
                        if (Funktionen.AccountGeldBekommen(Player) < Player.GetData("KaufenPreis"))
                        {
                            Player.SendChatMessage("~y~Info~w~: Du hast nicht genug Geld.");
                            Player.TriggerEvent("kaufenabbrechen");
                        }
                        else
                        {
                            long GeldAbzug = Funktionen.AccountGeldBekommen(Player) - Player.GetData("KaufenPreis");

                            tanke.TankstelleBesitzer = Player.GetData("Id");
                            tanke.TankstelleBeschreibung = Player.Name + "`s Tankstelle";

                            Player.SendChatMessage("~y~Info~w~: Die Tankstelle gehört jetzt dir.");
                            Player.SendChatMessage("~y~Info~w~: Diese wird sich gleich automatisch aktualisieren.");
                            Funktionen.AccountGeldSetzen(Player, GeldAbzug);
                            Player.TriggerEvent("kaufenabbrechen");

                            tanke.TankstelleGeändert = true;
                        }
                    }
                }
            }
            else if (Player.GetData("KaufenTyp") == 2)
            {
                //Nochmal die Tankstelle ermitteln
                ImmobilienLokal haus = new ImmobilienLokal();
                haus = Funktionen.NaheImmobilieBekommen(Player);

                //Schauen ob er noch an der Tanke ist
                if (haus != null && haus.Id == Player.GetData("KaufenId"))
                {
                    //Schauen ob die Tankstelle immer noch keinen Besitzer hat
                    if (haus.ImmobilienBesitzer == 0)
                    {
                        if (Funktionen.AccountGeldBekommen(Player) < Player.GetData("KaufenPreis"))
                        {
                            Player.SendChatMessage("~y~Info~w~: Du hast nicht genug Geld.");
                            Player.TriggerEvent("kaufenabbrechen");
                        }
                        else
                        {
                            long GeldAbzug = Funktionen.AccountGeldBekommen(Player) - Player.GetData("KaufenPreis");

                            haus.ImmobilienBesitzer = Player.GetData("Id");
                            haus.ImmobilienBeschreibung = Player.Name + "`s Immobilie";

                            Player.SendChatMessage("~y~Info~w~: Die Immobilie gehört jetzt dir.");
                            Player.SendChatMessage("~y~Info~w~: Diese wird sich gleich automatisch aktualisieren.");
                            Funktionen.AccountGeldSetzen(Player, GeldAbzug);
                            Player.TriggerEvent("kaufenabbrechen");

                            haus.ImmobilieGeändert = true;
                        }
                    }
                }
            }
            else if (Player.GetData("KaufenTyp") == 3)
            {
                //Nochmal die Tankstelle ermitteln
                SupermarktLokal supermarkt = new SupermarktLokal();
                supermarkt = Funktionen.NaheSupermarktBekommen(Player);

                //Schauen ob er noch an der Tanke ist
                if (supermarkt != null && supermarkt.Id == Player.GetData("KaufenId"))
                {
                    //Schauen ob die Tankstelle immer noch keinen Besitzer hat
                    if (supermarkt.SupermarktBesitzer == 0)
                    {
                        if (Funktionen.AccountGeldBekommen(Player) < Player.GetData("KaufenPreis"))
                        {
                            Player.SendChatMessage("~y~Info~w~: Du hast nicht genug Geld.");
                            Player.TriggerEvent("kaufenabbrechen");
                        }
                        else
                        {
                            long GeldAbzug = Funktionen.AccountGeldBekommen(Player) - Player.GetData("KaufenPreis");

                            supermarkt.SupermarktBesitzer = Player.GetData("Id");
                            supermarkt.SupermarktBeschreibung = Player.Name + "`s 24/7";

                            Player.SendChatMessage("~y~Info~w~: Der 24/7 gehört jetzt dir.");
                            Player.SendChatMessage("~y~Info~w~: Dieser wird sich gleich automatisch aktualisieren.");
                            Funktionen.AccountGeldSetzen(Player, GeldAbzug);
                            Player.TriggerEvent("kaufenabbrechen");

                            supermarkt.SupermarktGeändert = true;
                        }
                    }
                }
            }
        }

        public static void SpielerLaden(Client Player)
        {
            //Daten aus der SQL Datenbank ziehen
            var Account = ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == Player.SocialClubName).FirstOrDefault();

            AccountLokal account = new AccountLokal();

            account.Id = Account.Id;
            account.SocialClub = Account.SocialClub;
            account.NickName = Account.NickName;
            account.Passwort = Account.Passwort;
            account.AdminLevel = Account.AdminLevel;
            account.Fraktion = Account.Fraktion;
            account.Job = Account.Job;
            account.Geld = Account.Geld;
            account.BankGeld = Account.BankGeld;
            account.Perso = Account.Perso;
            account.EinreiseDatum = Account.EinreiseDatum;
            account.FahrzeugSchlüssel = Account.FahrzeugSchlüssel;
            account.AccountGeändert = false;

            //Dies sind lokale Dinge bitte keine floats etc nutzen, dass buggt per SetData
            Player.SetData("Eingeloggt", 1);
            Player.SetData("SiehtPerso", 0);
            Player.SetData("AmTanken", 0);
            Player.SetData("TankenTankstellenId", 0);
            Player.SetData("TankRechnung", 0);
            Player.SetData("KaufenTyp", 0);
            Player.SetData("KaufenId", 0);
            Player.SetData("KaufenPreis", 0);

            //Dialoge
            Player.SetData("StadthalleDialog", 0);
            Player.SetData("FahrzeugPrivatDialog", 0);

            //Dem Spieler seine ID setzen
            Player.SetData("Id", Account.Id);

            //Chat zeigen
            Player.TriggerEvent("Chatzeigen");
        }

        public static void AlleSpielerSpeichern()
        {
            foreach (var Spieler in NAPI.Pools.GetAllPlayers())
            {
                foreach (AccountLokal account in AccountListe)
                {
                    if(Spieler.GetData("Id") == account.Id && account.AccountGeändert == true)
                    {
                        var DBAccount = ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == Spieler.SocialClubName).FirstOrDefault();

                        DBAccount.NickName = account.NickName;
                        DBAccount.AdminLevel = account.AdminLevel;
                        DBAccount.Fraktion = account.Fraktion;
                        DBAccount.Job = account.Job;
                        DBAccount.Geld = account.Geld;
                        DBAccount.BankGeld = account.BankGeld;
                        DBAccount.Perso = account.Perso;
                        DBAccount.EinreiseDatum = account.EinreiseDatum;
                        DBAccount.FahrzeugSchlüssel = account.FahrzeugSchlüssel;

                        //Query absenden
                        ContextFactory.Instance.SaveChanges();

                        //Damit er nicht dauerthaft gespeichert wird
                        account.AccountGeändert = false;
                    }
                }
            }
        }

        public static void SpielerSpeichern(Client Player)
        {
            //Daten aus der SQL Datenbank ziehen
            var Account = ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == Player.SocialClubName).FirstOrDefault();

            AccountLokal account = new AccountLokal();
            account = AccountBekommen(Player);

            //Lokale Daten abfragen und in der Datenbank ablegen
            Account.NickName = account.NickName;
            Account.AdminLevel = account.AdminLevel;
            Account.Fraktion = account.Fraktion;
            Account.Job = account.Job;
            Account.Geld = account.Geld;
            Account.BankGeld = account.BankGeld;
            Account.Perso = account.Perso;
            Account.EinreiseDatum = account.EinreiseDatum;
            Account.FahrzeugSchlüssel = account.FahrzeugSchlüssel;

            //Query absenden
            ContextFactory.Instance.SaveChanges();
        }
        
        public static void FahrzeugeSpeichern()
        {
            //Schleife durch alle Fahrzeuge
            foreach (var Fahrzeuge in NAPI.Pools.GetAllVehicles())
            {
                //Benötigte Definitionen
                int AutoId = Fahrzeuge.GetData("Id");

                foreach (AutoLokal auto in Funktionen.AutoListe)
                {
                    if (AutoId == auto.Id && auto.FahrzeugGeändert == true)
                    {
                        //Kilometerstand teilen damit keine zu große Zahl entsteht
                        float AktuellerKilometerstand = auto.Kilometerstand / 10 / 100;
                        float AktuellerTank = auto.TankInhalt / 10 / 100;

                        //Fahrzeug aus der DB greifen
                        var AktuellesFahrzeug = ContextFactory.Instance.srp_fahrzeuge.Where(x => x.Id == AutoId).FirstOrDefault();

                        //Kilometerstand zuweisen
                        AktuellesFahrzeug.FahrzeugFraktion = auto.FahrzeugFraktion;
                        AktuellesFahrzeug.FahrzeugJob = auto.FahrzeugJob;
                        AktuellesFahrzeug.FahrzeugSpieler = auto.FahrzeugSpieler;
                        AktuellesFahrzeug.FahrzeugMietpreis = auto.FahrzeugMietpreis;
                        AktuellesFahrzeug.FahrzeugKaufpreis = auto.FahrzeugKaufpreis;
                        AktuellesFahrzeug.FahrzeugAutohaus = auto.FahrzeugAutohaus;
                        AktuellesFahrzeug.FahrzeugX = auto.FahrzeugX;
                        AktuellesFahrzeug.FahrzeugY = auto.FahrzeugY;
                        AktuellesFahrzeug.FahrzeugZ = auto.FahrzeugZ;
                        AktuellesFahrzeug.FahrzeugRot = auto.FahrzeugRot;
                        AktuellesFahrzeug.FahrzeugFarbe1 = auto.FahrzeugFarbe1;
                        AktuellesFahrzeug.FahrzeugFarbe2 = auto.FahrzeugFarbe2;
                        AktuellesFahrzeug.TankVolumen = auto.TankVolumen;
                        AktuellesFahrzeug.TankInhalt = AktuellerTank;
                        AktuellesFahrzeug.Kilometerstand = AktuellerKilometerstand;
                        AktuellesFahrzeug.FahrzeugHU = auto.FahrzeugHU;
                        AktuellesFahrzeug.FahrzeugAbgeschlossen = auto.FahrzeugAbgeschlossen;

                        //Query absenden
                        ContextFactory.Instance.SaveChanges();

                        //Damit er nicht dauerthaft gespeichert wird
                        auto.FahrzeugGeändert = false;
                    }
                }
            }
        }

        public static void AlleTankstellenUpdaten()
        {
            foreach (TankstelleLokal tankstelle in TankenListe)
            {
                String TankstellenText = null;
                if (tankstelle.TankstelleBesitzer == 0)
                {
                    TankstellenText = "~g~[~w~Tankstelle ID: " + tankstelle.Id + "~g~]~n~";
                    TankstellenText += "~w~Kaufpreis: " + GeldFormatieren(tankstelle.TankstelleKaufpreis) + "~n~";
                    TankstellenText += "Beschreibung: " + tankstelle.TankstelleBeschreibung;
                    tankstelle.TankstellenBlip.Name = tankstelle.TankstelleBeschreibung;
                    tankstelle.TankstellenBlip.ShortRange = true;
                    tankstelle.TankstellenBlip.Sprite = 361;
                    tankstelle.TankstellenBlip.Color = 2;
                }
                else
                {
                    TankstellenText = "~g~[~w~Tankstelle ID: " + tankstelle.Id + "~g~]~n~";
                    TankstellenText += "~w~Beschreibung: " + tankstelle.TankstelleBeschreibung + "~n~";
                    TankstellenText += "Besitzer: " + BesitzerNamenBekommen(tankstelle.TankstelleBesitzer);
                    tankstelle.TankstellenBlip.Name = tankstelle.TankstelleBeschreibung;
                    tankstelle.TankstellenBlip.ShortRange = true;
                    tankstelle.TankstellenBlip.Sprite = 361;
                    tankstelle.TankstellenBlip.Color = 1;
                }

                //Text Updaten
                NAPI.TextLabel.SetTextLabelText(tankstelle.TankstellenLabel, TankstellenText);

                foreach (TankstellenInfoLokal tankeninfo in TankenInfoListe)
                {
                    if(tankeninfo.TankstellenInfoId == tankstelle.Id)
                    {
                        String TankstellenInfoText = "~g~[~w~Für ID: " + tankeninfo.TankstellenInfoId + "~g~]~n~";
                        TankstellenInfoText += "~w~Diesel: ~g~" + tankstelle.TankstelleDiesel + "~w~L~n~";
                        TankstellenInfoText += "~w~E10: ~g~" + tankstelle.TankstelleE10 + "~w~L~n~";
                        TankstellenInfoText += "~w~Super: ~g~" + tankstelle.TankstelleSuper + "~w~L";

                        //Text Updaten
                        NAPI.TextLabel.SetTextLabelText(tankeninfo.TankstellenInfoLabel, TankstellenInfoText);
                    }
                }

                //Damit es nicht immer weiter gespeichert wird
                tankstelle.TankstelleGeändert = false;
            }
        }

        public static void AlleImmobilienUpdaten()
        {
            foreach (ImmobilienLokal haus in ImmobilienListe)
            {
                String ImmobilienText = null;
                if (haus.ImmobilienBesitzer == 0)
                {
                    ImmobilienText = "~g~[~w~Hausnummer: " + haus.Id + "~g~]~n~";
                    ImmobilienText += "~w~Kaufpreis: " + GeldFormatieren(haus.ImmobilienKaufpreis) + "~n~";
                    ImmobilienText += "Beschreibung: " + haus.ImmobilienBeschreibung;
                    haus.ImmobilienBlip.Name = haus.ImmobilienBeschreibung;
                    haus.ImmobilienBlip.ShortRange = true;
                    haus.ImmobilienBlip.Sprite = 40;
                    haus.ImmobilienBlip.Color = 2;
                }
                else
                {
                    ImmobilienText = "~g~[~w~Hausnummer: " + haus.Id + "~g~]~n~";
                    ImmobilienText += "~w~Beschreibung: " + haus.ImmobilienBeschreibung + "~n~";
                    ImmobilienText += "Besitzer: " + BesitzerNamenBekommen(haus.ImmobilienBesitzer);
                    haus.ImmobilienBlip.Name = haus.ImmobilienBeschreibung;
                    haus.ImmobilienBlip.ShortRange = true;
                    haus.ImmobilienBlip.Sprite = 40;
                    haus.ImmobilienBlip.Color = 1;
                }

                //Text Updaten
                NAPI.TextLabel.SetTextLabelText(haus.ImmobilienLabel, ImmobilienText);
            }
        }

        public static void AlleSupermärkteUpdaten()
        {
            foreach (SupermarktLokal supermarkt in SupermarktListe)
            {
                String SupermarktText = null;
                if (supermarkt.SupermarktBesitzer == 0)
                {
                    SupermarktText = "~g~[~w~24/7 ID: " + supermarkt.Id + "~g~]~n~";
                    SupermarktText += "~w~Kaufpreis: " + GeldFormatieren(supermarkt.SupermarktKaufpreis) + "~n~";
                    SupermarktText += "Beschreibung: " + supermarkt.SupermarktBeschreibung;
                    supermarkt.SupermarktBlip.Name = supermarkt.SupermarktBeschreibung;
                    supermarkt.SupermarktBlip.ShortRange = true;
                    supermarkt.SupermarktBlip.Sprite = 590;
                    supermarkt.SupermarktBlip.Color = 2;
                }
                else
                {
                    SupermarktText = "~g~[~w~24/7 ID: " + supermarkt.Id + "~g~]~n~";
                    SupermarktText += "~w~Beschreibung: " + supermarkt.SupermarktBeschreibung + "~n~";
                    SupermarktText += "Besitzer: " + BesitzerNamenBekommen(supermarkt.SupermarktBesitzer);
                    supermarkt.SupermarktBlip.Name = supermarkt.SupermarktBeschreibung;
                    supermarkt.SupermarktBlip.ShortRange = true;
                    supermarkt.SupermarktBlip.Sprite = 590;
                    supermarkt.SupermarktBlip.Color = 1;
                }
                //Text Updaten
                NAPI.TextLabel.SetTextLabelText(supermarkt.SupermarktLabel, SupermarktText);
            }
        }

        public static void AlleTankstellenSpeichern()
        {
            foreach (TankstelleLokal tanke in TankenListe)
            {
                if(tanke.TankstelleGeändert == true)
                {
                    var Tankstelle = ContextFactory.Instance.srp_tankstellen.Where(x => x.Id == tanke.Id).FirstOrDefault();

                    Tankstelle.TankstelleBeschreibung = tanke.TankstelleBeschreibung;
                    Tankstelle.TankstelleBesitzer = tanke.TankstelleBesitzer;
                    Tankstelle.TankstelleGeld = tanke.TankstelleGeld;
                    Tankstelle.TankstelleKaufpreis = tanke.TankstelleKaufpreis;
                    Tankstelle.TankstelleDiesel = tanke.TankstelleDiesel;
                    Tankstelle.TankstelleE10 = tanke.TankstelleE10;
                    Tankstelle.TankstelleSuper = tanke.TankstelleSuper;
                    Tankstelle.TankstelleDieselPreis = tanke.TankstelleDieselPreis;
                    Tankstelle.TankstelleE10Preis = tanke.TankstelleE10Preis;
                    Tankstelle.TankstelleSuperPreis = tanke.TankstelleSuperPreis;
                    Tankstelle.TankstelleX = tanke.TankstelleX;
                    Tankstelle.TankstelleY = tanke.TankstelleY;
                    Tankstelle.TankstelleZ = tanke.TankstelleZ;

                    //Query absenden
                    ContextFactory.Instance.SaveChanges();

                    //Damit Sie nicht dauerhaft gespeichert wird
                    tanke.TankstelleGeändert = false;
                }
            }
        }

        public static void AlleImmobilienSpeichern()
        {
            foreach (ImmobilienLokal haus in ImmobilienListe)
            {
                if (haus.ImmobilieGeändert == true)
                {
                    var Immobilie = ContextFactory.Instance.srp_immobilien.Where(x => x.Id == haus.Id).FirstOrDefault();

                    Immobilie.ImmobilienBeschreibung = haus.ImmobilienBeschreibung;
                    Immobilie.ImmobilienBesitzer = haus.ImmobilienBesitzer;
                    Immobilie.ImmobilienGeld = haus.ImmobilienGeld;
                    Immobilie.ImmobilienAbgeschlossen = haus.ImmobilienAbgeschlossen;
                    Immobilie.ImmobilienKaufpreis = haus.ImmobilienKaufpreis;
                    Immobilie.ImmobilienInteriorName = haus.ImmobilienInteriorName;
                    Immobilie.ImmobilienEingangX = haus.ImmobilienEingangX;
                    Immobilie.ImmobilienEingangY = haus.ImmobilienEingangY;
                    Immobilie.ImmobilienEingangZ = haus.ImmobilienEingangZ;
                    Immobilie.ImmobilienX = haus.ImmobilienX;
                    Immobilie.ImmobilienY = haus.ImmobilienY;
                    Immobilie.ImmobilienZ = haus.ImmobilienZ;

                    //Query absenden
                    ContextFactory.Instance.SaveChanges();

                    //Damit Sie nicht dauerhaft gespeichert wird
                    haus.ImmobilieGeändert = false;
                }
            }
        }

        public static void AlleSupermärkteSpeichern()
        {
            foreach (SupermarktLokal supermarkt in SupermarktListe)
            {
                if (supermarkt.SupermarktGeändert == true)
                {
                    var Supermarkt = ContextFactory.Instance.srp_supermärkte.Where(x => x.Id == supermarkt.Id).FirstOrDefault();

                    Supermarkt.SupermarktBeschreibung = supermarkt.SupermarktBeschreibung;
                    Supermarkt.SupermarktBesitzer = supermarkt.SupermarktBesitzer;
                    Supermarkt.SupermarktGeld = supermarkt.SupermarktGeld;
                    Supermarkt.SupermarktKaufpreis = supermarkt.SupermarktKaufpreis;
                    Supermarkt.SupermarktX = supermarkt.SupermarktX;
                    Supermarkt.SupermarktY = supermarkt.SupermarktY;
                    Supermarkt.SupermarktZ = supermarkt.SupermarktZ;

                    //Query absenden
                    ContextFactory.Instance.SaveChanges();

                    //Damit Sie nicht dauerhaft gespeichert wird
                    supermarkt.SupermarktGeändert = false;
                }
            }
        }

        public static void LogEintrag(Client Player, string Aktion)
        {
            //Kontruktor für die Logs
            var Log = new Log
            {
                Aktion = Aktion,
                SocialClub = Player.SocialClubName,
                Wann = DateTime.Now
            };

            //Objekt in der Datenbank speichern
            ContextFactory.Instance.srp_log.Add(Log);
            ContextFactory.Instance.SaveChanges();
        }

        public static void TextLabelsLaden()
        {
            //Noobspawn
            NAPI.TextLabel.CreateTextLabel("~r~StrawberryRP~w~~n~Hier kannst du einen Roller Mieten.~n~Preis: 30€", new Vector3(-3237.754, 969.6091, 12.94306), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
            NAPI.TextLabel.CreateTextLabel("~r~StrawberryRP~w~~n~Willkommen bei uns!~n~Bei Fragen nutze /support", new Vector3(-3237.508, 965.3855, 13.04449), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);

            //Stadthalle
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Stadthalle - Eingang~g~]", new Vector3(-1285.605, -567.0062, 31.7124), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Stadthalle - Ausgang~g~]", new Vector3(-139.473, -617.4647, 168.8204), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Stadthalle - Service~g~]", new Vector3(-138.8336, -632.2155, 168.8204), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, NAPI.GlobalDimension);

        }

        public static void BlipsLaden()
        {
            Blip Noobspawn = NAPI.Blip.CreateBlip(new Vector3(-3237.754, 969.6091, 12.94306)); Noobspawn.Name = "Neulingsspawn"; Noobspawn.ShortRange = true; Noobspawn.Sprite = 162; Noobspawn.Color = 12;
            Blip Stadthalle = NAPI.Blip.CreateBlip(new Vector3(-1285.605, -567.0062, 31.7124)); Stadthalle.Name = "Stadthalle"; Stadthalle.ShortRange = true; Stadthalle.Sprite = 120; Stadthalle.Color = 12;
        }

        public static void MarkersLaden()
        {
            //Noobspawn
            NAPI.Marker.CreateMarker(21, new Vector3(-3237.754, 969.6091, 12.94306), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
            NAPI.Marker.CreateMarker(21, new Vector3(-3237.508, 965.3855, 13.04449), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);

            //Stadthalle
            NAPI.Marker.CreateMarker(21, new Vector3(-1285.605, -567.0062, 31.7124), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
            NAPI.Marker.CreateMarker(21, new Vector3(-139.473, -617.4647, 168.8204), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
            NAPI.Marker.CreateMarker(21, new Vector3(-138.8336, -632.2155, 168.8204), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, NAPI.GlobalDimension);
        }

        //Stadthalle
        private static ColShape StadthalleEingang = null;
        private static ColShape StadthalleAusgang = null;

        public static void InteriorsLaden()
        {
            StadthalleEingang = NAPI.ColShape.CreateSphereColShape(new Vector3(-1285.605, -567.0062, 31.7124), 1.0f);
            StadthalleAusgang = NAPI.ColShape.CreateSphereColShape(new Vector3(-139.473, -617.4647, 168.8204), 1.0f);
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public static void OnPlayerEnterColshape(ColShape shape, Client Player)
        {
            if (shape == StadthalleEingang)
            {
                Player.TriggerEvent("InteriorLaden", "ex_dt1_02_office_02c");
                Player.Position = new Vector3(-141.5429, -620.9524, 168.8204);
                Freeze(Player);
                Timer.SetTimer(() => Unfreeze(Player), 3000, 1);
            }
            else if (shape == StadthalleAusgang)
            {
                Player.Position = new Vector3(-1282.161, -563.8747, 31.7124);
                Player.Rotation = new Vector3(0.0, 0.0, 309.4669);
                Freeze(Player);
                Timer.SetTimer(() => Unfreeze(Player), 3000, 1);
            }
        }

        [RemoteEvent("PersoSchliessen")]
        public void PersoSchliessen(Client Player)
        {
            Player.SetData("SiehtPerso", 0);
        }

        [RemoteEvent("AutoAnAus")]
        public void AutoAnAus(Client Player)
        {
            //Abfragen ob er in einem Auto ist
            if(Player.IsInVehicle && NAPI.Player.GetPlayerVehicleSeat(Player) == -1)
            {
                if (Player.Vehicle.EngineStatus == false)
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Motor wurde gestartet.");
                    Player.Vehicle.EngineStatus = true;
                }
                else
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Motor wurde gestoppt.");
                    Player.Vehicle.EngineStatus = false;
                }
            }
            else
            {
                //Wenn er in keinem Wagen ist soll nichts passieren
            }
        }

        public static string ErsterBuchstabeGroß(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static void Freeze(Client Player)
        {
            //Freeze
            Player.TriggerEvent("Freeze");
        }

        public static void Unfreeze(Client Player)
        {
            //Unfreeze
            Player.TriggerEvent("Unfreeze");
        }

        public static void SpawnManager(Client Player)
        {
            Player.Position = new Vector3(-3260.276, 967.3442, 8.832886);
            Player.Rotation = new Vector3(0.0, 0.0, 270.343);
            Player.TriggerEvent("moveSkyCamera", Player, "down");

            //Unfreeze Timer
            Timer.SetTimer(() => Unfreeze(Player), 2000, 1);
        }
    }
}
