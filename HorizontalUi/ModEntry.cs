using StardewModdingAPI;
using StardewModdingAPI.Events;
using VisibleStamina.Configuration;
using VisibleStamina.Harmony;
using VisibleStamina.Util;

// TODO config for position, size, opacity, etc.
// TODO animation for growing the bar?

namespace VisibleStamina
{
    public sealed class ModEntry : Mod
    {
        private TextureLoader TextureLoader { get; set; } = null!;
        private HarmonyLib.Harmony Harmony { get; set; } = null!;
        private ModConfig Config { get; set; } = null!;

        public override void Entry(IModHelper helper)
        {
            LoadConfig();

            TextureLoader = new(Helper, Monitor);
            DrawHudTranspiler.Hud = new(TextureLoader, Config, Monitor);
            PressUseToolButtonTranspiler.Hud = DrawHudTranspiler.Hud;
            Harmony = new(ModManifest.UniqueID);
            Harmony.PatchAll();

            Helper.Events.GameLoop.GameLaunched += RegisterConfig;
        }

        protected override void Dispose(bool disposing)
        {
            Helper.Events.GameLoop.GameLaunched -= RegisterConfig;
            Harmony.UnpatchAll(ModManifest.UniqueID);
            DrawHudTranspiler.Hud?.Dispose();
            DrawHudTranspiler.Hud = null;
            PressUseToolButtonTranspiler.Hud = null;
            TextureLoader.Dispose();
            base.Dispose(disposing);
        }

        private void LoadConfig()
        {
            Config = Helper.ReadConfig<ModConfig>();
        }

        private void RegisterConfig(object? sender, GameLaunchedEventArgs e)
        {
            var gmcm = Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (gmcm is null)
            {
                return;
            }

            gmcm.Register(
                mod: this.ModManifest,
                reset: () => this.Config.Reset(),
                save: () => this.Helper.WriteConfig(this.Config));

            gmcm.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "Bar Position");

            gmcm.AddNumberOption(
                mod: this.ModManifest,
                name: () => "Horizontal Margin",
                getValue: () => Config.HorizontalMargin,
                setValue: (value) => Config.HorizontalMargin = value);

            gmcm.AddNumberOption(
                mod: this.ModManifest,
                name: () => "Vertical Margin",
                getValue: () => Config.VerticalMargin,
                setValue: (value) => Config.VerticalMargin = value);

            gmcm.AddNumberOption(
                mod: this.ModManifest,
                name: () => "Spacing",
                getValue: () => Config.Spacing,
                setValue: (value) => Config.Spacing = value);

            gmcm.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "Bar Fill");

            gmcm.AddNumberOption(
                mod: this.ModManifest,
                name: () => "Fill Y Offset",
                getValue: () => Config.BarFillYOffset,
                setValue: (value) => Config.BarFillYOffset = value);

            gmcm.AddNumberOption(
                mod: this.ModManifest,
                name: () => "Fill Width Mod",
                getValue: () => Config.BarFillSizeOffstet,
                setValue: (value) => Config.BarFillSizeOffstet = value);

            gmcm.AddNumberOption(
                mod: this.ModManifest,
                name: () => "Fill Highlight Width",
                getValue: () => Config.FillHighlightWidth,
                setValue: (value) => Config.FillHighlightWidth = value);

            gmcm.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "Bar Visibility");

            gmcm.AddBoolOption(
                mod: this.ModManifest,
                name: () => "Always Show Health Bar",
                getValue: () => this.Config.AlwaysShowHealthBar,
                setValue: value => this.Config.AlwaysShowHealthBar = value
);

            gmcm.AddSectionTitle(
                mod: this.ModManifest,
                text: () => "Sweat and Blood");

            gmcm.AddNumberOption(
                mod: this.ModManifest,
                name: () => "Sweat Start",
                getValue: () => Config.SweatEnergy,
                setValue: (value) => Config.SweatEnergy = value);

            gmcm.AddNumberOption(
                mod: this.ModManifest,
                name: () => "Bleed Start",
                getValue: () => Config.BleedHealth,
                setValue: (value) => Config.BleedHealth = value);
        }
    }
}