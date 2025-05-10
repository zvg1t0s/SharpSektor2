using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;
using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using SampSharp;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using System.Numerics;
using System.Timers;
using System.Drawing;
using SampSharp.GameMode;
using System.Collections.Generic;
using System.Diagnostics;
using SampSharp.Streamer.World;

namespace SampSharpGameMode1
{

    [PooledType]
    public class Player : BasePlayer
    {
        MySqlConnection sqlCon = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=Samp");// CONNECTION VAR
        SampSharp.GameMode.Vector3 PlayerSavedPosition = new SampSharp.GameMode.Vector3(0f, 0f, 0f);
        bool isBooms = false;
        public bool isAutorised = false;
        int PlayerBooms = 0;
        public int tankAmount = 0;
        public int waterNumber = 100;
        public int eatNumber = 100;
        public int FoodNumber = 100;
        public int boomsTDID = 0;
        public int pMoney = 0;

        public bool isM4applied = false;
        public bool isDeagleapplied = false;
        public bool isColtapplied = false;
        public bool isAkapplied = false;
        public bool isSpasapplied = false;
        public bool isSawedoffapplied = false;
        public bool isfireechapplied = false;
        public bool isapkkkplied = false;

        public PlayerTextDraw inventoryBackPlate;
        public PlayerTextDraw playerButton;
        public PlayerTextDraw helmetbutton;
        public PlayerTextDraw backpackButton;
        public PlayerTextDraw armourButton;
        public PlayerTextDraw pagerButton;
        public PlayerTextDraw achievementsButton;
        public PlayerTextDraw woodLine;
        public PlayerTextDraw woodInvTxd;
        public PlayerTextDraw rockLine;
        public PlayerTextDraw rockInvTxd;
        public PlayerTextDraw ironLine;
        public PlayerTextDraw ironInvTxd;
        public PlayerTextDraw tcanLine;
        public PlayerTextDraw tkanInvTxd;
        
        public PlayerTextDraw useButton;
        public PlayerTextDraw dropButton;
        public PlayerTextDraw sellButton;
        public PlayerTextDraw infoButton;
        public PlayerTextDraw closeinv;
        public PlayerTextDraw[] weaponslots = new PlayerTextDraw[6]; 
        public PlayerTextDraw[] slots = new PlayerTextDraw[42];
        public PlayerTextDraw[] weaponslotsAmmo = new PlayerTextDraw[6];
        public int[] weaponslotsAmmoNum = new int[6];
        /**
        public PlayerTextDraw slots[1];
        public PlayerTextDraw slots[1];
        public PlayerTextDraw slots[2];
        public PlayerTextDraw slots[3];
        public PlayerTextDraw slots[4];
        public PlayerTextDraw slots[5];
        public PlayerTextDraw slots[6];
        public PlayerTextDraw slots[7];
        public PlayerTextDraw slots[8];
        public PlayerTextDraw slots[9];
        public PlayerTextDraw slots[10];
        public PlayerTextDraw slots[11];
        public PlayerTextDraw slots[12];
        public PlayerTextDraw slots[13];
        public PlayerTextDraw slots[14];
        public PlayerTextDraw slots[15];
        public PlayerTextDraw slots[16];
        public PlayerTextDraw slots[17];
        public PlayerTextDraw slots[18];
        public PlayerTextDraw slots[19];
        public PlayerTextDraw slots[20];
        public PlayerTextDraw slots[21];
        public PlayerTextDraw slots[22];
        public PlayerTextDraw slots[23];
        public PlayerTextDraw slots[24];
        public PlayerTextDraw slots[25];
        public PlayerTextDraw slots[26];
        public PlayerTextDraw slots[27];
        public PlayerTextDraw slots[28];
        public PlayerTextDraw slots[29];
        public PlayerTextDraw slots[30];
        public PlayerTextDraw slots[31];
        public PlayerTextDraw slots[32];
        public PlayerTextDraw slots[33];
        public PlayerTextDraw slots[34];
        public PlayerTextDraw slots[35];
        public PlayerTextDraw slots[36];
        public PlayerTextDraw slots[37];
        public PlayerTextDraw slots[38];
        public PlayerTextDraw slots[39];
        public PlayerTextDraw slots[40];
        public PlayerTextDraw slots[41];
        **/
        public PlayerTextDraw woodnum;
        public PlayerTextDraw rocknum;
        public PlayerTextDraw ironnum;
        public PlayerTextDraw tkannum;

        /**
        public int slot1item = 0;
        public int slot2item = 0;
        public int slot3item = 0;
        public int slot4item = 0;
        public int slot5item = 0;
        public int slot6item = 0;
        public int slot7item = 0;
        public int slot8item = 0;
        public int slot9item = 0;
        public int slot10item = 0;
        public int slot11item = 0;
        public int slot12item = 0;
        public int slot13item = 0;
        public int slot14item = 0;
        public int slot15item = 0;
        public int slot16item = 0;
        public int slot17item = 0;
        public int slot18item = 0;
        public int slot19item = 0;
        public int slot20item = 0;
        public int slot21item = 0;
        public int slot22item = 0;
        public int slot23item = 0;
        public int slot24item = 0;
        public int slot25item = 0;
        public int slot26item = 0;
        public int slot27item = 0;
        public int slot28item = 0;
        public int slot29item = 0;
        public int slot30item = 0;
        public int slot31item = 0;
        public int slot32item = 0;
        public int slot33item = 0;
        public int slot34item = 0;
        public int slot35item = 0;
        public int slot36item = 0;
        public int slot37item = 0;
        public int slot38item = 0;
        public int slot39item = 0;
        public int slot40item = 0;
        public int slot41item = 0;
        public int slot42item = 0;
        **/
        public int[] weaponSlotsInfo = new int[6];
        public bool[] isSlotSelected = new bool[42];

        public int[] slotsinfo = new int[42];

        public int helmetSlot = 0;
        public int ArmorSlot = 0;
        public int backpackSlot = 0;        
        List<LootItem> lootItems = new List<LootItem>();

        //Луты
        /**
        LootItem gazirovka = new LootItem(1,"Газировка", "Восполняет питье",2647,true, 5, 10, 1, true);
        LootItem burger = new LootItem(2,"Бургер", "Утоляет голод", 2703, true, 3, 6,1, true);
        LootItem milk = new LootItem(3,"Молоко", "Бутылка молока", 19570,true,4,8,1, true);
        LootItem mandarin = new LootItem(4, "Мандарин", "Да ну нахуй", 19574, true, 5, 10, 1, true);
        LootItem aptechka = new LootItem(5,"Аптечка", "Восстанавливает здоровье",11738,true,15,25,1,true);
        **/
        public void CreateItems()
        {
            lootItems.Add(new LootItem(1, "Газировка", "Восполняет питье", 2647, true, 5, 10, 1, true));
            lootItems.Add(new LootItem(2, "Бургер", "Утоляет голод", 2703, true, 3, 6, 1, true));
            lootItems.Add(new LootItem(3, "Молоко", "Бутылка молока", 19570, true, 4, 8, 1, true));
            lootItems.Add(new LootItem(4, "Мандарин", "Да ну нахуй", 19574, true, 5, 10, 1, true));
            lootItems.Add(new LootItem(5, "Аптечка", "Восстанавливает здоровье", 11738, true, 15, 25, 1, true));
            lootItems.Add(new LootItem(6, "M4", "надежное оружие, которое может стать непревзойденным помощником на поле боя", 356, false, 2000, 5000, 2, true));
            lootItems.Add(new LootItem(7, "патроны 5.56", "Патроны для М4, 50 ед.", 2041, false, 50,100,7,true));
            lootItems.Add(new LootItem(8, "Desert Eagle", "идеальный выбор для тех, кто ищет сбалансированное сочетание мощи и точности", 348,false, 1000,3000,2,true));
            lootItems.Add(new LootItem(9, "патроны 12.7" ,"Патроны для Desert Eagle 7 ед.", 2039, false, 10, 30,7,true));
            lootItems.Add(new LootItem(10, "Spas-12", "оружие, которое сочетает в себе высокую эффективность на дальних дистанциях и впечатляющую разрушительную силу на ближней", 351, false, 1000, 3000, 2, true));
            lootItems.Add(new LootItem(11, "патроны 18.5", "Патроны для Spas-12 7 ед.", 2038, false, 10, 30, 7, true));
            lootItems.Add(new LootItem(12, "Sawed-off", "идеальный выбор для тех, кто ищет сбалансированное сочетание мощи и точности", 350, false, 1000, 3000, 2, true));
            lootItems.Add(new LootItem(13, "дробь 12 cal", "Патроны для Sawed-off 2 ед.", 2037, false, 10, 30, 7, true));
            lootItems.Add(new LootItem(14, "Shotgun", "Мощное оружие для прицельной стрельбы на средние и ближние дистанции", 349, false, 1000, 3000, 2, true));
            lootItems.Add(new LootItem(15, "дробь 14 cal", "Патроны для Shotgun 8 ед.", 2037, false, 10, 30, 7, true));
            lootItems.Add(new LootItem(16, "Легкий бронежилет", "Простой бронежилет, прочность 100 AP", 1242, false, 100, 500,5,true));
            lootItems.Add(new LootItem(17, "Ремкомплект", "Стандартный набор ремонтника", 19900, true, 100, 250,1,true));
            


        }
        public void UpdateInventorySlotsFromBD()
        {
            

            
            sqlCon.Open();
            var updatehelmetslot = new MySqlCommand($"SELECT helmetSlot` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
            helmetSlot = Convert.ToInt32(updatehelmetslot.ExecuteScalar());
            var updatearmourslot = new MySqlCommand($"SELECT armourSlot` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
            ArmorSlot = Convert.ToInt32(updatearmourslot.ExecuteScalar());
            var updatebackpackslot = new MySqlCommand($"SELECT backpackSlot` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
            backpackSlot = Convert.ToInt32(updatebackpackslot.ExecuteScalar());

            for (int i = 0; i < 5; i++)
            {
                var updateweaponslot = new MySqlCommand($"SELECT `weaponslot{i}` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
                weaponSlotsInfo[i] = Convert.ToInt32(updateweaponslot.ExecuteScalar());
                var updateweaponslotAmmo = new MySqlCommand($"SELECT `weaponslotsAmmo{i}` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
                weaponslotsAmmoNum[i] = Convert.ToInt32(updateweaponslotAmmo.ExecuteScalar());

            }

            var inventoryUpdater = new System.Timers.Timer(1000);
            inventoryUpdater.Elapsed += inventoryUpdaterEvent;
            inventoryUpdater.Enabled = true;
            inventoryUpdater.AutoReset = true;
            void inventoryUpdaterEvent(object source, ElapsedEventArgs e)
            {

                Updater1();
            }
            sqlCon.Close();
        }
        public void Updater1()
        {

            for (int i = 0; i < 41; i++)
            {
                var updateslot = new MySqlCommand($"SELECT `slot{i}` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
                slotsinfo[i] = Convert.ToInt32(updateslot.ExecuteScalar());
            }

            for (int i = 0; i < 5; i++)
            {
                var updateweaponslots = new MySqlCommand($"UPDATE `Players` SET `weaponslot{i}` = '{this.weaponSlotsInfo[i]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                updateweaponslots.ExecuteNonQuery();
                var updateweaponslotsammos = new MySqlCommand($"UPDATE `Players` SET `weaponslotsAmmo{i}` = '{this.weaponslotsAmmoNum[i]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                updateweaponslotsammos.ExecuteNonQuery();
            }
            var updatehelmetslot = new MySqlCommand($"UPDATE `Players` SET `helmetSlot` = '{this.helmetSlot}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
            updatehelmetslot.ExecuteNonQuery();
            var updatearmour= new MySqlCommand($"UPDATE `Players` SET `armourSlot` = '{this.ArmorSlot}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
            updatehelmetslot.ExecuteNonQuery();
            var updatebackpackslot = new MySqlCommand($"UPDATE `Players` SET `backpackSlot` = '{this.backpackSlot}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
            updatebackpackslot.ExecuteNonQuery();
            if(this.Armour <= 0)
            {
                ArmorSlot = 0;
            }
        }
        public void InventoryUpdate()
        {
            if(helmetSlot == 0)
            {
                helmetbutton.PreviewModel = 19478;
                
            }
            else
            {
                helmetbutton.PreviewModel = lootItems.Find(item => item.Id == helmetSlot).modelId;
            }

            if (ArmorSlot == 0)
            {
                armourButton.PreviewModel = 19478;

            }
            else
            {
                armourButton.PreviewModel = lootItems.Find(item => item.Id == ArmorSlot).modelId;
            }

            if (backpackSlot == 0)
            {
                backpackButton.PreviewModel = 19478;

            }
            else
            {
                backpackButton.PreviewModel = lootItems.Find(item => item.Id == backpackSlot).modelId;
            }
            for (int i = 0; i < slotsinfo.Length; i++)
            {
                if (slotsinfo[i] == 0)
                {
                    slots[i].PreviewModel = 19478;
                    slots[i].Show();
                }
                else
                {
                    slots[i].PreviewModel = lootItems.Find(item => item.Id == slotsinfo[i]).modelId;
                    slots[i].Show();

                }
                
                /**
                if (slotsinfo[i] == burger.Id)
                {
                    slots[i].PreviewModel = burger.modelId;
                    slots[i].Show();

                }
                if (slotsinfo[i] == milk.Id)
                {
                    slots[i].PreviewModel = milk.modelId;
                    slots[i].Show();
                }
                if (slotsinfo[i] == mandarin.Id)
                {
                    slots[i].PreviewModel = mandarin.modelId;
                    slots[i].Show();
                }
                if (slotsinfo[i] == aptechka.Id)
                {
                    slots[i].PreviewModel = aptechka.modelId;
                    slots[i].Show();
                }
                **/
            }
            for(int i = 0; i <weaponSlotsInfo.Length; i++) {
                if (weaponSlotsInfo[i] == 0)
                {
                    weaponslots[i].PreviewModel = 19478;
                    weaponslots[i].Show();
                }
                else
                {
                    weaponslots[i].PreviewModel = lootItems.Find(item => item.Id == weaponSlotsInfo[i]).modelId;
                    weaponslots[i].Show();
                    weaponslotsAmmo[i].Text = weaponslotsAmmoNum[i].ToString();
                    weaponslotsAmmo[i].Show();
                }
            }
        }

        public void CreateInventory(Player p) {
            //________________________INVENTORY_________________________________________________________________________
            /**
inventorybackplate[playerid] = CreatePlayerTextDraw(playerid, 476.000000, 110.000000, "_");
PlayerTextDrawFont(playerid, inventorybackplate[playerid], 1);
PlayerTextDrawLetterSize(playerid, inventorybackplate[playerid], 0.600000, 36.599918);
PlayerTextDrawTextSize(playerid, inventorybackplate[playerid], 298.500000, 208.500000);
PlayerTextDrawSetOutline(playerid, inventorybackplate[playerid], 1);
PlayerTextDrawSetShadow(playerid, inventorybackplate[playerid], 0);
PlayerTextDrawAlignment(playerid, inventorybackplate[playerid], 2);
PlayerTextDrawColor(playerid, inventorybackplate[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, inventorybackplate[playerid], 255);
PlayerTextDrawBoxColor(playerid, inventorybackplate[playerid], 181);
PlayerTextDrawUseBox(playerid, inventorybackplate[playerid], 1);
PlayerTextDrawSetProportional(playerid, inventorybackplate[playerid], 1);
PlayerTextDrawSetSelectable(playerid, inventorybackplate[playerid], 0);
**/



            inventoryBackPlate = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(476.0f, 110.0f), "_");
            inventoryBackPlate.Font = TextDrawFont.Normal;
            inventoryBackPlate.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 36.599918f);
            inventoryBackPlate.Width = 298.5f;
            inventoryBackPlate.Height = 208.5f;
            inventoryBackPlate.Outline = 1;
            inventoryBackPlate.Shadow = 0;
            inventoryBackPlate.Alignment = TextDrawAlignment.Center;
            inventoryBackPlate.ForeColor = -1;
            inventoryBackPlate.BackColor = 255;
            inventoryBackPlate.BoxColor = 181;
            inventoryBackPlate.UseBox = true;
            inventoryBackPlate.Proportional = true;
            inventoryBackPlate.Selectable = false;



            /**
playerbutton[playerid] = CreatePlayerTextDraw(playerid, 404.000000, 114.000000, "Preview_Model");
PlayerTextDrawFont(playerid, playerbutton[playerid], 5);
PlayerTextDrawLetterSize(playerid, playerbutton[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, playerbutton[playerid], 62.500000, 93.500000);
PlayerTextDrawSetOutline(playerid, playerbutton[playerid], 0);
PlayerTextDrawSetShadow(playerid, playerbutton[playerid], 0);
PlayerTextDrawAlignment(playerid, playerbutton[playerid], 1);
PlayerTextDrawColor(playerid, playerbutton[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, playerbutton[playerid], -1094795651);
PlayerTextDrawBoxColor(playerid, playerbutton[playerid], -16777103);
PlayerTextDrawUseBox(playerid, playerbutton[playerid], 0);
PlayerTextDrawSetProportional(playerid, playerbutton[playerid], 1);
PlayerTextDrawSetSelectable(playerid, playerbutton[playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, playerbutton[playerid], 285);
PlayerTextDrawSetPreviewRot(playerid, playerbutton[playerid], -9.000000, 0.000000, -2.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, playerbutton[playerid], 1, 1);
            **/
            playerButton = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(404.0f, 114.0f), "Preview_Model");
            playerButton.Font = TextDrawFont.PreviewModel;
            playerButton.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            playerButton.Width = 62.5f;
            playerButton.Height = 93.5f;
            playerButton.Outline = 0;
            playerButton.Shadow = 0;
            playerButton.Alignment = TextDrawAlignment.Left;
            playerButton.ForeColor = -1;
            playerButton.BackColor = -1094795651;
            playerButton.BoxColor = -16777103;
            playerButton.UseBox = false;
            playerButton.Proportional = true;
            playerButton.Selectable = true;
            playerButton.PreviewModel = 285;
            playerButton.PreviewRotation = new SampSharp.GameMode.Vector3(-9.0, 0.0, -2.0);
            playerButton.PreviewZoom = 1;


            /**
helmetTD[playerid] = CreatePlayerTextDraw(playerid, 375.000000, 114.000000, "Preview_Model");
PlayerTextDrawFont(playerid, helmetTD[playerid], 5);
PlayerTextDrawLetterSize(playerid, helmetTD[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, helmetTD[playerid], 25.500000, 27.500000);
PlayerTextDrawSetOutline(playerid, helmetTD[playerid], 0);
PlayerTextDrawSetShadow(playerid, helmetTD[playerid], 0);
PlayerTextDrawAlignment(playerid, helmetTD[playerid], 1);
PlayerTextDrawColor(playerid, helmetTD[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, helmetTD[playerid], -1094795651);
PlayerTextDrawBoxColor(playerid, helmetTD[playerid], -16777103);
PlayerTextDrawUseBox(playerid, helmetTD[playerid], 0);
PlayerTextDrawSetProportional(playerid, helmetTD[playerid], 1);
PlayerTextDrawSetSelectable(playerid, helmetTD[playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, helmetTD[playerid], 19141);
PlayerTextDrawSetPreviewRot(playerid, helmetTD[playerid], -3.000000, -90.000000, 15.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, helmetTD[playerid], 1, 1);
**/
            helmetbutton = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(375.0f, 114.0f), "Preview_Model");
            helmetbutton.Font = TextDrawFont.PreviewModel;
            helmetbutton.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            helmetbutton.Width = 25.5f;
            helmetbutton.Height = 27.5f;
            helmetbutton.Outline = 0;
            helmetbutton.Shadow = 0;
            helmetbutton.Alignment = TextDrawAlignment.Left;
            helmetbutton.ForeColor = -1;
            helmetbutton.BackColor = -1094795651;
            helmetbutton.BoxColor = -16777103;
            helmetbutton.UseBox = false;
            helmetbutton.Proportional = true;
            helmetbutton.Selectable = true;
            helmetbutton.PreviewModel = 19141;
            helmetbutton.PreviewRotation = new SampSharp.GameMode.Vector3(-3.0, -90.0, 15.0);
            helmetbutton.PreviewZoom = 1;


            /**
rukzak[playerid] = CreatePlayerTextDraw(playerid, 375.000000, 180.000000, "Preview_Model");
PlayerTextDrawFont(playerid, rukzak[playerid], 5);
PlayerTextDrawLetterSize(playerid, rukzak[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, rukzak[playerid], 25.500000, 27.500000);
PlayerTextDrawSetOutline(playerid, rukzak[playerid], 0);
PlayerTextDrawSetShadow(playerid, rukzak[playerid], 0);
PlayerTextDrawAlignment(playerid, rukzak[playerid], 1);
PlayerTextDrawColor(playerid, rukzak[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, rukzak[playerid], -1094795651);
PlayerTextDrawBoxColor(playerid, rukzak[playerid], -16777103);
PlayerTextDrawUseBox(playerid, rukzak[playerid], 0);
PlayerTextDrawSetProportional(playerid, rukzak[playerid], 1);
PlayerTextDrawSetSelectable(playerid, rukzak[playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, rukzak[playerid], 371);
PlayerTextDrawSetPreviewRot(playerid, rukzak[playerid], -9.000000, 0.000000, -2.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, rukzak[playerid], 1, 1);
**/
            backpackButton = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(375.0f, 180.0f), "Preview_Model");
            backpackButton.Font = TextDrawFont.PreviewModel;
            backpackButton.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            backpackButton.Width = 25.5f;
            backpackButton.Height = 27.5f;
            backpackButton.Outline = 0;
            backpackButton.Shadow = 0;
            backpackButton.Alignment = TextDrawAlignment.Left;
            backpackButton.ForeColor = -1;
            backpackButton.BackColor = -1094795651;
            backpackButton.BoxColor = -16777103;
            backpackButton.UseBox = false;
            backpackButton.Proportional = true;
            backpackButton.Selectable = true;
            backpackButton.PreviewModel = 371;
            backpackButton.PreviewRotation = new SampSharp.GameMode.Vector3(-9.0, 0.0, -2.0);
            backpackButton.PreviewZoom = 1;

            /**
ARMOURTD[playerid] = CreatePlayerTextDraw(playerid, 375.000000, 147.000000, "Preview_Model");
PlayerTextDrawFont(playerid, ARMOURTD[playerid], 5);
PlayerTextDrawLetterSize(playerid, ARMOURTD[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, ARMOURTD[playerid], 25.500000, 27.500000);
PlayerTextDrawSetOutline(playerid, ARMOURTD[playerid], 0);
PlayerTextDrawSetShadow(playerid, ARMOURTD[playerid], 0);
PlayerTextDrawAlignment(playerid, ARMOURTD[playerid], 1);
PlayerTextDrawColor(playerid, ARMOURTD[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, ARMOURTD[playerid], -1094795651);
PlayerTextDrawBoxColor(playerid, ARMOURTD[playerid], -16777103);
PlayerTextDrawUseBox(playerid, ARMOURTD[playerid], 0);
PlayerTextDrawSetProportional(playerid, ARMOURTD[playerid], 1);
PlayerTextDrawSetSelectable(playerid, ARMOURTD[playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, ARMOURTD[playerid], 1242);
PlayerTextDrawSetPreviewRot(playerid, ARMOURTD[playerid], -9.000000, 0.000000, -2.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, ARMOURTD[playerid], 1, 1);
            **/
            armourButton = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(375.0f, 147.0f), "Preview_Model");
            armourButton.Font = TextDrawFont.PreviewModel;
            armourButton.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            armourButton.Width = 25.5f;
            armourButton.Height = 27.5f;
            armourButton.Outline = 0;
            armourButton.Shadow = 0;
            armourButton.Alignment = TextDrawAlignment.Left;
            armourButton.ForeColor = -1;
            armourButton.BackColor = -1094795651;
            armourButton.BoxColor = -16777103;
            armourButton.UseBox = false;
            armourButton.Proportional = true;
            armourButton.Selectable = true;
            armourButton.PreviewModel = 1242;
            armourButton.PreviewRotation = new SampSharp.GameMode.Vector3(-9.0, 0.0, -2.0);
            armourButton.PreviewZoom = 1;


            /**
pagertxd[playerid] = CreatePlayerTextDraw(playerid, 407.000000, 186.000000, "Preview_Model");
PlayerTextDrawFont(playerid, pagertxd[playerid], 5);
PlayerTextDrawLetterSize(playerid, pagertxd[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, pagertxd[playerid], 17.000000, 17.500000);
PlayerTextDrawSetOutline(playerid, pagertxd[playerid], 0);
PlayerTextDrawSetShadow(playerid, pagertxd[playerid], 0);
PlayerTextDrawAlignment(playerid, pagertxd[playerid], 1);
PlayerTextDrawColor(playerid, pagertxd[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, pagertxd[playerid], 125);
PlayerTextDrawBoxColor(playerid, pagertxd[playerid], -16777103);
PlayerTextDrawUseBox(playerid, pagertxd[playerid], 0);
PlayerTextDrawSetProportional(playerid, pagertxd[playerid], 1);
PlayerTextDrawSetSelectable(playerid, pagertxd[playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, pagertxd[playerid], 18875);
PlayerTextDrawSetPreviewRot(playerid, pagertxd[playerid], -81.000000, 0.000000, -179.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, pagertxd[playerid], 1, 1);
**/
            pagerButton = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(407.0f, 186.0f), "Preview_Model");
            pagerButton.Font = TextDrawFont.PreviewModel;
            pagerButton.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            pagerButton.Width = 17.0f;
            pagerButton.Height = 17.5f;
            pagerButton.Outline = 0;
            pagerButton.Shadow = 0;
            pagerButton.Alignment = TextDrawAlignment.Left;
            pagerButton.ForeColor = -1;
            pagerButton.BackColor = 125;
            pagerButton.BoxColor = -16777103;
            pagerButton.UseBox = false;
            pagerButton.Proportional = true;
            pagerButton.Selectable = true;
            pagerButton.PreviewModel = 18875;
            pagerButton.PreviewRotation = new SampSharp.GameMode.Vector3(-81.0, 0.0, -179.0);
            pagerButton.PreviewZoom = 1;


            /**
achievementsbutton[playerid] = CreatePlayerTextDraw(playerid, 447.000000, 186.000000, "Preview_Model");
PlayerTextDrawFont(playerid, achievementsbutton[playerid], 5);
PlayerTextDrawLetterSize(playerid, achievementsbutton[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, achievementsbutton[playerid], 17.000000, 17.500000);
PlayerTextDrawSetOutline(playerid, achievementsbutton[playerid], 0);
PlayerTextDrawSetShadow(playerid, achievementsbutton[playerid], 0);
PlayerTextDrawAlignment(playerid, achievementsbutton[playerid], 1);
PlayerTextDrawColor(playerid, achievementsbutton[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, achievementsbutton[playerid], 125);
PlayerTextDrawBoxColor(playerid, achievementsbutton[playerid], -16777103);
PlayerTextDrawUseBox(playerid, achievementsbutton[playerid], 0);
PlayerTextDrawSetProportional(playerid, achievementsbutton[playerid], 1);
PlayerTextDrawSetSelectable(playerid, achievementsbutton[playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, achievementsbutton[playerid], 19941);
PlayerTextDrawSetPreviewRot(playerid, achievementsbutton[playerid], -81.000000, 0.000000, -179.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, achievementsbutton[playerid], 1, 1);
**/
            achievementsButton = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(447.0f, 186.0f), "Preview_Model");
            achievementsButton.Font = TextDrawFont.PreviewModel;
            achievementsButton.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            achievementsButton.Width = 17.0f;
            achievementsButton.Height = 17.5f;
            achievementsButton.Outline = 0;
            achievementsButton.Shadow = 0;
            achievementsButton.Alignment = TextDrawAlignment.Left;
            achievementsButton.ForeColor = -1;
            achievementsButton.BackColor = 125;
            achievementsButton.BoxColor = -16777103;
            achievementsButton.UseBox = false;
            achievementsButton.Proportional = true;
            achievementsButton.Selectable = true;
            achievementsButton.PreviewModel = 19941;
            achievementsButton.PreviewRotation = new SampSharp.GameMode.Vector3(-81.0, 0.0, -179.0);
            achievementsButton.PreviewZoom = 1;


            /**
WoodInvTXD[playerid] = CreatePlayerTextDraw(playerid, 476.000000, 111.000000, "Preview_Model");
PlayerTextDrawFont(playerid, WoodInvTXD[playerid], 5);
PlayerTextDrawLetterSize(playerid, WoodInvTXD[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, WoodInvTXD[playerid], 30.000000, 35.000000);
PlayerTextDrawSetOutline(playerid, WoodInvTXD[playerid], 0);
PlayerTextDrawSetShadow(playerid, WoodInvTXD[playerid], 0);
PlayerTextDrawAlignment(playerid, WoodInvTXD[playerid], 1);
PlayerTextDrawColor(playerid, WoodInvTXD[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, WoodInvTXD[playerid], 0);
PlayerTextDrawBoxColor(playerid, WoodInvTXD[playerid], -16777103);
PlayerTextDrawUseBox(playerid, WoodInvTXD[playerid], 0);
PlayerTextDrawSetProportional(playerid, WoodInvTXD[playerid], 1);
PlayerTextDrawSetSelectable(playerid, WoodInvTXD[playerid], 0);
PlayerTextDrawSetPreviewModel(playerid, WoodInvTXD[playerid], 1463);
PlayerTextDrawSetPreviewRot(playerid, WoodInvTXD[playerid], -12.000000, 2.000000, 44.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, WoodInvTXD[playerid], 1, 1);
**/
            /**
            woodline[playerid] = CreatePlayerTextDraw(playerid, 533.000000, 127.000000, "_");
            PlayerTextDrawFont(playerid, woodline[playerid], 1);
            PlayerTextDrawLetterSize(playerid, woodline[playerid], 0.600000, 0.550001);
            PlayerTextDrawTextSize(playerid, woodline[playerid], 298.500000, 75.000000);
            PlayerTextDrawSetOutline(playerid, woodline[playerid], 1);
            PlayerTextDrawSetShadow(playerid, woodline[playerid], 0);
            PlayerTextDrawAlignment(playerid, woodline[playerid], 2);
            PlayerTextDrawColor(playerid, woodline[playerid], -1);
            PlayerTextDrawBackgroundColor(playerid, woodline[playerid], 255);
            PlayerTextDrawBoxColor(playerid, woodline[playerid], 1783367679);
            PlayerTextDrawUseBox(playerid, woodline[playerid], 1);
            PlayerTextDrawSetProportional(playerid, woodline[playerid], 1);
            PlayerTextDrawSetSelectable(playerid, woodline[playerid], 0);
            **/
            woodLine = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(533.0f, 127.0f), "_");
            woodLine.Font = TextDrawFont.Normal;
            woodLine.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 0.550001f);
            woodLine.Width = 298.5f;
            woodLine.Height = 75.0f;
            woodLine.Outline = 1;
            woodLine.Shadow = 0;
            woodLine.Alignment = TextDrawAlignment.Center;
            woodLine.ForeColor = -1;
            woodLine.BackColor = 255;
            woodLine.BoxColor = 1783367679;
            woodLine.UseBox = true;
            woodLine.Proportional = true;
            woodLine.Selectable = false;



            woodInvTxd = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(476.0f, 111.0f), "Preview_Model");
            woodInvTxd.Font = TextDrawFont.PreviewModel;
            woodInvTxd.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            woodInvTxd.Width = 30.0f;
            woodInvTxd.Height = 35.0f;
            woodInvTxd.Outline = 0;
            woodInvTxd.Shadow = 0;
            woodInvTxd.Alignment = TextDrawAlignment.Left;
            woodInvTxd.ForeColor = -1;
            woodInvTxd.BackColor = 0;
            woodInvTxd.BoxColor = -16777103;
            woodInvTxd.UseBox = false;
            woodInvTxd.Proportional = true;
            woodInvTxd.Selectable = false;
            woodInvTxd.PreviewModel = 1463;
            woodInvTxd.PreviewRotation = new SampSharp.GameMode.Vector3(-12.0, 2.0, 44.0);
            woodInvTxd.PreviewZoom = 1;




            /**
RockInvTXD[playerid] = CreatePlayerTextDraw(playerid, 478.000000, 139.000000, "Preview_Model");
PlayerTextDrawFont(playerid, RockInvTXD[playerid], 5);
PlayerTextDrawLetterSize(playerid, RockInvTXD[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, RockInvTXD[playerid], 23.500000, 21.500000);
PlayerTextDrawSetOutline(playerid, RockInvTXD[playerid], 0);
PlayerTextDrawSetShadow(playerid, RockInvTXD[playerid], 0);
PlayerTextDrawAlignment(playerid, RockInvTXD[playerid], 1);
PlayerTextDrawColor(playerid, RockInvTXD[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, RockInvTXD[playerid], 0);
PlayerTextDrawBoxColor(playerid, RockInvTXD[playerid], -16777103);
PlayerTextDrawUseBox(playerid, RockInvTXD[playerid], 0);
PlayerTextDrawSetProportional(playerid, RockInvTXD[playerid], 1);
PlayerTextDrawSetSelectable(playerid, RockInvTXD[playerid], 0);
PlayerTextDrawSetPreviewModel(playerid, RockInvTXD[playerid], 3929);
PlayerTextDrawSetPreviewRot(playerid, RockInvTXD[playerid], -12.000000, 2.000000, 44.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, RockInvTXD[playerid], 1, 1);
**/
            /**
rockline[playerid] = CreatePlayerTextDraw(playerid, 533.000000, 147.000000, "_");
PlayerTextDrawFont(playerid, rockline[playerid], 1);
PlayerTextDrawLetterSize(playerid, rockline[playerid], 0.600000, 0.550001);
PlayerTextDrawTextSize(playerid, rockline[playerid], 298.500000, 75.000000);
PlayerTextDrawSetOutline(playerid, rockline[playerid], 1);
PlayerTextDrawSetShadow(playerid, rockline[playerid], 0);
PlayerTextDrawAlignment(playerid, rockline[playerid], 2);
PlayerTextDrawColor(playerid, rockline[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, rockline[playerid], 255);
PlayerTextDrawBoxColor(playerid, rockline[playerid], -741092353);
PlayerTextDrawUseBox(playerid, rockline[playerid], 1);
PlayerTextDrawSetProportional(playerid, rockline[playerid], 1);
PlayerTextDrawSetSelectable(playerid, rockline[playerid], 0);
**/
            rockLine = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(533.0f, 147.0f), "_");
            rockLine.Font = TextDrawFont.Normal;
            rockLine.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 0.550001f);
            rockLine.Width = 298.5f;
            rockLine.Height = 75.0f;
            rockLine.Outline = 1;
            rockLine.Shadow = 0;
            rockLine.Alignment = TextDrawAlignment.Center;
            rockLine.ForeColor = -1;
            rockLine.BackColor = 255;
            rockLine.BoxColor = -741092353;
            rockLine.UseBox = true;
            rockLine.Proportional = true;
            rockLine.Selectable = false;

            rockInvTxd = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(478.0f, 139.0f), "Preview_Model");
            rockInvTxd.Font = TextDrawFont.PreviewModel;
            rockInvTxd.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            rockInvTxd.Width = 23.5f;
            rockInvTxd.Height = 21.5f;
            rockInvTxd.Outline = 0;
            rockInvTxd.Shadow = 0;
            rockInvTxd.Alignment = TextDrawAlignment.Left;
            rockInvTxd.ForeColor = -1;
            rockInvTxd.BackColor = 0;
            rockInvTxd.BoxColor = -16777103;
            rockInvTxd.UseBox = false;
            rockInvTxd.Proportional = true;
            rockInvTxd.Selectable = false;
            rockInvTxd.PreviewModel = 3929;
            rockInvTxd.PreviewRotation = new SampSharp.GameMode.Vector3(-12.0, 2.0, 44.0);
            rockInvTxd.PreviewZoom = 1;

            /**
IronInvTXD[playerid] = CreatePlayerTextDraw(playerid, 475.000000, 153.000000, "Preview_Model");
PlayerTextDrawFont(playerid, IronInvTXD[playerid], 5);
PlayerTextDrawLetterSize(playerid, IronInvTXD[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, IronInvTXD[playerid], 31.500000, 32.500000);
PlayerTextDrawSetOutline(playerid, IronInvTXD[playerid], 0);
PlayerTextDrawSetShadow(playerid, IronInvTXD[playerid], 0);
PlayerTextDrawAlignment(playerid, IronInvTXD[playerid], 1);
PlayerTextDrawColor(playerid, IronInvTXD[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, IronInvTXD[playerid], 0);
PlayerTextDrawBoxColor(playerid, IronInvTXD[playerid], -16777103);
PlayerTextDrawUseBox(playerid, IronInvTXD[playerid], 0);
PlayerTextDrawSetProportional(playerid, IronInvTXD[playerid], 1);
PlayerTextDrawSetSelectable(playerid, IronInvTXD[playerid], 0);
PlayerTextDrawSetPreviewModel(playerid, IronInvTXD[playerid], 2936);
PlayerTextDrawSetPreviewRot(playerid, IronInvTXD[playerid], -12.000000, 2.000000, 44.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, IronInvTXD[playerid], 1, 1);
**/
            /**
ironline[playerid] = CreatePlayerTextDraw(playerid, 533.000000, 168.000000, "_");
PlayerTextDrawFont(playerid, ironline[playerid], 1);
PlayerTextDrawLetterSize(playerid, ironline[playerid], 0.600000, 0.550001);
PlayerTextDrawTextSize(playerid, ironline[playerid], 298.500000, 75.000000);
PlayerTextDrawSetOutline(playerid, ironline[playerid], 1);
PlayerTextDrawSetShadow(playerid, ironline[playerid], 0);
PlayerTextDrawAlignment(playerid, ironline[playerid], 2);
PlayerTextDrawColor(playerid, ironline[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, ironline[playerid], 255);
PlayerTextDrawBoxColor(playerid, ironline[playerid], -764862721);
PlayerTextDrawUseBox(playerid, ironline[playerid], 1);
PlayerTextDrawSetProportional(playerid, ironline[playerid], 1);
PlayerTextDrawSetSelectable(playerid, ironline[playerid], 0);
**/
            ironLine = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(533.0f, 168.0f), "_");
            ironLine.Font = TextDrawFont.Normal;
            ironLine.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 0.550001f);
            ironLine.Width = 298.5f;
            ironLine.Height = 75.0f;
            ironLine.Outline = 1;
            ironLine.Shadow = 0;
            ironLine.Alignment = TextDrawAlignment.Center;
            ironLine.ForeColor = -1;
            ironLine.BackColor = 255;
            ironLine.BoxColor = -764862721;
            ironLine.UseBox = true;
            ironLine.Proportional = true;
            ironLine.Selectable = false;
            ironInvTxd = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(475.0f, 153.0f), "Preview_Model");
            ironInvTxd.Font = TextDrawFont.PreviewModel;
            ironInvTxd.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            ironInvTxd.Width = 31.5f;
            ironInvTxd.Height = 32.5f;
            ironInvTxd.Outline = 0;
            ironInvTxd.Shadow = 0;
            ironInvTxd.Alignment = TextDrawAlignment.Left;
            ironInvTxd.ForeColor = -1;
            ironInvTxd.BackColor = 0;
            ironInvTxd.BoxColor = -16777103;
            ironInvTxd.UseBox = false;
            ironInvTxd.Proportional = true;
            ironInvTxd.Selectable = false;
            ironInvTxd.PreviewModel = 2936;
            ironInvTxd.PreviewRotation = new SampSharp.GameMode.Vector3(-12.0, 2.0, 44.0);
            ironInvTxd.PreviewZoom = 1;

            /**
tcanline[playerid] = CreatePlayerTextDraw(playerid, 533.000000, 188.000000, "_");
PlayerTextDrawFont(playerid, tcanline[playerid], 1);
PlayerTextDrawLetterSize(playerid, tcanline[playerid], 0.600000, 0.550001);
PlayerTextDrawTextSize(playerid, tcanline[playerid], 298.500000, 75.000000);
PlayerTextDrawSetOutline(playerid, tcanline[playerid], 1);
PlayerTextDrawSetShadow(playerid, tcanline[playerid], 0);
PlayerTextDrawAlignment(playerid, tcanline[playerid], 2);
PlayerTextDrawColor(playerid, tcanline[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, tcanline[playerid], -2016478465);
PlayerTextDrawBoxColor(playerid, tcanline[playerid], 1687547391);
PlayerTextDrawUseBox(playerid, tcanline[playerid], 1);
PlayerTextDrawSetProportional(playerid, tcanline[playerid], 1);
PlayerTextDrawSetSelectable(playerid, tcanline[playerid], 0);
**/
            tcanLine = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(533.0f, 188.0f), "_");
            tcanLine.Font = TextDrawFont.Normal;
            tcanLine.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 0.550001f);
            tcanLine.Width = 298.5f;
            tcanLine.Height = 75.0f;
            tcanLine.Outline = 1;
            tcanLine.Shadow = 0;
            tcanLine.Alignment = TextDrawAlignment.Center;
            tcanLine.ForeColor = -1;
            tcanLine.BackColor = -2016478465;
            tcanLine.BoxColor = 1687547391;
            tcanLine.UseBox = true;
            tcanLine.Proportional = true;
            tcanLine.Selectable = false;

            /**
tkaninvtxd[playerid] = CreatePlayerTextDraw(playerid, 473.000000, 173.000000, "Preview_Model");
PlayerTextDrawFont(playerid, tkaninvtxd[playerid], 5);
PlayerTextDrawLetterSize(playerid, tkaninvtxd[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, tkaninvtxd[playerid], 36.500000, 31.500000);
PlayerTextDrawSetOutline(playerid, tkaninvtxd[playerid], 0);
PlayerTextDrawSetShadow(playerid, tkaninvtxd[playerid], 0);
PlayerTextDrawAlignment(playerid, tkaninvtxd[playerid], 1);
PlayerTextDrawColor(playerid, tkaninvtxd[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, tkaninvtxd[playerid], 0);
PlayerTextDrawBoxColor(playerid, tkaninvtxd[playerid], -16777103);
PlayerTextDrawUseBox(playerid, tkaninvtxd[playerid], 0);
PlayerTextDrawSetProportional(playerid, tkaninvtxd[playerid], 1);
PlayerTextDrawSetSelectable(playerid, tkaninvtxd[playerid], 0);
PlayerTextDrawSetPreviewModel(playerid, tkaninvtxd[playerid], 19518);
PlayerTextDrawSetPreviewRot(playerid, tkaninvtxd[playerid], 79.000000, 2.000000, 17.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, tkaninvtxd[playerid], 1, 1);
**/
            tkanInvTxd = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(473.0f, 173.0f), "Preview_Model");
            tkanInvTxd.Font = TextDrawFont.PreviewModel;
            tkanInvTxd.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            tkanInvTxd.Width = 36.5f;
            tkanInvTxd.Height = 31.5f;
            tkanInvTxd.Outline = 0;
            tkanInvTxd.Shadow = 0;
            tkanInvTxd.Alignment = TextDrawAlignment.Left;
            tkanInvTxd.ForeColor = -1;
            tkanInvTxd.BackColor = 0;
            tkanInvTxd.BoxColor = -16777103;
            tkanInvTxd.UseBox = false;
            tkanInvTxd.Proportional = true;
            tkanInvTxd.Selectable = false;
            tkanInvTxd.PreviewModel = 19518;
            tkanInvTxd.PreviewRotation = new SampSharp.GameMode.Vector3(79.0, 2.0, 17.0);
            tkanInvTxd.PreviewZoom = 1;

            /**
weapon1[playerid] = CreatePlayerTextDraw(playerid, 375.000000, 210.000000, "Preview_Model");
PlayerTextDrawFont(playerid, weapon1[playerid], 5);
PlayerTextDrawLetterSize(playerid, weapon1[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, weapon1[playerid], 27.500000, 18.500000);
PlayerTextDrawSetOutline(playerid, weapon1[playerid], 0);
PlayerTextDrawSetShadow(playerid, weapon1[playerid], 0);
PlayerTextDrawAlignment(playerid, weapon1[playerid], 1);
PlayerTextDrawColor(playerid, weapon1[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, weapon1[playerid], -1094795651);
PlayerTextDrawBoxColor(playerid, weapon1[playerid], -16777103);
PlayerTextDrawUseBox(playerid, weapon1[playerid], 0);
PlayerTextDrawSetProportional(playerid, weapon1[playerid], 1);
PlayerTextDrawSetSelectable(playerid, weapon1[playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, weapon1[playerid], 348);
PlayerTextDrawSetPreviewRot(playerid, weapon1[playerid], 3.000000, 0.000000, -131.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, weapon1[playerid], 1, 1);
**/
            weaponslots[0] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(375.0f, 210.0f), "Preview_Model");
            weaponslots[0].Font = TextDrawFont.PreviewModel;
            weaponslots[0].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            weaponslots[0].Width = 27.5f;
            weaponslots[0].Height = 18.5f;
            weaponslots[0].Outline = 0;
            weaponslots[0].Shadow = 0;
            weaponslots[0].Alignment = TextDrawAlignment.Left;
            weaponslots[0].ForeColor = -1;
            weaponslots[0].BackColor = -1094795651;
            weaponslots[0].BoxColor = -16777103;
            weaponslots[0].UseBox = false;
            weaponslots[0].Proportional = true;
            weaponslots[0].Selectable = true;
            weaponslots[0].PreviewModel = 348;
            weaponslots[0].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            weaponslots[0].PreviewZoom = 1;

            /**
weapon2[playerid] = CreatePlayerTextDraw(playerid, 407.000000, 210.000000, "Preview_Model");
PlayerTextDrawFont(playerid, weapon2[playerid], 5);
PlayerTextDrawLetterSize(playerid, weapon2[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, weapon2[playerid], 27.500000, 18.500000);
PlayerTextDrawSetOutline(playerid, weapon2[playerid], 0);
PlayerTextDrawSetShadow(playerid, weapon2[playerid], 0);
PlayerTextDrawAlignment(playerid, weapon2[playerid], 1);
PlayerTextDrawColor(playerid, weapon2[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, weapon2[playerid], -1094795651);
PlayerTextDrawBoxColor(playerid, weapon2[playerid], -16777103);
PlayerTextDrawUseBox(playerid, weapon2[playerid], 0);
PlayerTextDrawSetProportional(playerid, weapon2[playerid], 1);
PlayerTextDrawSetSelectable(playerid, weapon2[playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, weapon2[playerid], 351);
PlayerTextDrawSetPreviewRot(playerid, weapon2[playerid], 3.000000, 0.000000, -131.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, weapon2[playerid], 1, 1);
**/
            weaponslots[1] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(407.0f, 210.0f), "Preview_Model");
            weaponslots[1].Font = TextDrawFont.PreviewModel;
            weaponslots[1].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            weaponslots[1].Width = 27.5f;
            weaponslots[1].Height = 18.5f;
            weaponslots[1].Outline = 0;
            weaponslots[1].Shadow = 0;
            weaponslots[1].Alignment = TextDrawAlignment.Left;
            weaponslots[1].ForeColor = -1;
            weaponslots[1].BackColor = -1094795651;
            weaponslots[1].BoxColor = -16777103;
            weaponslots[1].UseBox = false;
            weaponslots[1].Proportional = true;
            weaponslots[1].Selectable = true;
            weaponslots[1].PreviewModel = 351;
            weaponslots[1].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            weaponslots[1].PreviewZoom = 1;

            /**
weapon3[playerid] = CreatePlayerTextDraw(playerid, 439.000000, 210.000000, "Preview_Model");
PlayerTextDrawFont(playerid, weapon3[playerid], 5);
PlayerTextDrawLetterSize(playerid, weapon3[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, weapon3[playerid], 27.500000, 18.500000);
PlayerTextDrawSetOutline(playerid, weapon3[playerid], 0);
PlayerTextDrawSetShadow(playerid, weapon3[playerid], 0);
PlayerTextDrawAlignment(playerid, weapon3[playerid], 1);
PlayerTextDrawColor(playerid, weapon3[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, weapon3[playerid], -1094795651);
PlayerTextDrawBoxColor(playerid, weapon3[playerid], -16777103);
PlayerTextDrawUseBox(playerid, weapon3[playerid], 0);
PlayerTextDrawSetProportional(playerid, weapon3[playerid], 1);
PlayerTextDrawSetSelectable(playerid, weapon3[playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, weapon3[playerid], 353);
PlayerTextDrawSetPreviewRot(playerid, weapon3[playerid], 3.000000, 0.000000, -131.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, weapon3[playerid], 1, 1);

**/
            weaponslots[2] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(439.0f, 210.0f), "Preview_Model");
            weaponslots[2].Font = TextDrawFont.PreviewModel;
            weaponslots[2].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            weaponslots[2].Width = 27.5f;
            weaponslots[2].Height = 18.5f;
            weaponslots[2].Outline = 0;
            weaponslots[2].Shadow = 0;
            weaponslots[2].Alignment = TextDrawAlignment.Left;
            weaponslots[2].ForeColor = -1;
            weaponslots[2].BackColor = -1094795651;
            weaponslots[2].BoxColor = -16777103;
            weaponslots[2].UseBox = false;
            weaponslots[2].Proportional = true;
            weaponslots[2].Selectable = true;
            weaponslots[2].PreviewModel = 353;
            weaponslots[2].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            weaponslots[2].PreviewZoom = 1;

            /**
weapon4[playerid] = CreatePlayerTextDraw(playerid, 375.000000, 232.000000, "Preview_Model");
PlayerTextDrawFont(playerid, weapon4[playerid], 5);
PlayerTextDrawLetterSize(playerid, weapon4[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, weapon4[playerid], 27.500000, 18.500000);
PlayerTextDrawSetOutline(playerid, weapon4[playerid], 0);
PlayerTextDrawSetShadow(playerid, weapon4[playerid], 0);
PlayerTextDrawAlignment(playerid, weapon4[playerid], 1);
PlayerTextDrawColor(playerid, weapon4[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, weapon4[playerid], -1094795651);
PlayerTextDrawBoxColor(playerid, weapon4[playerid], -16777103);
PlayerTextDrawUseBox(playerid, weapon4[playerid], 0);
PlayerTextDrawSetProportional(playerid, weapon4[playerid], 1);
PlayerTextDrawSetSelectable(playerid, weapon4[playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, weapon4[playerid], 366);
PlayerTextDrawSetPreviewRot(playerid, weapon4[playerid], 3.000000, 0.000000, -131.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, weapon4[playerid], 1, 1);
**/
            weaponslots[3] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(375.0f, 232.0f), "Preview_Model");
            weaponslots[3].Font = TextDrawFont.PreviewModel;
            weaponslots[3].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            weaponslots[3].Width = 27.5f;
            weaponslots[3].Height = 18.5f;
            weaponslots[3].Outline = 0;
            weaponslots[3].Shadow = 0;
            weaponslots[3].Alignment = TextDrawAlignment.Left;
            weaponslots[3].ForeColor = -1;
            weaponslots[3].BackColor = -1094795651;
            weaponslots[3].BoxColor = -16777103;
            weaponslots[3].UseBox = false;
            weaponslots[3].Proportional = true;
            weaponslots[3].Selectable = true;
            weaponslots[3].PreviewModel = 366;
            weaponslots[3].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            weaponslots[3].PreviewZoom = 1;

            /**
weapon5[playerid] = CreatePlayerTextDraw(playerid, 407.000000, 232.000000, "Preview_Model");
PlayerTextDrawFont(playerid, weapon5[playerid], 5);
PlayerTextDrawLetterSize(playerid, weapon5[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, weapon5[playerid], 27.500000, 18.500000);
PlayerTextDrawSetOutline(playerid, weapon5[playerid], 0);
PlayerTextDrawSetShadow(playerid, weapon5[playerid], 0);
PlayerTextDrawAlignment(playerid, weapon5[playerid], 1);
PlayerTextDrawColor(playerid, weapon5[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, weapon5[playerid], -1094795651);
PlayerTextDrawBoxColor(playerid, weapon5[playerid], -16777103);
PlayerTextDrawUseBox(playerid, weapon5[playerid], 0);
PlayerTextDrawSetProportional(playerid, weapon5[playerid], 1);
PlayerTextDrawSetSelectable(playerid, weapon5[playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, weapon5[playerid], 356);
PlayerTextDrawSetPreviewRot(playerid, weapon5[playerid], 3.000000, 0.000000, -131.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, weapon5[playerid], 1, 1);
**/
            weaponslots[4] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(407.0f, 232.0f), "Preview_Model");
            weaponslots[4].Font = TextDrawFont.PreviewModel;
            weaponslots[4].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            weaponslots[4].Width = 27.5f;
            weaponslots[4].Height = 18.5f;
            weaponslots[4].Outline = 0;
            weaponslots[4].Shadow = 0;
            weaponslots[4].Alignment = TextDrawAlignment.Left;
            weaponslots[4].ForeColor = -1;
            weaponslots[4].BackColor = -1094795651;
            weaponslots[4].BoxColor = -16777103;
            weaponslots[4].UseBox = false;
            weaponslots[4].Proportional = true;
            weaponslots[4].Selectable = true;
            weaponslots[4].PreviewModel = 356;
            weaponslots[4].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            weaponslots[4].PreviewZoom = 1;


            /**
weapon6[playerid] = CreatePlayerTextDraw(playerid, 439.000000, 232.000000, "Preview_Model");
PlayerTextDrawFont(playerid, weapon6[playerid], 5);
PlayerTextDrawLetterSize(playerid, weapon6[playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, weapon6[playerid], 27.500000, 18.500000);
PlayerTextDrawSetOutline(playerid, weapon6[playerid], 0);
PlayerTextDrawSetShadow(playerid, weapon6[playerid], 0);
PlayerTextDrawAlignment(playerid, weapon6[playerid], 1);
PlayerTextDrawColor(playerid, weapon6[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, weapon6[playerid], -1094795651);
PlayerTextDrawBoxColor(playerid, weapon6[playerid], -16777103);
PlayerTextDrawUseBox(playerid, weapon6[playerid], 0);
PlayerTextDrawSetProportional(playerid, weapon6[playerid], 1);
PlayerTextDrawSetSelectable(playerid, weapon6[playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, weapon6[playerid], 349);
PlayerTextDrawSetPreviewRot(playerid, weapon6[playerid], 3.000000, 0.000000, -131.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, weapon6[playerid], 1, 1);
**/
            weaponslots[5] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(439.0f, 232.0f), "Preview_Model");
            weaponslots[5].Font = TextDrawFont.PreviewModel;
            weaponslots[5].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            weaponslots[5].Width = 27.5f;
            weaponslots[5].Height = 18.5f;
            weaponslots[5].Outline = 0;
            weaponslots[5].Shadow = 0;
            weaponslots[5].Alignment = TextDrawAlignment.Left;
            weaponslots[5].ForeColor = -1;
            weaponslots[5].BackColor = -1094795651;
            weaponslots[5].BoxColor = -16777103;
            weaponslots[5].UseBox = false;
            weaponslots[5].Proportional = true;
            weaponslots[5].Selectable = true;
            weaponslots[5].PreviewModel = 349;
            weaponslots[5].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            weaponslots[5].PreviewZoom = 1;

            /**
use = TextDrawCreate(505.000000, 233.500000, "Use");
TextDrawFont(use, 2);
TextDrawLetterSize(use, 0.358332, 1.700000);
TextDrawTextSize(use, 11.500000, 38.000000);
TextDrawSetOutline(use, 1);
TextDrawSetShadow(use, 0);
TextDrawAlignment(use, 2);
TextDrawColor(use, -1);
TextDrawBackgroundColor(use, 255);
TextDrawBoxColor(use, -764862776);
TextDrawUseBox(use, 1);
TextDrawSetProportional(use, 1);
TextDrawSetSelectable(use, 1);
**/
            useButton = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(505.0f, 233.5f), "Use");
            useButton.Font = TextDrawFont.Slim;
            useButton.LetterSize = new SampSharp.GameMode.Vector2(0.358332f, 1.7f);
            useButton.Width = 11.5f;
            useButton.Height = 38.0f;
            useButton.Outline = 1;
            useButton.Shadow = 0;
            useButton.Alignment = TextDrawAlignment.Center;
            useButton.ForeColor = -1;
            useButton.BackColor = 255;
            useButton.BoxColor = -764862776;
            useButton.UseBox = true;
            useButton.Proportional = true;
            useButton.Selectable = true;

            dropButton = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(552.0f, 233.5f), "drop");
            dropButton.Font = TextDrawFont.Slim;
            dropButton.LetterSize = new SampSharp.GameMode.Vector2(0.358332f, 1.7f);
            dropButton.Width = 11.5f;
            dropButton.Height = 38.0f;
            dropButton.Outline = 1;
            dropButton.Shadow = 0;
            dropButton.Alignment = TextDrawAlignment.Center;
            dropButton.ForeColor = -1;
            dropButton.BackColor = 255;
            dropButton.BoxColor = -764862776;
            dropButton.UseBox = true;
            dropButton.Proportional = true;
            dropButton.Selectable = true;

            sellButton = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(552.0f, 209.5f), "give");
            sellButton.Font = TextDrawFont.Slim;
            sellButton.LetterSize = new SampSharp.GameMode.Vector2(0.358332f, 1.7f);
            sellButton.Width = 11.5f;
            sellButton.Height = 38.0f;
            sellButton.Outline = 1;
            sellButton.Shadow = 0;
            sellButton.Alignment = TextDrawAlignment.Center;
            sellButton.ForeColor = -1;
            sellButton.BackColor = 255;
            sellButton.BoxColor = -764862776;
            sellButton.UseBox = true;
            sellButton.Proportional = true;
            sellButton.Selectable = true;

            infoButton = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(505.0f, 209.5f), "info");
            infoButton.Font = TextDrawFont.Slim;
            infoButton.LetterSize = new SampSharp.GameMode.Vector2(0.358332f, 1.7f);
            infoButton.Width = 11.5f;
            infoButton.Height = 38.0f;
            infoButton.Outline = 1;
            infoButton.Shadow = 0;
            infoButton.Alignment = TextDrawAlignment.Center;
            infoButton.ForeColor = -1;
            infoButton.BackColor = 255;
            infoButton.BoxColor = -764862776;
            infoButton.UseBox = true;
            infoButton.Proportional = true;
            infoButton.Selectable = true;
            /**
        exitbutton[playerid] = CreatePlayerTextDraw(playerid, 568.000000, 110.000000, "ld_beat:cross");
        PlayerTextDrawFont(playerid, exitbutton[playerid], 4);
        PlayerTextDrawLetterSize(playerid, exitbutton[playerid], 0.600000, 2.000000);
        PlayerTextDrawTextSize(playerid, exitbutton[playerid], 12.500000, 12.500000);
        PlayerTextDrawSetOutline(playerid, exitbutton[playerid], 1);
        PlayerTextDrawSetShadow(playerid, exitbutton[playerid], 0);
        PlayerTextDrawAlignment(playerid, exitbutton[playerid], 1);
        PlayerTextDrawColor(playerid, exitbutton[playerid], -1);
        PlayerTextDrawBackgroundColor(playerid, exitbutton[playerid], 255);
        PlayerTextDrawBoxColor(playerid, exitbutton[playerid], 50);
        PlayerTextDrawUseBox(playerid, exitbutton[playerid], 1);
        PlayerTextDrawSetProportional(playerid, exitbutton[playerid], 1);
        PlayerTextDrawSetSelectable(playerid, exitbutton[playerid], 1);
        **/
            closeinv = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(568.0f, 110.0f), "ld_beat:cross");
            closeinv.Font = TextDrawFont.DrawSprite;
            closeinv.LetterSize = new SampSharp.GameMode.Vector2(0.6, 2.0);
            closeinv.Width = 12.5f;
            closeinv.Height = 12.5f;
            closeinv.Outline = 1;
            closeinv.Shadow = 0;
            closeinv.Alignment = TextDrawAlignment.Left;
            closeinv.ForeColor = -1;
            closeinv.BackColor = 255;
            closeinv.BoxColor = 50;
            closeinv.UseBox = true;
            closeinv.Proportional = true;
            closeinv.Selectable = true;

            /**
slots[1][playerid] = CreatePlayerTextDraw(playerid, 375.000000, 257.000000, "Preview_Model");
PlayerTextDrawFont(playerid, slots[1][playerid], 5);
PlayerTextDrawLetterSize(playerid, slots[1][playerid], 0.600000, 2.000000);
PlayerTextDrawTextSize(playerid, slots[1][playerid], 24.000000, 26.500000);
PlayerTextDrawSetOutline(playerid, slots[0][playerid], 0);
PlayerTextDrawSetShadow(playerid, slots[0][playerid], 0);
PlayerTextDrawAlignment(playerid, slots[0][playerid], 1);
PlayerTextDrawColor(playerid, slots[0][playerid], -1);
PlayerTextDrawBackgroundColor(playerid, slots[0][playerid], -1094795651);
PlayerTextDrawBoxColor(playerid, slots[0][playerid], -16777103);
PlayerTextDrawUseBox(playerid, slots[0][playerid], 0);
PlayerTextDrawSetProportional(playerid, slots[0][playerid], 1);
PlayerTextDrawSetSelectable(playerid, slots[0][playerid], 1);
PlayerTextDrawSetPreviewModel(playerid, slots[0][playerid], 11718);
PlayerTextDrawSetPreviewRot(playerid, slots[0][playerid], 3.000000, 0.000000, -131.000000, 1.000000);
PlayerTextDrawSetPreviewVehCol(playerid, slots[0][playerid], 1, 1);
**/
            slots[0] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(375.0f, 257.0f), "Preview_Model");
            slots[0].Font = TextDrawFont.PreviewModel;
            slots[0].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[0].Width = 24.0f;
            slots[0].Height = 26.5f;
            slots[0].Outline = 0;
            slots[0].Shadow = 0;
            slots[0].Alignment = TextDrawAlignment.Left;
            slots[0].ForeColor = -1;
            slots[0].BackColor = -1094795651;
            slots[0].BoxColor = -16777103;
            slots[0].UseBox = false;
            slots[0].Proportional = true;
            slots[0].Selectable = true;
            slots[0].PreviewModel = 11718;
            slots[0].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[0].PreviewZoom = 1;

            slots[1] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(404.0f, 257.0f), "Preview_Model");
            slots[1].Font = TextDrawFont.PreviewModel;
            slots[1].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[1].Width = 24.0f;
            slots[1].Height = 26.5f;
            slots[1].Outline = 0;
            slots[1].Shadow = 0;
            slots[1].Alignment = TextDrawAlignment.Left;
            slots[1].ForeColor = -1;
            slots[1].BackColor = -1094795651;
            slots[1].BoxColor = -16777103;
            slots[1].UseBox = false;
            slots[1].Proportional = true;
            slots[1].Selectable = true;
            slots[1].PreviewModel = 11718;
            slots[1].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[1].PreviewZoom = 1;

            slots[2] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(433.0f, 257.0f), "Preview_Model");
            slots[2].Font = TextDrawFont.PreviewModel;
            slots[2].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[2].Width = 24.0f;
            slots[2].Height = 26.5f;
            slots[2].Outline = 0;
            slots[2].Shadow = 0;
            slots[2].Alignment = TextDrawAlignment.Left;
            slots[2].ForeColor = -1;
            slots[2].BackColor = -1094795651;
            slots[2].BoxColor = -16777103;
            slots[2].UseBox = false;
            slots[2].Proportional = true;
            slots[2].Selectable = true;
            slots[2].PreviewModel = 11718;
            slots[2].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[2].PreviewZoom = 1;

            slots[3] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(462.0f, 257.0f), "Preview_Model");
            slots[3].Font = TextDrawFont.PreviewModel;
            slots[3].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[3].Width = 24.0f;
            slots[3].Height = 26.5f;
            slots[3].Outline = 0;
            slots[3].Shadow = 0;
            slots[3].Alignment = TextDrawAlignment.Left;
            slots[3].ForeColor = -1;
            slots[3].BackColor = -1094795651;
            slots[3].BoxColor = -16777103;
            slots[3].UseBox = false;
            slots[3].Proportional = true;
            slots[3].Selectable = true;
            slots[3].PreviewModel = 11718;
            slots[3].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[3].PreviewZoom = 1;

            slots[4] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(491.0f, 257.0f), "Preview_Model");
            slots[4].Font = TextDrawFont.PreviewModel;
            slots[4].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[4].Width = 24.0f;
            slots[4].Height = 26.5f;
            slots[4].Outline = 0;
            slots[4].Shadow = 0;
            slots[4].Alignment = TextDrawAlignment.Left;
            slots[4].ForeColor = -1;
            slots[4].BackColor = -1094795651;
            slots[4].BoxColor = -16777103;
            slots[4].UseBox = false;
            slots[4].Proportional = true;
            slots[4].Selectable = true;
            slots[4].PreviewModel = 11718;
            slots[4].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[4].PreviewZoom = 1;

            slots[5] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(520.0f, 257.0f), "Preview_Model");
            slots[5].Font = TextDrawFont.PreviewModel;
            slots[5].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[5].Width = 24.0f;
            slots[5].Height = 26.5f;
            slots[5].Outline = 0;
            slots[5].Shadow = 0;
            slots[5].Alignment = TextDrawAlignment.Left;
            slots[5].ForeColor = -1;
            slots[5].BackColor = -1094795651;
            slots[5].BoxColor = -16777103;
            slots[5].UseBox = false;
            slots[5].Proportional = true;
            slots[5].Selectable = true;
            slots[5].PreviewModel = 11718;
            slots[5].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[5].PreviewZoom = 1;

            slots[6] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(548.0f, 257.0f), "Preview_Model");
            slots[6].Font = TextDrawFont.PreviewModel;
            slots[6].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[6].Width = 24.0f;
            slots[6].Height = 26.5f;
            slots[6].Outline = 0;
            slots[6].Shadow = 0;
            slots[6].Alignment = TextDrawAlignment.Left;
            slots[6].ForeColor = -1;
            slots[6].BackColor = -1094795651;
            slots[6].BoxColor = -16777103;
            slots[6].UseBox = false;
            slots[6].Proportional = true;
            slots[6].Selectable = true;
            slots[6].PreviewModel = 11718;
            slots[6].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[6].PreviewZoom = 1;

            slots[7] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(375.0f, 287.0f), "Preview_Model");
            slots[7].Font = TextDrawFont.PreviewModel;
            slots[7].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[7].Width = 24.0f;
            slots[7].Height = 26.5f;
            slots[7].Outline = 0;
            slots[7].Shadow = 0;
            slots[7].Alignment = TextDrawAlignment.Left;
            slots[7].ForeColor = -1;
            slots[7].BackColor = -1094795651;
            slots[7].BoxColor = -16777103;
            slots[7].UseBox = false;
            slots[7].Proportional = true;
            slots[7].Selectable = true;
            slots[7].PreviewModel = 11718;
            slots[7].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[7].PreviewZoom = 1;

            slots[8] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(404.0f, 287.0f), "Preview_Model");
            slots[8].Font = TextDrawFont.PreviewModel;
            slots[8].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[8].Width = 24.0f;
            slots[8].Height = 26.5f;
            slots[8].Outline = 0;
            slots[8].Shadow = 0;
            slots[8].Alignment = TextDrawAlignment.Left;
            slots[8].ForeColor = -1;
            slots[8].BackColor = -1094795651;
            slots[8].BoxColor = -16777103;
            slots[8].UseBox = false;
            slots[8].Proportional = true;
            slots[8].Selectable = true;
            slots[8].PreviewModel = 11718;
            slots[8].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[8].PreviewZoom = 1;

            slots[9] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(433.0f, 287.0f), "Preview_Model");
            slots[9].Font = TextDrawFont.PreviewModel;
            slots[9].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[9].Width = 24.0f;
            slots[9].Height = 26.5f;
            slots[9].Outline = 0;
            slots[9].Shadow = 0;
            slots[9].Alignment = TextDrawAlignment.Left;
            slots[9].ForeColor = -1;
            slots[9].BackColor = -1094795651;
            slots[9].BoxColor = -16777103;
            slots[9].UseBox = false;
            slots[9].Proportional = true;
            slots[9].Selectable = true;
            slots[9].PreviewModel = 11718;
            slots[9].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[9].PreviewZoom = 1;

            slots[10] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(462.0f, 287.0f), "Preview_Model");
            slots[10].Font = TextDrawFont.PreviewModel;
            slots[10].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[10].Width = 24.0f;
            slots[10].Height = 26.5f;
            slots[10].Outline = 0;
            slots[10].Shadow = 0;
            slots[10].Alignment = TextDrawAlignment.Left;
            slots[10].ForeColor = -1;
            slots[10].BackColor = -1094795651;
            slots[10].BoxColor = -16777103;
            slots[10].UseBox = false;
            slots[10].Proportional = true;
            slots[10].Selectable = true;
            slots[10].PreviewModel = 11718;
            slots[10].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[10].PreviewZoom = 1;

            slots[11] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(491.0f, 287.0f), "Preview_Model");
            slots[11].Font = TextDrawFont.PreviewModel;
            slots[11].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[11].Width = 24.0f;
            slots[11].Height = 26.5f;
            slots[11].Outline = 0;
            slots[11].Shadow = 0;
            slots[11].Alignment = TextDrawAlignment.Left;
            slots[11].ForeColor = -1;
            slots[11].BackColor = -1094795651;
            slots[11].BoxColor = -16777103;
            slots[11].UseBox = false;
            slots[11].Proportional = true;
            slots[11].Selectable = true;
            slots[11].PreviewModel = 11718;
            slots[11].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[11].PreviewZoom = 1;

            slots[12] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(520.0f, 287.0f), "Preview_Model");
            slots[12].Font = TextDrawFont.PreviewModel;
            slots[12].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[12].Width = 24.0f;
            slots[12].Height = 26.5f;
            slots[12].Outline = 0;
            slots[12].Shadow = 0;
            slots[12].Alignment = TextDrawAlignment.Left;
            slots[12].ForeColor = -1;
            slots[12].BackColor = -1094795651;
            slots[12].BoxColor = -16777103;
            slots[12].UseBox = false;
            slots[12].Proportional = true;
            slots[12].Selectable = true;
            slots[12].PreviewModel = 11718;
            slots[12].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[12].PreviewZoom = 1;

            slots[13] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(548.0f, 287.0f), "Preview_Model");
            slots[13].Font = TextDrawFont.PreviewModel;
            slots[13].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[13].Width = 24.0f;
            slots[13].Height = 26.5f;
            slots[13].Outline = 0;
            slots[13].Shadow = 0;
            slots[13].Alignment = TextDrawAlignment.Left;
            slots[13].ForeColor = -1;
            slots[13].BackColor = -1094795651;
            slots[13].BoxColor = -16777103;
            slots[13].UseBox = false;
            slots[13].Proportional = true;
            slots[13].Selectable = true;
            slots[13].PreviewModel = 11718;
            slots[13].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[13].PreviewZoom = 1;

            slots[14] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(375.0f, 317.0f), "Preview_Model");
            slots[14].Font = TextDrawFont.PreviewModel;
            slots[14].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[14].Width = 24.0f;
            slots[14].Height = 26.5f;
            slots[14].Outline = 0;
            slots[14].Shadow = 0;
            slots[14].Alignment = TextDrawAlignment.Left;
            slots[14].ForeColor = -1;
            slots[14].BackColor = -1094795651;
            slots[14].BoxColor = -16777103;
            slots[14].UseBox = false;
            slots[14].Proportional = true;
            slots[14].Selectable = true;
            slots[14].PreviewModel = 11718;
            slots[14].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[14].PreviewZoom = 1;

            slots[15] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(404.0f, 317.0f), "Preview_Model");
            slots[15].Font = TextDrawFont.PreviewModel;
            slots[15].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[15].Width = 24.0f;
            slots[15].Height = 26.5f;
            slots[15].Outline = 0;
            slots[15].Shadow = 0;
            slots[15].Alignment = TextDrawAlignment.Left;
            slots[15].ForeColor = -1;
            slots[15].BackColor = -1094795651;
            slots[15].BoxColor = -16777103;
            slots[15].UseBox = false;
            slots[15].Proportional = true;
            slots[15].Selectable = true;
            slots[15].PreviewModel = 11718;
            slots[15].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[15].PreviewZoom = 1.0f;

            slots[16] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(433.0f, 317.0f), "Preview_Model");
            slots[16].Font = TextDrawFont.PreviewModel;
            slots[16].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[16].Width = 24.0f;
            slots[16].Height = 26.5f;
            slots[16].Outline = 0;
            slots[16].Shadow = 0;
            slots[16].Alignment = TextDrawAlignment.Left;
            slots[16].ForeColor = -1;
            slots[16].BackColor = -1094795651;
            slots[16].BoxColor = -16777103;
            slots[16].UseBox = false;
            slots[16].Proportional = true;
            slots[16].Selectable = true;
            slots[16].PreviewModel = 11718;
            slots[16].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[16].PreviewZoom = 1;

            slots[17] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(462.0f, 317.0f), "Preview_Model");
            slots[17].Font = TextDrawFont.PreviewModel;
            slots[17].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[17].Width = 24.0f;
            slots[17].Height = 26.5f;
            slots[17].Outline = 0;
            slots[17].Shadow = 0;
            slots[17].Alignment = TextDrawAlignment.Left;
            slots[17].ForeColor = -1;
            slots[17].BackColor = -1094795651;
            slots[17].BoxColor = -16777103;
            slots[17].UseBox = false;
            slots[17].Proportional = true;
            slots[17].Selectable = true;
            slots[17].PreviewModel = 11718;
            slots[17].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[17].PreviewZoom = 1;

            slots[18] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(491.0f, 317.0f), "Preview_Model");
            slots[18].Font = TextDrawFont.PreviewModel;
            slots[18].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[18].Width = 24.0f;
            slots[18].Height = 26.5f;
            slots[18].Outline = 0;
            slots[18].Shadow = 0;
            slots[18].Alignment = TextDrawAlignment.Left;
            slots[18].ForeColor = -1;
            slots[18].BackColor = -1094795651;
            slots[18].BoxColor = -16777103;
            slots[18].UseBox = false;
            slots[18].Proportional = true;
            slots[18].Selectable = true;
            slots[18].PreviewModel = 11718;
            slots[18].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[18].PreviewZoom = 1;

            slots[19] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(520.0f, 317.0f), "Preview_Model");
            slots[19].Font = TextDrawFont.PreviewModel;
            slots[19].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[19].Width = 24.0f;
            slots[19].Height = 26.5f;
            slots[19].Outline = 0;
            slots[19].Shadow = 0;
            slots[19].Alignment = TextDrawAlignment.Left;
            slots[19].ForeColor = -1;
            slots[19].BackColor = -1094795651;
            slots[19].BoxColor = -16777103;
            slots[19].UseBox = false;
            slots[19].Proportional = true;
            slots[19].Selectable = true;
            slots[19].PreviewModel = 11718;
            slots[19].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[19].PreviewZoom = 1;

            slots[20] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(548.0f, 317.0f), "Preview_Model");
            slots[20].Font = TextDrawFont.PreviewModel;
            slots[20].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[20].Width = 24.0f;
            slots[20].Height = 26.5f;
            slots[20].Outline = 0;
            slots[20].Shadow = 0;
            slots[20].Alignment = TextDrawAlignment.Left;
            slots[20].ForeColor = -1;
            slots[20].BackColor = -1094795651;
            slots[20].BoxColor = -16777103;
            slots[20].UseBox = false;
            slots[20].Proportional = true;
            slots[20].Selectable = true;
            slots[20].PreviewModel = 11718;
            slots[20].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[20].PreviewZoom = 1;

            slots[21] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(375.0f, 348.0f), "Preview_Model");
            slots[21].Font = TextDrawFont.PreviewModel;
            slots[21].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[21].Width = 24.0f;
            slots[21].Height = 26.5f;
            slots[21].Outline = 0;
            slots[21].Shadow = 0;
            slots[21].Alignment = TextDrawAlignment.Left;
            slots[21].ForeColor = -1;
            slots[21].BackColor = -1094795651;
            slots[21].BoxColor = -16777103;
            slots[21].UseBox = false;
            slots[21].Proportional = true;
            slots[21].Selectable = true;
            slots[21].PreviewModel = 11718;
            slots[21].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[21].PreviewZoom = 1;

            slots[22] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(404.0f, 348.0f), "Preview_Model");
            slots[22].Font = TextDrawFont.PreviewModel;
            slots[22].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[22].Width = 24.0f;
            slots[22].Height = 26.5f;
            slots[22].Outline = 0;
            slots[22].Shadow = 0;
            slots[22].Alignment = TextDrawAlignment.Left;
            slots[22].ForeColor = -1;
            slots[22].BackColor = -1094795651;
            slots[22].BoxColor = -16777103;
            slots[22].UseBox = false;
            slots[22].Proportional = true;
            slots[22].Selectable = true;
            slots[22].PreviewModel = 11718;
            slots[22].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[22].PreviewZoom = 1;

            slots[23] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(433.0f, 348.0f), "Preview_Model");
            slots[23].Font = TextDrawFont.PreviewModel;
            slots[23].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[23].Width = 24.0f;
            slots[23].Height = 26.5f;
            slots[23].Outline = 0;
            slots[23].Shadow = 0;
            slots[23].Alignment = TextDrawAlignment.Left;
            slots[23].ForeColor = -1;
            slots[23].BackColor = -1094795651;
            slots[23].BoxColor = -16777103;
            slots[23].UseBox = false;
            slots[23].Proportional = true;
            slots[23].Selectable = true;
            slots[23].PreviewModel = 11718;
            slots[23].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[23].PreviewZoom = 1;

            slots[24] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(462.0f, 348.0f), "Preview_Model");
            slots[24].Font = TextDrawFont.PreviewModel;
            slots[24].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[24].Width = 24.0f;
            slots[24].Height = 26.5f;
            slots[24].Outline = 0;
            slots[24].Shadow = 0;
            slots[24].Alignment = TextDrawAlignment.Left;
            slots[24].ForeColor = -1;
            slots[24].BackColor = -1094795651;
            slots[24].BoxColor = -16777103;
            slots[24].UseBox = false;
            slots[24].Proportional = true;
            slots[24].Selectable = true;
            slots[24].PreviewModel = 11718;
            slots[24].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[24].PreviewZoom = 1;

            slots[25] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(491.0f, 348.0f), "Preview_Model");
            slots[25].Font = TextDrawFont.PreviewModel;
            slots[25].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[25].Width = 24.0f;
            slots[25].Height = 26.5f;
            slots[25].Outline = 0;
            slots[25].Shadow = 0;
            slots[25].Alignment = TextDrawAlignment.Left;
            slots[25].ForeColor = -1;
            slots[25].BackColor = -1094795651;
            slots[25].BoxColor = -16777103;
            slots[25].UseBox = false;
            slots[25].Proportional = true;
            slots[25].Selectable = true;
            slots[25].PreviewModel = 11718;
            slots[25].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[25].PreviewZoom = 1;

            slots[26] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(520.0f, 348.0f), "Preview_Model");
            slots[26].Font = TextDrawFont.PreviewModel;
            slots[26].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[26].Width = 24.0f;
            slots[26].Height = 26.5f;
            slots[26].Outline = 0;
            slots[26].Shadow = 0;
            slots[26].Alignment = TextDrawAlignment.Left;
            slots[26].ForeColor = -1;
            slots[26].BackColor = -1094795651;
            slots[26].BoxColor = -16777103;
            slots[26].UseBox = false;
            slots[26].Proportional = true;
            slots[26].Selectable = true;
            slots[26].PreviewModel = 11718;
            slots[26].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[26].PreviewZoom = 1;

            slots[27] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(548.0f, 348.0f), "Preview_Model");
            slots[27].Font = TextDrawFont.PreviewModel;
            slots[27].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[27].Width = 24.0f;
            slots[27].Height = 26.5f;
            slots[27].Outline = 0;
            slots[27].Shadow = 0;
            slots[27].Alignment = TextDrawAlignment.Left;
            slots[27].ForeColor = -1;
            slots[27].BackColor = -1094795651;
            slots[27].BoxColor = -16777103;
            slots[27].UseBox = false;
            slots[27].Proportional = true;
            slots[27].Selectable = true;
            slots[27].PreviewModel = 11718;
            slots[27].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[27].PreviewZoom = 1;

            slots[28] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(375.0f, 379.0f), "Preview_Model");
            slots[28].Font = TextDrawFont.PreviewModel;
            slots[28].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[28].Width = 24.0f;
            slots[28].Height = 26.5f;
            slots[28].Outline = 0;
            slots[28].Shadow = 0;
            slots[28].Alignment = TextDrawAlignment.Left;
            slots[28].ForeColor = -1;
            slots[28].BackColor = -1094795651;
            slots[28].BoxColor = -16777103;
            slots[28].UseBox = false;
            slots[28].Proportional = true;
            slots[28].Selectable = true;
            slots[28].PreviewModel = 11718;
            slots[28].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[28].PreviewZoom = 1;

            slots[29] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(404.0f, 379.0f), "Preview_Model");
            slots[29].Font = TextDrawFont.PreviewModel;
            slots[29].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[29].Width = 24.0f;
            slots[29].Height = 26.5f;
            slots[29].Outline = 0;
            slots[29].Shadow = 0;
            slots[29].Alignment = TextDrawAlignment.Left;
            slots[29].ForeColor = -1;
            slots[29].BackColor = -1094795651;
            slots[29].BoxColor = -16777103;
            slots[29].UseBox = false;
            slots[29].Proportional = true;
            slots[29].Selectable = true;
            slots[29].PreviewModel = 11718;
            slots[29].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[29].PreviewZoom = 1;

            slots[30] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(433.0f, 379.0f), "Preview_Model");
            slots[30].Font = TextDrawFont.PreviewModel;
            slots[30].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[30].Width = 24.0f;
            slots[30].Height = 26.5f;
            slots[30].Outline = 0;
            slots[30].Shadow = 0;
            slots[30].Alignment = TextDrawAlignment.Left;
            slots[30].ForeColor = -1;
            slots[30].BackColor = -1094795651;
            slots[30].BoxColor = -16777103;
            slots[30].UseBox = false;
            slots[30].Proportional = true;
            slots[30].Selectable = true;
            slots[30].PreviewModel = 11718;
            slots[30].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[30].PreviewZoom = 1;

            slots[31] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(462.0f, 379.0f), "Preview_Model");
            slots[31].Font = TextDrawFont.PreviewModel;
            slots[31].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[31].Width = 24.0f;
            slots[31].Height = 26.5f;
            slots[31].Outline = 0;
            slots[31].Shadow = 0;
            slots[31].Alignment = TextDrawAlignment.Left;
            slots[31].ForeColor = -1;
            slots[31].BackColor = -1094795651;
            slots[31].BoxColor = -16777103;
            slots[31].UseBox = false;
            slots[31].Proportional = true;
            slots[31].Selectable = true;
            slots[31].PreviewModel = 11718;
            slots[31].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[31].PreviewZoom = 1;

            slots[32] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(491.0f, 379.0f), "Preview_Model");
            slots[32].Font = TextDrawFont.PreviewModel;
            slots[32].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[32].Width = 24.0f;
            slots[32].Height = 26.5f;
            slots[32].Outline = 0;
            slots[32].Shadow = 0;
            slots[32].Alignment = TextDrawAlignment.Left;
            slots[32].ForeColor = -1;
            slots[32].BackColor = -1094795651;
            slots[32].BoxColor = -16777103;
            slots[32].UseBox = false;
            slots[32].Proportional = true;
            slots[32].Selectable = true;
            slots[32].PreviewModel = 11718;
            slots[32].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[32].PreviewZoom = 1;

            slots[33] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(520.0f, 379.0f), "Preview_Model");
            slots[33].Font = TextDrawFont.PreviewModel;
            slots[33].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[33].Width = 24.0f;
            slots[33].Height = 26.5f;
            slots[33].Outline = 0;
            slots[33].Shadow = 0;
            slots[33].Alignment = TextDrawAlignment.Left;
            slots[33].ForeColor = -1;
            slots[33].BackColor = -1094795651;
            slots[33].BoxColor = -16777103;
            slots[33].UseBox = false;
            slots[33].Proportional = true;
            slots[33].Selectable = true;
            slots[33].PreviewModel = 11718;
            slots[33].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[33].PreviewZoom = 1;

            slots[34] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(548.0f, 379.0f), "Preview_Model");
            slots[34].Font = TextDrawFont.PreviewModel;
            slots[34].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[34].Width = 24.0f;
            slots[34].Height = 26.5f;
            slots[34].Outline = 0;
            slots[34].Shadow = 0;
            slots[34].Alignment = TextDrawAlignment.Left;
            slots[34].ForeColor = -1;
            slots[34].BackColor = -1094795651;
            slots[34].BoxColor = -16777103;
            slots[34].UseBox = false;
            slots[34].Proportional = true;
            slots[34].Selectable = true;
            slots[34].PreviewModel = 11718;
            slots[34].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[34].PreviewZoom = 1;

            slots[35] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(375.0f, 410.0f), "Preview_Model");
            slots[35].Font = TextDrawFont.PreviewModel;
            slots[35].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[35].Width = 24.0f;
            slots[35].Height = 26.5f;
            slots[35].Outline = 0;
            slots[35].Shadow = 0;
            slots[35].Alignment = TextDrawAlignment.Left;
            slots[35].ForeColor = -1;
            slots[35].BackColor = -1094795651;
            slots[35].BoxColor = -16777103;
            slots[35].UseBox = false;
            slots[35].Proportional = true;
            slots[35].Selectable = true;
            slots[35].PreviewModel = 11718;
            slots[35].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[35].PreviewZoom = 1;

            slots[36] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(404.0f, 410.0f), "Preview_Model");
            slots[36].Font = TextDrawFont.PreviewModel;
            slots[36].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[36].Width = 24.0f;
            slots[36].Height = 26.5f;
            slots[36].Outline = 0;
            slots[36].Shadow = 0;
            slots[36].Alignment = TextDrawAlignment.Left;
            slots[36].ForeColor = -1;
            slots[36].BackColor = -1094795651;
            slots[36].BoxColor = -16777103;
            slots[36].UseBox = false;
            slots[36].Proportional = true;
            slots[36].Selectable = true;
            slots[36].PreviewModel = 11718;
            slots[36].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[36].PreviewZoom = 1;

            slots[37] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(433.0f, 410.0f), "Preview_Model");
            slots[37].Font = TextDrawFont.PreviewModel;
            slots[37].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[37].Width = 24.0f;
            slots[37].Height = 26.5f;
            slots[37].Outline = 0;
            slots[37].Shadow = 0;
            slots[37].Alignment = TextDrawAlignment.Left;
            slots[37].ForeColor = -1;
            slots[37].BackColor = -1094795651;
            slots[37].BoxColor = -16777103;
            slots[37].UseBox = false;
            slots[37].Proportional = true;
            slots[37].Selectable = true;
            slots[37].PreviewModel = 11718;
            slots[37].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[37].PreviewZoom = 1;

            slots[38] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(462.0f, 410.0f), "Preview_Model");
            slots[38].Font = TextDrawFont.PreviewModel;
            slots[38].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[38].Width = 24.0f;
            slots[38].Height = 26.5f;
            slots[38].Outline = 0;
            slots[38].Shadow = 0;
            slots[38].Alignment = TextDrawAlignment.Left;
            slots[38].ForeColor = -1;
            slots[38].BackColor = -1094795651;
            slots[38].BoxColor = -16777103;
            slots[38].UseBox = false;
            slots[38].Proportional = true;
            slots[38].Selectable = true;
            slots[38].PreviewModel = 11718;
            slots[38].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[38].PreviewZoom = 1;

            slots[39] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(491.0f, 410.0f), "Preview_Model");
            slots[39].Font = TextDrawFont.PreviewModel;
            slots[39].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[39].Width = 24.0f;
            slots[39].Height = 26.5f;
            slots[39].Outline = 0;
            slots[39].Shadow = 0;
            slots[39].Alignment = TextDrawAlignment.Left;
            slots[39].ForeColor = -1;
            slots[39].BackColor = -1094795651;
            slots[39].BoxColor = -16777103;
            slots[39].UseBox = false;
            slots[39].Proportional = true;
            slots[39].Selectable = true;
            slots[39].PreviewModel = 11718;
            slots[39].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[39].PreviewZoom = 1;

            slots[40] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(520.0f, 410.0f), "Preview_Model");
            slots[40].Font = TextDrawFont.PreviewModel;
            slots[40].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[40].Width = 24.0f;
            slots[40].Height = 26.5f;
            slots[40].Outline = 0;
            slots[40].Shadow = 0;
            slots[40].Alignment = TextDrawAlignment.Left;
            slots[40].ForeColor = -1;
            slots[40].BackColor = -1094795651;
            slots[40].BoxColor = -16777103;
            slots[40].UseBox = false;
            slots[40].Proportional = true;
            slots[40].Selectable = true;
            slots[40].PreviewModel = 11718;
            slots[40].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[40].PreviewZoom = 1;

            slots[41] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(548.0f, 410.0f), "Preview_Model");
            slots[41].Font = TextDrawFont.PreviewModel;
            slots[41].LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            slots[41].Width = 24.0f;
            slots[41].Height = 26.5f;
            slots[41].Outline = 0;
            slots[41].Shadow = 0;
            slots[41].Alignment = TextDrawAlignment.Left;
            slots[41].ForeColor = -1;
            slots[41].BackColor = -1094795651;
            slots[41].BoxColor = -16777103;
            slots[41].UseBox = false;
            slots[41].Proportional = true;
            slots[41].Selectable = true;
            slots[41].PreviewModel = 11718;
            slots[41].PreviewRotation = new SampSharp.GameMode.Vector3(3.0, 0.0, -131.0);
            slots[41].PreviewZoom = 1;
            /**
             * ironnum[playerid] = CreatePlayerTextDraw(playerid, 530.000000, 164.000000, "0");
PlayerTextDrawFont(playerid, ironnum[playerid], 2);
PlayerTextDrawLetterSize(playerid, ironnum[playerid], 0.287499, 1.149999);
PlayerTextDrawTextSize(playerid, ironnum[playerid], 400.000000, 17.000000);
PlayerTextDrawSetOutline(playerid, ironnum[playerid], 1);
PlayerTextDrawSetShadow(playerid, ironnum[playerid], 0);
PlayerTextDrawAlignment(playerid, ironnum[playerid], 1);
PlayerTextDrawColor(playerid, ironnum[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, ironnum[playerid], 255);
PlayerTextDrawBoxColor(playerid, ironnum[playerid], 50);
PlayerTextDrawUseBox(playerid, ironnum[playerid], 0);
PlayerTextDrawSetProportional(playerid, ironnum[playerid], 1);
PlayerTextDrawSetSelectable(playerid, ironnum[playerid], 1);
             **/
            ironnum = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(530.0f, 164.0f), "3");
            ironnum.Font = TextDrawFont.Slim;
            ironnum.LetterSize = new SampSharp.GameMode.Vector2(0.287499, 1.149999);
            ironnum.Width = 400.0f;
            ironnum.Height = 17.0f;
            ironnum.Outline = 1;
            ironnum.Shadow = 0;
            ironnum.Alignment = TextDrawAlignment.Left;
            ironnum.ForeColor = -1;
            ironnum.BackColor = 255;
            ironnum.BoxColor = 50;
            ironnum.UseBox = false;
            ironnum.Proportional = true;
            ironnum.Selectable = false;

            /**
             * woodnum[playerid] = CreatePlayerTextDraw(playerid, 530.000000, 123.000000, "35000");
PlayerTextDrawFont(playerid, woodnum[playerid], 2);
PlayerTextDrawLetterSize(playerid, woodnum[playerid], 0.287499, 1.149999);
PlayerTextDrawTextSize(playerid, woodnum[playerid], 400.000000, 17.000000);
PlayerTextDrawSetOutline(playerid, woodnum[playerid], 1);
PlayerTextDrawSetShadow(playerid, woodnum[playerid], 0);
PlayerTextDrawAlignment(playerid, woodnum[playerid], 1);
PlayerTextDrawColor(playerid, woodnum[playerid], -1);
PlayerTextDrawBackgroundColor(playerid, woodnum[playerid], 255);
PlayerTextDrawBoxColor(playerid, woodnum[playerid], 50);
PlayerTextDrawUseBox(playerid, woodnum[playerid], 0);
PlayerTextDrawSetProportional(playerid, woodnum[playerid], 1);
PlayerTextDrawSetSelectable(playerid, woodnum[playerid], 1);
             **/

            woodnum = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(530.0f, 123.0f), "1");
            woodnum.Font = TextDrawFont.Slim;
            woodnum.LetterSize = new SampSharp.GameMode.Vector2(0.287499, 1.149999);
            woodnum.Width = 400.0f;
            woodnum.Height = 17.0f;
            woodnum.Outline = 1;
            woodnum.Shadow = 0;
            woodnum.Alignment = TextDrawAlignment.Left;
            woodnum.ForeColor = -1;
            woodnum.BackColor = 255;
            woodnum.BoxColor = 50;
            woodnum.UseBox = false;
            woodnum.Proportional = true;
            woodnum.Selectable = false;



            tkannum = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(530.0f, 184.0f), "4");
            tkannum.Font = TextDrawFont.Slim;
            tkannum.LetterSize = new SampSharp.GameMode.Vector2(0.287499, 1.149999);
            tkannum.Width = 400.0f;
            tkannum.Height = 17.0f;
            tkannum.Outline = 1;
            tkannum.Shadow = 0;
            tkannum.Alignment = TextDrawAlignment.Left;
            tkannum.ForeColor = -1;
            tkannum.BackColor = 255;
            tkannum.BoxColor = 50;
            tkannum.UseBox = false;
            tkannum.Proportional = true;
            tkannum.Selectable = false;

            rocknum = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(530.0f, 143.0f), "2");
            rocknum.Font = TextDrawFont.Slim;
            rocknum.LetterSize = new SampSharp.GameMode.Vector2(0.287499, 1.149999);
            rocknum.Width = 400.0f;
            rocknum.Height = 17.0f;
            rocknum.Outline = 1;
            rocknum.Shadow = 0;
            rocknum.Alignment = TextDrawAlignment.Left;
            rocknum.ForeColor = -1;
            rocknum.BackColor = 255;
            rocknum.BoxColor = 50;
            rocknum.UseBox = false;
            rocknum.Proportional = true;
            rocknum.Selectable = false;

            weaponslotsAmmo[0] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(376.0f, 220.0f), "0");
            weaponslotsAmmo[0].Font = TextDrawFont.Slim;
            weaponslotsAmmo[0].LetterSize = new SampSharp.GameMode.Vector2(0.212500, 0.899999);
            weaponslotsAmmo[0].Width = 400.0f;
            weaponslotsAmmo[0].Height = 17.0f;
            weaponslotsAmmo[0].Outline = 1;
            weaponslotsAmmo[0].Shadow = 0;
            weaponslotsAmmo[0].Alignment = TextDrawAlignment.Left;
            weaponslotsAmmo[0].ForeColor = -1;
            weaponslotsAmmo[0].BackColor = 255;
            weaponslotsAmmo[0].BoxColor = 50;
            weaponslotsAmmo[0].UseBox = false;
            weaponslotsAmmo[0].Proportional = true;
            weaponslotsAmmo[0].Selectable = false;

            weaponslotsAmmo[1] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(408.0f, 220.0f), "0");
            weaponslotsAmmo[1].Font = TextDrawFont.Slim;
            weaponslotsAmmo[1].LetterSize = new SampSharp.GameMode.Vector2(0.212500, 0.899999);
            weaponslotsAmmo[1].Width = 400.0f;
            weaponslotsAmmo[1].Height = 17.0f;
            weaponslotsAmmo[1].Outline = 1;
            weaponslotsAmmo[1].Shadow = 0;
            weaponslotsAmmo[1].Alignment = TextDrawAlignment.Left;
            weaponslotsAmmo[1].ForeColor = -1;
            weaponslotsAmmo[1].BackColor = 255;
            weaponslotsAmmo[1].BoxColor = 50;
            weaponslotsAmmo[1].UseBox = false;
            weaponslotsAmmo[1].Proportional = true;
            weaponslotsAmmo[1].Selectable = false;

            weaponslotsAmmo[2] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(440.0f, 220.0f), "0");
            weaponslotsAmmo[2].Font = TextDrawFont.Slim;
            weaponslotsAmmo[2].LetterSize = new SampSharp.GameMode.Vector2(0.212500, 0.899999);
            weaponslotsAmmo[2].Width = 400.0f;
            weaponslotsAmmo[2].Height = 17.0f;
            weaponslotsAmmo[2].Outline = 1;
            weaponslotsAmmo[2].Shadow = 0;
            weaponslotsAmmo[2].Alignment = TextDrawAlignment.Left;
            weaponslotsAmmo[2].ForeColor = -1;
            weaponslotsAmmo[2].BackColor = 255;
            weaponslotsAmmo[2].BoxColor = 50;
            weaponslotsAmmo[2].UseBox = false;
            weaponslotsAmmo[2].Proportional = true;
            weaponslotsAmmo[2].Selectable = false;

            weaponslotsAmmo[3] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(376.0f, 242.0f), "0");
            weaponslotsAmmo[3].Font = TextDrawFont.Slim;
            weaponslotsAmmo[3].LetterSize = new SampSharp.GameMode.Vector2(0.212500, 0.899999);
            weaponslotsAmmo[3].Width = 400.0f;
            weaponslotsAmmo[3].Height = 17.0f;
            weaponslotsAmmo[3].Outline = 1;
            weaponslotsAmmo[3].Shadow = 0;
            weaponslotsAmmo[3].Alignment = TextDrawAlignment.Left;
            weaponslotsAmmo[3].ForeColor = -1;
            weaponslotsAmmo[3].BackColor = 255;
            weaponslotsAmmo[3].BoxColor = 50;
            weaponslotsAmmo[3].UseBox = false;
            weaponslotsAmmo[3].Proportional = true;
            weaponslotsAmmo[3].Selectable = false;

            weaponslotsAmmo[4] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(408.0f, 242.0f), "0");
            weaponslotsAmmo[4].Font = TextDrawFont.Slim;
            weaponslotsAmmo[4].LetterSize = new SampSharp.GameMode.Vector2(0.212500, 0.899999);
            weaponslotsAmmo[4].Width = 400.0f;
            weaponslotsAmmo[4].Height = 17.0f;
            weaponslotsAmmo[4].Outline = 1;
            weaponslotsAmmo[4].Shadow = 0;
            weaponslotsAmmo[4].Alignment = TextDrawAlignment.Left;
            weaponslotsAmmo[4].ForeColor = -1;
            weaponslotsAmmo[4].BackColor = 255;
            weaponslotsAmmo[4].BoxColor = 50;
            weaponslotsAmmo[4].UseBox = false;
            weaponslotsAmmo[4].Proportional = true;
            weaponslotsAmmo[4].Selectable = false;

            weaponslotsAmmo[5] = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(440.0f, 242.0f), "0");
            weaponslotsAmmo[5].Font = TextDrawFont.Slim;
            weaponslotsAmmo[5].LetterSize = new SampSharp.GameMode.Vector2(0.212500, 0.899999);
            weaponslotsAmmo[5].Width = 400.0f;
            weaponslotsAmmo[5].Height = 17.0f;
            weaponslotsAmmo[5].Outline = 1;
            weaponslotsAmmo[5].Shadow = 0;
            weaponslotsAmmo[5].Alignment = TextDrawAlignment.Left;
            weaponslotsAmmo[5].ForeColor = -1;
            weaponslotsAmmo[5].BackColor = 255;
            weaponslotsAmmo[5].BoxColor = 50;
            weaponslotsAmmo[5].UseBox = false;
            weaponslotsAmmo[5].Proportional = true;
            weaponslotsAmmo[5].Selectable = false;
            //INVENTORY UP_________________________________________________________________________________________________


        }

        //TEXTDRAWS!!!!
        private void CreateLogo(Player p)
        {
            PlayerTextDraw wasteland = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(547.0f, 23.0f), "wasteland");
            wasteland.Font = TextDrawFont.Pricedown;
            wasteland.LetterSize = new SampSharp.GameMode.Vector2(0.208333f, 1.349997f);
            wasteland.Width = 400;
            wasteland.Height = 17;
            wasteland.Outline = 1;
            wasteland.Shadow = 0;
            wasteland.Alignment = TextDrawAlignment.Left;
            wasteland.ForeColor = -984082177;
            wasteland.BackColor = 255;
            wasteland.BoxColor = 50;
            wasteland.UseBox = false;
            wasteland.Proportional = true;
            wasteland.Selectable = false;
            wasteland.Show();

            PlayerTextDraw warriors = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(560.0f, 4.0f), "Warriors");
            warriors.Font = TextDrawFont.Diploma;
            warriors.LetterSize = new SampSharp.GameMode.Vector2(0.420832, 2.049998);
            warriors.Width = 400;
            warriors.Height = 17;
            warriors.Outline = 1;
            warriors.Shadow = 1;
            warriors.Alignment = TextDrawAlignment.Left;
            warriors.ForeColor = 1513890047;
            warriors.BackColor = 0;
            warriors.BoxColor = 50;
            warriors.UseBox = false;
            warriors.Proportional = true;
            warriors.Selectable = false;
            warriors.Show();
        }
        public void ShowInventory()
        {
            Updater1();
            inventoryBackPlate.Show();
            playerButton.PreviewModel = this.Skin;
            playerButton.Show();
            helmetbutton.Show();
            backpackButton.Show();

            armourButton.Show();
            pagerButton.Show();
            achievementsButton.Show();
            woodLine.Show();
            woodInvTxd.Show();
            rockLine.Show();
            rockInvTxd.Show();
            ironLine.Show();
            ironInvTxd.Show();
            tcanLine.Show();
            tkanInvTxd.Show();
            weaponslots[0].Show();
            weaponslots[1].Show();
            weaponslots[2].Show();
            weaponslots[3].Show();
            weaponslots[4].Show();
            weaponslots[5].Show();
            useButton.Show();
            dropButton.Show();
            sellButton.Show();
            infoButton.Show();
            closeinv.Show();
            InventoryUpdate();
            woodnum.Show();
            rocknum.Show();
            ironnum.Show();
            tkannum.Show();

        }
        public void CloseInventory()
        {
            inventoryBackPlate.Hide();
            playerButton.Hide();
            helmetbutton.Hide();
            backpackButton.Hide();
            armourButton.Hide();
            pagerButton.Hide();
            achievementsButton.Hide();
            woodLine.Hide();
            woodInvTxd.Hide();
            rockLine.Hide();
            rockInvTxd.Hide();
            ironLine.Hide();
            ironInvTxd.Hide();
            tcanLine.Hide();
            tkanInvTxd.Hide();
            weaponslots[0].Hide();
            weaponslots[1].Hide();
            weaponslots[2].Hide();
            weaponslots[3].Hide();
            weaponslots[4].Hide();
            weaponslots[5].Hide();
            useButton.Hide();
            dropButton.Hide();
            sellButton.Hide();
            infoButton.Hide();
            closeinv.Hide();
            slots[0].Hide();
            slots[1].Hide();
            slots[2].Hide();
            slots[3].Hide();
            slots[4].Hide();
            slots[5].Hide();
            slots[6].Hide();
            slots[7].Hide();
            slots[8].Hide();
            slots[9].Hide();
            slots[10].Hide();
            slots[11].Hide();
            slots[12].Hide();
            slots[13].Hide();
            slots[14].Hide();
            slots[15].Hide();
            slots[16].Hide();
            slots[17].Hide();
            slots[18].Hide();
            slots[19].Hide();
            slots[20].Hide();
            slots[21].Hide();
            slots[22].Hide();
            slots[23].Hide();
            slots[24].Hide();
            slots[25].Hide();
            slots[26].Hide();
            slots[27].Hide();
            slots[28].Hide();
            slots[29].Hide();
            slots[30].Hide();
            slots[31].Hide();
            slots[32].Hide();
            slots[33].Hide();
            slots[34].Hide();
            slots[35].Hide();
            slots[36].Hide();
            slots[37].Hide();
            slots[38].Hide();
            slots[39].Hide();
            slots[40].Hide();
            slots[41].Hide();
            woodnum.Hide();
            rocknum.Hide();
            ironnum.Hide();
            tkannum.Hide();
            weaponslotsAmmo[0].Hide();
            weaponslotsAmmo[1].Hide();
            weaponslotsAmmo[2].Hide();
            weaponslotsAmmo[3].Hide();
            weaponslotsAmmo[4].Hide();
            weaponslotsAmmo[5].Hide();
        }

        public void CreateTextdraws(Player p)
        {
            sqlCon.Open();
            var getMoney = new MySqlCommand($"SELECT `Money` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//VODA BD
            pMoney = Convert.ToInt32(getMoney.ExecuteScalar());

            PlayerTextDraw booms = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(513.0f, 9.0f), "100");

            PlayerTextDraw EatPNG = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(614.0f, 6.0f), "HUD:radar_datefood");
            PlayerTextDraw EatNum = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(617.0f, 23.0f), "100");
            PlayerTextDraw WaterPng = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(614.0f, 36.0f), "HUD:radar_diner");
            PlayerTextDraw WaterNum = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(617.0f, 53.0f), "100");
            PlayerTextDraw RadPng = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(614.0f, 66.0f), "HUD:radar_locosyndicate");
            PlayerTextDraw RadNum = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(617.0f, 84.0f), "0.5");

            PlayerTextDraw DangerPlate = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(609.0f, 95.0f), "DANGER!");
            PlayerTextDraw menuPlate1 = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(620.0f, 318.0f), "_");
            //PlayerTD[playerid][11] = CreatePlayerTextDraw(playerid, 612.000000, 345.000000, "LD_SHTR:ps3");
            PlayerTextDraw boomsTurn = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(612.0f, 345.0f), "LD_SHTR:ps3");
            //PlayerTD[playerid][12] = CreatePlayerTextDraw(playerid, 612.000000, 370.000000, "hud:radar_girlfriend");
            PlayerTextDraw healthMenuPNG = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(612.0f, 370.0f), "hud:radar_girlfriend");
            //PlayerTD[playerid][13] = CreatePlayerTextDraw(playerid, 612.000000, 395.000000, "hud:radar_impound");

            PlayerTextDraw carMenuPNG = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(612.0f, 395.0f), "hud:radar_impound");
            // PlayerTD[playerid][14] = CreatePlayerTextDraw(playerid, 612.000000, 420.000000, "hud:radar_gangP");

            PlayerTextDraw CostumeMenuPNG = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(612.0f, 420.0f), "hud:radar_gangP");
            //PlayerTD[playerid][15] = CreatePlayerTextDraw(playerid, 620.000000, 317.000000, "_");
            PlayerTextDraw menuPlate2 = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(620.0f, 317.0f), "_");
            //PlayerTD[playerid][16] = CreatePlayerTextDraw(playerid, 620.000000, 441.000000, "_");

            PlayerTextDraw menuPlate3 = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(620.0f, 441.0f), "_");
            //PlayerTD[playerid][17] = CreatePlayerTextDraw(playerid, 606.799987, 317.000000, "_");
            PlayerTextDraw menuPlate4 = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(606.799987f, 317.0f), "_");
            //PlayerTD[playerid][18] = CreatePlayerTextDraw(playerid, 633.200012, 317.000000, "_");

            PlayerTextDraw menuPlate5 = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(633.200012f, 317.0f), "_");
            //PlayerTD[playerid][19] = CreatePlayerTextDraw(playerid, 612.000000, 321.000000, "hud:radar_modGarage");
            PlayerTextDraw mMenuPNG = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(612.0f, 321.0f), "hud:radar_modGarage");
            var getBooms = new MySqlCommand($"SELECT `booms` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//BOOMS BD

            var getImmunity = new MySqlCommand($"SELECT `Imunity` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//IMMUNITY BD

            var getVoda = new MySqlCommand($"SELECT `Water` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//VODA BD

            var getFood = new MySqlCommand($"SELECT `Food` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon); // FOOD BD

            var getHolod = new MySqlCommand($"SELECT `Holod` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//HOLOD BD

            var getSleep = new MySqlCommand($"SELECT `Sleep` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//SLEEP BD

            var getPiss = new MySqlCommand($"SELECT `Piss` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//PISS BD

            var getNastroy = new MySqlCommand($"SELECT `Nastroy` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//NASTROY BD

            var getGigiena = new MySqlCommand($"SELECT `Gigiena` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//GIGIENA BD

            var getBleed = new MySqlCommand($"SELECT `Bleed` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//BLEED BD

            var getBolezn = new MySqlCommand($"SELECT `Bolezn` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//BOLEZN BD

            var getDanger = new MySqlCommand($"SELECT `Danger` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//DANGER BD

            var getVivih = new MySqlCommand($"SELECT `Vivih` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//VIVIH BD
            booms.Text = (getBooms.ExecuteScalar().ToString());
            booms.Font = TextDrawFont.Normal;
            booms.LetterSize = new SampSharp.GameMode.Vector2(0.345833f, 1.799998f);
            booms.Outline = 1;
            booms.Shadow = 0;
            booms.Alignment = TextDrawAlignment.Left;
            booms.ForeColor = -1;
            booms.BoxColor = 50;
            booms.UseBox = false;
            booms.Proportional = true;
            booms.Selectable = false;
            booms.Show();


            //______________________________________________________________________________________________________________________
            //Down here Name of server

            //______________________________________________________________________________________________


            EatPNG.Font = TextDrawFont.DrawSprite;
            EatPNG.LetterSize = new SampSharp.GameMode.Vector2(0.6, 2.0);
            EatPNG.Width = 17;
            EatPNG.Height = 17;
            EatPNG.Outline = 1;
            EatPNG.Shadow = 0;
            EatPNG.Alignment = TextDrawAlignment.Left;
            EatPNG.ForeColor = -1;
            EatPNG.BackColor = 255;
            EatPNG.BoxColor = 50;
            EatPNG.UseBox = true;
            EatPNG.Proportional = true;
            EatPNG.Selectable = false;
            EatPNG.Show();
            FoodNumber = Convert.ToInt32(getFood.ExecuteScalar());
            EatNum.Text = FoodNumber.ToString();
            EatNum.Font = TextDrawFont.Normal;
            EatNum.LetterSize = new SampSharp.GameMode.Vector2(0.191667f, 1.2f);
            EatNum.Width = 17;
            EatNum.Height = 17;
            EatNum.Outline = 1;
            EatNum.Shadow = 0;
            EatNum.Alignment = TextDrawAlignment.Left;
            EatNum.ForeColor = 1433087999;
            EatNum.BackColor = 255;
            EatNum.BoxColor = 50;
            EatNum.UseBox = false;
            EatNum.Proportional = true;
            EatNum.Selectable = false;
            EatNum.Show();
            EatCount();
            void EatCount()
            {

                var foodTimer = new System.Timers.Timer(47500);
                foodTimer.Elapsed += FoodTimerEvent;
                foodTimer.Enabled = true;
                foodTimer.AutoReset = true;
                void FoodTimerEvent(object source, ElapsedEventArgs e)
                {
                    if (this.IsConnected)
                    {

                        var getFood = new MySqlCommand($"SELECT `Food` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//VODA BD

                        FoodNumber = Convert.ToInt32(getFood.ExecuteScalar());

                        FoodNumber -= 1;

                        var updateFood = new MySqlCommand($"UPDATE `Players` SET `Food` = '{this.FoodNumber}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateFood.ExecuteScalar();
                        EatNum.Text = (this.FoodNumber.ToString());
                        EatNum.Show();



                    }
                    else
                    {
                        foodTimer.Enabled = false;
                        foodTimer.Dispose();
                    }
                }


            }

            WaterPng.Font = TextDrawFont.DrawSprite;
            WaterPng.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            WaterPng.Width = 17;
            WaterPng.Height = 17;
            WaterPng.Outline = 1;
            WaterPng.Shadow = 0;
            WaterPng.Alignment = TextDrawAlignment.Left;
            WaterPng.ForeColor = -1;
            WaterPng.BackColor = 255;
            WaterPng.BoxColor = 50;
            WaterPng.UseBox = true;
            WaterPng.Proportional = true;
            WaterPng.Selectable = false;
            WaterPng.Show();

            waterNumber = Convert.ToInt32(getVoda.ExecuteScalar());
            WaterNum.Text = waterNumber.ToString();
            WaterNum.Font = TextDrawFont.Normal;
            WaterNum.LetterSize = new SampSharp.GameMode.Vector2(0.191667f, 1.2f);
            WaterNum.Width = 17;
            WaterNum.Height = 17;
            WaterNum.Outline = 1;
            WaterNum.Shadow = 0;
            WaterNum.Alignment = TextDrawAlignment.Left;
            WaterNum.ForeColor = 1433087999;
            WaterNum.BackColor = 255;
            WaterNum.BoxColor = 50;
            WaterNum.UseBox = false;
            WaterNum.Proportional = true;
            WaterNum.Selectable = false;
            WaterNum.Show();
            WaterCount();
            void WaterCount()
            {

                var vodaTimer = new System.Timers.Timer(25000);
                vodaTimer.Elapsed += VodaTimerEvent;
                vodaTimer.Enabled = true;
                vodaTimer.AutoReset = true;
                void VodaTimerEvent(object source, ElapsedEventArgs e)
                {
                    if (this.IsConnected)
                    {

                        var getVoda = new MySqlCommand($"SELECT `Water` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);//VODA BD

                        waterNumber = Convert.ToInt32(getVoda.ExecuteScalar());

                        waterNumber -= 1;

                        var updateVoda = new MySqlCommand($"UPDATE `Players` SET `Water` = '{this.waterNumber}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateVoda.ExecuteScalar();
                        WaterNum.Text = (this.waterNumber.ToString());
                        WaterNum.Show();



                    }
                    else
                    {
                        vodaTimer.Enabled = false;
                        vodaTimer.Dispose();
                    }
                }


            }

            RadPng.Font = TextDrawFont.DrawSprite;
            RadPng.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            RadPng.Width = 17;
            RadPng.Height = 17;
            RadPng.Outline = 1;
            RadPng.Shadow = 0;
            RadPng.Alignment = TextDrawAlignment.Left;
            RadPng.ForeColor = -1;
            RadPng.BackColor = 255;
            RadPng.BoxColor = 50;
            RadPng.UseBox = true;
            RadPng.Proportional = true;
            RadPng.Selectable = false;
            RadPng.Show();

            RadNum.Font = TextDrawFont.Normal;
            RadNum.LetterSize = new SampSharp.GameMode.Vector2(0.191667f, 1.2f);
            RadNum.Width = 17;
            RadNum.Height = 17;
            RadNum.Outline = 1;
            RadNum.Shadow = 0;
            RadNum.Alignment = TextDrawAlignment.Left;
            RadNum.ForeColor = -1;
            RadNum.BackColor = 255;
            RadNum.BoxColor = 50;
            RadNum.UseBox = false;
            RadNum.Proportional = true;
            RadNum.Selectable = false;
            RadNum.Show();


            DangerPlate.Font = TextDrawFont.Normal;
            DangerPlate.LetterSize = new SampSharp.GameMode.Vector2(0.183330, 1.799998);
            DangerPlate.Width = 400;
            DangerPlate.Height = 17;
            DangerPlate.Outline = 1;
            DangerPlate.Shadow = 0;
            DangerPlate.Alignment = TextDrawAlignment.Left;
            DangerPlate.ForeColor = -16776961;
            DangerPlate.BackColor = 255;
            DangerPlate.BoxColor = 50;
            DangerPlate.UseBox = false;
            DangerPlate.Proportional = true;
            DangerPlate.Selectable = false;
            DangerPlate.Show();

            menuPlate1.Font = TextDrawFont.Diploma;
            menuPlate1.LetterSize = new SampSharp.GameMode.Vector2(0.6, 13.399991);
            menuPlate1.Width = 251.5f;
            menuPlate1.Height = 21.5f;
            menuPlate1.Outline = 1;
            menuPlate1.Shadow = 0;
            menuPlate1.Alignment = TextDrawAlignment.Center;
            menuPlate1.ForeColor = -1;
            menuPlate1.BackColor = 255;
            menuPlate1.BoxColor = 135;
            menuPlate1.UseBox = true;
            menuPlate1.Proportional = true;
            menuPlate1.Selectable = false;
            menuPlate1.Show();


            boomsTurn.Font = TextDrawFont.DrawSprite;
            boomsTurn.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            boomsTurn.Width = 17;
            boomsTurn.Height = 17;
            boomsTurn.Outline = 1;
            boomsTurn.Shadow = 0;
            boomsTurn.Alignment = TextDrawAlignment.Left;
            boomsTurn.ForeColor = -1;
            boomsTurn.BackColor = 255;
            boomsTurn.BoxColor = 50;
            boomsTurn.UseBox = false;
            boomsTurn.Proportional = true;
            boomsTurn.Selectable = true;
            boomsTurn.Show();
            boomsTDID = boomsTurn.Id;


            healthMenuPNG.Font = TextDrawFont.DrawSprite;
            healthMenuPNG.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            healthMenuPNG.Width = 17;
            healthMenuPNG.Height = 17;
            healthMenuPNG.Outline = 1;
            healthMenuPNG.Shadow = 0;
            healthMenuPNG.Alignment = TextDrawAlignment.Left;
            healthMenuPNG.ForeColor = -1;
            healthMenuPNG.BackColor = 255;
            healthMenuPNG.BoxColor = 50;
            healthMenuPNG.UseBox = false;
            healthMenuPNG.Proportional = true;
            healthMenuPNG.Selectable = true;
            healthMenuPNG.Show();

            carMenuPNG.Font = TextDrawFont.DrawSprite;
            carMenuPNG.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            carMenuPNG.Width = 17;
            carMenuPNG.Height = 17;
            carMenuPNG.Outline = 1;
            carMenuPNG.Shadow = 0;
            carMenuPNG.Alignment = TextDrawAlignment.Left;
            carMenuPNG.ForeColor = -1;
            carMenuPNG.BackColor = 255;
            carMenuPNG.BoxColor = 50;
            carMenuPNG.UseBox = false;
            carMenuPNG.Proportional = true;
            carMenuPNG.Selectable = true;
            carMenuPNG.Show();

            CostumeMenuPNG.Font = TextDrawFont.DrawSprite;
            CostumeMenuPNG.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            CostumeMenuPNG.Width = 17;
            CostumeMenuPNG.Height = 17;
            CostumeMenuPNG.Outline = 1;
            CostumeMenuPNG.Shadow = 0;
            CostumeMenuPNG.Alignment = TextDrawAlignment.Left;
            CostumeMenuPNG.ForeColor = -1;
            CostumeMenuPNG.BackColor = 255;
            CostumeMenuPNG.BoxColor = 50;
            CostumeMenuPNG.UseBox = false;
            CostumeMenuPNG.Proportional = true;
            CostumeMenuPNG.Selectable = true;
            CostumeMenuPNG.Show();

            menuPlate2.Font = TextDrawFont.Normal;
            menuPlate2.LetterSize = new SampSharp.GameMode.Vector2(0.6, 0.049988);
            menuPlate2.Width = 251.5f;
            menuPlate2.Height = 27.5f;
            menuPlate2.Outline = 1;
            menuPlate2.Shadow = 0;
            menuPlate2.Alignment = TextDrawAlignment.Center;
            menuPlate2.ForeColor = -1;
            menuPlate2.BackColor = 255;
            menuPlate2.BoxColor = 255;
            menuPlate2.UseBox = true;
            menuPlate2.Proportional = true;
            menuPlate2.Selectable = false;
            menuPlate2.Show();

            menuPlate3.Font = TextDrawFont.Normal;
            menuPlate3.LetterSize = new SampSharp.GameMode.Vector2(0.6, 0.049988);
            menuPlate3.Width = 251.5f;
            menuPlate3.Height = 27.5f;
            menuPlate3.Outline = 1;
            menuPlate3.Shadow = 0;
            menuPlate3.Alignment = TextDrawAlignment.Center;
            menuPlate3.ForeColor = -1;
            menuPlate3.BackColor = 255;
            menuPlate3.BoxColor = 255;
            menuPlate3.UseBox = true;
            menuPlate3.Proportional = true;
            menuPlate3.Selectable = false;
            menuPlate3.Show();

            menuPlate4.Font = TextDrawFont.Normal;
            menuPlate4.LetterSize = new SampSharp.GameMode.Vector2(0.6, 13.549992);
            menuPlate4.Width = 251.5f;
            menuPlate4.Height = 1.5f;
            menuPlate4.Outline = 1;
            menuPlate4.Shadow = 0;
            menuPlate4.Alignment = TextDrawAlignment.Center;
            menuPlate4.ForeColor = -1;
            menuPlate4.BackColor = 255;
            menuPlate4.BoxColor = 255;
            menuPlate4.UseBox = true;
            menuPlate4.Proportional = true;
            menuPlate4.Selectable = false;
            menuPlate4.Show();


            menuPlate5.Font = TextDrawFont.Normal;
            menuPlate5.LetterSize = new SampSharp.GameMode.Vector2(0.6, 13.549992);
            menuPlate5.Width = 251.5f;
            menuPlate5.Height = 1.5f;
            menuPlate5.Outline = 1;
            menuPlate5.Shadow = 0;
            menuPlate5.Alignment = TextDrawAlignment.Center;
            menuPlate5.ForeColor = -1;
            menuPlate5.BackColor = 255;
            menuPlate5.BoxColor = 255;
            menuPlate5.UseBox = true;
            menuPlate5.Proportional = true;
            menuPlate5.Selectable = false;
            menuPlate5.Show();

            mMenuPNG.Font = TextDrawFont.DrawSprite;
            mMenuPNG.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            mMenuPNG.Width = 17;
            mMenuPNG.Height = 17;
            mMenuPNG.Outline = 1;
            mMenuPNG.Shadow = 0;
            mMenuPNG.Alignment = TextDrawAlignment.Left;
            mMenuPNG.ForeColor = -1;
            mMenuPNG.BackColor = 255;
            mMenuPNG.BoxColor = 50;
            mMenuPNG.UseBox = false;
            mMenuPNG.Proportional = true;
            mMenuPNG.Selectable = true;
            mMenuPNG.Show();
            //____________________________________________________________________________
            PlayerTextDraw BackPlate = new PlayerTextDraw(p, new SampSharp.GameMode.Vector2(75.0f, 318.0f), "_");

            //19478
            BackPlate.Font = TextDrawFont.Normal;
            BackPlate.LetterSize = new SampSharp.GameMode.Vector2(0.808332f, 14.350014f);
            BackPlate.Outline = 1;
            BackPlate.Width = 101.5f;
            BackPlate.Height = 156.0f;
            BackPlate.Shadow = 0;
            BackPlate.Alignment = TextDrawAlignment.Center;
            BackPlate.ForeColor = -1;
            BackPlate.BackColor = 255;
            BackPlate.BoxColor = 50;
            BackPlate.UseBox = true;
            BackPlate.Proportional = true;
            BackPlate.Selectable = false;
            BackPlate.Show();

            void TextDrawUpdater()
            {

                var vodaTimer = new System.Timers.Timer(1000);
                vodaTimer.Elapsed += VodaTimerEvent;
                vodaTimer.Enabled = true;
                vodaTimer.AutoReset = true;
                void VodaTimerEvent(object source, ElapsedEventArgs e)
                {
                    if (this.IsConnected)
                    {


                        WaterNum.Text = (waterNumber.ToString());
                        WaterNum.Show();
                        var updateWater = new MySqlCommand($"UPDATE `Players` SET `Water` = '{this.waterNumber}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateWater.ExecuteScalar();

                        booms.Text = (PlayerBooms.ToString());
                        booms.Show();
                        EatNum.Text = (FoodNumber.ToString());
                        EatNum.Show();
                        var updateFood = new MySqlCommand($"UPDATE `Players` SET `Food` = '{this.FoodNumber}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateFood.ExecuteNonQuery();

                        var updateBooms = new MySqlCommand($"UPDATE `Players` SET `booms` = '{this.PlayerBooms}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateBooms.ExecuteScalar();
                        var updateMoney = new MySqlCommand($"UPDATE `Players` SET `Money` = '{this.pMoney}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);//VODA BD
                        updateMoney.ExecuteScalar();
                        if (this.Money != this.pMoney)
                        {
                            this.Money = this.pMoney;
                        }


                    }
                    else
                    {
                        vodaTimer.Enabled = false;
                        vodaTimer.Dispose();
                    }
                }


            }
            TextDrawUpdater();
            sqlCon.Close();




        }
        public override void OnCancelClickTextDraw(PlayerEventArgs e)
        {
            base.OnCancelClickTextDraw(e);
            CloseInventory();

        }
        public void SelectSlot(int i)
        {
            for (int j = 0; j < 41; j++)
            {
                if (j != i)
                {
                    isSlotSelected[j] = false;
                    slots[j].BackColor = -1094795651;
                }
                else
                {
                    isSlotSelected[j] = true;
                    slots[j].BackColor = -764862878;
                    slots[j].BoxColor = -764862878;
                }
            }

        }
        public override void OnClickPlayerTextDraw(ClickPlayerTextDrawEventArgs e)
        {

            base.OnClickPlayerTextDraw(e);
            if (e.PlayerTextDraw == closeinv)
            {
                CloseInventory();
                this.CancelSelectTextDraw();
            }
            for (int j = 0; j < weaponslots.Length; j++) {
                if (e.PlayerTextDraw == weaponslots[j])
                {
                    OnUseWeaponSlot(j);
                }
            }
            if (e.PlayerTextDraw.Id == boomsTDID)
            {
                //36401
                this.PlaySound(36401);
                if (isBooms == false)
                {
                    isBooms = true;
                    SendClientMessage("Взрывные боеприпасы {ff0000}активированы.");
                }
                else if (isBooms == true)
                {
                    isBooms = false;
                    SendClientMessage("Взрывные боеприпасы {6ac2f2}деактивированы.");
                }

                this.CancelSelectTextDraw();
            }
            if (e.PlayerTextDraw == useButton)
            {

                for (int i = 0; i < 41; i++)
                {
                    if (isSlotSelected[i] == true)
                    {

                        OnUseInventorySlot(i);
                    }
                }
            }
            if (e.PlayerTextDraw == dropButton)
            {
                for (int i = 0; i < 41; i++)
                {
                    if (isSlotSelected[i] == true)
                    {

                        if (slotsinfo[i] == 0)
                        {

                        }
                        else
                        {
                            GameMode.serverLoots.Add(new Loot(lootItems.Find(item => item.Id == slotsinfo[i]), this.Position, this.VirtualWorld));
                        }

                        slotsinfo[i] = 0;
                        var updateMoney = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '0' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateMoney.ExecuteNonQuery();
                        CloseInventory();
                        CancelSelectTextDraw();
                    }
                }
            }
            if (e.PlayerTextDraw == infoButton)
            {

                for (int i = 0; i < 41; i++)
                {
                    if (isSlotSelected[i] == true)
                    {

                        LootInfoShow(i);
                    }
                }
            }

            for (int i = 0; i < 41; i++)
            {
                if (e.PlayerTextDraw.Id == slots[i].Id)
                {
                    SelectSlot(i);
                }
            }
            /**
            for(int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 41; i++)
                {
                    if (isSlotSelected[i] == true)
                    {
                        if(e.PlayerTextDraw == weaponslots[j])
                        {
                            if (lootItems.Find(m => m.Id == slotsinfo[i]).LootType == 2)
                            {
                                weaponSlotsInfo[j] = slotsinfo[i];
                                weaponslots[j].PreviewModel = lootItems.Find(m => m.Id == slotsinfo[i]).modelId;
                                slotsinfo[i] = 0;
                                slots[i].PreviewModel = 19478;
                                var updateMoney = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '0' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                                updateMoney.ExecuteNonQuery();
                                var updateweaponslot = new MySqlCommand($"UPDATE `Players` SET `weaponslot{j}` = '{weaponSlotsInfo[j]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                                updateweaponslot.ExecuteNonQuery();
                            }
                            if(lootItems.Find(m => m.Id == slotsinfo[i]).LootType == 7)
                            {
                                if (weaponSlotsInfo[j] == 6)
                                {
                                    var updateMoney = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '0' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                                    updateMoney.ExecuteNonQuery();
                                    var updateweaponslot = new MySqlCommand($"UPDATE `Players` SET `weaponslotsAmmo{j}` = '50' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                                    updateweaponslot.ExecuteNonQuery();
                                    weaponslotsAmmo[j].Text = "50";
                                    weaponslotsAmmo[j].Show();
                                    
                                }
                            }
                        }
                        
                    }
                }
            }**/



        }

        //  TextDraw virusPNG = new TextDraw(new SampSharp.GameMode.Vector2(10.0f, 427.0f), "virus : 30%");

        public override void OnConnected(EventArgs e)
        {
            base.OnConnected(e);


            // base.CameraPosition = new SampSharp.GameMode.Vector3(189.96f, 1918.68, 29.92);



        }
        [Command("tank")]
        private void UseTank()
        {
            tankAmount = 3;
            SendClientMessage("Вы врубили модуль танк, он защищает от 3 попаданий в тело взрывными боеприпасами");
        }
        /**
        [Command("MakeGun")]
        private void MakeGun(Weapon WeaponId, int Bullets)
        {
            
            GiveWeapon(WeaponId, Bullets);
            
        }
        **/
        public override void OnRequestClass(RequestClassEventArgs e)
        {
            CreateItems();
            base.OnRequestClass(e);
            CreateLogo(this);
            if (this.IsConnected)
            {

                this.PlayAudioStream("https://rux.muzmo.cc/get/music/20190927/Syn_-_28_Days_Later_Skadespelare_edit_cut_66754918.mp3");
                this.ToggleSpectating(true);
                this.InterpolateCameraPosition(new SampSharp.GameMode.Vector3(-831.23755, 664.3757, 62.61588), new SampSharp.GameMode.Vector3(2018.7303, 2957.5984, 60.81522), 110000, CameraCut.Move);
                this.InterpolateCameraLookAt(new SampSharp.GameMode.Vector3(-831.23755, 664.3757, 62.61588), new SampSharp.GameMode.Vector3(2018.7303, 2957.5984, 60.81522), 1000, CameraCut.Cut);
                Autorization();
            }
        }
        public override void OnRequestSpawn(RequestSpawnEventArgs e)
        {
            base.OnRequestSpawn(e);

            Kick();


        }


        [Command("spawnCar")]

        private void MakeCar(VehicleModelType vh, float carHp)
        {
            if (carHp <= 50000)
            {
                var sv = BaseVehicle.Create(vh, this.Position, this.Angle, 1, 1, 1800, false);
                sv.Health = carHp;
                sv.Engine = false;
                sv.AddComponent(1010);
                int cSet = sv.Id;
                this.PutInVehicle(sv, 0);
            }
        }
        [Command("repairCar")]

        public void RepairCar()
        {
            if (this.InAnyVehicle == true)
            {
                var healthCar = this.Vehicle.Health;
                this.Vehicle.Repair();
                this.Vehicle.Health = healthCar;

            }
        }
        [Command("fixCar")]
        public void FixCar()
        {
            if(this.InAnyVehicle == true)
            {
                if(this.Vehicle.Health + 1000 <= 50000)
                {
                    this.Vehicle.Health += 1000;
                }
            }
        }
        public override void OnClickMap(PositionEventArgs e)
        {
            base.OnClickMap(e);
            this.Position = e.Position;

        }
        [Command("booms")]
        public void TurnOnBombs()
        {
            if (!isBooms)
            {
                SendClientMessage("{00ffff}взрывки активированы");
                isBooms = true;
            }
            else if (isBooms)
            {
                SendClientMessage("{00ffff}взрывки деактивированы");
                isBooms = false;
            }
            CancelSelectTextDraw();
        }
        [Command("buyBooms")]
        public void BuyBooms(int amount)
        {
            if (PlayerBooms + amount <= 30)
            {
                PlayerBooms += amount;
               // booms.Text = PlayerBooms.ToString();
            }
            else
            {
                this.SendClientMessage("{5c1c12}Вы не можете иметь более 30 взрывных боеприпасов!");
            }
        }
        [Command("glava")]
        public void TpToGlava()
        {

            this.Position = new SampSharp.GameMode.Vector3(2217.083, 1585.277, 1000);
            this.VirtualWorld = 1010;
            this.Interior = 1;
            this.SetTime(12,30);
        }
        [Command("shoptp")]
        public void TeleportToShop()
        {
            this.Position = new SampSharp.GameMode.Vector3(1330.74, 1360.70, 3001.11);
            this.VirtualWorld = 1003;
            this.Interior = 1;
        }
        [Command("healme")]
        public void HealPlayer()
        {
            this.Health = 100;
            this.Armour = 100;
        }
        [Command("tp")]
        public void TeleportToPlayer(int id)
        {
            this.Position = Find(id).Position;
        }
        public override void OnWeaponShot(WeaponShotEventArgs e)
        {
            
            base.OnWeaponShot(e);
            if(e.Weapon == Weapon.M4)
            {
                
                weaponslotsAmmoNum[0] -= 1;
                if (weaponslotsAmmoNum[0] < 0)
                {
                    weaponslotsAmmoNum[0] = 0;
                    this.Kick();
                }
            }
            if (e.Weapon == Weapon.Deagle)
            {

                weaponslotsAmmoNum[1] -= 1;
                if (weaponslotsAmmoNum[1] < 0)
                {
                    weaponslotsAmmoNum[1] = 0;
                    this.Kick();
                }
            }
            if (isBooms && PlayerBooms > 0 && (e.Weapon != Weapon.Spraycan) && (e.Weapon != Weapon.Cane) && (e.Weapon != Weapon.Bat) && (e.Weapon != Weapon.Knife) && (e.Weapon != Weapon.None))
            {
                SampSharp.GameMode.Vector3 shotVec = e.Position;
                PlayerBooms--;
                // booms.Text = PlayerBooms.ToString();
                if (GameMode.ar.IsInArea(shotVec) == false)
                {
                    CreateExplosionForAll(shotVec, ExplosionType.LargeVisibleDamage, 30);
                }
            }



        }
        [Command("s")]
        public void SavePosition()
        {
            PlayerSavedPosition = this.Position;
            SendClientMessage($"Вы сохранили позицию.{PlayerSavedPosition} {this.Angle}");
        }
        [Command("r")]
        public void ReturnToSavedPosition()
        {
            this.Position = PlayerSavedPosition;
            SendClientMessage("Вы телепортировались на сохраненную позицию");
        }

        public override void OnGiveDamage(DamageEventArgs e)
        {
            base.OnGiveDamage(e);
            
            this.SetChatBubble("АГРЕССОР", SampSharp.GameMode.SAMP.Color.DarkRed, 10, 1500);
            if (isBooms == true && PlayerBooms > 0 && (e.Weapon != Weapon.Cane) && (e.Weapon != Weapon.Bat) && (e.Weapon != Weapon.Knife) && (e.Weapon != Weapon.None) && (e.Weapon != Weapon.Brassknuckle) && (e.Weapon != Weapon.Golfclub) && (e.Weapon != Weapon.Shovel) && (e.Weapon != Weapon.Chainsaw) && (e.Weapon != Weapon.Dildo) && (e.Weapon != Weapon.DoubleEndedDildo) && (e.Weapon != Weapon.Vibrator) && (e.Weapon != Weapon.SilverVibrator) && (e.Weapon != Weapon.Flower) && (e.Weapon != Weapon.Grenade) && (e.Weapon != Weapon.Teargas) && (e.Weapon != Weapon.Moltov) && (e.Weapon != Weapon.SatchelCharge) && (e.Weapon != Weapon.FireExtinguisher) && (e.Weapon != Weapon.Spraycan))
            {
                if (((Player)e.OtherPlayer).tankAmount > 0)
                {
                    ((Player)e.OtherPlayer).tankAmount -= 1;
                }
                else {
                    CreateExplosionForAll(e.OtherPlayer.Position, ExplosionType.SmallVisibleDamage, 5);
                    e.OtherPlayer.Health = 0;
                }
            }
            if (this.Weapon == Weapon.Sniper && (e.BodyPart == BodyPart.Head) ) {
                e.OtherPlayer.SendClientMessage("{FF0000}Вам попали в голову из снайперской винтовки, {FFFFFF}ваши мозги превратились в кашу!");
                e.OtherPlayer.Health = 0;
                
            }
        }
        [Command("carcolor")]
        public void CarColor(int vc1, int vc2)
        {
            if (InAnyVehicle)
            {
                this.Vehicle.ChangeColor(vc1, vc2);
            }
        }

       

        
        public override void OnTakeDamage(DamageEventArgs e)
        {
            base.OnTakeDamage(e);
            
        }
        
        public override void OnDisconnected(DisconnectEventArgs e)
        {
            /**
            sqlCon.Open();
            var getX = new MySqlCommand($"UPDATE `Players` SET `exitposX` = '{exitposX}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
            var getY = new MySqlCommand($"UPDATE `Players` SET `exitposY` = '{exitposY}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
            var getZ = new MySqlCommand($"UPDATE `Players` SET `exitposZ` = '{exitposZ}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
            getX.ExecuteNonQuery();
            getY.ExecuteNonQuery();
            getZ.ExecuteNonQuery();
            sqlCon.Close();
            **/
            this.Dispose();
            base.OnDisconnected(e);
        }
        public override void OnSpawned(SpawnEventArgs e)
        {
            this.StopAudioStream();
            var getSkin = new MySqlCommand($"SELECT `SkinId` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
            //-404.76 2194.89 42.36 269.95
            SetPlayerMarker(this, SampSharp.GameMode.SAMP.Color.DimGray);
            //-138,64566, 1219,6842, 19,742188
            SetSpawnInfo(0, Convert.ToInt32(getSkin.ExecuteScalar()), new SampSharp.GameMode.Vector3(x: -138.64566f, y: 1219.6842f, z: 19.742188f), 269.95f);
            this.Skin = Convert.ToInt32(getSkin.ExecuteScalar());
            var getMoney = new MySqlCommand($"SELECT `Money` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
            this.Money = Convert.ToInt32(getMoney.ExecuteScalar());

            base.OnSpawned(e);
            
            sqlCon.Open();
            



        }
        [Command("cameratest")]
        public void CameraTest()
        {
            
            
        }


        public void Autorization()
        {

            int attemps = 4;
            
            
            sqlCon.Open();
            MySqlCommand initPlayer = new MySqlCommand($"SELECT COUNT(*) FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
            if (Convert.ToInt32(initPlayer.ExecuteScalar()) == 1) {
                var authDialog = new InputDialog("{fff536}Авторизация", $"{{ffffff}}Уважаемый {{6fd4f2}}{this.Name}, {{ffffff}}рады снова приветствовать вас на {{cf6611}} Wasteland {{66615d}}warriors. \n {{ffffff}}Данный аккаунт {{b53a40}}зарегестрирован!\n\n{{ffffff}}Для продолжения авторизации введите ваш пароль ниже:", true, "Принять", "Отмена");
                
                authDialog.Response += authDialog_Response;

                

                authDialog.Show(this);
                void authDialog_Response(object sender, DialogResponseEventArgs e)
                {

                    if (e.DialogButton == DialogButton.Left)
                    {
                        var getPassword = new MySqlCommand($"SELECT COUNT(*) FROM `Players` WHERE `NickName` = '{Name}' AND `Password` = '{e.InputText}'", sqlCon);
                        if (Convert.ToInt32(getPassword.ExecuteScalar()) == 1)
                        {

                            var getSkin = new MySqlCommand($"SELECT `SkinId` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
                            //-138,64566, 1219,6842, 19,742188
                            //2177.5847, 1584.5847 , 1000, 270
                            
                            this.SetSpawnInfo(0, Convert.ToInt32(getSkin.ExecuteScalar()), new SampSharp.GameMode.Vector3(2177.5847, 1584.5847, 1000), 270f);
                            this.Interior = 1;
                            this.VirtualWorld = 1010;
                            this.PutCameraBehindPlayer();
                            sqlCon.Close();
                            isAutorised = true;
                            // WaterCount(this);
                            this.ToggleSpectating(false);
                            this.Spawn();
                            CreateInventory(this);
                            CreateTextdraws(this);

                            UpdateInventorySlotsFromBD();
                            WaterDeath();
                            this.GiveWeaponOnConnect();


                        }
                        else {
                            attemps--;
                            authDialog.Message = $"Вы ввели неправильный пароль!\n У вас осталось {attemps} попытки";
                            authDialog.Show(this);
                            
                            if (attemps == 0)
                            {
                                SendClientMessage("Вы исчерпали ваши попытки!");
                                Kick();
                            }
                        }
                    }
                    else
                    {
                        SendClientMessage("Вы отказались от Авторизации");
                        Kick();
                    }
                }

            }
            else
            {
                var registerDialog = new InputDialog("{fff536}Регистрация", $"{{ffffff}}Уважаемый {{6fd4f2}}{this.Name}, {{ffffff}}рады приветствовать вас на проекте {{cf6611}} Wasteland {{66615d}}warriors. \n {{ffffff}}Данный аккаунт {{20e333}}не зарегестрирован.\n\n{{ffffff}}Для продолжения регистрации введите ваш будущий пароль ниже:", true, "Принять", "Отмена");
                
                registerDialog.Response += registerDialog_Response;
                registerDialog.Show(this);
                void registerDialog_Response(object sender, DialogResponseEventArgs e)
                {
                    if (e.DialogButton == DialogButton.Left)
                    {
                        
                        var AddUser = new MySqlCommand($"INSERT INTO `Players` (`NickName` , `Password`) VALUES ('{Name}', '{e.InputText}')", sqlCon);
                        AddUser.ExecuteNonQuery();
                        
                        var getSkin = new MySqlCommand($"SELECT `SkinId` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
                        //-138,64566, 1219,6842, 19,742188
                        this.SetSpawnInfo(0, Convert.ToInt32(getSkin.ExecuteScalar()), new SampSharp.GameMode.Vector3(x: -138.64566f, y: 1219.6842f, z: 19.742188), 355.68f);
                        this.PutCameraBehindPlayer();
                        
                        sqlCon.Close();
                        isAutorised = true;
                        this.ToggleSpectating(false);
                        
                        // WaterCount(this);
                        this.Spawn();
                        
                        CreateInventory(this);
                        CreateTextdraws(this);
                        
                        UpdateInventorySlotsFromBD();
                        WaterDeath();


                    }
                    else
                    {
                        SendClientMessage("Вы отменили регистрацию!");
                        Kick();
                    }
                }
                
            }



        }
        
        public override void OnUpdate(PlayerUpdateEventArgs e)
        {
            /**
            exitposX = this.Position.X;
            exitposY = this.Position.Y;
            exitposZ = this.Position.Z;
            **/

            base.OnUpdate(e);
            
            
            
        }
        public override void OnKeyStateChanged(KeyStateChangedEventArgs e)
        {
            base.OnKeyStateChanged(e);
            
                
                if (this.VirtualWorld == 1002 && this.IsInRangeOfPoint(2, new SampSharp.GameMode.Vector3(497.05862, -76.04029, 998.7578)) && (e.NewKeys == Keys.SecondaryAttack || e.OldKeys == Keys.SecondaryAttack))
            {
                var barmenu = new TablistDialog("{FF0a00}Бар", new[] {"{ffffff}Название", "{ffffff}Цена"},"Принять", "Отмена");
                barmenu.Add(new[] { "{3a9cde}Вода", "{1fbf37}10$"});
                barmenu.Add(new[] { "{49e374}Газировка Sprunk", "{1fbf37}15$" });
                barmenu.Add(new[] { "{c2ac40}Сэндвич", "{1fbf37}20$"});
                barmenu.Add(new[] { "{856542}Жаренное мясо", "{1fbf37}40$"});
                barmenu.Add(new[] { "{e8b715}Пиво", "{1fbf37}20$" });
                barmenu.Add(new[] { "{c25211}Виски \"Пойло мародера\"", "{1fbf37}25$",});
                barmenu.Add(new[] { "{adcccb}Водка \"Красная угроза\"", "{1fbf37}20$" });
                barmenu.Add(new[] { "{cfd989}Комплексный обед", "{1fbf37}100$" });
                barmenu.Response += barmenuDialog_Response;
                barmenu.Show(this);
                void barmenuDialog_Response(Object Sender, DialogResponseEventArgs e)
                {
                    if (e.DialogButton != DialogButton.Right)
                    {
                        switch (e.ListItem)
                        {
                            case 0:
                                if (this.pMoney >= 10)
                                {
                                    this.pMoney -= 10;
                                    this.ApplyAnimation("BAR", "DNK_STNDM_LOOP", 1, false, false,false,false,2000);
                                    if(this.waterNumber <= 80)
                                    {
                                        this.waterNumber += 20;
                                    }
                                    else
                                    {
                                        this.waterNumber = 100;
                                    }

                                }
                                break;
                            case 1:
                                if (this.pMoney >= 15)
                                {
                                    this.pMoney -= 15;
                                    this.ApplyAnimation("BAR", "DNK_STNDM_LOOP", 1, false, false, false, false, 2000);
                                    if (this.waterNumber <= 80)
                                    {
                                        this.waterNumber += 20;
                                    }
                                    else
                                    {
                                        this.waterNumber = 100;
                                    }

                                }

                                break;
                            case 2:
                                if (this.pMoney >= 20)
                                {
                                    this.pMoney -= 20;
                                    this.ApplyAnimation("VENDING", "VEND_EAT1_P", 1, false, false, false, false, 2000);
                                    if (this.FoodNumber <= 90)
                                    {
                                        this.FoodNumber += 10;
                                    }
                                    else
                                    {
                                        this.FoodNumber = 100;
                                    }

                                }
                                break;
                            case 3:
                                if (this.pMoney >= 40)
                                {
                                    this.pMoney -= 40;
                                    this.ApplyAnimation("VENDING", "VEND_EAT1_P", 1, false, false, false, false, 2000);
                                    if (this.FoodNumber <= 75)
                                    {
                                        this.FoodNumber += 25;
                                    }
                                    else
                                    {
                                        this.FoodNumber = 100;
                                    }

                                }
                                break;
                            case 4:
                                if (this.pMoney >= 20)
                                {
                                    this.pMoney -= 20;
                                    this.ApplyAnimation("BAR", "DNK_STNDM_LOOP", 1, false, false, false, false, 2000);
                                    if (this.waterNumber <= 80)
                                    {
                                        this.waterNumber += 20;
                                    }
                                    else
                                    {
                                        this.waterNumber = 100;
                                    }

                                }
                                break;
                            case 5:
                                if (this.pMoney >= 25)
                                {
                                    this.pMoney -= 25;
                                    this.ApplyAnimation("BAR", "DNK_STNDM_LOOP", 1, false, false, false, false, 2000);
                                    if (this.waterNumber <= 90)
                                    {
                                        this.waterNumber += 10;
                                    }
                                    else
                                    {
                                        this.waterNumber = 100;
                                    }

                                }
                                break;
                            case 6:
                                if (this.pMoney >= 20)
                                {
                                    this.pMoney -= 20;
                                    this.ApplyAnimation("BAR", "DNK_STNDM_LOOP", 1, false, false, false, false, 2000);
                                    if (this.waterNumber <= 90)
                                    {
                                        this.waterNumber += 10;
                                    }
                                    else
                                    {
                                        this.waterNumber = 100;
                                    }

                                }
                                break;
                            case 7:
                                if (this.pMoney >= 100)
                                {
                                    this.pMoney -= 100;
                                    this.ApplyAnimation("VENDING", "VEND_EAT1_P", 1, false, false, false, false, 2000);
                                    this.FoodNumber = 100;
                                    this.waterNumber = 100;

                                }

                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            {

            }
            
            if (this.IsInRangeOfPoint(5, new SampSharp.GameMode.Vector3(-175.88075f, 1226.6819f, 21.030312f)) && e.NewKeys == Keys.SecondaryAttack)
            {
                var mactepMenu = new ListDialog("Ремесленник", "принять", "отмена");
                mactepMenu.AddItem("Бронежилет");
                mactepMenu.AddItem("{47a3d1}Оружие m4");
                mactepMenu.AddItem("{47a3d1}Оружие desert eagle");
                mactepMenu.AddItem("{47a3d1}Оружие SawedOff");
                mactepMenu.AddItem("{ff0000}Взрывные боеприпасы");
                mactepMenu.Response += mactepMenuDialog_Response;

                mactepMenu.Show(this);
            }
                void mactepMenuDialog_Response(Object Sender, DialogResponseEventArgs e) 
                {
                if (e.DialogButton != DialogButton.Right)
                {
                    switch (e.ListItem)
                    {
                        case 0:
                            this.Armour = 100;
                            SendClientMessage("Вы скрафтили бронежилет");

                            break;
                        case 1:
                            this.GiveWeapon(Weapon.M4, 100);
                            SendClientMessage("Вы скрафтили m4");
                            break;
                        case 2:
                            this.GiveWeapon(Weapon.Deagle, 50);
                            SendClientMessage("Вы скрафтили Desert eagle");

                            break;
                        case 3:
                            this.GiveWeapon(Weapon.Sawedoff, 24);
                            SendClientMessage("Вы скрафтили sawedOff");

                            break;
                        case 4:
                            var boomsInput = new InputDialog("Взрывные боеприпасы", "Введите количество взрывных боеприпасов:", false, "принять", "отклонить");
                            boomsInput.Response += boomsInputDialog_Response;
                            boomsInput.Show(this);
                            void boomsInputDialog_Response(object Sender, DialogResponseEventArgs e)
                            {
                                int number = 0;
                                Int32.TryParse(e.InputText, out number);
                                if (PlayerBooms + number <= 30)
                                {
                                    SendClientMessage($"Вы скрафтили {number} взрывных боеприпасов");
                                    PlayerBooms += number;
                                }
                                else
                                {
                                    boomsInput.Message = "Вы не можете иметь более 30 взрывных боеприпасов! ";
                                    boomsInput.Show(this);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                

                
            }
            if(e.NewKeys == Keys.Action)
            {
                if (this.IsInVehicle(this.Vehicle))
                {
                    if(this.Vehicle.Engine == true)
                    {
                        this.Vehicle.Engine = false;
                    }
                    else if(this.Vehicle.Engine == false)
                    {
                        this.Vehicle.Engine = true;
                    }
                }
            }
            if(e.NewKeys == Keys.SecondaryAttack)
            {
                TakeDrop();
            }
            if(e.NewKeys == Keys.No)
            {
                SelectTextDraw(SampSharp.GameMode.SAMP.Color.Yellow);
                
            }
            if(e.NewKeys == Keys.Yes)
            {
                ShowInventory();



                SelectTextDraw(SampSharp.GameMode.SAMP.Color.Yellow);
            }
            
            if (e.NewKeys == Keys.AnalogLeft)
            {
                var mMenu = new ListDialog("Меню", "принять", "отмена");
                
                mMenu.AddItem("{FF0000}Здоровье {FFFFFF}+ броня");
                mMenu.AddItem("{00FFFF}Оружие");
                mMenu.AddItem("{FFAA00}Взрывные боеприпасы");
                mMenu.AddItem("{662487}Одежда");
                mMenu.Response += mMenuDialog_Response;
                mMenu.Show(this);
                void mMenuDialog_Response(object Sender, DialogResponseEventArgs e)
                {
                     if (e.ListItem == 0)
                    {
                        this.Health = 100;
                        this.Armour = 100;
                    }
                    if (e.ListItem == 1)
                    {
                        var weaponMenu = new ListDialog("Оружие", "принять", "отмена");
                        weaponMenu.AddItem("Deagle");
                        weaponMenu.AddItem("ShotGun");
                        weaponMenu.AddItem("Sawedoff");
                        weaponMenu.AddItem("SPAS-12");
                        weaponMenu.AddItem("M4");
                        weaponMenu.AddItem("Sniper");
                        weaponMenu.AddItem("Огнетушитель");
                        weaponMenu.Response += weaponMenuDialog_Response;
                        weaponMenu.Show(this);
                        void weaponMenuDialog_Response(object Sender, DialogResponseEventArgs e)
                        {
                            if (e.ListItem == 0)
                            {
                                this.GiveWeapon(Weapon.Deagle, 100);
                            }
                            if (e.ListItem == 1)
                            {
                                this.GiveWeapon(Weapon.Shotgun, 100);
                            }
                            if (e.ListItem == 2)
                            {
                                this.GiveWeapon(Weapon.Sawedoff, 100);
                            }
                            if (e.ListItem == 3)
                            {
                                this.GiveWeapon(Weapon.CombatShotgun, 100);
                            }
                            if (e.ListItem == 4)
                            {
                                this.GiveWeapon(Weapon.M4, 200);
                            }
                            if (e.ListItem == 5)
                            {
                                this.GiveWeapon(Weapon.Sniper, 30);
                            }
                            if (e.ListItem == 6)
                            {
                                this.GiveWeapon(Weapon.FireExtinguisher, 200000);
                            }
                        }
                    }
                    if (e.ListItem == 2)
                    {
                        var boomsInput = new InputDialog("Взрывные боеприпасы", "Введите количество взрывных боеприпасов:", false, "принять", "отклонить");
                        boomsInput.Response += boomsInputDialog_Response;
                        boomsInput.Show(this);
                        void boomsInputDialog_Response(object Sender, DialogResponseEventArgs e)
                        {
                            
                                int number = 0;
                                Int32.TryParse(e.InputText, out number);
                                if (PlayerBooms + number <= 30)
                                {
                                    PlayerBooms += number;
                                }
                                else
                                {
                                    boomsInput.Message = "Вы не можете иметь более 30 взрывных боеприпасов! ";
                                    boomsInput.Show(this);
                                }
                            
                        }
                    }
                    if (e.ListItem == 3)
                    {
                        var skinInput = new InputDialog("Одежда", "Введите номер одежды:", false, "принять", "отклонить");
                        skinInput.Response += skinInputDialog_Response;
                        skinInput.Show(this);
                        void skinInputDialog_Response(object Sender, DialogResponseEventArgs e)
                        {
                            Int32.TryParse(e.InputText, out int number);
                            if (number > 0 && number <= 311)
                            {
                                this.Skin = number;
                            }
                        }
                    }
                }

            }
        }
        public override void GetKeys(out Keys keys, out int updown, out int leftright)
        {
            base.GetKeys(out keys, out updown, out leftright);
            
            
        }
        
        public override void OnDeath(DeathEventArgs e)
        {
            this.waterNumber = 100;
            this.FoodNumber = 100;
            var updateToHudnreedFood = new MySqlCommand($"UPDATE `Players` SET `Food` = '{this.FoodNumber}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
            updateToHudnreedFood.ExecuteNonQuery();
            var updateToHudnreedWater = new MySqlCommand($"UPDATE `Players` SET `Water` = '{this.waterNumber}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
            updateToHudnreedWater.ExecuteNonQuery();

            SendDeathMessageToAll(e.Killer, this, e.Killer.Weapon);
            
            
            base.OnDeath(e);
            
            
            
            //var updateVod = new MySqlCommand($"SELECT `Water` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
            //
            //
            //ber = Convert.ToInt32(updateVod.ExecuteScalar());
            
            
            
            
        }

        [Command("Skin")]
        private void ChangeSkin(int skin)
        {
            Skin = skin;
        }
        [Command("Delveh")]
        private void DeleteCar()
        {
            if (this.IsInVehicle(this.Vehicle) == true)
            {
                SendClientMessage("Вы успешно удалили транспорт");
                this.Vehicle.Dispose();
                
            }
        }
        
        
        
             
            
        
        private void WaterDeath()
        {
            var VodaDeathTimer = new System.Timers.Timer(2000);
            VodaDeathTimer.Elapsed += VodaDeathTimerEvent;
            VodaDeathTimer.Enabled = true;
            VodaDeathTimer.AutoReset = true;
            void VodaDeathTimerEvent(object source, ElapsedEventArgs e)
            {
                if (this.IsConnected)
                {
                    if (this.waterNumber < 0)
                    {
                        this.Health -= 5;
                    }
                    if(this.eatNumber < 0)
                    {
                        this.Health -= 5;
                    }
                }
                else
                {
                    VodaDeathTimer.Dispose();
                }
            }
        }
        public void OnUseInventorySlot(int i)
        {
            bool isUsed = false;   
                if (slotsinfo[i] == 1)
                {
                    ApplyAnimation("BAR", "DNK_STNDM_LOOP", 1, false, false, false, true, 2000);
                    if (waterNumber <= 80)
                    {
                        waterNumber += 20;
                    }
                    else
                    {
                        waterNumber = 100;
                    }
                isUsed = true;
                }
                if (slotsinfo[i] == 2)
                {
                    ApplyAnimation("VENDING", "VEND_EAT1_P", 1, false, false, false, true, 2000);
                    if (FoodNumber <= 90)
                    {
                        FoodNumber += 10;
                    }
                    else
                    {
                        FoodNumber = 100;
                    }
                isUsed = true;
            }
                if (slotsinfo[i] == 3)
                {
                    ApplyAnimation("BAR", "DNK_STNDM_LOOP", 1, false, false, false, true, 2000);
                    if (waterNumber <= 80)
                    {
                        waterNumber += 20;
                    }
                    else
                    {
                        waterNumber = 100;
                    }
                isUsed = true;
            }
                if (slotsinfo[i] == 4)
                {
                ApplyAnimation("VENDING", "VEND_EAT1_P", 1 , false, false, false, true, 2000);
                
                if (FoodNumber <= 90)
                {
                    FoodNumber += 10;
                }
                else
                {
                    FoodNumber = 100;
                }
                isUsed = true;
            }
                if (slotsinfo[i] == 5)
                {
                this.PlaySound(42803);
                    SendClientMessage("Вы залечили рану аптечкой");
                    this.Health = 100;
                isUsed = true;
            }
            if (slotsinfo[i] == 6)
            {
                if (weaponSlotsInfo[0] == 0)
                {
                    weaponSlotsInfo[0] = 6;
                    weaponslots[0].PreviewModel = lootItems.Find(m => m.Id == 6).modelId;
                    weaponslots[0].Show();
                    isUsed = true;
                }
            }
            if (slotsinfo[i] == 7)
            {
                if (weaponSlotsInfo[0] == 6)
                {
                    this.PlaySound(36401);
                    SendClientMessage("Вы зарядили обойму в M4");
                    this.GiveWeapon(Weapon.M4, 50);
                    weaponslotsAmmoNum[0] += 50;
                    isUsed = true;

                }
            }
            if (slotsinfo[i] == 8)
            {
                if (weaponSlotsInfo[1] == 0)
                {
                    weaponSlotsInfo[1] = 8;
                    weaponslots[1].PreviewModel = lootItems.Find(m => m.Id == 8).modelId;
                    weaponslots[1].Show();
                    isUsed = true;
                }
            }
            if (slotsinfo[i] == 9)
            {
                if (weaponSlotsInfo[1] == 8)
                {
                    this.PlaySound(36401);
                    SendClientMessage("Вы зарядили обойму в Desert eagle");
                    this.GiveWeapon(Weapon.Deagle, 7);
                    weaponslotsAmmoNum[1] += 7;
                    isUsed = true;

                }
            }
            if (slotsinfo[i] == 10)
            {
                if (weaponSlotsInfo[2] == 0)
                {
                    weaponSlotsInfo[2] = 10;
                    weaponslots[2].PreviewModel = lootItems.Find(m => m.Id == 10).modelId;
                    weaponslots[2].Show();
                    isUsed = true;
                }
            }
            if (slotsinfo[i] == 11)
            {
                if (weaponSlotsInfo[2] == 10)
                {
                    this.PlaySound(36401);
                    SendClientMessage("Вы зарядили обойму в spas-12");
                    this.GiveWeapon(Weapon.CombatShotgun, 7);
                    weaponslotsAmmoNum[2] += 7;
                    isUsed = true;
                }
            }
            if (slotsinfo[i] == 12)
            {
                if (weaponSlotsInfo[2] == 0)
                {
                    weaponSlotsInfo[2] = 12;
                    weaponslots[2].PreviewModel = lootItems.Find(m => m.Id == 12).modelId;
                    weaponslots[2].Show();
                    isUsed = true;
                }
            }
            if (slotsinfo[i] == 13)
            {
                if (weaponSlotsInfo[2] == 12)
                {
                    this.PlaySound(36401);
                    SendClientMessage("Вы зарядили обойму в sawed-off");
                    this.GiveWeapon(Weapon.Sawedoff, 2);
                    weaponslotsAmmoNum[2] += 2;
                    isUsed = true;

                }
            }
            if (slotsinfo[i] == 14)
            {
                if (weaponSlotsInfo[2] == 0)
                {
                    weaponSlotsInfo[2] = 14;
                    weaponslots[2].PreviewModel = lootItems.Find(m => m.Id == 14).modelId;
                    weaponslots[2].Show();
                    isUsed = true;
                }
            }
            if (slotsinfo[i] == 15)
            {
                if (weaponSlotsInfo[2] == 14)
                {
                    this.PlaySound(36401);
                    SendClientMessage("Вы зарядили обойму в shotgun");
                    this.GiveWeapon(Weapon.Shotgun, 8);
                    weaponslotsAmmoNum[2] += 8;
                    isUsed = true;

                }
            }
            if (slotsinfo[i] == 16)
            {
                if(ArmorSlot == 0)
                {
                    ArmorSlot = lootItems.Find(m => m.Id == 16).Id;
                    this.PlaySound(20802);
                    SendClientMessage("Вы надели легкий бронежилет");
                    this.Armour = 100;
                    isUsed = true;
                }
            }
            if (slotsinfo[i] == 17)
            {
                if (this.InAnyVehicle)
                {
                    this.Vehicle.Health += 1000;
                    SendClientMessage("Вы отремонтировали свой транспорт");
                    isUsed = true;
                }
            }
            if (isUsed == true)
            {
                var updateMoney = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '0' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                updateMoney.ExecuteNonQuery();
                CloseInventory();
                this.CancelSelectTextDraw();
                slotsinfo[0] = 0;
            }
            

        }
        public void OnUseWeaponSlot(int j)
        {
            bool isUsed = false;
            //M4
            if (weaponSlotsInfo[j] == 6 && weaponslotsAmmoNum[j] >= 50)
            {
               
                for (int i = 0; i < slotsinfo.Length; i++)
                {
                    if (slotsinfo[i] == 0)
                    {


                        isUsed = true;
                        slotsinfo[i] = lootItems.Find(n => n.Id == 7).Id;
                        var updateSlot = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '{slotsinfo[i]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateSlot.ExecuteNonQuery();
                        this.ClearAnimations(true);
                        weaponslotsAmmoNum[j] -= 50;
                        this.GiveWeapon(Weapon.M4,-50);
                        break;
                    }
                }
                if(isUsed == false)
                {
                    SendClientMessage("Рюкзак заполнен!");
                }
            }
            else if (weaponSlotsInfo[j] == 6)
            {
                for (int i = 0; i < slotsinfo.Length; i++)
                {
                    if (slotsinfo[i] == 0)
                    {

                        weaponSlotsInfo[j] = 0;
                        isUsed = true;
                        slotsinfo[i] = lootItems.Find(n => n.Id == 6).Id;
                        var updateweaponSlot = new MySqlCommand($"UPDATE `Players` SET `weaponslotsAmmo{j}` = '0' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateweaponSlot.ExecuteNonQuery();
                        var updateSlot = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '{slotsinfo[i]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateSlot.ExecuteNonQuery();
                        this.ClearAnimations(true);
                        TakeGun(j);
                        break;
                        
                    }

                }
                if (isUsed == false)
                {
                    SendClientMessage("Рюкзак заполнен!");
                }
            }
            //DEAGLE
            if (weaponSlotsInfo[j] == 8 && weaponslotsAmmoNum[j] >= 7)
            {
                for (int i = 0; i < slotsinfo.Length; i++)
                {
                    if (slotsinfo[i] == 0)
                    {


                        isUsed = true;
                        slotsinfo[i] = lootItems.Find(n => n.Id == 9).Id;
                        var updateSlot = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '{slotsinfo[i]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateSlot.ExecuteNonQuery();
                        this.ClearAnimations(true);
                        weaponslotsAmmoNum[j] -= 7;
                        this.GiveWeapon(Weapon.Deagle, -7);
                        break;
                    }
                }
                if(isUsed == false)
                {
                    SendClientMessage("Рюкзак заполнен!");
                }

            }
            else if (weaponSlotsInfo[j] == 8)
            {
                for (int i = 0; i < slotsinfo.Length; i++)
                {
                    if (slotsinfo[i] == 0)
                    {

                        weaponSlotsInfo[j] = 0;
                        isUsed = true;
                        slotsinfo[i] = lootItems.Find(n => n.Id == 8).Id;
                        var updateweaponSlot = new MySqlCommand($"UPDATE `Players` SET `weaponslotsAmmo{j}` = '0' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateweaponSlot.ExecuteNonQuery();
                        var updateSlot = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '{slotsinfo[i]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateSlot.ExecuteNonQuery();
                        this.ClearAnimations(true);
                        TakeGun(j);
                        break;
                    }

                }
                if (isUsed == false)
                {
                    SendClientMessage("Рюкзак заполнен!");
                }
            }
            if (weaponSlotsInfo[j] == 10 && weaponslotsAmmoNum[j] >= 7)
            {
                for (int i = 0; i < slotsinfo.Length; i++)
                {
                    if (slotsinfo[i] == 0)
                    {


                        isUsed = true;
                        slotsinfo[i] = lootItems.Find(n => n.Id == 11).Id;
                        var updateSlot = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '{slotsinfo[i]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateSlot.ExecuteNonQuery();
                        this.ClearAnimations(true);
                        weaponslotsAmmoNum[j] -= 7;
                        this.GiveWeapon(Weapon.CombatShotgun, -7);
                        break;
                    }
                }
                if (isUsed == false)
                {
                    SendClientMessage("Рюкзак заполнен!");
                }
            }
            else if (weaponSlotsInfo[j] == 10)
            {
                for (int i = 0; i < slotsinfo.Length; i++)
                {
                    if (slotsinfo[i] == 0)
                    {

                        weaponSlotsInfo[j] = 0;
                        isUsed = true;
                        slotsinfo[i] = lootItems.Find(n => n.Id == 10).Id;
                        var updateweaponSlot = new MySqlCommand($"UPDATE `Players` SET `weaponslotsAmmo{j}` = '0' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateweaponSlot.ExecuteNonQuery();
                        var updateSlot = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '{slotsinfo[i]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateSlot.ExecuteNonQuery();
                        this.ClearAnimations(true);
                        TakeGun(j);
                        break;
                    }

                }
                if (isUsed == false)
                {
                    SendClientMessage("Рюкзак заполнен!");
                }
            }
            if (weaponSlotsInfo[j] == 12 && weaponslotsAmmoNum[j] >= 2)
            {
                for (int i = 0; i < slotsinfo.Length; i++)
                {
                    if (slotsinfo[i] == 0)
                    {


                        isUsed = true;
                        slotsinfo[i] = lootItems.Find(n => n.Id == 13).Id;
                        var updateSlot = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '{slotsinfo[i]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateSlot.ExecuteNonQuery();
                        this.ClearAnimations(true);
                        weaponslotsAmmoNum[j] -= 2;
                        this.GiveWeapon(Weapon.Sawedoff, -2);
                        break;
                    }
                }
                if (isUsed == false)
                {
                    SendClientMessage("Рюкзак заполнен!");
                }
            }
            else if (weaponSlotsInfo[j] == 12)
            {
                for (int i = 0; i < slotsinfo.Length; i++)
                {
                    if (slotsinfo[i] == 0)
                    {

                        weaponSlotsInfo[j] = 0;
                        isUsed = true;
                        slotsinfo[i] = lootItems.Find(n => n.Id == 12).Id;
                        var updateweaponSlot = new MySqlCommand($"UPDATE `Players` SET `weaponslotsAmmo{j}` = '0' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateweaponSlot.ExecuteNonQuery();
                        var updateSlot = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '{slotsinfo[i]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateSlot.ExecuteNonQuery();
                        this.ClearAnimations(true);
                        TakeGun(j);
                        break;
                    }

                }
                if (isUsed == false)
                {
                    SendClientMessage("Рюкзак заполнен!");
                }
            }
            if (weaponSlotsInfo[j] == 14 && weaponslotsAmmoNum[j] >= 8)
            {
                for (int i = 0; i < slotsinfo.Length; i++)
                {
                    if (slotsinfo[i] == 0)
                    {


                        isUsed = true;
                        slotsinfo[i] = lootItems.Find(n => n.Id == 15).Id;
                        var updateSlot = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '{slotsinfo[i]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateSlot.ExecuteNonQuery();
                        this.ClearAnimations(true);
                        weaponslotsAmmoNum[j] -= 8;
                        this.GiveWeapon(Weapon.Shotgun, -8);
                        break;
                    }
                }
                if (isUsed == false)
                {
                    SendClientMessage("Рюкзак заполнен!");
                }
            }
            else if (weaponSlotsInfo[j] == 14)
            {
                for (int i = 0; i < slotsinfo.Length; i++)
                {
                    if (slotsinfo[i] == 0)
                    {

                        weaponSlotsInfo[j] = 0;
                        isUsed = true;
                        slotsinfo[i] = lootItems.Find(n => n.Id == 14).Id;
                        var updateweaponSlot = new MySqlCommand($"UPDATE `Players` SET `weaponslotsAmmo{j}` = '0' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateweaponSlot.ExecuteNonQuery();
                        var updateSlot = new MySqlCommand($"UPDATE `Players` SET `slot{i}` = '{slotsinfo[i]}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                        updateSlot.ExecuteNonQuery();
                        this.ClearAnimations(true);
                        TakeGun(j);
                        break;
                    }

                }
                if (isUsed == false)
                {
                    SendClientMessage("Рюкзак заполнен!");
                }
            }
        }
        
        public void LootInfoShow(int i)
        {

            string name = "";
            string description = "";
            string botsellPrice = "";
            name = lootItems.Find(item => item.Id == slotsinfo[i]).Name;
            description = lootItems.Find(item => item.Id == slotsinfo[i]).Description;
            description = lootItems.Find(item => item.Id == slotsinfo[i]).Description;
            botsellPrice = lootItems.Find(item => item.Id == slotsinfo[i]).BotSellPrice.ToString();
            if (slotsinfo[i] != 0)
            {
                var lootinfo = new MessageDialog($"{name}", $"{{FFFFFF}}{description}\n\n{{e61e1e}}Продажа NPC:{{0e630f}} {botsellPrice}", "OK");
                lootinfo.Show(this);
            }
        }
        public void GiveWeaponOnConnect()
        {
            if (weaponSlotsInfo[0] == lootItems.Find(m => m.Id == 6).Id && weaponslotsAmmoNum[0] > 0)
            {
                this.GiveWeapon(Weapon.M4, weaponslotsAmmoNum[0]);
            }
            if (weaponSlotsInfo[1] == lootItems.Find(m => m.Id == 8).Id && weaponslotsAmmoNum[1] > 0)
            {
                this.GiveWeapon(Weapon.Deagle, weaponslotsAmmoNum[1]);
            }
            if (weaponSlotsInfo[2] == lootItems.Find(m => m.Id == 10).Id && weaponslotsAmmoNum[2] > 0)
            {
                this.GiveWeapon(Weapon.CombatShotgun, weaponslotsAmmoNum[2]);
            }
            if (weaponSlotsInfo[2] == lootItems.Find(m => m.Id == 12).Id && weaponslotsAmmoNum[2] > 0)
            {
                this.GiveWeapon(Weapon.Sawedoff, weaponslotsAmmoNum[2]);
            }
            if (weaponSlotsInfo[2] == lootItems.Find(m => m.Id == 14).Id && weaponslotsAmmoNum[2] > 0)
            {
                this.GiveWeapon(Weapon.Shotgun, weaponslotsAmmoNum[2]);
            }
        }
        [Command("lootget")]
        public void updateLootIn24(int num1)
        {
            

            //-688.274, 928.10254, 13.629317
            //Random rnd1 = new Random();
            //int num1 = rnd1.Next(0, 9);
            if(num1 == 0)
            {

            }
            else 
            {
                GameMode.serverLoots.Add(new Loot(lootItems.Find(m => m.Id == num1), this.Position, 0));
            }
            
        }
        void TakeGun(int i)
        {
            this.ResetWeapons();
            for(int j = 0; j < weaponSlotsInfo.Length; j++)
            {
                if (weaponSlotsInfo[j] != weaponSlotsInfo[i])
                {
                    if (weaponSlotsInfo[j] == lootItems.Find(m => m.Id == 6).Id) 
                    {
                        this.GiveWeapon(Weapon.M4, this.weaponslotsAmmoNum[j]);
                    }
                    if (weaponSlotsInfo[j] == lootItems.Find(m => m.Id == 8).Id)
                    {
                        this.GiveWeapon(Weapon.Deagle, this.weaponslotsAmmoNum[j]);
                    }
                    if (weaponSlotsInfo[j] == lootItems.Find(m => m.Id == 10).Id)
                    {
                        this.GiveWeapon(Weapon.CombatShotgun, this.weaponslotsAmmoNum[j]);
                    }
                    if (weaponSlotsInfo[j] == lootItems.Find(m => m.Id == 12).Id)
                    {
                        this.GiveWeapon(Weapon.Sawedoff, this.weaponslotsAmmoNum[j]);
                    }
                    if (weaponSlotsInfo[j] == lootItems.Find(m => m.Id == 14).Id)
                    {
                        this.GiveWeapon(Weapon.Shotgun, this.weaponslotsAmmoNum[j]);
                    }
                }
            }
        }
        void TakeDrop()
        {
            var dropDialog = new ListDialog("Предмет","Взять", "Отмена");
            Dictionary<int, Loot> dialogDic = new Dictionary<int, Loot>();
            int index = 0;
            foreach (Loot l in GameMode.serverLoots)
            {
                if (IsInRangeOfPoint(5, l.position))
                {
                    dialogDic.Add(index, l);
                    dropDialog.AddItem(l.Name);
                    index++;
                }
            }
            dropDialog.Response += dropDialog_Response;
            dropDialog.Show(this);
            void dropDialog_Response(Object sender,DialogResponseEventArgs e) 
            {
                 if (e.DialogButton != DialogButton.Right)
                {
                    this.ApplyAnimation("BOMBER", "BOM_PLANT_CROUCH_IN", 2, false, true, true, true, 2, true);
                    for (int i = 0; i<= dropDialog.Items.Count; i++)
                    {
                        if(e.ListItem == i)
                        {
                            for (int j = 0; j < slotsinfo.Length; j++)
                            {
                                if (slotsinfo[j] == 0)
                                {

                                    if (dialogDic[i].isTaked == false)
                                    {
                                        SendClientMessage($"{{B1BA24}}Подобрано: {{177B3D}}{dialogDic[i].Name}");
                                        slotsinfo[j] = dialogDic[i].Id;
                                        var updateSlot = new MySqlCommand($"UPDATE `Players` SET `slot{j}` = '{dialogDic[i].Id}' WHERE `Players`.`NickName` = '{Name}'", sqlCon);
                                        updateSlot.ExecuteNonQuery();
                                        GameMode.serverLoots.Find(item => item.Idd == dialogDic[i].Idd).isTaked = true;
                                        GameMode.serverLoots.Find(item => item.Idd == dialogDic[i].Idd).DeleteLoot();

                                        GameMode.serverLoots.Remove(dialogDic[i]);
                                        dialogDic.Clear();
                                        this.ClearAnimations(true);

                                        break;
                                    }
                                    else
                                    {
                                        SendClientMessage("{FF0000}Данный предмет уже подобрал другой человек.");
                                    }

                                }
                            }

                        }
                    }
                }
            }

        }
    }
}