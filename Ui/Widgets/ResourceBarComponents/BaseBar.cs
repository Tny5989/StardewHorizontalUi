using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using VisibleStamina.Configuration;

namespace VisibleStamina.Ui.Widgets.ResourceBarComponents
{
    public class BaseBar : ResourceWidget
    {
        protected Background Background { get; init; } = null!;
        protected Fill Fill { get; init; } = null!;
        protected Readout Readout { get; init; } = null!;
        protected float DefaultResourceAmount { get; init; } = 0f;
        protected float ResourceScalar { get; init; } = 1f;
        private int ExtraWidthFromResourceAmount { get; set; } = 0;
        protected override int MaxResource
        {
            get => base.MaxResource;
            set 
            {
                base.MaxResource = value;
                UpdateFill();
            }
        }
        protected override int CurrentResource
        {
            get => base.CurrentResource;
            set
            {
                base.CurrentResource = value;
                Readout.Text = Math.Max(0, value).ToString();
                UpdateFill();
            }
        }
        public override Point Position
        { 
            get => base.Position;
            set 
            {
                base.Position = value;
                Background.Position = value;
                Readout.Position = Background.MiddleImage.Position;
                UpdateFill();
            }
        }
        public override Point Size
        {
            get => Background.Size; // Use background size
            set
            {
                Background.Size = value;
                Readout.Size = Background.MiddleImage.Size;
                UpdateFill();
            }
        }

        public override bool Visible
        {
            get => base.Visible;
            set
            {
                base.Visible = value;
                Background.Visible = value;
                Fill.Visible = value;
                Readout.Visible = value;
            }
        }

        public BaseBar(Texture2D tabTexture, Texture2D barTexture, Texture2D capTexture, Texture2D fillTexture, ModConfig config, IMonitor monitor) : base(config, monitor)
        {
            Background = new(tabTexture, barTexture, capTexture, Config, Monitor);
            Fill = new(fillTexture, Config, Monitor);
            Readout = new(Config, Monitor);
            Size = Size;
            Position = Position;
        }

        public override void Dispose()
        {
            Readout.Dispose();
            Fill.Dispose();
            Background.Dispose();
        }

        public override void Update()
        {
            UpdateExtraWidth();
            Background.Update();
            Fill.Update();
            Readout.Update();
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (!Visible)
            {
                return;
            }

            Background.Draw(spritebatch);
            Fill.Draw(spritebatch);
            Readout.Draw(spritebatch);
        }

        protected void UpdateExtraWidth()
        {
            var oldExtraWidth = ExtraWidthFromResourceAmount;
            ExtraWidthFromResourceAmount = (int)((MaxResource - DefaultResourceAmount) * ResourceScalar);
            Size = new(Size.X + (ExtraWidthFromResourceAmount - oldExtraWidth), Size.Y);
        }

        protected void UpdateFill()
        {
            var fullWidth = Background.MiddleImage.Size.X;
            var currentWidth = (int)(CurrentResource / (float)MaxResource * fullWidth);
            var widthDifference = fullWidth - currentWidth;

            Fill.Position = new(Background.MiddleImage.Position.X + widthDifference, Background.MiddleImage.Position.Y + Config.BarFillYOffset);
            Fill.Size = new(currentWidth, Background.MiddleImage.Size.Y - Config.BarFillSizeOffstet);
            Fill.Color = Utility.getRedToGreenLerpColor(CurrentResource / (float)MaxResource);
        }
    }
}
