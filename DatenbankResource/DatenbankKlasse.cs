using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Datenbank
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        public string SocialClub { get; set; }
        public string NickName { get; set; }
        public string Passwort { get; set; }
        public int AdminLevel { get; set; }
    }

    public class Log
    {
        [Key]
        public int Id { get; set; }

        public string Aktion { get; set; }
        public string SocialClub { get; set; }
        public DateTime Wann { get; set; }
    }

    public class Auto
    {
        [Key]
        public int Id { get; set; }

        public string FahrzeugBeschreibung { get; set; }
        public string FahrzeugName { get; set; }
        public int FahrzeugTyp { get; set; }
        public float FahrzeugX { get; set; }
        public float FahrzeugY { get; set; }
        public float FahrzeugZ { get; set; }
        public float FahrzeugRot { get; set; }
        public int FahrzeugFarbe1 { get; set; }
        public int FahrzeugFarbe2 { get; set; }
        public int TankVolumen { get; set; }
        public int TankInhalt { get; set; }
        public double Kilometerstand { get; set; }

    }
}