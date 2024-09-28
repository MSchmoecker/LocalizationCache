using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using HarmonyLib.Public.Patching;
using UnityEngine;

namespace LocalizationCache {
    [HarmonyPatch]
    public static class LocalizationPatches {
        private static Dictionary<Tuple<string, string>, Dictionary<string, string>> languageTranslations =
            new Dictionary<Tuple<string, string>, Dictionary<string, string>>();

        private static List<Patch> patchedMods = new List<Patch>();

        private static MethodInfo LoadCSVMethod { get; } = AccessTools.Method(typeof(Localization), nameof(Localization.LoadCSV));

        [HarmonyPatch(typeof(Localization), nameof(Localization.LoadCSV)), HarmonyPrefix, HarmonyPriority(Priority.First)]
        public static bool SetupLanguage_Prefix(Localization __instance, TextAsset file, string language, ref bool __result) {
            if (!file) {
                return true;
            }

            Tuple<string, string> key = new Tuple<string, string>(file.name, language);

            if (!languageTranslations.TryGetValue(key, out Dictionary<string, string> translations)) {
                return true;
            }

            __instance.m_translations = translations;
            __result = true;
            return false;
        }

        [HarmonyPatch(typeof(Localization), nameof(Localization.LoadCSV)), HarmonyPostfix, HarmonyPriority(Priority.Last)]
        public static void SetupLanguage_Postfix(Localization __instance, TextAsset file, string language, bool __result) {
            if (__result && file) {
                Tuple<string, string> key = new Tuple<string, string>(file.name, language);
                languageTranslations[key] = __instance.m_translations;
                CacheOtherMods();
            }
        }

        private static void CacheOtherMods() {
            if (!Plugin.CacheMods.Value) {
                return;
            }

            foreach (Patch csvPatch in LoadCSVMethod.ToPatchInfo().postfixes) {
                if (csvPatch.owner == Plugin.ModGuid) {
                    continue;
                }

                if (patchedMods.Contains(csvPatch)) {
                    continue;
                }

                Plugin.harmony.Patch(csvPatch.PatchMethod, prefix: new HarmonyMethod(typeof(LocalizationPatches), nameof(SkipLoad)));
                patchedMods.Add(csvPatch);
            }
        }

        private static bool SkipLoad() {
            return false;
        }
    }
}
