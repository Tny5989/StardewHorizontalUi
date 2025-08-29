using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using VisibleStamina.Configuration;

namespace VisibleStamina.Ui.Widgets.ResourceBarComponents
{
    public class Background : Widget
    {
        public Image LeftImage { get; init; } = null!;
        public Image MiddleImage { get; init; } = null!;
        public Image RightImage { get; init; } = null!;
        public override Point Position 
        {
            get => base.Position;
            set 
            {
                base.Position = value;
                LeftImage.Position = value;
                MiddleImage.Position = new(value.X + LeftImage.Size.X, value.Y);
                RightImage.Position = new(value.X + LeftImage.Size.X + MiddleImage.Size.X, value.Y);
            }
        }
        public override Point Size 
        { 
            get => base.Size;
            set 
            {
                base.Size = value;
                LeftImage.Size = new(LeftImage.Size.X, LeftImage.Size.Y);
                MiddleImage.Size = new(Math.Max(0, value.X - LeftImage.Size.X - RightImage.Size.X), MiddleImage.Size.Y);
                RightImage.Size = new(RightImage.Size.X, RightImage.Size.Y);
            }
        }

        public Background(Texture2D leftImage, Texture2D middleImage, Texture2D rightImage, ModConfig config, IMonitor monitor)
            : base(config, monitor)
        {
            LeftImage = new(leftImage, Config, Monitor);
            MiddleImage = new(middleImage, Config, Monitor);
            RightImage = new(rightImage, Config, Monitor);
            Size = new(LeftImage.Size.X + MiddleImage.Size.X + RightImage.Size.X, LeftImage.Size.Y);
        }

        public override void Dispose()
        {
            LeftImage.Dispose();
            MiddleImage.Dispose();
            RightImage.Dispose();
        }

        public override void Update()
        {
            LeftImage.Update();
            MiddleImage.Update();
            RightImage.Update();
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (!Visible)
            {
                return;
            }

            LeftImage.Draw(spritebatch);
            MiddleImage.Draw(spritebatch);
            RightImage.Draw(spritebatch);
        }
    }
}
