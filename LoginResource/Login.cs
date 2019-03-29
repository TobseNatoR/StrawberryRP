using GTANetworkAPI;
using Datenbank;
using Haupt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Login
{
    public class Event : Script
    {
        [RemoteEvent("LoginVersuch")]
        public void LoginVersuch(Client player, string passwort)
        {
            var Check = ContextFactory.Instance.srp_accounts.Count(x => x.SocialClub == player.SocialClubName);
            if (Check == 0)
            {
                NAPI.Notification.SendNotificationToPlayer(player, "~y~Info~w~: Der Name " + player.SocialClubName + " ist noch nicht bei uns registriert!");
            }
            else
            {
                foreach (var Account in ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == player.SocialClubName).ToList())
                {
                    if (GetMD5Hash(passwort) == Account.Passwort)
                    {
                        player.TriggerEvent("browserschliessen");

                        Funktionen.LogEintrag(player, "Eingeloggt");
                        if(Account.NickName == "Keiner")
                        {
                            player.TriggerEvent("nicknamebrowseroeffnen");
                        }
                        else if(Account.GeburtsDatum == DateTime.Parse("01/01/1900"))
                        {
                            player.TriggerEvent("geburtstagbrowseroeffnen");
                        }
                        else
                        {
                            Funktionen.SpielerLaden(player);
                            Funktionen.SpawnManager(player);
                            player.TriggerEvent("kameraoff");
                        }

                        NAPI.Player.SetPlayerName(player, Account.NickName);
                    }
                    else
                    {
                        NAPI.Notification.SendNotificationToPlayer(player, "~y~Info~w~: ~w~Dieses Passwort scheint nicht zu stimmen!");
                    }
                }
            }
        }

        [RemoteEvent("RegistrierenVersuch")]
        public void RegistrierenVersuch(Client player, string passwort)
        {

            var Check = ContextFactory.Instance.srp_accounts.Count(x => x.SocialClub == player.SocialClubName);
            {
                //Prüfen ob der Social Club Name bereits registriert ist
                if(Check > 0)
                {
                    NAPI.Notification.SendNotificationToPlayer(player, "~y~Info~w~: Der Name " + player.SocialClubName + " ist bereits bei uns registriert!");
                }
                else
                {
                    //Passwortlänge prüfen
                    if(passwort.Length < 6) { NAPI.Notification.SendNotificationToPlayer(player, "~y~Info~w~: Das Passwort sollte minimum 6 Zeichen haben!"); return;  }
                    if (passwort.Contains(" ")) { NAPI.Notification.SendNotificationToPlayer(player, "~y~Info~w~: Leerzeichen sind ungültig!"); return; }

                    var NeuerAccount = new Account
                    {
                        SocialClub = player.SocialClubName,
                        NickName = "Keiner",
                        Passwort = GetMD5Hash(passwort),
                        AdminLevel = 5,
                        Fraktion = 0,
                        Job = 0,
                        Geld = 100,
                        BankGeld = 0,
                        Perso = 0,
                        Spielzeit = 0,
                        Exp = 0,
                        Gruppe = 0,
                        GeburtsDatum = DateTime.Parse("01/01/1900"),
                        EinreiseDatum = DateTime.Now,
                        ZuletztOnline = DateTime.Now,
                        Verheiratet = "Nein",
                        FahrzeugSchlüssel = 0,
                        Kündigungszeit = 0,
                        PositionX = -3260.276f,
                        PositionY = 967.3442f,
                        PositionZ = 8.832886f,
                        PositionRot = 270.343f,
                        Dimension = 0,
                        Interior = "0",
                        BerufskraftfahrerExp = 0
                    };

                    //Query absenden
                    ContextFactory.Instance.srp_accounts.Add(NeuerAccount);
                    ContextFactory.Instance.SaveChanges();

                    Funktionen.LogEintrag(player, "Registriert");

                    player.TriggerEvent("browserschliessen");
                    player.TriggerEvent("nicknamebrowseroeffnen");

                    NAPI.Notification.SendNotificationToPlayer(player, "~y~Info~w~: Du hast dich erfolgreich als " + player.SocialClubName + " registriert!");
                }
            }
        }

        public static string GetMD5Hash(string TextToHash)
        {
          //Prüfen ob Daten übergeben wurden.
          if((TextToHash == null) || (TextToHash.Length == 0))
          {
            return string.Empty;
          }

          //MD5 Hash aus dem String berechnen. Dazu muss der string in ein Byte[]
          //zerlegt werden. Danach muss das Resultat wieder zurück in ein string.
          MD5 md5 = new MD5CryptoServiceProvider();
          byte[] textToHash = Encoding.Default.GetBytes (TextToHash);
          byte[] result = md5.ComputeHash(textToHash); 

          return System.BitConverter.ToString(result); 
        } 

        [RemoteEvent("NicknameVersuch")]
        public void NicknameVersuch(Client Player, String nickname)
        {

            var Check = ContextFactory.Instance.srp_accounts.Count(x => x.NickName == nickname);
            {
                //Prüfen ob der Nickname bereits vorhanden ist
                if (Check > 0)
                {
                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Name" + nickname + " wird bereits bei uns verwendet!");
                }
                else
                {
                    if (nickname.Length < 4) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Nickname sollte mindestens 4 Zeichen haben!"); return; }
                    if(nickname.Contains(" ")) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Dieser Nickname ist nicht möglich!"); return; }

                    var Account = ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == Player.SocialClubName).FirstOrDefault();
                    Account.NickName = nickname; 

                    NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: ~w~Du heißt jetzt " + nickname + "!");
                    NAPI.Player.SetPlayerName(Player, nickname);

                    Player.TriggerEvent("nicknamebrowserschliessen");
                    Player.TriggerEvent("geburtstagbrowseroeffnen");

                    Funktionen.LogEintrag(Player, "Nickname gesetzt: " + nickname);

                    ContextFactory.Instance.SaveChanges();
                    
                }
            }
        }

        [RemoteEvent("GeburtstagVersuch")]
        public void GeburtstagVersuch(Client Player, String geburtstag)
        {
            DateTime Geburtstag = DateTime.Parse(geburtstag);
            if (Geburtstag > DateTime.Now) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Der Geburtstag muss in der Vergangenheit liegen."); return; }
            if (Geburtstag < DateTime.Parse("01/01/1950")) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: So alt kannst du nicht sein!"); return; }
            if (Geburtstag > DateTime.Today.AddYears(-18)) { NAPI.Notification.SendNotificationToPlayer(Player, "~y~Info~w~: Du musst mindestens 18 Jahre alt sein!"); return; }

            var Account = ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == Player.SocialClubName).FirstOrDefault();
            Account.GeburtsDatum = Geburtstag;

            Player.TriggerEvent("kameraoff");
            Player.TriggerEvent("geburtstagbrowserschliessen");

            Funktionen.SpielerLaden(Player);
            Funktionen.SpawnManager(Player);
            Funktionen.LogEintrag(Player, "Geburtstag gesetzt: " + Geburtstag);

            ContextFactory.Instance.SaveChanges();

        }
    }
}

