using System.Collections.Generic;
using HarmonyLib;

namespace LocalizationCache {
    [HarmonyPatch]
    public static class LocalizationPatches {
        private static Dictionary<string, Dictionary<string, string>> languageTranslations = new Dictionary<string, Dictionary<string, string>>();

        [HarmonyPatch(typeof(Localization), nameof(Localization.LoadCSV)), HarmonyPrefix, HarmonyPriority(Priority.First)]
        public static bool SetupLanguage_Prefix(Localization __instance, string language, ref bool __result) {
            if (!languageTranslations.TryGetValue(language, out Dictionary<string, string> translations)) {
                return true;
            }

            __instance.m_translations = translations;
            __result = true;
            return false;
        }

        [HarmonyPatch(typeof(Localization), nameof(Localization.LoadCSV)), HarmonyPostfix, HarmonyPriority(Priority.Last)]
        public static void SetupLanguage_Postfix(Localization __instance, string language, bool __result) {
            if (__result) {
                languageTranslations[language] = __instance.m_translations;
            }
        }
    }
}
