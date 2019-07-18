/************************************************************************************************************
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        @@ Strawberry Roleplay Gamemode                                                                   @@
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
*************************************************************************************************************/

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
        public const String Verbindung = "Server=127.0.0.1; Database=strawberryrp_server; Uid=strawberryrpserver; Pwd=rElciatks7Pn7DpH";
        //public const String Verbindung = "Server=localhost; Database=strawberryrp_server; Uid=root;Pwd=";

        //Versionen
        public const String Server_Version = "1.0.0";

        //Verbrauch
        public const float Verbrauch = 0.7f;

        //Wetter
        public static int ServerWetter = 0;

        //Registrierung
        public const int StartGeld = 100;

        //Cooldown
        public static uint KeyCoolDownZeit = 5000;
        public static uint MenuCoolDownZeit = 2000;
        public static uint ScoreboardCoolDownZeit = 3000;

        //Marker
        public const int TankstellenMarker = 0;
        public const int TankstellenZapfsäuleMarker = 20;
        public const int TankstellenInfoMarker = 20;
        public const int ImmobilienMarker = 0;
        public const int SupermarktMarker = 0;
        public const int AutohausMarker = 0;
        public const int ATMMarker = 20;
        public const int FVermietungMarker = 0;

        //Preise
        public const int PersonalausweisPreis = 100;
        public const int HeiratenPreis = 15000;
        public const int GruppenPreis = 100000;
        public const int GruppenEinlage = 25000;

        //Limits
        public const int GruppenMemberLimit = 25;

        //Dinge die am laufen sind
        public static int Heiraten = 0;
        public static int BerufskraftFahrerFahrzeugGespawnt = 0;
        public static int BusfahrerFahrzeugGespawnt = 0;

        //Löhne
        public static long Berufskraftfahrer_Benzin_Entfernungsbonus = 100;
        public static long Berufskraftfahrer_Benzin_LiterBonus = 2;
        public static long Berufskraftfahrer_Holz_KiloBonus = 2;
        public static long Busfahrer_Route1_HaltestellenLohn = 25;
        public static long Busfahrer_Route1_EndBonus = 150;
        public static long Busfahrer_Route2_HaltestellenLohn = 25;
        public static long Busfahrer_Route2_EndBonus = 300;
        public static long Busfahrer_Route3_HaltestellenLohn = 25;
        public static long Busfahrer_Route3_EndBonus = 140;
        public static long Busfahrer_Route4_HaltestellenLohn = 25;
        public static long Busfahrer_Route4_EndBonus = 100;

        //Globale Job Daten
        public static long Busfahrer_Route1_Haltestellen = 8;
        public static long Busfahrer_Route2_Haltestellen = 12;
        public static long Busfahrer_Route3_Haltestellen = 7;
        public static long Busfahrer_Route4_Haltestellen = 4;

        //Busfahrer Route Blips
        public static Marker Route1_1, Route3_1;
        public static Marker Route1_2, Route2_2, Route3_2, Route4_2;
        public static Marker Route1_3, Route2_3, Route3_3, Route4_3;
        public static Marker Route1_4, Route2_4, Route3_4, Route4_4;
        public static Marker Route1_5, Route2_5, Route3_5;
        public static Marker Route1_6, Route2_6, Route3_6;
        public static Marker Route1_7, Route2_7, Route3_7;
        public static Marker Route1_8, Route2_8;
        public static Marker Route2_9;
        public static Marker Route2_10;

        //NPC Texte
        public static String EinreiseNPCText;
        public static String HelmutNPCText;
    }

    public class AdminBefehle
    {
        //Hier kann man die Berechtigungen für die Befehle ändern
        public const int AdminGeben = 5;
        public const int Speichern = 4;
        public const int GeldGeben = 5;
        public const int VerwaltungsModus = 4;
        public const int AlleAdminFahrzeugeLöschen = 4;
        public const int RandomSpawnErstellen = 4;
        public const int FraktionSetzen = 4;
        public const int ATMErstellen = 4;
        public const int FVermietungErstellen = 4;
        public const int FVermietungLöschen = 4;
        public const int ATMLöschen = 4;
        public const int TankeBeschreibung = 4;
        public const int TankeKaufpreis = 4;
        public const int FahrzeugErstellen = 4;
        public const int ManufakturFahrzeugErstellen = 4;
        public const int AutohausErstellen = 4;
        public const int AutohausBeschreibung = 4;
        public const int AutohausLöschen = 4;
        public const int PedErstellen = 4;
        public const int PedLöschen = 4;
        public const int FahrzeugLöschen = 4;
        public const int FahrzeugMietPreis = 4;
        public const int FahrzeugKaufPreis = 4;
        public const int HausErstellen = 4;
        public const int HausKaufPreis = 4;
        public const int HausInterior = 4;
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
        public const int FahrzeugParken = 3;
        public const int FahrzeugPorten = 3;
        public const int AdminModFahrzeugErstellen = 3;
        public const int HausTeleport = 1;
        public const int Teleporten = 1;
        public const int AdminChat = 1;
        public const int FahrzeugRespawn = 3;
        public const int FahrzeugReparieren = 3;
        public const int FahrzeugZuweisen = 3;
        public const int AdminFahrzeugErstellen = 1;
        public const int PositionSpeichern = 3;
        public const int SaveListe = 1;
        public const int CoordinatenTeleport = 1;
    }

    public class Zahlen
    {
        //Globale Zahlen
        public const int Jobs = 2;
        public const int Fraktionen = 1;
    }

    public class Funktionen : Script
    {
        //Listen für Dynamische Systeme
        public static List<RandomSpawns> RandomSpawnListe;
        public static List<AccountLokal> AccountListe;
        public static List<ImmobilienLokal> ImmobilienListe;
        public static List<AutoLokal> AutoListe;
        public static List<TankstelleLokal> TankenListe;
        public static List<TankstellenPunktLokal> TankenPunktListe;
        public static List<TankstellenInfoLokal> TankenInfoListe;
        public static List<SupermarktLokal> SupermarktListe;
        public static List<AutohausLokal> AutohausListe;
        public static List<BotLokal> BotListe;
        public static List<SaveLokal> SaveListe;
        public static List<GruppenLokal> GruppenListe;
        public static List<BankautomatenLokal> ATMListe;
        public static List<FahrzeugvermietungenLokal> FVermietungListe;
        public static List<FahrzeugMods> FModsListe;

        public static void NPCTexteInitialisieren()
        {
            //EinreiseNPC
            GlobaleSachen.EinreiseNPCText = "Willkommen in Los Santos, zeigen Sie mir  bitte Ihren  Ausweis… <br>" +
            "Sie haben keine Papiere? Hmm..<br>" +
            "Nun gut, ich werde Ihnen die Einreise gewähren, jedoch müssen Sie sich in 7 Tagen beim Bürgeramt melden, dort müssen Sie dann einen neuen Ausweis beantragen.<br>" +
            "Ich gebe Ihnen noch einen guten Rat, gleich hier vorne am Parkplatz steht mein Freund Helmut, der wird Ihnen ein Fahrzeug zur Verfügung stellen.";

            GlobaleSachen.HelmutNPCText = "Hallo ich bin Helmut, ich hoffe du hattest eine angenehme Reise. <br>" +
            "Mir wurde gesagt du brauchst ein Fahrzeug?<br>" +
            "Mein Enkel hat sein altes Auto hier gelassen, bevor er auf Weltreise gegangen ist, er wird noch einige Tage wegbleiben, solange kann ich es dir seinen Golf ausleihen.<br>" +
            "Er hat den Wagen noch nicht so lange, geh bitte gut damit um.<br>" +
            "Dafür musst du mir aber einen Gefallen tun. Ich habe vorhin im Vorbeifahren einen alten Mann am Blumenfeld gesehen, er sah aus als könnte er Hilfe gebrauchen…<br>" +
            "Da ich aber schnell wieder hierher zurück musste konnte ich nicht anhalten.<br>" +
            "Geh bitte los und schau mal nach ihm.<br>" +
            "Bitte bring mir den Wagen in 2 Tagen wieder. Ich brauche ihn dann selber.<br>";
        }

        public static void AllesStarten()
        {
            //Globaler Server Chat aus
            NAPI.Server.SetGlobalServerChat(false);

            //Wichtige Dinge die geladen werden müssen
            RandomSpawnsLaden();
            NPCTexteInitialisieren();
            FahrzeugModsLaden();
            AccountsLadenLokal();
            FahrzeugeLadenLokal();
            TextLabelsLaden();
            BlipsLaden();
            MarkersLaden();
            TankstellenLadenLokal();
            TankstellenPunkteLadenLokal();
            TankstellenInfoLadenLokal();
            ImmobilienLadenLokal();
            SupermärkteLadenLokal();
            PedsLadenLokal();
            SavesLadenLokal();
            GruppenLadenLokal();
            AutohäuserLadenLokal();
            BankautomatenLadenLokal();
            FahrzeugvermietungenLadenLokal();

            //Speicherungs Timer
            //Timer.SetTimer(AlleSpielerSpeichern, 30000, 0);
            //Timer.SetTimer(FahrzeugeSpeichern, 15000, 0);
            //Timer.SetTimer(AlleTankstellenSpeichern, 40000, 0);
            //Timer.SetTimer(AlleSupermärkteSpeichern, 41000, 0);
            //Timer.SetTimer(AlleImmobilienSpeichern, 42000, 0);
            //Timer.SetTimer(AlleBotsSpeichern, 10000, 0);
            //Timer.SetTimer(AlleAutohäuserSpeichern, 43000, 0);

            //Update Timer
            Timer.SetTimer(AlleTankstellenUpdaten, 20000, 0);
            Timer.SetTimer(AlleSupermärkteUpdaten, 41000, 0);
            Timer.SetTimer(AlleImmobilienUpdaten, 42000, 0);
            Timer.SetTimer(AlleAutohäuserUpdaten, 43000, 0);
            Timer.SetTimer(FahrzeugeUpdaten, 10000, 0);

            //Spieler Timer
            Timer.SetTimer(Alle, 1000, 0);

            //Sonstige Timer
            Timer.SetTimer(WetterAendern, 14400000, 0); //4 Stunden
            Timer.SetTimer(WetterStation, 900000, 0);
            Timer.SetTimer(Uhrzeit, 1000, 0);
            Timer.SetTimer(Fahrzeuge.TachoUpdaten, 300, 0);
            Timer.SetTimer(Fahrzeuge.FahrzeugCheck, 5000, 0);
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
            var Autohäuser = ContextFactory.Instance.srp_autohäuser.Count();
            var Bots = ContextFactory.Instance.srp_bots.Count();
            var Saves = ContextFactory.Instance.srp_saves.Count();
            var Gruppen = ContextFactory.Instance.srp_gruppen.Count();
            var ATMs = ContextFactory.Instance.srp_bankautomaten.Count();
            var Fahrzeugvermietungen = ContextFactory.Instance.srp_fahrzeugvermietungen.Count();
            var RandomSpawns = ContextFactory.Instance.srp_randomspawns.Count();
            var FahrzeugMods = ContextFactory.Instance.srp_fahrzeugmods.Count();

            //Gezählte Werte in der Log ausgeben
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Accounts + " Accounts wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Log + " Log Einträge wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Fahzeuge + " Fahrzeuge wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Tankstellen + " Tankstellen wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + TankstellenPunkte + " Tankstellen Punkte wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + TankstellenInfoPunkte + " Tankstellen Info Punkte wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Immobilien + " Immobilien wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Supermärkte + " Supermärkte wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Autohäuser + " Autohäuser wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Bots + " Peds wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Saves + " Saves wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Gruppen + " Gruppen wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + ATMs + " ATMs wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + Fahrzeugvermietungen + " Fahrzeugvermietungen wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + RandomSpawns + " Randomspawns wurden geladen.");
            NAPI.Util.ConsoleOutput("[StrawberryRP] " + FahrzeugMods + " Fahrzeugmods wurden geladen.");

            //Spieler auf 0 setzen
            var Server = ContextFactory.Instance.srp_server.Where(x => x.Id == 1).FirstOrDefault();
            Server.Online = 0;
            ContextFactory.Instance.SaveChanges();
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
                tankstelle.TankstellenLabel = NAPI.TextLabel.CreateTextLabel(TankstellenText, new Vector3(tankstelle.TankstelleX, tankstelle.TankstelleY, tankstelle.TankstelleZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
                tankstelle.TankstellenMarker = NAPI.Marker.CreateMarker(GlobaleSachen.TankstellenMarker, new Vector3(tankstelle.TankstelleX, tankstelle.TankstelleY, tankstelle.TankstelleZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
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
                haus.ImmobilienLabel = NAPI.TextLabel.CreateTextLabel(ImmobilienText, new Vector3(haus.ImmobilienX, haus.ImmobilienY, haus.ImmobilienZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
                haus.ImmobilienMarker = NAPI.Marker.CreateMarker(GlobaleSachen.ImmobilienMarker, new Vector3(haus.ImmobilienX, haus.ImmobilienY, haus.ImmobilienZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            }
        }

        public static void AllesSpeichern()
        {
            var Spieler = System.Diagnostics.Stopwatch.StartNew();
            AlleSpielerSpeichern();
            Spieler.Stop();
            var SpielerMS = Spieler.ElapsedMilliseconds;
            NAPI.Util.ConsoleOutput("[StrawberryRP] Alle Spieler gespeichert [" + SpielerMS + "ms]");

            var Fahrzeuge = System.Diagnostics.Stopwatch.StartNew();
            FahrzeugeSpeichern();
            Fahrzeuge.Stop();
            var FahrzeugeMS = Fahrzeuge.ElapsedMilliseconds;
            NAPI.Util.ConsoleOutput("[StrawberryRP] Alle Fahrzeuge gespeichert [" + FahrzeugeMS + "ms]");

            var Tankstellen = System.Diagnostics.Stopwatch.StartNew();
            AlleTankstellenSpeichern();
            Tankstellen.Stop();
            var TankstellenMS = Tankstellen.ElapsedMilliseconds;
            NAPI.Util.ConsoleOutput("[StrawberryRP] Alle Tankstellen gespeichert [" + TankstellenMS + "ms]");

            var Supermärkte = System.Diagnostics.Stopwatch.StartNew();
            AlleSupermärkteSpeichern();
            Tankstellen.Stop();
            var SupermärkteMS = Supermärkte.ElapsedMilliseconds;
            NAPI.Util.ConsoleOutput("[StrawberryRP] Alle Supermärkte gespeichert [" + SupermärkteMS + "ms]");

            var Immobilien = System.Diagnostics.Stopwatch.StartNew();
            AlleImmobilienSpeichern();
            Immobilien.Stop();
            var ImmobilienMS = Immobilien.ElapsedMilliseconds;
            NAPI.Util.ConsoleOutput("[StrawberryRP] Alle Immobilien gespeichert [" + ImmobilienMS + "ms]");

            var Bots = System.Diagnostics.Stopwatch.StartNew();
            AlleBotsSpeichern();
            Bots.Stop();
            var BotsMS = Bots.ElapsedMilliseconds;
            NAPI.Util.ConsoleOutput("[StrawberryRP] Alle Bots gespeichert [" + BotsMS + "ms]");

            var Autohäuser = System.Diagnostics.Stopwatch.StartNew();
            AlleAutohäuserSpeichern();
            Autohäuser.Stop();
            var AutohäuserMS = Autohäuser.ElapsedMilliseconds;
            NAPI.Util.ConsoleOutput("[StrawberryRP] Alle Autohäuser gespeichert [" + AutohäuserMS + "ms]");

            var Gruppen = System.Diagnostics.Stopwatch.StartNew();
            AlleGruppenSpeichern();
            Gruppen.Stop();
            var GruppenMS = Gruppen.ElapsedMilliseconds;
            NAPI.Util.ConsoleOutput("[StrawberryRP] Alle Gruppen gespeichert [" + GruppenMS + "ms]");
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
                    supermarkt.SupermarktBlip.Sprite = 52;
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
                    supermarkt.SupermarktBlip.Sprite = 52;
                    supermarkt.SupermarktBlip.Color = 1;
                }

                //TextLabel und Marker erstellen
                supermarkt.SupermarktLabel = NAPI.TextLabel.CreateTextLabel(SupermarktText, new Vector3(supermarkt.SupermarktX, supermarkt.SupermarktY, supermarkt.SupermarktZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
                supermarkt.SupermarktMarker = NAPI.Marker.CreateMarker(GlobaleSachen.SupermarktMarker, new Vector3(supermarkt.SupermarktX, supermarkt.SupermarktY, supermarkt.SupermarktZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            }
        }

        public static void AutohäuserLadenLokal()
        {
            AutohausListe = AlleAutohäuserLadenDB();

            foreach (AutohausLokal autohaus in AutohausListe)
            {
                String AutohausText = null;
                if (autohaus.AutohausBesitzer == 0)
                {
                    AutohausText = "~g~[~w~Autohaus: " + autohaus.Id + "~g~]~n~";
                    AutohausText += "~w~Kaufpreis: " + GeldFormatieren(autohaus.AutohausKaufpreis) + "~n~";
                    AutohausText += "~w~Beschreibung: " + autohaus.AutohausBeschreibung;
                    autohaus.AutohausBlip = NAPI.Blip.CreateBlip(new Vector3(autohaus.AutohausX, autohaus.AutohausY, autohaus.AutohausZ));
                    autohaus.AutohausBlip.Name = autohaus.AutohausBeschreibung;
                    autohaus.AutohausBlip.ShortRange = true;
                    autohaus.AutohausBlip.Sprite = 523;
                    autohaus.AutohausBlip.Color = 2;
                }
                else
                {
                    AutohausText = "~g~[~w~Autohaus: " + autohaus.Id + "~g~]~n~";
                    AutohausText += "~w~Beschreibung: " + autohaus.AutohausBeschreibung + "~n~";
                    AutohausText += "~w~Besitzer: " + BesitzerNamenBekommen(autohaus.AutohausBesitzer);
                    autohaus.AutohausBlip = NAPI.Blip.CreateBlip(new Vector3(autohaus.AutohausX, autohaus.AutohausY, autohaus.AutohausZ));
                    autohaus.AutohausBlip.Name = autohaus.AutohausBeschreibung;
                    autohaus.AutohausBlip.ShortRange = true;
                    autohaus.AutohausBlip.Sprite = 523;
                    autohaus.AutohausBlip.Color = 1;
                }

                //TextLabel und Marker erstellen
                autohaus.AutohausLabel = NAPI.TextLabel.CreateTextLabel(AutohausText, new Vector3(autohaus.AutohausX, autohaus.AutohausY, autohaus.AutohausZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
                autohaus.AutohausMarker = NAPI.Marker.CreateMarker(GlobaleSachen.AutohausMarker, new Vector3(autohaus.AutohausX, autohaus.AutohausY, autohaus.AutohausZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            }
        }

        public static void BankautomatenLadenLokal()
        {
            ATMListe = AlleBankautomatenLadenDB();

            foreach (BankautomatenLokal atm in ATMListe)
            {
                String ATMText = null;
                ATMText = "~g~[~w~ATM~g~]~n~";
                atm.ATMBlip = NAPI.Blip.CreateBlip(new Vector3(atm.PositionX, atm.PositionY, atm.PositionZ));
                atm.ATMBlip.Name = "ATM";
                atm.ATMBlip.ShortRange = true;
                atm.ATMBlip.Sprite = 500;

                //TextLabel und Marker erstellen
                atm.ATMText = NAPI.TextLabel.CreateTextLabel(ATMText, new Vector3(atm.PositionX, atm.PositionY, atm.PositionZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
                atm.ATMMarker = NAPI.Marker.CreateMarker(GlobaleSachen.ATMMarker, new Vector3(atm.PositionX, atm.PositionY, atm.PositionZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            }
        }

        public static void RandomSpawnsLaden()
        {
            RandomSpawnListe = AlleRandomSpawnsLadenDB();
        }

        public static void FahrzeugModsLaden()
        {
            FModsListe = AlleFahrzeugModsLadenDB();
        }

        public static void FahrzeugvermietungenLadenLokal()
        {
            FVermietungListe = AlleFahrzeugvermiertungenLadenDB();

            foreach (FahrzeugvermietungenLokal fverm in FVermietungListe)
            {
                String fVermText = null;
                fVermText = "~g~[~w~Fahrzeugvermietung~g~]~n~";
                fverm.FVermietungBlip = NAPI.Blip.CreateBlip(new Vector3(fverm.PositionX, fverm.PositionY, fverm.PositionZ));
                fverm.FVermietungBlip.Name = "Fahrzeugvermietung";
                fverm.FVermietungBlip.ShortRange = true;
                fverm.FVermietungBlip.Sprite = 81;
                fverm.FVermietungBlip.Color = 17;

                //TextLabel und Marker erstellen
                fverm.FVermietungText = NAPI.TextLabel.CreateTextLabel(fVermText, new Vector3(fverm.PositionX, fverm.PositionY, fverm.PositionZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
                fverm.FVermietungMarker = NAPI.Marker.CreateMarker(GlobaleSachen.FVermietungMarker, new Vector3(fverm.PositionX, fverm.PositionY, fverm.PositionZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            }
        }

        public static void PedsLadenLokal()
        {
            BotListe = AlleBotsLadenDB();
        }

        public static void SavesLadenLokal()
        {
            SaveListe = AlleSavesLadenDB();
        }

        public static void GruppenLadenLokal()
        {
            GruppenListe = AlleGruppenLadenDB();
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
                tankpunkt.TankstellenPunktLabel = NAPI.TextLabel.CreateTextLabel(TankstellenPunktText, new Vector3(tankpunkt.TankstellenPunktX, tankpunkt.TankstellenPunktY, tankpunkt.TankstellenPunktZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
                tankpunkt.TankstellenPunktMarker = NAPI.Marker.CreateMarker(GlobaleSachen.TankstellenZapfsäuleMarker, new Vector3(tankpunkt.TankstellenPunktX, tankpunkt.TankstellenPunktY, tankpunkt.TankstellenPunktZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
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
                tankinfo.TankstellenInfoLabel = NAPI.TextLabel.CreateTextLabel(TankstellenInfoText, new Vector3(tankinfo.TankstellenInfoX, tankinfo.TankstellenInfoY, tankinfo.TankstellenInfoZ), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
                tankinfo.TankstellenInfoMarker = NAPI.Marker.CreateMarker(GlobaleSachen.TankstellenInfoMarker, new Vector3(tankinfo.TankstellenInfoX, tankinfo.TankstellenInfoY, tankinfo.TankstellenInfoZ), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            }
        }

        public static int HatImmobilie(Client Player)
        {
            int HatImmobilie = 0;
            foreach (ImmobilienLokal immobilielocal in ImmobilienListe)
            {
                if (immobilielocal.ImmobilienBesitzer == Player.GetData("Id"))
                {
                    HatImmobilie = 1;
                    break;
                }
            }
            return HatImmobilie;
        }

        public static int HatBiz(Client Player)
        {
            int HatBiz = 0;
            foreach (TankstelleLokal tankstellelocal in TankenListe)
            {
                if (tankstellelocal.TankstelleBesitzer == Player.GetData("Id"))
                {
                    HatBiz = 1;
                    break;
                }
            }
            
            if(HatBiz == 0)
            {
                foreach (SupermarktLokal supermarktlocal in SupermarktListe)
                {
                    if (supermarktlocal.SupermarktBesitzer == Player.GetData("Id"))
                    {
                        HatBiz = 1;
                        break;
                    }
                }
            }
            else if (HatBiz == 0)
            {
                foreach (AutohausLokal autohauslocal in AutohausListe)
                {
                    if (autohauslocal.AutohausBesitzer == Player.GetData("Id"))
                    {
                        HatBiz = 1;
                        break;
                    }
                }
            }
            return HatBiz;
        }

        public static TankstelleLokal NaheTankeBekommen(Client Player, float distance = 2.0f)
        {
            TankstelleLokal Tankstelle = null;
            foreach (TankstelleLokal tankstellelocal in TankenListe)
            {
                if (Player.Position.DistanceTo(new Vector3(tankstellelocal.TankstelleX, tankstellelocal.TankstelleY, tankstellelocal.TankstelleZ)) < distance)
                {
                    Tankstelle = tankstellelocal;
                    break;
                }
            }
            return Tankstelle;
        }

        public static BotLokal NahePedBekommen(Client Player, float distance = 2.0f)
        {
            BotLokal Bot = null;
            foreach (BotLokal botlocal in BotListe)
            {
                if (Player.Position.DistanceTo(new Vector3(botlocal.BotX, botlocal.BotY, botlocal.BotZ)) < distance)
                {
                    Bot = botlocal;
                    break;
                }
            }
            return Bot;
        }

        public static void PedsFürSpielerLaden(Client Player)
        {

            foreach (BotLokal botlocal in BotListe)
            {
                var botPosition = new Vector3(botlocal.BotX, botlocal.BotY, botlocal.BotZ);
                var botRotation = new Vector3(0.0, 0.0, botlocal.BotKopf);
                Player.TriggerEvent("boterstellen", botlocal.BotName, botPosition.X, botPosition.Y, botPosition.Z, botRotation.Z);
            }
        }

        public static BankautomatenLokal NaheBankautomatenBekommen(Client Player, float distance = 2.0f)
        {
            BankautomatenLokal atm = null;
            foreach (BankautomatenLokal atmlocal in ATMListe)
            {
                if (Player.Position.DistanceTo(new Vector3(atmlocal.PositionX, atmlocal.PositionY, atmlocal.PositionZ)) < distance)
                {
                    atm = atmlocal;
                    break;
                }
            }
            return atm;
        }

        public static FahrzeugvermietungenLokal NaheFahrzeugvermietungBekommen(Client Player, float distance = 2.0f)
        {
            FahrzeugvermietungenLokal fverm = null;
            foreach (FahrzeugvermietungenLokal fvermlocal in FVermietungListe)
            {
                if (Player.Position.DistanceTo(new Vector3(fvermlocal.PositionX, fvermlocal.PositionY, fvermlocal.PositionZ)) < distance)
                {
                    fverm = fvermlocal;
                    break;
                }
            }
            return fverm;
        }

        public static int RandomSpawnBekommen(String Name)
        {
            int i = 0, ii = 0;
            foreach (RandomSpawns randomspawn in RandomSpawnListe)
            {
                i += 1;
            }

            var idarray = new int[i + 1];

            foreach (RandomSpawns randomspawn in RandomSpawnListe)
            {
                if(randomspawn.Name == Name)
                {
                    ii += 1;
                    idarray[ii] = randomspawn.Id;
                }
            }

            Random random = new Random();
            int start2 = random.Next(1, idarray.Length);

            return idarray[start2];
        }

        public static RandomSpawns RandomSpawnObjektBekommen(int Id)
        {
            RandomSpawns rsgefunden = null;
            foreach (RandomSpawns rs in RandomSpawnListe)
            {
                if(rs.Id == Id)
                {
                    rsgefunden = rs;
                    break;
                }
            }
            return rsgefunden;
        }

        public static SupermarktLokal NaheSupermarktBekommen(Client Player, float distance = 2.0f)
        {
            SupermarktLokal Supermarkt = null;
            foreach (SupermarktLokal supermarktlocal in SupermarktListe)
            {
                if (Player.Position.DistanceTo(new Vector3(supermarktlocal.SupermarktX, supermarktlocal.SupermarktY, supermarktlocal.SupermarktZ)) < distance)
                {
                    Supermarkt = supermarktlocal;
                    break;
                }
            }
            return Supermarkt;
        }

        public static AutohausLokal NaheAutohausBekommen(Client Player, float distance = 2.0f)
        {
            AutohausLokal Autohaus = null;
            foreach (AutohausLokal autohauslocal in AutohausListe)
            {
                if (Player.Position.DistanceTo(new Vector3(autohauslocal.AutohausX, autohauslocal.AutohausY, autohauslocal.AutohausZ)) < distance)
                {
                    Autohaus = autohauslocal;
                    break;
                }
            }
            return Autohaus;
        }

        public static TankstellenPunktLokal NaheTankePunktBekommen(Client Player, float distance = 4.0f)
        {
            TankstellenPunktLokal TankstellenPunkt = null;
            foreach (TankstellenPunktLokal tankstellenpunktlocal in TankenPunktListe)
            {
                if (Player.Position.DistanceTo(new Vector3(tankstellenpunktlocal.TankstellenPunktX, tankstellenpunktlocal.TankstellenPunktY, tankstellenpunktlocal.TankstellenPunktZ)) < distance)
                {
                    TankstellenPunkt = tankstellenpunktlocal;
                    break;
                }
            }
            return TankstellenPunkt;
        }

        public static AutoLokal NaheAutosBekommen(Client Player, float distance = 4.0f)
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
                            break;
                        }
                    }
                }
            }
            return Auto;
        }

        public static AutoLokal NaheAutosBekommenVonInnen(Client Player)
        {
            AutoLokal Auto = null;
            
            //Benötigte Definitionen
            int FahrzeugID = Player.Vehicle.GetData("Id");

            foreach (AutoLokal autolocal in AutoListe)
            {
                if (FahrzeugID == autolocal.Id)
                {
                    Auto = autolocal;
                    break;
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

        public static AutoLokal AutoBekommen(Vehicle Fahrzeug)
        {
            AutoLokal Auto = null;
            foreach (AutoLokal auto in AutoListe)
            {
                if (Fahrzeug == auto.Fahrzeug)
                {
                    Auto = auto;
                    break;
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
                    break;
                }
            }
            return Auto;
        }

        public static TankstelleLokal TankeVonIdBekommen(int Id)
        {
            TankstelleLokal Tanke = null;
            foreach (TankstelleLokal tanke in TankenListe)
            {
                if (tanke.Id == Id)
                {
                    Tanke = tanke;
                    break;
                }
            }
            return Tanke;
        }

        public static AutohausLokal AutohausVonIdBekommen(int Id)
        {
            AutohausLokal Autohaus = null;
            foreach (AutohausLokal autohauslocal in AutohausListe)
            {
                if (autohauslocal.Id == Id)
                {
                    Autohaus = autohauslocal;
                    break;
                }
            }
            return Autohaus;
        }

        public static AccountLokal AccountBekommen(Client Player)
        {
            AccountLokal acc = null;
            foreach (AccountLokal account in AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    acc = account;
                    break;
                }
            }
            return acc;
        }

        public static int AccountHatAutohaus(Client Player)
        {
            int ah = 0;
            foreach (AutohausLokal autohaus in AutohausListe)
            {
                if (Player.GetData("Id") == autohaus.AutohausBesitzer)
                {
                    ah = 1;
                    break;
                }
            }
            return ah;
        }

        public static int AccountAutohausBekommen(Client Player)
        {
            int ahid = 0;
            foreach (AutohausLokal autohaus in AutohausListe)
            {
                if (Player.GetData("Id") == autohaus.AutohausBesitzer)
                {
                    ahid = autohaus.Id;
                    break;
                }
            }
            return ahid;
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
                    break;
                }
            }
            return AdminLevel;
        }

        public static String AccountGruppenRangNamenBekommen(int Gruppe, int Rang)
        {
            //Benötigte Definitionen
            String RangName = null;

            foreach (GruppenLokal gruppe in GruppenListe)
            {
                if (gruppe.Id == Gruppe)
                {
                    if(Rang == 1) { RangName = gruppe.GruppenRang1Name; }
                    else if (Rang == 2) { RangName = gruppe.GruppenRang2Name; }
                    else if (Rang == 3) { RangName = gruppe.GruppenRang3Name; }
                    else if (Rang == 4) { RangName = gruppe.GruppenRang4Name; }
                    else if (Rang == 5) { RangName = gruppe.GruppenRang5Name; }
                    break;
                }
            }
            return RangName;
        }

        public static Vehicle AccountJobFahrzeugBekommen(Client Player)
        {
            //Benötigte Definitionen
            Vehicle Fahrzeug = null;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    Fahrzeug = account.JobFahrzeug;
                    break;
                }
            }
            return Fahrzeug;
        }

        public static int AccountSpielzeitBekommen(Client Player)
        {
            //Benötigte Definitionen
            int Spielzeit = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    Spielzeit = account.Spielzeit;
                    break;
                }
            }
            return Spielzeit;
        }

        public static String AccountSpielzeitBerechnen(Client Player)
        {
            //Benötigte Definitionen
            int SpielzeitMinuten = 0;
            int SpielzeitStunden = 0;
            String SpielzeitBerechnet = null;

            SpielzeitMinuten = AccountSpielzeitBekommen(Player);

            if(SpielzeitMinuten >= 60)
            {
                SpielzeitStunden = SpielzeitMinuten / 60;
            }
            

            if(SpielzeitMinuten <= 60)
            {
                SpielzeitBerechnet = SpielzeitMinuten + " Minuten";
            }
            else if (SpielzeitMinuten > 60)
            {
                SpielzeitBerechnet = SpielzeitStunden + " Stunden";
            }

            return SpielzeitBerechnet;
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
                    break;
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
                    break;
                }
            }
            return Geld;
        }

        public static long AccountTutorialBekommen(Client Player)
        {
            //Benötigte Definitionen
            int Tutorial = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    Tutorial = account.Tutorial;
                    break;
                }
            }
            return Tutorial;
        }

        public static String AccountVerheiratetBekommen(Client Player)
        {
            //Benötigte Definitionen
            String Name = null;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    Name = account.Verheiratet;
                    break;
                }
            }
            return Name;
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
                    break;
                }
            }
            return BankGeld;
        }

        public static int AccountGruppeBekommen(Client Player)
        {
            //Benötigte Definitionen
            int Gruppierung = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    Gruppierung = account.Gruppe;
                    break;
                }
            }
            return Gruppierung;
        }

        public static int AccountGruppeRangBekommen(Client Player)
        {
            //Benötigte Definitionen
            int Rang = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    Rang = account.GruppenRang;
                    break;
                }
            }
            return Rang;
        }

        public static void AccountGruppeSetzen(Client Player, int Id)
        {
            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    account.Gruppe = Id;
                    account.AccountGeändert = true;
                    break;
                }
            }
        }

        public static void AccountGruppeRangSetzen(Client Player, int Rang)
        {
            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    account.GruppenRang = Rang;
                    account.AccountGeändert = true;
                    break;
                }
            }
        }

        public static void AccountTutorialSetzen(Client Player, int Tutorial)
        {
            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    account.Tutorial = Tutorial;
                    account.AccountGeändert = true;
                    break;
                }
            }
        }

        public static long AccountGesamtVermögenBekommen(Client Player)
        {
            //Benötigte Definitionen
            long BankGeld = 0;
            long HandGeld = 0;
            long GesamtGeld = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    BankGeld = account.BankGeld;
                    HandGeld = account.Geld;
                    GesamtGeld = BankGeld + HandGeld;
                }
            }
            return GesamtGeld;
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

        public static void AccountGeldSetzen(Client Player, int Wie, long Geld)
        {
            AccountLokal account = new AccountLokal();
            account = AccountBekommen(Player);

            if (Wie == 1)
            {
                account.Geld += Geld;
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Geld: + " + GeldFormatieren(Geld));
            }
            else
            {
                account.Geld += Geld;
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Geld: - " + GeldFormatieren(Geld));
            }

            account.AccountGeändert = true;
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

        public static void AccountFraktionSetzen(Client Player, int Fraktion)
        {
            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    account.Fraktion = Fraktion;
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

        public static int AccountJobBekommen(Client Player)
        {
            //Benötigte Definitionen
            int Job = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    Job = account.Job;
                }
            }
            return Job;
        }

        public static Boolean FahrzeugModVorhanden(String Name)
        {
            //Benötigte Definitionen
            Boolean Gefunden = false;

            foreach (FahrzeugMods fmod in FModsListe)
            {
                if(fmod.Name == Name)
                {
                    Gefunden = true;
                    break;
                }
            }
            return Gefunden;
        }

        public static int AccountExpBekommen(Client Player)
        {
            //Benötigte Definitionen
            int Exp = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    Exp = account.Exp;
                }
            }
            return Exp;
        }

        public static String AccountJobInNamen(Client Player)
        {
            //Benötigte Definitionen
            int Job = 0;
            String JobName;

            Job = AccountJobBekommen(Player);

            switch (Job)
            {
                case 0:
                    JobName = "Zivilist";
                    break;
                case 1:
                    JobName = "Berufskraftfahrer";
                    break;
                case 2:
                    JobName = "Busfahrer";
                    break;
                default:
                    JobName = "Unbekannter Job";
                    break;
            }
            return JobName;
        }

        public static String AccountLevelBekommen(Client Player)
        {
            //Benötigte Definitionen
            int Exp = 0;
            String LevelName = null;

            Exp = AccountExpBekommen(Player);

            if(Exp < 500)
            {
                LevelName = "Level 1";
            }
            else if (Exp >= 500 && Exp < 1000)
            {
                LevelName = "Level 2";
            }

            return LevelName;
        }

        public static DateTime AccountGeburtstagBekommen(Client Player)
        {
            //Benötigte Definitionen
            DateTime Geburtstag = DateTime.Now;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    Geburtstag = account.GeburtsDatum;
                }
            }
            return Geburtstag;
        }

        public static int AccountBerufskraftfahrerVolumenBerechnen(Client Player)
        {
            //Benötigte Definitionen
            int EXP = 0;
            int Volumen = 0;

            foreach (AccountLokal account in Funktionen.AccountListe)
            {
                if (Player.GetData("Id") == account.Id)
                {
                    EXP = account.BerufskraftfahrerExp;
                }
            }

            if(EXP < 1000) { Volumen = 200; }
            else if(EXP >= 1000 && EXP < 2000) { Volumen = 400; }
            else if (EXP >= 2000 && EXP < 3000) { Volumen = 600; }
            else if (EXP >= 3000 && EXP < 4000) { Volumen = 800; }
            else if (EXP >= 4000 && EXP < 5000) { Volumen = 1000; }

            return Volumen;
        }

        public static void AccountBerufskraftfahrerExpSetzen(Client Player, int Wie, int Exp)
        {
            AccountLokal account = new AccountLokal();
            account = AccountBekommen(Player);

            if(Wie == 1)
            {
                account.BerufskraftfahrerExp += Exp;
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Berufskraftfahrer + " + Exp + " Exp");
            }
            else
            {
                account.BerufskraftfahrerExp -= Exp;
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Berufskraftfahrer - " + Exp + " Exp");
            }

            account.AccountGeändert = true;
        }

        public static void AccountPositionInteriorDimensionUpdaten(Client Player)
        {
            AccountLokal account = new AccountLokal();
            account = AccountBekommen(Player);

            account.PositionX = Player.Position.X;
            account.PositionY = Player.Position.Y;
            account.PositionZ = Player.Position.Z;
            account.PositionRot = Player.Rotation.Z;
            account.Dimension = Player.Dimension;
            account.Interior = Player.GetData("InteriorName");

            account.AccountGeändert = true;
        }

        public static void AccountJobFahrzeugSetzen(Client Player, Vehicle Fahrzeug)
        {
            AccountLokal account = new AccountLokal();
            account = AccountBekommen(Player);

            account.JobFahrzeug = Fahrzeug;
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

        public static int HatTutorialFahrzeug(Client Player)
        {
            int i = 0;
            foreach (AutoLokal auto in AutoListe)
            {
                if (auto.FahrzeugSpieler == Player.GetData("Id") && auto.FahrzeugTyp == 2 && auto.FahrzeugBeschreibung == "Nicos Auto")
                {
                    i = 1;
                    break;
                }
            }
            return i;
        }

        public static void MietFahrzeugSetzen(Client Player)
        {
            foreach (AutoLokal auto in AutoListe)
            {
                if (auto.FahrzeugSpieler == Player.GetData("Id") && auto.FahrzeugTyp == 2)
                {
                    if(auto.FahrzeugMaxMietzeit > auto.FahrzeugMietzeit)
                    {
                        auto.FahrzeugMietzeit += 1;
                    }
                    else if (auto.FahrzeugMaxMietzeit == auto.FahrzeugMietzeit)
                    {
                        //Hier dann alles weitere was passieren soll, falls ein Spieler meint seine Karre nicht zurück zu bringen ;)
                    }

                    //Damit wir auch speichern
                    auto.FahrzeugGeändert = true;
                }
            }
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
            else if(Account != null)
            {
                Name = Account.NickName;
            }
            else
            {
                Name = "Niemand";
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

        public static void HeiratenSound()
        {
            foreach (var Spieler in NAPI.Pools.GetAllPlayers())
            {
                if (Spieler.Position.DistanceTo(new Vector3(-329.9244, 6150.168, 32.31319)) < 500.0f)
                {
                    Spieler.TriggerEvent("soundbrowseroeffnen");
                    Spieler.TriggerEvent("SoundStarten", 1);
                    Spieler.SetData("HeiratenBrowser", 1);
                }
            }

            //Sound beenden
            Timer.SetTimer(HeiratenSoundBeenden, 60000, 1);

        }

        public static void Ladebalken(Client Player, int Typ, uint ZeitMS)
        {
            //CEF öffnen
            Player.TriggerEvent("ladebalkenoeffnen");

            //Ladebalken starten
            Player.TriggerEvent("Ladebalken", Typ, ZeitMS);

            //Timer zum Beenden
            Timer.SetTimer(() => LadebalkenBeenden(Player), ZeitMS, 1);
        }

        public static void LadebalkenBeenden(Client Player)
        {
            //CEF schliessen
            Player.TriggerEvent("ladebalkenschliessen");
        }

        public static void HeiratenBeenden(Client Player)
        {
            if(GlobaleSachen.Heiraten == 1)
            {
                if (NAPI.Player.IsPlayerConnected(Player))
                {
                    if (AccountVerheiratetBekommen(Player) == "Nein")
                    {
                        NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Heirat wurde beendet da niemand angenommen / abgelehnt hat.");
                        Player.SetData("HeiratenId", 0);
                        Player.SetData("HeiratsId", 0);
                        Player.SetData("HeiratsAntrag", 0);
                    }
                }

                foreach (var Spieler in NAPI.Pools.GetAllPlayers())
                {
                    if (Spieler.GetData("HeiratsId") == Player.GetData("Id"))
                    {
                        if (AccountVerheiratetBekommen(Player) == "Nein")
                        {
                            NAPI.Notification.SendNotificationToPlayer(Spieler, "~y~Info~w~: Die Heirat wurde beendet da niemand angenommen / abgelehnt hat.");
                            Spieler.SetData("HeiratsId", 0);
                            Spieler.SetData("HeiratenId", 0);
                            Spieler.SetData("HeiratsAntrag", 0);
                        }
                    }
                }

                //Heiraten Resetten
                GlobaleSachen.Heiraten = 0;
            }
        }

        public static void HeiratenSoundBeenden()
        {
            foreach (var Spieler in NAPI.Pools.GetAllPlayers())
            {
                if (Spieler.GetData("HeiratenBrowser") == 1)
                {
                    Spieler.TriggerEvent("soundbrowserschliessen");
                    Spieler.SetData("HeiratenBrowser", 0);
                }
            }

            //Kirche wieder freigeben
            GlobaleSachen.Heiraten = 0;
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
                                NAPI.Notification.SendNotificationToPlayer(Spieler, "~y~Info~w~: Die Polizei wurde darüber benachrichtigt das du deine Rechnung nicht bezahlt hast.");
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
                    if (Random == 4) { GlobaleSachen.ServerWetter = 6; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 6; }
                    break;
                case 3:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 2; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 2; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 2; }
                    break;
                case 4:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 2; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 2; }
                    break;
                case 5:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 0; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 2; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 1; }
                    break;
                case 6:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 0; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 0; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 0; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 0; }
                    break;
                case 7:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 0; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 0; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 0; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 0; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 0; }
                    break;
                case 8:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 0; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 0; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 0; }
                    break;
                case 9:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 2; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 2; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 1; }
                    break;
                case 10:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 2; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 2; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 1; }
                    break;
                case 11:
                    if (Random == 1) { GlobaleSachen.ServerWetter = 1; }
                    if (Random == 2) { GlobaleSachen.ServerWetter = 2; }
                    if (Random == 3) { GlobaleSachen.ServerWetter = 11; }
                    if (Random == 4) { GlobaleSachen.ServerWetter = 10; }
                    if (Random == 5) { GlobaleSachen.ServerWetter = 1; }
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
                    OnlineRekord = 0,
                    OnlineRekordDatum = DateTime.Now,
                    Online = 0
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
                    AdminLevelName = "SPieler";
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

            if(Eingeloggt == 0)
            {
                Player.TriggerEvent("LoginEnterTaste");
            }
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

                //Tanke erfassen für Sprit abzug
                TankstelleLokal dietanke = new TankstelleLokal();
                dietanke = TankeVonIdBekommen(Player.GetData("TankenTankstellenId"));

                //Dem Spieler nun sagen das er bezahlen soll
                int iLiter = (int)Liter;
                if(Diesel == 1) { TankRechnung = DieselPreis * iLiter; dietanke.TankstelleDiesel -= iLiter; }
                else if (E10 == 1) { TankRechnung = E10Preis * iLiter; dietanke.TankstelleE10 -= iLiter; }
                else if (Super == 1) { TankRechnung = SuperPreis * iLiter; dietanke.TankstelleSuper -= iLiter; }

                //Tanke wurde geändert
                dietanke.TankstelleGeändert = true;

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
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dein Fahrzeug wurde getankt.");
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Bezahle nun an der Tankstelle " + GeldFormatieren(TankRechnung));
                Player.TriggerEvent("tankenschliessen");

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
        }

        [RemoteEvent("IBerry")]
        public static void IBerry(Client Player)
        {
            if (Player.GetData("Eingeloggt") == 0) { return; }

            //Cooldown
            if (Player.GetData("KeyCoolDown") == 1) { return; }
            Player.SetData("KeyCoolDown", 1);
            Timer.SetTimer(() => KeyCoolDown(Player), GlobaleSachen.KeyCoolDownZeit, 1);

            if (Player.GetData("IBerry") == 0)
            {
                //Damit wir wissen das er das CEF geöffnet hat
                Player.SetData("IBerry", 1);

                //Alle Daten vom Spieler laden
                String Nickname = Player.Name;
                String Level = AccountLevelBekommen(Player);
                String Playtime = AccountSpielzeitBerechnen(Player);
                String Lastonline = "%";
                int Exp = AccountExpBekommen(Player);
                String Geschlecht = "%";
                String Ehepartner = AccountVerheiratetBekommen(Player);
                String Job = AccountJobInNamen(Player);
                String Geld = GeldFormatieren(AccountGesamtVermögenBekommen(Player));
                String Handynummer = "%";

                //CEF öffnen
                Player.TriggerEvent("IBerryoeffnen");

                //Daten an das CEF übergeben
                Player.TriggerEvent("IBerry_Laden_Server", 19);
                Player.TriggerEvent("IBerry_Laden_Spieler", Nickname, Level, Playtime, Lastonline, Exp, Geschlecht, Ehepartner, Job, Geld, Handynummer);

                //Log Eintrag
                LogEintrag(Player, "IBerry geöffnet");
            }
            else
            {
                Player.SetData("IBerry", 0);

                Player.TriggerEvent("IBerryschliessen");

                //Log Eintrag
                LogEintrag(Player, "IBerry geschlossen");
            }
        }

        [RemoteEvent("Fahrzeugverwaltung_Privat_Abbrechen")]
        public static void FahrzeugVerwaltungPrivatAbbrechen(Client Player)
        {
            Player.SetData("FahrzeugPrivatDialog", 0);
        }

        [RemoteEvent("Fahrzeug_Privat_Abschliessen")]
        public static void FahrzeugPrivatAbschliessen(Client Player, int Status)
        {
            AutoLokal auto = new AutoLokal();
            auto = NaheEigenesAutoBekommenPrivat(Player);

            if (auto != null)
            {
                if(Status == 1)
                {
                    auto.FahrzeugAbgeschlossen = 1;
                    NAPI.Vehicle.SetVehicleLocked(auto.Fahrzeug, true);
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast dein Fahrzeug abgeschlossen.");
                    auto.FahrzeugGeändert = true;
                }
                else
                {
                    auto.FahrzeugAbgeschlossen = 0;
                    NAPI.Vehicle.SetVehicleLocked(auto.Fahrzeug, false);
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast dein Fahrzeug aufgeschlossen.");
                    auto.FahrzeugGeändert = true;
                }
            }
        }

        [RemoteEvent("BCheck")]
        public static void BCheck(Client Player)
        {
            //Cooldown
            if (Player.GetData("KeyCoolDown") == 1) { return; }
            Player.SetData("KeyCoolDown", 1);
            Timer.SetTimer(() => KeyCoolDown(Player), GlobaleSachen.KeyCoolDownZeit, 1);

            //Wichtige Abfragen
            if (Player.GetData("Eingeloggt") == 0) { return; }

            //Alles was nicht mit Fahrzeugen zu tun hat
            if (!Player.IsInVehicle)
            {
                //Stadthalle Eingang
                if (Player.Position.DistanceTo(new Vector3(337.764, -1562.13, 30.298)) < 3.0f)
                {

                    InteriorLaden(Player, "StadthalleINT");
                    Player.Position = new Vector3(334.039, -1564.35, 30.3066);
                    Freeze(Player);
                    Timer.SetTimer(() => Unfreeze(Player), 3000, 1);
                    return;
                }
                //Stadthalle Ausgang
                else if (Player.Position.DistanceTo(new Vector3(334.039, -1564.35, 30.3066)) < 3.0f)
                {
                    InteriorLaden(Player, "0");
                    Player.Position = new Vector3(337.764, -1562.13, 30.298);
                    Player.Rotation = new Vector3(0.0, 0.0, 309.4669);
                    Freeze(Player);
                    Timer.SetTimer(() => Unfreeze(Player), 3000, 1);
                    return;
                }
                //Arbeitsamt Eingang
                else if (Player.Position.DistanceTo(new Vector3(250.553, -1594.62, 31.5322)) < 4.0f)
                {
                    InteriorLaden(Player, "ArbeitsamtINT");
                    Player.Position = new Vector3(249.744, -1597.58, 25.5466);
                    Freeze(Player);
                    Timer.SetTimer(() => Unfreeze(Player), 3000, 1);
                    return;
                }
                //Arbeitsamt Ausgang
                else if (Player.Position.DistanceTo(new Vector3(249.744, -1597.58, 25.5466)) < 4.0f)
                {
                    InteriorLaden(Player, "0");
                    Player.Position = new Vector3(250.553, -1594.62, 31.5322);
                    Player.Rotation = new Vector3(0.0, 0.0, 309.4669);
                    Freeze(Player);
                    Timer.SetTimer(() => Unfreeze(Player), 3000, 1);
                    return;
                }

                //Schauen ob ein Haus in der nähe ist
                ImmobilienLokal haus = new ImmobilienLokal();
                haus = NaheImmobilieBekommen(Player);

                if(haus != null)
                {
                    if (Player.Position.DistanceTo(new Vector3(haus.ImmobilienX, haus.ImmobilienY, haus.ImmobilienZ)) < 2.0f)
                    {
                        InteriorLaden(Player, haus.ImmobilienInteriorName);
                        Player.Position = new Vector3(haus.ImmobilienEingangX, haus.ImmobilienEingangY, haus.ImmobilienEingangZ);
                        uint Dimension = Convert.ToUInt32(haus.Id);
                        NAPI.Entity.SetEntityDimension(Player, Dimension);
                        Freeze(Player);
                        Timer.SetTimer(() => Unfreeze(Player), 3000, 1);
                        return;
                    }
                    else if (Player.Position.DistanceTo(new Vector3(haus.ImmobilienEingangX, haus.ImmobilienEingangY, haus.ImmobilienEingangZ)) < 2.0f)
                    {
                        InteriorLaden(Player, "0");
                        Player.Position = new Vector3(haus.ImmobilienX, haus.ImmobilienY, haus.ImmobilienZ);
                        Player.Dimension = 0;
                        Freeze(Player);
                        Timer.SetTimer(() => Unfreeze(Player), 3000, 1);
                        return;
                    }
                }
            }
        }

        [RemoteEvent("NCheck")]
        public static void NCheck(Client Player)
        {
            //Cooldown
            if (Player.GetData("KeyCoolDown") == 1) { return; }
            Player.SetData("KeyCoolDown", 1);
            Timer.SetTimer(() => KeyCoolDown(Player), GlobaleSachen.KeyCoolDownZeit, 1);
            
            if(AccountAdminLevelBekommen(Player) == 0) { return; }

            if(Player.GetData("Chat") == 0)
            {
                Player.TriggerEvent("Chatzeigen");
                Player.SetData("Chat", 1);
            }
            else
            {
                Player.TriggerEvent("Chathiden");
                Player.SetData("Chat", 0);
            }
        }

        [RemoteEvent("F2Check")]
        public static void F2Check(Client Player)
        {
            if(Player.GetData("Eingeloggt") == 0) { return; }

            //Cooldown
            if (Player.GetData("KeyCoolDown") == 1) { return; }
            Player.SetData("KeyCoolDown", 1);
            Timer.SetTimer(() => KeyCoolDown(Player), GlobaleSachen.KeyCoolDownZeit, 1);

            //Benötigte Abfragen
            if (Player.GetData("FahrzeugPrivatDialog") == 1) { Player.TriggerEvent("Fahrzeugverwaltung_Privat_Abbrechen"); Player.SetData("FahrzeugPrivatDialog", 0); return; }

            if (FahrzeugeZählenPrivat(Player) != 0)
            {
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
            //Cooldown
            if (Player.GetData("KeyCoolDown") == 1) { return; }
            Player.SetData("KeyCoolDown", 1);
            Timer.SetTimer(() => KeyCoolDown(Player), GlobaleSachen.KeyCoolDownZeit, 1);

            //Wichtige Abfragen
            if (Player.GetData("Eingeloggt") == 0) { return; }

            //Alles was nicht mit Fahrzeugen zu tun hat
            if (!Player.IsInVehicle)
            {
                TankstelleLokal tanke = new TankstelleLokal();
                tanke = NaheTankeBekommen(Player);

                if (tanke != null)
                {
                    if (tanke.TankstelleBesitzer == 0)
                    {
                        if(HatBiz(Player) == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits ein Business."); return; }
                        if (Player.GetData("KaufenTyp") == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Schließe erst das aktuelle Fenster."); return; }
                        Player.SetData("KaufenTyp", 1);
                        Player.SetData("KaufenId", tanke.Id);
                        Player.SetData("KaufenPreis", tanke.TankstelleKaufpreis);

                        Player.TriggerEvent("Kaufen", 1, GeldFormatieren(tanke.TankstelleKaufpreis));
                        return;
                    }
                }

                ImmobilienLokal haus = new ImmobilienLokal();
                haus = NaheImmobilieBekommen(Player);

                if (haus != null)
                {
                    if (haus.ImmobilienBesitzer == 0)
                    {
                        if (HatImmobilie(Player) == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits ein Haus."); return; }
                        if (Player.GetData("KaufenTyp") == 2) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Schließe erst das aktuelle Fenster."); return; }
                        Player.SetData("KaufenTyp", 2);
                        Player.SetData("KaufenId", haus.Id);
                        Player.SetData("KaufenPreis", haus.ImmobilienKaufpreis);

                        //Freeze
                        Freeze(Player);

                        Player.TriggerEvent("Kaufen", 2, GeldFormatieren(haus.ImmobilienKaufpreis));
                        return;
                    }
                }

                SupermarktLokal supermarkt = new SupermarktLokal();
                supermarkt = NaheSupermarktBekommen(Player);

                if (supermarkt != null)
                {
                    if (supermarkt.SupermarktBesitzer == 0)
                    {
                        if (HatBiz(Player) == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits ein Business."); return; }
                        if (Player.GetData("KaufenTyp") == 3) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Schließe erst das aktuelle Fenster."); return; }
                        Player.SetData("KaufenTyp", 3);
                        Player.SetData("KaufenId", supermarkt.Id);
                        Player.SetData("KaufenPreis", supermarkt.SupermarktKaufpreis);

                        //Freeze
                        Freeze(Player);

                        Player.TriggerEvent("Kaufen", 3, GeldFormatieren(supermarkt.SupermarktKaufpreis));
                        return;
                    }
                }

                AutohausLokal autohaus = new AutohausLokal();
                autohaus = NaheAutohausBekommen(Player);

                if (autohaus != null)
                {
                    if (autohaus.AutohausBesitzer == 0)
                    {
                        if (HatBiz(Player) == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits ein Business."); return; }
                        if (Player.GetData("KaufenTyp") == 4) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Schließe erst das aktuelle Fenster."); return; }
                        Player.SetData("KaufenTyp", 4);
                        Player.SetData("KaufenId", autohaus.Id);
                        Player.SetData("KaufenPreis", autohaus.AutohausKaufpreis);

                        //Freeze
                        Freeze(Player);

                        Player.TriggerEvent("Kaufen", 4, GeldFormatieren(autohaus.AutohausKaufpreis));
                        return;
                    }
                }
            }
        }

        [RemoteEvent("ScoreboardZeigen")]
        public static void ScoreboardZeigen(Client Player)
        {
            //Wichtige Abfragen
            if (Player.GetData("Eingeloggt") == 0) { return; }
            if (Player.GetData("Scoreboard") == 1) { return; }

            //Cooldown
            if (Player.GetData("KeyCoolDown") == 1) { return; }
            Player.SetData("KeyCoolDown", 1);
            Timer.SetTimer(() => KeyCoolDown(Player), GlobaleSachen.ScoreboardCoolDownZeit, 1);

            Player.TriggerEvent("Scoreboardoeffnen");
            Player.SetData("Scoreboard", 1);
            foreach (var Spieler in NAPI.Pools.GetAllPlayers())
            {
                if (Spieler.GetData("Eingeloggt") == 1)
                {
                    DateTime Datum1 = AccountEinreiseDatumBekommen(Spieler);
                    String EinreiseDatum = Datum1.ToString("dd.MM.yyyy");
                    String Spielzeit = AccountSpielzeitBerechnen(Spieler);
                    String Level = AdminLevelName(AccountAdminLevelBekommen(Spieler));
                    int Ping = NAPI.Player.GetPlayerPing(Spieler);
                    Player.TriggerEvent("Scoreboard_Eintragen", Spieler.Name, Level, Spielzeit, Ping);
                }
            }

            //Spieleranzahl zeigen
            Player.TriggerEvent("ScoreboardSpielerzahl");
        }

        [RemoteEvent("ScoreboardHiden")]
        public static void ScoreboardHiden(Client Player)
        {
            //Wichtige Abfragen
            if(Player.GetData("Scoreboard") == 0) { return; }
            if (Player.GetData("Eingeloggt") == 0) { return; }

            //Cooldown
            if (Player.GetData("KeyCoolDown") == 1) { return; }
            Player.SetData("KeyCoolDown", 1);
            Timer.SetTimer(() => KeyCoolDown(Player), GlobaleSachen.ScoreboardCoolDownZeit, 1);

            Player.TriggerEvent("Scoreboardschliessen");
            Player.SetData("Scoreboard", 0);
            Player.SetData("ScoreboardScroll", 0);
        }

        [RemoteEvent("ScoreboardDown")]
        public static void ScoreboardDown(Client Player)
        {
            //Wichtige Abfragen
            if (Player.GetData("Scoreboard") == 0) { return; }
            if (Player.GetData("Eingeloggt") == 0) { return; }

            Player.TriggerEvent("ScoreboardScrollDown");
        }

        [RemoteEvent("ScoreboardUp")]
        public static void ScoreboardUp(Client Player)
        {
            //Wichtige Abfragen
            if (Player.GetData("Scoreboard") == 0) { return; }
            if (Player.GetData("Eingeloggt") == 0) { return; }

            Player.TriggerEvent("ScoreboardScrollUp");
        }

        //Wichtige INFORMATION ZU DEN CHECK METHODEN
        //Sobald irgendwas zutrifft, immer return nutzten
        //Sonst würde es auf Dauer zu viele Ressourcen verbrauchen!
        [RemoteEvent("ECheck")]
        public static void ECheck(Client Player)
        {
            //Cooldown
            if (Player.GetData("KeyCoolDown") == 1) { return; }
            Player.SetData("KeyCoolDown", 1);
            Timer.SetTimer(() => KeyCoolDown(Player), GlobaleSachen.KeyCoolDownZeit, 1);

            //Wichtige Abfragen
            if (Player.GetData("Eingeloggt") == 0) { return; }

            //Sieht er einen Perso oder was anderes? Falls ja schließen
            if (Player.GetData("SiehtPerso") == 1) { Player.TriggerEvent("persoschliessen"); return; }
           
            //Alles was mit Fahrzeugen zu tun hat
            if (Player.IsInVehicle)
            {
                if (NAPI.Player.GetPlayerVehicleSeat(Player) == -1)
                {
                    //Berufskraftfahrer Kraftstoff
                    if (Player.Position.DistanceTo(new Vector3(815.2087, -1590.846, 31.01333)) < 5.0f)
                    {
                        if (AccountJobBekommen(Player) != 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du bist kein Berufskraftfahrer!"); return; }
                        AutoLokal auto = new AutoLokal();
                        auto = AutoBekommen(Player.Vehicle);

                        if (auto.FahrzeugJob == 1)
                        {
                            if (Player.GetData("BerufskraftfahrerKraftstoffTyp") == 1 && Player.GetData("BerufskraftfahrerDieselTanke") == 0)
                            {
                                JobBerufskraftfahrerDiesel(Player);
                                return;
                            }
                            else if (Player.GetData("BerufskraftfahrerKraftstoffTyp") == 2 && Player.GetData("BerufskraftfahrerE10Tanke") == 0)
                            {
                                JobBerufskraftfahrerE10(Player);
                                return;
                            }
                            else if (Player.GetData("BerufskraftfahrerKraftstoffTyp") == 3 && Player.GetData("BerufskraftfahrerSuperTanke") == 0)
                            {
                                JobBerufskraftfahrerSuper(Player);
                                return;
                            }
                        }
                    }
                    //Berufskraftfahrer Holz beladen
                    else if (Player.Position.DistanceTo(new Vector3(-511.5431, 5241.104, 80.30409)) < 5.0f)
                    {
                        if (AccountJobBekommen(Player) != 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du bist kein Berufskraftfahrer!"); return; }
                        AutoLokal auto = new AutoLokal();
                        auto = AutoBekommen(Player.Vehicle);

                        if (auto.FahrzeugJob == 1)
                        {
                            if (Player.GetData("BerufskraftfahrerHolz") == 1 && Player.GetData("BerufskraftfahrerHolzGeladen") == 0)
                            {
                                JobBerufskraftfahrerHolzLaden(Player);
                                return;
                            }
                        }
                    }
                    //Job Berufskraftfahrer Holz entladen
                    else if (Player.Position.DistanceTo(new Vector3(1307.297, 4324.441, 38.20026)) < 5.0f)
                    {
                        if (Player.GetData("BerufskraftfahrerHolz") == 1 && Player.GetData("BerufskraftfahrerHolzGeladen") == 1)
                        {
                            JobBerufskraftFahrerHolzEntladen(Player);
                        }
                    }
                    else if(Player.GetData("BerufskraftfahrerDieselTanke") != 0)
                    {
                        JobBerufskraftFahrerDieselTankeCheck(Player);
                    }
                    else if (Player.GetData("BerufskraftfahrerE10Tanke") != 0)
                    {
                        JobBerufskraftFahrerE10TankeCheck(Player);
                    }
                    else if (Player.GetData("BerufskraftfahrerSuperTanke") != 0)
                    {
                        JobBerufskraftFahrerSuperTankeCheck(Player);
                    }
                    //Busfahrer Routen Check
                    else if (Player.GetData("BusfahrerJobAngenommen") != 0 && Player.GetData("BusfahrerRoute") != 0)
                    {
                        if(AccountJobFahrzeugBekommen(Player) == Player.Vehicle)
                        {
                            if (AccountJobBekommen(Player) != 2) { return; }
                            if (Player.GetData("BewegtSichMitFahrzeug") == 1) { return; }
                            JobBusfahrerRoutenCheck(Player);
                            return;
                        }
                    }
                    //Busfahrer wenn er nochmal fahren will
                    else if (Player.Position.DistanceTo(new Vector3(403.169, -642.016, 28.5002)) < 5.0f && Player.GetData("BusfahrerJobAngenommen") == 0 && Player.GetData("BusfahrerRoute") == 0 && Player.GetData("BusfahrerFahrzeug") == 1)
                    {
                        if (AccountJobBekommen(Player) != 2) { return; }

                        if (AccountJobFahrzeugBekommen(Player) == Player.Vehicle)
                        {
                            Player.TriggerEvent("busfahrerbrowseroeffnen");
                            return;
                        }
                    }
                }
            }
            //Alles ohne Fahrzeuge
            else
            {
                //Wenn der Spieler im Tutorial ist darf er nirgendwo anders interagieren
                //Alles was mit dem Tutorial Interaktionen zu tun hat
                if (AccountTutorialBekommen(Player) != 100)
                {
                    Tutorial(Player);

                    //Hier der Stop der Codeausführung
                    return;
                }


                //Stadthalle
                if (Player.Position.DistanceTo(new Vector3(331.347, -1569.97, 30.3076)) < 3.0f)
                {
                    Player.TriggerEvent("Stadthalle");
                    return;
                }
                //Arbeitsamt
                else if (Player.Position.DistanceTo(new Vector3(246.22, -1603.7, 25.5601)) < 3.0f)
                {
                    Player.TriggerEvent("Arbeitsamt");
                    return;
                }
                //Berufskraftfahrer
                else if (Player.Position.DistanceTo(new Vector3(-1546.57, 1367.763, 126.1016)) < 5.0f)
                {
                    if (AccountJobBekommen(Player) != 1) { return; }
                    JobBerufskraftfahrerFahrzeugSpawnen(Player);
                    return;
                }
                //Busfahrer
                else if (Player.Position.DistanceTo(new Vector3(403.169, -642.016, 28.5002)) < 5.0f)
                {
                    if (AccountJobBekommen(Player) != 2) { return; }
                    JobBusfahrerFahrzeugSpawnen(Player);
                    return;
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
                    if (Player.GetData("AmTanken") == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du bist bereits am tanken."); return; }

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
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast nicht genug Geld.");
                        }
                        else
                        {
                            //Der Tankstelle das Geld geben
                            tanke.TankstelleGeld += Player.GetData("TankRechnung");

                            //Nachricht an den Spieler
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast deine Rechnung bezahlt.");
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Eine angenehme Weiterfahrt.");

                            //Dem Spieler das Geld abziehen
                            Funktionen.AccountGeldSetzen(Player, 2, Player.GetData("TankRechnung"));

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

        public static void JobBerufskraftFahrerHolzEntladen(Client Player)
        {
            if(Player.IsInVehicle == false) { return; }
            NAPI.Notification.SendNotificationToPlayer(Player, "Debug1");
            if(Player.GetData("BerufskraftfahrerAmAbladen") == 1) { return; }
            NAPI.Notification.SendNotificationToPlayer(Player, "Debug2");
            if (Player.GetData("BerufskraftfahrerHolzGeladen") == 0) { return; }
            NAPI.Notification.SendNotificationToPlayer(Player, "Debug3");
            if (Player.Vehicle != AccountJobFahrzeugBekommen(Player)) { return; }

            if(Player.GetData("BerufskraftfahrerJobAngenommen") == 0)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst erst einen Job annehmen!");
                return;
            }

            Player.SetData("BerufskraftfahrerAmAbladen", 1);
            Freeze(Player);
            Player.Vehicle.FreezePosition = true;
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dein LKW wird entladen...");
            Timer.SetTimer(() => JobBerufskraftFahrerHolzEntladenBeenden(Player), 20000, 1);
            Ladebalken(Player, 3, 20000);
            Player.Vehicle.EngineStatus = false;
        }

        public static void JobBerufskraftFahrerHolzEntladenBeenden(Client Player)
        {
            if (NAPI.Player.IsPlayerConnected(Player) == false) { return; }
            if (AccountJobBekommen(Player) != 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du bist kein Berufskraftfahrer!"); return; }
            AutoLokal auto = new AutoLokal();
            auto = AutoBekommen(Player.Vehicle);

            if (auto.FahrzeugJob == 1)
            {
                if (Player.GetData("BerufskraftfahrerHolzGeladen") == 1)
                {
                    int Volumen = AccountBerufskraftfahrerVolumenBerechnen(Player);
                    AccountBerufskraftfahrerExpSetzen(Player, 1, 40);
                    AccountGeldSetzen(Player, 1, Volumen * GlobaleSachen.Berufskraftfahrer_Holz_KiloBonus);
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast das Holz erfolgreich abgeladen.");
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du kannst nun neues Holz holen.");
                    var Ladepunkt = new Vector3(-511.5431, 5241.104, 0);
                    Player.TriggerEvent("Navigation", Ladepunkt.X, Ladepunkt.Y);
                    Unfreeze(Player);
                    Player.Vehicle.FreezePosition = false;
                    Player.Vehicle.EngineStatus = true;
                    Player.SetData("BerufskraftfahrerHolzGeladen", 0);
                    Player.SetData("BerufskraftfahrerAmAbladen", 0);
                    LogEintrag(Player, "Holz entladen");
                    return;
                }
            }
        }

        public static void JobBerufskraftFahrerDieselTankeCheck(Client Player)
        {
            if (Player.IsInVehicle == false) { return; }
            if (Player.GetData("BerufskraftfahrerAmAbladen") == 1) { return; }
            if (Player.Vehicle != AccountJobFahrzeugBekommen(Player)) { return; }

            AutoLokal auto = new AutoLokal();
            auto = AutoBekommen(Player.Vehicle);

            if (auto.FahrzeugJob == 1)
            {
                foreach (TankstelleLokal tanke in Funktionen.TankenListe)
                {
                    if (tanke.Id == Player.GetData("BerufskraftfahrerDieselTanke"))
                    {
                        if (Player.Position.DistanceTo(new Vector3(tanke.TankstelleX, tanke.TankstelleY, tanke.TankstelleZ)) < 8.0f)
                        {
                            Player.SetData("BerufskraftfahrerAmAbladen", 1);
                            FreezeAuto(Player);
                            Ladebalken(Player, 3, 20000);
                            Timer.SetTimer(() => JobBerufskraftFahrerDieselTankeCheck1(Player), 20000, 1);
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dein LKW wird entladen...");
                            return;
                        }
                    }
                }
            }
        }

        public static void JobBerufskraftFahrerDieselTankeCheck1(Client Player)
        {
            //Server crash prevention
            if (NAPI.Player.IsPlayerConnected(Player) == false) { return; }
            if (Player.IsInVehicle == false) { Unfreeze(Player); return; }

            UnfreezeAuto(Player);

            AutoLokal auto = new AutoLokal();
            auto = AutoBekommen(Player.Vehicle);

            if (auto.FahrzeugJob == 1)
            {
                foreach (TankstelleLokal tanke in Funktionen.TankenListe)
                {
                    if (tanke.Id == Player.GetData("BerufskraftfahrerDieselTanke"))
                    {
                        if (Player.Position.DistanceTo(new Vector3(tanke.TankstelleX, tanke.TankstelleY, tanke.TankstelleZ)) < 8.0f)
                        {
                            int Volumen = 0;
                            Volumen = AccountBerufskraftfahrerVolumenBerechnen(Player);

                            if (tanke.TankstelleDiesel + Volumen > 2000)
                            {
                                AccountBerufskraftfahrerExpSetzen(Player, 1, 20);
                                tanke.TankstelleDiesel = 2000;
                                tanke.TankstelleGeändert = true;
                                AccountGeldSetzen(Player, 1, Player.GetData("BerufskraftfahrerVerdienst"));
                                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Tankstelle erfolgreich mit Diesel beliefert.");
                            }
                            else
                            {
                                tanke.TankstelleDiesel += Volumen;
                                tanke.TankstelleGeändert = true;
                                AccountGeldSetzen(Player, 1, Player.GetData("BerufskraftfahrerVerdienst"));
                                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Tankstelle erfolgreich mit Diesel beliefert.");
                            }

                            Player.SetData("BerufskraftfahrerAmAbladen", 0);
                            Player.SetData("BerufskraftfahrerAmAbladen", 0);
                            Player.SetData("BerufskraftfahrerDieselTanke", 0);
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du kannst nun eine neue Tankstelle beliefern.");
                            var Ladepunkt = new Vector3(815.2087, -1590.846, 0);
                            Player.TriggerEvent("Navigation", Ladepunkt.X, Ladepunkt.Y);
                            LogEintrag(Player, "Tankstelle mit Diesel beliefert");
                            return;
                        }
                    }
                }
            }
        }

        public static void JobBerufskraftFahrerE10TankeCheck(Client Player)
        {
            if (Player.IsInVehicle == false) { return; }
            if (Player.GetData("BerufskraftfahrerAmAbladen") == 1) { return; }
            if (Player.Vehicle != AccountJobFahrzeugBekommen(Player)) { return; }

            AutoLokal auto = new AutoLokal();
            auto = AutoBekommen(Player.Vehicle);

            if (auto.FahrzeugJob == 1)
            {
                foreach (TankstelleLokal tanke in Funktionen.TankenListe)
                {
                    if (tanke.Id == Player.GetData("BerufskraftfahrerE10Tanke"))
                    {
                        if (Player.Position.DistanceTo(new Vector3(tanke.TankstelleX, tanke.TankstelleY, tanke.TankstelleZ)) < 8.0f)
                        {
                            Player.SetData("BerufskraftfahrerAmAbladen", 1);

                            FreezeAuto(Player);
                            Ladebalken(Player, 3, 20000);
                            Timer.SetTimer(() => JobBerufskraftFahrerE10TankeCheck1(Player), 20000, 1);
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dein LKW wird entladen...");
                            return;
                        }
                    }
                }
            }
        }

        public static void JobBerufskraftFahrerE10TankeCheck1(Client Player)
        {
            //Server crash prevention
            if (NAPI.Player.IsPlayerConnected(Player) == false) { return; }
            if (Player.IsInVehicle == false) { Unfreeze(Player); return; }

            UnfreezeAuto(Player);

            AutoLokal auto = new AutoLokal();
            auto = AutoBekommen(Player.Vehicle);

            if (auto.FahrzeugJob == 1)
            {
                foreach (TankstelleLokal tanke in Funktionen.TankenListe)
                {
                    if (tanke.Id == Player.GetData("BerufskraftfahrerE10Tanke"))
                    {
                        if (Player.Position.DistanceTo(new Vector3(tanke.TankstelleX, tanke.TankstelleY, tanke.TankstelleZ)) < 8.0f)
                        {
                            int Volumen = 0;
                            Volumen = AccountBerufskraftfahrerVolumenBerechnen(Player);

                            if (tanke.TankstelleE10 + Volumen > 2000)
                            {
                                tanke.TankstelleE10 = 2000;
                                tanke.TankstelleGeändert = true;
                                AccountGeldSetzen(Player, 1, Player.GetData("BerufskraftfahrerVerdienst"));
                                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Tankstelle erfolgreich mit E10 beliefert.");
                            }
                            else
                            {
                                tanke.TankstelleE10 += Volumen;
                                tanke.TankstelleGeändert = true;
                                AccountGeldSetzen(Player, 1, Player.GetData("BerufskraftfahrerVerdienst"));
                                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Tankstelle erfolgreich mit E10 beliefert.");
                            }

                            Player.SetData("BerufskraftfahrerAmAbladen", 0);
                            Player.SetData("BerufskraftfahrerE10Tanke", 0);
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du kannst nun eine neue Tankstelle beliefern.");
                            var Ladepunkt = new Vector3(815.2087, -1590.846, 0);
                            Player.TriggerEvent("Navigation", Ladepunkt.X, Ladepunkt.Y);
                            LogEintrag(Player, "Tankstelle mit E10 beliefert");
                            return;
                        }
                    }
                }
            }
        }

        public static void JobBerufskraftFahrerSuperTankeCheck(Client Player)
        {
            if (Player.IsInVehicle == false) { return; }
            if (Player.GetData("BerufskraftfahrerAmAbladen") == 1) { return; }
            if (Player.Vehicle != AccountJobFahrzeugBekommen(Player)) { return; }

            AutoLokal auto = new AutoLokal();
            auto = AutoBekommen(Player.Vehicle);

            if (auto.FahrzeugJob == 1)
            {
                foreach (TankstelleLokal tanke in Funktionen.TankenListe)
                {
                    if (tanke.Id == Player.GetData("BerufskraftfahrerSuperTanke"))
                    {
                        if (Player.Position.DistanceTo(new Vector3(tanke.TankstelleX, tanke.TankstelleY, tanke.TankstelleZ)) < 8.0f)
                        {
                            FreezeAuto(Player);
                            Ladebalken(Player, 3, 20000);
                            Timer.SetTimer(() => JobBerufskraftFahrerSuperTankeCheck1(Player), 20000, 1);
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dein LKW wird entladen...");
                            return;
                        }
                    }
                }
            }
        }

        public static void JobBerufskraftFahrerSuperTankeCheck1(Client Player)
        {
            //Server crash prevention
            if (NAPI.Player.IsPlayerConnected(Player) == false) { return; }
            if (Player.IsInVehicle == false) { Unfreeze(Player); return; }

            UnfreezeAuto(Player);

            AutoLokal auto = new AutoLokal();
            auto = AutoBekommen(Player.Vehicle);

            if (auto.FahrzeugJob == 1)
            {
                foreach (TankstelleLokal tanke in Funktionen.TankenListe)
                {
                    if (tanke.Id == Player.GetData("BerufskraftfahrerSuperTanke"))
                    {
                        if (Player.Position.DistanceTo(new Vector3(tanke.TankstelleX, tanke.TankstelleY, tanke.TankstelleZ)) < 8.0f)
                        {
                            int Volumen = 0;
                            Volumen = AccountBerufskraftfahrerVolumenBerechnen(Player);

                            if (tanke.TankstelleSuper + Volumen > 2000)
                            {
                                tanke.TankstelleSuper = 2000;
                                tanke.TankstelleGeändert = true;
                                AccountGeldSetzen(Player, 1, Player.GetData("BerufskraftfahrerVerdienst"));
                                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Tankstelle erfolgreich mit Super beliefert.");
                            }
                            else
                            {
                                tanke.TankstelleSuper += Volumen;
                                tanke.TankstelleGeändert = true;
                                AccountGeldSetzen(Player, 1, Player.GetData("BerufskraftfahrerVerdienst"));
                                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Tankstelle erfolgreich mit Super beliefert.");
                            }

                            Player.SetData("BerufskraftfahrerSuperTanke", 0);
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du kannst nun eine neue Tankstelle beliefern.");
                            var Ladepunkt = new Vector3(815.2087, -1590.846, 0);
                            Player.TriggerEvent("Navigation", Ladepunkt.X, Ladepunkt.Y);
                            LogEintrag(Player, "Tankstelle mit Super beliefert");
                            return;
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

                //Job Kram
                tanke.TankstelleJobSpieler = 0;

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

        public static List<AutohausLokal> AlleAutohäuserLadenDB()
        {
            List<AutohausLokal> AutohausListe = new List<AutohausLokal>();
            foreach (var Autohaus in ContextFactory.Instance.srp_autohäuser.Where(x => x.Id > 0).ToList())
            {
                AutohausLokal autohaus = new AutohausLokal();

                autohaus.Id = Autohaus.Id;
                autohaus.AutohausBeschreibung = Autohaus.AutohausBeschreibung;
                autohaus.AutohausBesitzer = Autohaus.AutohausBesitzer;
                autohaus.AutohausGeld = Autohaus.AutohausGeld;
                autohaus.AutohausKaufpreis = Autohaus.AutohausKaufpreis;
                autohaus.AutohausX = Autohaus.AutohausX;
                autohaus.AutohausY = Autohaus.AutohausY;
                autohaus.AutohausZ = Autohaus.AutohausZ;
                autohaus.AutohausGeändert = false;

                AutohausListe.Add(autohaus);
            }
            return AutohausListe;
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
            //Nur die Liste muss erzeugt werden
            List<AccountLokal> AccountListe = new List<AccountLokal>();

            return AccountListe;
        }

        public static List<BotLokal> AlleBotsLadenDB()
        {
            List<BotLokal> BotListe = new List<BotLokal>();

            foreach (var Bot in ContextFactory.Instance.srp_bots.Where(x => x.Id > 0).ToList())
            {
                BotLokal bot = new BotLokal();

                //if (Bot.BotDimension == 0)
                //{
                //    bot.Bot = NAPI.Ped.CreatePed(NAPI.Util.PedNameToModel(Bot.BotName), new Vector3(Bot.BotX, Bot.BotY, Bot.BotZ), Bot.BotKopf, 0);
                //}
                //else
                //{
                //    bot.Bot = NAPI.Ped.CreatePed(NAPI.Util.PedNameToModel(Bot.BotName), new Vector3(Bot.BotX, Bot.BotY, Bot.BotZ), Bot.BotKopf, Bot.BotDimension);
                //}

                bot.BotName = Bot.BotName;
                bot.BotBeschreibung = Bot.BotBeschreibung;
                bot.BotX = Bot.BotX;
                bot.BotY = Bot.BotY;
                bot.BotZ = Bot.BotZ;
                bot.BotKopf = Bot.BotKopf;
                bot.BotDimension = Bot.BotDimension;

                bot.BotGeändert = false;

                //Zur lokalen Liste hinzufügen
                BotListe.Add(bot);
            }
            return BotListe;
        }

        public static List<BankautomatenLokal> AlleBankautomatenLadenDB()
        {
            List<BankautomatenLokal> ATMListe = new List<BankautomatenLokal>();

            foreach (var Atm in ContextFactory.Instance.srp_bankautomaten.Where(x => x.Id > 0).ToList())
            {
                BankautomatenLokal atm = new BankautomatenLokal();

                atm.Id = Atm.Id;
                atm.PositionX = Atm.PositionX;
                atm.PositionY = Atm.PositionY;
                atm.PositionZ = Atm.PositionZ;

                //Zur lokalen Liste hinzufügen
                ATMListe.Add(atm);
            }
            return ATMListe;
        }

        public static List<RandomSpawns> AlleRandomSpawnsLadenDB()
        {
            List<RandomSpawns> RandomSpawnListe = new List<RandomSpawns>();

            foreach (var Spawn in ContextFactory.Instance.srp_randomspawns.Where(x => x.Id > 0).ToList())
            {
                RandomSpawns spawn = new RandomSpawns();

                spawn.Id = Spawn.Id;
                spawn.Name = Spawn.Name;
                spawn.PosX = Spawn.PosX;
                spawn.PosY = Spawn.PosY;
                spawn.PosZ = Spawn.PosZ;
                spawn.RotZ = Spawn.RotZ;

                //Zur lokalen Liste hinzufügen
                RandomSpawnListe.Add(spawn);
            }
            return RandomSpawnListe;
        }

        public static List<FahrzeugMods> AlleFahrzeugModsLadenDB()
        {
            List<FahrzeugMods> FModsListe = new List<FahrzeugMods>();

            foreach (var Fmod in ContextFactory.Instance.srp_fahrzeugmods.Where(x => x.Id > 0).ToList())
            {
                FahrzeugMods fmod = new FahrzeugMods();

                fmod.Id = Fmod.Id;
                fmod.Name = Fmod.Name;


                //Zur lokalen Liste hinzufügen
                FModsListe.Add(fmod);
            }

            return FModsListe;
        }

        public static List<FahrzeugvermietungenLokal> AlleFahrzeugvermiertungenLadenDB()
        {
            List<FahrzeugvermietungenLokal> FVermiertungListe = new List<FahrzeugvermietungenLokal>();

            foreach (var Fverm in ContextFactory.Instance.srp_fahrzeugvermietungen.Where(x => x.Id > 0).ToList())
            {
                FahrzeugvermietungenLokal fverm = new FahrzeugvermietungenLokal();

                fverm.Id = Fverm.Id;
                fverm.PositionX = Fverm.PositionX;
                fverm.PositionY = Fverm.PositionY;
                fverm.PositionZ = Fverm.PositionZ;

                //Zur lokalen Liste hinzufügen
                FVermiertungListe.Add(fverm);
            }
            return FVermiertungListe;
        }

        public static List<SaveLokal> AlleSavesLadenDB()
        {
            List<SaveLokal> SaveListe = new List<SaveLokal>();

            foreach (var Save in ContextFactory.Instance.srp_saves.Where(x => x.Id > 0).ToList())
            {
                SaveLokal save = new SaveLokal();

                save.Id = Save.Id;
                save.Von = Save.Von;
                save.Beschreibung = Save.Beschreibung;
                save.PositionX = Save.PositionX;
                save.PositionY = Save.PositionY;
                save.PositionZ = Save.PositionZ;
                save.PositionRot = Save.PositionRot;

                //Zur lokalen Liste hinzufügen
                SaveListe.Add(save);
            }
            return SaveListe;
        }

        public static List<GruppenLokal> AlleGruppenLadenDB()
        {
            List<GruppenLokal> GruppenListe = new List<GruppenLokal>();

            foreach (var Gruppe in ContextFactory.Instance.srp_gruppen.Where(x => x.Id > 0).ToList())
            {
                GruppenLokal gruppe = new GruppenLokal();

                gruppe.Id = Gruppe.Id;
                gruppe.GruppenName = Gruppe.GruppenName;
                gruppe.GruppenBesitzer = Gruppe.GruppenBesitzer;
                gruppe.GruppenTag = Gruppe.GruppenTag;
                gruppe.GruppenFarbe = Gruppe.GruppenFarbe;
                gruppe.GruppenGeld = Gruppe.GruppenGeld;
                gruppe.GruppenRang1Name = Gruppe.GruppenRang1Name;
                gruppe.GruppenRang2Name = Gruppe.GruppenRang2Name;
                gruppe.GruppenRang3Name = Gruppe.GruppenRang3Name;
                gruppe.GruppenRang4Name = Gruppe.GruppenRang4Name;
                gruppe.GruppenRang5Name = Gruppe.GruppenRang5Name;

                gruppe.GruppeGeändert = false;

                //Zur lokalen Liste hinzufügen
                GruppenListe.Add(gruppe);
            }
            return GruppenListe;
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

        public static Client SpielerSuchen(String Sender)
        {
            //Benötigte Definitionen
            Client Spieler = null;
            String TempName = null;
            String TempSender = null;
            foreach (var Typ in NAPI.Pools.GetAllPlayers())
            {
                TempName = Typ.Name;
                TempName = TempName.ToLower();
                TempSender = Sender;
                TempSender = TempSender.ToLower();

                if (TempName.Contains(TempSender))
                {
                    Spieler = Typ;
                    break;
                }
            }
            return Spieler;
        }

        [RemoteEvent("KaufenAbbrechen")]
        public static void KaufenAbbrechen(Client Player)
        {
            if(Player.GetData("KaufenTyp") == 5 || Player.GetData("KaufenTyp") == 6)
            {
                Player.TriggerEvent("FahrzeugVerlassen");
            }

            Player.SetData("KaufenTyp", 0);
            Player.SetData("KaufenId", 0);
            Player.SetData("KaufenPreis", 0);

            //Unfreeze
            Unfreeze(Player);
        }

        [RemoteEvent("Kaufen")]
        public static void Kaufen(Client Player)
        {
            if(Player.GetData("KaufenTyp") == 1)
            {
                //Nochmal die Tankstelle ermitteln
                TankstelleLokal tanke = new TankstelleLokal();
                tanke = NaheTankeBekommen(Player);

                //Schauen ob er noch an der Tanke ist
                if (tanke != null && tanke.Id == Player.GetData("KaufenId"))
                {
                    //Schauen ob die Tankstelle immer noch keinen Besitzer hat
                    if (tanke.TankstelleBesitzer == 0)
                    {
                        if (AccountGeldBekommen(Player) < Player.GetData("KaufenPreis"))
                        {
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast nicht genug Geld.");
                            Player.TriggerEvent("kaufenabbrechen");
                        }
                        else
                        {
                            tanke.TankstelleBesitzer = Player.GetData("Id");
                            tanke.TankstelleBeschreibung = Player.Name + "`s Tankstelle";

                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Tankstelle gehört jetzt dir.");
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Diese wird sich gleich automatisch aktualisieren.");
                            AccountGeldSetzen(Player, 2, Player.GetData("KaufenPreis"));
                            Player.TriggerEvent("kaufenabbrechen");

                            tanke.TankstelleGeändert = true;
                        }
                    }
                }
            }
            else if (Player.GetData("KaufenTyp") == 2)
            {
                //Nochmal die Immobilie ermitteln
                ImmobilienLokal haus = new ImmobilienLokal();
                haus = NaheImmobilieBekommen(Player);

                //Schauen ob er noch an der Immobilie ist
                if (haus != null && haus.Id == Player.GetData("KaufenId"))
                {
                    //Schauen ob die Immobilie immer noch keinen Besitzer hat
                    if (haus.ImmobilienBesitzer == 0)
                    {
                        if (AccountGeldBekommen(Player) < Player.GetData("KaufenPreis"))
                        {
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast nicht genug Geld.");
                            Player.TriggerEvent("kaufenabbrechen");
                        }
                        else
                        {
                            haus.ImmobilienBesitzer = Player.GetData("Id");
                            haus.ImmobilienBeschreibung = Player.Name + "`s Immobilie";

                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Immobilie gehört jetzt dir.");
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Diese wird sich gleich automatisch aktualisieren.");
                            AccountGeldSetzen(Player, 2, Player.GetData("KaufenPreis"));
                            Player.TriggerEvent("kaufenabbrechen");

                            haus.ImmobilieGeändert = true;
                        }
                    }
                }
            }
            else if (Player.GetData("KaufenTyp") == 3)
            {
                //Nochmal den Supermarkt ermitteln
                SupermarktLokal supermarkt = new SupermarktLokal();
                supermarkt = NaheSupermarktBekommen(Player);

                //Schauen ob er noch an dem Supermarkt ist
                if (supermarkt != null && supermarkt.Id == Player.GetData("KaufenId"))
                {
                    //Schauen ob der Supermarkt immer noch keinen Besitzer hat
                    if (supermarkt.SupermarktBesitzer == 0)
                    {
                        if (AccountGeldBekommen(Player) < Player.GetData("KaufenPreis"))
                        {
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast nicht genug Geld.");
                            Player.TriggerEvent("kaufenabbrechen");
                        }
                        else
                        {
                            supermarkt.SupermarktBesitzer = Player.GetData("Id");
                            supermarkt.SupermarktBeschreibung = Player.Name + "`s 24/7";

                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der 24/7 gehört jetzt dir.");
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieser wird sich gleich automatisch aktualisieren.");
                            Funktionen.AccountGeldSetzen(Player, 2, Player.GetData("KaufenPreis"));
                            Player.TriggerEvent("kaufenabbrechen");

                            supermarkt.SupermarktGeändert = true;
                        }
                    }
                }
            }
            else if (Player.GetData("KaufenTyp") == 4)
            {
                //Nochmal das Autohaus ermitteln
                AutohausLokal autohaus = new AutohausLokal();
                autohaus = NaheAutohausBekommen(Player);

                //Schauen ob er noch an dem Autohaus ist
                if (autohaus != null && autohaus.Id == Player.GetData("KaufenId"))
                {
                    //Schauen ob das Autohaus immer noch keinen Besitzer hat
                    if (autohaus.AutohausBesitzer == 0)
                    {
                        if (AccountGeldBekommen(Player) < Player.GetData("KaufenPreis"))
                        {
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast nicht genug Geld.");
                            Player.TriggerEvent("kaufenabbrechen");
                        }
                        else
                        {
                            autohaus.AutohausBesitzer = Player.GetData("Id");

                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Autohaus gehört jetzt dir.");
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieses wird sich gleich automatisch aktualisieren.");
                            AccountGeldSetzen(Player, 2, Player.GetData("KaufenPreis"));
                            Player.TriggerEvent("kaufenabbrechen");

                            autohaus.AutohausGeändert = true;
                        }
                    }
                }
            }
            else if (Player.GetData("KaufenTyp") == 5)
            {
                //Schauen ob er noch an dem Autohaus ist
                if (Fahrzeuge.IdBekommen(Player.Vehicle) == Player.GetData("KaufenId") && Fahrzeuge.TypBekommen(Player.Vehicle) == 5)
                {
                    //Schauen ob das Autohaus immer noch keinen Besitzer hat
                    if (AccountGeldBekommen(Player) < Player.GetData("KaufenPreis"))
                    {
                        NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast nicht genug Geld.");
                        Player.TriggerEvent("kaufenabbrechen");
                    }
                    else
                    {
                        Fahrzeuge.FahrzeugKaufen(Player, Player.Vehicle);

                        NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Fahrzeug gehört jetzt dir.");
                        AccountGeldSetzen(Player, 2, Player.GetData("KaufenPreis"));
                        Player.TriggerEvent("kaufenabbrechen");
                    }
                }
            }
            else if (Player.GetData("KaufenTyp") == 6)
            {
                //Schauen ob er noch an dem Autohaus ist
                if (Fahrzeuge.IdBekommen(Player.Vehicle) == Player.GetData("KaufenId") && Fahrzeuge.TypBekommen(Player.Vehicle) == 5 && Fahrzeuge.AutoHausBekommen(Player.Vehicle) == -1)
                {
                    //Schauen ob das Autohaus immer noch keinen Besitzer hat
                    if (AccountGeldBekommen(Player) < Player.GetData("KaufenPreis"))
                    {
                        NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast nicht genug Geld.");
                        Player.TriggerEvent("kaufenabbrechen");
                    }
                    else
                    {
                        Fahrzeuge.FahrzeugKaufenAutohaus(Player, Player.Vehicle);

                        NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Das Fahrzeug gehört jetzt deinem Autohaus.");
                        AccountGeldSetzen(Player, 2, Player.GetData("KaufenPreis"));
                        Player.TriggerEvent("kaufenabbrechen");
                    }
                }
            }
        }
        public static void SpielerLaden(Client Player)
        {
            //Daten aus der SQL Datenbank ziehen
            var Account = ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == Player.SocialClubName).FirstOrDefault();

            //Dem Spieler seine ID setzen
            Player.SetData("Id", Account.Id);

            AccountLokal account = new AccountLokal();

            account.Id = Account.Id;
            account.SocialClub = Account.SocialClub;
            account.NickName = Account.NickName;
            account.Passwort = Account.Passwort;
            account.AdminLevel = Account.AdminLevel;
            account.Fraktion = Account.Fraktion;
            account.FraktionRang = Account.FraktionRang;
            account.Job = Account.Job;
            account.Geld = Account.Geld;
            account.BankGeld = Account.BankGeld;
            account.Perso = Account.Perso;
            account.Spielzeit = Account.Spielzeit;
            account.Exp = Account.Exp;
            account.Gruppe = Account.Gruppe;
            account.GruppenRang = Account.GruppenRang;
            account.GeburtsDatum = Account.GeburtsDatum;
            account.EinreiseDatum = Account.EinreiseDatum;
            account.ZuletztOnline = DateTime.Now;
            account.Verheiratet = Account.Verheiratet;
            account.FahrzeugSchlüssel = Account.FahrzeugSchlüssel;
            account.Kündigungszeit = Account.Kündigungszeit;
            account.PositionX = Account.PositionX;
            account.PositionY = Account.PositionY;
            account.PositionZ = Account.PositionZ;
            account.PositionRot = Account.PositionRot;
            account.Dimension = Account.Dimension;
            account.Interior = Account.Interior;
            account.Component1Drawable = Account.Component1Drawable;
            account.Component3Drawable = Account.Component3Drawable;
            account.Component4Drawable = Account.Component4Drawable;
            account.Component6Drawable = Account.Component6Drawable;
            account.Component7Drawable = Account.Component7Drawable;
            account.Component8Drawable = Account.Component8Drawable;
            account.Component11Drawable = Account.Component11Drawable;
            account.BerufskraftfahrerExp = Account.BerufskraftfahrerExp;
            account.Tutorial = Account.Tutorial;

            account.JobFahrzeug = null;
            account.AccountGeändert = true;

            //Kleidung laden
            Player.TriggerEvent("KleidungZuJava", 1, Account.Component1Drawable);
            Player.TriggerEvent("KleidungZuJava", 3, Account.Component3Drawable);
            Player.TriggerEvent("KleidungZuJava", 4, Account.Component4Drawable);
            Player.TriggerEvent("KleidungZuJava", 6, Account.Component6Drawable);
            Player.TriggerEvent("KleidungZuJava", 7, Account.Component7Drawable);
            Player.TriggerEvent("KleidungZuJava", 8, Account.Component8Drawable);
            Player.TriggerEvent("KleidungZuJava", 11, Account.Component11Drawable);

            //Zur Liste adden
            AccountListe.Add(account);

            //Minutentimer für Spieler
            Timer.SetTimer(() => SpielerMinutenTimer(Player), 60000, 1);

            //Dies sind lokale Dinge bitte keine floats etc nutzen, dass buggt per SetData
            Player.SetData("InteriorName", 0);
            Player.SetData("Eingeloggt", 1);
            Player.SetData("BewegtSichMitFahrzeug", 0);
            Player.SetData("SiehtPerso", 0);
            Player.SetData("IBerry", 0);
            Player.SetData("Scoreboard", 0);
            Player.SetData("Freezed", 0);
            Player.SetData("AmTanken", 0);
            Player.SetData("TankenTankstellenId", 0);
            Player.SetData("TankRechnung", 0);
            Player.SetData("KaufenTyp", 0);
            Player.SetData("KaufenId", 0);
            Player.SetData("KaufenPreis", 0);
            Player.SetData("KeyCoolDown", 0);
            Player.SetData("MenuCoolDown", 0);
            Player.SetData("VerwaltungsModus", 0);
            Player.SetData("NachträglicherNickname", 0);
            Player.SetData("HeiratsAntrag", 0);
            Player.SetData("HeiratsId", 0);
            Player.SetData("HeiratenId", 0);
            Player.SetData("HeiratenBrowser", 0);
            Player.SetData("GruppenEinladungId", 0);
            Player.SetData("StadthalleInt", 0);
            Player.SetData("Chat", 0);

            //Job Daten
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

            //Job Daten Busfahrer
            Player.SetData("BusfahrerFahrzeug", 0);
            Player.SetData("BusfahrerJobAngenommen", 0);
            Player.SetData("BusfahrerRoute", 0);
            Player.SetData("BusfahrerRoutePosition", 0);

            //Dialoge
            Player.SetData("FahrzeugPrivatDialog", 0);

            //Tutorial Daten
            Player.SetData("EinreiseNPC", 0);
            Player.SetData("HelmutNPC", 0);

            //Discord
            Player.TriggerEvent("DiscordStatusSetzen", "Strawberry Roleplay","Spielt als " + Player.Name);

            //Spieler Online Status
            ServerSpielerGejoined(1);

            //Nachrichten
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast dich erfolgreich eingeloggt!");
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dein letzter Login war am ~r~" + DatumFormatieren(Account.ZuletztOnline));

            //Peds Laden
            //PedsFürSpielerLaden(Player);
        }

        public static void ServerSpielerGejoined(int Status)
        {
            //Spieleranzahl updaten
            var Server = ContextFactory.Instance.srp_server.Where(x => x.Id == 1).FirstOrDefault();
            if(Status == 1)
            {
                Server.Online += 1;
                if (Server.Online > Server.OnlineRekord)
                {
                    Server.OnlineRekord = Server.Online;
                    Server.OnlineRekordDatum = DateTime.Now;
                }
            }
            else if(Status == 2)
            {
                Server.Online -= 1;
            }

            //Speichern
            ContextFactory.Instance.SaveChanges();
        }

        public static void AlleSpielerSpeichern()
        {
            foreach (var Spieler in NAPI.Pools.GetAllPlayers())
            {
                if(Spieler.GetData("Eingeloggt") == 1)
                {
                    AccountPositionInteriorDimensionUpdaten(Spieler);

                    foreach (AccountLokal account in AccountListe)
                    {
                        if (Spieler.GetData("Id") == account.Id && account.AccountGeändert == true)
                        {
                            var DBAccount = ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == Spieler.SocialClubName).FirstOrDefault();

                            DBAccount.NickName = account.NickName;
                            DBAccount.AdminLevel = account.AdminLevel;
                            DBAccount.Fraktion = account.Fraktion;
                            DBAccount.FraktionRang = account.FraktionRang;
                            DBAccount.Job = account.Job;
                            DBAccount.Geld = account.Geld;
                            DBAccount.BankGeld = account.BankGeld;
                            DBAccount.Perso = account.Perso;
                            DBAccount.Spielzeit = account.Spielzeit;
                            DBAccount.Exp = account.Exp;
                            DBAccount.Gruppe = account.Gruppe;
                            DBAccount.GruppenRang = account.GruppenRang;
                            DBAccount.GeburtsDatum = account.GeburtsDatum;
                            DBAccount.EinreiseDatum = account.EinreiseDatum;
                            DBAccount.ZuletztOnline = account.ZuletztOnline;
                            DBAccount.Verheiratet = account.Verheiratet;
                            DBAccount.FahrzeugSchlüssel = account.FahrzeugSchlüssel;
                            DBAccount.Kündigungszeit = account.Kündigungszeit;
                            DBAccount.PositionX = account.PositionX;
                            DBAccount.PositionY = account.PositionY;
                            DBAccount.PositionZ = account.PositionZ;
                            DBAccount.PositionRot = account.PositionRot;
                            DBAccount.Dimension = account.Dimension;
                            DBAccount.Interior = account.Interior;
                            DBAccount.Component1Drawable = account.Component1Drawable;
                            DBAccount.Component3Drawable = account.Component3Drawable;
                            DBAccount.Component4Drawable = account.Component4Drawable;
                            DBAccount.Component6Drawable = account.Component6Drawable;
                            DBAccount.Component7Drawable = account.Component7Drawable;
                            DBAccount.Component8Drawable = account.Component8Drawable;
                            DBAccount.Component11Drawable = account.Component11Drawable;
                            DBAccount.BerufskraftfahrerExp = account.BerufskraftfahrerExp;
                            DBAccount.Tutorial = account.Tutorial;

                            //Query absenden
                            ContextFactory.Instance.SaveChanges();

                            //Damit er nicht dauerthaft gespeichert wird
                            account.AccountGeändert = false;
                        }
                    }
                }
            }
        }

        public static void SpielerSpeichernDisconnect(Client Player)
        {
            //Daten aus der SQL Datenbank ziehen
            var Account = ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == Player.SocialClubName).FirstOrDefault();

            AccountLokal account = new AccountLokal();
            account = AccountBekommen(Player);

            //Lokale Daten abfragen und in der Datenbank ablegen
            Account.NickName = account.NickName;
            Account.AdminLevel = account.AdminLevel;
            Account.Fraktion = account.Fraktion;
            Account.FraktionRang = account.FraktionRang;
            Account.Job = account.Job;
            Account.Geld = account.Geld;
            Account.BankGeld = account.BankGeld;
            Account.Perso = account.Perso;
            Account.Spielzeit = account.Spielzeit;
            Account.Exp = account.Exp;
            Account.Gruppe = account.Gruppe;
            Account.GeburtsDatum = account.GeburtsDatum;
            Account.EinreiseDatum = account.EinreiseDatum;
            Account.ZuletztOnline = account.ZuletztOnline;
            Account.Verheiratet = account.Verheiratet;
            Account.FahrzeugSchlüssel = account.FahrzeugSchlüssel;
            Account.Kündigungszeit = account.Kündigungszeit;
            Account.PositionX = account.PositionX;
            Account.PositionY = account.PositionY;
            Account.PositionZ = account.PositionZ;
            Account.PositionRot = account.PositionRot;
            Account.Dimension = account.Dimension;
            Account.Interior = account.Interior;
            Account.Component1Drawable = account.Component1Drawable;
            Account.Component3Drawable = account.Component3Drawable;
            Account.Component4Drawable = account.Component4Drawable;
            Account.Component6Drawable = account.Component6Drawable;
            Account.Component7Drawable = account.Component7Drawable;
            Account.Component8Drawable = account.Component8Drawable;
            Account.Component11Drawable = account.Component11Drawable;
            Account.BerufskraftfahrerExp = account.BerufskraftfahrerExp;
            Account.Tutorial = account.Tutorial;

            //Query absenden
            ContextFactory.Instance.SaveChanges();

            //Von der Liste entfernen
            AccountListe.Remove(account);
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
            Account.FraktionRang = account.FraktionRang;
            Account.Job = account.Job;
            Account.Geld = account.Geld;
            Account.BankGeld = account.BankGeld;
            Account.Perso = account.Perso;
            Account.Spielzeit = account.Spielzeit;
            Account.Exp = account.Exp;
            Account.Gruppe = account.Gruppe;
            Account.GruppenRang = account.GruppenRang;
            Account.GeburtsDatum = account.GeburtsDatum;
            Account.EinreiseDatum = account.EinreiseDatum;
            Account.ZuletztOnline = account.ZuletztOnline;
            Account.Verheiratet = account.Verheiratet;
            Account.FahrzeugSchlüssel = account.FahrzeugSchlüssel;
            Account.Kündigungszeit = account.Kündigungszeit;
            Account.PositionX = account.PositionX;
            Account.PositionY = account.PositionY;
            Account.PositionZ = account.PositionZ;
            Account.PositionRot = account.PositionRot;
            Account.Dimension = account.Dimension;
            Account.Interior = account.Interior;
            Account.Component1Drawable = account.Component1Drawable;
            Account.Component3Drawable = account.Component3Drawable;
            Account.Component4Drawable = account.Component4Drawable;
            Account.Component6Drawable = account.Component6Drawable;
            Account.Component7Drawable = account.Component7Drawable;
            Account.Component8Drawable = account.Component8Drawable;
            Account.Component11Drawable = account.Component11Drawable;
            Account.BerufskraftfahrerExp = account.BerufskraftfahrerExp;
            Account.Tutorial = account.Tutorial;

            //Query absenden
            ContextFactory.Instance.SaveChanges();
        }


        [RemoteEvent("JobBusfahrerRoute1")]
        public static void JobBusfahrerRoute1(Client Player)
        {
            Player.TriggerEvent("busfahrerbrowserschliessen");
            if (Player.GetData("BusfahrerJobAngenommen") == 1)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits eine Route angenommen.");
                return;
            }

            //Das er angenommen hat
            Player.SetData("BusfahrerRoute", 1);

            //Das er angenommen hat
            Player.SetData("BusfahrerJobAngenommen", 1);

            //Route setzen
            Player.SetData("BusfahrerRoutePosition", 1);

            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Fahre nun die Route ab.");
            var BusfahrerPunkt = new Vector3(GlobaleSachen.Route1_1.Position.X, GlobaleSachen.Route1_1.Position.Y, 0);
            Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
        }

        [RemoteEvent("JobBusfahrerRoute2")]
        public static void JobBusfahrerRoute2(Client Player)
        {
            Player.TriggerEvent("busfahrerbrowserschliessen");
            if (Player.GetData("BusfahrerJobAngenommen") == 1)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits eine Route angenommen.");
                return;
            }

            //Das er angenommen hat
            Player.SetData("BusfahrerRoute", 2);

            //Das er angenommen hat
            Player.SetData("BusfahrerJobAngenommen", 1);

            //Route setzen
            Player.SetData("BusfahrerRoutePosition", 1);

            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Fahre nun die Route ab.");
            var BusfahrerPunkt = new Vector3(GlobaleSachen.Route1_1.Position.X, GlobaleSachen.Route1_1.Position.Y, 0);
            Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
        }

        [RemoteEvent("JobBusfahrerRoute3")]
        public static void JobBusfahrerRoute3(Client Player)
        {
            Player.TriggerEvent("busfahrerbrowserschliessen");
            if (Player.GetData("BusfahrerJobAngenommen") == 1)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits eine Route angenommen.");
                return;
            }

            //Das er angenommen hat
            Player.SetData("BusfahrerRoute", 3);

            //Das er angenommen hat
            Player.SetData("BusfahrerJobAngenommen", 1);

            //Route setzen
            Player.SetData("BusfahrerRoutePosition", 1);

            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Fahre nun die Route ab.");
            var BusfahrerPunkt = new Vector3(GlobaleSachen.Route3_1.Position.X, GlobaleSachen.Route3_1.Position.Y, 0);
            Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
        }

        [RemoteEvent("JobBusfahrerRoute4")]
        public static void JobBusfahrerRoute4(Client Player)
        {
            Player.TriggerEvent("busfahrerbrowserschliessen");
            if (Player.GetData("BusfahrerJobAngenommen") == 1)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits eine Route angenommen.");
                return;
            }

            //Das er angenommen hat
            Player.SetData("BusfahrerRoute", 4);

            //Das er angenommen hat
            Player.SetData("BusfahrerJobAngenommen", 1);

            //Route setzen
            Player.SetData("BusfahrerRoutePosition", 1);

            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Fahre nun die Route ab.");
            var BusfahrerPunkt = new Vector3(GlobaleSachen.Route2_9.Position.X, GlobaleSachen.Route2_9.Position.Y, 0);
            Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
        }

        public static void JobBusfahrerRoutenCheck(Client Player)
        {
            if (Player.GetData("BusfahrerRoute") == 1)
            {
                if (Player.GetData("BusfahrerRoutePosition") == 1)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route1_1.Position.X, GlobaleSachen.Route1_1.Position.Y, GlobaleSachen.Route1_1.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route1_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 2);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route1_2.Position.X, GlobaleSachen.Route1_2.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                else if (Player.GetData("BusfahrerRoutePosition") == 2)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route1_2.Position.X, GlobaleSachen.Route1_2.Position.Y, GlobaleSachen.Route1_2.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route1_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 3);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route1_3.Position.X, GlobaleSachen.Route1_3.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                else if (Player.GetData("BusfahrerRoutePosition") == 3)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route1_3.Position.X, GlobaleSachen.Route1_3.Position.Y, GlobaleSachen.Route1_3.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route1_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 4);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route1_4.Position.X, GlobaleSachen.Route1_4.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                else if (Player.GetData("BusfahrerRoutePosition") == 4)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route1_4.Position.X, GlobaleSachen.Route1_4.Position.Y, GlobaleSachen.Route1_4.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route1_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 5);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route1_5.Position.X, GlobaleSachen.Route1_5.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                else if (Player.GetData("BusfahrerRoutePosition") == 5)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route1_5.Position.X, GlobaleSachen.Route1_5.Position.Y, GlobaleSachen.Route1_5.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route1_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 6);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route1_6.Position.X, GlobaleSachen.Route1_6.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                else if (Player.GetData("BusfahrerRoutePosition") == 6)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route1_6.Position.X, GlobaleSachen.Route1_6.Position.Y, GlobaleSachen.Route1_6.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route1_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 7);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route1_7.Position.X, GlobaleSachen.Route1_7.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                else if (Player.GetData("BusfahrerRoutePosition") == 7)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route1_7.Position.X, GlobaleSachen.Route1_7.Position.Y, GlobaleSachen.Route1_7.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route1_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 8);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route1_8.Position.X, GlobaleSachen.Route1_8.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                else if (Player.GetData("BusfahrerRoutePosition") == 8)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route1_8.Position.X, GlobaleSachen.Route1_8.Position.Y, GlobaleSachen.Route1_8.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route1_HaltestellenLohn);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route1_EndBonus);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 0);
                        Player.SetData("BusfahrerJobAngenommen", 0);
                        Player.SetData("BusfahrerRoute", 0);

                        NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Route erfolgreich beendet.");

                        var BusfahrerPunkt = new Vector3(403.169, -642.016, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
            }
            else if (Player.GetData("BusfahrerRoute") == 2)
            {
                if (Player.GetData("BusfahrerRoutePosition") == 1)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route1_1.Position.X, GlobaleSachen.Route1_1.Position.Y, GlobaleSachen.Route1_1.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 2);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route2_2.Position.X, GlobaleSachen.Route2_2.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 2)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route2_2.Position.X, GlobaleSachen.Route2_2.Position.Y, GlobaleSachen.Route2_2.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 3);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route2_3.Position.X, GlobaleSachen.Route2_3.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 3)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route2_3.Position.X, GlobaleSachen.Route2_3.Position.Y, GlobaleSachen.Route2_3.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 4);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route2_4.Position.X, GlobaleSachen.Route2_4.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 4)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route2_4.Position.X, GlobaleSachen.Route2_4.Position.Y, GlobaleSachen.Route2_4.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 5);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route2_5.Position.X, GlobaleSachen.Route2_5.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 5)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route2_5.Position.X, GlobaleSachen.Route2_5.Position.Y, GlobaleSachen.Route2_5.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 6);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route2_6.Position.X, GlobaleSachen.Route2_6.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 6)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route2_6.Position.X, GlobaleSachen.Route2_6.Position.Y, GlobaleSachen.Route2_6.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 7);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route2_7.Position.X, GlobaleSachen.Route2_7.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 7)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route2_7.Position.X, GlobaleSachen.Route2_7.Position.Y, GlobaleSachen.Route2_7.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 8);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route2_8.Position.X, GlobaleSachen.Route2_8.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 8)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route2_8.Position.X, GlobaleSachen.Route2_8.Position.Y, GlobaleSachen.Route2_8.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 9);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route2_9.Position.X, GlobaleSachen.Route2_9.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 9)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route2_9.Position.X, GlobaleSachen.Route2_9.Position.Y, GlobaleSachen.Route2_9.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 10);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route2_10.Position.X, GlobaleSachen.Route2_10.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 10)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route2_10.Position.X, GlobaleSachen.Route2_10.Position.Y, GlobaleSachen.Route2_10.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 11);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route1_6.Position.X, GlobaleSachen.Route1_6.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 11)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route1_6.Position.X, GlobaleSachen.Route1_6.Position.Y, GlobaleSachen.Route1_6.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 12);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route1_7.Position.X, GlobaleSachen.Route1_7.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 12)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route1_7.Position.X, GlobaleSachen.Route1_7.Position.Y, GlobaleSachen.Route1_7.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_HaltestellenLohn);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route2_EndBonus);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 0);
                        Player.SetData("BusfahrerJobAngenommen", 0);
                        Player.SetData("BusfahrerRoute", 0);

                        NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Route erfolgreich beendet.");

                        var BusfahrerPunkt = new Vector3(403.169, -642.016, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
            }
            else if (Player.GetData("BusfahrerRoute") == 3)
            {
                if (Player.GetData("BusfahrerRoutePosition") == 1)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route3_1.Position.X, GlobaleSachen.Route3_1.Position.Y, GlobaleSachen.Route3_1.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route3_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 2);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route3_2.Position.X, GlobaleSachen.Route3_2.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 2)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route3_2.Position.X, GlobaleSachen.Route3_2.Position.Y, GlobaleSachen.Route3_2.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route3_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 3);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route3_3.Position.X, GlobaleSachen.Route3_3.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 3)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route3_3.Position.X, GlobaleSachen.Route3_3.Position.Y, GlobaleSachen.Route3_3.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route3_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 4);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route3_4.Position.X, GlobaleSachen.Route3_4.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 4)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route3_4.Position.X, GlobaleSachen.Route3_4.Position.Y, GlobaleSachen.Route3_4.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route3_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 5);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route3_5.Position.X, GlobaleSachen.Route3_5.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 5)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route3_5.Position.X, GlobaleSachen.Route3_5.Position.Y, GlobaleSachen.Route3_5.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route3_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 6);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route3_6.Position.X, GlobaleSachen.Route3_6.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 6)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route3_6.Position.X, GlobaleSachen.Route3_6.Position.Y, GlobaleSachen.Route3_6.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route3_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 7);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route3_7.Position.X, GlobaleSachen.Route3_7.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 7)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route3_7.Position.X, GlobaleSachen.Route3_7.Position.Y, GlobaleSachen.Route3_7.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route3_HaltestellenLohn);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route3_EndBonus);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 0);
                        Player.SetData("BusfahrerJobAngenommen", 0);
                        Player.SetData("BusfahrerRoute", 0);

                        NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Route erfolgreich beendet.");

                        var BusfahrerPunkt = new Vector3(403.169, -642.016, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
            }
            else if (Player.GetData("BusfahrerRoute") == 4)
            {
                if (Player.GetData("BusfahrerRoutePosition") == 1)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route2_9.Position.X, GlobaleSachen.Route2_9.Position.Y, GlobaleSachen.Route2_9.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route4_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 2);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route4_2.Position.X, GlobaleSachen.Route4_2.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 2)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route4_2.Position.X, GlobaleSachen.Route4_2.Position.Y, GlobaleSachen.Route4_2.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route4_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 3);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route4_3.Position.X, GlobaleSachen.Route4_3.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 3)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route4_3.Position.X, GlobaleSachen.Route4_3.Position.Y, GlobaleSachen.Route4_3.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route4_HaltestellenLohn);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 4);

                        var BusfahrerPunkt = new Vector3(GlobaleSachen.Route4_4.Position.X, GlobaleSachen.Route4_4.Position.Y, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
                if (Player.GetData("BusfahrerRoutePosition") == 4)
                {
                    if (Player.Position.DistanceTo(new Vector3(GlobaleSachen.Route4_4.Position.X, GlobaleSachen.Route4_4.Position.Y, GlobaleSachen.Route4_4.Position.Z)) < 10.0f)
                    {
                        JobBusFahrerRoutenDaten(Player);
                        Ladebalken(Player, 4, 10000);
                        FreezeAuto(Player);
                        Timer.SetTimer(() => UnfreezeAuto(Player), 10000, 1);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route4_HaltestellenLohn);
                        AccountGeldSetzen(Player, 1, GlobaleSachen.Busfahrer_Route4_EndBonus);

                        //Route setzen
                        Player.SetData("BusfahrerRoutePosition", 0);
                        Player.SetData("BusfahrerJobAngenommen", 0);
                        Player.SetData("BusfahrerRoute", 0);

                        NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Route erfolgreich beendet.");

                        var BusfahrerPunkt = new Vector3(403.169, -642.016, 0);
                        Player.TriggerEvent("Navigation", BusfahrerPunkt.X, BusfahrerPunkt.Y);
                        return;
                    }
                }
            }
        }

        public static void JobBusFahrerRoutenDaten(Client Player)
        {
            int Route = Player.GetData("BusfahrerRoute");
            int Position = Player.GetData("BusfahrerRoutePosition");
            int Linie = 0;
            String HaltestellenString = null;

            if(Route == 1) { Linie = 22; HaltestellenString = Position + " / " + GlobaleSachen.Busfahrer_Route1_Haltestellen; }
            else if (Route == 2) { Linie = 18; HaltestellenString = Position + " / " + GlobaleSachen.Busfahrer_Route2_Haltestellen; }
            else if (Route == 3) { Linie = 21; HaltestellenString = Position + " / " + GlobaleSachen.Busfahrer_Route3_Haltestellen; }
            else if (Route == 4) { Linie = 29; HaltestellenString = Position + " / " + GlobaleSachen.Busfahrer_Route4_Haltestellen; }

            Player.TriggerEvent("busfahreranzeigeoeffnen");
            Player.TriggerEvent("busdaten", Linie, HaltestellenString);

            Timer.SetTimer(() => JobBusFahrerRoutenCEFWeg(Player), 5000, 1);
        }

        public static void JobBusFahrerRoutenCEFWeg(Client Player)
        {
            Player.TriggerEvent("busfahreranzeigeschliessen");
        }

        [RemoteEvent("JobBerufskraftfahrerHolz")]
        public static void JobBerufskraftfahrerHolz(Client Player)
        {
            Player.TriggerEvent("berufskraftfahrerbrowserschliessen");
            if (Player.GetData("BerufskraftfahrerJobAngenommen") == 1)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits einen Job angenommen.");
                return;
            }

            //Das er angenommen hat
            Player.SetData("BerufskraftfahrerHolz", 1);

            //Das er angenommen hat
            Player.SetData("BerufskraftfahrerJobAngenommen", 1);

            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Fahre nun zum Holz Ladepunkt.");
            var Ladepunkt = new Vector3(-511.5431, 5241.104, 0);
            Player.TriggerEvent("Navigation", Ladepunkt.X, Ladepunkt.Y);
        }

        [RemoteEvent("JobBerufskraftfahrerDiesel")]
        public static void JobBerufskraftfahrerDiesel(Client Player)
        {
            if (Player.GetData("BerufskraftfahrerJobAngenommen") == 0)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst erst einen Job annehmen!");
                return;
            }

            Player.TriggerEvent("Berufskraftfahrer_Tankstellenbrowseroeffnen", 1);

            int Volumen = 0;
            Volumen = AccountBerufskraftfahrerVolumenBerechnen(Player);

            foreach (TankstelleLokal tanke in Funktionen.TankenListe)
            {
                if (tanke.TankstelleJobSpieler == 0 && tanke.TankstelleDiesel <= 1800)
                {
                    //Benötigte Definitionen
                    long Kraftstoff = 0, EntfernungsBonus = 0, SpritBonus = 0, GesamtBonus = 0;
                    float Distanz = 0.0f;

                    Distanz = Vector3.Distance(new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(tanke.TankstelleX, tanke.TankstelleY, tanke.TankstelleZ));

                    //EntfernungsBonus berechnen
                    EntfernungsBonus = (int)Math.Round(Distanz, 0);
                    EntfernungsBonus = EntfernungsBonus / 200;
                    EntfernungsBonus = EntfernungsBonus * GlobaleSachen.Berufskraftfahrer_Benzin_Entfernungsbonus;

                    //Kraftstoff der übergeben wird
                    Kraftstoff = 2000 - tanke.TankstelleDiesel;
                    if(tanke.TankstelleDiesel + Volumen > 2000)
                    {
                        SpritBonus = Kraftstoff * GlobaleSachen.Berufskraftfahrer_Benzin_LiterBonus;
                    }
                    else
                    {
                        SpritBonus = Volumen * GlobaleSachen.Berufskraftfahrer_Benzin_LiterBonus;
                    }

                    //Der Gesamte Gewinn
                    GesamtBonus += EntfernungsBonus += SpritBonus;

                    //Dem Spieler den Verdienst setzen
                    Player.SetData("BerufskraftfahrerVerdienst", GesamtBonus);

                    //Distanz die an das CEF übergeben wird
                    Distanz = Distanz / 10 / 100;
                    Player.TriggerEvent("Berufskraftfahrer_Tankstelleeintragen", tanke.Id, tanke.TankstelleBeschreibung, Kraftstoff, NAPI.Util.ToJson(Math.Round(Distanz, 2)), GeldFormatieren(GesamtBonus));
                }
            }
        }

        public static void JobBerufskraftfahrerHolzLaden(Client Player)
        {
            if (Player.GetData("BerufskraftfahrerJobAngenommen") == 0)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst erst einen Job annehmen!");
                return;
            }
            Player.SetData("BerufskraftfahrerHolzGeladen", 1);
            FreezeAuto(Player);
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dein LKW wird beladen...");
            Ladebalken(Player, 3, 20000);
            Timer.SetTimer(() => JobBerufskraftfahrerHolzLadenWeiterFahren(Player), 20000, 1);
        }

        public static void JobBerufskraftfahrerHolzLadenWeiterFahren(Client Player)
        {
            //Server crash prevention
            if (Player.IsInVehicle == false) { Unfreeze(Player); return; }

            //Ja er hat geladen
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: " + AccountBerufskraftfahrerVolumenBerechnen(Player) + " Kilogramm Holz wurden in deinen LKW geladen.");

            var HolzEntladePunkt = new Vector3(1307.297, 4324.441, 38.20026);
            Player.TriggerEvent("Navigation", HolzEntladePunkt.X, HolzEntladePunkt.Y);

            UnfreezeAuto(Player);

        }

        [RemoteEvent("JobBerufskraftfahrerKraftstoff")]
        public static void JobBerufskraftfahrerKraftstoff(Client Player, int Typ)
        {
            Player.TriggerEvent("berufskraftfahrerbrowserschliessen");
            if (Player.GetData("BerufskraftfahrerJobAngenommen") == 1)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits einen Job angenommen.");
                return;
            }

            //Damit wir wissen welchen Kraftstoff er liefern möchte
            Player.SetData("BerufskraftfahrerKraftstoffTyp", Typ);

            //Das er angenommen hat
            Player.SetData("BerufskraftfahrerJobAngenommen", 1);

            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Fahre nun zum Kraftstoff Ladepunkt.");
            var Ladepunkt = new Vector3(815.2087, -1590.846, 0);
            Player.TriggerEvent("Navigation", Ladepunkt.X, Ladepunkt.Y);
        }

        [RemoteEvent("JobBerufskraftfahrerTanke")]
        public static void JobBerufskraftfahrerTanke(Client Player, int Id)
        {
            Player.TriggerEvent("Berufskraftfahrer_Tankstellenbrowserschliessen");

            FreezeAuto(Player);
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dein LKW wird beladen...");
            Ladebalken(Player, 3, 20000);
            Timer.SetTimer(() => JobBerufskraftfahrerTankeBeladen(Player, Id), 20000, 1);

        }

        public static void JobBerufskraftfahrerTankeBeladen(Client Player, int Id)
        {
            //Server crash prevention
            if (Player.IsInVehicle == false) { Unfreeze(Player); return; }

            UnfreezeAuto(Player);

            if (Player.GetData("BerufskraftfahrerKraftstoffTyp") == 1)
            {
                foreach (TankstelleLokal tanke in Funktionen.TankenListe)
                {
                    if (tanke.Id == Id)
                    {
                        if (tanke.TankstelleJobSpieler == 0)
                        {
                            var Tankstelle = new Vector3(tanke.TankstelleX, tanke.TankstelleY, tanke.TankstelleZ);
                            Player.TriggerEvent("Navigation", Tankstelle.X, Tankstelle.Y);
                            tanke.TankstelleJobSpieler = 1;
                            tanke.TankstelleGeändert = true;
                            Player.SetData("BerufskraftfahrerDieselTanke", tanke.Id);
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Beliefere nun die Tankstelle mit Diesel.");
                        }
                        else
                        {
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es tut uns leid. Diese Tanke wird bereits beliefert.");
                        }
                    }
                }
            }
            else if (Player.GetData("BerufskraftfahrerKraftstoffTyp") == 2)
            {
                foreach (TankstelleLokal tanke in Funktionen.TankenListe)
                {
                    if (tanke.Id == Id)
                    {
                        if (tanke.TankstelleJobSpieler == 0)
                        {
                            var Tankstelle = new Vector3(tanke.TankstelleX, tanke.TankstelleY, tanke.TankstelleZ);
                            Player.TriggerEvent("Navigation", Tankstelle.X, Tankstelle.Y);
                            tanke.TankstelleJobSpieler = 1;
                            tanke.TankstelleGeändert = true;
                            Player.SetData("BerufskraftfahrerE10Tanke", tanke.Id);
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Beliefere nun die Tankstelle mit E10.");
                        }
                    }
                }
            }
            else if (Player.GetData("BerufskraftfahrerKraftstoffTyp") == 3)
            {
                foreach (TankstelleLokal tanke in Funktionen.TankenListe)
                {
                    if (tanke.Id == Id)
                    {
                        if (tanke.TankstelleJobSpieler == 0)
                        {
                            var Tankstelle = new Vector3(tanke.TankstelleX, tanke.TankstelleY, tanke.TankstelleZ);
                            Player.TriggerEvent("Navigation", Tankstelle.X, Tankstelle.Y);
                            tanke.TankstelleJobSpieler = 1;
                            tanke.TankstelleGeändert = true;
                            Player.SetData("BerufskraftfahrerSuperTanke", tanke.Id);
                            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Beliefere nun die Tankstelle mit Super.");
                        }
                    }
                }
            }
        }

        public static void JobBerufskraftfahrerE10(Client Player)
        {
            if (Player.GetData("BerufskraftfahrerJobAngenommen") == 0)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst erst einen Job annehmen!");
                return;
            }

            Player.TriggerEvent("Berufskraftfahrer_Tankstellenbrowseroeffnen", 2);

            int Volumen = 0;
            Volumen = AccountBerufskraftfahrerVolumenBerechnen(Player);

            foreach (TankstelleLokal tanke in Funktionen.TankenListe)
            {
                if (tanke.TankstelleJobSpieler == 0 && tanke.TankstelleE10 <= 1800)
                {
                    //Benötigte Definitionen
                    int Kraftstoff = 0, EntfernungsBonus = 0, SpritBonus = 0, GesamtBonus = 0;
                    float Distanz = 0.0f;

                    Distanz = Vector3.Distance(new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(tanke.TankstelleX, tanke.TankstelleY, tanke.TankstelleZ));

                    //EntfernungsBonus berechnen
                    EntfernungsBonus = (int)Math.Round(Distanz, 0);
                    EntfernungsBonus = EntfernungsBonus / 200;
                    EntfernungsBonus = EntfernungsBonus * 100;

                    //Kraftstoff der übergeben wird
                    Kraftstoff = 2000 - tanke.TankstelleE10;
                    if (tanke.TankstelleE10 + Volumen > 2000)
                    {
                        SpritBonus = Kraftstoff * 2;
                    }
                    else
                    {
                        SpritBonus = Volumen * 2;
                    }

                    //Der Gesamte Gewinn
                    GesamtBonus += EntfernungsBonus += SpritBonus;

                    //Dem Spieler den Verdienst setzen
                    Player.SetData("BerufskraftfahrerVerdienst", GesamtBonus);

                    //Distanz die an das CEF übergeben wird
                    Distanz = Distanz / 10 / 100;
                    Player.TriggerEvent("Berufskraftfahrer_Tankstelleeintragen", tanke.Id, tanke.TankstelleBeschreibung, Kraftstoff, NAPI.Util.ToJson(Math.Round(Distanz, 2)), GeldFormatieren(GesamtBonus));
                }
            }
        }

        public static void JobBerufskraftfahrerSuper(Client Player)
        {
            if (Player.GetData("BerufskraftfahrerJobAngenommen") == 0)
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst erst einen Job annehmen!");
                return;
            }

            Player.TriggerEvent("Berufskraftfahrer_Tankstellenbrowseroeffnen", 3);

            int Volumen = 0;
            Volumen = AccountBerufskraftfahrerVolumenBerechnen(Player);

            foreach (TankstelleLokal tanke in Funktionen.TankenListe)
            {
                if (tanke.TankstelleJobSpieler == 0 && tanke.TankstelleSuper <= 1800)
                {
                    //Benötigte Definitionen
                    int Kraftstoff = 0, EntfernungsBonus = 0, SpritBonus = 0, GesamtBonus = 0;
                    float Distanz = 0.0f;

                    Distanz = Vector3.Distance(new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), new Vector3(tanke.TankstelleX, tanke.TankstelleY, tanke.TankstelleZ));

                    //EntfernungsBonus berechnen
                    EntfernungsBonus = (int)Math.Round(Distanz, 0);
                    EntfernungsBonus = EntfernungsBonus / 200;
                    EntfernungsBonus = EntfernungsBonus * 100;

                    //Kraftstoff der übergeben wird
                    Kraftstoff = 2000 - tanke.TankstelleSuper;
                    if (tanke.TankstelleSuper + Volumen > 2000)
                    {
                        SpritBonus = Kraftstoff * 2;
                    }
                    else
                    {
                        SpritBonus = Volumen * 2;
                    }

                    //Der Gesamte Gewinn
                    GesamtBonus += EntfernungsBonus += SpritBonus;

                    //Dem Spieler den Verdienst setzen
                    Player.SetData("BerufskraftfahrerVerdienst", GesamtBonus);

                    //Distanz die an das CEF übergeben wird
                    Distanz = Distanz / 10 / 100;
                    Player.TriggerEvent("Berufskraftfahrer_Tankstelleeintragen", tanke.Id, tanke.TankstelleBeschreibung, Kraftstoff, NAPI.Util.ToJson(Math.Round(Distanz, 2)), GeldFormatieren(GesamtBonus));
                }
            }
        }

        public static void JobBerufskraftfahrerFahrzeugSpawnen(Client Player)
        {
            //Benötigte Abfragen
            if(Player.GetData("BerufskraftfahrerFahrzeug") == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits ein Job Fahrzeug!"); return; }
            if(GlobaleSachen.BerufskraftFahrerFahrzeugGespawnt == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde gerade erst ein LKW gespawnt. Warte kurz."); return; }

            //Fahrzeug
            String Fahrzeug = "pounder2";
            uint AutoCode = NAPI.Util.GetHashKey(Fahrzeug);

            //Objekt für die Liste erzeugen
            AutoLokal auto = new AutoLokal();

            //Das Fahrzeug spawnen
            auto.Fahrzeug = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), Player.Rotation.Z, 113, 113, numberPlate: "Job");

            auto.Fahrzeug.NumberPlate = "Job";
            auto.Fahrzeug.Dimension = 0;

            auto.Fahrzeug.SetData("Id", -2);

            //Daten setzen
            auto.Id = -2;
            auto.FahrzeugBeschreibung = "Job";
            auto.FahrzeugName = Fahrzeug;
            auto.FahrzeugTyp = 1;
            auto.FahrzeugFraktion = 0;
            auto.FahrzeugJob = 1;
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
            auto.FahrzeugFarbe1 = 113;
            auto.FahrzeugFarbe2 = 113;
            auto.TankVolumen = Funktionen.TankVolumenBerechnen(Fahrzeug);
            auto.TankInhalt = Funktionen.TankVolumenBerechnen(Fahrzeug) * 10 * 100;
            auto.Kilometerstand = 0;
            auto.KraftstoffArt = 1;
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
            AutoListe.Add(auto);

            //Spieler in das Job Fahrzeug setzen
            Player.SetIntoVehicle(auto.Fahrzeug, -1);

            //Lokal setzen das er ein Fahrzeug hat
            Player.SetData("BerufskraftfahrerFahrzeug", 1);
            auto.Fahrzeug.SetData("GeradeGespawnt", 1);
            Timer.SetTimer(() => JobFahrzeugNichtGespawnt(auto.Fahrzeug), 2000, 1);

            //Dem Spieler das Fahrzeug zuweisen
            AccountJobFahrzeugSetzen(Player, auto.Fahrzeug);

            Player.TriggerEvent("LKWSpeed");
            Player.TriggerEvent("berufskraftfahrerbrowseroeffnen");

            //Damit niemand anderes ein Fahrzeug spawnen kann
            GlobaleSachen.BerufskraftFahrerFahrzeugGespawnt = 1;
            Timer.SetTimer(() => JobBerufskraftFahrerFahrzeugSpawnPause(), 20000, 1);
        }

        public static void JobBusfahrerFahrzeugSpawnen(Client Player)
        {
            //Benötigte Abfragen
            if (Player.GetData("BusfahrerFahrzeug") == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits ein Job Fahrzeug!"); return; }
            if (GlobaleSachen.BusfahrerFahrzeugGespawnt == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es wurde gerade erst ein Bus gespawnt. Warte kurz."); return; }

            //Fahrzeug
            String Fahrzeug = "bus";
            uint AutoCode = NAPI.Util.GetHashKey(Fahrzeug);

            //Objekt für die Liste erzeugen
            AutoLokal auto = new AutoLokal();

            //Das Fahrzeug spawnen
            auto.Fahrzeug = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z), Player.Rotation.Z, 44, 44, numberPlate: "Job");

            auto.Fahrzeug.NumberPlate = "Job";
            auto.Fahrzeug.Dimension = 0;

            auto.Fahrzeug.SetData("Id", -2);

            //Daten setzen
            auto.Id = -2;
            auto.FahrzeugBeschreibung = "Job";
            auto.FahrzeugName = Fahrzeug;
            auto.FahrzeugTyp = 1;
            auto.FahrzeugFraktion = 0;
            auto.FahrzeugJob = 1;
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
            auto.FahrzeugFarbe1 = 44;
            auto.FahrzeugFarbe2 = 44;
            auto.TankVolumen = Funktionen.TankVolumenBerechnen(Fahrzeug);
            auto.TankInhalt = Funktionen.TankVolumenBerechnen(Fahrzeug) * 10 * 100;
            auto.Kilometerstand = 0;
            auto.KraftstoffArt = 1;
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
            AutoListe.Add(auto);

            //Spieler in das Job Fahrzeug setzen
            Player.SetIntoVehicle(auto.Fahrzeug, -1);

            //Lokal setzen das er ein Fahrzeug hat
            Player.SetData("BusfahrerFahrzeug", 1);
            auto.Fahrzeug.SetData("GeradeGespawnt", 1);
            Timer.SetTimer(() => JobFahrzeugNichtGespawnt(auto.Fahrzeug), 2000, 1);

            //Dem Spieler das Fahrzeug zuweisen
            AccountJobFahrzeugSetzen(Player, auto.Fahrzeug);

            Player.TriggerEvent("busfahrerbrowseroeffnen");

            //Damit niemand anderes ein Fahrzeug spawnen kann
            GlobaleSachen.BusfahrerFahrzeugGespawnt = 1;
            Timer.SetTimer(() => JobBusfahrerFahrzeugSpawnPause(), 20000, 1);
        }

        public static void JobBerufskraftFahrerFahrzeugSpawnPause()
        {
            GlobaleSachen.BerufskraftFahrerFahrzeugGespawnt = 0;
        }

        public static void JobBusfahrerFahrzeugSpawnPause()
        {
            GlobaleSachen.BusfahrerFahrzeugGespawnt = 0;
        }

        public static void JobFahrzeugNichtGespawnt(Vehicle Fahrzeug)
        {
            Fahrzeug.SetData("GeradeGespawnt", 0);
        }

        public static void AdminFahrzeugLöschen(Client Player, Vehicle Fahrzeug)
        {
            AutoLokal auto = new AutoLokal();
            auto = Funktionen.AutoBekommen(Fahrzeug);

            //Auto zerstören
            auto.Fahrzeug.Delete();

            //Von der lokalen Liste entfernen
            AutoListe.Remove(auto);

            //Log Eintrag
            LogEintrag(Player, "Fahrzeug ID: " + auto.Id + " gelöscht");
        }

        public static void SpielerMinutenTimer(Client Player)
        {
            if(NAPI.Player.IsPlayerConnected(Player) && Player.GetData("Eingeloggt") == 1)
            {
                //Lokalen Account bekommen
                AccountLokal account = new AccountLokal();
                account = AccountBekommen(Player);

                //Speilzeit der Spieler
                account.Spielzeit += 1;
                if(account.Kündigungszeit > 0)
                {
                    account.Kündigungszeit -= 1;
                }
                account.AccountGeändert = true;

                //Mietzeit der Spieler
                MietFahrzeugSetzen(Player);
                Timer.SetTimer(() => SpielerMinutenTimer(Player), 60000, 1);
            }
        }

        public static void FahrzeugeSpeichern()
        {
            //Schleife durch alle Fahrzeuge
            foreach (var Fahrzeuge in NAPI.Pools.GetAllVehicles())
            {
                //Benötigte Definitionen
                int AutoId = Fahrzeuge.GetData("Id");

                AutoLokal auto = AutoListe.Find(x => x.Fahrzeug == Fahrzeuge);

                if(auto.FahrzeugTyp == 0 || auto.FahrzeugTyp == 1)
                {
                    return;
                }

                if (auto.FahrzeugGeändert == true)
                {
                    //Kilometerstand teilen damit keine zu große Zahl entsteht
                    float AktuellerKilometerstand = auto.Kilometerstand / 10 / 100;
                    float AktuellerTank = auto.TankInhalt / 10 / 100;

                    //Fahrzeug aus der DB greifen
                    var AktuellesFahrzeug = ContextFactory.Instance.srp_fahrzeuge.Where(x => x.Id == AutoId).FirstOrDefault();

                    //Kilometerstand zuweisen
                    AktuellesFahrzeug.FahrzeugTyp = auto.FahrzeugTyp;
                    AktuellesFahrzeug.FahrzeugFraktion = auto.FahrzeugFraktion;
                    AktuellesFahrzeug.FahrzeugJob = auto.FahrzeugJob;
                    AktuellesFahrzeug.FahrzeugSpieler = auto.FahrzeugSpieler;
                    AktuellesFahrzeug.FahrzeugMietpreis = auto.FahrzeugMietpreis;
                    AktuellesFahrzeug.FahrzeugKaufpreis = auto.FahrzeugKaufpreis;
                    AktuellesFahrzeug.FahrzeugAutohaus = auto.FahrzeugAutohaus;
                    AktuellesFahrzeug.FahrzeugMaxMietzeit = auto.FahrzeugMaxMietzeit;
                    AktuellesFahrzeug.FahrzeugMietzeit = auto.FahrzeugMietzeit;
                    AktuellesFahrzeug.FahrzeugX = Fahrzeuge.Position.X;
                    AktuellesFahrzeug.FahrzeugY = Fahrzeuge.Position.Y;
                    AktuellesFahrzeug.FahrzeugZ = Fahrzeuge.Position.Z;
                    AktuellesFahrzeug.FahrzeugRot = Fahrzeuge.Rotation.Z;
                    AktuellesFahrzeug.FahrzeugFarbe1 = auto.FahrzeugFarbe1;
                    AktuellesFahrzeug.FahrzeugFarbe2 = auto.FahrzeugFarbe2;
                    AktuellesFahrzeug.TankVolumen = auto.TankVolumen;
                    AktuellesFahrzeug.TankInhalt = AktuellerTank;
                    AktuellesFahrzeug.Kilometerstand = AktuellerKilometerstand;
                    AktuellesFahrzeug.KraftstoffArt = auto.KraftstoffArt;
                    AktuellesFahrzeug.FahrzeugHU = auto.FahrzeugHU;
                    AktuellesFahrzeug.FahrzeugAbgeschlossen = auto.FahrzeugAbgeschlossen;
                    AktuellesFahrzeug.FahrzeugMotor = auto.FahrzeugMotor;
                    AktuellesFahrzeug.FahrzeugGespawnt = auto.FahrzeugGespawnt;

                    //Lokal updaten
                    auto.FahrzeugX = Fahrzeuge.Position.X;
                    auto.FahrzeugY = Fahrzeuge.Position.Y;
                    auto.FahrzeugZ = Fahrzeuge.Position.Z;
                    auto.FahrzeugRot = Fahrzeuge.Rotation.Z;

                    //Query absenden
                    ContextFactory.Instance.SaveChanges();

                    //Sachen am Fahrzeug updaten wie Nummernschild etc.
                    Fahrzeuge.NumberPlate = auto.FahrzeugBeschreibung;

                    //Damit er nicht dauerthaft gespeichert wird
                    auto.FahrzeugGeändert = false;
                }
            }
        }

        public static void FahrzeugSpeichern(Vehicle Fahrzeuge)
        {
            if(Fahrzeuge.GetData("Id") == 0) { return; }

            //Benötigte Definitionen
            int AutoId = Fahrzeuge.GetData("Id");

            AutoLokal auto = AutoListe.Find(x => x.Fahrzeug == Fahrzeuge);

            if (auto.FahrzeugTyp == 0 || auto.FahrzeugTyp == 1)
            {
                return;
            }

            if (auto.FahrzeugGeändert == true)
            {
                //Kilometerstand teilen damit keine zu große Zahl entsteht
                float AktuellerKilometerstand = auto.Kilometerstand / 10 / 100;
                float AktuellerTank = auto.TankInhalt / 10 / 100;

                //Fahrzeug aus der DB greifen
                var AktuellesFahrzeug = ContextFactory.Instance.srp_fahrzeuge.Where(x => x.Id == AutoId).FirstOrDefault();

                //Kilometerstand zuweisen
                AktuellesFahrzeug.FahrzeugTyp = auto.FahrzeugTyp;
                AktuellesFahrzeug.FahrzeugFraktion = auto.FahrzeugFraktion;
                AktuellesFahrzeug.FahrzeugJob = auto.FahrzeugJob;
                AktuellesFahrzeug.FahrzeugSpieler = auto.FahrzeugSpieler;
                AktuellesFahrzeug.FahrzeugMietpreis = auto.FahrzeugMietpreis;
                AktuellesFahrzeug.FahrzeugKaufpreis = auto.FahrzeugKaufpreis;
                AktuellesFahrzeug.FahrzeugAutohaus = auto.FahrzeugAutohaus;
                AktuellesFahrzeug.FahrzeugMaxMietzeit = auto.FahrzeugMaxMietzeit;
                AktuellesFahrzeug.FahrzeugMietzeit = auto.FahrzeugMietzeit;
                AktuellesFahrzeug.FahrzeugX = Fahrzeuge.Position.X;
                AktuellesFahrzeug.FahrzeugY = Fahrzeuge.Position.Y;
                AktuellesFahrzeug.FahrzeugZ = Fahrzeuge.Position.Z;
                AktuellesFahrzeug.FahrzeugRot = Fahrzeuge.Rotation.Z;
                AktuellesFahrzeug.FahrzeugFarbe1 = auto.FahrzeugFarbe1;
                AktuellesFahrzeug.FahrzeugFarbe2 = auto.FahrzeugFarbe2;
                AktuellesFahrzeug.TankVolumen = auto.TankVolumen;
                AktuellesFahrzeug.TankInhalt = AktuellerTank;
                AktuellesFahrzeug.Kilometerstand = AktuellerKilometerstand;
                AktuellesFahrzeug.KraftstoffArt = auto.KraftstoffArt;
                AktuellesFahrzeug.FahrzeugHU = auto.FahrzeugHU;
                AktuellesFahrzeug.FahrzeugAbgeschlossen = auto.FahrzeugAbgeschlossen;
                AktuellesFahrzeug.FahrzeugMotor = auto.FahrzeugMotor;
                AktuellesFahrzeug.FahrzeugGespawnt = auto.FahrzeugGespawnt;

                //Lokal updaten
                auto.FahrzeugX = Fahrzeuge.Position.X;
                auto.FahrzeugY = Fahrzeuge.Position.Y;
                auto.FahrzeugZ = Fahrzeuge.Position.Z;
                auto.FahrzeugRot = Fahrzeuge.Rotation.Z;

                //Query absenden
                ContextFactory.Instance.SaveChanges();

                //Sachen am Fahrzeug updaten wie Nummernschild etc.
                Fahrzeuge.NumberPlate = auto.FahrzeugBeschreibung;

                //Damit er nicht dauerthaft gespeichert wird
                auto.FahrzeugGeändert = false;
            }
        }

        public static void FahrzeugeUpdaten()
        {
            //Schleife durch alle Fahrzeuge
            foreach (var Fahrzeuge in NAPI.Pools.GetAllVehicles())
            {
                //Benötigte Definitionen
                int AutoId = Fahrzeuge.GetData("Id");
                String VerkaufsText = null;

                foreach (AutoLokal auto in Funktionen.AutoListe)
                {
                    if (AutoId == auto.Id && auto.FahrzeugAutohaus > 0)
                    {
                        auto.AutohausTextLabel.Delete();

                        VerkaufsText = "~r~[~w~Zu verkaufen~r~]~n~";
                        VerkaufsText += "Name~w~: " + auto.FahrzeugName + "~n~~r~";
                        VerkaufsText += "Autohaus~w~: " + auto.FahrzeugAutohaus + "~n~~r~";
                        VerkaufsText += "Preis~w~: " + GeldFormatieren(auto.FahrzeugKaufpreis);

                        auto.AutohausTextLabel = NAPI.TextLabel.CreateTextLabel(VerkaufsText, new Vector3(Fahrzeuge.Position.X, Fahrzeuge.Position.Y, Fahrzeuge.Position.Z), 18.0f, 1.00f, 4, new Color(255, 255, 255), false, 0);
                    }
                    else if (AutoId == auto.Id && auto.FahrzeugAutohaus < 0)
                    {
                        auto.AutohausTextLabel.Delete();

                        VerkaufsText = "~r~[~w~Manufaktur~r~]~n~";
                        VerkaufsText += "Name~w~: " + auto.FahrzeugName + "~n~~r~";
                        VerkaufsText += "Preis~w~: " + GeldFormatieren(auto.FahrzeugKaufpreis);

                        auto.AutohausTextLabel = NAPI.TextLabel.CreateTextLabel(VerkaufsText, new Vector3(Fahrzeuge.Position.X, Fahrzeuge.Position.Y, Fahrzeuge.Position.Z), 18.0f, 1.00f, 4, new Color(255, 255, 255), false, 0);
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
                    supermarkt.SupermarktBlip.Sprite = 52;
                    supermarkt.SupermarktBlip.Color = 2;
                }
                else
                {
                    SupermarktText = "~g~[~w~24/7 ID: " + supermarkt.Id + "~g~]~n~";
                    SupermarktText += "~w~Beschreibung: " + supermarkt.SupermarktBeschreibung + "~n~";
                    SupermarktText += "Besitzer: " + BesitzerNamenBekommen(supermarkt.SupermarktBesitzer);
                    supermarkt.SupermarktBlip.Name = supermarkt.SupermarktBeschreibung;
                    supermarkt.SupermarktBlip.ShortRange = true;
                    supermarkt.SupermarktBlip.Sprite = 52;
                    supermarkt.SupermarktBlip.Color = 1;
                }
                //Text Updaten
                NAPI.TextLabel.SetTextLabelText(supermarkt.SupermarktLabel, SupermarktText);
            }
        }

        public static void AlleAutohäuserUpdaten()
        {
            foreach (AutohausLokal autohaus in AutohausListe)
            {
                String AutohausText = null;
                if (autohaus.AutohausBesitzer == 0)
                {
                    AutohausText = "~g~[~w~Autohaus ID: " + autohaus.Id + "~g~]~n~";
                    AutohausText += "~w~Kaufpreis: " + GeldFormatieren(autohaus.AutohausKaufpreis) + "~n~";
                    AutohausText += "Beschreibung: " + autohaus.AutohausBeschreibung;
                    autohaus.AutohausBlip.Name = autohaus.AutohausBeschreibung;
                    autohaus.AutohausBlip.ShortRange = true;
                    autohaus.AutohausBlip.Sprite = 523;
                    autohaus.AutohausBlip.Color = 2;
                }
                else
                {
                    AutohausText = "~g~[~w~Autohaus ID: " + autohaus.Id + "~g~]~n~";
                    AutohausText += "~w~Beschreibung: " + autohaus.AutohausBeschreibung + "~n~";
                    AutohausText += "Besitzer: " + BesitzerNamenBekommen(autohaus.AutohausBesitzer);
                    autohaus.AutohausBlip.Name = autohaus.AutohausBeschreibung;
                    autohaus.AutohausBlip.ShortRange = true;
                    autohaus.AutohausBlip.Sprite = 523;
                    autohaus.AutohausBlip.Color = 1;
                }
                //Text Updaten
                NAPI.TextLabel.SetTextLabelText(autohaus.AutohausLabel, AutohausText);
            }
        }

        public static void AlleTankstellenSpeichern()
        {
            foreach (TankstelleLokal tanke in TankenListe)
            {
                if (tanke.TankstelleGeändert == true)
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

        public static void AlleBotsSpeichern()
        {
            foreach (BotLokal bot in BotListe)
            {
                if (bot.BotGeändert == true)
                {
                    var Bot = ContextFactory.Instance.srp_bots.Where(x => x.Id == bot.Id).FirstOrDefault();

                    Bot.BotName = bot.BotName;
                    Bot.BotBeschreibung = bot.BotBeschreibung;
                    Bot.BotX = bot.BotX;
                    Bot.BotY = bot.BotY;
                    Bot.BotZ = bot.BotZ;
                    Bot.BotKopf = bot.BotKopf;
                    Bot.BotDimension = bot.BotDimension;

                    //Query absenden
                    ContextFactory.Instance.SaveChanges();

                    //Damit Sie nicht dauerhaft gespeichert wird
                    bot.BotGeändert = false;
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

        public static void AlleGruppenSpeichern()
        {
            foreach (GruppenLokal gruppe in GruppenListe)
            {
                if (gruppe.GruppeGeändert == true)
                {
                    var Gruppe = ContextFactory.Instance.srp_gruppen.Where(x => x.Id == gruppe.Id).FirstOrDefault();

                    Gruppe.GruppenName = gruppe.GruppenName;
                    Gruppe.GruppenBesitzer = gruppe.GruppenBesitzer;
                    Gruppe.GruppenTag = gruppe.GruppenTag;
                    Gruppe.GruppenFarbe = gruppe.GruppenFarbe;
                    Gruppe.GruppenGeld = gruppe.GruppenGeld;
                    Gruppe.GruppenRang1Name = gruppe.GruppenRang1Name;
                    Gruppe.GruppenRang2Name = gruppe.GruppenRang2Name;
                    Gruppe.GruppenRang3Name = gruppe.GruppenRang3Name;
                    Gruppe.GruppenRang4Name = gruppe.GruppenRang4Name;
                    Gruppe.GruppenRang5Name = gruppe.GruppenRang5Name;

                    //Query absenden
                    ContextFactory.Instance.SaveChanges();

                    //Damit Sie nicht dauerhaft gespeichert wird
                    gruppe.GruppeGeändert = false;
                }
            }
        }

        public static void AlleAutohäuserSpeichern()
        {
            foreach (AutohausLokal autohaus in AutohausListe)
            {
                if (autohaus.AutohausGeändert == true)
                {
                    var Autohaus = ContextFactory.Instance.srp_autohäuser.Where(x => x.Id == autohaus.Id).FirstOrDefault();

                    Autohaus.AutohausBeschreibung = autohaus.AutohausBeschreibung;
                    Autohaus.AutohausBesitzer = autohaus.AutohausBesitzer;
                    Autohaus.AutohausGeld = autohaus.AutohausGeld;
                    Autohaus.AutohausKaufpreis = autohaus.AutohausKaufpreis;
                    Autohaus.AutohausX = autohaus.AutohausX;
                    Autohaus.AutohausY = autohaus.AutohausY;
                    Autohaus.AutohausZ = autohaus.AutohausZ;

                    //Query absenden
                    ContextFactory.Instance.SaveChanges();

                    //Damit Sie nicht dauerhaft gespeichert wird
                    autohaus.AutohausGeändert = false;
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
            NAPI.TextLabel.CreateTextLabel("~r~StrawberryRP~w~~n~Hier kannst du einen Roller Mieten.~n~Preis: 30€", new Vector3(-3237.754, 969.6091, 12.94306), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~r~StrawberryRP~w~~n~Willkommen bei uns!~n~Wir hoffen du bist gut angekommen.", new Vector3(-3237.508, 965.3855, 13.04449), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);

            //Stadthalle
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Stadthalle - Eingang~g~]", new Vector3(337.764, -1562.13, 30.298), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Stadthalle - Ausgang~g~]", new Vector3(334.039, -1564.35, 30.3066), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Stadthalle - Service~g~]", new Vector3(331.347, -1569.97, 30.3076), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);

            //Heiraten
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Kirche - /heiraten~g~]", new Vector3(-329.9244, 6150.168, 32.31319), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);

            //Arbeitsamt
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Arbeitsamt - Eingang~g~]", new Vector3(250.553, -1594.62, 31.5322), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Arbeitsamt - Ausgang~g~]", new Vector3(249.744, -1597.58, 25.5466), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Arbeitsamt - Service~g~]", new Vector3(246.22, -1603.7, 25.5601), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);

            //Berufskraftfahrer
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Berufskraftfahrer - Spawnpunkt~g~]", new Vector3(-1546.57, 1367.763, 126.1016), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Berufskraftfahrer - Kraftstoff Ladepunkt~g~]", new Vector3(815.2087, -1590.846, 31.01333), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Berufskraftfahrer - Holz Ladepunkt~g~]", new Vector3(-511.5431, 5241.104, 80.30409), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Berufskraftfahrer - Holz Abladepunkt~g~]", new Vector3(1307.297, 4324.441, 38.20026), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);

            //Busfahrer
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Busfahrer - Spawnpunkt~g~]", new Vector3(403.169, -642.016, 28.5002), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);

            //Busfahrer Route 1
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [22], [18]", new Vector3(306.639, -765.855, 28.7369), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [22]", new Vector3(112.742, -783.588, 30.8554), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [22]", new Vector3(-244.619, -715.44, 32.9559), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [22]", new Vector3(-712.426, -827.37, 22.952), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [22]", new Vector3(-740.321, -750.287, 26.2996), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [22], [18]", new Vector3(-689.944, -668.301, 30.3546), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [22], [18]", new Vector3(-506.123, -668.609, 32.5175), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [22]", new Vector3(224.769, -853.519, 29.5364), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);

            //Busfahrer Route 2
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [18]", new Vector3(275.028, -585.099, 43.1406), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [18]", new Vector3(269.834, -358.579, 44.7709), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [18]", new Vector3(334.736, 160.639, 103.246), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [18]", new Vector3(-247.98, 30.9539, 56.751), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [18]", new Vector3(-499.74, 20.2629, 44.7918), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [18]", new Vector3(-688.621, -6.3535, 38.2303), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [18]", new Vector3(-753.362, -34.1787, 37.6805), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [18]", new Vector3(-927.927, -125.374, 37.5825), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [18]", new Vector3(-989.048, -399.978, 37.7187), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);

            //Busfahrer Route 3
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [21]", new Vector3(61.4772, -652.598, 31.0374), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [21]", new Vector3(-173.5, -820.069, 30.5454), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [21]", new Vector3(-1277.57, -1224.05, 3.95632), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [21]", new Vector3(-1668.04, -543.493, 34.45), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [21]", new Vector3(-1940.37, -305.581, 43.7902), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [21]", new Vector3(-1476.76, -632.458, 30.0296), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [21]", new Vector3(-1272.07, -560.944, 29.3496), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);

            //Busfahrer Route 4
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [29]", new Vector3(-1167.17, -401.918, 34.9337), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [29]", new Vector3(-1409.08, -569.181, 29.8156), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~g~[~w~Bushaltestelle~g~]~n~~g~Linie~w~: [29]", new Vector3(-1516.2, -373.59, 41.7262), 12.0f, 0.60f, 4, new Color(255, 255, 255), false, 0);
        }

        public static void BlipsLaden()
        {
            Blip Noobspawn = NAPI.Blip.CreateBlip(new Vector3(-3237.754, 969.6091, 12.94306)); Noobspawn.Name = "Neulingsspawn"; Noobspawn.ShortRange = true; Noobspawn.Sprite = 162; Noobspawn.Color = 12;
            Blip Stadthalle = NAPI.Blip.CreateBlip(new Vector3(337.126, -1562.91, 30.298)); Stadthalle.Name = "Stadthalle"; Stadthalle.ShortRange = true; Stadthalle.Sprite = 498;
            Blip Medial_Department = NAPI.Blip.CreateBlip(new Vector3(375.0317, -594.1043, 28.72511)); Medial_Department.Name = "Medical Department"; Medial_Department.ShortRange = true; Medial_Department.Sprite = 153; Medial_Department.Color = 1;
            Blip Manufaktur_Mittelklasse = NAPI.Blip.CreateBlip(new Vector3(-1073.475, -2147.839, 13.40069)); Manufaktur_Mittelklasse.Name = "Manufaktur Mittelklassefahrzeuge"; Manufaktur_Mittelklasse.ShortRange = true; Manufaktur_Mittelklasse.Sprite = 78;
            Blip Vespucci_Police = NAPI.Blip.CreateBlip(new Vector3(-1092.624, -809.7153, 19.27477)); Vespucci_Police.Name = "Vespucci Police Department"; Vespucci_Police.ShortRange = true; Vespucci_Police.Sprite = 60; Vespucci_Police.Color = 38;
            Blip Liveinvader = NAPI.Blip.CreateBlip(new Vector3(-1033.761, -223.1129, 39.01439)); Liveinvader.Name = "Lifeinvador"; Liveinvader.ShortRange = true; Liveinvader.Sprite = 77; Liveinvader.Color = 1;
            Blip Arbeitsamt = NAPI.Blip.CreateBlip(new Vector3(250.553, -1594.62, 31.5322)); Arbeitsamt.Name = "Arbeitsamt"; Arbeitsamt.ShortRange = true; Arbeitsamt.Sprite = 280;
            Blip Berufskraftfahrer = NAPI.Blip.CreateBlip(new Vector3(-1546.57, 1367.763, 126.1016)); Berufskraftfahrer.Name = "Berufskraftfahrer"; Berufskraftfahrer.ShortRange = true; Berufskraftfahrer.Sprite = 67;
            Blip Berufskraftfahrer_Kraftstoff = NAPI.Blip.CreateBlip(new Vector3(815.2087, -1590.846, 31.01333)); Berufskraftfahrer_Kraftstoff.Name = "Berufskraftfahrer Kraftstoff Ladepunkt"; Berufskraftfahrer_Kraftstoff.ShortRange = true; Berufskraftfahrer_Kraftstoff.Sprite = 67;
            Blip Berufskraftfahrer_Holz_Einladepunkt = NAPI.Blip.CreateBlip(new Vector3(-511.5431, 5241.104, 80.30409)); Berufskraftfahrer_Holz_Einladepunkt.Name = "Berufskraftfahrer Holz Ladepunkt"; Berufskraftfahrer_Holz_Einladepunkt.ShortRange = true; Berufskraftfahrer_Holz_Einladepunkt.Sprite = 67;
            Blip Berufskraftfahrer_Holz_Abladepunkt = NAPI.Blip.CreateBlip(new Vector3(1307.297, 4324.441, 38.20026)); Berufskraftfahrer_Holz_Abladepunkt.Name = "Berufskraftfahrer Holz Abladepunkt"; Berufskraftfahrer_Holz_Abladepunkt.ShortRange = true; Berufskraftfahrer_Holz_Abladepunkt.Sprite = 67;
            Blip Busfahrer = NAPI.Blip.CreateBlip(new Vector3(403.169, -642.016, 28.5002)); Busfahrer.Name = "Busfahrer"; Busfahrer.ShortRange = true; Busfahrer.Sprite = 513;
            Blip Heiraten = NAPI.Blip.CreateBlip(new Vector3(-329.9244, 6150.168, 32.31319)); Heiraten.Name = "Heiraten"; Heiraten.ShortRange = true; Heiraten.Sprite = 621; Heiraten.Color = 1;
            Blip TUEV = NAPI.Blip.CreateBlip(new Vector3(533.015, -178.646, 54.4207)); TUEV.Name = "TÜV"; TUEV.ShortRange = true; TUEV.Sprite = 544;
        }

        public static void MarkersLaden()
        {
            //Noobspawn
            NAPI.Marker.CreateMarker(21, new Vector3(-3237.754, 969.6091, 12.94306), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            NAPI.Marker.CreateMarker(21, new Vector3(-3237.508, 965.3855, 13.04449), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Stadthalle
            NAPI.Marker.CreateMarker(21, new Vector3(337.764, -1562.13, 30.298), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            NAPI.Marker.CreateMarker(21, new Vector3(334.039, -1564.35, 30.3066), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            NAPI.Marker.CreateMarker(21, new Vector3(331.347, -1569.97, 30.3076), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Heiraten
            NAPI.Marker.CreateMarker(21, new Vector3(-329.9244, 6150.168, 32.31319), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Arbeitsamt
            NAPI.Marker.CreateMarker(21, new Vector3(250.553, -1594.62, 31.5322), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            NAPI.Marker.CreateMarker(21, new Vector3(249.744, -1597.58, 25.5466), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            NAPI.Marker.CreateMarker(21, new Vector3(246.22, -1603.7, 25.5601), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Berufskraftfahrer
            NAPI.Marker.CreateMarker(21, new Vector3(-1546.57, 1367.763, 126.1016), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            NAPI.Marker.CreateMarker(21, new Vector3(815.2087, -1590.846, 31.01333), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            NAPI.Marker.CreateMarker(21, new Vector3(-511.5431, 5241.104, 80.30409), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            NAPI.Marker.CreateMarker(21, new Vector3(1307.297, 4324.441, 38.20026), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Busfahrer
            NAPI.Marker.CreateMarker(21, new Vector3(403.169, -642.016, 28.5002), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Busfahrer Route 1
            GlobaleSachen.Route1_1 = NAPI.Marker.CreateMarker(21, new Vector3(306.639, -765.855, 28.7369), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route1_2 = NAPI.Marker.CreateMarker(21, new Vector3(112.742, -783.588, 30.8554), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route1_3 = NAPI.Marker.CreateMarker(21, new Vector3(-244.619, -715.44, 32.9559), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route1_4 = NAPI.Marker.CreateMarker(21, new Vector3(-712.426, -827.37, 22.952), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route1_5 = NAPI.Marker.CreateMarker(21, new Vector3(-740.321, -750.287, 26.2996), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route1_6 = NAPI.Marker.CreateMarker(21, new Vector3(-689.944, -668.301, 30.3546), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route1_7 = NAPI.Marker.CreateMarker(21, new Vector3(-506.123, -668.609, 32.5175), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route1_8 = NAPI.Marker.CreateMarker(21, new Vector3(224.769, -853.519, 29.5364), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Busfahrer Route 2
            GlobaleSachen.Route2_2 = NAPI.Marker.CreateMarker(21, new Vector3(275.028, -585.099, 43.1406), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route2_3 = NAPI.Marker.CreateMarker(21, new Vector3(269.834, -358.579, 44.7709), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route2_4 = NAPI.Marker.CreateMarker(21, new Vector3(334.736, 160.639, 103.246), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route2_5 = NAPI.Marker.CreateMarker(21, new Vector3(-247.98, 30.9539, 56.751), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route2_6 = NAPI.Marker.CreateMarker(21, new Vector3(-499.74, 20.2629, 44.7918), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route2_7 = NAPI.Marker.CreateMarker(21, new Vector3(-688.621, -6.3535, 38.2303), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route2_8 = NAPI.Marker.CreateMarker(21, new Vector3(-753.362, -34.1787, 37.6805), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route2_9 = NAPI.Marker.CreateMarker(21, new Vector3(-927.927, -125.374, 37.5825), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route2_10 = NAPI.Marker.CreateMarker(21, new Vector3(-989.048, -399.978, 37.7187), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Busfahrer Route 3
            GlobaleSachen.Route3_1 = NAPI.Marker.CreateMarker(21, new Vector3(61.4772, -652.598, 31.0374), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route3_2 = NAPI.Marker.CreateMarker(21, new Vector3(-173.5, -820.069, 30.5454), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route3_3 = NAPI.Marker.CreateMarker(21, new Vector3(-1277.57, -1224.05, 3.95632), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route3_4 = NAPI.Marker.CreateMarker(21, new Vector3(-1668.04, -543.493, 34.45), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route3_5 = NAPI.Marker.CreateMarker(21, new Vector3(-1940.37, -305.581, 43.7902), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route3_6 = NAPI.Marker.CreateMarker(21, new Vector3(-1476.76, -632.458, 30.0296), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route3_7 = NAPI.Marker.CreateMarker(21, new Vector3(-1272.07, -560.944, 29.3496), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

            //Busfahrer Route 4
            GlobaleSachen.Route4_2 = NAPI.Marker.CreateMarker(21, new Vector3(-1167.17, -401.918, 34.9337), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route4_3 = NAPI.Marker.CreateMarker(21, new Vector3(-1409.08, -569.181, 29.8156), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);
            GlobaleSachen.Route4_4 = NAPI.Marker.CreateMarker(21, new Vector3(-1516.2, -373.59, 41.7262), new Vector3(), new Vector3(), 0.5f, new Color(255, 0, 0, 100), true, 0);

        }

        [RemoteEvent("PersoSchliessen")]
        public void PersoSchliessen(Client Player)
        {
            Player.SetData("SiehtPerso", 0);
        }

        [RemoteEvent("Personalausweis")]
        public void PersonalausweisStadthalle(Client Player)
        {
            //Cooldown
            if (Player.GetData("MenuCoolDown") == 1) { return; }
            Player.SetData("MenuCoolDown", 1);
            Timer.SetTimer(() => MenuCoolDown(Player), GlobaleSachen.MenuCoolDownZeit, 1);

            //Lokalen Account bekommen
            AccountLokal account = new AccountLokal();
            account = AccountBekommen(Player);

            if (account.Perso == 0)
            {
                if (Funktionen.AccountGeldBekommen(Player) < GlobaleSachen.PersonalausweisPreis)
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast nicht genug Geld.");
                }
                else
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast einen Personalausweis erhalten.");
                    account.Perso = 1;
                    account.AccountGeändert = true;
                    Funktionen.AccountGeldSetzen(Player, 2, GlobaleSachen.PersonalausweisPreis);
                    Player.TriggerEvent("StadthalleWeg");
                }
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast bereits einen Personalausweis.");
            }
        }

        [RemoteEvent("Arbeitsamt_Berufskraftfahrer")]
        public void Arbeitsamt_Berufskraftfahrer(Client Player)
        {
            //Perso
            if(AccountPersoBekommen(Player) == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast keinen gültigen Personalausweis!"); return; }

            //Cooldown
            if (Player.GetData("MenuCoolDown") == 1) { return; }
            Player.SetData("MenuCoolDown", 1);
            Timer.SetTimer(() => MenuCoolDown(Player), GlobaleSachen.MenuCoolDownZeit, 1);

            //Lokalen Account bekommen
            AccountLokal account = new AccountLokal();
            account = AccountBekommen(Player);

            if(account.Job != 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst deinen bisherigen Job erst kündigen!"); return; }

            account.Job = 1;
            account.Kündigungszeit = 60;
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du bist jetzt Berufskraftfahrer.");

            account.AccountGeändert = true;

        }

        [RemoteEvent("Arbeitsamt_Busfahrer")]
        public void Arbeitsamt_Busfahrer(Client Player)
        {
            //Perso
            if (AccountPersoBekommen(Player) == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast keinen gültigen Personalausweis!"); return; }

            //Cooldown
            if (Player.GetData("MenuCoolDown") == 1) { return; }
            Player.SetData("MenuCoolDown", 1);
            Timer.SetTimer(() => MenuCoolDown(Player), GlobaleSachen.MenuCoolDownZeit, 1);

            //Lokalen Account bekommen
            AccountLokal account = new AccountLokal();
            account = AccountBekommen(Player);

            if (account.Job != 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst deinen bisherigen Job erst kündigen!"); return; }

            account.Job = 2;
            account.Kündigungszeit = 60;
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du bist jetzt Busfahrer.");

            account.AccountGeändert = true;

        }

        [RemoteEvent("Arbeitsamt_Kündigen")]
        public void Arbeitsamt_Kündigen(Client Player)
        {
            //Perso
            if (AccountPersoBekommen(Player) == 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast keinen gültigen Personalausweis!"); return; }

            //Cooldown
            if (Player.GetData("MenuCoolDown") == 1) { return; }
            Player.SetData("MenuCoolDown", 1);
            Timer.SetTimer(() => MenuCoolDown(Player), GlobaleSachen.MenuCoolDownZeit, 1);

            //Lokalen Account bekommen
            AccountLokal account = new AccountLokal();
            account = AccountBekommen(Player);

            if (account.Kündigungszeit != 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst deinen Job noch " + account.Kündigungszeit + " Minute(n) ausüben."); return; }

            account.Job = 0;
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du bist jetzt Zivilist.");

            account.AccountGeändert = true;

        }

        [RemoteEvent("AutoAnAus")]
        public void AutoAnAus(Client Player)
        {
            //Freezed?
            if(Player.GetData("Freezed") == 1) { return; }

            //Cooldown
            if (Player.GetData("KeyCoolDown") == 1) { return; }
            Player.SetData("KeyCoolDown", 1);
            Timer.SetTimer(() => KeyCoolDown(Player), GlobaleSachen.KeyCoolDownZeit, 1);

            //Abfragen ob er in einem Auto ist
            if (Player.IsInVehicle && NAPI.Player.GetPlayerVehicleSeat(Player) == -1)
            {
                if (Player.Vehicle.EngineStatus == false && Fahrzeuge.TankInhaltBekommen(Player.Vehicle) > 0)
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Motor wurde gestartet.");
                    Player.Vehicle.EngineStatus = true;
                    Fahrzeuge.FahrzeugMotor(Player.Vehicle, 1);
                    Player.TriggerEvent("Fahrbar");
                }
                else if (Player.Vehicle.EngineStatus == true)
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Motor wurde gestoppt.");
                    Player.Vehicle.EngineStatus = false;
                    Fahrzeuge.FahrzeugMotor(Player.Vehicle, 0);
                    Player.TriggerEvent("NichtFahrbar");
                }
                else
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Tank ist leer.");
                }
            }
            else
            {
                //Wenn er in keinem Wagen ist soll nichts passieren
            }
        }

        [RemoteEvent("SaveListeTeleportieren")]
        public void SaveListeTeleportieren(Client Player, int Id)
        {
            foreach (SaveLokal save in Funktionen.SaveListe)
            {
                if(save.Id == Id)
                {
                    Player.Position = new Vector3(save.PositionX, save.PositionY, save.PositionZ);
                    Player.Rotation = new Vector3(0.0, 0.0, save.PositionRot);
                    Player.TriggerEvent("savelisteschliessen");

                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast dich zum Save Eintrag teleportiert.");
                    break;
                }
            }
        }

        [RemoteEvent("SaveListeLöschen")]
        public void SaveListeLöschen(Client Player, int Id)
        {
            foreach (SaveLokal save in Funktionen.SaveListe)
            {
                if (save.Id == Id)
                {
                    var Save = ContextFactory.Instance.srp_saves.Where(x => x.Id == save.Id).FirstOrDefault();

                    //Query absenden
                    ContextFactory.Instance.srp_saves.Remove(Save);

                    //Saveliste entfernen
                    SaveListe.Remove(save);

                    Player.TriggerEvent("savelisteschliessen");

                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Save Eintrag wurde gelöscht.");
                    break;
                }
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
            if (NAPI.Player.IsPlayerConnected(Player) == false) { return; }

            //Freeze
            Player.TriggerEvent("Freeze");
            Player.SetData("Freezed", 1);
        }

        public static void Unfreeze(Client Player)
        {
            if (NAPI.Player.IsPlayerConnected(Player) == false) { return; }

            //Unfreeze
            Player.TriggerEvent("Unfreeze");
            Player.SetData("Freezed", 0);
        }

        public static void FreezeAuto(Client Player)
        {
            //Freeze
            Player.TriggerEvent("Freeze");
            Player.SetData("Freezed", 1);

            Player.Vehicle.EngineStatus = false;
            Player.Vehicle.FreezePosition = true;
        }

        public static void UnfreezeAuto(Client Player)
        {
            //Unfreeze
            Player.TriggerEvent("Unfreeze");
            Player.SetData("Freezed", 0);

            Player.Vehicle.EngineStatus = true;
            Player.Vehicle.FreezePosition = false;
        }

        public static void FahrzeugMotor(Vehicle vehicle)
        {
            AutoLokal auto = new AutoLokal();
            auto = AutoBekommen(vehicle);

            if (auto.FahrzeugMotor == 0)
            {
                auto.Fahrzeug.EngineStatus = false;
            }
            else
            {
                auto.Fahrzeug.EngineStatus = true;
            }
        }

        public static void KeyCoolDown(Client Player)
        {
            //Cooldown beenden
            Player.SetData("KeyCoolDown", 0);
        }

        public static void MenuCoolDown(Client Player)
        {
            //Cooldown beenden
            Player.SetData("MenuCoolDown", 0);
        }

        public static void LoginLadenBeenden(Client Player)
        {
            Player.TriggerEvent("LadenBeenden");
            Player.TriggerEvent("browseroeffnen");

            var Check = ContextFactory.Instance.srp_accounts.Count(x => x.SocialClub == Player.SocialClubName);

            if(Check == 0)
            {
                Player.TriggerEvent("HatAccount", 0, Player.SocialClubName, 0);
            }
            else
            {
                var Account = ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == Player.SocialClubName).FirstOrDefault();
                Player.TriggerEvent("HatAccount", 1, Player.SocialClubName, Account.NickName);
            }
        }

        public static void SpawnManager(Client Player)
        {
            AccountLokal account = new AccountLokal();
            account = AccountBekommen(Player);

            if(account.Tutorial == 100)
            {
                //Spieler hat das Tutorial bereits abgeschlossen
                Player.Position = new Vector3(account.PositionX, account.PositionY, account.PositionZ);
                Player.Rotation = new Vector3(0.0, 0.0, account.PositionRot);
                Player.Dimension = Convert.ToUInt32(account.Dimension);
                InteriorLaden(Player, account.Interior);
            }
            else
            {
                //Spieler hat das Tutorial noch nicht abgeschlossen
                Player.Position = new Vector3(826.526, -3014.08, 5.90631);
                Player.Rotation = new Vector3(0.0, 0.0, 355.688);
                Player.Dimension = Convert.ToUInt32(account.Dimension);
                InteriorLaden(Player, account.Interior);
            }

            if(account.AdminLevel == 0)
            {
                Player.TriggerEvent("Chathiden");
                Player.SetData("Chat", 0);
            }
            else
            {
                Player.TriggerEvent("Chatzeigen");
                Player.SetData("Chat", 1);
            }
            
            LogEintrag(Player, "Gespawnt (" + account.PositionX + ", " + account.PositionY + ", " + account.PositionZ + ") Dimension: " + account.Dimension + " | Interior: " + account.Interior);

            Player.TriggerEvent("moveSkyCamera", Player, "down");

            Ladebalken(Player, 1, 10000);

            //Unfreeze Timer
            Timer.SetTimer(() => Unfreeze(Player), 10000, 1);
        }

        public static void InteriorLaden(Client Player, String InteriorName)
        {
            LogEintrag(Player, "Interior geladen: " + InteriorName);
            Player.SetData("InteriorName", InteriorName);
            Player.TriggerEvent("InteriorLaden", InteriorName);
        }

        [ServerEvent(Event.PlayerDeath)]
        public void OnPlayerDeath(Client Player, Client Killer, uint Reason)
        {
            Player.TriggerEvent("moveSkyCamera", Player, "up", 1, false);
            LogEintrag(Player, "Gestorben Killer: " + Killer.Name + " | Grund: " + Reason);
            Timer.SetTimer(() => TotRespawn(Player), 30000, 1);
            Ladebalken(Player, 2, 30000);
            Player.Position = new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z - 10);
            Freeze(Player);
        }

        public static void TotRespawn(Client Player)
        {
            if (NAPI.Player.IsPlayerConnected(Player) == false) { return; }

            Player.Health = 100;
            Player.Position = new Vector3(358.764f, -589.17f, 28.8006f);
            Player.Rotation = new Vector3(0.0, 0.0, 270.343f);
            Player.TriggerEvent("moveSkyCamera", Player, "down");
            Unfreeze(Player);
        }

        [RemoteEvent("GruppierungErstellen")]
        public static void GruppierungErstellen(Client Player, String Gruppenname, String Gruppentag, String Gruppenfarbe)
        {
            if(AccountSpielzeitBekommen(Player) < 240) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du benötigst mindestens 4 Spielstunden."); return; }
            if (Funktionen.AccountGeldBekommen(Player) < GlobaleSachen.GruppenPreis){ NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast nicht genug Geld."); return; }
            if (Funktionen.AccountGeldBekommen(Player) < GlobaleSachen.GruppenPreis + GlobaleSachen.GruppenEinlage) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast nicht genug Geld."); return; }
            if (Gruppenname.Length < 5 || Gruppenname.Length > 20) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Gruppenname benötigt min. 4 Zeichen und max. 20 Zeichen."); return; }
            if (Gruppentag.Length < 2 || Gruppentag.Length > 3) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Gruppentag benötigt min. 2 Zeichen und max. 3 Zeichen."); return; }
            if (Gruppenfarbe == "rot" || Gruppenfarbe == "Rot" || Gruppenfarbe == "blau" || Gruppenfarbe == "Blau" || Gruppenfarbe == "grün" || Gruppenfarbe == "Grün" || Gruppenfarbe == "gelb" || Gruppenfarbe == "Gelb" || Gruppenfarbe == "lila" || Gruppenfarbe == "Lila")
            {
                var Check = ContextFactory.Instance.srp_gruppen.Count(x => x.GruppenName == Gruppenname);
                if(Check == 1) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Diese Gruppe gibt es schon!"); return; }

                var Gruppe = new Gruppen
                {
                    GruppenName = Gruppenname,
                    GruppenBesitzer = Player.GetData("Id"),
                    GruppenTag = Gruppentag,
                    GruppenFarbe = ErsterBuchstabeGroß(Gruppenfarbe),
                    GruppenGeld = GlobaleSachen.GruppenEinlage,
                    GruppenRang1Name = "Rang 1",
                    GruppenRang2Name = "Rang 2",
                    GruppenRang3Name = "Rang 3",
                    GruppenRang4Name = "Rang 4",
                    GruppenRang5Name = "Rang 5"

                };

                //Query absenden
                ContextFactory.Instance.srp_gruppen.Add(Gruppe);
                ContextFactory.Instance.SaveChanges();

                GruppenLokal gruppe = new GruppenLokal();

                gruppe.Id = ContextFactory.Instance.srp_gruppen.Max(x => x.Id);
                gruppe.GruppenName = Gruppenname;
                gruppe.GruppenBesitzer = Player.GetData("Id");
                gruppe.GruppenTag = Gruppentag;
                gruppe.GruppenFarbe = ErsterBuchstabeGroß(Gruppenfarbe);
                gruppe.GruppenGeld = GlobaleSachen.GruppenEinlage;
                gruppe.GruppenRang1Name = "Rang 1";
                gruppe.GruppenRang2Name = "Rang 2";
                gruppe.GruppenRang3Name = "Rang 3";
                gruppe.GruppenRang4Name = "Rang 4";
                gruppe.GruppenRang5Name = "Rang 5";

                //Dem Spieler Leader setzen
                AccountGruppeSetzen(Player, ContextFactory.Instance.srp_gruppen.Max(x => x.Id));
                AccountGruppeRangSetzen(Player, 5);
                AccountGeldSetzen(Player, 2, GlobaleSachen.GruppenPreis + GlobaleSachen.GruppenEinlage);

                GruppenListe.Add(gruppe);

                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Gruppierung " + gruppe.GruppenName + " erstellt.");

                SpielerSpeichern(Player);

                Player.TriggerEvent("gruppenerstellenbrowserschliessen");
            }
            else
            {
                NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Diese Farbe ist uns nicht bekannt.");
            }
        }

        [RemoteEvent("GruppeEinladungAblehnen")]
        public static void GruppeEinladungAblehnen(Client Player)
        {
            Player.SetData("GruppenEinladungId", 0);
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast die Einladung abgelehnt.");

            Player.TriggerEvent("invitebrowserschliessen");
        }

        [RemoteEvent("GruppeEinladungAnnehmen")]
        public static void GruppeEinladungAnnehmen(Client Player)
        {
            int Gruppe = Player.GetData("GruppenEinladungId");
            AccountGruppeSetzen(Player, Gruppe);
            Player.SetData("GruppenEinladungId", 0);
            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du bist der Gruppe erfolgreich beigetreten.");

            foreach (var GesuchterSpieler in NAPI.Pools.GetAllPlayers())
            {
                
                if(AccountGruppeBekommen(GesuchterSpieler) == Gruppe)
                {
                    NAPI.Notification.SendNotificationToPlayer(GesuchterSpieler, "~y~Info~w~: ~r~" + Player.Name + " ~w~ist der Gruppe beigetreten!");
                }
            }

            AccountGruppeRangSetzen(Player, 1);
            Player.TriggerEvent("invitebrowserschliessen");
            SpielerSpeichern(Player);

            LogEintrag(Player, "Gruppen Invite angenommen");
        }

        public static void NPCPopupSchliessen(Client Player)
        {
            Player.TriggerEvent("npcpopupschliessen");
            Unfreeze(Player);
        }

        [RemoteEvent("Gruppierung_IBerry")]
        public static void Gruppierung_IBerry(Client Player, int Typ)
        {
            //Daten erfassen
            int Gruppe = AccountGruppeBekommen(Player);
            int Rang = AccountGruppeRangBekommen(Player);

            if(Typ == 1)
            {
                //IBerry schließen und alles
                Player.SetData("IBerry", 0);
                Player.TriggerEvent("IBerryschliessen");
                LogEintrag(Player, "IBerry geschlossen");
            }

            if(Player.GetData("GruppenEinladungId") != 0)
            {
                foreach (GruppenLokal gruppe in GruppenListe)
                {
                    if (Player.GetData("GruppenEinladungId") == gruppe.Id)
                    {
                        Player.TriggerEvent("invitebrowseroeffnen", gruppe.GruppenName);
                        break;
                    }
                }
                return;
            }
            
            if(Gruppe == 0)
            {
                Player.TriggerEvent("gruppenerstellenbrowseroeffnen");
                LogEintrag(Player, "Gruppen erstellen geöffnet");
                return;
            }
            else if (Gruppe != 0)
            {
                if (Rang == 5)
                {
                    Player.TriggerEvent("meinegruppierungbrowseroeffnen");

                    foreach (GruppenLokal gruppe in GruppenListe)
                    {
                        if (Gruppe == gruppe.Id)
                        {
                            //Alles an das CEF übergeben
                            String Tag = null;
                            Tag = "[" + gruppe.GruppenTag + "]";
                            Player.TriggerEvent("GruppenDaten", gruppe.GruppenName, DatenbankSpielerNamenBekommen(gruppe.GruppenBesitzer), Tag, gruppe.GruppenFarbe, GeldFormatieren(gruppe.GruppenGeld), 1);
                            Player.TriggerEvent("RangDaten", gruppe.GruppenRang5Name, gruppe.GruppenRang4Name, gruppe.GruppenRang3Name, gruppe.GruppenRang2Name, gruppe.GruppenRang1Name);

                            foreach (var Account in ContextFactory.Instance.srp_accounts.Where(x => x.Gruppe == Gruppe).OrderByDescending(y => y.GruppenRang).ToList())
                            {
                                Player.TriggerEvent("Mitglieder_Eintragen", Account.Id, Account.NickName, DatumFormatieren(Account.ZuletztOnline), AccountGruppenRangNamenBekommen(Gruppe, Account.GruppenRang), Account.GruppenRang, 1);
                            }
                        }
                    }
                }
                else if(Rang != 5)
                {
                    Player.TriggerEvent("meinegruppierungbrowseroeffnen");

                    foreach (GruppenLokal gruppe in GruppenListe)
                    {
                        if (Gruppe == gruppe.Id)
                        {
                            //Alles an das CEF übergeben
                            String Tag = null;
                            Tag = "[" + gruppe.GruppenTag + "]";
                            Player.TriggerEvent("GruppenDaten", gruppe.GruppenName, DatenbankSpielerNamenBekommen(gruppe.GruppenBesitzer), Tag, gruppe.GruppenFarbe, GeldFormatieren(gruppe.GruppenGeld), 0);

                            foreach (var Account in ContextFactory.Instance.srp_accounts.Where(x => x.Gruppe == Gruppe).OrderByDescending(y => y.GruppenRang).ToList())
                            {
                                Player.TriggerEvent("Mitglieder_Eintragen", Account.Id, Account.NickName, DatumFormatieren(Account.ZuletztOnline), AccountGruppenRangNamenBekommen(Gruppe, Account.GruppenRang), Account.GruppenRang, 0);
                            }
                        }
                    }
                }

                //Log Eintrag
                LogEintrag(Player, "Meine Gruppe geöffnet");
            }
        }

        [RemoteEvent("GruppenRaengeSpeichern")]
        public static void GruppenRaengeSpeichern(Client Player, String Rang5, String Rang4, String Rang3, String Rang2, String Rang1)
        {
            if(Rang1.Length < 3 || Rang1.Length > 12 || Rang2.Length < 3 || Rang2.Length > 12 || Rang3.Length < 3 || Rang3.Length > 12 || Rang4.Length < 3 || Rang4.Length > 12 || Rang5.Length < 3 || Rang5.Length > 12) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Ein Rangname benötigt min. 3 und max. 12 Zeichen."); return; }
            int Gruppe = AccountGruppeBekommen(Player);

            foreach (GruppenLokal gruppe in GruppenListe)
            {
                if (Gruppe == gruppe.Id)
                {
                    gruppe.GruppenRang1Name = Rang1;
                    gruppe.GruppenRang2Name = Rang2;
                    gruppe.GruppenRang3Name = Rang3;
                    gruppe.GruppenRang4Name = Rang4;
                    gruppe.GruppenRang5Name = Rang5;

                    gruppe.GruppeGeändert = true;
                }
            }

            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Die Ränge wurden gespeichert.");
            Player.TriggerEvent("meinegruppierungbrowserschliessen");
            Gruppierung_IBerry(Player, 0);

            //Log Eintrag
            LogEintrag(Player, "Gruppen Ränge geändert");
        }

        [RemoteEvent("GruppenSpielerSpeichern")]
        public static void GruppenSpielerSpeichern(Client Player, int SpielerID, int Rang)
        {
            if(Rang < 1 || Rang > 5) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Rang muss zwischen 1 und 5 sein.");  return; }

            //Benötigte Definitionen
            Client Spieler = null;
            String Name = null;

            foreach (var GesuchterSpieler in NAPI.Pools.GetAllPlayers())
            {
                if(GesuchterSpieler.GetData("Id") == SpielerID)
                {
                    Spieler = GesuchterSpieler;
                }
            }

            if(Spieler == null)
            {
                var Account = ContextFactory.Instance.srp_accounts.Where(x => x.Id == SpielerID).FirstOrDefault();
                Account.GruppenRang = Rang;

                //Nickname holen
                Name = Account.NickName;

                //Speichern
                ContextFactory.Instance.SaveChanges();
            }
            else
            {
                AccountGruppeRangSetzen(Spieler, Rang);
                Name = Spieler.Name;
                NAPI.Notification.SendNotificationToPlayer(Spieler, "~y~Info~w~: ~r~" + Player.Name + " ~w~hat deinen Rang auf ~r~" + Rang + " ~w~geändert.");
                SpielerSpeichern(Spieler);
            }

            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast den Rang von ~r~" + Name + " ~w~auf ~r~" + Rang + " ~w~geändert.");
            Player.TriggerEvent("meinegruppierungbrowserschliessen");
            Gruppierung_IBerry(Player, 0);

            //Log Eintrag
            LogEintrag(Player, "Gruppe Spieler Rang geändert");
        }

        [RemoteEvent("GruppeSpielerInviten")]
        public static void GruppeSpielerInviten(Client Player, String Name)
        {
            //Den Spieler über den Namen ermitteln
            Client Spieler1 = NAPI.Player.GetPlayerFromName(Name);
            if (Spieler1 == null)
            {
                if (Funktionen.SpielerSuchen(Name) == null)
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieser Spieler ist uns nicht bekannt. Er muss online sein.");
                    return;
                }
                else
                {
                    Spieler1 = Funktionen.SpielerSuchen(Name);
                }
            }

            var Count = ContextFactory.Instance.srp_accounts.Count(x => x.Gruppe == AccountGruppeBekommen(Spieler1));
            if (Count >= GlobaleSachen.GruppenMemberLimit) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Es sind maximal " + GlobaleSachen.GruppenMemberLimit + " möglich!"); return; }
            if (Spieler1.GetData("GruppenEinladungId") != 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieser Spieler wurde bereits in eine Gruppe eingeladen.");  return; }
            if(AccountGruppeBekommen(Spieler1) != 0) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieser Spieler kann keiner weiteren Gruppe beitreten."); return; }

            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast ~r~" + Spieler1.Name + " ~w~erfolgreich eingeladen.");
            NAPI.Notification.SendNotificationToPlayer(Spieler1, "~y~Info~w~: Du hast eine Gruppeneinladung erhalten. (F8 -> Gruppierung)");

            Spieler1.SetData("GruppenEinladungId", AccountGruppeBekommen(Player));

            //Log Eintrag
            LogEintrag(Player, "Gruppe Spieler invitet");
        }

        public static void Tutorial(Client Player)
        {
            //Einreise Bot
            if (Player.Position.DistanceTo(new Vector3(815.822, -3001.06, 6.02094)) < 5.0f)
            {
                if (Player.GetData("EinreiseNPC") == 0 && AccountTutorialBekommen(Player) == 0)
                {
                    Player.TriggerEvent("npcpopupoeffnen", "Mitarbeiter des auswärtigen Amts", GlobaleSachen.EinreiseNPCText);
                    Freeze(Player);
                    Timer.SetTimer(() => NPCPopupSchliessen(Player), 35000, 1);
                    Player.SetData("EinreiseNPC", 1);
                    AccountTutorialSetzen(Player, 1);
                }
            }
            //Helmut
            else if (Player.Position.DistanceTo(new Vector3(793.214, -3021.96, 6.02094)) < 5.0f)
            {
                if (Player.GetData("HelmutNPC") == 0 && AccountTutorialBekommen(Player) == 1 && HatTutorialFahrzeug(Player) == 0)
                {
                    Player.TriggerEvent("npcpopupoeffnen", "Helmut", GlobaleSachen.HelmutNPCText);
                    Freeze(Player);
                    Timer.SetTimer(() => NPCPopupSchliessen(Player), 45000, 1);
                    Timer.SetTimer(() => Fahrzeuge.HelmutFahrzeugErstellen(Player), 45000, 1);
                    Player.SetData("HelmutNPC", 1);
                    AccountTutorialSetzen(Player, 2);
                }
            }
        }

        public static String DatumFormatieren(DateTime Datum)
        {
            String DatumFormatiert = null;

            DateTime Datum1 = Datum;
            DatumFormatiert = Datum1.ToString("dd.MM.yyyy HH:mm");

            return DatumFormatiert;
        }

        [RemoteEvent("GruppeSpielerUninviten")]
        public static void GruppeSpielerUninviten(Client Player, int SpielerID)
        {
            //Benötigte Definitionen
            Client Spieler = null;
            String Name = null;

            foreach (var GesuchterSpieler in NAPI.Pools.GetAllPlayers())
            {
                if (GesuchterSpieler.GetData("Id") == SpielerID)
                {
                    Spieler = GesuchterSpieler;
                }
            }

            if (Spieler == null)
            {
                var Account = ContextFactory.Instance.srp_accounts.Where(x => x.Id == SpielerID).FirstOrDefault();
                Account.Gruppe = 0;

                //Nickname holen
                Name = Account.NickName;

                //Speichern
                ContextFactory.Instance.SaveChanges();
            }
            else
            {
                AccountGruppeSetzen(Spieler, 0);
                Name = Spieler.Name;
                NAPI.Notification.SendNotificationToPlayer(Spieler, "~y~Info~w~: Du wurdest von ~r~" + Player.Name + " ~w~aus der Gruppe gekickt.");

                SpielerSpeichern(Spieler);
            }

            NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du hast den ~r~" + Name + " ~w~aus der Gruppe gekickt.");
            Player.TriggerEvent("meinegruppierungbrowserschliessen");
            Gruppierung_IBerry(Player, 0);

            //Log Eintrag
            LogEintrag(Player, "Gruppe Spieler uninvited");
        }

        public static String DatenbankSpielerNamenBekommen(int Id)
        {
            String Name = null;
            var Account = ContextFactory.Instance.srp_accounts.Where(x => x.Id == Id).FirstOrDefault();
            Name = Account.NickName;

            return Name;
        }

        [RemoteEvent("KleidungSetzen")]
        public static void KleidungSetzen(Client Player, int componentId, int drawable, int texture)
        {
            AccountLokal account = new AccountLokal();
            account = AccountBekommen(Player);

            if(componentId == 1) { account.Component1Drawable = drawable; }
            else if (componentId == 3) { account.Component3Drawable = drawable; }
            else if (componentId == 4) { account.Component4Drawable = drawable; }
            else if (componentId == 6) { account.Component6Drawable = drawable; }
            else if (componentId == 7) { account.Component7Drawable = drawable; }
            else if (componentId == 8) { account.Component8Drawable = drawable; }
            else if (componentId == 11) { account.Component11Drawable = drawable; }

            account.AccountGeändert = true;
        }
    }
}
