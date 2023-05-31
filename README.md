# LocalizationCache

## About
This is small mod that caches localization loading, which can help improve startup performance and reduce possible connection timeouts.

With no other mods and the language set to English, only a small improvement is achieved.
On my system, the load time is reduced from about 0.5s to 0.25s.
If you have mods that call the localization multiple times, you can expect an improvement of a few seconds.
Some mods also cache internally, so the improvement may not stack beyond a certain point.

In some cases, loading is also triggered when joining a world which can exceed the 30s Steam timeout and cause a disconnect.
This mod can help to reduce those disconnects, especially on low end systems.

The log messages `Loaded localization file #0 - 'localization' language: 'English'`... will still be printed, as some mods rely on the underlying functionality.
While the vanilla localization is cached in this case, mods may load their own localization files for the first time which are not cached.


## Manual Installation
This mod requires [BepInEx](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/).
Extract all files to `BepInEx/plugins/LocalizationCache`


## Links
- [Thunderstore](https://valheim.thunderstore.io/package/MSchmoecker/LocalizationCache/)
- [Github](https://github.com/MSchmoecker/LocalizationCache)
- Discord: Margmas#9562. Feel free to DM or ping me, for example in the [Jötunn discord](https://discord.gg/DdUt6g7gyA)


## Changelog
0.1.0
- Release
