using System.Collections.Generic;
using Terraria.ModLoader.IO;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.IO;

namespace tmoddurability.Items
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
                float durabilityPercentage = durability / 100f;

                Color color = Color.Lerp(Color.Red, Color.Green, durabilityPercentage);

                TooltipLine durabilityLine = new TooltipLine(Mod, "Durability", $"Durability: {durability}");
                durabilityLine.OverrideColor = color;

                tooltips.Add(durabilityLine);
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
