using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Tools;
using VisibleStamina.Configuration;
using VisibleStamina.Ui.Widgets.ResourceBarComponents;
using VisibleStamina.Util;

namespace VisibleStamina.Ui.Widgets
{
    public class EnergyBar : BaseBar
    {
        public EnergyBar(TextureLoader textureLoader, ModConfig config, IMonitor monitor) 
            : base(textureLoader.EnergyTabTexture!, textureLoader.BarTexture!, textureLoader.BarCapTexture!, textureLoader.FillTexture!, config, monitor)
        {
            DefaultResourceAmount = 270f;
            ResourceScalar = 0.625f;
        }

        public override void Update()
        {
            MaxResource = Game1.player.MaxStamina;
            CurrentResource = (int)Game1.player.Stamina;
            base.Update();
        }

        public void OnAction(bool ogResult)
        {
            if (ogResult)
            {
                return;
            }

            if (Game1.fadeToBlack)
            {
                return;
            }

            if (Game1.currentMinigame == null && !Game1.player.UsingTool && (Game1.player.IsSitting() || Game1.player.isRidingHorse() || Game1.player.onBridge.Value || Game1.dialogueUp || (Game1.eventUp && !Game1.CurrentEvent.canPlayerUseTool() && (!Game1.currentLocation.currentEvent.playerControlSequence || (Game1.activeClickableMenu == null && Game1.currentMinigame == null))) || (Game1.player.CurrentTool != null && (Game1.currentLocation.doesPositionCollideWithCharacter(Utility.getRectangleCenteredAt(Game1.player.GetToolLocation(), 64), ignoreMonsters: true)?.IsVillager ?? false))))
            {
                return;
            }

            Vector2 position = ((!Game1.wasMouseVisibleThisFrame) ? Game1.player.GetToolLocation() : new Vector2(Game1.getOldMouseX() + Game1.viewport.X, Game1.getOldMouseY() + Game1.viewport.Y));
            if (Utility.canGrabSomethingFromHere((int)position.X, (int)position.Y, Game1.player))
            {
                return;
            }

            Sweat();
        }

        public void Sweat()
        {
            var lowStamina = (Game1.player.Stamina <= Config.SweatEnergy);
            var noActiveObject = Game1.player.ActiveObject == null;
            var notEating = !Game1.player.isEating;
            var hasCurrentTool = Game1.player.CurrentTool != null && Game1.player.CurrentTool is not MeleeWeapon;
            var noEvent = !Game1.eventUp && Game1.CurrentEvent == null;
            var noScreenChange = (Game1.fadeToBlackAlpha <= 0f);

            if (Visible && lowStamina && noActiveObject && notEating && hasCurrentTool && noEvent && noScreenChange)
            {
                for (var i = 0; i < 4; ++i)
                {
                    Game1.uiOverlayTempSprites.Add(new TemporaryAnimatedSprite("Loosesprites\\Cursors", new Rectangle(366, 412, 5, 6), new Vector2(Position.X + Game1.random.Next(32), Position.Y), flipped: false, 0.012f, Color.SkyBlue)
                    {
                        motion = new(-2f, -10f),
                        acceleration = new(0f, 0.5f),
                        local = true,
                        scale = 4f + Game1.random.Next(-1, 0),
                        delayBeforeAnimationStart = i * 30
                    });
                }
            }
        }
    }
}
