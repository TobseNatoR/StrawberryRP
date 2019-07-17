/************************************************************************************************************
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        @@ Strawberry Roleplay Gamemode                                                                   @@
        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
*************************************************************************************************************/

using System;
using GTANetworkAPI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Datenbank
{
    public class Server
    {
        [Key]
        public int Id { get; set; }

        public int Wetter { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WetterAktualisiert { get; set; }
        public long Staatskasse { get; set; }
        public int OnlineRekord { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OnlineRekordDatum { get; set; }
        public int Online { get; set; }
    }

    public class Whitelist
    {
        [Key]
        public int Id { get; set; }

        public string SocialClub { get; set; }
    }

    public class Account
    {
        //Immer wenn was verändert wird auch das Objekt anpassen beim registrieren!
        //Einmal bei RegistrierenVersuch
        //Und bei SpielerLaden

        [Key]
        public int Id { get; set; }

        public string SocialClub { get; set; }
        public string NickName { get; set; }
        public string Passwort { get; set; }
        public int AdminLevel { get; set; }
        public int Fraktion { get; set; }
        public int FraktionRang { get; set; }
        public int Job { get; set; }
        public long Geld { get; set; }
        public long BankGeld { get; set; }
        public int Perso { get; set; }
        public int Spielzeit { get; set; }
        public int Exp { get; set; }
        public int Gruppe { get; set; }
        public int GruppenRang { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime GeburtsDatum { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EinreiseDatum { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ZuletztOnline { get; set; }
        public String Verheiratet { get; set; }
        public int FahrzeugSchlüssel { get; set; }
        public int Kündigungszeit { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float PositionRot { get; set; }
        public Int64 Dimension { get; set; }
        public string Interior { get; set; }
        public int Component1Drawable { get; set; }
        public int Component3Drawable { get; set; }
        public int Component4Drawable { get; set; }
        public int Component6Drawable { get; set; }
        public int Component7Drawable { get; set; }
        public int Component8Drawable { get; set; }
        public int Component11Drawable { get; set; }
        public int BerufskraftfahrerExp { get; set; }
        public int Tutorial { get; set; }
    }

    public class AccountLokal
    {
        [Key]
        public int Id { get; set; }

        public string SocialClub { get; set; }
        public string NickName { get; set; }
        public string Passwort { get; set; }
        public int AdminLevel { get; set; }
        public int Fraktion { get; set; }
        public int FraktionRang { get; set; }
        public int Job { get; set; }
        public long Geld { get; set; }
        public long BankGeld { get; set; }
        public int Perso { get; set; }
        public int Spielzeit { get; set; }
        public int Exp { get; set; }
        public int Gruppe { get; set; }
        public int GruppenRang { get; set; }
        public DateTime GeburtsDatum { get; set; }
        public DateTime EinreiseDatum { get; set; }
        public DateTime ZuletztOnline { get; set; }
        public String Verheiratet { get; set; }
        public int FahrzeugSchlüssel { get; set; }
        public int Kündigungszeit { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float PositionRot { get; set; }
        public Int64 Dimension { get; set; }
        public string Interior { get; set; }
        public int Component1Drawable { get; set; }
        public int Component3Drawable { get; set; }
        public int Component4Drawable { get; set; }
        public int Component6Drawable { get; set; }
        public int Component7Drawable { get; set; }
        public int Component8Drawable { get; set; }
        public int Component11Drawable { get; set; }
        public int BerufskraftfahrerExp { get; set; }
        public int Tutorial { get; set; }

        public Vehicle JobFahrzeug { get; set; }
        public Boolean AccountGeändert { get; set; }
    }

    public class Log
    {
        [Key]
        public int Id { get; set; }

        public string Aktion { get; set; }
        public string SocialClub { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Wann { get; set; }
    }

    public class Bankautomaten
    {
        [Key]
        public int Id { get; set; }

        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
    }

    public class BankautomatenLokal
    {
        [Key]
        public int Id { get; set; }

        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }

        public TextLabel ATMText { get; set; }
        public Marker ATMMarker { get; set; }
        public Blip ATMBlip { get; set; }
    }

    public class Fahrzeugvermietungen
    {
        [Key]
        public int Id { get; set; }

        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
    }

    public class FahrzeugvermietungenLokal
    {
        [Key]
        public int Id { get; set; }

        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }

        public TextLabel FVermietungText { get; set; }
        public Marker FVermietungMarker { get; set; }
        public Blip FVermietungBlip { get; set; }
    }

    public class Save
    {
        [Key]
        public int Id { get; set; }

        public string Beschreibung { get; set; }
        public string Von { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float PositionRot { get; set; }
    }

    public class SaveLokal
    {
        [Key]
        public int Id { get; set; }

        public string Beschreibung { get; set; }
        public string Von { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float PositionRot { get; set; }
    }

    public class Immobilien
    {
        [Key]
        public int Id { get; set; }

        public String ImmobilienBeschreibung { get; set; }
        public int ImmobilienBesitzer { get; set; }
        public long ImmobilienGeld { get; set; }
        public int ImmobilienAbgeschlossen { get; set; }
        public long ImmobilienKaufpreis { get; set; }
        public String ImmobilienInteriorName { get; set; }
        public float ImmobilienX { get; set; }
        public float ImmobilienY { get; set; }
        public float ImmobilienZ { get; set; }
        public float ImmobilienEingangX { get; set; }
        public float ImmobilienEingangY { get; set; }
        public float ImmobilienEingangZ { get; set; }
    }

    public class ImmobilienLokal
    {
        public int Id { get; set; }

        public String ImmobilienBeschreibung { get; set; }
        public int ImmobilienBesitzer { get; set; }
        public long ImmobilienGeld { get; set; }
        public int ImmobilienAbgeschlossen { get; set; }
        public long ImmobilienKaufpreis { get; set; }
        public String ImmobilienInteriorName { get; set; }
        public float ImmobilienX { get; set; }
        public float ImmobilienY { get; set; }
        public float ImmobilienZ { get; set; }
        public float ImmobilienEingangX { get; set; }
        public float ImmobilienEingangY { get; set; }
        public float ImmobilienEingangZ { get; set; }
        public TextLabel ImmobilienLabel { get; set; }
        public Marker ImmobilienMarker { get; set; }
        public Blip ImmobilienBlip { get; set; }
        public ColShape ImmobilienColShape { get; set; }

        public Boolean ImmobilieGeändert { get; set; }
    }

    public class Tankstelle
    {
        [Key]
        public int Id { get; set; }

        public string TankstelleBeschreibung { get; set; }
        public int TankstelleBesitzer { get; set; }
        public long TankstelleGeld { get; set; }
        public long TankstelleKaufpreis { get; set; }
        public int TankstelleDiesel { get; set; }
        public int TankstelleE10 { get; set; }
        public int TankstelleSuper { get; set; }
        public int TankstelleDieselPreis { get; set; }
        public int TankstelleE10Preis { get; set; }
        public int TankstelleSuperPreis { get; set; }
        public float TankstelleX { get; set; }
        public float TankstelleY { get; set; }
        public float TankstelleZ { get; set; }
    }

    public class TankstelleLokal
    {
        public int Id { get; set; }

        public string TankstelleBeschreibung { get; set; }
        public int TankstelleBesitzer { get; set; }
        public long TankstelleGeld { get; set; }
        public long TankstelleKaufpreis { get; set; }
        public int TankstelleDiesel { get; set; }
        public int TankstelleE10 { get; set; }
        public int TankstelleSuper { get; set; }
        public int TankstelleDieselPreis { get; set; }
        public int TankstelleE10Preis { get; set; }
        public int TankstelleSuperPreis { get; set; }
        public float TankstelleX { get; set; }
        public float TankstelleY { get; set; }
        public float TankstelleZ { get; set; }
        public TextLabel TankstellenLabel { get; set; }
        public Marker TankstellenMarker { get; set; }
        public Blip TankstellenBlip { get; set; }

        //Job Kram
        public int TankstelleJobSpieler { get; set; }

        public Boolean TankstelleGeändert { get; set; }
    }


    public class TankstellenPunkt
    {
        [Key]
        public int Id { get; set; }

        public int TankstellenId { get; set; }
        public float TankstellenPunktX { get; set; }
        public float TankstellenPunktY { get; set; }
        public float TankstellenPunktZ { get; set; }
    }

    public class TankstellenPunktLokal
    {
        public int Id { get; set; }

        public int TankstellenId { get; set; }
        public float TankstellenPunktX { get; set; }
        public float TankstellenPunktY { get; set; }
        public float TankstellenPunktZ { get; set; }
        public TextLabel TankstellenPunktLabel { get; set; }
        public Marker TankstellenPunktMarker { get; set; }
    }

    public class TankstellenInfo
    {
        [Key]
        public int Id { get; set; }

        public int TankstellenInfoId { get; set; }
        public float TankstellenInfoX { get; set; }
        public float TankstellenInfoY { get; set; }
        public float TankstellenInfoZ { get; set; }
    }

    public class TankstellenInfoLokal
    {
        public int Id { get; set; }

        public int TankstellenInfoId { get; set; }
        public float TankstellenInfoX { get; set; }
        public float TankstellenInfoY { get; set; }
        public float TankstellenInfoZ { get; set; }
        public TextLabel TankstellenInfoLabel { get; set; }
        public Marker TankstellenInfoMarker { get; set; }
    }

    public class Auto
    {
        [Key]
        public int Id { get; set; }

        public string FahrzeugBeschreibung { get; set; }
        public string FahrzeugName { get; set; }
        public int FahrzeugTyp { get; set; }
        public int FahrzeugFraktion { get; set; }
        public int FahrzeugJob { get; set; }
        public int FahrzeugSpieler { get; set; }
        public long FahrzeugMietpreis { get; set; }
        public long FahrzeugKaufpreis { get; set; }
        public int FahrzeugAutohaus { get; set; }
        public float FahrzeugX { get; set; }
        public float FahrzeugY { get; set; }
        public float FahrzeugZ { get; set; }
        public float FahrzeugRot { get; set; }
        public int FahrzeugFarbe1 { get; set; }
        public int FahrzeugFarbe2 { get; set; }
        public float TankVolumen { get; set; }
        public float TankInhalt { get; set; }
        public float Kilometerstand { get; set; }
        public int KraftstoffArt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FahrzeugHU { get; set; }
        public int FahrzeugAbgeschlossen { get; set; }
        public int FahrzeugMotor { get; set; }
        public int FahrzeugGespawnt { get; set; }

    }

    public class AutoLokal
    {
        [Key]
        public int Id { get; set; }

        public string FahrzeugBeschreibung { get; set; }
        public string FahrzeugName { get; set; }
        public int FahrzeugTyp { get; set; }
        public int FahrzeugFraktion { get; set; }
        public int FahrzeugJob { get; set; }
        public int FahrzeugSpieler { get; set; }
        public long FahrzeugMietpreis { get; set; }
        public long FahrzeugKaufpreis { get; set; }
        public int FahrzeugAutohaus { get; set; }
        public float FahrzeugX { get; set; }
        public float FahrzeugY { get; set; }
        public float FahrzeugZ { get; set; }
        public float FahrzeugRot { get; set; }
        public int FahrzeugFarbe1 { get; set; }
        public int FahrzeugFarbe2 { get; set; }
        public float TankVolumen { get; set; }
        public float TankInhalt { get; set; }
        public float Kilometerstand { get; set; }
        public int KraftstoffArt { get; set; }
        public DateTime FahrzeugHU { get; set; }
        public int FahrzeugAbgeschlossen { get; set; }
        public int FahrzeugMotor { get; set; }
        public int FahrzeugGespawnt { get; set; }
        public Vehicle Fahrzeug { get; set; }
        public float FahrzeugAltePositionX { get; set; }
        public float FahrzeugAltePositionY { get; set; }
        public float FahrzeugAltePositionZ { get; set; }
        public float FahrzeugNeuePositionX { get; set; }
        public float FahrzeugNeuePositionY { get; set; }
        public float FahrzeugNeuePositionZ { get; set; }
        public TextLabel AutohausTextLabel { get; set; }

        public Boolean FahrzeugGeändert { get; set; }

    }

    public class Supermarkt
    {
        [Key]
        public int Id { get; set; }

        public String SupermarktBeschreibung { get; set; }
        public int SupermarktBesitzer { get; set; }
        public long SupermarktGeld { get; set; }
        public long SupermarktKaufpreis { get; set; }
        public float SupermarktX { get; set; }
        public float SupermarktY { get; set; }
        public float SupermarktZ { get; set; }
    }

    public class SupermarktLokal
    {
        [Key]
        public int Id { get; set; }

        public String SupermarktBeschreibung { get; set; }
        public int SupermarktBesitzer { get; set; }
        public long SupermarktGeld { get; set; }
        public long SupermarktKaufpreis { get; set; }
        public float SupermarktX { get; set; }
        public float SupermarktY { get; set; }
        public float SupermarktZ { get; set; }
        public TextLabel SupermarktLabel { get; set; }
        public Marker SupermarktMarker { get; set; }
        public Blip SupermarktBlip { get; set; }

        public Boolean SupermarktGeändert { get; set; }
    }

    public class Autohaus
    {
        [Key]
        public int Id { get; set; }

        public String AutohausBeschreibung { get; set; }
        public int AutohausBesitzer { get; set; }
        public long AutohausGeld { get; set; }
        public long AutohausKaufpreis { get; set; }
        public float AutohausX { get; set; }
        public float AutohausY { get; set; }
        public float AutohausZ { get; set; }
    }

    public class AutohausLokal
    {
        [Key]
        public int Id { get; set; }

        public String AutohausBeschreibung { get; set; }
        public int AutohausBesitzer { get; set; }
        public long AutohausGeld { get; set; }
        public long AutohausKaufpreis { get; set; }
        public float AutohausX { get; set; }
        public float AutohausY { get; set; }
        public float AutohausZ { get; set; }
        public TextLabel AutohausLabel { get; set; }
        public Marker AutohausMarker { get; set; }
        public Blip AutohausBlip { get; set; }

        public Boolean AutohausGeändert { get; set; }
    }

    public class Bot
    {
        [Key]
        public int Id { get; set; }

        public String BotName { get; set; }
        public String BotBeschreibung { get; set; }
        public float BotX { get; set; }
        public float BotY { get; set; }
        public float BotZ { get; set; }
        public float BotKopf { get; set; }
        public uint BotDimension { get; set; }
    }

    public class BotLokal
    {
        [Key]
        public int Id { get; set; }

        public String BotName { get; set; }
        public String BotBeschreibung { get; set; }
        public float BotX { get; set; }
        public float BotY { get; set; }
        public float BotZ { get; set; }
        public float BotKopf { get; set; }
        public uint BotDimension { get; set; }
        public Ped Bot { get; set; }

        public Boolean BotGeändert { get; set; }
    }

    public class Gruppen
    {
        [Key]
        public int Id { get; set; }

        public string GruppenName { get; set; }
        public int GruppenBesitzer { get; set; }
        public string GruppenTag { get; set; }
        public string GruppenFarbe { get; set; }
        public long GruppenGeld { get; set; }
        public string GruppenRang1Name { get; set; }
        public string GruppenRang2Name { get; set; }
        public string GruppenRang3Name { get; set; }
        public string GruppenRang4Name { get; set; }
        public string GruppenRang5Name { get; set; }
    }

    public class GruppenLokal
    {
        [Key]
        public int Id { get; set; }

        public string GruppenName { get; set; }
        public int GruppenBesitzer { get; set; }
        public string GruppenTag { get; set; }
        public string GruppenFarbe { get; set; }
        public long GruppenGeld { get; set; }
        public string GruppenRang1Name { get; set; }
        public string GruppenRang2Name { get; set; }
        public string GruppenRang3Name { get; set; }
        public string GruppenRang4Name { get; set; }
        public string GruppenRang5Name { get; set; }

        public Boolean GruppeGeändert { get; set; }
    }

    public class Fraktionen
    {
        [Key]
        public int Id { get; set; }

        public string FraktionName { get; set; }
        public long FraktionGeld { get; set; }
        public string FraktionRang1Name { get; set; }
        public string FraktionRang2Name { get; set; }
        public string FraktionRang3Name { get; set; }
        public string FraktionRang4Name { get; set; }
        public string FraktionRang5Name { get; set; }
    }

    public class RandomSpawns
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float RotZ { get; set; }
    }
}