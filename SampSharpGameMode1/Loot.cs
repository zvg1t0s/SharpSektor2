using SampSharp.Streamer.World;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SampSharpGameMode1
{
    public class Loot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DynamicObject DynamicObject { get; set; }
        public DynamicTextLabel Label { get; set; }
        public bool isTaked = false;
        // 0-noloot 1-usableLoot 2-weapon 3-clothes 4-helmet 5-armour 6-backpack
        static int LootIdentifier = 0;
        public int Idd = 0;
        public int modelId { get; set; }
        public SampSharp.GameMode.Vector3 position {get;set;}
        public Loot(LootItem lt, SampSharp.GameMode.Vector3 vector, int world)
        {
            Idd = LootIdentifier;
            LootIdentifier++;
            Id = lt.Id;
            Name = lt.Name;
            Description = lt.Description;
            modelId = lt.modelId;
            position = vector;

            DynamicObject = new DynamicObject(modelId, new SampSharp.GameMode.Vector3(vector.X, vector.Y, vector.Z - 0.9), worldid: 0);
            DynamicObject.ShowInWorld(world);
            Label = new DynamicTextLabel($"{{e6dd37}}{Name}",0, new SampSharp.GameMode.Vector3(vector.X, vector.Y, vector.Z - 0.8), 30.0f);
            Label.ShowInWorld(world);

            
           
        }
        public void DeleteLoot()
        {
            DynamicObject.Dispose();
            DynamicObject = null;
            Label.Dispose();
            Label = null;
        }

    }
}
