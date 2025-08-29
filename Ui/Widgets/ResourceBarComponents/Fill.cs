using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using VisibleStamina.Configuration;

namespace VisibleStamina.Ui.Widgets.ResourceBarComponents
{
    public class Fill : Image
    {
        public Fill(Texture2D texture, ModConfig config, IMonitor monitor) : base(texture, config, monitor)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
            {
                return;
            }

            base.Draw(spriteBatch);
            var highlightRectangle = new Rectangle(Position, new(Config.FillHighlightWidth, Size.Y));
            var highlightColor = new Color(Math.Max(0, Color.R - 50), Math.Max(0, Color.G - 50), Color.B, Color.A);
            spriteBatch.Draw(Texture, highlightRectangle, highlightColor);
        }
    }
}
