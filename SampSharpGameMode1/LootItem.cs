using System;
using System.Collections.Generic;
using System.Text;
using SampSharp.Core;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Helpers;
using SampSharp.GameMode.World;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.Factories;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP.Commands;

namespace SampSharpGameMode1
{
    public class LootItem
    {
        
        public int Id {get;set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isUsable {  get; set; }
        public int BotSellPrice { get; set; }
        public int SellPrice {  get; set; }
        // 0-noloot 1-usableLoot 2-weapon 3-clothes 4-helmet 5-armour 6-backpack 7-weaponAmmo
        public int LootType { get; set; }
        public bool isDropable { get; set; }
        public int modelId {  get; set; }
        public LootItem(int Id,string Name, string Description, int modelId, bool isUsable, int BotSellPrice, int SellPrice, int LootType, bool isDropable) {
            
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.modelId = modelId;
            this.isUsable = isUsable;
            this.BotSellPrice = BotSellPrice;
            this.SellPrice = SellPrice;
            this.LootType = LootType;
            this.isDropable = isDropable;

        }
        public void UseItem(Player p)
        {
            if(Id == 1)
            {
                p.ApplyAnimation("BAR", "DNK_STNDM_LOOP", 1, false, false, false, false, 2000);
                if(p.waterNumber <= 80)
                {
                    p.waterNumber += 20;
                }
                else
                {
                    p.waterNumber = 100;
                }
            }
            if(Id == 2)
            {
                p.ApplyAnimation("VENDING", "VEND_EAT1_P", 1, false, false, false, false, 2000);
                if (p.FoodNumber <= 90)
                {
                    p.FoodNumber += 10;
                }
                else
                {
                    p.FoodNumber = 100;
                }
            }
        }
        



    }
}
