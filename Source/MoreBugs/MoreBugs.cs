using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MoreBugs
{
    [StaticConstructorOnStartup]
    static class MoreBugs
    {
        static MoreBugs()
        {
            // there will be patches at some point I'm sure
            Harmony harmony = new Harmony("zylle.morebugs");
            harmony.PatchAll();
            Log.Message("More Bugs initialized");

            InitBugs();
        }

        static void InitBugs()
        {
            // unfertilized chicken eggs hatch, even if frozen
            ThingDef chickenEgg = DefDatabase<ThingDef>.GetNamed("EggChickenUnfertilized");
            CompProperties_Hatcher chickenHatcher = new CompProperties_Hatcher();
            chickenHatcher.hatcherDaystoHatch = 3.5f;
            chickenHatcher.hatcherPawn = PawnKindDef.Named("Chicken");
            chickenEgg.comps.Add(chickenHatcher);
            chickenEgg.tickerType = TickerType.Normal;


            // tables can explode when damaged
            CompProperties_Explosive explodeComp = new CompProperties_Explosive();
            explodeComp.wickTicks = new IntRange(100, 350);
            explodeComp.explosiveRadius = 4.5f;
            explodeComp.explosiveDamageType = DamageDefOf.Bomb;
            explodeComp.chanceNeverExplodeFromDamage = 0.65f;
            DefDatabase<ThingDef>.GetNamed("Table1x2c").comps.Add(explodeComp);
            DefDatabase<ThingDef>.GetNamed("Table2x2c").comps.Add(explodeComp);
            DefDatabase<ThingDef>.GetNamed("Table2x4c").comps.Add(explodeComp);
            DefDatabase<ThingDef>.GetNamed("Table3x3c").comps.Add(explodeComp);


            // turkey eggs hatch into ducks and vice versa
            ((CompProperties_Hatcher)DefDatabase<ThingDef>.GetNamed("EggDuckFertilized").comps.Where(c => c.GetType() == typeof(CompProperties_Hatcher)).FirstOrDefault()).hatcherPawn = PawnKindDef.Named("Turkey");
            ((CompProperties_Hatcher)DefDatabase<ThingDef>.GetNamed("EggTurkeyFertilized").comps.Where(c => c.GetType() == typeof(CompProperties_Hatcher)).FirstOrDefault()).hatcherPawn = PawnKindDef.Named("Duck");


            // rats make glitterworld medicine sound
            ThingDef ratDef = DefDatabase<ThingDef>.GetNamed("Rat");
            ratDef.race.lifeStageAges.Last().soundAngry = SoundDefOf.TechMedicineUsed;
            ratDef.race.lifeStageAges.Last().soundCall = SoundDefOf.TechMedicineUsed;


            // weed gives food poisoning
            StatModifier foodPoisonStat = new StatModifier();
            foodPoisonStat.stat = StatDefOf.FoodPoisonChanceFixedHuman;
            foodPoisonStat.value = 0.1f;
            DefDatabase<ThingDef>.GetNamed("SmokeleafJoint").statBases.Add(foodPoisonStat);


            // swap visual size for adult and puppy labradors, including skeletons and shadows
            PawnKindDef labDef = DefDatabase<PawnKindDef>.GetNamed("LabradorRetriever");
            GraphicData lapPupGraphic = labDef.lifeStages[0].bodyGraphicData;
            labDef.lifeStages[0].bodyGraphicData = labDef.lifeStages[labDef.lifeStages.Count - 1].bodyGraphicData;
            labDef.lifeStages[labDef.lifeStages.Count - 1].bodyGraphicData = lapPupGraphic;


            // invisible male thrumbos (adult only)
            PawnKindDef thrumDef = DefDatabase<PawnKindDef>.GetNamed("Thrumbo");
            GraphicData invisible = thrumDef.lifeStages[0].bodyGraphicData;
            invisible.shadowData = null;
            invisible.texPath = "Things/Invisible";
            thrumDef.lifeStages[thrumDef.lifeStages.Count - 1].femaleGraphicData = thrumDef.lifeStages[thrumDef.lifeStages.Count - 1].bodyGraphicData;
            thrumDef.lifeStages[thrumDef.lifeStages.Count - 1].bodyGraphicData = invisible;


            // juvenile huskies use pig graphic
            PawnKindDef huskyPawnDef = DefDatabase<PawnKindDef>.GetNamed("Husky");
            PawnKindDef pigDef = DefDatabase<PawnKindDef>.GetNamed("Pig");
            huskyPawnDef.lifeStages[1].bodyGraphicData = pigDef.lifeStages[1].bodyGraphicData;
            huskyPawnDef.lifeStages[0].bodyGraphicData = pigDef.lifeStages[0].bodyGraphicData;

            ThingDef shortArrow = DefDatabase<ThingDef>.GetNamed("Arrow_Short");
            shortArrow.projectile.explosionRadius = 2.5f;
            shortArrow.projectile.damageDef = DamageDefOf.EMP;
            shortArrow.projectile.explosionDelay = 150;
            shortArrow.projectile.speed = 56;

            CompProperties_Hatcher oakHatcher = new CompProperties_Hatcher();
            oakHatcher.hatcherDaystoHatch = 30f;
            oakHatcher.hatcherPawn = PawnKindDef.Named("Cow");
            DefDatabase<ThingDef>.GetNamed("Turkey").comps.Add(oakHatcher);




        }
    }
}
