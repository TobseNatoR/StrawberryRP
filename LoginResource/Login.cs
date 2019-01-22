using GTANetworkAPI;
using Datenbank;
using Haupt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Login
{
    //Hallo Torben
    public class Event : Script
    {
        [RemoteEvent("LoginVersuch")]
        public void LoginVersuch(Client player, string passwort)
        {
            var Check = ContextFactory.Instance.srp_accounts.Count(x => x.SocialClub == player.SocialClubName);
            if (Check == 0)
            {
                NAPI.Notification.SendNotificationToPlayer(player, "~w~[~r~StrawberryRP~w~] Der Name " + player.SocialClubName + " ist noch nicht bei uns registriert!");
            }
            else
            {
                foreach (var Account in ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == player.SocialClubName).ToList())
                {
                    if (passwort == Account.Passwort)
                    {
                        NAPI.Notification.SendNotificationToPlayer(player, "~w~[~r~StrawberryRP~w~] ~w~Du hast dich erfolgreich als " + player.SocialClubName + " angemeldet!");
                        player.TriggerEvent("browserschliessen");
                        player.TriggerEvent("kameraoff");
                        Funktionen.LogEintrag(player, "Eingeloggt");
                        Funktionen.SpielerLaden(player);
                        Funktionen.SpawnManager(player);
                        NAPI.Player.SetPlayerName(player, Account.NickName);
                    }
                    else
                    {
                        NAPI.Notification.SendNotificationToPlayer(player, "~w~[~r~StrawberryRP~w~] ~w~Dieses Passwort scheint nicht zu stimmen!");
                    }
                }
            }
        }

        [RemoteEvent("RegistrierenVersuch")]
        public void RegistrierenVersuch(Client player, string passwort)
        {

            var Check = ContextFactory.Instance.srp_accounts.Count(x => x.SocialClub == player.SocialClubName);
            {
                if(Check > 0)
                {
                    NAPI.Notification.SendNotificationToPlayer(player, "~w~[~r~StrawberryRP~w~] Der Name" + player.SocialClubName + " ist bereits bei uns registriert!");
                }
                else
                {
                    var NeuerAccount = new Account
                    {
                        SocialClub = player.SocialClubName,
                        NickName = "Nicht gesetzt",
                        Passwort = passwort,
                        AdminLevel = 0
                    };
                    ContextFactory.Instance.srp_accounts.Add(NeuerAccount);
                    ContextFactory.Instance.SaveChanges();
                    Funktionen.LogEintrag(player, "Registriert");
                    NAPI.Notification.SendNotificationToPlayer(player, "~w~[~r~StrawberryRP~w~] Du hast dich erfolgreich als " + player.SocialClubName + " registriert!");
                    player.TriggerEvent("browserschliessen");
                    player.TriggerEvent("nicknamebrowseroeffnen");
                }
            }
        }

        [RemoteEvent("NicknameVersuch")]
        public void NicknameVersuch(Client player, string nickname)
        {

            var Check = ContextFactory.Instance.srp_accounts.Count(x => x.NickName == nickname);
            {
                if (Check > 0)
                {
                    NAPI.Notification.SendNotificationToPlayer(player, "~w~[~r~StrawberryRP~w~] Der Name" + nickname + " wird bereits bei uns verwendet!");
                }
                else
                {
                    var Account = ContextFactory.Instance.srp_accounts.Where(x => x.SocialClub == player.SocialClubName).FirstOrDefault();
                    Account.NickName = nickname; 
                    NAPI.Notification.SendNotificationToPlayer(player, "~w~[~r~StrawberryRP~w~] ~w~Du heißt jetzt " + nickname + "!");
                    Funktionen.LogEintrag(player, "Nickname gesetzt");
                    player.TriggerEvent("kameraoff");
                    player.TriggerEvent("nicknamebrowserschliessen");
                    Funktionen.SpielerLaden(player);
                    Funktionen.SpawnManager(player);
                    NAPI.Player.SetPlayerName(player, nickname);
                    ContextFactory.Instance.SaveChanges();
                    
                }
            }
        }

    }
}

