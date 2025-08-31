using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using VisibleStamina.Configuration;
using VisibleStamina.Ui.Widgets.ResourceBarComponents;
using VisibleStamina.Util;

namespace VisibleStamina.Ui.Widgets
{
    public class HealthBar : BaseBar
    {
        public HealthBar(TextureLoader textureLoader, ModConfig config, IMonitor monitor) 
            : base(textureLoader.HealthTabTexture!, textureLoader.BarTexture!, textureLoader.BarCapTexture!, textureLoader.FillTexture!, config, monitor)
        {
            DefaultResourceAmount = 100f;
        }

        public override void Update()
        {
            MaxResource = Game1.player.maxHealth;
            CurrentResource = Game1.player.health;
            base.Update();
            Bleed();
        }
        
        private void Bleed()
        {
            var timerTick = (Game1.noteBlockTimer == 0f);
            var lowHealth = (Game1.player.health <= Config.BleedHealth);
            var noEvents = (Game1.CurrentEvent == null);
            var noScreenChange = (Game1.fadeToBlackAlpha <= 0f);

            if (Visible && timerTick && lowHealth && noEvents && noScreenChange)
            {
                for (var i = 0; i < 3; ++i)
                {
                    Game1.uiOverlayTempSprites.Add(new TemporaryAnimatedSprite("Loosesprites\\Cursors", new Rectangle(366, 412, 5, 6), new Vector2(Position.X + Game1.random.Next(32), Position.Y), flipped: false, 0.017f, Color.Red)
                    {
                        motion = new(-1.5f, -8 + Game1.random.Next(-1, 2)),
                        acceleration = new(0f, 0.5f),
                        local = true,
                        scale = 4f,
                        delayBeforeAnimationStart = i * 150
                    });
                }
            }
        }
    }
}
