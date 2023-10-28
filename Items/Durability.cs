using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.IO;

namespace ViralsMod.Items
{
    public class DurabilityGlobalItem : GlobalItem
    {
        public float durability;

        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            if (item.damage > 0)
                return true;
            else
                return false;
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            tag["durability"] = durability;
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            durability = (float)tag["durability"];
        }

        public override void OnCreated(Item item, ItemCreationContext context)
        {
            if (item.damage > 0)
                durability = 100;
        }

        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (durability > 0)
                durability--;

            TagCompound tag = new TagCompound();

            tag["durability"] = durability;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (durability > 0)
            {
                tooltips.Add(new TooltipLine(Mod, "durability", $"Durability: {durability}") { OverrideColor = Color.LightGreen });
            }
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(durability);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            durability = reader.ReadSingle();
        }
    }
}
