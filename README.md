# Overcooked AYCE Ultrawide Fix (MelonLoader)

A small MelonLoader mod for **Overcooked! All You Can Eat** that forces a custom resolution (e.g. 21:9 / 3840x1600) and re-applies it on scene loads.

## Requirements
- Windows
- Overcooked! All You Can Eat (Steam)
- MelonLoader installed for the game

## Install
1. Install MelonLoader into the game.
2. Copy `AYCEUltrawideFix.dll` into:
   `<GameFolder>\Mods\`
3. Launch the game once to generate the config:
   `<GameFolder>\UserData\MelonPreferences.cfg`
4. Edit the config values under `[AYCEUltrawideFix]`:
   - `TargetWidth = 3840`
   - `TargetHeight = 1600`
5. Restart the game.

## Config
- `Enabled` (true/false)
- `TargetWidth` / `TargetHeight`
- `UseBorderless` (recommended true)
- `ForceCameraRect` (removes black bars if they are caused by camera viewport)
- `Burst1Delay/Burst2Delay/Burst3Delay` (if scenes keep resetting res)

## Uninstall
Remove the DLL from `<GameFolder>\Mods\`.

## Donations
If you like the mod and want to support future updates:
- Ko-fi: [https://ko-fi.com/axiolus]
