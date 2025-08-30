using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using VisibleStamina.Configuration;
using VisibleStamina.Ui.Widgets;
using VisibleStamina.Util;

namespace VisibleStamina.Ui
{
    public sealed class Hud : Object
    {
        private TextureLoader TextureLoader { get; set; } = null!;
        private EnergyBar EnergyBar { get; init; } = null!;
        private HealthBar HealthBar { get; init; } = null!;
        private Image ExhaustionIndicator { get; init; } = null!;

        public Hud(TextureLoader textureLoader, ModConfig config, IMonitor monitor)
            : base(config, monitor)
        {
            TextureLoader = textureLoader;
            Monitor = monitor;
            EnergyBar = new(TextureLoader, Config, Monitor);
            HealthBar = new(TextureLoader, Config, Monitor);
            ExhaustionIndicator = new(TextureLoader.ExhaustionTexture!, Config, Monitor);
        }

        public override void Dispose()
        {
            EnergyBar.Dispose();
            HealthBar.Dispose();
            ExhaustionIndicator.Dispose();
        }

        public void Update()
        {
            var titleSafeArea = Game1.graphics.GraphicsDevice.Viewport.TitleSafeArea;

            var position = new Point(titleSafeArea.Right - EnergyBar.Size.X - Config.HorizontalMargin, titleSafeArea.Bottom - EnergyBar.Size.Y - Config.VerticalMargin);
            EnergyBar.Position = position + GetShakeOffset(Game1.staminaShakeTimer > 0);
            EnergyBar.Update();

            position = new(titleSafeArea.Right - HealthBar.Size.X - Config.HorizontalMargin, titleSafeArea.Bottom - EnergyBar.Size.Y - HealthBar.Size.Y - Config.VerticalMargin - Config.Spacing);
            HealthBar.Position = position + GetShakeOffset(Game1.hitShakeTimer > 0);
            HealthBar.Visible = Game1.showingHealth || Game1.showingHealthBar || Config.AlwaysShowHealthBar;
            HealthBar.Update();

            ExhaustionIndicator.Position = new(EnergyBar.Position.X - ExhaustionIndicator.Size.X, EnergyBar.Position.Y);
            ExhaustionIndicator.Visible = Game1.player.exhausted.Value;
            ExhaustionIndicator.Update();
        }

        public void Draw()
        { 
            if (Game1.eventUp || Game1.farmEvent != null)
            {
                return;
            }

            EnergyBar.Draw(Game1.spriteBatch);
            HealthBar.Draw(Game1.spriteBatch);
            ExhaustionIndicator.Draw(Game1.spriteBatch);
        }

        public void OnAction(bool ogResult)
        {
            EnergyBar.OnAction(ogResult);
        }

        private static Point GetShakeOffset(bool shake)
        {
            if (shake)
            {
                return new(Game1.random.Next(-3, 4), Game1.random.Next(-3, 4));
            }
            return Point.Zero;
        }
    }
}
