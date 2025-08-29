using HarmonyLib;
using StardewValley;
using System.Reflection.Emit;
using VisibleStamina.Ui;

namespace VisibleStamina.Harmony
{
    [HarmonyPatch(typeof(Game1), "drawHUD", MethodType.Normal)]
    public static class DrawHudTranspiler
    {
        public static Hud? Hud { get; set; } = null;

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; ++i)
            {
                if (i >= 88 && i <= 103 || i >= 104 && i <= 142 || i >= 143 && i <= 176 || i >= 214 && i <= 261
                    || i >= 272 && i <= 276 || i >= 298 && i <= 302 || i >= 303 && i <= 368 || i >= 446 && i <= 486
                    || i >= 487 && i <= 548 || i >= 549 && i <= 603 || i >= 633 && i <= 644 || i >= 663 && i <= 707
                    || i >= 711 && i <= 722)
                {
                    yield return new CodeInstruction(OpCodes.Nop) { labels = codes[i].labels };
                }
                else
                {
                    yield return codes[i];
                }
            }
        }

        public static void Postfix()
        {
            Hud?.Update();
            Hud?.Draw();
        }
    }

    [HarmonyPatch(typeof(Game1), "pressUseToolButton", MethodType.Normal)]
    public static class PressUseToolButtonTranspiler
    {
        public static Hud? Hud { get; set; } = null;

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; ++i)
            {
                if (i >= 533 && i <= 600)
                {
                    yield return new CodeInstruction(OpCodes.Nop) { labels = codes[i].labels };
                }
                else
                {
                    yield return codes[i];
                }
            }
        }

        public static void Postfix(ref bool __result)
        {
            Hud?.OnAction(__result);
        }
    }

    [HarmonyPatch(typeof(Game1), "UpdateOther", MethodType.Normal)]
    public static class UpdateOtherTranspiler
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; ++i)
            {
                if (i >= 253 && i <= 323)
                {
                    yield return new CodeInstruction(OpCodes.Nop) { labels = codes[i].labels };
                }
                else
                {
                    yield return codes[i];
                }
            }
        }
    }
}