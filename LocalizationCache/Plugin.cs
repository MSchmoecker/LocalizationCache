﻿using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace LocalizationCache {
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    public class Plugin : BaseUnityPlugin {
        public const string ModName = "LocalizationCache";
        public const string ModGuid = "com.maxsch.valheim.LocalizationCache";
        public const string ModVersion = "0.3.0";

        public static ConfigEntry<bool> EnableCache;
        public static ConfigEntry<bool> CacheMods;
        public static ConfigEntry<bool> DebugTiming;
        public static ConfigEntry<bool> DebugStacktrace;

        internal static ManualLogSource Log { get; private set; }
        internal static Harmony harmony;

        private void Awake() {
            Log = Logger;

            EnableCache = Config.Bind("1 - General", "Enable Cache", true, "Enable caching of localization files. Disable to compare loading times. Requires a restart to take effect");
            CacheMods = Config.Bind("1 - General", "Cache Mods", true, "Cache localization calls from some mods. Breaks switching the language at runtime but can drastically improve loading time. Requires a restart to take effect");
            DebugTiming = Config.Bind("2 - Debug", "Log Timing", false, "Log timing information for Localization.SetupLanguage. Requires a restart to take effect");
            DebugStacktrace = Config.Bind("2 - Debug", "Log Stacktrace", false, "Log stacktrace for each Localization.SetupLanguage call. Requires a restart to take effect");

            harmony = new Harmony(ModGuid);

            if (EnableCache.Value) {
                harmony.PatchAll(typeof(LocalizationPatches));
            }

            if (DebugTiming.Value || DebugStacktrace.Value) {
                harmony.PatchAll(typeof(DebugTimingPatch));
            }
        }
    }
}
