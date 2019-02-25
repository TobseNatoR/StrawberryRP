using GTANetworkAPI;
using Datenbank;
using Haupt;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    if (passwort == Account.Passwort)
                    {
                        player.TriggerEvent("browserschliessen");
                        player.TriggerEvent("kameraoff");

                        Funktionen.LogEintrag(player, "Eingeloggt");
                        Funktionen.SpielerLaden(player);
                        Funktionen.SpawnManager(player);

                        NAPI.Player.SetPlayerName(player, Account.NickName);
                        NAPI.Notification.SendNotificationToPlayer(player, "~y~Info~w~: ~w~Du hast dich erfolgreich als " + player.SocialClubName + " angemeldet!");
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
                    NAPI.Notification.SendNotificationToPlayer(player, "~y~Info~w~: Der Name" + player.SocialClubName + " ist bereits bei uns registriert!");
                }
                else
                {
                    //Passwortlänge prüfen
                    if(passwort.Length < 6) { NAPI.Notification.SendNotificationToPlayer(player, "~y~Info~w~: Das Passwort sollte minimum 6 Zeichen haben!"); return;  }
                    if (passwort.Contains(" ")) { NAPI.Notification.SendNotificationToPlayer(player, "~y~Info~w~: Leerzeichen sind ungültig!"); return; }

                    var NeuerAccount = new Account
                    {
                        SocialClub = player.SocialClubName,
                        NickName = "Nicht gesetzt",
                        Passwort = passwort,
                        AdminLevel = 0,
                        Fraktion = 0,
                        Job = 0,
                        Geld = 0,
                        BankGeld = 0,
                        Perso = 0,
                        EinreiseDatum = DateTime.Now,
                        FahrzeugSchlüssel = 2
                    };

                    //Query absenden
                    ContextFactory.Instance.srp_accounts.Add(NeuerAccount);
                    ContextFactory.Instance.SaveChanges();

                    //Account Objekt erzeugen
                    AccountLokal account = new AccountLokal();

                    account.Id = ContextFactory.Instance.srp_accounts.Max(x => x.Id);
                    account.SocialClub = player.SocialClubName;
                    account.NickName = "Nicht gesetzt";
                    account.Passwort = passwort;
                    account.AdminLevel = 0;
                    account.Fraktion = 0;
                    account.Job = 0;
                    account.Geld = 100;
                    account.BankGeld = 0;
                    account.Perso = 0;
                    account.EinreiseDatum = DateTime.Now;
                    account.FahrzeugSchlüssel = 2;

                    //Damit der Account nochmal im Anschluss gespeichert wird
                    account.AccountGeändert = true;

                    //Account in der Liste Lokal speichern
                    Funktionen.AccountListe.Add(account);

                    Funktionen.LogEintrag(player, "Registriert");

                    player.TriggerEvent("browserschliessen");
                    player.TriggerEvent("nicknamebrowseroeffnen");

                    NAPI.Notification.SendNotificationToPlayer(player, "~y~Info~w~: Du hast dich erfolgreich als " + player.SocialClubName + " registriert!");
                }
            }
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

                    foreach (AccountLokal account in Funktionen.AccountListe)
                    {
                        if (account.SocialClub == Player.SocialClubName)
                        {
                            account.NickName = nickname;
                            account.AccountGeändert = true;
                        }
                    }

                    Player.TriggerEvent("kameraoff");
                    Player.TriggerEvent("nicknamebrowserschliessen");

                    Funktionen.SpielerLaden(Player);
                    Funktionen.SpawnManager(Player);
                    Funktionen.LogEintrag(Player, "Nickname gesetzt");

                    ContextFactory.Instance.SaveChanges();
                    
                }
            }
        }

    }
}

