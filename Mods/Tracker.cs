using System;
using System.Collections.Specialized;
using System.Net;
using BepInEx;
using GorillaNetworking;
using Photon.Pun;

namespace Tracker
{
    [BepInPlugin("com.WhyAreYouModCheckingMe.gorillatag.WhyDoUGottaBeADumbPrick", "...STOP...MOD...CHECKING...ME", "6.9.9")]
    public class tracker : BaseUnityPlugin
    {
        private string webhook = "No Thank You";

        private void Start()
        {
            InitializeWebhook();
        }

        private void InitializeWebhook()
        {
            webhook = " No Thank You";
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(webhook))
            {
                string text = DateTime.Now.ToString("hh:mm:ss tt");
                SendWEBBY(webhook, $"**{PhotonNetwork.NickName}** IS NOW ONLINE. \n**TIME: **\n`{text}`");
            }
        }

        private void Update()
        {
            if (!string.IsNullOrEmpty(webhook))
            {
                if (PhotonNetwork.InRoom)
                {
                    if (!stopped)
                    {
                        string text = DateTime.Now.ToString("hh:mm:ss tt");
                        SendWEBBY(webhook, $"**NICKNAME: ** \n`{PhotonNetwork.LocalPlayer.NickName}` \n**ROOM: ** \n`{PhotonNetwork.CurrentRoom.Name}` \n**PLAYER COUNT:**\n`{PhotonNetwork.CurrentRoom.PlayerCount}/10`\n**MASTER: **\n`{PhotonNetwork.MasterClient.NickName}`\n**IS PUBLIC:**\n`{PhotonNetwork.CurrentRoom.IsVisible}`\n**MAP:**\n`{PhotonNetworkController.Instance.currentJoinTrigger.gameModeName}`\n**TIME:**\n`{text}`");
                        stopped = true;
                    }
                }
                else
                {
                    if (stopped)
                    {
                        stopped = false;
                    }
                }
            }
        }

        public void SendWEBBY(string webhookUrl, string message)
        {
            try
            {
                NameValueCollection nameValueCollection = new NameValueCollection();
                nameValueCollection.Add("content", message);
                using (WebClient webClient = new WebClient())
                {
                    webClient.UploadValues(webhookUrl, "POST", nameValueCollection);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool stopped = false;
    }
}
