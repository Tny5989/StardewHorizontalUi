using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using VisibleStamina.Configuration;

namespace VisibleStamina.Ui
{
    public abstract class Widget : Object
    {
        public virtual Point Position { get; set; } = Point.Zero;
        public virtual Point Size { get; set; } = Point.Zero;
        public virtual bool Visible { get; set; } = true;

        public Widget(ModConfig config, IMonitor monitor)
            : base(config, monitor)
        {
        }

        public abstract void Update();

        public abstract void Draw(SpriteBatch spritebatch);
    }
}