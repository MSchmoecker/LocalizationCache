# LocalizationCache

## About
This is small mod that caches localization loading, which can help improve startup performance and reduce possible connection timeouts.

With no other mods and the language set to English, only a small improvement is achieved.
On my system, the load time is reduced by a few hundred milliseconds.
If you have mods that call the localization multiple times, you can expect an improvement of a few seconds.
Some mods also cache internally, so the improvement may not stack beyond a certain point.

In some cases, loading is also triggered when joining a world which can exceed the 30s Steam timeout and cause a disconnect.
This mod can help to reduce those disconnects, especially on low end systems.

The log messages `Loaded localization file #0 - 'localization' language: 'English'`... will still be printed, as some mods rely on the underlying functionality.
While the vanilla localization is cached in this case, mods may load their own localization files for the first time which are not cached.

## Configuration

General:
- Enable Cache: Enable caching of localization files. Disable to compare loading times. Requires a restart to take effect. Default: true

Debugging:
- Log Timing: Log timing information for Localization.SetupLanguage. Requires a restart to take effect. Default: false
- Log Stacktrace: Log stacktrace for each Localization.SetupLanguage call. Requires a restart to take effect. Default: false


## Manual Installation
This mod requires [BepInEx](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/).
Extract all files to `BepInEx/plugins/LocalizationCache`.

The mod can be installed on both server and client and is optional.
However, it will have the most impact on the client.

## Links
- [Thunderstore](https://valheim.thunderstore.io/package/MSchmoecker/LocalizationCache/)
- [Github](https://github.com/MSchmoecker/LocalizationCache)
- Discord: margmas. Feel free to DM or ping me about feedback or questions, for example in the [Jötunn discord](https://discord.gg/DdUt6g7gyA)


## Changelog

0.3.0
- Added caching of some mod translation loading calls, which can help a lot with many mods. However, this breaks switching languages in-game, as the mod translations are not reloaded. Can be disabled in the config.

0.2.0
- Fixed caching did not distinguish between different localization files, resulting in missing vanilla translations
- Added config options for debugging timing and stacktrace

0.1.0
- Release
