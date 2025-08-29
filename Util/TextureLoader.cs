using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using static StardewValley.LocalizedContentManager;

namespace VisibleStamina.Util
{
    public sealed class TextureLoader : IDisposable
    {
        private static string HealthTabHPath { get; } = "Assets/H.png";
        private static string HealthTabSPath { get;  } = "Assets/S.png";
        private static string EnergyTabPath { get; } = "Assets/E.png";
        private static string BarCapPath { get; } = "Assets/Cap.png";
        private static string BarPath { get; } = "Assets/Bar.png";
        private static string FillPath { get; } = "Assets/Fill.png";
        private static string ExhaustionPath { get; } = "Assets/Exhaustion.png";

        private IModHelper Helper { get; init; } = null!;
        private IMonitor Monitor { get; init; } = null!;

        public Texture2D? HealthTabTexture { get; private set; } = null;
        public Texture2D? EnergyTabTexture { get; private set; } = null;
        public Texture2D? BarCapTexture { get; private set; } = null;
        public Texture2D? BarTexture { get; private set; } = null;
        public Texture2D? FillTexture { get; private set; } = null;
        public Texture2D? ExhaustionTexture { get; private set; } = null;

        public TextureLoader(IModHelper helper, IMonitor monitor)
        {
            Helper = helper;
            Monitor = monitor;
            LoadTextures(HealthTabHPath);
            Helper.Events.Content.LocaleChanged += OnLocaleChanged;
        }

        public void Dispose()
        {
            Helper.Events.Content.LocaleChanged -= OnLocaleChanged;
            HealthTabTexture?.Dispose();
            EnergyTabTexture?.Dispose();
            BarCapTexture?.Dispose();
            BarTexture?.Dispose();
        }

        public void OnLocaleChanged(object? sender, LocaleChangedEventArgs e)
        {
            switch (e.NewLanguage)
            {
                case LanguageCode.en:
                case LanguageCode.ja:
                case LanguageCode.ru:
                case LanguageCode.zh:
                case LanguageCode.de:
                case LanguageCode.ko:
                case LanguageCode.hu:
                    LoadTextures(HealthTabHPath);
                    break;
                case LanguageCode.es:
                case LanguageCode.fr:
                case LanguageCode.it:
                case LanguageCode.tr:
                    LoadTextures(HealthTabSPath);
                    break;
                case LanguageCode.th:
                case LanguageCode.pt:
                    Monitor.Log($"No specific texture for language: {e.NewLanguage}. Using default.", LogLevel.Warn);
                    LoadTextures(HealthTabHPath);
                    break;
                case LanguageCode.mod:
                    Monitor.Log($"Mod language detected. Using default texture.", LogLevel.Warn);
                    LoadTextures(HealthTabHPath);
                    break;
            }
        }

        private void LoadTextures(string healthTabPath)
        {
            HealthTabTexture?.Dispose();
            HealthTabTexture = LoadTexture(healthTabPath);
            EnergyTabTexture?.Dispose();
            EnergyTabTexture = LoadTexture(EnergyTabPath);
            BarCapTexture?.Dispose();
            BarCapTexture = LoadTexture(BarCapPath);
            BarTexture?.Dispose();
            BarTexture = LoadTexture(BarPath);
            FillTexture?.Dispose();
            FillTexture = LoadTexture(FillPath);
            ExhaustionTexture?.Dispose();
            ExhaustionTexture = LoadTexture(ExhaustionPath);
        }

        private Texture2D? LoadTexture(string path)
        {
            return Helper.ModContent.Load<Texture2D>(path);
        }
    }
}
