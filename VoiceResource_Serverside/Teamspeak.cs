using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamSpeak3QueryApi.Net;
using TeamSpeak3QueryApi.Net.Specialized;
using TeamSpeak3QueryApi.Net.Specialized.Responses;
using Haupt;

namespace RAGEMP_TsVoice
{
	public class Teamspeak : Script
	{
		private TeamSpeakClient tsQuery;

		private static string TeamspeakQueryAddress { get; set; }
		private static short TeamspeakQueryPort { get; set; }
		private static string TeamspeakPort { get; set; }
		private static string TeamspeakLogin { get; set; }
		private static string TeamspeakPassword { get; set; }
		private static string TeamspeakChannel { get; set; }

		public Teamspeak()
		{
			Console.WriteLine("Teamspeak Wrapper Initialization...");
		}

		//Here we start generation of a random name
		public static String GetRandomString()
		{
			var allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
			var length = 16;

			var chars = new char[length];
			var rd = new Random();

			for (var i = 0; i < length; i++)
			{
				chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
			}

			return new String(chars);
		}

		[ServerEvent(Event.ResourceStart)]
		public void OnResourceStart()
		{
			Task.Run(async () =>
			{
				try
				{
					TeamspeakQueryAddress = NAPI.Resource.GetSetting<string>(this, "teamspeak_query_address");
					TeamspeakQueryPort = NAPI.Resource.GetSetting<short>(this, "teamspeak_query_port");
					TeamspeakPort = NAPI.Resource.GetSetting<string>(this, "teamspeak_port");
					TeamspeakLogin = NAPI.Resource.GetSetting<string>(this, "teamspeak_login");
					TeamspeakPassword = NAPI.Resource.GetSetting<string>(this, "teamspeak_password");
					TeamspeakChannel = NAPI.Resource.GetSetting<string>(this, "teamspeak_channel");

					await InitTSQuery();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}


				Console.WriteLine("Teamspeak Wrapper Initialised!");
			});
		}

		[ServerEvent(Event.ResourceStop)]
		public static void OnResourceStop()
		{
			NAPI.ClientEvent.TriggerClientEventForAll("DisconnectTeamspeak");
		}

		[RemoteEvent("ChangeVoiceRange")]
		public void ChangeVoiceRange(Client client)
		{
			string voiceRange = "Normal";
			string Lautstärke = null;
			if (client.HasSharedData("VOICE_RANGE"))
				voiceRange = client.GetSharedData("VOICE_RANGE");

			switch (voiceRange)
			{
				case "Normal":
					voiceRange = "Weit";
					Lautstärke = "laut";
					break;
				case "Weit":
					voiceRange = "Kurz";
					Lautstärke = "leise";
					break;
				case "Kurz":
					voiceRange = "Normal";
					Lautstärke = "normal";
					break;
			}
			client.SetSharedData("VOICE_RANGE", voiceRange);
			client.SendNotification("~y~Info~w~: Du sprichst nun " + Lautstärke);
		}

		public static void Connect(Client client, string characterName)
		{
			client.SetSharedData("VOICE_RANGE", "Normal");
			client.SetSharedData("TsName", characterName);
			client.TriggerEvent("ConnectTeamspeak", characterName);
		}

		private async Task InitTSQuery()
		{
			tsQuery = new TeamSpeakClient(TeamspeakQueryAddress, TeamspeakQueryPort); // Create rich client instance

			try
			{
				await tsQuery.Connect(); // connect to the server
				await tsQuery.Login(TeamspeakLogin, TeamspeakPassword); // login to do some stuff that requires permission
				await tsQuery.UseServer(1); // Use the server with id '1'
				var me = await tsQuery.WhoAmI(); // Get information about yourself!

				var channel = (await tsQuery.FindChannel(TeamspeakChannel)).FirstOrDefault();

				Utils.Delay(100, false, async () =>
					await UpdateTeamspeak(channel)
				);
			}
			catch (QueryException ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}

		private async Task UpdateTeamspeak(FoundChannel channel)
		{
			for (int i = 0; i < NAPI.Pools.GetAllPlayers().Count; i++)
			{
				if (NAPI.Pools.GetAllPlayers()[i] != null && NAPI.Pools.GetAllPlayers()[i].Exists)
				{
					await RefreshSpeaker(channel, NAPI.Pools.GetAllPlayers()[i], NAPI.Pools.GetAllPlayers());
				}
			}
		}

		public async Task RefreshSpeaker(FoundChannel channel, Client Player, List<Client> players)
		{
			
			if (!tsQuery.Client.IsConnected)
			{
				return;
			}
			try
			{
				//Alter kram der rumgebuggt hat
				//var clients = await tsQuery.GetClients(GetClientOptions.Voice);
				//var clientschannel = clients.ToList().FindAll(c => c.ChannelId == channel.Id);
				//var tsplayer = clientschannel.Find(p => p.NickName == name);

				var name = Player.GetSharedData("TsName");
				var currentClients = await tsQuery.GetClients(GetClientOptions.Voice);
				var tsplayer = currentClients.SingleOrDefault(c => c.NickName == name); 
				
				if (tsplayer != null)
				{
					if (tsplayer.Talk && Player.GetData("IS_SPEAKING") == 0)
					{
						players.FindAll(p => p.Exists && p.Position.DistanceTo2D(Player.Position) < 5f)
							.ForEach((client) => client.TriggerEvent("Teamspeak_LipSync", Player.Handle.Value, true));

						Player.SetData("IS_SPEAKING", 1);
					}
					else if (!tsplayer.Talk && Player.GetData("IS_SPEAKING") == 1)
					{
						players.FindAll(p => p.Exists && p.Position.DistanceTo2D(Player.Position) < 5f)
							.ForEach((client) => client.TriggerEvent("Teamspeak_LipSync", Player.Handle.Value, false));

						Player.SetData("IS_SPEAKING", 0);
					}
				}
				else
				{
					//DISCO falls man es so will später
					//if(Player.GetData("Eingeloggt") == 1 && NAPI.Player.IsPlayerConnected(Player))
					//{
					//	Player.TriggerEvent("DisconnectTeamspeak");
					//	Funktionen.SpielerSpeichern(Player);

					//	NAPI.Notification.SendNotificationToPlayer(Player, "~r~Du musst auf unserem Teamspeak sein um den Voice Chat nutzen zu können. IP: ts.strawberry-rp.de");

					//	//Log Eintrag
					//	Funktionen.LogEintrag(Player, "Nicht auf dem Teamspeak");

					//	NAPI.Player.KickPlayer(Player, "Nicht auf dem Teamspeak");
					//}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("TS RefreshSpeaker Error: " + ex.ToString());
			}
		}
	}
}