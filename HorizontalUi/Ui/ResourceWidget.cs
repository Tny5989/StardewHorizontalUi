using StardewModdingAPI;
using VisibleStamina.Configuration;

namespace VisibleStamina.Ui
{
    public abstract class ResourceWidget : Widget
    {
        protected virtual int MaxResource { get; set; } = 0;
        protected virtual int CurrentResource { get; set; } = 0;

        public ResourceWidget(ModConfig config, IMonitor monitor)
            : base(config, monitor)
        {
        }
    }
}
