﻿using GorillaNetworking;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace KosmosModMenu.Mods
{
    internal class AntiBan
    {
        public AntiBan()
        {
            GameObject.Find("SaveModAccountData").GetComponent<GorillaComputer>().enabled = false;
            PhotonNetwork.LocalPlayer.CustomProperties["mods"] = "";
        }
    }
}
