using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using VisibleStamina.Configuration;

namespace VisibleStamina.Ui.Widgets.ResourceBarComponents
{
    public class Readout : Widget
    {
        public string Text { get; set; } = string.Empty;
        public Color Color { get; set; } = Color.Black;
        private Point Offset { get; set; } = Point.Zero;

        public Readout(ModConfig config, IMonitor monitor) : base(config, monitor)
        {
        }   

        public override void Dispose()
        {
        }

        public override void Update()
        {
            var textSize = Game1.smallFont.MeasureString(Text);
            var bounds = new Rectangle(Position, Size);
            var offset = bounds.Center - Position;
            Offset = new(offset.X - (int)(textSize.X / 2) + 1, offset.Y - (int)(textSize.Y / 2) + 2);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (!Visible)
            {
                return;
            }

            spritebatch.DrawString(Game1.smallFont, Text, (Position + Offset).ToVector2(), Color);
        }
    }
}
