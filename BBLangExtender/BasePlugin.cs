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
    [BepInPlugin("mtm101.rulerp.bbplus.bble", "BB+ Language Extender", "1.0.0.0")]

    public class BaldiLanguageExtender : BaseUnityPlugin
    {

        
        void Awake()
        {
            Harmony harmony = new Harmony("mtm101.rulerp.bbplus.bble");
            
            harmony.PatchAll();
        }
    }
}
