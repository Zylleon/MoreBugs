using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace MoreBugs
{

    [HarmonyPatch(typeof(ThingMaker), "MakeThing")]
    static class ThingSwapperPatch
    {
        static bool Prefix(ref ThingDef def, ref ThingDef stuff)
        {
            if (def == ThingDefOf.Hay && Rand.Chance(0.01f))
            {
                def = ThingDefOf.Chocolate;
            }

            else if (def == ThingDefOf.DiningChair && Rand.Chance(0.20f))
            {
                def = ThingDef.Named("Armchair");
                stuff = ThingDef.Named("DevilstrandCloth");
            }

            else if (def == ThingDefOf.Stool && Rand.Chance(0.25f))
            {
                def = ThingDefOf.DiningChair;
            }

            else if (def == ThingDef.Named("Armchair") && Rand.Chance(0.16f))
            {
                def = ThingDefOf.Stool;
                stuff = ThingDefOf.WoodLog;
            }

            else if (def == ThingDef.Named("EggChickenFertilized") && Rand.Chance(0.04f))
            {
                def = ThingDef.Named("EggTurkeyFertilized");
            }


            return true;

        }
    }

    [HarmonyPatch(typeof(Verse.PawnGenerator), nameof(PawnGenerator.GeneratePawn), new[] { typeof(PawnGenerationRequest) })]
    static class SpeciesSwappingPatch
    {
        static void Prefix(ref PawnGenerationRequest request)
        {
            if (request.KindDef == PawnKindDefOf.Megaspider && Rand.Chance(0.2f))
            {
                request.KindDef = PawnKindDef.Named("Cow");
                request.Faction = Faction.OfPlayer;
            }
            else if (request.KindDef == PawnKindDef.Named("Alpaca") && Rand.Chance(0.2f) && request.Newborn)
            {
                request.KindDef = PawnKindDef.Named("Bison");
            }
            else if (request.KindDef == PawnKindDef.Named("Muffalo") && Rand.Chance(0.2f))
            {
                request.KindDef = PawnKindDef.Named("Megascarab");
                if (request.Faction.IsPlayer)
                {
                    request.Faction = Faction.OfInsects;
                }
            }
            else if (request.Newborn && request.Faction.IsPlayer && request.KindDef == PawnKindDef.Named("Husky") && Rand.Chance(0.05f))
            {
                request.KindDef = PawnKindDef.Named("Pig");
            }
        }
    }


    [HarmonyPatch(typeof(MusicManagerPlay), "AppropriateNow")]
    public static class Song_MapAppropriate_Patch
    {
        public static bool Prefix(ref bool __result, SongDef song)
        {
            if (song.tense)
            {
                __result = true;
                return false;
            }
            return true;
        }

    }
}
