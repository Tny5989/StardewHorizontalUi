using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibleStamina.Configuration
{
    public class ModConfig
    {
        public int HorizontalMargin { get; set; } = 16;
        public int VerticalMargin { get; set; } = 8;
        public int Spacing { get; set; } = 16;
        public int BleedHealth { get; set; } = 10;
        public int SweatEnergy { get; set; } = 20;
        public int BarFillYOffset { get; set; } = 12;
        public int BarFillSizeOffstet { get; set; } = 24;
        public int FillHighlightWidth { get; set; } = 4;
        public bool AlwaysShowHealthBar { get; set; } = false;

        public void Reset()
        {
            HorizontalMargin = 16;
            VerticalMargin = 8;
            Spacing = 16;
            BleedHealth = 10;
            SweatEnergy = 20;
            BarFillYOffset = 12;
            BarFillSizeOffstet = 24;
            FillHighlightWidth = 4;
            AlwaysShowHealthBar = false;
        }
    }

    /// <summary>The API which lets other mods add a config UI through Generic Mod Config Menu.</summary>
    public interface IGenericModConfigMenuApi
    {
        void Register(IManifest mod, Action reset, Action save, bool titleScreenOnly = false);
        void AddSectionTitle(IManifest mod, Func<string> text, Func<string>? tooltip = null);
        void AddNumberOption(IManifest mod, Func<int> getValue, Action<int> setValue, Func<string> name, Func<string>? tooltip = null, int? min = null, int? max = null, int? interval = null, Func<int, string>? formatValue = null, string? fieldId = null);
        void AddBoolOption(IManifest mod, Func<bool> getValue, Action<bool> setValue, Func<string> name, Func<string>? tooltip = null, string? fieldId = null);
    }
}
