using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using PointBlank.API;
using PointBlank.API.Server;
using PointBlank.API.Server.Extensions;
using PointBlank.API.Server.Attributes;
using System.IO;
using System.Net;

namespace PlayerList
{
    [Plugin("PlayerList", "Roy", false, false)]
    public class Main : PBPlugin
    {
        public string directory = Directory.GetCurrentDirectory() + "/..";
        WebClient webclient;
        public override void onLoad()
        {
            PBLogging.logImportant("PlayerList Loaded!");
            if (File.Exists(directory + "/PlayerList.txt"))
            {
                PBLogging.log(directory + "/PlayerList.txt Already Exists");
            }
            else
            {
                File.CreateText(directory + "/PlayerList.txt");
            }
            PBServer.OnPlayerJoin += PBServer_OnPlayerJoin;
        }

        private void PBServer_OnPlayerJoin(PBPlayer player)
        {
            using (StreamWriter w = File.AppendText(directory + "/PlayerList.txt"))
            {
                w.WriteLine("[" + player.playerID.characterName + "]" + " " + "[Ping : " + player.steamPlayer.lastPing + " ] " + "[IP : " + player.IP + " ]");
                w.Close();
            }
        }
    }
}
