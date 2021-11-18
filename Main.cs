
using MelonLoader;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System;
using System.Collections.Generic;
using HarmonyLib;

namespace TIFAModding
{

    public class BGColorChange : MelonMod
    {
        public static class BuildInfo
        {
            public const string Name = "BGColorChange";  // Name of the Mod.  (MUST BE SET)
            public const string Author = "MeepsKitten"; // Author of the Mod.  (Set as null if none)
            public const string Company = null; // Company that made the Mod.  (Set as null if none)
            public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
            public const string DownloadLink = ""; // Download Link for the Mod.  (Set as null if none)
        }

        public static ClothesData data = new ClothesData();
        public static GameObject Avatar = null;
        public static string PreviousArg = null;

        static Dictionary<string, object> runningToggleTimers = new Dictionary<string, object>();

        private static bool controllerToggle = false;
        public class ClothesData
        {
            public Outfit[] outfits;
            public Toggle[] toggles;
            public string defaultOutfit;

        }

        public class Toggle
        {
            public string[] toToggle;
            public string[] reverseToggle;
            public string channelPointName;
            public float timer = 0;
        }

        public class Outfit
        {
            public string[] toDisable;
            public string[] toEnable;
            public string channelPointName;
        }


        [HarmonyPatch(typeof(TwitchLogin), "LoadData")]
        private static class Start
        {
            private static void Prefix()
            {
                if (!File.Exists("./bg.color")) return;

                var data = File.ReadAllText("./bg.color");

                Color newCol;

                if (data.Length <= 0) return;

                if (ColorUtility.TryParseHtmlString(data, out newCol))
                {
                    Camera.main.backgroundColor = newCol;
                }
            }

        }

    }
}



