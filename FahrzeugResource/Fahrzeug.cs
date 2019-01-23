using GTANetworkAPI;
using Datenbank;
using Haupt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fahrzeug
{
    public class Fahrzeuge
    {
        public static void FahrzeugeSpawnen()
        {
            foreach (var Fahrzeuge in ContextFactory.Instance.srp_fahrzeuge.Where(x => x.Id > 0).ToList())
            {
                //Definitionen
                uint AutoCode = NAPI.Util.GetHashKey(Fahrzeuge.FahrzeugName);
                Vehicle Auto = null;

                //Die eigentliche Erstellung des Fahrzeuges
                Auto = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Fahrzeuge.FahrzeugX, Fahrzeuge.FahrzeugY, Fahrzeuge.FahrzeugZ), Fahrzeuge.FahrzeugRot, Fahrzeuge.FahrzeugFarbe1, Fahrzeuge.FahrzeugFarbe2);

                //Zuweisungen für das Auto
                Auto.NumberPlate = Fahrzeuge.FahrzeugBeschreibung;
                Auto.EngineStatus = false;

                //Daten Lokal setzen damit man diese weiterverarbeiten kann
                Auto.SetData("Id", Fahrzeuge.Id);
                Auto.SetData("FahrzeugBeschreibung", Fahrzeuge.FahrzeugBeschreibung);
                Auto.SetData("FahrzeugName", Fahrzeuge.FahrzeugName);
                Auto.SetData("FahrzeugTyp", Fahrzeuge.FahrzeugTyp);
                Auto.SetData("FahrzeugFraktion", Fahrzeuge.FahrzeugFraktion);
                Auto.SetData("FahrzeugJob", Fahrzeuge.FahrzeugJob);
                Auto.SetData("FahrzeugSpieler", Fahrzeuge.FahrzeugSpieler);
                Auto.SetData("FahrzeugMietpreis", Fahrzeuge.FahrzeugMietpreis);
                Auto.SetData("FahrzeugX", Fahrzeuge.FahrzeugX);
                Auto.SetData("FahrzeugY", Fahrzeuge.FahrzeugY);
                Auto.SetData("FahrzeugZ", Fahrzeuge.FahrzeugZ);
                Auto.SetData("FahrzeugRot", Fahrzeuge.FahrzeugRot);
                Auto.SetData("FahrzeugFarbe1", Fahrzeuge.FahrzeugFarbe1);
                Auto.SetData("FahrzeugFarbe2", Fahrzeuge.FahrzeugFarbe2);
                Auto.SetData("TankVolumen", Fahrzeuge.TankVolumen);
                Auto.SetData("TankInhalt", Fahrzeuge.TankInhalt);
                Auto.SetData("Kilometerstand", Fahrzeuge.Kilometerstand);
            }
        }
       
    }
}
