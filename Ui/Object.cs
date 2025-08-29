using StardewModdingAPI;
using VisibleStamina.Configuration;

namespace VisibleStamina.Ui
{
    public abstract class Object : IDisposable
    {
        protected IMonitor Monitor { get; init; } = null!;
        protected ModConfig Config { get; init; } = null!;

        public Object(ModConfig config, IMonitor monitor)
        {
            Monitor = monitor;
            Config = config;
        }

        public abstract void Dispose();
    }
}
