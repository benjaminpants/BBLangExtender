using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Net;
using System.IO;
//BepInEx stuff
using BepInEx;
using BepInEx.Logging;
using UnityEngine;
using UnityEngine.SceneManagement;
using HarmonyLib;
using BepInEx.Configuration;
using System.Collections.Generic;

namespace BBLangExtender
{
    [HarmonyPatch(typeof(LocalizationManager))]
    [HarmonyPatch("LoadLocalizedText")]
    class LoaderExtension
    {
        static void Finalizer(LocalizationManager __instance, ref Language language, ref Dictionary<string,string> ___localizedText)
        {
            string moddedfolderpath = Path.Combine(Application.streamingAssetsPath, "Modded", language.ToString());
            if (Directory.Exists(moddedfolderpath))
            {
                string[] dirs = Directory.GetFiles(moddedfolderpath, "*.json");
                if (dirs.Length == 0)
                {
                    return;
                }
                for (int i = 0; i < dirs.Length; i++)
                {
                    LocalizationData localizationData = null;
                    try
                    {
                        localizationData = JsonUtility.FromJson<LocalizationData>(File.ReadAllText(dirs[i]));
                    }
                    catch(Exception E)
                    {
                        UnityEngine.Debug.LogError("Given JSON for file: " + Path.GetFileName(dirs[i]) + " is invalid!");
                        UnityEngine.Debug.LogError(E.Message);
                        continue;
                    }
                    for (int j = 0; j < localizationData.items.Length; j++)
                    {
                        if (!___localizedText.ContainsKey(localizationData.items[j].key))
                        {
                            ___localizedText.Add(localizationData.items[j].key, localizationData.items[j].value);
                        }
                        else
                        {
                            ___localizedText[localizationData.items[j].key] = localizationData.items[j].value;
                        }
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(moddedfolderpath);
                return;
            }

            UnityEngine.Debug.Log(__instance.GetLocalizedText("TEST_One"));
        }
    }
}
