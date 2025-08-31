using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using VisibleStamina.Configuration;

namespace VisibleStamina.Ui.Widgets
{
    public class Image : Widget
    {
        public Texture2D Texture { get; init; } = null!;
        public Color Color { get; set; } = Color.White;

        public Image(Texture2D texture, ModConfig config, IMonitor monitor)
            : base(config, monitor)
        {
            Texture = texture;
            Size = new(Texture.Width, Texture.Height);
        }

        public override void Dispose()
        {
        }

        public override void Update()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Visible)
            {
                return;
            }

            spriteBatch.Draw(Texture, new Rectangle(Position, Size), Color);
        }
    }
}
