using System.Collections;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(AYCEUltrawideFix.Main), "AYCE Ultrawide Fix", "1.0.3", "yourname")]
[assembly: MelonGame(null, null)]

namespace AYCEUltrawideFix
{
    public class Main : MelonMod
    {
        // --- Config (MelonPreferences -> UserData\MelonPreferences.cfg) ---
        private static MelonPreferences_Category Cat;

        private static MelonPreferences_Entry<bool> Enabled;

        private static MelonPreferences_Entry<int> TargetWidth;
        private static MelonPreferences_Entry<int> TargetHeight;

        private static MelonPreferences_Entry<bool> UseBorderless;
        private static MelonPreferences_Entry<bool> ForceCameraRect;
        private static MelonPreferences_Entry<bool> ShowOverlay;

        // Burst timing knobs (some games override resolution after scene load)
        private static MelonPreferences_Entry<float> Burst1Delay;
        private static MelonPreferences_Entry<float> Burst2Delay;
        private static MelonPreferences_Entry<float> Burst3Delay;

        public override void OnInitializeMelon()
        {
            Cat = MelonPreferences.CreateCategory("AYCEUltrawideFix", "AYCE Ultrawide Fix");

            Enabled = Cat.CreateEntry("Enabled", true);

            // Your main request: configurable target resolution
            TargetWidth = Cat.CreateEntry("TargetWidth", 3840);
            TargetHeight = Cat.CreateEntry("TargetHeight", 1600);

            UseBorderless = Cat.CreateEntry("UseBorderless", true);
            ForceCameraRect = Cat.CreateEntry("ForceCameraRect", true);
            ShowOverlay = Cat.CreateEntry("ShowOverlay", true);

            Burst1Delay = Cat.CreateEntry("Burst1Delay", 0.10f);
            Burst2Delay = Cat.CreateEntry("Burst2Delay", 0.50f);
            Burst3Delay = Cat.CreateEntry("Burst3Delay", 1.00f);

            // Ensures the entries exist in UserData\MelonPreferences.cfg after first run
            MelonPreferences.Save(); // config lives in UserData\MelonPreferences.cfg [web:229][web:330]

            MelonLogger.Msg("[AYCEUltrawideFix] Initialized. Edit UserData\\MelonPreferences.cfg to change TargetWidth/TargetHeight.");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (!Enabled.Value) return;
            MelonCoroutines.Start(ApplyBurst());
        }

        public override void OnLateUpdate()
        {
            if (!Enabled.Value) return;
            ApplyOnce();
        }

        public override void OnGUI()
        {
            if (!Enabled.Value || !ShowOverlay.Value) return;

            GUI.Label(new Rect(10, 10, 1200, 24),
                $"AYCEUltrawideFix | Target={TargetWidth.Value}x{TargetHeight.Value} | Live={Screen.width}x{Screen.height}");
        }

        private IEnumerator ApplyBurst()
        {
            ApplyOnce();
            yield return new WaitForSeconds(Mathf.Max(0f, Burst1Delay.Value));
            ApplyOnce();
            yield return new WaitForSeconds(Mathf.Max(0f, Burst2Delay.Value));
            ApplyOnce();
            yield return new WaitForSeconds(Mathf.Max(0f, Burst3Delay.Value));
            ApplyOnce();
        }

        private void ApplyOnce()
        {
            int w = Mathf.Max(640, TargetWidth.Value);
            int h = Mathf.Max(480, TargetHeight.Value);

            if (Screen.width != w || Screen.height != h)
            {
                var mode = UseBorderless.Value ? FullScreenMode.FullScreenWindow : FullScreenMode.ExclusiveFullScreen;
                Screen.SetResolution(w, h, mode);
            }

            if (ForceCameraRect.Value)
                ForceAllCamerasFullRect();
        }

        private static void ForceAllCamerasFullRect()
        {
            var full = new Rect(0f, 0f, 1f, 1f);
            foreach (var cam in Camera.allCameras)
            {
                if (cam == null) continue;
                if (cam.rect != full) cam.rect = full;
            }
        }
    }
}
