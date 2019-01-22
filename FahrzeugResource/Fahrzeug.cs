﻿using GTANetworkAPI;
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
                uint AutoCode = NAPI.Util.GetHashKey(Fahrzeuge.FahrzeugName);
                Vehicle Auto = null;
                Auto = NAPI.Vehicle.CreateVehicle(AutoCode, new Vector3(Fahrzeuge.FahrzeugX, Fahrzeuge.FahrzeugY, Fahrzeuge.FahrzeugZ), Fahrzeuge.FahrzeugRot, Fahrzeuge.FahrzeugFarbe1, Fahrzeuge.FahrzeugFarbe2);
                Auto.NumberPlate = Fahrzeuge.FahrzeugBeschreibung;
                Auto.EngineStatus = false;

                Auto.SetData("ID", Fahrzeuge.Id);
            }
        }
       
    }
}
