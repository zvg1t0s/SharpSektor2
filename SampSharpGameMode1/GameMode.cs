using Microsoft.Identity.Client;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Encoders;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using System;
using System.Drawing;
using SampSharp.FCNPCs;
using SampSharp.FCNPCs.Definitions;
using static System.Math;
using SampSharp.GameMode.SAMP.Commands;
using System.Net.Mail;
using System.Threading;
using System.Timers;
using Org.BouncyCastle.Asn1.Gnu;
using SampSharp.Streamer;
using SampSharp.Streamer.World;
using System.Collections.Generic;

namespace SampSharpGameMode1
{
    public class GameMode : BaseMode
    {
        public static List<Loot> serverLoots = new List<Loot>();

        static TextDraw TimePNG = new TextDraw(new SampSharp.GameMode.Vector2(88.0f, 319.0f), "0");
        static int hours = 0;
        static int minutes = 0;
       

        private static FCNPC clone;
        private static FCNPC zombie1;
        private static FCNPC zombie2;
        private static FCNPC zombie3;
        private static FCNPC zombie4;
        private static FCNPC zombie5;
        private static FCNPC zombie6;
        private static FCNPC zombie7;
        private static FCNPC zombie8;
        private static FCNPC zombie9;
        private static FCNPC zombie10;
        private static FCNPC zombie11;
        private static FCNPC soldierCenterRight;
        public static DynamicArea ar;

        protected override void OnPlayerDisconnected(BasePlayer player, DisconnectEventArgs e)
        {
            base.OnPlayerDisconnected(player, e);
            player.Dispose();
        }
       
        MySqlConnection sqlCon = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=Samp");
        protected override void OnInitialized(EventArgs e)
        {
            
            LimitGlobalChatRadius(0);
            EnableStuntBonusForAll(false);
            DisableInteriorEnterExits();
            //
            TimePNG.Font = TextDrawFont.Pricedown;
            TimePNG.LetterSize = new SampSharp.GameMode.Vector2(0.279166f, 1.299998f);
            TimePNG.Outline = 2;
            TimePNG.Width = 405.5f;
            TimePNG.Height = 28.0f;
            TimePNG.Shadow = 0;
            TimePNG.Alignment = TextDrawAlignment.Center;
            TimePNG.ForeColor = 1433087999;
            TimePNG.BackColor = 255;
            TimePNG.BoxColor = 50;
            TimePNG.UseBox = true;
            TimePNG.Proportional = true;
            TimePNG.Selectable = false;
            //
            //PICKUPS DOWN
            var glavaPickUpExit = Pickup.Create(1318,1, new Vector3(1325.5162, 1570.7372, 3000.0054), 1001 );
            var glavaPickUpEnter = Pickup.Create(1318,1, new Vector3(-206.8012, 1119.1671, 20.429688), -1);
            TextLabel tlglava = new TextLabel("{f2e82c}Глава поселения", 0, new Vector3(-206.8012, 1119.1671, 20.429688), 15.0f, 0);
            var ShopPickUpEnter = Pickup.Create(1318,1, new Vector3(-176.74492, 1111.7572, 19.742188), -1);
            TextLabel tlshop = new TextLabel("{f2e82c}Магазин", 0, new Vector3(-176.74492, 1111.7572, 19.742188), 15.0f, 0);
            var ShopPickUpExit = Pickup.Create(1318,1,new Vector3(1333.2317, 1360.9196, 3001.1155), 1003);
            var BarPickUpEnter = Pickup.Create(1254,1, new Vector3(-181.0763, 1034.7943, 19.742188));
            TextLabel tlbar = new TextLabel("{f2e82c}Бар", 0, new Vector3(-181.0763, 1034.7943, 19.742188), 15.0f, 0);
            var BarPickUpExit = Pickup.Create(1318,1, new Vector3(501.8956, -68.29118, 998.7578));
            //{ "Zone 5", -414, 798.5, 258, 1290.5, 0xFF7F00FF }
            //var zone = DynamicArea.CreateRectangle(-414,798.5,258,1290.5,0,0);

            //PICKUPS UP
            UsePlayerPedAnimations();
            base.OnInitialized(e);
            
            //-175.88075, 1226.6819, 21.030312 216.54417
            var MACTEP = Actor.Create(6, new Vector3(-175.88075f,1226.6819f, 21.030312f), 216.54417f);
            MACTEP.IsInvulnerable = true;
            TextLabel tdmactep = new TextLabel("{00FFFF}Мастер {FF0000}F", 0, new Vector3(-175.88075f, 1226.6819f, 21.030312f), 15.0f, 0);
            
            var barmen = Actor.Create(171, new Vector3(497.05154, -77.56168, 998.7651), 0);
            TextLabel tdbar = new TextLabel("{1b6b2c}Бар {911313}F", 0, new Vector3(497.05862, -76.04029, 998.7578), 15.0f, 1002);
            barmen.VirtualWorld = 1002;
            barmen.IsInvulnerable = true;

            var banditsHead = Actor.Create(149, new Vector3(510.90982, -80.66605, 998.96094), 113.04387f);
            banditsHead.VirtualWorld = 1002;
            banditsHead.IsInvulnerable = true;
            TextLabel banditsHeadTL = new TextLabel("{5c2fa8}Смоук {4acfd4}F", 0, new Vector3(509.29916, -81.20589, 998.96094), 20.0f, 1002);

            var shopSeller = Actor.Create(241, new Vector3(1329.6319, 1355.4971, 3001.1155), 0f);
            shopSeller.VirtualWorld = 1003;
            banditsHead.IsInvulnerable = true;
            TextLabel shopSellerTL = new TextLabel("{55d13d}Магазин {ff0000}F", 0, new Vector3(1329.6433, 1357.181, 3001.1155), 20.0f, 1003);

            //-225.80734, 1069.6211, 19.742188, 358,54773

            var bomjValera = Actor.Create(78, new Vector3(-225.80734, 1069.6211, 19.742188), 10f);
            bomjValera.VirtualWorld = 0;
            bomjValera.IsInvulnerable = true;

            TextLabel bomjValeraTd = new TextLabel("{c74332}Даркел {ffffff}[{2491bf}F{ffffff}]", 0, new Vector3(-225.80734, 1069.6211, 19.742188), 20.0f, 0);

            // -225.72682, 1066.9615, 20.023155, 90

            var bomjValeraJ = Actor.Create(75, new Vector3(-225.72682, 1066.9615, 20.023155), 87f);
            bomjValeraJ.VirtualWorld = 0;
            bomjValeraJ.IsInvulnerable = true;
            bomjValeraJ.ApplyAnimation("CRACK", "CRCKIDLE4",1,false,false,false,true,-1);

            var glava = Actor.Create(295, new Vector3(1337.9585, 1582.9047, 3000.0054), 138f);
            glava.VirtualWorld = 1001;
            glava.IsInvulnerable = true;
            TextLabel glavaTL = new TextLabel("{55d13d}Глава поселения {ff0000}F", 0, new Vector3(1335.6156, 1580.1486, 3000.0054), 20.0f, 1001);

            var glavaOhrana1 = Actor.Create(164,new Vector3 (1335.9097, 1583.972, 3000.0054), 163f);
            glavaOhrana1.VirtualWorld = 1001;
            glavaOhrana1.IsInvulnerable = true;

            var glavaOhrana2 = Actor.Create(163, new Vector3(1324.1565, 1573.9795, 3000.0054), 271f);
            glavaOhrana2.VirtualWorld = 1001;
            glavaOhrana2.IsInvulnerable = true;

            var technicue = Actor.Create(289, new Vector3(-90.98175, 1162.773, 19.742188), 13.393677f);
            technicue.VirtualWorld = 0;
            technicue.IsInvulnerable = true;

            SpawnCars();
            //var soldierCenterRight = Actor.Create(286, new Vector3(-145.98532, 1129.9305, 35.72811), 325);
            //soldierCenterRight.VirtualWorld = 0;
            //soldierCenterRight.IsInvulnerable= true;
            
            //-196.06013, 1219.5857, 19.902187 165.61018
            var mecanic = Actor.Create(50, new Vector3(-196.06013f, 1219.5857f, 19.902187f), 165.61018f);
            TextLabel tdmecanic = new TextLabel("{00FFFF}Механик {FF0000}F", 0, new Vector3(-196.06013, 1219.5857, 19.902187), 20.0f, 0);
            mecanic.IsInvulnerable = true;
            Object();
            SpawnNpc();
            TimeUpdate();
            

            Console.WriteLine("\n----------------------------------");
            Console.WriteLine(" Blank game mode by your name here");
            Console.WriteLine("----------------------------------\n");
            Server.SetWeather(30);
            
            SetGameModeText(FCNPC.Version);
            AddPlayerClass(17, new Vector3(834.8044f, -2052.1753f, 12.8672f), 0f);



            // TODO: Put logic to initialize your game mode here
        }
        void SpawnCars()
        {
            // -205.78366, 1215.857, 20.244665, 180.461
            //-214.60028, 1215.2217, 20.250952 0
            var mechCar1 = BaseVehicle.Create(VehicleModelType.Sandking, new Vector3(-205.78366, 1215.857, 20.244665), 180, 1, 0, 1800, false);
            mechCar1.Doors = true;
            mechCar1.Engine = false;
            mechCar1.Health = 10000;

            var mechCar2 = BaseVehicle.Create(VehicleModelType.Sandking, new Vector3(-214.60028, 1215.2217, 20.250952), 0, 1, 0, 1800, false);
            mechCar2.Doors = true;
            mechCar2.Engine = false;
            mechCar2.Health = 10000;

        }
        void AntiDmSystem(BasePlayer p)
        {
            ar = DynamicArea.CreateRectangle(-414, 798.5f, 258, 1290.5f, 0, 0,p);
            
        }
        protected override void OnPlayerKeyStateChanged(BasePlayer player, KeyStateChangedEventArgs e)
        {
            base.OnPlayerKeyStateChanged(player, e);

            /**
            if (e.NewKeys == Keys.Fire || (e.NewKeys == Keys.Aim) || (e.OldKeys == Keys.Aim) && (e.NewKeys == Keys.SecondaryAttack))
            {
                if (ar.IsInArea(player, true))
                {
                    player.ToggleControllable(false);
                    var dmTimer = new System.Timers.Timer(2000);
                    dmTimer.Elapsed += OnTimedEvent;
                    dmTimer.Enabled = true;
                    dmTimer.AutoReset = false;
                    void OnTimedEvent(object source, ElapsedEventArgs e)
                    {
                        player.ToggleControllable(true);

                    }
                }
            }
            **/
        }
        protected override void OnPlayerPickUpPickup(BasePlayer player, PickUpPickupEventArgs e)
        {
            base.OnPlayerPickUpPickup(player, e);
            if(e.Pickup.Id == 0)
            {
                player.Interior = 0;
                player.VirtualWorld = 0;
                player.SetTime(hours, minutes);
                player.Position = new Vector3(-204.44403, 1122.2489, 19.742188);
                player.PutCameraBehindPlayer();
            }
            if (e.Pickup.Id == 1)
            {
                player.VirtualWorld = 1001;
                player.Interior = 1;
                player.Angle = 318.07553f;
                player.Position = new Vector3(1327.1235, 1572.3376, 3000.0054);
                player.PutCameraBehindPlayer();

            }
            if(e.Pickup.Id == 2)
            {
                player.VirtualWorld = 1003;
                player.Interior = 1;
                player.Angle = 132.94858f;
                player.Position = new Vector3(1331.1042, 1361.6592, 3001.1155);
                player.PutCameraBehindPlayer();

            }
            if (e.Pickup.Id == 3)
            {
                player.VirtualWorld = 0;
                player.Interior = 0;
                player.Angle = 177.61566f;
                player.Position = new Vector3(-178.5, 1108.9822, 19.742188);
                player.PutCameraBehindPlayer();

            }
            if (e.Pickup.Id == 4)
            {
                player.VirtualWorld = 1002;
                player.Interior = 11;
                player.Angle = 176.73378f;
                player.Position = new Vector3(501.7085, -70.99589, 998.7578);
                player.PutCameraBehindPlayer();

            }
            if (e.Pickup.Id == 5)
            {
                player.VirtualWorld = 0;
                player.Interior = 0;
                player.Angle = 92.65823f;
                player.Position = new Vector3(-181.37987, 1037.9471, 19.742188);
                player.PutCameraBehindPlayer();

            }
        }
        private static void SpawnNpc()
        {
            soldierCenterRight = FCNPC.Create("soldierCenterRight");
            soldierCenterRight.Spawn(286, new Vector3(-145.98532, 1129.9305, 35.72811));
            soldierCenterRight.Angle = 325f;
            soldierCenterRight.Weapon = Weapon.M4;
            soldierCenterRight.Ammo = 100;
            soldierCenterRight.Health = 10000;
            

            zombie1 = FCNPC.Create("Zombie_1");
            var zombiepos1 = new SampSharp.GameMode.Vector3(338.84, 1385.00, 7.10); 
            zombie1.Spawn(135, zombiepos1);
            zombie1.ApplyAnimation("GANGS", "prtial_gngtlkC", time: -1);
            zombie2 = FCNPC.Create("Zombie_2");
            //-183.63 1097.28 19.59 278.86
            var zombiepos2 = new SampSharp.GameMode.Vector3(-293.93576, 837.3982, 12.3200245);
            zombie2.Spawn(135, zombiepos2);

            zombie3 = FCNPC.Create("Zombie_3");
            //-197.97 980.73 19.03 179.53
            var zombiepos3 = new SampSharp.GameMode.Vector3(-293.93576, 827.3982, 12.3200245);
            zombie3.Spawn(135, zombiepos3);

            zombie4 = FCNPC.Create("Zombie_4");
            //-307.76 1067.02 19.59 15.20
            var zombiepos4 = new SampSharp.GameMode.Vector3(-307.76, 1067.02, 15.20);
            zombie4.Spawn(135, zombiepos4);

            zombie5 = FCNPC.Create("Zombie_5");
            //-807.63 1566.54 26.96 212.37
            var zombiepos5 = new SampSharp.GameMode.Vector3(-807.63, 1566.54, 26.96);
            zombie5.Spawn(135, zombiepos5);

            zombie6 = FCNPC.Create("Zombie_6");
            //-184.82 2498.63 24.33 207.55
            var zombiepos6 = new SampSharp.GameMode.Vector3(-184.82, 2498.63, 24.33);
            zombie6.Spawn(135, zombiepos6);

            zombie7 = FCNPC.Create("Zombie_7");
            //-166.52 2478.64 21.14 212.81
            var zombiepos7 = new SampSharp.GameMode.Vector3(-166.52, 2478.64, 21.14);
            zombie7.Spawn(135, zombiepos7);

            zombie8 = FCNPC.Create("Zombie_8");
            //-174.10 55.00 3.11 253.04
            var zombiepos8 = new SampSharp.GameMode.Vector3(-174.10, 55.00, 3.11);
            zombie8.Spawn(135, zombiepos8);

            zombie9 = FCNPC.Create("Zombie_9");
            //934.19 -1404.86 13.29 266.33
            var zombiepos9 = new SampSharp.GameMode.Vector3(934.19, - 1404.86, 13.29);
            zombie9.Spawn(135, zombiepos9);

            zombie10 = FCNPC.Create("Zombie_10");
            //1256.18 -1327.12 12.94 286.00
            var zombiepos10 = new SampSharp.GameMode.Vector3(1256.18, - 1327.12 ,12.94);
            zombie10.Spawn(135, zombiepos10);

            zombie11 = FCNPC.Create("Zombie_11");
            //1528.98 -1685.54 13.38 187.01
            var zombiepos11 = new SampSharp.GameMode.Vector3(1528.98, - 1685.54, 13.38);
            zombie11.Spawn(135, zombiepos11);
        }
        
        private static void AttackCurrentPlayer(BasePlayer player)
        {
            if (player.IsInRangeOfPoint(30.0f, zombie1.Position))
            {
                if (player.IsInRangeOfPoint(1.0f, zombie1.Position))
                {
                    zombie1.FightStyle = FightStyle.Boxing;
                    zombie1.MeleeAttack(useFightStyle: true);

                    player.Health = player.Health - 0.0005f;
                    
                    
                }
                
                zombie1.SetAngleToPlayer(player);
                zombie1.GoTo(player, type: MoveType.Drive);
                zombie1.ApplyAnimation("GANGS", "prtial_gngtlkC", time: -1);




            }
            if (player.IsInRangeOfPoint(30.0f, zombie2.Position))
            {
                if (player.IsInRangeOfPoint(1.0f, zombie2.Position))
                {

                    zombie2.MeleeAttack();
                }
                zombie2.SetAngleToPlayer(player);
                zombie2.GoTo(player, type: MoveType.Sprint);
                
                


            }
            if (player.IsInRangeOfPoint(30.0f, zombie3.Position))
            {
                if (player.IsInRangeOfPoint(1.0f, zombie3.Position))
                {

                    zombie3.MeleeAttack();
                }
                zombie3.SetAngleToPlayer(player);
                zombie3.GoTo(player, type: MoveType.Sprint);
                
                


            }
            if (player.IsInRangeOfPoint(30.0f, zombie4.Position))
            {
                if (player.IsInRangeOfPoint(1.0f, zombie4.Position))
                {

                    zombie4.MeleeAttack();
                }
                zombie4.SetAngleToPlayer(player);
                zombie4.GoTo(player, type: MoveType.Sprint);
               
                zombie4.MeleeAttack();


            }
            if (player.IsInRangeOfPoint(30.0f, zombie5.Position))
            {
                if (player.IsInRangeOfPoint(1.0f, zombie5.Position))
                {

                    zombie5.MeleeAttack();
                }
                zombie5.SetAngleToPlayer(player);
                zombie5.GoTo(player, type: MoveType.Sprint);
                
                


            }
            if (player.IsInRangeOfPoint(30.0f, zombie5.Position))
            {
                if (player.IsInRangeOfPoint(1.0f, zombie5.Position))
                {

                    zombie5.MeleeAttack();
                }
                zombie5.SetAngleToPlayer(player);
                zombie5.GoTo(player, type: MoveType.Sprint);
                


            }
            if (player.IsInRangeOfPoint(30.0f, zombie6.Position))
            {
                if (player.IsInRangeOfPoint(1.0f, zombie6.Position))
                {

                    zombie6.MeleeAttack();
                }
                zombie6.SetAngleToPlayer(player);
                zombie6.GoTo(player, type: MoveType.Sprint);
                


            }
            if (player.IsInRangeOfPoint(30.0f, zombie7.Position))
            {
                if (player.IsInRangeOfPoint(1.0f, zombie7.Position))
                {

                    zombie7.MeleeAttack();
                }
                
                zombie7.SetAngleToPlayer(player);
                zombie7.GoTo(player, type: MoveType.Sprint);
                


            }
            if (player.IsInRangeOfPoint(30.0f, zombie8.Position))
            {
                if (player.IsInRangeOfPoint(1.0f, zombie8.Position))
                {
                    zombie8.MeleeAttack();
                }
                zombie8.SetAngleToPlayer(player);
                zombie8.GoTo(player, type: MoveType.Sprint);
                


            }
            if (player.IsInRangeOfPoint(30.0f, zombie9.Position))
            {
                if (player.IsInRangeOfPoint(1.0f, zombie9.Position))
                {
                    zombie9.MeleeAttack();
                }
                zombie9.SetAngleToPlayer(player);
                zombie9.GoTo(player, type: MoveType.Sprint);
                


            }
            if (player.IsInRangeOfPoint(30.0f, zombie10.Position))
            {
                if (player.IsInRangeOfPoint(1.0f, zombie10.Position))
                {
                    zombie10.MeleeAttack();
                    
                }
                zombie10.SetAngleToPlayer(player);
                zombie10.GoTo(player, type: MoveType.Sprint);
                


            }
            if (player.IsInRangeOfPoint(30.0f, zombie11.Position))
            {
                if (player.IsInRangeOfPoint(1.0f, zombie11.Position))
                {

                    zombie11.MeleeAttack();
                }
                zombie11.SetAngleToPlayer(player);
                zombie11.GoTo(player, type: MoveType.Sprint);
                


            }
        }
        protected override void OnPlayerConnected(BasePlayer player, EventArgs e)
        {
            AntiDmSystem(player);
            base.OnPlayerConnected(player, e);
            //TERR REMOVE
            GlobalObject.Remove(player, 3425, new Vector3(-466.429688, 2190.273438, 55.992199), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16777, -105.359, 1212.069, 18.734, 0.250);
            GlobalObject.Remove(player, 16777, new Vector3(-105.359, 1212.069, 18.734), 0.25f);

            //RemoveBuildingForPlayer(playerid, 1352, -109.944, 1188.020, 18.710, 0.250);
            GlobalObject.Remove(player, 16777, new Vector3(-105.359, 1212.069, 18.734), 0.25f);

            //RemoveBuildingForPlayer(playerid, 1352, -126.101, 1192.920, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-126.101, 1192.920, 18.710), 0.25f);

            //RemoveBuildingForPlayer(playerid, 1352, -180.108, 1203.900, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-180.108, 1203.900, 18.710), 0.25f);
            // RemoveBuildingForPlayer(playerid, 1352, -206.070, 1192.920, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-206.070, 1192.920, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1352, -201.195, 1208.800, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-201.195, 1208.800, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1352, -201.046, 1108.900, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-201.046, 1108.900, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1352, -205.921, 1093.020, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-205.921, 1093.020, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1352, -180.266, 1103.910, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-180.266, 1103.910, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1352, -185.179, 1087.839, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-185.179, 1087.839, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1352, -264.906, 1103.989, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-264.906, 1103.989, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1352, -281.070, 1108.900, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-281.070, 1108.900, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1352, -285.937, 1093.020, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-285.937, 1093.020, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1352, -269.781, 1088.109, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-269.781, 1088.109, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 652, 71.179, 1182.170, 16.265, 0.250);
            GlobalObject.Remove(player, 652, new Vector3(71.179, 1182.170, 16.265), 0.25f);
            //RemoveBuildingForPlayer(playerid, 652, 59.039, 1181.680, 16.265, 0.250);
            GlobalObject.Remove(player, 652, new Vector3(59.039, 1181.680, 16.265), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1413, 72.085, 1206.880, 18.992, 0.250);
            GlobalObject.Remove(player, 1413, new Vector3(72.085, 1206.880, 18.992), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1413, 77.359, 1206.880, 18.992, 0.250);
            GlobalObject.Remove(player, 1413, new Vector3(77.359, 1206.880, 18.992), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1413, 66.796, 1207.160, 18.992, 0.250);
            GlobalObject.Remove(player, 1413, new Vector3(66.796, 1207.160, 18.992), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1413, 73.093, 1228.040, 19.078, 0.250);
            GlobalObject.Remove(player, 1413, new Vector3(73.093, 1228.040, 19.078), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1413, 78.257, 1226.849, 19.367, 0.250);
            GlobalObject.Remove(player, 1413, new Vector3(78.257, 1226.849, 19.367), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1413, 83.281, 1225.270, 19.820, 0.250);
            GlobalObject.Remove(player, 1413, new Vector3(83.281, 1225.270, 19.820), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, 49.140, 1193.640, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(49.140, 1193.640, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, 49.140, 1202.880, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(49.140, 1202.880, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, 20.914, 1193.640, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(20.914, 1193.640, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, 20.914, 1202.880, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(20.914, 1202.880, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -11.765, 1202.880, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-11.765, 1202.880, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -0.148, 1193.640, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-0.148, 1193.640, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -25.781, 1193.640, 22.812, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-25.781, 1193.640, 22.812), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -37.976, 1202.880, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-37.976, 1202.880, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -65.898, 1202.880, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-65.898, 1202.880, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -81.718, 1193.640, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-81.718, 1193.640, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -96.078, 1202.880, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-96.078, 1202.880, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1352, -105.069, 1203.900, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-105.069, 1203.900, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1352, -121.234, 1208.800, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-121.234, 1208.800, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -120.491, 1183.349, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-120.491, 1183.349, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -110.780, 1158.739, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-110.780, 1158.739, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -220.039, 1202.880, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-220.039, 1202.880, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -220.039, 1193.640, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-220.039, 1193.640, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -250.218, 1202.880, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-250.218, 1202.880, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -250.218, 1193.640, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-250.218, 1193.640, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1294, -280.226, 1184.750, 23.203, 0.250);
            GlobalObject.Remove(player, 1294, new Vector3(-280.226, 1184.750, 23.203), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16781, -144.054, 1227.300, 18.898, 0.250);
            GlobalObject.Remove(player, 16781, new Vector3(-144.054, 1227.300, 18.898), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16506, -144.054, 1227.300, 18.898, 0.250);
            GlobalObject.Remove(player, 16506, new Vector3(-144.054, 1227.300, 18.898), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1345, -155.695, 1234.420, 19.476, 0.250);
            GlobalObject.Remove(player, 1345, new Vector3(-155.695, 1234.420, 19.476), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16776, -237.022, 2662.840, 62.609, 0.250);
            GlobalObject.Remove(player, 16776, new Vector3(-237.022, 2662.840, 62.609), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16066, -186.483, 1217.630, 20.562, 0.250);
            GlobalObject.Remove(player, 16066, new Vector3(-186.483, 1217.630, 20.562), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1308, -225.733, 1208.810, 17.367, 0.250);
            GlobalObject.Remove(player, 1308, new Vector3(-225.733, 1208.810, 17.367), 0.25f);
            //BAR REMOVE

            //RemoveBuildingForPlayer(playerid, 2681, 504.835, -81.171, 997.960, 0.250);
            GlobalObject.Remove(player, 2681, new Vector3(504.835, -81.171, 997.960), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2778, 504.820, -79.929, 997.960, 0.250);
            GlobalObject.Remove(player, 2778, new Vector3(504.820, -79.929, 997.960), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2964, 506.484, -84.835, 997.937, 0.250);
            GlobalObject.Remove(player, 2964, new Vector3(506.484, -84.835, 997.937), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2964, 510.101, -84.835, 997.937, 0.250);
            GlobalObject.Remove(player, 2964, new Vector3(510.101, -84.835, 997.937), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2232, 507.179, -88.390, 998.539, 0.250);
            GlobalObject.Remove(player, 2232, new Vector3(507.179, -88.390, 998.539), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2232, 510.523, -88.257, 998.539, 0.250);
            GlobalObject.Remove(player, 2232, new Vector3(510.523, -88.257, 998.539), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2691, 504.375, -81.328, 1000.409, 0.250);
            GlobalObject.Remove(player, 2691, new Vector3(504.375, -81.328, 1000.409), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2696, 512.390, -85.242, 999.734, 0.250);
            GlobalObject.Remove(player, 2696, new Vector3(512.390, -85.242, 999.734), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2659, 512.382, -86.664, 1000.849, 0.250);
            GlobalObject.Remove(player, 2659, new Vector3(512.382, -86.664, 1000.849), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2660, 509.851, -89.078, 1000.919, 0.250);
            GlobalObject.Remove(player, 2660, new Vector3(509.851, -89.078, 1000.919), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2662, 507.507, -89.070, 1000.460, 0.250);
            GlobalObject.Remove(player, 2662, new Vector3(507.507, -89.070, 1000.460), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2695, 505.359, -89.078, 1000.010, 0.250);
            GlobalObject.Remove(player, 2695, new Vector3(505.359, -89.078, 1000.010), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2657, 504.343, -88.031, 999.054, 0.250);
            GlobalObject.Remove(player, 2657, new Vector3(504.343, -88.031, 999.054), 0.25f);

            //RemoveBuildingForPlayer(playerid, 2670, 505.492, -81.296, 998.070, 0.250);
            GlobalObject.Remove(player, 2670, new Vector3(505.492, -81.296, 998.070), 0.25f);
            //fk 11 removes
            //RemoveBuildingForPlayer(playerid, 16735, -49.242, 1137.699, 28.781, 0.250);
            GlobalObject.Remove(player, 16735, new Vector3(-49.242, 1137.699, 28.781), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16736, 11.015, 959.882, 24.703, 0.250);
            GlobalObject.Remove(player, 16736, new Vector3(11.015, 959.882, 24.703), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16737, -94.617, 923.289, 26.179, 0.250);
            GlobalObject.Remove(player, 16737, new Vector3(-94.617, 923.289, 26.179), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16738, -217.492, 1026.819, 27.679, 0.250);
            GlobalObject.Remove(player, 16738, new Vector3(-217.492, 1026.819, 27.679), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16739, -297.101, 1152.969, 27.007, 0.250);
            GlobalObject.Remove(player, 16739, new Vector3(-297.101, 1152.969, 27.007), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16740, -152.320, 1144.069, 30.304, 0.250);
            GlobalObject.Remove(player, 16740, new Vector3(-152.320, 1144.069, 30.304), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16742, 207.429, 1077.300, 31.914, 0.250);
            GlobalObject.Remove(player, 16742, new Vector3(207.429, 1077.300, 31.914), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16743, 266.445, 1225.290, 32.281, 0.250);
            GlobalObject.Remove(player, 16743, new Vector3(266.445, 1225.290, 32.281), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16744, 351.984, 1368.900, 20.109, 0.250);
            GlobalObject.Remove(player, 16744, new Vector3(351.984, 1368.900, 20.109), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16745, 392.914, 1511.560, 21.585, 0.250);
            GlobalObject.Remove(player, 16745, new Vector3(392.914, 1511.560, 21.585), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16746, 472.687, 1639.569, 13.765, 0.250);
            GlobalObject.Remove(player, 16746, new Vector3(472.687, 1639.569, 13.765), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16442, -18.468, 1178.880, 29.375, 0.250);
            GlobalObject.Remove(player, 16442, new Vector3(-18.468, 1178.880, 29.375), 0.25f);
            //RemoveBuildingForPlayer(playerid, 956, -76.031, 1227.989, 19.125, 0.250);
            GlobalObject.Remove(player, 956, new Vector3(-76.031, 1227.989, 19.125), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16475, -98.195, 1180.069, 18.734, 0.250);
            GlobalObject.Remove(player, 16475, new Vector3(-98.195, 1180.069, 18.734), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16476, -98.195, 1180.069, 18.734, 0.250);
            GlobalObject.Remove(player, 16476, new Vector3(-98.195, 1180.069, 18.734), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1308, -96.718, 1164.349, 18.734, 0.250);
            GlobalObject.Remove(player, 1308, new Vector3(-96.718, 1164.349, 18.734), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1345, -88.859, 1165.380, 19.460, 0.250);
            GlobalObject.Remove(player, 1345, new Vector3(-88.859, 1165.380, 19.460), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1308, -76.531, 1187.640, 18.734, 0.250);
            GlobalObject.Remove(player, 1308, new Vector3(-76.531, 1187.640, 18.734), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16337, 713.804, 906.812, -19.914, 0.250);
            GlobalObject.Remove(player, 16337, new Vector3(713.804, 906.812, -19.914), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16334, 709.445, 915.929, 34.617, 0.250);
            GlobalObject.Remove(player, 16334, new Vector3(709.445, 915.929, 34.617), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1345, -170.171, 1169.050, 19.539, 0.250);
            GlobalObject.Remove(player, 1345, new Vector3(-170.171, 1169.050, 19.539), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1692, -174.242, 1177.900, 22.781, 0.250);
            GlobalObject.Remove(player, 1692, new Vector3(-174.242, 1177.900, 22.781), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1352, -109.944, 1188.020, 18.710, 0.250);
            GlobalObject.Remove(player, 1352, new Vector3(-109.944, 1188.020, 18.710), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16777, -105.359, 1212.069, 18.734, 0.250);
            GlobalObject.Remove(player, 16777, new Vector3(-105.359, 1212.069, 18.734), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1308, 292.898, 1237.150, 14.859, 0.250);
            GlobalObject.Remove(player, 1308, new Vector3(292.898, 1237.150, 14.859), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16385, -122.741, 1122.750, 18.734, 0.250);
            GlobalObject.Remove(player, 16385, new Vector3(-122.741, 1122.750, 18.734), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16617, -122.741, 1122.750, 18.734, 0.250);
            GlobalObject.Remove(player, 16617, new Vector3(-122.741, 1122.750, 18.734), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1412, -133.852, 1134.410, 20.023, 0.250);
            GlobalObject.Remove(player, 1412, new Vector3(-133.852, 1134.410, 20.023), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1308, -133.358, 1137.589, 18.734, 0.250);
            GlobalObject.Remove(player, 1308, new Vector3(-133.358, 1137.589, 18.734), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1412, -133.983, 1111.079, 20.023, 0.250);
            GlobalObject.Remove(player, 1412, new Vector3(-133.983, 1111.079, 20.023), 0.25f);
            //RemoveBuildingForPlayer(playerid, 669, -120.875, 1110.420, 18.679, 0.250);
            GlobalObject.Remove(player, 669, new Vector3(-120.875, 1110.420, 18.679), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1411, -136.539, 1108.229, 20.335, 0.250);
            GlobalObject.Remove(player, 1411, new Vector3(-136.539, 1108.229, 20.335), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1411, -141.733, 1108.229, 20.335, 0.250);
            GlobalObject.Remove(player, 1411, new Vector3(-141.733, 1108.229, 20.335), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1411, -146.929, 1108.229, 20.335, 0.250);
            GlobalObject.Remove(player, 1411, new Vector3(-146.929, 1108.229, 20.335), 0.25f);
            //RemoveBuildingForPlayer(playerid, 1345, -149.852, 1133.770, 19.539, 0.250);
            GlobalObject.Remove(player, 1345, new Vector3(-149.852, 1133.770, 19.539), 0.25f);
            //RemoveBuildingForPlayer(playerid, 669, -228.828, 1050.750, 18.812, 0.250);
            GlobalObject.Remove(player, 669, new Vector3(-228.828, 1050.750, 18.812), 0.25f);
            //RemoveBuildingForPlayer(playerid, 669, -233.117, 1061.660, 18.859, 0.250);
            GlobalObject.Remove(player, 669, new Vector3(-233.117, 1061.660, 18.859), 0.25f);
            //fk 11 removes up

            /**
             new tmpobjid, object_world = -1, object_int = -1;
tmpobjid = CreateDynamicObject(19433, 505.475189, -78.922294, 1003.211303, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
SetDynamicObjectMaterial(tmpobjid, 0, 3922, "bistro", "sw_wallbrick_01", 0x00000000);
tmpobjid = CreateDynamicObject(19376, 511.085784, -78.926460, 999.708374, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
SetDynamicObjectMaterial(tmpobjid, 0, 3922, "bistro", "sw_wallbrick_01", 0x00000000);

             **/
            var bar1 = new DynamicObject(19433, new Vector3(505.475189, -78.922294, 1003.211303), new Vector3(0.0, 0.0, 90.0), 1002, 11, player, 25, 25);
            bar1.SetMaterial(0, 3922, "bistro", "sw_wallbrick_01", 0);
            bar1.ShowInInterior(11);
            var bar2 = new DynamicObject(19376, new Vector3(511.085784, -78.926460, 999.708374), new Vector3(0.0, 0.0, 90.0), 1002, 11, player, 25, 25);
            bar2.SetMaterial(0, 3922, "bistro", "sw_wallbrick_01", 0);
            bar2.ShowInInterior(11);

            //tmpobjid = CreateDynamicObject(2205, 510.326538, -81.602119, 997.940856, 0.000000, 0.000000, 107.400016, object_world, object_int, -1, 300.00, 300.00); 
            var bar3 = new DynamicObject(2205, new Vector3(510.326538, -81.602119, 997.940856), new Vector3(0.000000, 0.000000, 107.400016), 1002, 11, player, 25, 25);
            bar3.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(1671, 511.589538, -80.387573, 998.391113, 0.000000, 0.000000, -54.700004, object_world, object_int, -1, 300.00, 300.00); 
            var bar4 = new DynamicObject(1671, new Vector3(511.589538, -80.387573, 998.391113), new Vector3(0.000000, 0.000000, -54.700004), 1002, 11, player, 25, 25);
            bar4.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(1728, 511.626251, -85.992668, 997.900878, 0.000000, 0.000000, -109.900039, object_world, object_int, -1, 300.00, 300.00);
            var bar5 = new DynamicObject(1728, new Vector3(511.626251, -85.992668, 997.900878), new Vector3(0.000000, 0.000000, -109.900039), 1002, 11, player, 25, 25);
            bar5.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(2083, 509.925415, -85.762634, 997.830627, 0.000000, 0.000000, -129.800018, object_world, object_int, -1, 300.00, 300.00);
            var bar6 = new DynamicObject(2083, new Vector3(509.925415, -85.762634, 997.830627), new Vector3(0.000000, 0.000000, -129.800018), 1002, 11, player, 25, 25);
            bar6.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(1729, 509.318237, -88.108978, 997.910888, 0.000000, 0.000000, 162.700042, object_world, object_int, -1, 300.00, 300.00); 
            var bar7 = new DynamicObject(1729, new Vector3(509.318237, -88.108978, 997.910888), new Vector3(0.000000, 0.000000, 162.700042), 1002, 11, player, 25, 25);
            bar7.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(1575, 510.075561, -86.646141, 998.301269, 0.000000, 0.000000, 57.100006, object_world, object_int, -1, 300.00, 300.00); 
            var bar8 = new DynamicObject(1575, new Vector3(510.075561, -86.646141, 998.301269), new Vector3(0.000000, 0.000000, 57.100006), 1002, 11, player, 25, 25);
            bar8.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(335, 510.063659, -86.532058, 998.660644, 178.699996, -6.399999, -12.599999, object_world, object_int, -1, 300.00, 300.00); 
            var bar9 = new DynamicObject(335, new Vector3(510.063659, -86.532058, 998.660644), new Vector3(178.699996, -6.399999, -12.599999), 1002, 11, player, 25, 25);
            bar9.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(19177, 510.096099, -86.580162, 998.481018, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var bar10 = new DynamicObject(19177, new Vector3(510.096099, -86.580162, 998.481018), new Vector3(0.0, 0.0, 90.0), 1002, 11, player, 25, 25);
            bar10.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(19177, 510.216217, -86.320182, 998.340881, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var bar11 = new DynamicObject(19177, new Vector3(510.216217, -86.320182, 998.340881), new Vector3(0.0, 0.0, 0.0), 1002, 11, player, 25, 25);
            bar11.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(3109, 505.189270, -89.185325, 999.141174, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var bar12 = new DynamicObject(3109, new Vector3(505.189270, -89.185325, 999.141174), new Vector3(0.0, 0.0, -90.0), 1002, 11, player, 25, 25);
            bar12.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(348, 510.340576, -80.542182, 998.911010, 270.000000, -166.199981, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var bar13 = new DynamicObject(348, new Vector3(510.340576, -80.542182, 998.911010), new Vector3(270.000000, -166.199981, 0.000000), 1002, 11, player, 25, 25);
            bar13.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(19893, 510.302368, -81.220336, 998.871154, 0.000000, 0.000000, 133.500000, object_world, object_int, -1, 300.00, 300.00); 
            var bar14 = new DynamicObject(19893, new Vector3(510.302368, -81.220336, 998.871154), new Vector3(0.0, 0.0, 133.5), 1002, 11, player, 25, 25);
            bar14.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(1550, 509.668945, -79.683685, 998.398986, 15.200001, 0.000000, 38.900001, object_world, object_int, -1, 300.00, 300.00); 
            var bar15 = new DynamicObject(1550, new Vector3(509.668945, -79.683685, 998.398986), new Vector3(15.200001, 0.000000, 38.900001), 1002, 11, player, 25, 25);
            bar15.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(2059, 510.137420, -80.590240, 998.881835, 0.000000, 0.000000, -51.000007, object_world, object_int, -1, 300.00, 300.00); 
            var bar16 = new DynamicObject(2059, new Vector3(510.137420, -80.590240, 998.881835), new Vector3(0.000000, 0.000000, -51.000007), 1002, 11, player, 25, 25);
            bar16.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(1665, 510.132781, -80.162391, 998.891845, 0.000000, 0.000000, 58.299995, object_world, object_int, -1, 300.00, 300.00); 
            var bar17 = new DynamicObject(1665, new Vector3(510.132781, -80.162391, 998.891845), new Vector3(0.000000, 0.000000, 58.299995), 1002, 11, player, 25, 25);
            bar17.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(18673, 510.059875, -80.152351, 997.350585, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var bar18 = new DynamicObject(18673, new Vector3(510.059875, -80.152351, 997.350585), new Vector3(0.0, 0.0, 0.0), 1002, 11, player, 25, 25);
            bar18.ShowInInterior(11);
            //tmpobjid = CreateDynamicObject(1893, 510.169097, -81.358398, 1002.201416, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var bar19 = new DynamicObject(1893, new Vector3(510.169097, -81.358398, 1002.201416), new Vector3(0.0, 0.0, 90.0), 1002, 11, player, 25, 25);
            bar19.ShowInInterior(11);
            //____________________________________________________________________SHOP MAPP______________________________________________________________
            //tmpobjid = CreateDynamicObject(19379, 1330.697998, 1363.093750, 3000.029541, -90.000000, 270.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 16640, "a51", "des_tunnellight", 0x00000000);
            var shop1 = new DynamicObject(19379, new Vector3(1330.697998, 1363.093750, 3000.029541), new Vector3(-90.000000, 270.000000, 0.000000), 1003, 1, player, 25, 25);
            shop1.SetMaterial(0, 16640, "a51", "des_tunnellight", 0);
            shop1.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19379, 1324.976562, 1358.404785, 3000.029541, 0.000000, 180.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0x00000000);
            var shop2 = new DynamicObject(19379, new Vector3(1324.976562, 1358.404785, 3000.029541), new Vector3(0.000000, 180.000000, 0.000000), 1003, 1, player, 25, 25);
            shop2.SetMaterial(0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0);
            shop2.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19379, 1335.397216, 1358.404785, 3000.029541, 90.000000, 180.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 16640, "a51", "des_tunnellight", 0x00000000);
            var shop3 = new DynamicObject(19379, new Vector3(1335.397216, 1358.404785, 3000.029541), new Vector3(90.000000, 180.000000, 0.000000), 1003, 1, player, 25, 25);
            shop3.SetMaterial(0, 16640, "a51", "des_tunnellight", 0);
            shop3.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19379, 1330.187500, 1353.653564, 3000.029541, -90.000000, 270.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0x00000000);
            var shop4 = new DynamicObject(19379, new Vector3(1330.187500, 1353.653564, 3000.029541), new Vector3(-90.000000, 270.000000, 0.000000), 1003, 1, player, 25, 25);
            shop4.SetMaterial(0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0);
            shop4.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19379, 1333.060546, 1362.302246, 3000.029541, -90.000000, 315.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0x00000000);
            var shop5 = new DynamicObject(19379, new Vector3(1333.060546, 1362.302246, 3000.029541), new Vector3(-90.000000, 315.000000, 0.000000), 1003, 1, player, 25, 25);
            shop5.SetMaterial(0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0);
            shop5.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19379, 1330.095947, 1358.404785, 3003.939208, 0.000000, 90.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 11301, "carshow_sfse", "ws_officy_ceiling", 0x00000000);
            var shop6 = new DynamicObject(19379, new Vector3(1330.095947, 1358.404785, 3003.939208), new Vector3(0.000000, 90.000000, 0.000000), 1003, 1, player, 25, 25);
            shop6.SetMaterial(0, 11301, "carshow_sfse", "ws_officy_ceiling", 0);
            shop6.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19379, 1337.008422, 1363.023925, 3000.029541, -90.000000, 270.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0x00000000);
            var shop7 = new DynamicObject(19379, new Vector3(1337.008422, 1363.023925, 3000.029541), new Vector3(-90.000000, 270.000000, 0.000000), 1003, 1, player, 25, 25);
            shop7.SetMaterial(0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0);
            shop7.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19379, 1320.377929, 1363.023925, 3000.029541, -90.000000, 270.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0x00000000);
            var shop8 = new DynamicObject(19379, new Vector3(1320.377929, 1363.023925, 3000.029541), new Vector3(-90.000000, 270.000000, 0.000000), 1003, 1, player, 25, 25);
            shop8.SetMaterial(0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0);
            shop8.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19379, 1335.328002, 1364.022827, 3000.029541, -90.000000, 270.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0x00000000);
            var shop9 = new DynamicObject(19379, new Vector3(1335.328002, 1364.022827, 3000.029541), new Vector3(-90.000000, 270.000000, 90.000000), 1003, 1, player, 25, 25);
            shop9.SetMaterial(0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0);
            shop9.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19379, 1335.328002, 1349.572509, 3000.029541, -90.000000, 270.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0x00000000);
            var shop10 = new DynamicObject(19379, new Vector3(1335.328002, 1349.572509, 3000.029541), new Vector3(-90.000000, 270.000000, 90.000000), 1003, 1, player, 25, 25);
            shop10.SetMaterial(0, 5406, "jeffers5a_lae", "vgshopwall05_64", 0);
            shop10.ShowInWorld(1003);
            //tmpobjid = CreateDynamicObject(14902, 1329.656372, 1377.856445, 3000.035400, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            //tmpobjid = CreateDynamicObject(14902, 1320.457275, 1364.107666, 3000.035400, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00); 

            //tmpobjid = CreateDynamicObject(14902, 1329.656372, 1377.856445, 3000.035400, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop11 = new DynamicObject(14902, new Vector3(1329.656372, 1377.876464, 3000.035400), new Vector3(0.000000, 0.000000, 180.000000), 1003, 1, player, 25, 25);
            shop11.ShowInWorld(1003);
            //tmpobjid = CreateDynamicObject(14902, 1320.457275, 1364.107666, 3000.035400, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop12 = new DynamicObject(14902, new Vector3(1320.457275, 1364.107666, 3000.035400), new Vector3(0.000000, 0.000000, 270.000000), 1003, 1, player, 25, 25);
            shop12.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1532, 1333.204711, 1362.015747, 3000.095458, 0.000000, 0.000000, -45.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop13 = new DynamicObject(1532, new Vector3(1333.204711, 1362.015747, 3000.095458), new Vector3(0.000000, 0.000000, -45.000000), 1003, 1, player, 25, 25);
            shop13.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(2582, 1335.048950, 1357.887695, 3000.956298, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop14 = new DynamicObject(2582, new Vector3(1335.048950, 1357.887695, 3000.956298), new Vector3(0.000000, 0.000000, 270.000000), 1003, 1, player, 25, 25);
            shop14.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1893, 1332.182373, 1355.174072, 3004.317138, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop15 = new DynamicObject(1893, new Vector3(1332.182373, 1355.174072, 3004.317138), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop15.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1893, 1332.182373, 1359.994628, 3004.317138, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop16 = new DynamicObject(1893, new Vector3(1332.182373, 1359.994628, 3004.317138), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop16.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1893, 1326.941040, 1355.174072, 3004.337158, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop17 = new DynamicObject(1893, new Vector3(1326.941040, 1355.174072, 3004.337158), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop17.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1893, 1326.911987, 1359.995605, 3004.083496, 0.000000, -12.000000, -5.999999, object_world, object_int, -1, 300.00, 300.00); 
            var shop18 = new DynamicObject(1893, new Vector3(1326.911987, 1359.995605, 3004.083496), new Vector3(0.000000, -12.000000, -5.999999), 1003, 1, player, 25, 25);
            shop18.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(2582, 1335.048950, 1355.907104, 3000.956298, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop19 = new DynamicObject(2582, new Vector3(1335.048950, 1355.907104, 3000.956298), new Vector3(0.000000, 0.000000, 270.000000), 1003, 1, player, 25, 25);
            shop19.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(11706, 1334.541015, 1360.332885, 3000.306152, 87.700012, -33.399997, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop20 = new DynamicObject(11706, new Vector3(1334.541015, 1360.332885, 3000.306152), new Vector3(87.700012, -33.399997, 0.000000), 1003, 1, player, 25, 25);
            shop20.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1984, 1328.613647, 1356.336425, 3000.105468, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop21 = new DynamicObject(1984, new Vector3(1328.613647, 1356.336425, 3000.105468), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop21.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1850, 1329.979736, 1354.135498, 3000.045410, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop22 = new DynamicObject(1850, new Vector3(1329.979736, 1354.135498, 3000.045410), new Vector3(0.000000, 0.000000, 180.000000), 1003, 1, player, 25, 25);
            shop22.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1850, 1326.920288, 1354.135498, 3000.045410, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop23 = new DynamicObject(1850, new Vector3(1326.920288, 1354.135498, 3000.045410), new Vector3(0.000000, 0.000000, 180.000000), 1003, 1, player, 25, 25);
            shop23.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1843, 1325.734252, 1356.451293, 3000.035400, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop24 = new DynamicObject(1843, new Vector3(1325.734252, 1356.451293, 3000.035400), new Vector3(0.000000, 0.000000, 90.000000), 1003, 1, player, 25, 25);
            shop24.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(2365, 1332.344360, 1354.440551, 3000.095458, 0.000000, 0.000000, 23.400003, object_world, object_int, -1, 300.00, 300.00); 
            var shop25 = new DynamicObject(2365, new Vector3(1332.344360, 1354.440551, 3000.095458), new Vector3(0.000000, 0.000000, 23.400003), 1003, 1, player, 25, 25);
            shop25.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1886, 1325.594848, 1354.228271, 3004.075195, 9.300000, 0.000000, 128.400024, object_world, object_int, -1, 300.00, 300.00); 
            var shop26 = new DynamicObject(1886, new Vector3(1325.594848, 1354.228271, 3004.075195), new Vector3(9.300000, 0.000000, 128.400024), 1003, 1, player, 25, 25);
            shop26.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(2541, 1331.121704, 1354.183471, 3002.056884, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop27 = new DynamicObject(2541, new Vector3(1331.121704, 1354.183471, 3002.056884), new Vector3(0.000000, 0.000000, 180.000000), 1003, 1, player, 25, 25);
            shop27.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(2541, 1330.131103, 1354.183471, 3002.056884, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop28 = new DynamicObject(2541, new Vector3(1330.131103, 1354.183471, 3002.056884), new Vector3(0.000000, 0.000000, 180.000000), 1003, 1, player, 25, 25);
            shop28.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1455, 1331.355224, 1354.209960, 3000.445800, 0.000000, 0.000000, -43.799995, object_world, object_int, -1, 300.00, 300.00); 
            var shop29 = new DynamicObject(1455, new Vector3(1331.355224, 1354.209960, 3000.445800), new Vector3(0.000000, 0.000000, -43.799995), 1003, 1, player, 25, 25);
            shop29.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1546, 1330.912353, 1354.084594, 3000.455810, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop30 = new DynamicObject(1546, new Vector3(1330.912353, 1354.084594, 3000.455810), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop30.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1546, 1331.252685, 1354.084594, 3000.886230, 0.000000, 0.000000, 74.399986, object_world, object_int, -1, 300.00, 300.00); 
            var shop31 = new DynamicObject(1546, new Vector3(1331.252685, 1354.084594, 3000.886230), new Vector3(0.000000, 0.000000, 74.399986), 1003, 1, player, 25, 25);
            shop31.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1577, 1330.351196, 1354.097045, 3000.345703, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop32 = new DynamicObject(1577, new Vector3(1330.351196, 1354.097045, 3000.345703), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop32.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1577, 1330.311157, 1354.097045, 3000.485839, 0.000000, 0.000000, -12.499998, object_world, object_int, -1, 300.00, 300.00); 
            var shop33 = new DynamicObject(1577, new Vector3(1330.311157, 1354.097045, 3000.485839), new Vector3(0.000000, 0.000000, -12.499998), 1003, 1, player, 25, 25);
            shop33.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1778, 1335.137329, 1354.365600, 3000.105468, 0.000000, 0.000000, -43.200008, object_world, object_int, -1, 300.00, 300.00); 
            var shop34 = new DynamicObject(1778, new Vector3(1335.137329, 1354.365600, 3000.105468), new Vector3(0.000000, 0.000000, -43.200008), 1003, 1, player, 25, 25);
            shop34.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1810, 1329.083251, 1355.350097, 3000.095458, 0.000000, 0.000000, 146.000030, object_world, object_int, -1, 300.00, 300.00); 
            var shop35 = new DynamicObject(1810, new Vector3(1329.083251, 1355.350097, 3000.095458), new Vector3(0.000000, 0.000000, 146.000030), 1003, 1, player, 25, 25);
            shop35.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1901, 1330.687866, 1354.001708, 3000.786132, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop36 = new DynamicObject(1901, new Vector3(1330.687866, 1354.001708, 3000.786132), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop36.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1901, 1330.577758, 1354.091796, 3000.786132, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop37 = new DynamicObject(1901, new Vector3(1330.577758, 1354.091796, 3000.786132), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop37.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1901, 1330.797607, 1354.131835, 3000.786132, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop38 = new DynamicObject(1901, new Vector3(1330.797607, 1354.131835, 3000.786132), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop38.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1901, 1330.887695, 1354.001708, 3000.786132, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop39 = new DynamicObject(1901, new Vector3(1330.887695, 1354.001708, 3000.786132), new Vector3(0.0, 0.0, 0.0), 1003, 1, player, 25, 25);
            shop39.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1951, 1329.758544, 1354.114868, 3000.545898, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop40 = new DynamicObject(1951, new Vector3(1329.758544, 1354.114868, 3000.545898), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop40.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(1951, 1329.518310, 1354.214965, 3000.545898, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop41 = new DynamicObject(1951, new Vector3(1329.518310, 1354.214965, 3000.545898), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop41.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(2057, 1327.954467, 1354.087646, 3000.555908, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop42 = new DynamicObject(2057, new Vector3(1327.954467, 1354.087646, 3000.555908), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop42.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(2060, 1326.858032, 1354.109130, 3000.485839, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop43 = new DynamicObject(2060, new Vector3(1326.858032, 1354.109130, 3000.485839), new Vector3(0.000000, 0.000000, 0.000000), 1003, 1, player, 25, 25);
            shop43.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(2286, 1333.476196, 1353.765136, 3002.371826, 0.000000, -6.099999, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop44 = new DynamicObject(2286, new Vector3(1333.476196, 1353.765136, 3002.371826), new Vector3(0.000000, -6.099999, 180.000000), 1003, 1, player, 25, 25);
            shop44.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(2342, 1329.139282, 1356.332031, 3001.156494, 0.000000, 0.000000, 42.099998, object_world, object_int, -1, 300.00, 300.00); 
            var shop45 = new DynamicObject(2342, new Vector3(1329.139282, 1356.332031, 3001.156494), new Vector3(0.000000, 0.000000, 42.099998), 1003, 1, player, 25, 25);
            shop45.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(2647, 1329.337646, 1354.131347, 3002.157470, 0.000000, 0.000000, -119.500000, object_world, object_int, -1, 300.00, 300.00); 
            var shop46 = new DynamicObject(2647, new Vector3(1329.337646, 1354.131347, 3002.157470), new Vector3(0.000000, 0.000000, -119.500000), 1003, 1, player, 25, 25);
            shop46.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(11736, 1331.146972, 1354.106323, 3001.266601, 0.000000, 0.000000, 18.899999, object_world, object_int, -1, 300.00, 300.00); 
            var shop47 = new DynamicObject(11736, new Vector3(1331.146972, 1354.106323, 3001.266601), new Vector3(0.000000, 0.000000, 18.899999), 1003, 1, player, 25, 25);
            shop47.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(11738, 1330.609130, 1354.075195, 3001.256591, 0.000000, 0.000000, -10.399998, object_world, object_int, -1, 300.00, 300.00); 
            var shop48 = new DynamicObject(11738, new Vector3(1330.609130, 1354.075195, 3001.256591), new Vector3(0.000000, 0.000000, -10.399998), 1003, 1, player, 25, 25);
            shop48.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(11738, 1330.077270, 1354.041137, 3001.256591, 0.000000, 0.000000, 8.899999, object_world, object_int, -1, 300.00, 300.00); 
            var shop49 = new DynamicObject(11738, new Vector3(1330.077270, 1354.041137, 3001.256591), new Vector3(0.000000, 0.000000, 8.899999), 1003, 1, player, 25, 25);
            shop49.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19563, 1329.972534, 1354.043334, 3000.796142, 0.000000, 0.000000, -22.799999, object_world, object_int, -1, 300.00, 300.00); 
            var shop50 = new DynamicObject(19563, new Vector3(1329.972534, 1354.043334, 3000.796142), new Vector3(0.000000, 0.000000, -22.799999), 1003, 1, player, 25, 25);
            shop50.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19563, 1330.203979, 1354.077148, 3000.796142, 0.000000, 0.000000, -22.799999, object_world, object_int, -1, 300.00, 300.00); 
            var shop51 = new DynamicObject(19563, new Vector3(1330.203979, 1354.077148, 3000.796142), new Vector3(0.000000, 0.000000, -22.799999), 1003, 1, player, 25, 25);
            shop51.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19573, 1328.375244, 1354.012939, 3000.892822, 99.999931, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop52 = new DynamicObject(19563, new Vector3(1328.375244, 1354.012939, 3000.892822), new Vector3(99.999931, 0.000000, -90.000000), 1003, 1, player, 25, 25);
            shop52.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19998, 1327.512451, 1354.024047, 3000.906250, 0.000000, 0.000000, -29.799999, object_world, object_int, -1, 300.00, 300.00); 
            var shop53 = new DynamicObject(19998, new Vector3(1327.512451, 1354.024047, 3000.906250), new Vector3(0.000000, 0.000000, -29.799999), 1003, 1, player, 25, 25);
            shop53.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19998, 1327.268066, 1354.060302, 3000.906250, 0.000000, 0.000000, -29.799999, object_world, object_int, -1, 300.00, 300.00); 
            var shop54 = new DynamicObject(19998, new Vector3(1327.268066, 1354.060302, 3000.906250), new Vector3(0.000000, 0.000000, -29.799999), 1003, 1, player, 25, 25);
            shop54.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19998, 1327.028686, 1354.104858, 3000.906250, 0.000000, 0.000000, -29.799999, object_world, object_int, -1, 300.00, 300.00); 
            var shop55 = new DynamicObject(19998, new Vector3(1327.028686, 1354.104858, 3000.906250), new Vector3(0.000000, 0.000000, -29.799999), 1003, 1, player, 25, 25);
            shop55.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(19107, 1327.910766, 1354.175170, 3002.101318, -14.500000, -88.199913, -3.399999, object_world, object_int, -1, 300.00, 300.00); 
            var shop56 = new DynamicObject(19107, new Vector3(1327.910766, 1354.175170, 3002.101318), new Vector3(-14.500000, -88.199913, -3.399999), 1003, 1, player, 25, 25);
            shop56.ShowInWorld(1003);

            //tmpobjid = CreateDynamicObject(2002, 1325.560302, 1360.847412, 3000.115478, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var shop57 = new DynamicObject(2002, new Vector3(1325.560302, 1360.847412, 3000.115478), new Vector3(0.000000, 0.000000, 90.000000), 1003, 1, player, 25, 25);
            shop57.ShowInWorld(1003);
            // - dynamic fk mapp 
            //new tmpobjid, object_world = -1, object_int = -1;
            //tmpobjid = CreateDynamicObject(19321, -165.911666, 1162.784790, 24.745386, 0.000000, -1.700000, 540.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 10850, "bakerybit2_sfse", "frate64_yellow", 0x00000000);
            var fk1 = new DynamicObject(19321, new Vector3(-165.911666, 1162.784790, 24.745386), new Vector3(0.000000, -1.700000, 540.000000), 0, 0, player, 200, 200);
            fk1.SetMaterial(0,10850, "barekybit2_sfse", "frate64_yellow" , 0);

            //SetDynamicObjectMaterial(tmpobjid, 1, 10850, "bakerybit2_sfse", "frate_doors64yellow", 0x00000000);
            fk1.SetMaterial(1, 10850, "barekybit2_sfse", "frate_doors64yellow", 0);
            fk1.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19462, -436.328308, 1223.960449, 33.771858, 0.299998, 90.000000, 12.099986, object_world, object_int, -1, 300.00, 300.00); 
            var fk2 = new DynamicObject(19462, new Vector3(-436.328308, 1223.960449, 33.771858), new Vector3(0.299998, 90.000000, 12.099986), 0, 0, player, 200, 200);
            fk2.SetMaterial(0, 1692, "moregenroofstuff", "skylight_meshed", 0);
            fk2.ShowInWorld(0);
            //SetDynamicObjectMaterial(tmpobjid, 0, 1692, "moregenroofstuff", "skylight_meshed", 0x00000000);
            //tmpobjid = CreateDynamicObject(970, -438.493011, 1226.274047, 34.281845, 0.000000, 0.000000, -78.399917, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 2772, "airp_prop", "CJ_BANDEDMETAL", 0x00000000);
            //SetDynamicObjectMaterial(tmpobjid, 1, 2772, "airp_prop", "CJ_BANDEDMETAL", 0x00000000);
            var fk3 = new DynamicObject(970, new Vector3(-438.493011, 1226.274047, 34.281845), new Vector3(0.000000, 0.000000, -78.399917), 0, 0, player, 200, 200);
            fk3.SetMaterial(0, 2772, "airp_prop", "CJ_BANDEDMETAL", 0);
            fk3.SetMaterial(1, 2772, "airp_prop", "CJ_BANDEDMETAL", 0);
            fk3.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(970, -437.399078, 1220.945678, 34.281845, 0.000000, 0.000000, -78.399917, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 2772, "airp_prop", "CJ_BANDEDMETAL", 0x00000000);
            //SetDynamicObjectMaterial(tmpobjid, 1, 2772, "airp_prop", "CJ_BANDEDMETAL", 0x00000000);
            var fk4 = new DynamicObject(970, new Vector3(-437.399078, 1220.945678, 34.281845), new Vector3(0.000000, 0.000000, -78.399917), 0, 0, player, 200, 200);
            fk4.SetMaterial(0, 2772, "airp_prop", "CJ_BANDEDMETAL", 0);
            fk4.SetMaterial(1, 2772, "airp_prop", "CJ_BANDEDMETAL", 0);
            fk4.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19906, -116.160247, 1122.709106, 21.272209, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 16644, "a51_detailstuff", "steel256128", 0x00000000);
            var fk5 = new DynamicObject(19906, new Vector3(-116.160247, 1122.709106, 21.272209), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk5.SetMaterial(0, 16644, "a51_detailstuff", "steel256128", 0);
            
            fk5.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19906, -116.160247, 1110.476074, 21.272209, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 16644, "a51_detailstuff", "steel256128", 0x00000000);
            var fk6 = new DynamicObject(19906, new Vector3(-116.160247, 1110.476074, 21.272209), new Vector3(0.000000, 0.000000, 180.000000), 0, 0, player, 200, 200);
            fk6.SetMaterial(0, 16644, "a51_detailstuff", "steel256128", 0);
            fk6.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3861, -80.803718, 1184.961181, 19.859954, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk7 = new DynamicObject(3861, new Vector3(-80.803718, 1184.961181, 19.859954), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk7.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3863, -86.881233, 1184.968261, 19.832212, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk8 = new DynamicObject(3863, new Vector3(-86.881233, 1184.968261, 19.832212), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk8.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3861, -92.363731, 1184.961181, 19.859954, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk9 = new DynamicObject(3861, new Vector3(-92.363731, 1184.961181, 19.859954), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk9.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1440, -80.108375, 1169.435302, 19.242193, 0.000000, 0.000000, -91.900001, object_world, object_int, -1, 300.00, 300.00); 
            var fk10 = new DynamicObject(1440, new Vector3(-80.108375, 1169.435302, 19.242193), new Vector3(0.000000, 0.000000, -91.900001), 0, 0, player, 200, 200);
            fk10.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(9238, -79.800178, 1163.579345, 20.492195, 0.000000, 0.000000, -18.700000, object_world, object_int, -1, 300.00, 300.00); 
            var fk11 = new DynamicObject(9238, new Vector3(-79.800178, 1163.579345, 20.492195), new Vector3(0.000000, 0.000000, -18.700000), 0, 0, player, 200, 200);
            fk11.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -103.746353, 1188.331665, 18.749971, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk12 = new DynamicObject(994, new Vector3(-103.746353, 1188.331665, 18.749971), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk12.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -95.876480, 1188.331665, 18.749971, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk13 = new DynamicObject(994, new Vector3(-95.876480, 1188.331665, 18.749971), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk13.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -81.816604, 1188.331665, 18.749971, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk14 = new DynamicObject(994, new Vector3(-81.816604, 1188.331665, 18.749971), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk14.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -88.096534, 1188.331665, 18.749971, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk15 = new DynamicObject(994, new Vector3(-88.096534, 1188.331665, 18.749971), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk15.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -105.526397, 1180.331298, 18.749971, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk16 = new DynamicObject(994, new Vector3(-105.526397, 1180.331298, 18.749971), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk16.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -105.526397, 1173.972778, 18.749971, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk17 = new DynamicObject(994, new Vector3(-105.526397, 1173.972778, 18.749971), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk17.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -105.526397, 1158.428833, 18.749971, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk18 = new DynamicObject(994, new Vector3(-105.526397, 1158.428833, 18.749971), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk18.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -105.526397, 1166.210327, 18.749971, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk19 = new DynamicObject(994, new Vector3(-105.526397, 1166.210327, 18.749971), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk19.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -97.646392, 1158.428833, 18.749971, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk20 = new DynamicObject(994, new Vector3(-97.646392, 1158.428833, 18.749971), new Vector3(0.000000, 0.000000, 180.000000), 0, 0, player, 200, 200);
            fk20.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -95.446426, 1158.428833, 18.749971, 0.000000, 0.000000, -28.899986, object_world, object_int, -1, 300.00, 300.00); 
            var fk21 = new DynamicObject(994, new Vector3(-95.446426, 1158.428833, 18.749971), new Vector3(0.000000, 0.000000, -28.899986), 0, 0, player, 200, 200);
            fk21.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -75.626426, 1155.418090, 18.749971, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk22 = new DynamicObject(994, new Vector3(), new Vector3(), 0, 0, player, 200, 200);
            fk22.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -83.686347, 1155.418090, 18.749971, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk23 = new DynamicObject(994, new Vector3(-83.686347, 1155.418090, 18.749971), new Vector3(0.000000, 0.000000, 180.000000), 0, 0, player, 200, 200);
            fk23.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -75.626426, 1161.878662, 18.749971, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk24 = new DynamicObject(994, new Vector3(-75.626426, 1161.878662, 18.749971), new Vector3(0.000000, 0.000000, 270.000000), 0, 0, player, 200, 200);
            fk24.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -75.626426, 1188.309326, 18.749971, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk25 = new DynamicObject(994, new Vector3(-75.626426, 1188.309326, 18.749971), new Vector3(0.000000, 0.000000, 270.000000), 0, 0, player, 200, 200);
            fk25.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -75.626426, 1168.155273, 18.749971, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk26 = new DynamicObject(994, new Vector3(-75.626426, 1168.155273, 18.749971), new Vector3(0.000000, 0.000000, 270.000000), 0, 0, player, 200, 200);
            fk26.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -75.626426, 1174.423461, 18.749971, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk27 = new DynamicObject(994, new Vector3(-75.626426, 1174.423461, 18.749971), new Vector3(0.000000, 0.000000, 270.000000), 0, 0, player, 200, 200);
            fk27.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(994, -75.626426, 1180.682983, 18.749971, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk28 = new DynamicObject(994, new Vector3(-75.626426, 1180.682983, 18.749971), new Vector3(0.000000, 0.000000, 270.000000), 0, 0, player, 200, 200);
            fk28.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2671, -97.292160, 1176.086914, 18.749971, 0.000000, 0.000000, -70.799995, object_world, object_int, -1, 300.00, 300.00); 
            var fk29 = new DynamicObject(2671, new Vector3(-97.292160, 1176.086914, 18.749971), new Vector3(0.000000, 0.000000, -70.799995), 0, 0, player, 200, 200);
            fk29.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2670, -91.722518, 1180.281738, 18.829973, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk30 = new DynamicObject(2670, new Vector3(-91.722518, 1180.281738, 18.829973), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk30.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2671, -101.407836, 1181.432006, 18.749971, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk31 = new DynamicObject(2671, new Vector3(-101.407836, 1181.432006, 18.749971), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk31.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3863, -97.571243, 1184.968261, 19.832212, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk32 = new DynamicObject(3863, new Vector3(-97.571243, 1184.968261, 19.832212), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk32.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3014, -81.628555, 1164.227905, 18.932191, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk33 = new DynamicObject(3014, new Vector3(-81.628555, 1164.227905, 18.932191), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk33.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3014, -81.533897, 1164.878662, 18.932191, 0.000000, 0.000000, -17.699996, object_world, object_int, -1, 300.00, 300.00); 
            var fk34 = new DynamicObject(3014, new Vector3(-81.533897, 1164.878662, 18.932191), new Vector3(0.000000, 0.000000, -17.699996), 0, 0, player, 200, 200);
            fk34.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3014, -81.101936, 1165.374755, 18.932191, 0.000000, 0.000000, -34.299995, object_world, object_int, -1, 300.00, 300.00); 
            var fk35 = new DynamicObject(3014, new Vector3(-81.101936, 1165.374755, 18.932191), new Vector3(0.000000, 0.000000, -34.299995), 0, 0, player, 200, 200);
            fk35.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3014, -81.270996, 1165.127075, 19.392202, 0.000000, 0.000000, -34.299995, object_world, object_int, -1, 300.00, 300.00); 
            var fk36 = new DynamicObject(3014, new Vector3(-81.270996, 1165.127075, 19.392202), new Vector3(0.000000, 0.000000, -34.299995), 0, 0, player, 200, 200);
            fk36.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3014, -81.664154, 1164.547851, 19.412172, 0.000000, 2.299961, -34.299995, object_world, object_int, -1, 300.00, 300.00); 
            var fk37 = new DynamicObject(3014, new Vector3(-81.664154, 1164.547851, 19.412172), new Vector3(0.000000, 2.299961, -34.299995), 0, 0, player, 200, 200);
            fk37.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2040, -80.635620, 1165.645141, 18.832189, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk38 = new DynamicObject(2040, new Vector3(-80.635620, 1165.645141, 18.832189), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk38.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2040, -80.947235, 1165.786865, 18.832189, 0.000000, 0.000000, -98.199989, object_world, object_int, -1, 300.00, 300.00); 
            var fk39 = new DynamicObject(2040, new Vector3(-80.947235, 1165.786865, 18.832189), new Vector3(0.000000, 0.000000, -98.199989), 0, 0, player, 200, 200);
            fk39.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2040, -80.952682, 1165.460083, 19.298551, 5.299997, 0.000000, -119.500007, object_world, object_int, -1, 300.00, 300.00); 
            var fk40 = new DynamicObject(2040, new Vector3(-80.952682, 1165.460083, 19.298551), new Vector3(5.299997, 0.000000, -119.500007), 0, 0, player, 200, 200);
            fk40.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(355, -81.636398, 1165.197998, 19.034393, 9.199998, -90.000000, 61.300014, object_world, object_int, -1, 300.00, 300.00); 
            var fk41 = new DynamicObject(355, new Vector3(-81.636398, 1165.197998, 19.034393), new Vector3(9.199998, -90.000000, 61.300014), 0, 0, player, 200, 200);
            fk41.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(358, -81.310081, 1164.917236, 19.705297, -88.499923, -177.600006, 44.100021, object_world, object_int, -1, 300.00, 300.00); 
            var fk42 = new DynamicObject(358, new Vector3(-81.310081, 1164.917236, 19.705297), new Vector3(-88.499923, -177.600006, 44.100021), 0, 0, player, 200, 200);
            fk42.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1271, -83.008399, 1160.570800, 19.072195, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk43 = new DynamicObject(1271, new Vector3(-83.008399, 1160.570800, 19.072195), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk43.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1271, -82.719406, 1161.493286, 19.072195, 0.000000, 0.000000, -29.799997, object_world, object_int, -1, 300.00, 300.00); 
            var fk44 = new DynamicObject(1271, new Vector3(-82.719406, 1161.493286, 19.072195), new Vector3(0.000000, 0.000000, -29.799997), 0, 0, player, 200, 200);
            fk44.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1271, -82.967864, 1161.059936, 19.752193, 0.000000, 0.000000, -24.699991, object_world, object_int, -1, 300.00, 300.00); 
            var fk45 = new DynamicObject(1271, new Vector3(-82.967864, 1161.059936, 19.752193), new Vector3(0.000000, 0.000000, -24.699991), 0, 0, player, 200, 200);
            fk45.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3862, -103.372169, 1177.188110, 19.899993, 0.000000, 0.000000, 90.800048, object_world, object_int, -1, 300.00, 300.00); 
            var fk46 = new DynamicObject(3862, new Vector3(-103.372169, 1177.188110, 19.899993), new Vector3(0.000000, 0.000000, 90.800048), 0, 0, player, 200, 200);
            fk46.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3862, -103.282890, 1170.797729, 19.899993, 0.000000, 0.000000, 90.800048, object_world, object_int, -1, 300.00, 300.00); 
            var fk47 = new DynamicObject(3862, new Vector3(-103.282890, 1170.797729, 19.899993), new Vector3(0.000000, 0.000000, 90.800048), 0, 0, player, 200, 200);
            fk47.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3861, -103.243782, 1165.081176, 19.859954, 0.000000, 0.000000, -270.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk48 = new DynamicObject(3861, new Vector3(-103.243782, 1165.081176, 19.859954), new Vector3(0.000000, 0.000000, -270.000000), 0, 0, player, 200, 200);
            fk48.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1342, -78.701759, 1178.785888, 19.742198, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk49 = new DynamicObject(1342, new Vector3(-78.701759, 1178.785888, 19.742198), new Vector3(0.000000, 0.000000, 180.000000), 0, 0, player, 200, 200);
            fk49.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1342, -78.701759, 1174.595458, 19.742198, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk50 = new DynamicObject(1342, new Vector3(-78.701759, 1174.595458, 19.742198), new Vector3(0.000000, 0.000000, 180.000000), 0, 0, player, 200, 200);
            fk50.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3171, -93.008857, 1161.033691, 18.692186, 0.000000, 0.000000, -109.300025, object_world, object_int, -1, 300.00, 300.00); 
            var fk51 = new DynamicObject(3171, new Vector3(-93.008857, 1161.033691, 18.692186), new Vector3(0.000000, 0.000000, -109.300025), 0, 0, player, 200, 200);
            fk51.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2115, -91.583679, 1163.970947, 18.742187, 0.000000, 0.000000, -1.399999, object_world, object_int, -1, 300.00, 300.00); 
            var fk52 = new DynamicObject(2115, new Vector3(-91.583679, 1163.970947, 18.742187), new Vector3(0.000000, 0.000000, -1.399999), 0, 0, player, 200, 200);
            fk52.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2232, -89.481521, 1161.511108, 19.302200, 0.000000, 0.000000, -159.500045, object_world, object_int, -1, 300.00, 300.00); 
            var fk53 = new DynamicObject(2232, new Vector3(-89.481521, 1161.511108, 19.302200), new Vector3(0.000000, 0.000000, -159.500045), 0, 0, player, 200, 200);
            fk53.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2232, -88.946716, 1162.189208, 19.302200, 0.000000, 0.000000, -133.200027, object_world, object_int, -1, 300.00, 300.00); 
            var fk54 = new DynamicObject(2232, new Vector3(-88.946716, 1162.189208, 19.302200), new Vector3(0.000000, 0.000000, -133.200027), 0, 0, player, 200, 200);
            fk54.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2322, -89.182647, 1161.856689, 20.192209, 0.000000, 0.000000, -132.900024, object_world, object_int, -1, 300.00, 300.00); 
            var fk55 = new DynamicObject(2322, new Vector3(-89.182647, 1161.856689, 20.192209), new Vector3(0.000000, 0.000000, -132.900024), 0, 0, player, 200, 200);
            fk55.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(18872, -90.349845, 1164.089843, 19.532190, 0.000000, 0.000000, 33.799999, object_world, object_int, -1, 300.00, 300.00); 
            var fk56 = new DynamicObject(18872, new Vector3(-90.349845, 1164.089843, 19.532190), new Vector3(0.000000, 0.000000, 33.799999), 0, 0, player, 200, 200);
            fk56.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(18872, -90.457901, 1164.017822, 19.532190, 0.000000, 0.000000, 33.799999, object_world, object_int, -1, 300.00, 300.00); 
            var fk57 = new DynamicObject(18872, new Vector3(-90.457901, 1164.017822, 19.532190), new Vector3(0.000000, 0.000000, 33.799999), 0, 0, player, 200, 200);
            fk57.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(18867, -90.627708, 1163.989624, 19.532197, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk58 = new DynamicObject(18867, new Vector3(-90.627708, 1163.989624, 19.532197), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk58.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(18875, -90.617301, 1164.211303, 19.552200, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk59 = new DynamicObject(18875, new Vector3(-90.617301, 1164.211303, 19.552200), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk59.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1839, -90.676040, 1163.997680, 18.722187, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk60 = new DynamicObject(1839, new Vector3(-90.676040, 1163.997680, 18.722187), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk60.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1839, -91.586067, 1163.997680, 18.722187, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk61 = new DynamicObject(1839, new Vector3(-91.586067, 1163.997680, 18.722187), new Vector3(0.000000, 0.000000, 180.000000), 0, 0, player, 200, 200);
            fk61.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1785, -91.465965, 1164.010620, 19.622188, 0.000000, 0.000000, 28.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk62 = new DynamicObject(1785, new Vector3(-91.465965, 1164.010620, 19.622188), new Vector3(0.000000, 0.000000, 28.000000), 0, 0, player, 200, 200);
            fk62.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19318, -90.946540, 1161.736938, 19.401264, -11.600000, 0.000000, 161.099990, object_world, object_int, -1, 300.00, 300.00); 
            var fk63 = new DynamicObject(19318, new Vector3(-90.946540, 1161.736938, 19.401264), new Vector3(-11.600000, 0.000000, 161.099990), 0, 0, player, 200, 200);
            fk63.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3031, -87.934089, 1162.640991, 20.282199, 0.000000, 0.000000, 72.799995, object_world, object_int, -1, 300.00, 300.00); 
            var fk64 = new DynamicObject(3031, new Vector3(-87.934089, 1162.640991, 20.282199), new Vector3(0.000000, 0.000000, 72.799995), 0, 0, player, 200, 200);
            fk64.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3031, -92.773498, 1161.287963, 23.102201, 0.000000, 0.000000, -172.199981, object_world, object_int, -1, 300.00, 300.00); 
            var fk65 = new DynamicObject(3031, new Vector3(-92.773498, 1161.287963, 23.102201), new Vector3(0.000000, 0.000000, -172.199981), 0, 0, player, 200, 200);
            fk65.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19602, -82.697998, 1161.581298, 19.472187, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk66 = new DynamicObject(19602, new Vector3(-82.697998, 1161.581298, 19.472187), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk66.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1439, -83.778106, 1187.782104, 18.812189, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk67 = new DynamicObject(1439, new Vector3(-83.778106, 1187.782104, 18.812189), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk67.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1439, -104.838081, 1180.363037, 18.812189, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk68 = new DynamicObject(1439, new Vector3(-104.838081, 1180.363037, 18.812189), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk68.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2670, -91.722518, 1165.581909, 18.829973, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk69 = new DynamicObject(2670, new Vector3(-91.722518, 1165.581909, 18.829973), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk69.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1437, -170.510177, 1173.039184, 22.534311, -63.200057, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk70 = new DynamicObject(1437, new Vector3(-170.510177, 1173.039184, 22.534311), new Vector3(-63.200057, 0.000000, 180.000000), 0, 0, player, 200, 200);
            fk70.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(8572, -176.897857, 1172.841430, 20.942203, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk71 = new DynamicObject(8572, new Vector3(-176.897857, 1172.841430, 20.942203), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk71.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1711, -178.369247, 1176.847412, 21.911260, 0.000000, 0.000000, 134.399963, object_world, object_int, -1, 300.00, 300.00); 
            var fk72 = new DynamicObject(1711, new Vector3(-178.369247, 1176.847412, 21.911260), new Vector3(0.000000, 0.000000, 134.399963), 0, 0, player, 200, 200);
            fk72.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1438, -174.368743, 1179.872070, 21.911260, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk73 = new DynamicObject(1438, new Vector3(-174.368743, 1179.872070, 21.911260), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk73.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2111, -177.731231, 1178.913940, 22.291254, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk74 = new DynamicObject(2111, new Vector3(-177.731231, 1178.913940, 22.291254), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk74.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3062, -170.501571, 1177.096191, 23.351274, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk75 = new DynamicObject(3062, new Vector3(-170.501571, 1177.096191, 23.351274), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk75.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19321, -166.945556, 1178.674926, 23.406112, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk76 = new DynamicObject(19321, new Vector3(-166.945556, 1178.674926, 23.406112), new Vector3(0.000000, 0.000000, 270.000000), 0, 0, player, 200, 200);
            fk76.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3062, -170.501571, 1180.196533, 23.351274, 0.000000, 0.000000, 154.500076, object_world, object_int, -1, 300.00, 300.00); 
            var fk77 = new DynamicObject(3062, new Vector3(-170.501571, 1180.196533, 23.351274), new Vector3(0.000000, 0.000000, 154.500076), 0, 0, player, 200, 200);
            fk77.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1793, -166.838562, 1179.865112, 21.933109, 0.000000, 0.000000, -107.399971, object_world, object_int, -1, 300.00, 300.00); 
            var fk78 = new DynamicObject(1793, new Vector3(-166.838562, 1179.865112, 21.933109), new Vector3(0.000000, 0.000000, -107.399971), 0, 0, player, 200, 200);
            fk78.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1362, -171.524703, 1177.684204, 22.531274, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk79 = new DynamicObject(1362, new Vector3(-171.524703, 1177.684204, 22.531274), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk79.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(18688, -171.631439, 1177.581420, 21.341255, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk80 = new DynamicObject(18688, new Vector3(-171.631439, 1177.581420, 21.341255), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk80.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1372, -165.089385, 1173.205932, 18.862190, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk81 = new DynamicObject(1372, new Vector3(-165.089385, 1173.205932, 18.862190), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk81.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1372, -167.489410, 1173.205932, 18.862190, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk82 = new DynamicObject(1372, new Vector3(-167.489410, 1173.205932, 18.862190), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk82.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(14861, -168.618057, 1169.144897, 19.099996, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk83 = new DynamicObject(14861, new Vector3(-168.618057, 1169.144897, 19.099996), new Vector3(0.000000, 0.000000, -90.000000), 0, 0, player, 200, 200);
            fk83.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1483, -167.113601, 1170.209594, 21.602216, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk84 = new DynamicObject(1483, new Vector3(-167.113601, 1170.209594, 21.602216), new Vector3(0.000000, 0.000000, -90.000000), 0, 0, player, 200, 200);
            fk84.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1483, -167.113601, 1172.929565, 18.092205, 0.000000, 180.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk85 = new DynamicObject(1483, new Vector3(-167.113601, 1172.929565, 18.092205), new Vector3(0.000000, 180.000000, -90.000000), 0, 0, player, 200, 200);
            fk85.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1464, -172.671707, 1173.013305, 19.810003, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk86 = new DynamicObject(1464, new Vector3(-172.671707, 1173.013305, 19.810003), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk86.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1464, -169.911727, 1173.013305, 19.810003, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk87 = new DynamicObject(1464, new Vector3(-169.911727, 1173.013305, 19.810003), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk87.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(14880, -165.141937, 1169.664916, 19.079984, 0.000000, 0.000000, 90.199996, object_world, object_int, -1, 300.00, 300.00); 
            var fk88 = new DynamicObject(14880, new Vector3(-165.141937, 1169.664916, 19.079984), new Vector3(0.000000, 0.000000, 90.199996), 0, 0, player, 200, 200);
            fk88.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1265, -170.466110, 1168.853637, 19.119993, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk89 = new DynamicObject(1265, new Vector3(-170.466110, 1168.853637, 19.119993), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk89.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1721, -175.000732, 1172.779785, 18.729988, 0.000000, 0.000000, 119.400016, object_world, object_int, -1, 300.00, 300.00); 
            var fk90 = new DynamicObject(1721, new Vector3(-175.000732, 1172.779785, 18.729988), new Vector3(0.000000, 0.000000, 119.400016), 0, 0, player, 200, 200);
            fk90.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1219, -180.085861, 1165.808837, 19.932189, 0.000000, -90.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk91 = new DynamicObject(1219, new Vector3(-180.085861, 1165.808837, 19.932189), new Vector3(0.000000, -90.000000, 0.000000), 0, 0, player, 200, 200);
            fk91.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1219, -180.095855, 1160.959228, 19.932262, 0.000000, -90.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk92 = new DynamicObject(1219, new Vector3(-180.095855, 1160.959228, 19.932262), new Vector3(0.000000, -90.000000, 0.000000), 0, 0, player, 200, 200);
            fk92.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1219, -180.105850, 1184.277832, 19.932346, 0.000000, -90.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk93 = new DynamicObject(1219, new Vector3(-180.105850, 1184.277832, 19.932346), new Vector3(0.000000, -90.000000, 0.000000), 0, 0, player, 200, 200);
            fk93.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(18660, -173.350509, 1181.628295, 23.541265, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk94 = new DynamicObject(18660, new Vector3(-173.350509, 1181.628295, 23.541265), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk94.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2866, -177.994857, 1178.681518, 22.711265, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk95 = new DynamicObject(2866, new Vector3(-177.994857, 1178.681518, 22.711265), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk95.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1812, -165.158523, 1159.527832, 23.256544, 0.000000, 3.400002, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk96 = new DynamicObject(1812, new Vector3(-165.158523, 1159.527832, 23.256544), new Vector3(0.000000, 3.400002, 0.000000), 0, 0, player, 200, 200);
            fk96.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1812, -165.158523, 1163.027954, 23.256544, 0.000000, 3.400002, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk97 = new DynamicObject(1812, new Vector3(-165.158523, 1163.027954, 23.256544), new Vector3(0.000000, 3.400002, 0.000000), 0, 0, player, 200, 200);
            fk97.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1812, -166.960144, 1160.145751, 23.365673, 0.000000, 3.400002, -5.299999, object_world, object_int, -1, 300.00, 300.00); 
            var fk98 = new DynamicObject(1812, new Vector3(-166.960144, 1160.145751, 23.365673), new Vector3(0.000000, 3.400002, -5.299999), 0, 0, player, 200, 200);
            fk98.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1369, -171.112365, 1162.002197, 24.006126, 0.000000, 1.799999, 164.999923, object_world, object_int, -1, 300.00, 300.00); 
            var fk99 = new DynamicObject(1369, new Vector3(-171.112365, 1162.002197, 24.006126), new Vector3(0.000000, 1.799999, 164.999923), 0, 0, player, 200, 200);
            fk99.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1722, -174.651443, 1163.716430, 23.516119, -1.299993, 2.300000, -59.500000, object_world, object_int, -1, 300.00, 300.00); 
            var fk100 = new DynamicObject(1722, new Vector3(-174.651443, 1163.716430, 23.516119), new Vector3(-1.299993, 2.300000, -59.500000), 0, 0, player, 200, 200);
            fk100.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1362, -173.948303, 1161.040771, 24.056156, 1.799998, 1.600000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk101 = new DynamicObject(1362, new Vector3(-173.948303, 1161.040771, 24.056156), new Vector3(1.799998, 1.600000, 0.000000), 0, 0, player, 200, 200);
            fk101.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(18688, -174.046630, 1160.951293, 22.996103, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk102 = new DynamicObject(18688, new Vector3(-174.046630, 1160.951293, 22.996103), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk102.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1474, -168.349624, 1160.214111, 24.676137, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk103 = new DynamicObject(1474, new Vector3(-168.349624, 1160.214111, 24.676137), new Vector3(0.000000, 0.000000, 270.000000), 0, 0, player, 200, 200);
            fk103.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1474, -168.349624, 1161.714599, 24.676137, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk104 = new DynamicObject(1474, new Vector3(-168.349624, 1161.714599, 24.676137), new Vector3(0.000000, 0.000000, 270.000000), 0, 0, player, 200, 200);
            fk104.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(917, -168.969360, 1164.771850, 23.496051, 0.000000, 1.800001, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk105 = new DynamicObject(917, new Vector3(-168.969360, 1164.771850, 23.496051), new Vector3(0.000000, 1.800001, 0.000000), 0, 0, player, 200, 200);
            fk105.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, -81.232734, 1305.032470, 10.902971, 0.000000, 0.000000, 360.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk106 = new DynamicObject(3279, new Vector3(-81.232734, 1305.032470, 10.902971), new Vector3(0.000000, 0.000000, 360.000000), 0, 0, player, 200, 200);
            fk106.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, -56.142932, 1305.032470, 10.622963, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk107 = new DynamicObject(3279, new Vector3(-56.142932, 1305.032470, 10.622963), new Vector3(0.000000, 0.000000, -90.000000), 0, 0, player, 200, 200);
            fk107.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, -371.263000, 1272.294189, 23.312990, 0.000000, 0.000000, 360.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk108 = new DynamicObject(3279, new Vector3(-371.263000, 1272.294189, 23.312990), new Vector3(0.000000, 0.000000, 360.000000), 0, 0, player, 200, 200);
            fk108.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(12991, -381.001556, 1267.727294, 23.648094, 0.000000, 0.000000, -76.899948, object_world, object_int, -1, 300.00, 300.00); 
            var fk109 = new DynamicObject(12991, new Vector3(-381.001556, 1267.727294, 23.648094), new Vector3(0.000000, 0.000000, -76.899948), 0, 0, player, 200, 200);
            fk109.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, 173.302993, 1130.456787, 13.462953, 0.000000, 0.000000, 161.800018, object_world, object_int, -1, 300.00, 300.00); 
            var fk110 = new DynamicObject(3279, new Vector3(173.302993, 1130.456787, 13.462953), new Vector3(0.000000, 0.000000, 161.800018), 0, 0, player, 200, 200);
            fk110.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1437, 96.145240, 1173.430541, 18.434040, -20.000003, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk111 = new DynamicObject(1437, new Vector3(96.145240, 1173.430541, 18.434040), new Vector3(-20.000003, 0.000000, -90.000000), 0, 0, player, 200, 200);
            fk111.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1431, 103.847793, 1177.935791, 23.004253, 0.000000, 0.000000, 99.299987, object_world, object_int, -1, 300.00, 300.00); 
            var fk112 = new DynamicObject(1431, new Vector3(103.847793, 1177.935791, 23.004253), new Vector3(0.000000, 0.000000, 99.299987), 0, 0, player, 200, 200);
            fk112.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1431, 104.093673, 1175.803710, 23.004253, 0.000000, 0.000000, 90.200004, object_world, object_int, -1, 300.00, 300.00); 
            var fk113 = new DynamicObject(1431, new Vector3(104.093673, 1175.803710, 23.004253), new Vector3(0.000000, 0.000000, 90.200004), 0, 0, player, 200, 200);
            fk113.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2985, 103.234451, 1176.754272, 22.429550, 0.000000, 0.000000, 9.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk114 = new DynamicObject(2985, new Vector3(103.234451, 1176.754272, 22.429550), new Vector3(0.000000, 0.000000, 9.000000), 0, 0, player, 200, 200);
            fk114.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2973, 104.437965, 1170.842407, 22.424585, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk115 = new DynamicObject(2973, new Vector3(104.437965, 1170.842407, 22.424585), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk115.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2359, 102.877006, 1178.120727, 22.657268, 0.000000, 0.000000, 95.300010, object_world, object_int, -1, 300.00, 300.00); 
            var fk116 = new DynamicObject(2359, new Vector3(102.877006, 1178.120727, 22.657268), new Vector3(0.000000, 0.000000, 95.300010), 0, 0, player, 200, 200);
            fk116.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2977, 96.367874, 1171.428466, 17.514059, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk117 = new DynamicObject(2977, new Vector3(96.367874, 1171.428466, 17.514059), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk117.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2977, 96.367874, 1170.228149, 17.514059, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk118 = new DynamicObject(2977, new Vector3(96.367874, 1170.228149, 17.514059), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk118.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3015, 102.837356, 1170.074707, 22.591102, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk119 = new DynamicObject(3015, new Vector3(102.837356, 1170.074707, 22.591102), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk119.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3015, 102.837356, 1170.554809, 22.591102, 0.000000, 0.000000, -9.500000, object_world, object_int, -1, 300.00, 300.00); 
            var fk120 = new DynamicObject(3015, new Vector3(102.837356, 1170.554809, 22.591102), new Vector3(0.000000, 0.000000, -9.500000), 0, 0, player, 200, 200);
            fk120.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3014, 102.739761, 1171.062011, 22.680717, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk121 = new DynamicObject(3014, new Vector3(102.739761, 1171.062011, 22.680717), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk121.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1431, 102.671936, 1179.756835, 23.004253, 0.000000, 0.000000, 143.399963, object_world, object_int, -1, 300.00, 300.00); 
            var fk122 = new DynamicObject(1431, new Vector3(102.671936, 1179.756835, 23.004253), new Vector3(0.000000, 0.000000, 143.399963), 0, 0, player, 200, 200);
            fk122.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19996, 101.728736, 1177.355590, 22.491416, 0.000000, 0.000000, 64.999992, object_world, object_int, -1, 300.00, 300.00); 
            var fk123 = new DynamicObject(19996, new Vector3(101.728736, 1177.355590, 22.491416), new Vector3(0.000000, 0.000000, 64.999992), 0, 0, player, 200, 200);
            fk123.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, 294.230133, 1236.629760, 13.882926, 0.000000, 0.000000, -125.899955, object_world, object_int, -1, 300.00, 300.00); 
            var fk124 = new DynamicObject(3279, new Vector3(294.230133, 1236.629760, 13.882926), new Vector3(0.000000, 0.000000, -125.899955), 0, 0, player, 200, 200);
            fk124.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, 272.742156, 1247.566162, 14.562927, 0.000000, 0.000000, -125.899955, object_world, object_int, -1, 300.00, 300.00); 
            var fk125 = new DynamicObject(3279, new Vector3(272.742156, 1247.566162, 14.562927), new Vector3(0.000000, 0.000000, -125.899955), 0, 0, player, 200, 200);
            fk125.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, 208.148681, 976.581604, 27.442966, 0.000000, 0.000000, 108.900070, object_world, object_int, -1, 300.00, 300.00); 
            var fk126 = new DynamicObject(3279, new Vector3(208.148681, 976.581604, 27.442966), new Vector3(0.000000, 0.000000, 108.900070), 0, 0, player, 200, 200);
            fk126.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, 232.312774, 985.868530, 27.102958, 0.000000, 0.000000, 108.900070, object_world, object_int, -1, 300.00, 300.00); 
            var fk127 = new DynamicObject(3279, new Vector3(232.312774, 985.868530, 27.102958), new Vector3(0.000000, 0.000000, 108.900070), 0, 0, player, 200, 200);
            fk127.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, -129.893020, 807.794189, 19.802949, 0.000000, 0.000000, 108.900070, object_world, object_int, -1, 300.00, 300.00); 
            var fk128 = new DynamicObject(3279, new Vector3(-129.893020, 807.794189, 19.802949), new Vector3(0.000000, 0.000000, 108.900070), 0, 0, player, 200, 200);
            fk128.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, -275.602600, 809.060485, 13.773155, -2.099999, 0.000000, 38.500072, object_world, object_int, -1, 300.00, 300.00); 
            var fk129 = new DynamicObject(3279, new Vector3(-275.602600, 809.060485, 13.773155), new Vector3(-2.099999, 0.000000, 38.500072), 0, 0, player, 200, 200);
            fk129.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, -398.146850, 994.450561, 9.602944, 0.000000, 0.000000, -37.999885, object_world, object_int, -1, 300.00, 300.00); 
            var fk130 = new DynamicObject(3279, new Vector3(-398.146850, 994.450561, 9.602944), new Vector3(0.000000, 0.000000, -37.999885), 0, 0, player, 200, 200);
            fk130.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19966, -445.321228, 1218.676391, 28.736557, -4.399999, 6.500000, -96.800018, object_world, object_int, -1, 300.00, 300.00); 
            var fk131 = new DynamicObject(19966, new Vector3(-445.321228, 1218.676391, 28.736557), new Vector3(-4.399999, 6.500000, -96.800018), 0, 0, player, 200, 200);
            fk131.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(8615, -433.956665, 1230.297729, 32.151775, 0.000000, 0.000000, -168.099960, object_world, object_int, -1, 300.00, 300.00); 
            var fk132 = new DynamicObject(8615, new Vector3(-433.956665, 1230.297729, 32.151775), new Vector3(0.000000, 0.000000, -168.099960), 0, 0, player, 200, 200);
            fk132.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2985, -436.756530, 1223.901855, 33.837959, 0.000000, 0.000000, -166.399932, object_world, object_int, -1, 300.00, 300.00); 
            var fk133 = new DynamicObject(2985, new Vector3(-436.756530, 1223.901855, 33.837959), new Vector3(0.000000, 0.000000, -166.399932), 0, 0, player, 200, 200);
            fk133.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2060, -437.351257, 1223.039794, 33.977897, 0.000000, 0.000000, -45.200000, object_world, object_int, -1, 300.00, 300.00); 
            var fk134 = new DynamicObject(2060, new Vector3(-437.351257, 1223.039794, 33.977897), new Vector3(0.000000, 0.000000, -45.200000), 0, 0, player, 200, 200);
            fk134.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2060, -437.595275, 1224.205200, 33.957901, 0.000000, 0.000000, -103.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk135 = new DynamicObject(2060, new Vector3(-437.595275, 1224.205200, 33.957901), new Vector3(0.000000, 0.000000, -103.000000), 0, 0, player, 200, 200);
            fk135.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2060, -437.611145, 1224.136840, 34.187862, 0.000000, 0.000000, -103.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk136 = new DynamicObject(2060, new Vector3(-437.611145, 1224.136840, 34.187862), new Vector3(0.000000, 0.000000, -103.000000), 0, 0, player, 200, 200);
            fk136.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2060, -437.407653, 1223.096435, 34.237854, 0.000000, 0.000000, -45.200000, object_world, object_int, -1, 300.00, 300.00); 
            var fk137 = new DynamicObject(2060, new Vector3(-437.407653, 1223.096435, 34.237854), new Vector3(0.000000, 0.000000, -45.200000), 0, 0, player, 200, 200);
            fk137.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2060, -437.713409, 1223.598144, 34.447822, 0.000000, 0.000000, -79.600028, object_world, object_int, -1, 300.00, 300.00); 
            var fk138 = new DynamicObject(2060, new Vector3(-437.713409, 1223.598144, 34.447822), new Vector3(0.000000, 0.000000, -79.600028), 0, 0, player, 200, 200);
            fk138.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3585, -421.725006, 1231.690673, 31.116085, -5.900001, 0.000000, 9.599995, object_world, object_int, -1, 300.00, 300.00); 
            var fk139 = new DynamicObject(3585, new Vector3(-421.725006, 1231.690673, 31.116085), new Vector3(-5.900001, 0.000000, 9.599995), 0, 0, player, 200, 200);
            fk139.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3585, -413.564331, 1232.900756, 31.344280, -3.900000, -0.699998, 6.799995, object_world, object_int, -1, 300.00, 300.00); 
            var fk140 = new DynamicObject(3585, new Vector3(-413.564331, 1232.900756, 31.344280), new Vector3(-3.900000, -0.699998, 6.799995), 0, 0, player, 200, 200);
            fk140.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3633, -434.771697, 1220.246704, 34.281646, 0.000000, 0.000000, 15.000007, object_world, object_int, -1, 300.00, 300.00); 
            var fk141 = new DynamicObject(3633, new Vector3(-434.771697, 1220.246704, 34.281646), new Vector3(0.000000, 0.000000, 15.000007), 0, 0, player, 200, 200);
            fk141.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3593, -482.617584, 1216.961425, 29.047889, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk142 = new DynamicObject(3593, new Vector3(-482.617584, 1216.961425, 29.047889), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk142.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3788, -429.329010, 1221.959472, 29.896795, 0.000000, 0.000000, 11.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk143 = new DynamicObject(3788, new Vector3(-429.329010, 1221.959472, 29.896795), new Vector3(0.000000, 0.000000, 11.000000), 0, 0, player, 200, 200);
            fk143.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2041, -434.532012, 1220.602416, 34.965194, 0.000000, 0.000000, -31.400003, object_world, object_int, -1, 300.00, 300.00); 
            var fk144 = new DynamicObject(2041, new Vector3(-434.532012, 1220.602416, 34.965194), new Vector3(0.000000, 0.000000, -31.400003), 0, 0, player, 200, 200);
            fk144.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2619, -435.697326, 1219.263427, 35.640365, -3.300004, 0.000000, -79.699951, object_world, object_int, -1, 300.00, 300.00); 
            var fk145 = new DynamicObject(2619, new Vector3(-435.697326, 1219.263427, 35.640365), new Vector3(-3.300004, 0.000000, -79.699951), 0, 0, player, 200, 200);
            fk145.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2358, -437.496948, 1225.206176, 33.982406, 0.000000, 0.000000, 135.499923, object_world, object_int, -1, 300.00, 300.00); 
            var fk146 = new DynamicObject(2358, new Vector3(-437.496948, 1225.206176, 33.982406), new Vector3(0.000000, 0.000000, 135.499923), 0, 0, player, 200, 200);
            fk146.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19641, -145.505035, 1108.478027, 18.749988, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk147 = new DynamicObject(19641, new Vector3(-145.505035, 1108.478027, 18.749988), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk147.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19641, -137.505004, 1108.478027, 18.749988, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk148 = new DynamicObject(19641, new Vector3(-137.505004, 1108.478027, 18.749988), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk148.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19641, -129.504943, 1108.478027, 18.749988, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk149 = new DynamicObject(19641, new Vector3(-129.504943, 1108.478027, 18.749988), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk149.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19641, -121.505065, 1108.478027, 18.749988, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk150 = new DynamicObject(19641, new Vector3(-121.505065, 1108.478027, 18.749988), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk150.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19641, -113.505088, 1108.478027, 18.749988, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk151 = new DynamicObject(19641, new Vector3(-113.505088, 1108.478027, 18.749988), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk151.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19641, -109.685111, 1112.296875, 18.749988, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk152 = new DynamicObject(19641, new Vector3(-109.685111, 1112.296875, 18.749988), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk152.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19641, -109.685111, 1120.295776, 18.749988, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk153 = new DynamicObject(19641, new Vector3(-109.685111, 1120.295776, 18.749988), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk153.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19641, -109.685111, 1128.295776, 18.749988, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk154 = new DynamicObject(19641, new Vector3(-109.685111, 1128.295776, 18.749988), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk154.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(8674, -150.538604, 1113.586791, 22.000013, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk155 = new DynamicObject(8674, new Vector3(-150.538604, 1113.586791, 22.000013), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk155.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(8674, -150.538604, 1123.937133, 22.000013, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk156 = new DynamicObject(8674, new Vector3(-150.538604, 1123.937133, 22.000013), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk156.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(8674, -150.538604, 1131.647338, 22.000013, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk157 = new DynamicObject(8674, new Vector3(-150.538604, 1131.647338, 22.000013), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk157.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19641, -113.575134, 1132.186889, 18.749988, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk158 = new DynamicObject(19641, new Vector3(-113.575134, 1132.186889, 18.749988), new Vector3(0.000000, 0.000000, 180.000000), 0, 0, player, 200, 200);
            fk158.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19641, -121.575126, 1132.186889, 18.749988, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk159 = new DynamicObject(19641, new Vector3(-121.575126, 1132.186889, 18.749988), new Vector3(0.000000, 0.000000, 180.000000), 0, 0, player, 200, 200);
            fk159.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19641, -129.575134, 1132.186889, 18.749988, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk160 = new DynamicObject(19641, new Vector3(-129.575134, 1132.186889, 18.749988), new Vector3(0.000000, 0.000000, 180.000000), 0, 0, player, 200, 200);
            fk160.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, -145.829360, 1130.541137, 18.649986, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk161 = new DynamicObject(3279, new Vector3(-145.829360, 1130.541137, 18.649986), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk161.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19641, -146.525085, 1136.707763, 18.749988, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk162 = new DynamicObject(19641, new Vector3(-146.525085, 1136.707763, 18.749988), new Vector3(0.000000, 0.000000, 180.000000), 0, 0, player, 200, 200);
            fk162.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19912, -132.262512, 1131.557617, 20.342159, 0.000000, 0.000000, -27.100027, object_world, object_int, -1, 300.00, 300.00); 
            var fk163 = new DynamicObject(19912, new Vector3(-132.262512, 1131.557617, 20.342159), new Vector3(0.000000, 0.000000, -27.100027), 0, 0, player, 200, 200);
            fk163.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(16095, -129.823028, 1127.316772, 18.739988, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk164 = new DynamicObject(16095, new Vector3(-129.823028, 1127.316772, 18.739988), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk164.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1697, -129.623046, 1127.234130, 23.799987, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk165 = new DynamicObject(1697, new Vector3(-129.623046, 1127.234130, 23.799987), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk165.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19909, -116.976196, 1116.400634, 18.749971, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk166 = new DynamicObject(19909, new Vector3(-116.976196, 1116.400634, 18.749971), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk166.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2634, -122.862106, 1116.398803, 19.959980, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk167 = new DynamicObject(2634, new Vector3(-122.862106, 1116.398803, 19.959980), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk167.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1697, -114.963096, 1119.844238, 25.179985, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk168 = new DynamicObject(1697, new Vector3(-114.963096, 1119.844238, 25.179985), new Vector3(0.000000, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk168.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3279, -114.469375, 1127.069091, 18.649986, 0.000000, 0.000000, -180.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk169 = new DynamicObject(3279, new Vector3(-114.469375, 1127.069091, 18.649986), new Vector3(0.000000, 0.000000, -180.000000), 0, 0, player, 200, 200);
            fk169.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3173, -223.104812, 1054.487792, 18.662185, 0.000000, 0.000000, -179.200103, object_world, object_int, -1, 300.00, 300.00); 
            var fk170 = new DynamicObject(3173, new Vector3(-223.104812, 1054.487792, 18.662185), new Vector3(0.000000, 0.000000, -179.200103), 0, 0, player, 200, 200);
            fk170.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3171, -234.733489, 1040.640258, 18.702186, 0.000000, 0.000000, -163.399993, object_world, object_int, -1, 300.00, 300.00); 
            var fk171 = new DynamicObject(3171, new Vector3(-234.733489, 1040.640258, 18.702186), new Vector3(0.000000, 0.000000, -163.399993), 0, 0, player, 200, 200);
            fk171.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19632, -229.703536, 1045.759765, 18.744390, 0.000000, 0.000000, 54.399997, object_world, object_int, -1, 300.00, 300.00); 
            var fk172 = new DynamicObject(19632, new Vector3(-229.703536, 1045.759765, 18.744390), new Vector3(0.000000, 0.000000, 54.399997), 0, 0, player, 200, 200);
            fk172.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1358, -221.183593, 1045.108764, 19.942193, 0.000000, 0.000000, 37.899993, object_world, object_int, -1, 300.00, 300.00); 
            var fk173 = new DynamicObject(1358, new Vector3(-221.183593, 1045.108764, 19.942193), new Vector3(0.000000, 0.000000, 37.899993), 0, 0, player, 200, 200);
            fk173.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(12957, -234.719238, 1058.412475, 19.402202, 0.000000, 0.000000, 27.000009, object_world, object_int, -1, 300.00, 300.00); 
            var fk174 = new DynamicObject(12957, new Vector3(-234.719238, 1058.412475, 19.402202), new Vector3(0.000000, 0.000000, 27.000009), 0, 0, player, 200, 200);
            fk174.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1334, -235.815216, 1046.485717, 19.552198, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk175 = new DynamicObject(1334, new Vector3(-235.815216, 1046.485717, 19.552198), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk175.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(3005, -230.595062, 1047.780395, 18.742187, 0.000000, 0.000000, -38.199996, object_world, object_int, -1, 300.00, 300.00); 
            var fk176 = new DynamicObject(3005, new Vector3(-230.595062, 1047.780395, 18.742187), new Vector3(0.000000, 0.000000, -38.199996), 0, 0, player, 200, 200);
            fk176.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(18862, -221.315933, 1044.479736, 18.792165, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk177 = new DynamicObject(18862, new Vector3(-221.315933, 1044.479736, 18.792165), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk177.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1415, -226.201004, 1060.501464, 18.742187, 0.000000, 0.000000, -48.299995, object_world, object_int, -1, 300.00, 300.00); 
            var fk178 = new DynamicObject(1415, new Vector3(-226.201004, 1060.501464, 18.742187), new Vector3(0.000000, 0.000000, -48.299995), 0, 0, player, 200, 200);
            fk178.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(2674, -227.133834, 1047.771240, 18.762187, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk179 = new DynamicObject(2674, new Vector3(-227.133834, 1047.771240, 18.762187), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk179.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1793, -232.665802, 1057.979248, 18.712186, 0.000000, 0.000000, -52.399978, object_world, object_int, -1, 300.00, 300.00); 
            var fk180 = new DynamicObject(1793, new Vector3(-232.665802, 1057.979248, 18.712186), new Vector3(0.000000, 0.000000, -52.399978), 0, 0, player, 200, 200);
            fk180.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19573, -224.934127, 1050.061523, 18.758342, -5.899999, 0.000000, -89.399971, object_world, object_int, -1, 300.00, 300.00); 
            var fk181 = new DynamicObject(19573, new Vector3(-224.934127, 1050.061523, 18.758342), new Vector3(-5.899999, 0.000000, -89.399971), 0, 0, player, 200, 200);
            fk181.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(18663, -213.790100, 1065.782714, 24.396249, 3.599999, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk182 = new DynamicObject(18663, new Vector3(-213.790100, 1065.782714, 24.396249), new Vector3(3.599999, 0.000000, 90.000000), 0, 0, player, 200, 200);
            fk182.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1457, -225.685302, 1067.539916, 20.382207, 0.000000, 0.000000, -177.400009, object_world, object_int, -1, 300.00, 300.00); 
            var fk183 = new DynamicObject(1457, new Vector3(-225.685302, 1067.539916, 20.382207), new Vector3(0.000000, 0.000000, -177.400009), 0, 0, player, 200, 200);
            fk183.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1793, -227.856887, 1067.403930, 18.382179, 0.000000, 0.000000, -87.999946, object_world, object_int, -1, 300.00, 300.00); 
            var fk184 = new DynamicObject(1793, new Vector3(-227.856887, 1067.403930, 18.382179), new Vector3(0.000000, 0.000000, -87.999946), 0, 0, player, 200, 200);
            fk184.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1438, -228.462478, 1068.048828, 18.722187, 0.000000, 0.000000, 124.100013, object_world, object_int, -1, 300.00, 300.00); 
            var fk185 = new DynamicObject(1438, new Vector3(-228.462478, 1068.048828, 18.722187), new Vector3(0.000000, 0.000000, 124.100013), 0, 0, player, 200, 200);
            fk185.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1370, -223.796768, 1067.992309, 19.272199, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk186 = new DynamicObject(1370, new Vector3(-223.796768, 1067.992309, 19.272199), new Vector3(0.000000, 0.000000, 0.000000), 0, 0, player, 200, 200);
            fk186.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1462, -219.683837, 1072.614746, 18.732187, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00); 
            var fk187 = new DynamicObject(1462, new Vector3(-219.683837, 1072.614746, 18.732187), new Vector3(0.000000, 0.000000, -90.000000), 0, 0, player, 200, 200);
            fk187.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(849, -223.431808, 1071.410400, 19.002193, 0.000000, 0.000000, -35.500000, object_world, object_int, -1, 300.00, 300.00); 
            var fk188 = new DynamicObject(849, new Vector3(-223.431808, 1071.410400, 19.002193), new Vector3(0.000000, 0.000000, -35.500000), 0, 0, player, 200, 200);
            fk188.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(1812, -230.462188, 1068.958251, 18.732187, 0.000000, 0.000000, 81.500007, object_world, object_int, -1, 300.00, 300.00); 
            var fk189 = new DynamicObject(1812, new Vector3(-230.462188, 1068.958251, 18.732187), new Vector3(0.000000, 0.000000, 81.500007), 0, 0, player, 200, 200);
            fk189.ShowInWorld(0);
            //tmpobjid = CreateDynamicObject(19306, -225.753463, 1069.229370, 21.957248, 0.000000, 14.900001, 82.700004, object_world, object_int, -1, 300.00, 300.00); 
            var fk190 = new DynamicObject(19306, new Vector3(-225.753463, 1069.229370, 21.957248), new Vector3(0.000000, 14.900001, 82.700004), 0, 0, player, 200, 200);
            fk190.ShowInWorld(0);

        }
        private static void TimeUpdate()
        {
            
            var minutesTimer = new System.Timers.Timer(3500);
            minutesTimer.Elapsed += OnTimedEvent;
            minutesTimer.Enabled = true;
            minutesTimer.AutoReset = true;
             void OnTimedEvent(object source, ElapsedEventArgs e)
            {
                minutes ++;
                
                if (minutes == 60)
                {
                    minutes = 0;
                    hours++;
                    if (hours == 24)
                    {
                        hours = 0;
                        minutes = 0;
                    }
                }
                if (minutes < 10 && hours < 10)
                {
                    TimePNG.Text = $"0{hours}:0{minutes}";
                }
                else if (hours >= 10 && minutes < 10)
                {
                    TimePNG.Text = $"{hours}:0{minutes}";
                }
                else if (hours < 10 && minutes >= 10)
                {
                    TimePNG.Text = $"0{hours}:{minutes}";
                }
                else if (hours >= 10 && minutes >= 10)
                {
                    TimePNG.Text = $"{hours}:{minutes}";
                }
                
                Server.SetWorldTime(hours);
                
                
                return;
            }
            
            
        }
        protected override void OnPlayerUpdate(BasePlayer player, PlayerUpdateEventArgs e)
        {
            if (((Player)player).isAutorised == true)
            {
                TimePNG.Show(player);
            }
                base.OnPlayerUpdate(player, e);
                AttackCurrentPlayer(player);
            


        }

        private static Vector3 GetPositionInFront(Vector3 pos, float angle, float distance)
        {
            float x = pos.X;
            float y = pos.Y;
            float z = pos.Z;
            float a = (angle + 90) * (float)(PI / 180d);

            x += distance * (float)Cos(a);
            y += distance * (float)Sin(a);
            return new Vector3(x, y, z);
        }
        protected override void OnPlayerSpawned(BasePlayer player, SpawnEventArgs e)
        {
            GangZoneCreate(player);
            base.OnPlayerSpawned(player, e);
            

        }
        public void Object()
        {
            //SHOPFLOOR
            //tmpobjid = CreateDynamicObject(19379, 1330.095947, 1358.404785, 3000.029541, 0.000000, 90.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00); 
            //SetDynamicObjectMaterial(tmpobjid, 0, 18065, "ab_sfammumain", "gun_floor2", 0x00000000);
            GlobalObject ShopFloor = new GlobalObject(19379, new Vector3(1330.095947, 1358.404785, 3000.029541), new Vector3(00.000000, 90.000000, 0.000000), 25.0f);
            ShopFloor.SetMaterial(0, 18065, "ab_sfammumain", "gun_floor2", 0);
            //
            //METRO MAPP HERE MA BOYS 
            //CreateDynamicObject(1219, 809.394836, -1337.105347, 13.752862, 90.000000, 180.000000, 0.000000, -1, -1);
            GlobalObject vdnhp1 = new GlobalObject(1219, new Vector3(809.394836, -1337.105347, 13.752862), new Vector3(90.000000, 180.000000, 0.000000), 300.0f);
            //CreateDynamicObject(1219, 810.665222, -1337.105347, 13.752862, 90.000000, 180.000000, 0.000000, -1, -1);
            GlobalObject vdnhp2 = new GlobalObject(1219, new Vector3(810.665222, -1337.105347, 13.752862), new Vector3(90.000000, 180.000000, 0.000000), 300.0f);
            //CreateDynamicObject(19869, 811.019775, -1337.477295, 12.466875, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp3 = new GlobalObject(19869, new Vector3(811.019775, -1337.477295, 12.466875), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //CreateDynamicObject(1219, 813.405884, -1337.105347, 13.752862, 90.000000, 180.000000, 0.000000, -1, -1);
            GlobalObject vdnhp4 = new GlobalObject(1219, new Vector3(813.405884, -1337.105347, 13.752862), new Vector3(90.000000, 180.000000, 0.000000), 300.0f);
            //CreateDynamicObject(1219, 808.114136, -1338.623901, 13.752862, 90.000000, 180.000000, 90.000000, -1, -1);
            GlobalObject vdnhp5 = new GlobalObject(1219, new Vector3(808.114136, -1338.623901, 13.752862), new Vector3(90.000000, 180.000000, 90.000000), 300.0f);
            // CreateDynamicObject(1219, 815.085693, -1337.052490, 13.752862, 90.000000, 180.000000, 0.000000, -1, -1);
            GlobalObject vdnhp6 = new GlobalObject(1219, new Vector3(815.085693, -1337.052490, 13.752862), new Vector3(90.000000, 180.000000, 00.000000), 300.0f);
            //fso_map = CreateDynamicObject(19362, 811.086304, -1338.399170, 17.897688, 0.000000, 0.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterialText(fso_map, 0, "{ff0000}Х", 10, "Consolas", 0, 1, 0xFFFFFFFFFFFF0000, 0, 1);
            GlobalObject vdnhp7 = new GlobalObject(19362, new Vector3(811.086304, -1338.399170, 17.897688), new Vector3(00.000000, 00.000000, 90.000000), 300.0f);
            vdnhp7.SetMaterialText(0, "{ff0000}X", ObjectMaterialSize.X32X32, "Consolas", 0, true, new SampSharp.GameMode.SAMP.Color(256f, 0f, 0f), 0, ObjectMaterialTextAlign.Center);
            //CreateDynamicObject(1219, 808.765442, -1339.550903, 13.759748, 90.000000, 180.000000, -90.399940, -1, -1);
            GlobalObject vdnhp8 = new GlobalObject(1219, new Vector3(808.765442, -1339.550903, 13.759748), new Vector3(90.000000, 180.000000, -90.000000), 300.0f);
            //SetDynamicObjectMaterial(fso_map, 0, 18777, "TunnelSections", "stonewall4", 0);
            //fso_map = CreateDynamicObject(19455, 813.319458, -1338.412598, 17.719076, 0.000000, 0.000000, 90.000000, -1, -1);
            GlobalObject vdnhp9 = new GlobalObject(19455, new Vector3(813.319458, -1338.412598, 17.719076), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            vdnhp9.SetMaterial(0, 18777, "TunnelSections", "stonewall4", 0);

            //fso_map = CreateDynamicObject(19362, 813.786194, -1338.399170, 17.897688, 0.000000, 0.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterialText(fso_map, 0, "{ff0000}Н", 10, "Consolas", 0, 1, 0xFFFFFFFFFFFF0000, 0, 1);
            GlobalObject vdnhp10 = new GlobalObject(19362, new Vector3(813.786194, -1338.399170, 17.897688), new Vector3(00.000000, 00.000000, 90.000000), 300.0f);
            vdnhp10.SetMaterialText(0, "{ff0000}Н", ObjectMaterialSize.X32X32, "Consolas", 0, true, new SampSharp.GameMode.SAMP.Color(256f, 0f, 0f), 0, ObjectMaterialTextAlign.Center);
            //CreateDynamicObject(19869, 808.369141, -1340.059448, 12.466879, 0.000000, 0.000000, 90.000000, -1, -1);
            GlobalObject vdnhp11 = new GlobalObject(19869, new Vector3(808.369141, -1340.059448, 12.466879), new Vector3(00.000000, 00.000000, 90.000000), 300.0f);
            //CreateDynamicObject(1219, 817.915649, -1337.092529, 13.752862, 90.000000, 180.000000, 0.000000, -1, -1);
            GlobalObject vdnhp12 = new GlobalObject(1219, new Vector3(817.915649, -1337.092529, 13.752862), new Vector3(90.000000, 180.000000, 0.000000), 300.0f);
            //CreateDynamicObject(1219, 817.565796, -1337.703125, 13.752862, 90.000000, 180.000000, 0.000000, -1, -1);
            GlobalObject vdnhp13 = new GlobalObject(1219, new Vector3(817.565796, -1337.703125, 13.752862), new Vector3(90.000000, 180.000000, 0.000000), 300.0f);
            //CreateDynamicObject(19869, 818.032471, -1337.477295, 12.466875, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp14 = new GlobalObject(19869, new Vector3(818.032471, -1337.477295, 12.466875), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //fso_map = CreateDynamicObject(19362, 816.536438, -1338.399170, 17.897688, 0.000000, 0.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterialText(fso_map, 0, "{ff0000}Д", 10, "Consolas", 0, 1, 0xFFFFFFFFFFFF0000, 0, 1);
            GlobalObject vdnhp15 = new GlobalObject(19362, new Vector3(816.536438, -1338.399170, 17.897688), new Vector3(00.000000, 00.000000, 90.000000), 300.0f);
            vdnhp15.SetMaterialText(0, "{ff0000}Д", ObjectMaterialSize.X32X32, "Consolas", 0, true, new SampSharp.GameMode.SAMP.Color(256f, 0f, 0f), 0, ObjectMaterialTextAlign.Center);
            //fso_map = CreateDynamicObject(19362, 808.585144, -1340.900269, 17.897688, 0.000000, 0.000000, 180.000000, -1, -1);
            //SetDynamicObjectMaterialText(fso_map, 0, "{ff0000}В", 10, "Consolas", 0, 1, 0xFFFFFFFFFFFF0000, 0, 1);
            GlobalObject vdnhp16 = new GlobalObject(19362, new Vector3(808.585144, -1340.900269, 17.897688), new Vector3(00.000000, 00.000000, 180.000000), 300.0f);
            vdnhp16.SetMaterialText(0, "{ff0000}В", ObjectMaterialSize.X32X32, "Consolas", 0, true, new SampSharp.GameMode.SAMP.Color(256f, 0f, 0f), 0, ObjectMaterialTextAlign.Center);
            //CreateDynamicObject(1219, 808.114136, -1341.364502, 13.752862, 90.000000, 180.000000, 90.000000, -1, -1);
            GlobalObject vdnhp17 = new GlobalObject(1219, new Vector3(808.114136, -1341.364502, 13.752862), new Vector3(90.000000, 180.000000, 90.000000), 300.0f);

            //CreateDynamicObject(19869, 819.791931, -1337.477295, 12.466875, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp18 = new GlobalObject(19869, new Vector3(819.791931, -1337.477295, 12.466875), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

            //CreateDynamicObject(1219, 820.665466, -1337.092529, 13.752862, 90.000000, 180.000000, 0.000000, -1, -1);
            GlobalObject vdnhp19 = new GlobalObject(1219, new Vector3(820.665466, -1337.092529, 13.752862), new Vector3(90.000000, 180.000000, 0.000000), 300.0f);

            //CreateDynamicObject(1219, 808.746338, -1342.280518, 13.759748, 90.000000, 180.000000, -90.399940, -1, -1);
            GlobalObject vdnhp20 = new GlobalObject(1219, new Vector3(808.746338, -1342.280518, 13.759748), new Vector3(90.000000, 180.000000, -90.000000), 300.0f);

            //CreateDynamicObject(1219, 820.415649, -1337.703125, 13.752862, 90.000000, 180.000000, 0.000000, -1, -1);
            GlobalObject vdnhp21 = new GlobalObject(1219, new Vector3(820.415649, -1337.703125, 13.752862), new Vector3(90.000000, 180.000000, 0.000000), 300.0f);
            //fso_map = CreateDynamicObject(19362, 819.126282, -1338.399170, 17.897688, 0.000000, 0.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterialText(fso_map, 0, "{ff0000}В", 10, "Consolas", 0, 1, 0xFFFFFFFFFFFF0000, 0, 1);
            GlobalObject vdnhp22 = new GlobalObject(19362, new Vector3(819.126282, -1338.399170, 17.897688), new Vector3(00.000000, 00.000000, 90.000000), 300.0f);
            vdnhp22.SetMaterialText(0, "{ff0000}В", ObjectMaterialSize.X32X32, "Consolas", 0, true, new SampSharp.GameMode.SAMP.Color(256f, 0f, 0f), 0, ObjectMaterialTextAlign.Center);
            //fso_map = CreateDynamicObject(3092, 821.239746, -1337.375244, 17.547911, 0.000000, 0.000000, 0.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 1, 1243, "buoy", "buoyrust_128", 0);
            GlobalObject vdnhp23 = new GlobalObject(3092, new Vector3(821.239746, -1337.375244, 17.547911), new Vector3(0.00000f, 0.00000f, 00.0f), 300.0f);
            vdnhp23.SetMaterial(1, 1243, "lsv1", "lsv1", 0);
            //SetDynamicObjectMaterial(fso_map, 0, 3945, "alpha_fence", "bistro_alpha", 0);
            vdnhp23.SetMaterial(0, 3945, "alpha_fence", "bistro_alpha", 0);
            //fso_map = CreateDynamicObject(19362, 808.585144, -1343.201172, 17.897688, 0.000000, 0.000000, 180.000000, -1, -1);
            //SetDynamicObjectMaterialText(fso_map, 0, "{ff0000}Д", 10, "Consolas", 0, 1, 0xFFFFFFFFFFFF0000, 0, 1);
            GlobalObject vdnhp24 = new GlobalObject(19362, new Vector3(808.585144, -1343.201172, 17.897688), new Vector3(00.000000, 00.000000, 180.000000), 300.0f);
            vdnhp24.SetMaterialText(0, "{ff0000}Д", ObjectMaterialSize.X32X32, "Consolas", 0, true, new SampSharp.GameMode.SAMP.Color(256f, 0f, 0f), 0, ObjectMaterialTextAlign.Center);
            //fso_map = CreateDynamicObject(19455, 808.588989, -1343.263184, 17.719076, 0.000000, 0.000000, 180.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 18777, "TunnelSections", "stonewall4", 0);
            GlobalObject vdnhp25 = new GlobalObject(19455, new Vector3(808.588989, -1343.263184, 17.719076), new Vector3(0.00000f, 0.00000f, 180.0f), 300.0f);
            vdnhp25.SetMaterial(0, 18777, "TunnelSections", "stonewall4", 0);
            // CreateDynamicObject(1362, 798.959534, -1332.036499, -0.927812, 0.000000, 0.000000, -35.200005, -1, -1);
            GlobalObject vdnhp26 = new GlobalObject(1362, new Vector3(798.959534, -1332.036499, -0.927812), new Vector3(0.000000, 0.000000, -35.200005), 300.0f);

            // CreateDynamicObject(19088, 821.234314, -1337.606323, 20.673393, 0.000000, 0.000000, 179.899979, -1, -1);
            GlobalObject vdnhp27 = new GlobalObject(19088, new Vector3(821.234314, -1337.606323, 20.673393), new Vector3(0.000000, 0.000000, 179.899979), 300.0f);

            // CreateDynamicObject(1219, 823.515808, -1337.092529, 13.752862, 90.000000, 180.000000, 0.000000, -1, -1);
            GlobalObject vdnhp28 = new GlobalObject(1219, new Vector3(823.515808, -1337.092529, 13.752862), new Vector3(90.000000, 180.000000, 0.000000), 300.0f);

            // CreateDynamicObject(1219, 808.114136, -1344.135010, 13.752862, 90.000000, 180.000000, 90.000000, -1, -1);
            GlobalObject vdnhp29 = new GlobalObject(1219, new Vector3(808.114136, -1344.135010, 13.752862), new Vector3(90.000000, 180.000000, 90.000000), 300.0f);

            // CreateDynamicObject(1219, 823.225464, -1337.703125, 13.752862, 90.000000, 180.000000, 0.000000, -1, -1);
            GlobalObject vdnhp30 = new GlobalObject(1219, new Vector3(823.225464, -1337.703125, 13.752862), new Vector3(90.000000, 180.000000, 0.000000), 300.0f);

            // CreateDynamicObject(18688, 798.828125, -1332.099976, -1.807812, 0.000000, 0.000000, -41.099995, -1, -1);
            GlobalObject vdnhp31 = new GlobalObject(1219, new Vector3(798.828125, -1332.099976, -1.807812), new Vector3(90.000000, 180.000000, -41.099995), 300.0f);
            //fso_map = CreateDynamicObject(19455, 822.919434, -1338.412598, 17.719076, 0.000000, 0.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 18777, "TunnelSections", "stonewall4", 0);
            GlobalObject vdnhp32 = new GlobalObject(19455, new Vector3(822.919434, -1338.412598, 17.719076), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            vdnhp32.SetMaterial(0, 18777, "TunnelSections", "stonewall4", 0);

            //CreateDynamicObject(1219, 808.727539, -1344.970215, 13.759748, 90.000000, 180.000000, -90.399940, -1, -1);
            GlobalObject vdnhp33 = new GlobalObject(1219, new Vector3(808.727539, -1344.970215, 13.759748), new Vector3(90.000000, 180.000000, -90.399940), 300.0f);
            //CreateDynamicObject(19869, 808.369141, -1345.239258, 12.466879, 0.000000, 0.000000, 90.000000, -1, -1);
            GlobalObject vdnhp34 = new GlobalObject(19869, new Vector3(808.369141, -1345.239258, 12.466879), new Vector3(0.000000, 0.000000, 90.000000), 300.0f);
            // CreateDynamicObject(19869, 824.963562, -1337.477051, 12.466875, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp35 = new GlobalObject(19869, new Vector3(824.963562, -1337.477051, 12.466875), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            // CreateDynamicObject(1362, 807.096313, -1339.546753, -0.937812, 0.000000, 0.000000, -43.300007, -1, -1);
            GlobalObject vdnhp36 = new GlobalObject(1362, new Vector3(807.096313, -1339.546753, -0.937812), new Vector3(0.000000, 0.000000, -43.300007), 300.0f);

            // CreateDynamicObject(1219, 826.285950, -1337.092529, 13.752862, 90.000000, 180.000000, 0.000000, -1, -1);
            GlobalObject vdnhp37 = new GlobalObject(1219, new Vector3(826.285950, -1337.092529, 13.752862), new Vector3(90.000000, 180.000000, 0.000000), 300.0f);

            // CreateDynamicObject(1219, 826.065735, -1337.703125, 13.752862, 90.000000, 180.000000, 0.000000, -1, -1);
            GlobalObject vdnhp38 = new GlobalObject(1219, new Vector3(826.065735, -1337.703125, 13.752862), new Vector3(90.000000, 180.000000, 0.000000), 300.0f);

            // CreateDynamicObject(18688, 806.948853, -1339.472534, -1.847811, 0.000000, 0.000000, -44.399998, -1, -1);
            GlobalObject vdnhp39 = new GlobalObject(1219, new Vector3(806.948853, -1339.472534, -1.847811), new Vector3(0.000000, 0.000000, -44.399998), 300.0f);
            //fso_map = CreateDynamicObject(19362, 808.585144, -1346.480835, 17.897688, 0.000000, 0.000000, 180.000000, -1, -1);
            //SetDynamicObjectMaterialText(fso_map, 0, "{ff0000}Н", 10, "Consolas", 0, 1, 0xFFFFFFFFFFFF0000, 0, 1);
            GlobalObject vdnhp40 = new GlobalObject(19362, new Vector3(808.585144, -1346.480835, 17.897688), new Vector3(00.000000, 00.000000, 180.000000), 300.0f);
            vdnhp40.SetMaterialText(0, "{ff0000}Н", ObjectMaterialSize.X32X32, "Consolas", 0, true, new SampSharp.GameMode.SAMP.Color(256f, 0f, 0f), 0, ObjectMaterialTextAlign.Center);
            //fso_map = CreateDynamicObject(19362, 808.585144, -1349.780396, 17.897688, 0.000000, 0.000000, 180.000000, -1, -1);
            // SetDynamicObjectMaterialText(fso_map, 0, "{ff0000}Х", 10, "Consolas", 0, 1, 0xFFFFFFFFFFFF0000, 0, 1);
            GlobalObject vdnhp41 = new GlobalObject(19362, new Vector3(808.585144, -1349.780396, 17.897688), new Vector3(00.000000, 00.000000, 180.000000), 300.0f);
            vdnhp41.SetMaterialText(0, "{ff0000}Х", ObjectMaterialSize.X32X32, "Consolas", 0, true, new SampSharp.GameMode.SAMP.Color(256f, 0f, 0f), 0, ObjectMaterialTextAlign.Center);
            //CreateDynamicObject(1219, 808.114136, -1346.885132, 13.752862, 90.000000, 180.000000, 90.000000, -1, -1);
            GlobalObject vdnhp42 = new GlobalObject(1219, new Vector3(808.114136, -1346.885132, 13.752862), new Vector3(90.000000, 180.000000, 90.000000), 300.0f);

            //CreateDynamicObject(1440, 785.643127, -1317.135986, 12.892807, 0.000000, 0.000000, 127.899963, -1, -1);
            GlobalObject vdnhp43 = new GlobalObject(1440, new Vector3(785.643127, -1317.135986, 12.892807), new Vector3(0.000000, 0.000000, 127.899963), 300.0f);

            //CreateDynamicObject(1219, 808.708923, -1347.661255, 13.759748, 90.000000, 180.000000, -90.399940, -1, -1);
            GlobalObject vdnhp44 = new GlobalObject(1219, new Vector3(808.708923, -1347.661255, 13.759748), new Vector3(90.000000, 180.000000, -90.399940), 300.0f);

            //CreateDynamicObject(1219, 808.114136, -1349.634888, 13.752862, 90.000000, 180.000000, 90.000000, -1, -1);
            GlobalObject vdnhp45 = new GlobalObject(1219, new Vector3(808.114136, -1349.634888, 13.752862), new Vector3(90.000000, 180.000000, 90.000000), 300.0f);
            //CreateDynamicObject(1219, 808.690186, -1350.351685, 13.759748, 90.000000, 180.000000, -90.399940, -1, -1);
            GlobalObject vdnhp46 = new GlobalObject(1219, new Vector3(808.690186, -1350.351685, 13.759748), new Vector3(90.000000, 180.000000, -90.399940), 300.0f);

            //CreateDynamicObject(19869, 808.369141, -1350.410645, 12.466879, 0.000000, 0.000000, 90.000000, -1, -1);
            GlobalObject vdnhp47 = new GlobalObject(19869, new Vector3(808.369141, -1350.410645, 12.466879), new Vector3(0.000000, 0.000000, 90.000000), 300.0f);
            //CreateDynamicObject(1219, 808.114136, -1352.374756, 13.752862, 90.000000, 180.000000, 90.000000, -1, -1);
            GlobalObject vdnhp48 = new GlobalObject(1219, new Vector3(808.114136, -1352.374756, 13.752862), new Vector3(90.000000, 180.000000, 90.000000), 300.0f);

            //CreateDynamicObject(2840, 825.163757, -1347.352783, 12.501474, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp49 = new GlobalObject(1219, new Vector3(825.163757, -1347.352783, 12.501474), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //CreateDynamicObject(1219, 808.671387, -1353.041748, 13.759748, 90.000000, 180.000000, -90.399940, -1, -1);
            GlobalObject vdnhp50 = new GlobalObject(1219, new Vector3(808.671387, -1353.041748, 13.759748), new Vector3(90.000000, 180.000000, -90.399940), 300.0f);
            //fso_map = CreateDynamicObject(19455, 808.588989, -1352.893188, 17.719076, 0.000000, 0.000000, 180.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 18777, "TunnelSections", "stonewall4", 0);
            GlobalObject vdnhp51 = new GlobalObject(19455, new Vector3(808.588989, -1352.893188, 17.719076), new Vector3(0.00000f, 0.00000f, 180.0f), 300.0f);
            vdnhp51.SetMaterial(0, 18777, "TunnelSections", "stonewall4", 0);
            //CreateDynamicObject(2671, 822.852112, -1349.351074, 12.537049, 0.000000, 0.000000, -28.500002, -1, -1);
            GlobalObject vdnhp52 = new GlobalObject(2671, new Vector3(822.852112, -1349.351074, 12.537049), new Vector3(0.000000, 0.000000, -28.500002), 300.0f);

            //CreateDynamicObject(1440, 817.400208, -1352.201172, 12.992450, 0.000000, 0.000000, -125.099976, -1, -1);
            GlobalObject vdnhp53 = new GlobalObject(1440, new Vector3(817.400208, -1352.201172, 12.992450), new Vector3(0.000000, 0.000000, -125.099976), 300.0f);

            // CreateDynamicObject(1362, 827.076843, -1347.560669, 13.126866, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp54 = new GlobalObject(1362, new Vector3(827.076843, -1347.560669, 13.126866), new Vector3(0.000000, 0.000000, 0.0), 300.0f);

            // CreateDynamicObject(1219, 808.114136, -1355.065308, 13.752862, 90.000000, 180.000000, 90.000000, -1, -1);
            GlobalObject vdnhp55 = new GlobalObject(1219, new Vector3(808.114136, -1355.065308, 13.752862), new Vector3(90.000000, 180.000000, 90.000000), 300.0f);

            // CreateDynamicObject(849, 821.047913, -1352.279541, 12.789195, 0.000000, 0.000000, -56.599995, -1, -1);
            GlobalObject vdnhp56 = new GlobalObject(849, new Vector3(821.047913, -1352.279541, 12.789195), new Vector3(0.000000, 0.000000, -56.599995), 300.0f);//

            // CreateDynamicObject(1219, 808.677429, -1355.632813, 13.759748, 90.000000, 180.000000, -88.099976, -1, -1);
            GlobalObject vdnhp57 = new GlobalObject(1219, new Vector3(808.677429, -1355.632813, 13.759748), new Vector3(90.000000, 180.000000, -88.099976), 300.0f);

            // CreateDynamicObject(19869, 808.369141, -1355.582520, 12.466879, 0.000000, 0.000000, 90.000000, -1, -1);
            GlobalObject vdnhp58 = new GlobalObject(19869, new Vector3(808.369141, -1355.582520, 12.466879), new Vector3(0.000000, 0.000000, 90.000000), 300.0f);

            // CreateDynamicObject(3594, 840.170471, -1323.998535, 12.992815, 0.000000, 0.000000, -65.099983, -1, -1);
            GlobalObject vdnhp59 = new GlobalObject(3594, new Vector3(840.170471, -1323.998535, 12.992815), new Vector3(0.000000, 0.000000, -65.099983), 300.0f);

            // CreateDynamicObject(3564, 785.869751, -1340.842285, -0.881461, 0.000000, 0.000000, -41.399990, -1, -1);
            GlobalObject vdnhp60 = new GlobalObject(3564, new Vector3(785.869751, -1340.842285, -0.881461), new Vector3(0.000000, 0.000000, -41.399990), 300.0f);

            // CreateDynamicObject(3564, 794.012817, -1348.152344, -0.881461, 0.000000, 0.000000, -42.799976, -1, -1);
            GlobalObject vdnhp61 = new GlobalObject(3564, new Vector3(794.012817, -1348.152344, -0.881461), new Vector3(0.000000, 0.000000, -42.799976), 300.0f);

            //  CreateDynamicObject(3564, 776.920837, -1331.066162, -0.945018, 0.000000, 0.000000, 47.099972, -1, -1);
            GlobalObject vdnhp62 = new GlobalObject(3564, new Vector3(776.920837, -1331.066162, -0.945018), new Vector3(0.000000, 0.000000, 47.099972), 300.0f);

            // CreateDynamicObject(1362, 822.361450, -1353.078613, -0.937812, 0.000000, 0.000000, -32.200008, -1, -1);
            GlobalObject vdnhp63 = new GlobalObject(1362, new Vector3(822.361450, -1353.078613, -0.937812), new Vector3(0.000000, 0.000000, -32.200008), 300.0f);

            //  CreateDynamicObject(18688, 822.173889, -1353.079712, -1.801461, 0.000000, 0.000000, -44.299999, -1, -1);
            GlobalObject vdnhp64 = new GlobalObject(18688, new Vector3(822.173889, -1353.079712, -1.801461), new Vector3(0.000000, 0.000000, -44.299999), 300.0f);

            //  CreateDynamicObject(3564, 804.736206, -1357.443237, -0.881461, 0.000000, 0.000000, -42.799976, -1, -1);
            GlobalObject vdnhp65 = new GlobalObject(3564, new Vector3(804.736206, -1357.443237, -0.881461), new Vector3(0.000000, 0.000000, -42.799976), 300.0f);

            //  CreateDynamicObject(3564, 773.915771, -1334.370117, -0.937812, 0.000000, 0.000000, 47.799992, -1, -1);
            GlobalObject vdnhp66 = new GlobalObject(3564, new Vector3(773.915771, -1334.370117, -0.937812), new Vector3(0.000000, 0.000000, 47.799992), 300.0f);

            //  CreateDynamicObject(3564, 813.225403, -1365.305298, -0.881461, 0.000000, 0.000000, -42.799976, -1, -1);
            GlobalObject vdnhp67 = new GlobalObject(3564, new Vector3(813.225403, -1365.305298, -0.881461), new Vector3(0.000000, 0.000000, -42.799976), 300.0f);

            //  CreateDynamicObject(1362, 832.833923, -1362.559204, -0.911461, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp68 = new GlobalObject(1362, new Vector3(832.833923, -1362.559204, -0.911461), new Vector3(0.000000, 0.000000, 0.0), 300.0f);

            //  CreateDynamicObject(18688, 832.657166, -1362.567871, -1.711460, 0.000000, 0.000000, -39.799999, -1, -1);
            GlobalObject vdnhp69 = new GlobalObject(18688, new Vector3(832.657166, -1362.567871, -1.711460), new Vector3(0.000000, 0.000000, -39.799999), 300.0f);

            //  CreateDynamicObject(3564, 820.797485, -1372.317627, -0.881461, 0.000000, 0.000000, -42.799976, -1, -1);
            GlobalObject vdnhp70 = new GlobalObject(3564, new Vector3(820.797485, -1372.317627, -0.881461), new Vector3(0.000000, 0.000000, -42.799976), 300.0f);

            //  CreateDynamicObject(1362, 841.491211, -1370.560913, -0.921461, 0.000000, 0.000000, -38.099991, -1, -1);
            GlobalObject vdnhp71 = new GlobalObject(1362, new Vector3(841.491211, -1370.560913, -0.921461), new Vector3(0.000000, 0.000000, -38.099991), 300.0f);

            // CreateDynamicObject(18688, 841.317261, -1370.639282, -1.811460, 0.000000, 0.000000, -39.899998, -1, -1);
            GlobalObject vdnhp72 = new GlobalObject(18688, new Vector3(841.317261, -1370.639282, -1.811460), new Vector3(0.000000, 0.000000, -39.899998), 300.0f);

            // CreateDynamicObject(1440, 842.652039, -1372.261597, -1.061460, 0.000000, 0.000000, -40.800003, -1, -1);
            GlobalObject vdnhp73 = new GlobalObject(1440, new Vector3(842.652039, -1372.261597, -1.061460), new Vector3(0.000000, 0.000000, -40.800003), 300.0f);

            //  CreateDynamicObject(3564, 828.985840, -1379.900635, -0.881461, 0.000000, 0.000000, -42.799976, -1, -1);
            GlobalObject vdnhp74 = new GlobalObject(3564, new Vector3(828.985840, -1379.900635, -0.881461), new Vector3(0.000000, 0.000000, -42.799976), 300.0f);

            // CreateDynamicObject(1449, 846.555908, -1375.013306, -0.961461, 0.000000, 0.000000, -39.500004, -1, -1);
            GlobalObject vdnhp75 = new GlobalObject(1449, new Vector3(846.555908, -1375.013306, -0.961461), new Vector3(0.000000, 0.000000, -39.500004), 300.0f);

            //  CreateDynamicObject(1431, 840.134705, -1382.089600, -1.041460, 0.000000, 0.000000, -41.099995, -1, -1);
            GlobalObject vdnhp76 = new GlobalObject(1431, new Vector3(840.134705, -1382.089600, -1.041460), new Vector3(0.000000, 0.000000, -41.099995), 300.0f);
            //ВАЛЕРА

            //  CreateDynamicObject(1440, 876.842224, -1320.755249, 13.107939, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp77 = new GlobalObject(1440, new Vector3(876.842224, -1320.755249, 13.107939), new Vector3(0.000000, 0.000000, 0.000000), 300.0f); //

            //  CreateDynamicObject(1440, 849.266968, -1377.974121, -1.061460, 0.000000, 0.000000, -40.800003, -1, -1);
            GlobalObject vdnhp78 = new GlobalObject(1440, new Vector3(849.266968, -1377.974121, -1.061460), new Vector3(0.000000, 0.000000, -40.800003), 300.0f); //

            //  CreateDynamicObject(3564, 836.947266, -1387.273071, -0.881461, 0.000000, 0.000000, -42.799976, -1, -1);
            GlobalObject vdnhp79 = new GlobalObject(3564, new Vector3(836.947266, -1387.273071, -0.881461), new Vector3(0.000000, 0.000000, -42.799976), 300.0f); //

            //  CreateDynamicObject(1362, 852.549255, -1381.164063, -0.991461, 0.000000, 0.000000, -43.600002, -1, -1);
            GlobalObject vdnhp80 = new GlobalObject(1362, new Vector3(852.549255, -1381.164063, -0.991461), new Vector3(0.000000, 0.000000, -43.600002), 300.0f); //

            //  CreateDynamicObject(18688, 852.405579, -1381.203247, -1.791461, 0.000000, 0.000000, -36.900002, -1, -1);
            GlobalObject vdnhp81 = new GlobalObject(18688, new Vector3(852.405579, -1381.203247, -1.791461), new Vector3(0.000000, 0.000000, -36.900002), 300.0f); //

            //  CreateDynamicObject(955, 854.516357, -1382.729858, -1.141461, 0.000000, 0.000000, -42.599964, -1, -1);
            GlobalObject vdnhp82 = new GlobalObject(955, new Vector3(854.516357, -1382.729858, -1.141461), new Vector3(0.000000, 0.000000, -42.599964), 300.0f); //

            //  CreateDynamicObject(955, 855.407288, -1383.547974, -1.141461, 0.000000, 0.000000, -42.599964, -1, -1);
            GlobalObject vdnhp83 = new GlobalObject(955, new Vector3(855.407288, -1383.547974, -1.141461), new Vector3(0.000000, 0.000000, -42.599964), 300.0f); //

            // CreateDynamicObject(1220, 849.973022, -1388.678101, -0.928211, 0.000000, 26.999996, 43.299999, -1, -1);
            GlobalObject vdnhp84 = new GlobalObject(1220, new Vector3(849.973022, -1388.678101, -0.928211), new Vector3(0.000000, 26.999996, 43.299999), 300.0f); //

            // CreateDynamicObject(1220, 849.630737, -1389.536499, -0.501461, 0.000000, 26.999996, 43.299999, -1, -1);
            GlobalObject vdnhp85 = new GlobalObject(1220, new Vector3(849.630737, -1389.536499, -0.501461), new Vector3(0.000000, 26.999996, 43.299999), 300.0f); //

            // CreateDynamicObject(1458, 849.201416, -1389.912476, -1.291460, 0.000000, 0.000000, -45.700012, -1, -1);
            GlobalObject vdnhp86 = new GlobalObject(1458, new Vector3(849.201416, -1389.912476, -1.291460), new Vector3(0.000000, 0.000000, -45.700012), 300.0f); //

            //  CreateDynamicObject(1431, 848.823242, -1390.217529, -1.051460, 0.000000, 0.000000, -42.600006, -1, -1);
            GlobalObject vdnhp87 = new GlobalObject(1431, new Vector3(848.823242, -1390.217529, -1.051460), new Vector3(0.000000, 0.000000, -42.600006), 300.0f); //

            //  CreateDynamicObject(3564, 843.433594, -1393.279175, -0.881461, 0.000000, 0.000000, -42.799976, -1, -1);
            GlobalObject vdnhp88 = new GlobalObject(3564, new Vector3(843.433594, -1393.279175, -0.881461), new Vector3(0.000000, 0.000000, -42.799976), 300.0f); //

            //  CreateDynamicObject(1221, 851.232971, -1390.490601, -1.111461, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp89 = new GlobalObject(1221, new Vector3(851.232971, -1390.490601, -1.111461), new Vector3(0.000000, 0.000000, 0.000000), 300.0f); //

            //  CreateDynamicObject(1221, 851.823120, -1390.860962, -0.221460, 0.000000, 0.000000, 65.200005, -1, -1);
            GlobalObject vdnhp90 = new GlobalObject(1221, new Vector3(851.823120, -1390.860962, -0.221460), new Vector3(0.000000, 0.000000, 65.200005), 300.0f); //

            //  CreateDynamicObject(1221, 852.350952, -1390.687744, -1.111461, 0.000000, 0.000000, -75.199982, -1, -1);
            GlobalObject vdnhp91 = new GlobalObject(1221, new Vector3(852.350952, -1390.687744, -1.111461), new Vector3(0.000000, 0.000000, -75.199982), 300.0f); //

            //  CreateDynamicObject(1221, 851.232971, -1391.580811, -1.111461, 0.000000, 0.000000, 39.300007, -1, -1);
            GlobalObject vdnhp92 = new GlobalObject(1221, new Vector3(851.232971, -1391.580811, -1.111461), new Vector3(0.000000, 0.000000, 39.300007), 300.0f); //

            // CreateDynamicObject(3564, 848.172852, -1400.000977, -1.101461, 90.000000, -155.300064, 0.000000, -1, -1);
            GlobalObject vdnhp93 = new GlobalObject(3564, new Vector3(848.172852, -1400.000977, -1.101461), new Vector3(90.000000, -155.300064, 0.000000), 300.0f); //

            // CreateDynamicObject(1271, 855.426208, -1389.060547, -1.281461, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp94 = new GlobalObject(1271, new Vector3(855.426208, -1389.060547, -1.281461), new Vector3(0.000000, 0.000000, 0.000000), 300.0f); //

            // CreateDynamicObject(1271, 855.426208, -1389.581055, -0.611460, 0.000000, 0.000000, -8.299999, -1, -1);
            GlobalObject vdnhp95 = new GlobalObject(1271, new Vector3(855.426208, -1389.581055, -0.611460), new Vector3(0.000000, 0.000000, -8.299999), 300.0f); //

            //   CreateDynamicObject(1271, 855.109985, -1390.097412, -1.281461, 0.000000, 0.000000, -39.700005, -1, -1);
            GlobalObject vdnhp96 = new GlobalObject(1271, new Vector3(855.109985, -1390.097412, -1.281461), new Vector3(0.000000, 0.000000, -39.700005), 300.0f);//

            //  CreateDynamicObject(1271, 854.649658, -1390.614258, -0.641461, 0.000000, 0.000000, 34.099998, -1, -1);
            GlobalObject vdnhp97 = new GlobalObject(1271, new Vector3(854.649658, -1390.614258, -0.641461), new Vector3(0.000000, 0.000000, 34.099998), 300.0f);//

            // CreateDynamicObject(1271, 854.185730, -1390.928711, -1.281461, 0.000000, 0.000000, -23.599998, -1, -1);
            GlobalObject vdnhp98 = new GlobalObject(1271, new Vector3(854.185730, -1390.928711, -1.281461), new Vector3(0.000000, 0.000000, -23.599998), 300.0f); //

            //  CreateDynamicObject(3014, 854.724365, -1389.407349, -1.301460, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp99 = new GlobalObject(3014, new Vector3(854.724365, -1389.407349, -1.301460), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);//

            //  CreateDynamicObject(3014, 854.303955, -1390.228149, -1.301460, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp100 = new GlobalObject(3014, new Vector3(854.303955, -1390.228149, -1.301460), new Vector3(0.000000, 0.000000, 0.000000), 300.0f); //

            //  CreateDynamicObject(3014, 855.084167, -1390.168091, -0.701461, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject vdnhp101 = new GlobalObject(3014, new Vector3(855.084167, -1390.168091, -0.701461), new Vector3(0.000000, 0.000000, 0.000000), 300.0f); //


            //METRO END

              //ТЕРАААААА
            //CreateDynamicObject(874, -305.799988, 2215.830078, 48.529999, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv1 = new GlobalObject(874, new Vector3(-305.799988, 2215.830078, 48.529999), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

            
//CreateDynamicObject(735, -306.049988, 2217.770020, 47.060001, 0.000000, 0.000000, 173.039993, -1, -1);
GlobalObject terrv2 = new GlobalObject(735, new Vector3(-306.049988, 2217.770020, 47.060001), new Vector3(0.000000, 0.000000, 173.039993), 300.0f);

//CreateDynamicObject(874, -313.019989, 2217.399902, 46.549999, 0.000000, 0.000000, 0.000000, -1, -1); 
GlobalObject terrv3 = new GlobalObject(874, new Vector3(-313.019989, 2217.399902, 46.549999), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
//CreateDynamicObject(874, -314.200012, 2217.959961, 44.490002, 0.000000, 0.000000, 262.029999, -1, -1);
GlobalObject terrv4 = new GlobalObject(874, new Vector3(-314.200012, 2217.959961, 44.490002), new Vector3(0.000000, 0.000000, 262.029999), 300.0f);
//CreateDynamicObject(874, -319.790009, 2218.229980, 42.930000, 0.000000, 0.000000, 262.029999, -1, -1);
GlobalObject terrv5 = new GlobalObject(874, new Vector3(-319.790009, 2218.229980, 42.930000), new Vector3(0.000000, 0.000000, 262.029999), 300.0f);
//CreateDynamicObject(1412, -347.743286, 2200.257080, 41.520000, 90.000000, 0.000000, 46.060001, -1, -1);
GlobalObject terrv6 = new GlobalObject(1412, new Vector3(-347.743286, 2200.257080, 41.520000), new Vector3(90.000000, 0.000000, 46.060001), 300.0f);
//CreateDynamicObject(1412, -351.858704, 2196.270020, 42.708000, -10.000000, 0.000000, 23.340000, -1, -1);
GlobalObject terrv7 = new GlobalObject(1412, new Vector3(-351.858704, 2196.270020, 42.708000), new Vector3(-10.000000, 0.000000, 23.340000), 300.0f);
//CreateDynamicObject(874, -331.250000, 2220.830078, 41.459999, 0.000000, 0.000000, 221.899994, -1, -1);
GlobalObject terrv8 = new GlobalObject(874, new Vector3(-331.250000, 2220.830078, 41.459999), new Vector3(0.000000, 0.000000, 221.899994), 300.0f);
//CreateDynamicObject(874, -355.369995, 2195.830078, 41.330002, 0.000000, 0.000000, 78.389999, -1, -1);
GlobalObject terrv9 = new GlobalObject(874, new Vector3(-355.369995, 2195.830078, 41.330002), new Vector3(0.000000, 0.000000, 78.389999), 300.0f);
//CreateDynamicObject(1414, -356.762115, 2194.503418, 42.653999, 0.000000, 0.000000, 12.300000, -1, -1);
GlobalObject terrv10 = new GlobalObject(1414, new Vector3(-356.762115, 2194.503418, 42.653999), new Vector3(0.000000, 0.000000, 12.300000), 300.0f);
//CreateDynamicObject(1412, -356.783966, 2194.593750, 42.708000, 0.000000, 0.000000, 192.119934, -1, -1);
GlobalObject terrv11 = new GlobalObject(1412, new Vector3(-356.783966, 2194.593750, 42.708000), new Vector3(0.000000, 0.000000, 192.119934), 300.0f);
//CreateDynamicObject(874, -344.391296, 2208.942627, 41.213001, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv12 = new GlobalObject(874, new Vector3(-344.391296, 2208.942627, 41.213001), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
//CreateDynamicObject(3593, -364.517761, 2188.029053, 41.853901, 0.000000, 0.000000, 194.160004, -1, -1);
GlobalObject terrv13 = new GlobalObject(3593, new Vector3(-364.517761, 2188.029053, 41.853901), new Vector3(0.000000, 0.000000, 194.160004), 300.0f);
//CreateDynamicObject(1447, -361.874786, 2193.363770, 42.653999, 0.000000, 0.000000, 13.260000, -1, -1);
GlobalObject terrv14 = new GlobalObject(1447, new Vector3(-361.874786, 2193.363770, 42.653999), new Vector3(0.000000, 0.000000, 13.260000), 300.0f);
//CreateDynamicObject(874, -368.554077, 2186.473633, 40.489399, 0.000000, 0.000000, 1.800000, -1, -1);
GlobalObject terrv15 = new GlobalObject(874, new Vector3(-368.554077, 2186.473633, 40.489399), new Vector3(0.000000, 0.000000, 1.800000), 300.0f);
//CreateDynamicObject(1412, -366.995697, 2192.278564, 42.653999, 0.000000, -2.000000, 12.120000, -1, -1);
GlobalObject terrv16 = new GlobalObject(1412, new Vector3(-366.995697, 2192.278564, 42.653999), new Vector3(0.000000, -2.000000, 12.120000), 300.0f);
//CreateDynamicObject(773, -415.470001, 2096.000000, 60.150002, 0.000000, 0.000000, 282.489990, -1, -1);
GlobalObject terrv17 = new GlobalObject(773, new Vector3(-415.470001, 2096.000000, 60.150002), new Vector3(0.000000, 0.000000, 282.489990), 300.0f);
//CreateDynamicObject(1218, -358.206512, 2205.001465, 41.901798, 0.000000, 90.000000, -13.080000, -1, -1);
GlobalObject terrv18 = new GlobalObject(1218, new Vector3(-358.206512, 2205.001465, 41.901798), new Vector3(0.000000, 90.000000, -13.080000), 300.0f);
//CreateDynamicObject(1218, -359.373047, 2204.390625, 41.939800, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv19 = new GlobalObject(1218, new Vector3(-359.373047, 2204.390625, 41.939800), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
//CreateDynamicObject(967, -370.336914, 2192.258789, 41.090000, 6.000000, 0.000000, 283.859802, -1, -1);
GlobalObject terrv20 = new GlobalObject(967, new Vector3(-370.336914, 2192.258789, 41.090000), new Vector3(6.000000, 0.000000, 283.859802), 300.0f);
//CreateDynamicObject(1225, -360.908508, 2204.772949, 41.901100, 0.000000, 0.000000, 89.160004, -1, -1);
GlobalObject terrv21 = new GlobalObject(1225, new Vector3(-360.908508, 2204.772949, 41.901100), new Vector3(0.000000, 0.000000, 89.160004), 300.0f);
//CreateDynamicObject(2673, -370.518402, 2193.555664, 41.231400, 0.000000, 0.000000, 160.619904, -1, -1);
GlobalObject terrv22 = new GlobalObject(2673, new Vector3(-370.518402, 2193.555664, 41.231400), new Vector3(0.000000, 0.000000, 160.619904), 300.0f);
//CreateDynamicObject(2675, -376.265991, 2186.112305, 41.300400, 0.000000, 0.000000, -9.360000, -1, -1);
GlobalObject terrv23 = new GlobalObject(2675, new Vector3(-376.265991, 2186.112305, 41.300400), new Vector3(0.000000, 0.000000, -9.360000), 300.0f);
//CreateDynamicObject(970, -373.059692, 2191.350098, 41.545502, 0.000000, -1.500000, 14.700000, -1, -1);
GlobalObject terrv24 = new GlobalObject(970, new Vector3(-373.059692, 2191.350098, 41.545502), new Vector3(0.000000, -1.500000, 14.700000), 300.0f);
//CreateDynamicObject(1327, -377.230072, 2188.766113, 41.088001, 0.000000, 90.000000, 0.000000, -1, -1);
GlobalObject terrv25 = new GlobalObject(1327, new Vector3(-377.230072, 2188.766113, 41.088001), new Vector3(0.000000, 90.000000, 0.000000), 300.0f);
//CreateDynamicObject(967, -375.871307, 2191.016602, 40.874001, 0.000000, 0.000000, 102.960007, -1, -1);
GlobalObject terrv26 = new GlobalObject(967, new Vector3(-375.871307, 2191.016602, 40.874001), new Vector3(0.000000, 0.000000, 102.960007), 300.0f);
//CreateDynamicObject(2674, -375.173828, 2193.604004, 41.080200, 0.000000, 0.000000, 76.320000, -1, -1);
GlobalObject terrv27 = new GlobalObject(2674, new Vector3(-375.173828, 2193.604004, 41.080200), new Vector3(0.000000, 0.000000, 76.320000), 300.0f);
//CreateDynamicObject(2673, -376.213196, 2192.956299, 41.231400, 0.000000, 0.000000, 65.580002, -1, -1);
GlobalObject terrv28 = new GlobalObject(2673, new Vector3(-376.213196, 2192.956299, 41.231400), new Vector3(0.000000, 0.000000, 65.580002), 300.0f);
//CreateDynamicObject(3594, -382.734222, 2184.116699, 41.783401, 0.000000, 0.000000, 12.300000, -1, -1);
GlobalObject terrv29 = new GlobalObject(3594, new Vector3(-382.734222, 2184.116699, 41.783401), new Vector3(0.000000, 0.000000, 12.300000), 300.0f);
//CreateDynamicObject(1412, -379.014648, 2189.656982, 42.546001, 0.000000, -2.000000, 191.760101, -1, -1);
GlobalObject terrv30 = new GlobalObject(1412, new Vector3(-379.014648, 2189.656982, 42.546001), new Vector3(0.000000, -2.000000, 191.760101), 300.0f);
//CreateDynamicObject(1430, -364.550323, 2209.770752, 41.863899, 0.000000, 90.000000, 144.479996, -1, -1);
GlobalObject terrv31 = new GlobalObject(1430, new Vector3(-364.550323, 2209.770752, 41.863899), new Vector3(0.000000, 90.000000, 144.479996), 300.0f);
//CreateDynamicObject(1412, -342.448090, 2234.420898, 41.520000, 90.000000, 0.000000, -77.839996, -1, -1);
GlobalObject terrv32 = new GlobalObject(1412, new Vector3(-342.448090, 2234.420898, 41.520000), new Vector3(90.000000, 0.000000, -77.839996), 300.0f);
//CreateDynamicObject(874, -374.959991, 2198.459961, 40.770000, 0.000000, 0.000000, 94.379997, -1, -1);
GlobalObject terrv33 = new GlobalObject(874, new Vector3(-374.959991, 2198.459961, 40.770000), new Vector3(0.000000, 0.000000, 94.379997), 300.0f);
//CreateDynamicObject(2672, -366.309875, 2210.710693, 41.797600, 0.000000, 0.000000, -64.260002, -1, -1);
GlobalObject terrv34 = new GlobalObject(2672, new Vector3(-366.309875, 2210.710693, 41.797600), new Vector3(0.000000, 0.000000, -64.260002), 300.0f);
//CreateDynamicObject(2905, -356.995209, 2222.128418, 49.986401, -10.000000, 90.000000, -105.239998, -1, -1);
GlobalObject terrv35 = new GlobalObject(2905, new Vector3(-356.995209, 2222.128418, 49.986401), new Vector3(-10.000000, 90.000000, -105.239998), 300.0f);
//CreateDynamicObject(1412, -384.136383, 2188.589844, 42.653999, 0.000000, 0.000000, 191.760101, -1, -1);
GlobalObject terrv36 = new GlobalObject(1412, new Vector3(-384.136383, 2188.589844, 42.653999), new Vector3(0.000000, 0.000000, 191.760101), 300.0f);
//CreateDynamicObject(2906, -357.302948, 2222.147949, 50.428699, -60.000000, 0.000000, -135.600067, -1, -1);
GlobalObject terrv37 = new GlobalObject(2906, new Vector3(-357.302948, 2222.147949, 50.428699), new Vector3(-60.000000, 0.000000, -135.600067), 300.0f);
//CreateDynamicObject(2905, -357.047119, 2222.547119, 49.880402, -30.000000, 90.000000, -68.879997, -1, -1);
GlobalObject terrv38 = new GlobalObject(2905, new Vector3(-357.047119, 2222.547119, 49.880402), new Vector3(-30.000000, 90.000000, -68.879997), 300.0f);
//CreateDynamicObject(1517, -357.102509, 2222.588379, 50.253502, -10.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv39 = new GlobalObject(1517, new Vector3(-357.102509, 2222.588379, 50.253502), new Vector3(-10.000000, 0.000000, 0.000000), 300.0f);
//CreateDynamicObject(2908, -357.293396, 2222.334473, 50.926300, -90.000000, 180.000000, 0.000000, -1, -1);
GlobalObject terrv40 = new GlobalObject(2908, new Vector3(-357.293396, 2222.334473, 50.926300), new Vector3(-90.000000, 180.000000, 0.000000), 300.0f);
//CreateDynamicObject(2907, -357.339203, 2222.368164, 50.381001, -90.000000, 90.000000, 0.000000, -1, -1);
GlobalObject terrv41 = new GlobalObject(2907, new Vector3(-357.339203, 2222.368164, 50.381001), new Vector3(-90.000000, 90.000000, 0.000000), 300.0f);
//CreateDynamicObject(2906, -357.223969, 2222.583984, 50.428699, -60.000000, 180.000000, 297.479889, -1, -1);
GlobalObject terrv42 = new GlobalObject(2906, new Vector3(-357.223969, 2222.583984, 50.428699), new Vector3(-60.000000, 180.000000, 297.479889), 300.0f);
//CreateDynamicObject(3594, -389.026489, 2183.076172, 41.783401, 0.000000, 0.000000, 12.300000, -1, -1);
GlobalObject terrv43 = new GlobalObject(3594, new Vector3(-389.026489, 2183.076172, 41.783401), new Vector3(0.000000, 0.000000, 12.300000), 300.0f);
//CreateDynamicObject(1412, -343.773804, 2240.048828, 42.762001, 10.000000, 2.000000, -56.240009, -1, -1);
GlobalObject terrv44 = new GlobalObject(1412, new Vector3(-343.773804, 2240.048828, 42.762001), new Vector3(10.000000, 2.000000, -56.240009), 300.0f);
//CreateDynamicObject(2676, -360.638428, 2225.512451, 41.625801, 0.000000, 0.000000, -296.399811, -1, -1);
GlobalObject terrv45 = new GlobalObject(2676, new Vector3(-360.638428, 2225.512451, 41.625801), new Vector3(0.000000, 0.000000, -296.399811), 300.0f);
//CreateDynamicObject(1412, -389.256439, 2187.761963, 42.653999, 0.000000, 0.000000, 186.300049, -1, -1);
GlobalObject terrv46 = new GlobalObject(1412, new Vector3(-389.256439, 2187.761963, 42.653999), new Vector3(0.000000, 0.000000, 186.300049), 300.0f);
//CreateDynamicObject(2671, -376.514099, 2207.791992, 41.106602, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv47 = new GlobalObject(2671, new Vector3(-376.514099, 2207.791992, 41.106602), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
//CreateDynamicObject(874, -384.477081, 2198.304932, 40.621201, 0.000000, 0.000000, 72.300003, -1, -1);
GlobalObject terrv48 = new GlobalObject(874, new Vector3(-384.477081, 2198.304932, 40.621201), new Vector3(0.000000, 0.000000, 72.300003), 300.0f);
//CreateDynamicObject(1412, -346.567200, 2244.403809, 42.762001, 0.000000, 2.000000, -58.820000, -1, -1);
GlobalObject terrv49 = new GlobalObject(1412, new Vector3(-346.567200, 2244.403809, 42.762001), new Vector3(0.000000, 2.000000, -58.820000), 300.0f);
//CreateDynamicObject(874, -371.288788, 2218.344238, 41.321098, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv50 = new GlobalObject(874, new Vector3(-371.288788, 2218.344238, 41.321098), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
/*
fso_map = CreateDynamicObject(19993, -382.810913, 2202.516357, 44.651901, 0.000000, 0.000000, 0.000000, -1, -1);
SetDynamicObjectMaterial(fso_map, 0, 1453, "break_farm", "CJ_HAY", 0);
*/
            GlobalObject terrvtexture1 = new GlobalObject(19993, new Vector3(-382.810913, 2202.516357, 44.651901), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
terrvtexture1.SetMaterial(0,1453, "break_farm", "CJ_HAY", 0);
//CreateDynamicObject(1412, -350.751099, 2248.326416, 41.520000, 90.000000, 0.000000, -53.540031, -1, -1);
GlobalObject terrv51 = new GlobalObject(1412, new Vector3(-350.751099, 2248.326416, 41.520000), new Vector3(90.000000, 0.000000, -53.540031), 300.0f);
//CreateDynamicObject(874, -363.053009, 2234.755371, 41.139801, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv52 = new GlobalObject(874, new Vector3(-363.053009, 2234.755371, 41.139801), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
//CreateDynamicObject(1447, -394.608612, 2187.346436, 42.546001, 0.000000, -3.000000, -3.180000, -1, -1);
GlobalObject terrv53 = new GlobalObject(1447, new Vector3(-394.608612, 2187.346436, 42.546001), new Vector3(0.000000, -3.000000, -3.180000), 300.0f);
//CreateDynamicObject(1439, -397.773621, 2186.358154, 40.840199, 0.000000, 0.000000, -2.820000, -1, -1);
GlobalObject terrv54 = new GlobalObject(1439, new Vector3(-397.773621, 2186.358154, 40.840199), new Vector3(0.000000, 0.000000, -2.820000), 300.0f);
//CreateDynamicObject(2671, -399.452209, 2183.674316, 40.832298, 0.000000, 0.000000, 134.399887, -1, -1);
GlobalObject terrv55 = new GlobalObject(2671, new Vector3(-399.452209, 2183.674316, 40.832298), new Vector3(0.000000, 0.000000, 134.399887), 300.0f);
//CreateDynamicObject(3302, -383.240112, 2213.674316, 41.457199, 0.000000, 0.000000, 44.580002, -1, -1);
GlobalObject terrv56 = new GlobalObject(3302, new Vector3(-383.240112, 2213.674316, 41.457199), new Vector3(0.000000, 0.000000, 44.580002), 300.0f);
//CreateDynamicObject(1439, -399.940948, 2186.720459, 40.840199, 0.000000, 0.000000, -12.780000, -1, -1);
GlobalObject terrv57 = new GlobalObject(1439, new Vector3(-399.940948, 2186.720459, 40.840199), new Vector3(0.000000, 0.000000, -12.780000), 300.0f);
//CreateDynamicObject(874, -415.392883, 2151.282959, 43.476330, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv58 = new GlobalObject(874, new Vector3(-415.392883, 2151.282959, 43.476330), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
//CreateDynamicObject(1447, -399.958618, 2187.487305, 42.546001, 0.000000, -3.000000, -11.460000, -1, -1);
GlobalObject terrv59 = new GlobalObject(1447, new Vector3(-399.958618, 2187.487305, 42.546001), new Vector3(0.000000, -3.000000, -11.460000), 300.0f);
//CreateDynamicObject(1412, -352.996094, 2253.884766, 42.762001, 0.000000, 2.000000, -53.060001, -1, -1);
GlobalObject terrv60 = new GlobalObject(1412, new Vector3(-352.996094, 2253.884766, 42.762001), new Vector3(0.000000, 2.000000, -53.060001), 300.0f);
//CreateDynamicObject(1428, -387.165009, 2214.099365, 42.776001, 0.000000, 0.000000, 15.120000, -1, -1);
GlobalObject terrv61 = new GlobalObject(1428, new Vector3(-387.165009, 2214.099365, 42.776001), new Vector3(0.000000, 0.000000, 15.120000), 300.0f);
//CreateDynamicObject(2062, -367.399475, 2241.216064, 41.853699, 0.000000, 90.000000, -19.500019, -1, -1);
GlobalObject terrv62 = new GlobalObject(2062, new Vector3(-367.399475, 2241.216064, 41.853699), new Vector3(0.000000, 90.000000, -19.500019), 300.0f);
//CreateDynamicObject(819, -359.399994, 2251.209961, 40.660000, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv63 = new GlobalObject(819, new Vector3(-359.399994, 2251.209961, 40.660000), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
//CreateDynamicObject(1449, -372.360229, 2236.187988, 41.959702, 0.000000, 0.000000, 12.780000, -1, -1);
GlobalObject terrv64 = new GlobalObject(1449, new Vector3(-372.360229, 2236.187988, 41.959702), new Vector3(0.000000, 0.000000, 12.780000), 300.0f);
//CreateDynamicObject(773, -359.160004, 2252.159912, 40.689999, 0.000000, 0.000000, 282.489990, -1, -1);
GlobalObject terrv65 = new GlobalObject(773, new Vector3(-359.160004, 2252.159912, 40.689999), new Vector3(0.000000, 0.000000, 282.489990), 300.0f);
//CreateDynamicObject(2062, -366.055450, 2244.509277, 41.906700, 0.000000, 90.000000, 71.160004, -1, -1);
GlobalObject terrv66 = new GlobalObject(2062, new Vector3(-366.055450, 2244.509277, 41.906700), new Vector3(0.000000, 90.000000, 71.160004), 300.0f);
//CreateDynamicObject(2062, -368.334137, 2242.154053, 42.012699, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv67 = new GlobalObject(2062, new Vector3(-368.334137, 2242.154053, 42.012699), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
//CreateDynamicObject(2062, -368.518951, 2242.989990, 42.012699, 0.000000, 0.000000, 92.279991, -1, -1);
GlobalObject terrv68 = new GlobalObject(2062, new Vector3(-368.518951, 2242.989990, 42.012699), new Vector3(0.000000, 0.000000, 92.279991), 300.0f);
//CreateDynamicObject(1412, -405.361603, 2187.927246, 42.653999, 10.000000, 0.000000, 179.999908, -1, -1);
GlobalObject terrv69 = new GlobalObject(1412, new Vector3(-405.361603, 2187.927246, 42.653999), new Vector3(10.000000, 0.000000, 179.999908), 300.0f);
//CreateDynamicObject(735, -411.029999, 2178.199951, 37.529999, 0.000000, 0.000000, 173.039993, -1, -1);
GlobalObject terrv70 = new GlobalObject(735, new Vector3(-411.029999, 2178.199951, 37.529999), new Vector3(0.000000, 0.000000, 173.039993), 300.0f);
//CreateDynamicObject(819, -435.299988, 2083.739990, 59.900002, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv71 = new GlobalObject(819, new Vector3(-435.299988, 2083.739990, 59.900002), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
//CreateDynamicObject(819, -411.190002, 2178.570068, 40.660000, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv72 = new GlobalObject(819, new Vector3(-411.190002, 2178.570068, 40.660000), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
//CreateDynamicObject(1413, -355.989197, 2258.170166, 42.938000, 0.000000, -2.000000, -235.139893, -1, -1);
GlobalObject terrv73 = new GlobalObject(1413, new Vector3(-355.989197, 2258.170166, 42.938000), new Vector3(0.000000, -2.000000, -235.139893), 300.0f);
//CreateDynamicObject(2062, -368.995697, 2244.820557, 42.012699, 0.000000, 0.000000, 155.039978, -1, -1);
GlobalObject terrv74 = new GlobalObject(2062, new Vector3(-368.995697, 2244.820557, 42.012699), new Vector3(0.000000, 0.000000, 155.039978), 300.0f);
//CreateDynamicObject(2674, -376.404724, 2237.527100, 41.665699, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrv75 = new GlobalObject(2674, new Vector3(-376.404724, 2237.527100, 41.665699), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
//CreateDynamicObject(1412, -410.342896, 2189.227295, 41.096001, -88.000000, 0.000000, -10.520000, -1, -1);
GlobalObject terrv76 = new GlobalObject(1412, new Vector3(-410.342896, 2189.227295, 41.096001), new Vector3(-88.000000, 0.000000, -10.520000), 300.0f);
//МАЯ ТЕРРИТОРИЯ
//CreateDynamicObject(1413, -359.479187, 2262.132324, 42.883999, 0.000000, 3.000000, -223.380005, -1, -1);
GlobalObject terrz1 = new GlobalObject(1413, new Vector3(-359.479187, 2262.132324, 42.883999), new Vector3(0.000000, 3.000000, -223.380005), 300.0f);

//CreateDynamicObject(11428, -442.880005, 2074.889893, 64.010002, 0.000000, 0.000000, 90.940002, -1, -1);
GlobalObject terrz2 = new GlobalObject(11428, new Vector3(-442.880005, 2074.889893, 64.010002), new Vector3(0.000000, 0.000000, 90.940002), 300.0f);

//CreateDynamicObject(918, -377.998108, 2246.792725, 41.954102, 0.000000, 0.000000, 48.900002, -1, -1);
GlobalObject terrz3 = new GlobalObject(918, new Vector3(-377.998108, 2246.792725, 41.954102), new Vector3(0.000000, 0.000000, 48.900002), 300.0f);

//CreateDynamicObject(874, -408.195190, 2206.438477, 41.164200, 0.000000, 0.000000, 38.340000, -1, -1);
GlobalObject terrz4 = new GlobalObject(874, new Vector3(-408.195190, 2206.438477, 41.164200), new Vector3(0.000000, 0.000000, 38.340000), 300.0f);

//CreateDynamicObject(1412, -417.083588, 2190.853271, 41.202000, -88.000000, 0.000000, 3.520000, -1, -1);
GlobalObject terrz5 = new GlobalObject(1412, new Vector3(-417.083588, 2190.853271, 41.202000), new Vector3(-88.000000, 0.000000, 3.520000), 300.0f);

//CreateDynamicObject(952, -375.787537, 2251.080811, 42.649700, 0.000000, 0.000000, -74.759979, -1, -1);
GlobalObject terrz6 = new GlobalObject(952, new Vector3(-375.787537, 2251.080811, 42.649700), new Vector3(0.000000, 0.000000, -74.759979), 300.0f);

//CreateDynamicObject(1449, -398.089508, 2224.234619, 41.904301, 0.000000, 0.000000, -72.059982, -1, -1);
GlobalObject terrz7 = new GlobalObject(1449, new Vector3(-398.089508, 2224.234619, 41.904301), new Vector3(0.000000, 0.000000, -72.059982), 300.0f);

//CreateDynamicObject(1413, -363.286285, 2265.688721, 42.667999, 0.000000, 3.000000, -222.360001, -1, -1);
GlobalObject terrz8 = new GlobalObject(1413, new Vector3(-363.286285, 2265.688721, 42.667999), new Vector3(0.000000, 3.000000, -222.360001), 300.0f);

//CreateDynamicObject(1462, -398.959229, 2226.407471, 41.428200, 0.000000, 0.000000, -73.079987, -1, -1);
GlobalObject terrz9 = new GlobalObject(1462, new Vector3(-398.959229, 2226.407471, 41.428200), new Vector3(0.000000, 0.000000, -73.079987), 300.0f);

//CreateDynamicObject(819, -451.869995, 2064.949951, 59.580002, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz10 = new GlobalObject(819, new Vector3(-451.869995, 2064.949951, 59.580002), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(3593, -394.901611, 2233.361816, 41.849602, 0.000000, 0.000000, -75.000000, -1, -1);
GlobalObject terrz11 = new GlobalObject(3593, new Vector3(-394.901611, 2233.361816, 41.849602), new Vector3(0.000000, 0.000000, -75.000000), 300.0f);

//CreateDynamicObject(2968, -412.308594, 2210.147461, 41.745602, 0.000000, 0.000000, -22.500000, -1, -1);
GlobalObject terrz12 = new GlobalObject(2968, new Vector3(-412.308594, 2210.147461, 41.745602), new Vector3(0.000000, 0.000000, -22.500000), 300.0f);

//CreateDynamicObject(918, -414.467804, 2206.994141, 41.732700, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz13 = new GlobalObject(918, new Vector3(-414.467804, 2206.994141, 41.732700), new Vector3(0.000000, 0.000000, 0.00), 300.0f);

//CreateDynamicObject(918, -414.884369, 2206.625244, 41.732700, 0.000000, 0.000000, 64.980003, -1, -1);
GlobalObject terrz14 = new GlobalObject(918, new Vector3(-414.884369, 2206.625244, 41.732700), new Vector3(0.000000, 0.000000, 64.980003), 300.0f);

//CreateDynamicObject(1230, -412.653320, 2211.063721, 41.852100, 0.000000, 0.000000, 14.640000, -1, -1);
GlobalObject terrz15 = new GlobalObject(1230, new Vector3(-412.653320, 2211.063721, 41.852100), new Vector3(0.000000, 0.000000, 14.640000), 300.0f);

//CreateDynamicObject(1558, -412.158600, 2212.136719, 42.010300, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz16 = new GlobalObject(1558, new Vector3(-412.158600, 2212.136719, 42.010300), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(874, -405.713196, 2222.884277, 41.309299, 0.000000, 0.000000, 82.379997, -1, -1);
GlobalObject terrz17 = new GlobalObject(874, new Vector3(-405.713196, 2222.884277, 41.309299), new Vector3(0.000000, 0.000000, 82.379997), 300.0f);

//CreateDynamicObject(1413, -367.179810, 2269.259277, 42.397999, 0.000000, 3.000000, -222.360001, -1, -1);
GlobalObject terrz18 = new GlobalObject(1413, new Vector3(-367.179810, 2269.259277, 42.397999), new Vector3(0.000000, 3.000000, -222.360001), 300.0f);

//CreateDynamicObject(2676, -415.589294, 2207.871826, 41.533798, 0.000000, 0.000000, 42.060001, -1, -1);
GlobalObject terrz19 = new GlobalObject(2676, new Vector3(-415.589294, 2207.871826, 41.533798), new Vector3(0.000000, 0.000000, 42.060001), 300.0f);

//CreateDynamicObject(933, -379.023895, 2257.755859, 41.429600, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz20 = new GlobalObject(933, new Vector3(-379.023895, 2257.755859, 41.429600), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(2677, -393.673309, 2241.203613, 41.727501, 0.000000, 0.000000, 80.400002, -1, -1);
GlobalObject terrz21 = new GlobalObject(2677, new Vector3(-393.673309, 2241.203613, 41.727501), new Vector3(0.000000, 0.000000, 80.400002), 300.0f);

//CreateDynamicObject(3403, -416.331604, 2208.938965, 44.175598, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz22 = new GlobalObject(3403, new Vector3(-416.331604, 2208.938965, 44.175598), new Vector3(0.000000, 0.000000, 0.00000), 300.0f);

//CreateDynamicObject(874, -429.799988, 2185.969971, 41.360001, 0.000000, 0.000000, 322.549988, -1, -1);
GlobalObject terrz23 = new GlobalObject(874, new Vector3(-429.799988, 2185.969971, 41.360001), new Vector3(0.000000, 0.000000, 322.549988), 300.0f);

//CreateDynamicObject(2675, -417.908783, 2210.969971, 41.533798, 0.000000, 0.000000, 13.740000, -1, -1);
GlobalObject terrz24 = new GlobalObject(2675, new Vector3(-417.908783, 2210.969971, 41.533798), new Vector3(0.000000, 0.000000, 13.740000), 300.0f);

//CreateDynamicObject(2968, -419.599396, 2209.518555, 41.745602, 0.000000, 0.000000, -44.520000, -1, -1);
GlobalObject terrz25 = new GlobalObject(2968, new Vector3(-419.599396, 2209.518555, 41.745602), new Vector3(0.000000, 0.000000, -44.520000), 300.0f);

//CreateDynamicObject(1438, -420.398895, 2208.458008, 41.427700, 0.000000, 0.000000, 97.379997, -1, -1);
GlobalObject terrz26 = new GlobalObject(1438, new Vector3(-420.398895, 2208.458008, 41.427700), new Vector3(0.000000, 0.000000, 97.379997), 300.0f);

//CreateDynamicObject(2971, -419.976288, 2211.079346, 41.373798, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz27 = new GlobalObject(2971, new Vector3(-419.976288, 2211.079346, 41.373798), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(1412, -371.468170, 2272.223145, 42.167999, 0.000000, -3.500000, -27.180000, -1, -1);
GlobalObject terrz28 = new GlobalObject(1412, new Vector3(-371.468170, 2272.223145, 42.167999), new Vector3(0.000000, -3.500000, -27.180000), 300.0f);

//CreateDynamicObject(1347, -394.614838, 2251.035645, 41.903599, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz29 = new GlobalObject(1347, new Vector3(-394.614838, 2251.035645, 41.903599), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(1412, -432.268585, 2196.547119, 42.653999, 0.000000, 0.000000, 151.800003, -1, -1);
GlobalObject terrz30 = new GlobalObject(1412, new Vector3(-432.268585, 2196.547119, 42.653999), new Vector3(0.000000, 0.000000, 151.800003), 300.0f);

//CreateDynamicObject(874, -406.995605, 2238.829102, 41.007500, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz31 = new GlobalObject(874, new Vector3(-406.995605, 2238.829102, 41.007500), new Vector3(0.000000, 0.000000, 0.00000), 300.0f);

//CreateDynamicObject(1412, -376.089508, 2274.586182, 41.844002, 0.000000, -3.500000, -27.179991, -1, -1);
GlobalObject terrz32 = new GlobalObject(1412, new Vector3(-376.089508, 2274.586182, 41.844002), new Vector3(0.000000, -3.500000, -27.179991), 300.0f);

//CreateDynamicObject(874, -385.807709, 2267.532959, 40.516899, 0.000000, 0.000000, 60.720001, -1, -1);
GlobalObject terrz300 = new GlobalObject(874, new Vector3(-385.807709, 2267.532959, 40.516899), new Vector3(0.000000, 0.000000, 60.720001), 300.0f);

//CreateDynamicObject(1337, -420.370636, 2226.105225, 42.075600, 0.000000, 0.000000, -71.220016, -1, -1);
GlobalObject terrz33 = new GlobalObject(1337, new Vector3(-420.370636, 2226.105225, 42.075600), new Vector3(0.000000, 0.000000, -71.220016), 300.0f);

//CreateDynamicObject(1412, -436.458710, 2199.657471, 42.653999, 10.000000, 0.000000, 139.199982, -1, -1);
GlobalObject terrz34 = new GlobalObject(1412, new Vector3(-436.458710, 2199.657471, 42.653999), new Vector3(10.000000, 0.000000, 139.199982), 300.0f);

//CreateDynamicObject(874, -427.628021, 2216.021729, 41.309299, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz35 = new GlobalObject(874, new Vector3(-427.628021, 2216.021729, 41.309299), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(1412, -380.894012, 2275.120117, 41.736000, 0.000000, 0.000000, 13.440000, -1, -1);
GlobalObject terrz36 = new GlobalObject(1412, new Vector3(-380.894012, 2275.120117, 41.736000), new Vector3(0.000000, 0.000000, 13.440000), 300.0f);

//CreateDynamicObject(1412, -385.888397, 2273.870117, 41.736000, 0.000000, 0.000000, 13.900000, -1, -1);
GlobalObject terrz37 = new GlobalObject(1412, new Vector3(-385.888397, 2273.870117, 41.736000), new Vector3(0.000000, 0.000000, 13.900000), 300.0f);

//CreateDynamicObject(1412, -440.023254, 2203.980225, 41.466999, 90.000000, 0.000000, -214.100037, -1, -1);
GlobalObject terrz38 = new GlobalObject(1412, new Vector3(-440.023254, 2203.980225, 41.466999), new Vector3(90.000000, 0.000000, -214.100037), 300.0f);

//CreateDynamicObject(967, -389.047150, 2272.344238, 40.106201, 0.000000, 0.000000, 282.600006, -1, -1);
GlobalObject terrz39 = new GlobalObject(967, new Vector3(-389.047150, 2272.344238, 40.106201), new Vector3(0.000000, 0.000000, 282.600006), 300.0f);

//CreateDynamicObject(1219, -406.695496, 2254.568604, 41.427299, 0.000000, 0.000000, -3.000000, -1, -1);
GlobalObject terrz40 = new GlobalObject(1219, new Vector3(-406.695496, 2254.568604, 41.427299), new Vector3(0.000000, 0.000000, -3.000000), 300.0f);

//CreateDynamicObject(960, -406.841187, 2254.709961, 42.014599, 0.000000, 0.000000, 18.600000, -1, -1);
GlobalObject terrz41 = new GlobalObject(960, new Vector3(-406.841187, 2254.709961, 42.014599), new Vector3(0.000000, 0.000000, 18.600000), 300.0f);

//CreateDynamicObject(874, -445.160004, 2200.449951, 42.090000, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz42 = new GlobalObject(874, new Vector3(-445.160004, 2200.449951, 42.090000), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(967, -394.614410, 2270.955566, 40.106201, 0.000000, 0.000000, 461.399902, -1, -1);
GlobalObject terrz43 = new GlobalObject(967, new Vector3(-394.614410, 2270.955566, 40.106201), new Vector3(0.000000, 0.000000, 461.399902), 300.0f);

//CreateDynamicObject(970, -392.530701, 2273.977295, 40.114498, 90.000000, 0.000000, -152.039993, -1, -1);
GlobalObject terrz44 = new GlobalObject(970, new Vector3(-392.530701, 2273.977295, 40.114498), new Vector3(90.000000, 0.000000, -152.039993), 300.0f);

//CreateDynamicObject(850, -443.577698, 2207.188477, 41.530399, 0.000000, 0.000000, -55.680000, -1, -1);
GlobalObject terrz45 = new GlobalObject(850, new Vector3(-443.577698, 2207.188477, 41.530399), new Vector3(0.000000, 0.000000, -55.680000), 300.0f);

//CreateDynamicObject(874, -429.229095, 2231.920654, 41.309299, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz46 = new GlobalObject(874, new Vector3(-429.229095, 2231.920654, 41.309299), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(1441, -406.260010, 2262.100098, 41.910000, -5.700000, 3.720000, 0.000000, -1, -1);
GlobalObject terrz47 = new GlobalObject(1441, new Vector3(-406.260010, 2262.100098, 41.910000), new Vector3(-5.700000, 3.720000, 0.000000), 300.0f);

//CreateDynamicObject(1412, -398.033875, 2270.932617, 41.736000, 0.000000, 3.000000, 13.440000, -1, -1);
GlobalObject terrz48 = new GlobalObject(1412, new Vector3(-398.033875, 2270.932617, 41.736000), new Vector3(0.000000, 3.000000, 13.440000), 300.0f);

//CreateDynamicObject(3363, -450.369995, 2197.790039, 42.540001, -5.940000, 1.920000, 334.869995, -1, -1);
GlobalObject terrz49 = new GlobalObject(3363, new Vector3(-450.369995, 2197.790039, 42.540001), new Vector3(-5.940000, 1.920000, 334.869995), 300.0f);

//CreateDynamicObject(933, -436.390900, 2225.253174, 41.372501, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz50 = new GlobalObject(933, new Vector3(-436.390900, 2225.253174, 41.372501), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(1574, -427.126099, 2239.598389, 41.375900, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz51 = new GlobalObject(1574, new Vector3(-427.126099, 2239.598389, 41.375900), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(1357, -413.279999, 2257.080078, 41.700001, 0.000000, 0.000000, 321.829987, -1, -1);
GlobalObject terrz52 = new GlobalObject(1357, new Vector3(-413.279999, 2257.080078, 41.700001), new Vector3(0.000000, 0.000000, 321.829987), 300.0f);

//CreateDynamicObject(874, -415.790009, 2254.330078, 40.860001, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz53 = new GlobalObject(874, new Vector3(-415.790009, 2254.330078, 40.860001), new Vector3(0.000000, 0.000000, 0.0), 300.0f);

//CreateDynamicObject(2671, -426.100983, 2241.726807, 41.463200, 0.000000, 0.000000, -73.320000, -1, -1);
GlobalObject terrz54 = new GlobalObject(2671, new Vector3(-426.100983, 2241.726807, 41.463200), new Vector3(0.000000, 0.000000, -73.320000), 300.0f);

//CreateDynamicObject(1413, -403.146210, 2269.861816, 42.020000, 0.000000, -3.000000, -168.720001, -1, -1);
GlobalObject terrz55 = new GlobalObject(1413, new Vector3(-403.146210, 2269.861816, 42.020000), new Vector3(0.000000, -3.000000, -168.720001), 300.0f);

//CreateDynamicObject(1357, -412.239990, 2260.010010, 41.700001, 0.000000, 0.000000, 211.509995, -1, -1);
GlobalObject terrz56 = new GlobalObject(1357, new Vector3(-412.239990, 2260.010010, 41.700001), new Vector3(0.000000, 0.000000, 211.509995), 300.0f);

//CreateDynamicObject(1773, -426.992859, 2245.875244, 42.170799, 0.000000, 0.000000, 93.480003, -1, -1);
GlobalObject terrz57 = new GlobalObject(1773, new Vector3(-426.992859, 2245.875244, 42.170799), new Vector3(0.000000, 0.000000, 93.480003), 300.0f);

//CreateDynamicObject(734, -454.487274, 2203.406250, 41.245300, 0.000000, 0.000000, 75.959999, -1, -1);
GlobalObject terrz58 = new GlobalObject(734, new Vector3(-454.487274, 2203.406250, 41.245300), new Vector3(0.000000, 0.000000, 75.959999), 300.0f);
/*
fso_map = CreateDynamicObject(19993, -440.157104, 2227.749023, 42.093102, 0.000000, 0.000000, 0.000000, -1, -1);
SetDynamicObjectMaterial(fso_map, 0, 1453, "break_farm", "CJ_HAY", 0);
*/
GlobalObject terrztexture1 = new GlobalObject(19993, new Vector3(-440.157104, 2227.749023, 42.093102), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
terrztexture1.SetMaterial(0,1453, "break_farm", "CJ_HAY", 0);
//CreateDynamicObject(1412, -408.233704, 2268.669922, 42.222000, 0.000000, 2.000000, 13.440000, -1, -1);
GlobalObject terrz59 = new GlobalObject(1412, new Vector3(-408.233704, 2268.669922, 42.222000), new Vector3(0.000000, 2.000000, 13.440000), 300.0f);

//CreateDynamicObject(874, -418.134888, 2257.813477, 41.343498, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz60 = new GlobalObject(874, new Vector3(-418.134888, 2257.813477, 41.343498), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(735, -487.959991, 2052.689941, 58.790001, 0.000000, 0.000000, 155.449997, -1, -1);
GlobalObject terrz61 = new GlobalObject(735, new Vector3(-487.959991, 2052.689941, 58.790001), new Vector3(0.000000, 0.000000, 155.449997), 300.0f);

//CreateDynamicObject(1412, -413.331085, 2267.335693, 42.438000, 10.000000, 2.000000, 12.240000, -1, -1);
GlobalObject terrz62 = new GlobalObject(1412, new Vector3(-413.331085, 2267.335693, 42.438000), new Vector3(10.000000, 2.000000, 12.240000), 300.0f);

//CreateDynamicObject(1327, -448.280914, 2221.588623, 42.185398, 10.000000, -20.000000, 0.000000, -1, -1);
GlobalObject terrz63 = new GlobalObject(1327, new Vector3(-448.280914, 2221.588623, 42.185398), new Vector3(10.000000, -20.000000, 0.000000), 300.0f);

//CreateDynamicObject(3425, -465.040009, 2188.820068, 55.279999, 356.859985, 20.000000, 260.149994, -1, -1);
GlobalObject terrz64 = new GlobalObject(3425, new Vector3(-465.040009, 2188.820068, 55.279999), new Vector3(356.859985, 20.000000, 260.149994), 300.0f);

//CreateDynamicObject(1447, -418.463104, 2266.498535, 42.653999, 0.000000, 0.000000, 190.020096, -1, -1);
GlobalObject terrz65 = new GlobalObject(1447, new Vector3(-418.463104, 2266.498535, 42.653999), new Vector3(0.000000, 0.000000, 190.020096), 300.0f);

//CreateDynamicObject(874, -419.649994, 2269.389893, 40.639999, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz66 = new GlobalObject(874, new Vector3(-419.649994, 2269.389893, 40.639999), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(1412, -423.561707, 2265.309326, 42.653999, 0.000000, 0.000000, 13.440000, -1, -1);
GlobalObject terrz67 = new GlobalObject(1412, new Vector3(-423.561707, 2265.309326, 42.653999), new Vector3(0.000000, 0.000000, 13.440000), 300.0f);

//CreateDynamicObject(1412, -428.696259, 2264.114014, 42.653999, 0.000000, 0.000000, 13.440000, -1, -1);
GlobalObject terrz68 = new GlobalObject(1412, new Vector3(-428.696259, 2264.114014, 42.653999), new Vector3(0.000000, 0.000000, 13.440000), 300.0f);

//CreateDynamicObject(1413, -433.834991, 2263.095459, 42.667999, 0.000000, 0.000000, -168.720001, -1, -1);
GlobalObject terrz69 = new GlobalObject(1413, new Vector3(-433.834991, 2263.095459, 42.667999), new Vector3(0.000000, 0.000000, -168.720001), 300.0f);

//CreateDynamicObject(3593, -446.549988, 2248.899902, 41.849998, 0.000000, 0.000000, 331.540009, -1, -1);
GlobalObject terrz70 = new GlobalObject(3593, new Vector3(-446.549988, 2248.899902, 41.849998), new Vector3(0.000000, 0.000000, 331.540009), 300.0f);

//CreateDynamicObject(735, -393.279999, 2304.000000, 35.099998, 0.000000, 0.000000, 69.809998, -1, -1);
GlobalObject terrz71 = new GlobalObject(735, new Vector3(-393.279999, 2304.000000, 35.099998), new Vector3(0.000000, 0.000000, 69.809998), 300.0f);

//CreateDynamicObject(1412, -441.743103, 2256.155273, 41.466000, 90.000000, 0.000000, 39.600010, -1, -1);
GlobalObject terrz72 = new GlobalObject(1412, new Vector3(-441.743103, 2256.155273, 41.466000), new Vector3(90.000000, 0.000000, 39.600010), 300.0f);

//CreateDynamicObject(1413, -438.426605, 2260.921631, 42.667999, 0.000000, 0.000000, -139.800156, -1, -1);
GlobalObject terrz73 = new GlobalObject(1413, new Vector3(-438.426605, 2260.921631, 42.667999), new Vector3(0.000000, 0.000000, -139.800156), 300.0f);

//CreateDynamicObject(874, -440.200012, 2260.469971, 40.750000, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz74 = new GlobalObject(874, new Vector3(-440.200012, 2260.469971, 40.750000), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(734, -435.902496, 2266.051270, 41.184101, 0.000000, 0.000000, 0.000000, -1, -1);
GlobalObject terrz75 = new GlobalObject(734, new Vector3(-435.902496, 2266.051270, 41.184101), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);

//CreateDynamicObject(850, -450.648102, 2249.116211, 41.530399, 0.000000, 0.000000, 51.180000, -1, -1);
GlobalObject terrz76 = new GlobalObject(850, new Vector3(-450.648102, 2249.116211, 41.530399), new Vector3(0.000000, 0.000000, 51.180000), 300.0f);

//CreateDynamicObject(1412, -449.074127, 2253.392090, 41.466000, 90.000000, 0.000000, -9.119990, -1, -1);
GlobalObject terrz77 = new GlobalObject(1412, new Vector3(-449.074127, 2253.392090, 41.466000), new Vector3(90.000000, 0.000000, -9.119990), 300.0f);

//CreateDynamicObject(773, -476.390015, 2317.020020, 62.820000, 0.000000, 0.000000, 282.489990, -1, -1);
GlobalObject terrz78 = new GlobalObject(773, new Vector3(-476.390015, 2317.020020, 62.820000), new Vector3(0.000000, 0.000000, 282.489990), 300.0f);

//CreateDynamicObject(1265, -397.616211, 2186.191406, 41.695499, 0.000000, 360.000000, 300.000000, -1, -1);
GlobalObject terrz79 = new GlobalObject(1265, new Vector3(-397.616211, 2186.191406, 41.695499), new Vector3(0.000000, 360.000000, 300.000000), 300.0f);

//CreateDynamicObject(1666, -366.226807, 2210.822510, 41.636299, 0.000000, 360.000000, 300.000000, -1, -1);
GlobalObject terrz80 = new GlobalObject(1666, new Vector3( -366.226807, 2210.822510, 41.636299), new Vector3(0.000000, 360.000000, 300.000000), 300.0f);

//CreateDynamicObject(1950, -394.001587, 2241.227539, 41.912399, 0.000000, 360.000000, 300.000000, -1, -1);
GlobalObject terrz81 = new GlobalObject(1950, new Vector3(-394.001587, 2241.227539, 41.912399), new Vector3(0.000000, 360.000000, 300.000000), 300.0f);
            //RemoveBuildingForPlayer(playerid, 3425, -466.429688, 2190.273438, 55.992199, 0.250000);
            //  RemoveBuildingForPlayer(playerid, 3396, 275.312500, 1874.242188, 7.750000, 0.250000);
            //GlobalObject.Remove(player, 3396, new Vector3(275.312500f, 1874.242188f, 7.750000f), 0.25f);//

            //Remove Buildings///////////////////////////////////////////////////////////////////////////////////////////////
           
            //Objects////////////////////////////////////////////////////////////////////////////////////////////////////////
            //tmpobjid = CreateDynamicObject(2206, -171.855133, 1213.638793, 20.030311, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 18065, "ab_sfammumain", "plywood_gym", 0x00000000);
            GlobalObject fkmapp1 = new GlobalObject(2206, new Vector3(-171.855133, 1213.638793, 20.030311), new Vector3(0.000000, 0.000000, 180.000000), 300.0f);
            fkmapp1.SetMaterial(0, 18065, "ab_sfammumain", "plywood_gym", 0);

            //tmpobjid = CreateDynamicObject(19772, -171.973815, 1228.211303, 20.570323, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 3967, "cj_airprt", "bigbrick", 0x00000000);
            GlobalObject fkmapp2 = new GlobalObject(19772, new Vector3(-171.973815, 1228.211303, 20.570323), new Vector3(0.000000, 0.000000, 180.000000), 300.0f);
            fkmapp2.SetMaterial(0, 3967, "cj_airprt", "bigbrick", 0);

            //tmpobjid = CreateDynamicObject(19772, -173.893692, 1228.211303, 20.570323, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 3967, "cj_airprt", "bigbrick", 0x00000000);
            GlobalObject fkmapp3 = new GlobalObject(19772, new Vector3(-173.893692, 1228.211303, 20.570323), new Vector3(0.000000, 0.000000, 180.000000), 300.0f);
            fkmapp3.SetMaterial(0, 3967, "cj_airprt", "bigbrick", 0);

            //tmpobjid = CreateDynamicObject(19772, -172.193939, 1227.350463, 20.310293, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 3967, "cj_airprt", "bigbrick", 0x00000000);
            GlobalObject fkmapp4 = new GlobalObject(19772, new Vector3(-172.193939, 1227.350463, 20.310293), new Vector3(0.000000, 0.000000, 270.000000), 300.0f);
            fkmapp4.SetMaterial(0, 3967, "cj_airprt", "bigbrick", 0);


            //tmpobjid = CreateDynamicObject(19772, -172.913787, 1228.211425, 21.850353, 90.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 3967, "cj_airprt", "bigbrick", 0x00000000);
            GlobalObject fkmapp5 = new GlobalObject(19772, new Vector3(-172.913787, 1228.211425, 21.850353), new Vector3(90.000000, 0.000000, 180.000000), 300.0f);
            fkmapp5.SetMaterial(0, 3967, "cj_airprt", "bigbrick", 0);

            //tmpobjid = CreateDynamicObject(19772, -172.913787, 1228.211425, 23.290380, 90.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 3967, "cj_airprt", "bigbrick", 0x00000000);
            GlobalObject fkmapp6 = new GlobalObject(19772, new Vector3(-172.913787, 1228.211425, 23.290380), new Vector3(90.000000, 0.000000, 180.000000), 300.0f);
            fkmapp6.SetMaterial(0, 3967, "cj_airprt", "bigbrick", 0);

            //tmpobjid = CreateDynamicObject(19772, -173.643798, 1227.350463, 20.310293, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 3967, "cj_airprt", "bigbrick", 0x00000000);
            GlobalObject fkmapp7 = new GlobalObject(19772, new Vector3(-173.643798, 1227.350463, 20.310293), new Vector3(0.000000, 0.000000, 270.000000), 300.0f);
            fkmapp7.SetMaterial(0, 3967, "cj_airprt", "bigbrick", 0);

            //tmpobjid = CreateDynamicObject(2206, -177.135025, 1220.840820, 20.030311, 0.000000, 0.000000, 450.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 18065, "ab_sfammumain", "plywood_gym", 0x00000000);
            GlobalObject fkmapp8 = new GlobalObject(2206, new Vector3(-177.135025, 1220.840820, 20.030311), new Vector3(0.000000, 0.000000, 450.000000), 300.0f);
            fkmapp8.SetMaterial(0, 18065, "ab_sfammumain", "plywood_gym", 0);

            //tmpobjid = CreateDynamicObject(19439, -193.225280, 1219.755493, 19.832208, 0.000000, 90.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 10871, "blacksky_sfse", "ws_altz_wall7_top", 0x00000000);
            GlobalObject fkmapp9 = new GlobalObject(19439, new Vector3(-193.225280, 1219.755493, 19.832208), new Vector3(0.000000, 90.000000, -90.000000), 300.0f);
            fkmapp9.SetMaterial(0, 18065, "ab_sfammumain", "plywood_gym", 0);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //tmpobjid = CreateDynamicObject(18267, -172.907287, 1219.782592, 19.952186, 0.000000, 0.000000, 0.300039, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp10 = new GlobalObject(18267, new Vector3(-172.907287, 1219.782592, 19.952186), new Vector3(0.000000, 0.000000, 0.300039), 300.0f);

            //tmpobjid = CreateDynamicObject(929, -168.808151, 1215.007446, 20.830327, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp11 = new GlobalObject(929, new Vector3(-168.808151, 1215.007446, 20.830327), new Vector3(0.000000, 0.000000, 0.0), 300.0f);

            //tmpobjid = CreateDynamicObject(2509, -172.198272, 1213.011596, 21.990322, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp12 = new GlobalObject(2509, new Vector3(-172.198272, 1213.011596, 21.990322), new Vector3(0.000000, 0.000000, 180.000000), 300.0f);

            //tmpobjid = CreateDynamicObject(2509, -173.508346, 1213.011596, 21.990322, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp13 = new GlobalObject(2509, new Vector3(-173.508346, 1213.011596, 21.990322), new Vector3(0.000000, 0.000000, 180.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(18635, -172.068420, 1213.864135, 20.943723, 88.400108, -33.000030, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp14 = new GlobalObject(18635, new Vector3(-172.068420, 1213.864135, 20.943723), new Vector3(88.400108, -33.000030, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(18633, -173.514022, 1213.485473, 20.967359, 0.000000, 94.100059, -42.800014, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp15 = new GlobalObject(18633, new Vector3(-173.514022, 1213.485473, 20.967359), new Vector3(0.000000, 94.100059, -42.800014), 300.0f);
            //tmpobjid = CreateDynamicObject(2048, -177.588012, 1218.329833, 22.360328, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp16 = new GlobalObject(2048, new Vector3(-177.588012, 1218.329833, 22.360328), new Vector3(0.000000, 0.000000, 90.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(2587, -176.432922, 1213.051635, 22.140314, 0.000000, 11.499984, 180.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp17 = new GlobalObject(2587, new Vector3(-176.432922, 1213.051635, 22.140314), new Vector3(0.000000, 11.499984, 180.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(18688, -173.010620, 1227.851562, 18.750282, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp18 = new GlobalObject(18688, new Vector3(-173.010620, 1227.851562, 18.750282), new Vector3(0.0,0.0,0.0), 300.0f);
            //tmpobjid = CreateDynamicObject(18634, -172.385330, 1227.207153, 20.931745, -1.600004, 94.400001, 38.300022, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp19 = new GlobalObject(18634, new Vector3(-172.385330, 1227.207153, 20.931745), new Vector3(-1.600004, 94.400001, 38.300022), 300.0f);
            //tmpobjid = CreateDynamicObject(19573, -173.887847, 1226.703613, 20.031082, -2.200000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp20 = new GlobalObject(19573, new Vector3(-173.887847, 1226.703613, 20.031082), new Vector3(-2.200000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(3035, -168.841079, 1227.719970, 20.730327, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp21 = new GlobalObject(3035, new Vector3(-168.841079, 1227.719970, 20.730327), new Vector3(0.000000, 0.000000, -90.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(19626, -169.884216, 1228.775146, 20.797676, -15.800000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp22 = new GlobalObject(19626, new Vector3(-169.884216, 1228.775146, 20.797676), new Vector3(-15.800000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(19815, -177.596908, 1221.810302, 21.830352, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00);

            GlobalObject fkmapp23 = new GlobalObject(19815, new Vector3(-177.596908, 1221.810302, 21.830352), new Vector3(0.000000, 0.000000, 90.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(1413, -132.562194, 1231.007080, 18.865201, -90.000000, 32.400001, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp24 = new GlobalObject(1413, new Vector3(-132.562194, 1231.007080, 18.865201), new Vector3(-90.000000, 32.400001, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(13591, -154.100234, 1234.181640, 19.052194, 0.000000, 0.000000, 9.400005, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp25 = new GlobalObject(13591, new Vector3(-154.100234, 1234.181640, 19.052194), new Vector3(0.000000, 0.000000, 9.400005), 300.0f);
            //tmpobjid = CreateDynamicObject(3593, -170.829849, 1232.379150, 19.212198, 0.000000, 0.000000, 98.999954, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp26 = new GlobalObject(3593, new Vector3(-170.829849, 1232.379150, 19.212198), new Vector3(0.000000, 0.000000, 98.999954), 300.0f);
            //tmpobjid = CreateDynamicObject(1463, -158.945632, 1226.081542, 19.052190, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp27 = new GlobalObject(1463, new Vector3(-158.945632, 1226.081542, 19.052190), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(12957, -146.348022, 1234.565673, 19.252193, 0.000000, 0.000000, 30.999998, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp28 = new GlobalObject(12957, new Vector3(-146.348022, 1234.565673, 19.252193), new Vector3(0.000000, 0.000000, 30.999998), 300.0f);
            //tmpobjid = CreateDynamicObject(1463, -158.945632, 1227.780883, 19.052190, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp29 = new GlobalObject(1463, new Vector3(-158.945632, 1227.780883, 19.052190), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(1463, -158.945632, 1227.000244, 19.292196, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp30 = new GlobalObject(1463, new Vector3(-158.945632, 1227.000244, 19.292196), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(2121, -165.929718, 1215.262329, 20.530323, 0.000000, 0.000000, 153.800064, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp31 = new GlobalObject(2121, new Vector3(-165.929718, 1215.262329, 20.530323), new Vector3(0.000000, 0.000000, 153.800064), 300.0f);
            //tmpobjid = CreateDynamicObject(19905, -210.518676, 1216.525268, 18.702186, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp32 = new GlobalObject(19905, new Vector3(-210.518676, 1216.525268, 18.702186), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(16448, -159.708465, 1225.942260, 22.812276, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp33 = new GlobalObject(16448, new Vector3(), new Vector3(), 300.0f);
            //tmpobjid = CreateDynamicObject(1282, -191.728240, 1205.869873, 19.361406, 0.000000, 0.000000, 142.699981, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp34 = new GlobalObject(1282, new Vector3(-191.728240, 1205.869873, 19.361406), new Vector3(0.000000, 0.000000, 142.699981), 300.0f);
            //tmpobjid = CreateDynamicObject(19817, -234.733367, 1216.109985, 18.702186, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp35 = new GlobalObject(19817, new Vector3(-234.733367, 1216.109985, 18.702186), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(19872, -223.707260, 1215.260009, 18.823757, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp36 = new GlobalObject(19872, new Vector3(-223.707260, 1215.260009, 18.823757), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(19899, -196.773849, 1223.368530, 18.902187, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp37 = new GlobalObject(19899, new Vector3(-196.773849, 1223.368530, 18.902187), new Vector3(0.000000, 0.000000, -90.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(19900, -198.384811, 1223.570800, 18.882186, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp38 = new GlobalObject(19900, new Vector3(-198.384811, 1223.570800, 18.882186), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(19899, -194.203948, 1223.368530, 18.902187, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp39 = new GlobalObject(19899, new Vector3(-194.203948, 1223.368530, 18.902187), new Vector3(0.000000, 0.000000, -90.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(19903, -199.340820, 1223.544555, 18.902187, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp40 = new GlobalObject(19903, new Vector3(-199.340820, 1223.544555, 18.902187), new Vector3(0.000000, 0.000000, -90.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(19900, -198.384811, 1223.570800, 18.882186, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp41 = new GlobalObject(19900, new Vector3(-198.384811, 1223.570800, 18.882186), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(19815, -192.916748, 1219.778686, 20.600337, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp42 = new GlobalObject(19815, new Vector3(-192.916748, 1219.778686, 20.600337), new Vector3(0.000000, 0.000000, 270.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(19906, -214.916839, 1210.147460, 24.383893, -27.000041, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp43 = new GlobalObject(19906, new Vector3(-214.916839, 1210.147460, 24.383893), new Vector3(-27.000041, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(19906, -223.904037, 1208.548950, 22.083766, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp44 = new GlobalObject(19906, new Vector3(-223.904037, 1208.548950, 22.083766), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(1025, -193.036407, 1216.802490, 21.522197, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp45 = new GlobalObject(1025, new Vector3(-193.036407, 1216.802490, 21.522197), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(1025, -193.036407, 1215.332275, 22.692224, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp46 = new GlobalObject(1025, new Vector3(-193.036407, 1215.332275, 22.692224), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(1025, -193.036407, 1213.891967, 21.522197, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp47 = new GlobalObject(1025, new Vector3(-193.036407, 1213.891967, 21.522197), new Vector3(0.000000, 0.000000, 0.000000), 300.0f);
            //tmpobjid = CreateDynamicObject(1025, -193.036407, 1212.242065, 22.692224, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp48 = new GlobalObject(1025, new Vector3(-193.036407, 1212.242065, 22.692224), new Vector3(0.0,0.0,0.0), 300.0f);
            //tmpobjid = CreateDynamicObject(1085, -193.317672, 1222.411010, 19.289224, 0.000000, -9.099895, -144.999938, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp49 = new GlobalObject(1085, new Vector3(-193.317672, 1222.411010, 19.289224), new Vector3(0.000000, -9.099895, -144.999938), 300.0f);
            //tmpobjid = CreateDynamicObject(3171, -137.414733, 1225.568847, 18.692186, 0.000000, 0.000000, 40.500000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp50 = new GlobalObject(3171, new Vector3(-137.414733, 1225.568847, 18.692186), new Vector3(0.000000, 0.000000, 40.500000), 300.0f);
            //tmpobjid = CreateDynamicObject(3168, -146.750991, 1228.765747, 18.715198, 0.000000, 0.000000, 83.200057, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp51 = new GlobalObject(3168, new Vector3(-146.750991, 1228.765747, 18.715198), new Vector3(0.000000, 0.000000, 83.200057), 300.0f);
            //tmpobjid = CreateDynamicObject(19632, -143.494338, 1220.778564, 18.742187, 0.000000, 0.000000, -22.100000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp52 = new GlobalObject(19632, new Vector3(-143.494338, 1220.778564, 18.742187), new Vector3(0.000000, 0.000000, -22.100000), 300.0f);
            //tmpobjid = CreateDynamicObject(2121, -142.523391, 1219.660888, 18.995204, 0.000000, -90.000000, -107.000076, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp53 = new GlobalObject(2121, new Vector3(-142.523391, 1219.660888, 18.995204), new Vector3(0.000000, -90.000000, -107.000076), 300.0f);
            //tmpobjid = CreateDynamicObject(2121, -142.321334, 1221.721557, 19.215209, 0.000000, 0.000000, -37.199977, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp54 = new GlobalObject(2121, new Vector3(-142.321334, 1221.721557, 19.215209), new Vector3(0.000000, 0.000000, -37.199977), 300.0f);
            //tmpobjid = CreateDynamicObject(2121, -144.646179, 1221.684326, 19.215209, 0.000000, 0.000000, 31.900020, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject fkmapp55 = new GlobalObject(2121, new Vector3(-144.646179, 1221.684326, 19.215209), new Vector3(0.000000, 0.000000, 31.900020), 300.0f);
            //UP+ FK MAPP 0.1a
            //GLAVA HOUSE FIRST VERSION
            //tmpobjid = CreateDynamicObject(19379, 1338.385009, 1572.716552, 2998.919433, 0.000000, 90.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 14777, "int_casinoint3", "GB_midbar05", 0x00000000);
            GlobalObject glavamapp1 = new GlobalObject(19379, new Vector3(1338.385009, 1572.716552, 2998.919433), new Vector3(0.000000, 90.000000, 0.000000), 30.0f);
            glavamapp1.SetMaterial(0, 14777, "int_casinoint3", "GB_midbar05", 0);

            //tmpobjid = CreateDynamicObject(19379, 1338.385009, 1582.346557, 2998.919433, 0.000000, 90.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 14777, "int_casinoint3", "GB_midbar05", 0x00000000);
            GlobalObject glavamapp2 = new GlobalObject(19379, new Vector3(1338.385009, 1582.346557, 2998.919433), new Vector3(0.000000, 90.000000, 0.000000), 30.0f);
            glavamapp2.SetMaterial(0, 14777, "int_casinoint3", "GB_midbar05", 0);

            //tmpobjid = CreateDynamicObject(19379, 1327.892333, 1572.716552, 3003.919433, 0.000000, 90.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4828, "airport3_las", "brwall_128", 0x00000000);
            GlobalObject glavamapp3 = new GlobalObject(19379, new Vector3(1327.892333, 1572.716552, 3003.919433), new Vector3(0.000000, 90.000000, 0.000000), 30.0f);
            glavamapp3.SetMaterial(0, 4828, "airport3_las", "brwall_128", 0);

            //tmpobjid = CreateDynamicObject(19379, 1327.892333, 1582.338500, 2998.919433, 0.000000, 90.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 14777, "int_casinoint3", "GB_midbar05", 0x00000000);
            GlobalObject glavamapp4 = new GlobalObject(19379, new Vector3(1327.892333, 1582.338500, 2998.919433), new Vector3(0.000000, 90.000000, 0.000000), 30.0f);
            glavamapp4.SetMaterial(0, 14777, "int_casinoint3", "GB_midbar05", 0);

            //tmpobjid = CreateDynamicObject(19379, 1336.059204, 1586.072021, 2998.919433, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 30.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4830, "airport2", "brnstucco1", 0x00000000);
            GlobalObject glavamapp5 = new GlobalObject(19379, new Vector3(1336.059204, 1586.072021, 2998.919433), new Vector3(0.000000, 0.000000, 90.000000), 30.0f);
            glavamapp5.SetMaterial(0, 4830, "airport2", "brnstucco1", 0);

            //tmpobjid = CreateDynamicObject(19379, 1338.385009, 1572.716552, 3003.919433, 0.000000, 90.000000, 0.000000, object_world, object_int, -1, 300.00, 30.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4828, "airport3_las", "brwall_128", 0x00000000);
            GlobalObject glavamapp6 = new GlobalObject(19379, new Vector3(1338.385009, 1572.716552, 3003.919433), new Vector3(0.000000, 90.000000, 0.000000), 30.0f);
            glavamapp6.SetMaterial(0, 4828, "airport3_las", "brwall_128", 0);

            //tmpobjid = CreateDynamicObject(19379, 1338.385009, 1582.346557, 3003.919433, 0.000000, 90.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4828, "airport3_las", "brwall_128", 0x00000000);
            GlobalObject glavamapp7 = new GlobalObject(19379, new Vector3(1338.385009, 1582.346557, 3003.919433), new Vector3(0.000000, 90.000000, 0.000000), 30.0f);
            glavamapp7.SetMaterial(0, 4828, "airport3_las", "brwall_128", 0);

            //tmpobjid = CreateDynamicObject(19379, 1327.892333, 1572.716552, 2998.919433, 0.000000, 90.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 14777, "int_casinoint3", "GB_midbar05", 0x00000000);
            GlobalObject glavamapp8 = new GlobalObject(19379, new Vector3(1327.892333, 1572.716552, 2998.919433), new Vector3(0.000000, 90.000000, 0.000000), 30.0f);
            glavamapp8.SetMaterial(0, 14777, "int_casinoint3", "GB_midbar05", 0);

            //tmpobjid = CreateDynamicObject(19379, 1326.429199, 1568.072021, 2998.919433, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4830, "airport2", "brnstucco1", 0x00000000);
            GlobalObject glavamapp9 = new GlobalObject(19379, new Vector3(1326.429199, 1568.072021, 2998.919433), new Vector3(0.000000, 0.000000, 90.000000), 30.0f);
            glavamapp9.SetMaterial(0, 14777, "airport2", "brnstucco1", 0);

            //tmpobjid = CreateDynamicObject(19379, 1327.892333, 1582.338500, 3003.919433, 0.000000, 90.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4828, "airport3_las", "brwall_128", 0x00000000);
            GlobalObject glavamapp10 = new GlobalObject(19379, new Vector3(1327.892333, 1582.338500, 3003.919433), new Vector3(0.000000, 90.000000, 0.000000), 30.0f);
            glavamapp10.SetMaterial(0, 4828, "airport3_las", "brwall_128", 0);

            //tmpobjid = CreateDynamicObject(19379, 1326.429199, 1586.072021, 2998.919433, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4830, "airport2", "brnstucco1", 0x00000000);
            GlobalObject glavamapp11 = new GlobalObject(19379, new Vector3(1326.429199, 1586.072021, 2998.919433), new Vector3(0.000000, 0.000000, 90.000000), 30.0f);
            glavamapp11.SetMaterial(0, 4830, "airport2", "brnstucco1", 0);

            //tmpobjid = CreateDynamicObject(19379, 1336.059204, 1568.072021, 2998.919433, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4830, "airport2", "brnstucco1", 0x00000000);
            GlobalObject glavamapp12 = new GlobalObject(19379, new Vector3(1336.059204, 1568.072021, 2998.919433), new Vector3(0.000000, 0.000000, 90.000000), 30.0f);
            glavamapp12.SetMaterial(0, 4830, "airport2", "brnstucco1", 0);

            //tmpobjid = CreateDynamicObject(19379, 1322.892333, 1572.716552, 2998.919433, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4830, "airport2", "brnstucco1", 0x00000000);
            GlobalObject glavamapp13 = new GlobalObject(19379, new Vector3(1322.892333, 1572.716552, 2998.919433), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            glavamapp13.SetMaterial(0, 4830, "airport2", "brnstucco1", 0);

            //tmpobjid = CreateDynamicObject(19379, 1322.892333, 1582.338500, 2998.919433, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4830, "airport2", "brnstucco1", 0x00000000);
            GlobalObject glavamapp14 = new GlobalObject(19379, new Vector3(1322.892333, 1582.338500, 2998.919433), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            glavamapp14.SetMaterial(0, 4830, "airport2", "brnstucco1", 0);

            //tmpobjid = CreateDynamicObject(19379, 1340.892333, 1582.338500, 2998.919433, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4830, "airport2", "brnstucco1", 0x00000000);
            GlobalObject glavamapp15 = new GlobalObject(19379, new Vector3(1340.892333, 1582.338500, 2998.919433), new Vector3(0.0, 0.0, 0.0), 30.0f);
            glavamapp15.SetMaterial(0, 4830, "airport2", "brnstucco1", 0);

            //tmpobjid = CreateDynamicObject(19379, 1340.892333, 1572.716552, 2998.919433, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4830, "airport2", "brnstucco1", 0x00000000);
            GlobalObject glavamapp16 = new GlobalObject(19379, new Vector3(1340.892333, 1572.716552, 2998.919433), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            glavamapp16.SetMaterial(0, 4830, "airport2", "brnstucco1", 0);

            //tmpobjid = CreateDynamicObject(19379, 1324.880615, 1569.620605, 2998.919433, 0.000000, 0.000000, 45.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 4830, "airport2", "brnstucco1", 0x00000000);
            GlobalObject glavamapp17 = new GlobalObject(19379, new Vector3(1324.880615, 1569.620605, 2998.919433), new Vector3(0.000000, 0.000000, 45.000000), 30.0f);
            glavamapp17.SetMaterial(0, 4830, "airport2", "brnstucco1", 0);

            //tmpobjid = CreateDynamicObject(3034, 1330.667602, 1568.170288, 3001.087402, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 14383, "burg_1", "kit_windo_12", 0x00000000);
            GlobalObject glavamapp18 = new GlobalObject(3034, new Vector3(1330.667602, 1568.170288, 3001.087402), new Vector3(0.000000, 0.000000, 180.000000), 30.0f);
            glavamapp18.SetMaterial(0, 14383, "burg_1", "kit_windo_12", 0);

            //tmpobjid = CreateDynamicObject(3034, 1337.277954, 1568.160278, 3001.087402, 0.000000, 0.000000, 180.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 14383, "burg_1", "kit_windo_12", 0x00000000);
            GlobalObject glavamapp19 = new GlobalObject(3034, new Vector3(1337.277954, 1568.160278, 3001.087402), new Vector3(0.000000, 0.000000, 180.000000), 30.0f);
            glavamapp19.SetMaterial(0, 14383, "burg_1", "kit_windo_12", 0);

            //tmpobjid = CreateDynamicObject(3034, 1322.995117, 1575.552490, 3001.087402, 0.000000, 0.000000, 450.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 14383, "burg_1", "kit_windo_12", 0x00000000);
            GlobalObject glavamapp20 = new GlobalObject(3034, new Vector3(1322.995117, 1575.552490, 3001.087402), new Vector3(0.000000, 0.000000, 450.000000), 30.0f);
            glavamapp20.SetMaterial(0, 14383, "burg_1", "kit_windo_12", 0);

            //tmpobjid = CreateDynamicObject(3034, 1322.995117, 1581.992431, 3001.087402, 0.000000, 0.000000, 450.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 14383, "burg_1", "kit_windo_12", 0x00000000);
            GlobalObject glavamapp21 = new GlobalObject(3034, new Vector3(1322.995117, 1581.992431, 3001.087402), new Vector3(0.000000, 0.000000, 450.000000), 30.0f);
            glavamapp21.SetMaterial(0, 14383, "burg_1", "kit_windo_12", 0);

            //tmpobjid = CreateDynamicObject(2366, 1336.479003, 1581.258178, 2998.544921, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //SetDynamicObjectMaterial(tmpobjid, 0, 1355, "break_s_bins", "CJ_WOOD_DARK", 0x00000000);
            //SetDynamicObjectMaterial(tmpobjid, 1, 1355, "break_s_bins", "CJ_WOOD_DARK", 0x00000000);
            GlobalObject glavamapp22 = new GlobalObject(2366, new Vector3(1336.479003, 1581.258178, 2998.544921), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            glavamapp22.SetMaterial(0, 1355, "break_s_bins", "CJ_WOOD_DARK", 0);
            glavamapp22.SetMaterial(0, 1355, "break_s_bins", "CJ_WOOD_DARK", 0);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //tmpobjid = CreateDynamicObject(1535, 1323.655883, 1570.894409, 2999.005371, 0.000000, 0.000000, -45.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv1 = new GlobalObject(1535, new Vector3(1323.655883, 1570.894409, 2999.005371), new Vector3(0.000000, 0.000000, -45.000000), 30.0f); // - це так должно выгладеть          
                                                                                                                                                                       //tmpobjid = CreateDynamicObject(1535, 1325.815185, 1568.777587, 2999.005371, 0.000000, 0.000000, 135.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv2 = new GlobalObject(1535, new Vector3(1325.815185, 1568.777587, 2999.005371), new Vector3(0.000000, 0.000000, 135.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(16779, 1331.728881, 1577.794433, 3003.949951, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv3 = new GlobalObject(16779, new Vector3(1331.728881, 1577.794433, 3003.949951), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2207, 1335.423217, 1581.524658, 2999.005371, 0.000000, 0.000000, -45.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv4 = new GlobalObject(2207, new Vector3(1335.423217, 1581.524658, 2999.005371), new Vector3(0.000000, 0.000000, -45.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2207, 1338.090209, 1581.586181, 2999.005371, 0.000000, 0.000000, 495.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv5 = new GlobalObject(2207, new Vector3(1338.090209, 1581.586181, 2999.005371), new Vector3(0.000000, 0.000000, 495.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(1671, 1338.528564, 1583.486450, 2999.415771, 0.000000, 0.000000, -32.699985, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv6 = new GlobalObject(1671, new Vector3(1338.528564, 1583.486450, 2999.415771), new Vector3(0.000000, 0.000000, -32.699985), 30.0f);
            //tmpobjid = CreateDynamicObject(2309, 1336.274291, 1579.074707, 2998.995361, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv7 = new GlobalObject(2309, new Vector3(1336.274291, 1579.074707, 2998.995361), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2309, 1334.570312, 1580.773803, 2998.995361, 0.000000, 0.000000, -71.599899, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv8 = new GlobalObject(2309, new Vector3(1334.570312, 1580.773803, 2998.995361), new Vector3(0.000000, 0.000000, -71.599899), 30.0f);
            //tmpobjid = CreateDynamicObject(2007, 1339.877075, 1585.515502, 2998.995361, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv9 = new GlobalObject(2007, new Vector3(1339.877075, 1585.515502, 2998.995361), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2007, 1340.347534, 1585.025024, 2998.995361, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv10 = new GlobalObject(2007, new Vector3(1340.347534, 1585.025024, 2998.995361), new Vector3(0.000000, 0.000000, -90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2007, 1338.896118, 1585.515502, 2998.995361, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv11 = new GlobalObject(2007, new Vector3(1338.896118, 1585.515502, 2998.995361), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2007, 1338.896118, 1585.515502, 3000.395263, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv12 = new GlobalObject(2007, new Vector3(1338.896118, 1585.515502, 3000.395263), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2007, 1339.877075, 1585.515502, 3000.395263, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv13 = new GlobalObject(2007, new Vector3(1339.877075, 1585.515502, 3000.395263), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2007, 1340.347534, 1585.025024, 3000.395263, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv14 = new GlobalObject(2007, new Vector3(1340.347534, 1585.025024, 3000.395263), new Vector3(0.000000, 0.000000, -90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2007, 1340.347534, 1584.024047, 2998.995361, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv15 = new GlobalObject(2007, new Vector3(1340.347534, 1584.024047, 2998.995361), new Vector3(0.000000, 0.000000, -90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2007, 1340.347534, 1584.024047, 3000.395263, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv16 = new GlobalObject(2007, new Vector3(1340.347534, 1584.024047, 3000.395263), new Vector3(0.000000, 0.000000, -90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2111, 1326.589355, 1578.987060, 2999.335693, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv17 = new GlobalObject(2111, new Vector3(1326.589355, 1578.987060, 2999.335693), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(1723, 1324.765502, 1578.014404, 2998.985351, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv18 = new GlobalObject(1723, new Vector3(1324.765502, 1578.014404, 2998.985351), new Vector3(0.000000, 0.000000, 90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(1724, 1326.038085, 1581.132934, 2998.995361, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv19 = new GlobalObject(1724, new Vector3(1326.038085, 1581.132934, 2998.995361), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2258, 1323.001342, 1578.817138, 3001.167480, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv20 = new GlobalObject(2258, new Vector3(1323.001342, 1578.817138, 3001.167480), new Vector3(0.000000, 0.000000, 90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(1723, 1328.318481, 1580.014892, 2998.985351, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv21 = new GlobalObject(1723, new Vector3(1328.318481, 1580.014892, 2998.985351), new Vector3(0.000000, 0.000000, 270.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2801, 1326.480834, 1579.095947, 2999.395751, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappv22 = new GlobalObject(2801, new Vector3(1326.480834, 1579.095947, 2999.395751), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2161, 1337.541870, 1585.966552, 2999.005371, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            //ZONA Z
            GlobalObject glavamappz1 = new GlobalObject(2161, new Vector3(1337.541870, 1585.966552, 2999.005371), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2161, 1337.541870, 1585.966552, 3000.356689, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz2 = new GlobalObject(2161, new Vector3(1337.541870, 1585.966552, 3000.356689), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(1742, 1336.116577, 1586.089355, 2999.005371, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz3 = new GlobalObject(1742, new Vector3(1336.116577, 1586.089355, 2999.005371), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2161, 1334.760742, 1585.966552, 2999.005371, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz4 = new GlobalObject(2161, new Vector3(1334.760742, 1585.966552, 2999.005371), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2167, 1340.785888, 1583.020019, 2998.965332, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz5 = new GlobalObject(2167, new Vector3(1340.785888, 1583.020019, 2998.965332), new Vector3(0.000000, 0.000000, -90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2167, 1340.785888, 1581.768798, 2998.965332, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz6 = new GlobalObject(2167, new Vector3(1340.785888, 1581.768798, 2998.965332), new Vector3(0.000000, 0.000000, -90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2167, 1340.785888, 1583.020019, 3000.476806, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz7 = new GlobalObject(2167, new Vector3(1340.785888, 1583.020019, 3000.476806), new Vector3(0.000000, 0.000000, -90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2167, 1340.785888, 1581.768798, 3000.476806, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz8 = new GlobalObject(2167, new Vector3(1340.785888, 1581.768798, 3000.476806), new Vector3(0.000000, 0.000000, -90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2036, 1340.593872, 1582.423583, 2999.552490, 0.000000, -68.800018, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz9 = new GlobalObject(2036, new Vector3(1340.593872, 1582.423583, 2999.552490), new Vector3(0.000000, -68.800018, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(1669, 1336.887939, 1582.615844, 2999.936279, 0.000000, 0.000000, 25.699998, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz10 = new GlobalObject(1669, new Vector3(1336.887939, 1582.615844, 2999.936279), new Vector3(0.000000, 0.000000, 25.699998), 30.0f);
            //tmpobjid = CreateDynamicObject(19818, 1336.997924, 1582.343750, 2999.866210, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz11 = new GlobalObject(19818, new Vector3(1336.997924, 1582.343750, 2999.866210), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(19893, 1337.688110, 1582.085449, 2999.786132, 0.000000, 0.000000, 170.500015, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz12 = new GlobalObject(19893, new Vector3(1337.688110, 1582.085449, 2999.786132), new Vector3(0.000000, 0.000000, 170.500015), 30.0f);
            //tmpobjid = CreateDynamicObject(2674, 1336.545532, 1583.177490, 2999.015380, 0.000000, 0.000000, -24.200016, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz13 = new GlobalObject(2674, new Vector3(1336.545532, 1583.177490, 2999.015380), new Vector3(0.000000, 0.000000, -24.200016), 30.0f);
            //tmpobjid = CreateDynamicObject(11705, 1336.472656, 1582.508911, 2999.746093, 0.000000, 0.000000, 110.699996, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz14 = new GlobalObject(11705, new Vector3(1336.472656, 1582.508911, 2999.746093), new Vector3(0.000000, 0.000000, 110.699996), 30.0f);
            //tmpobjid = CreateDynamicObject(2256, 1339.139770, 1585.963623, 3002.739013, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz15 = new GlobalObject(2256, new Vector3(1339.139770, 1585.963623, 3002.739013), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2204, 1340.817016, 1580.841552, 2999.005371, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz16 = new GlobalObject(2204, new Vector3(1340.817016, 1580.841552, 2999.005371), new Vector3(0.000000, 0.000000, -90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(2253, 1340.596191, 1581.000488, 3001.047363, 0.000000, 0.000000, -16.599998, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz17 = new GlobalObject(2253, new Vector3(1340.596191, 1581.000488, 3001.047363), new Vector3(0.000000, 0.000000, -16.599998), 30.0f);
            //tmpobjid = CreateDynamicObject(638, 1330.740722, 1568.567871, 2999.625976, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz18 = new GlobalObject(638, new Vector3(1330.740722, 1568.567871, 2999.625976), new Vector3(0.000000, 0.000000, 90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(638, 1337.341674, 1568.567871, 2999.625976, 0.000000, 0.000000, 90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz19 = new GlobalObject(638, new Vector3(1337.341674, 1568.567871, 2999.625976), new Vector3(0.000000, 0.000000, 90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(1713, 1337.076293, 1573.350219, 2999.005371, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz20 = new GlobalObject(1713, new Vector3(1337.076293, 1573.350219, 2999.005371), new Vector3(0.000000, 0.000000, -90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(1713, 1334.194580, 1571.668701, 2999.005371, 0.000000, 0.000000, 90.000000, objct_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz21 = new GlobalObject(1713, new Vector3(1334.194580, 1571.668701, 2999.005371), new Vector3(0.000000, 0.000000, 90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(19786, 1340.857910, 1572.564575, 3001.247558, 0.000000, 0.000000, -90.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz22 = new GlobalObject(19786, new Vector3(1340.857910, 1572.564575, 3001.247558), new Vector3(0.000000, 0.000000, -90.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(1814, 1339.769165, 1573.057495, 2998.975341, 0.000000, 0.000000, 270.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz23 = new GlobalObject(1814, new Vector3(1339.769165, 1573.057495, 2998.975341), new Vector3(0.000000, 0.000000, 270.000000), 30.0f);
            //tmpobjid = CreateDynamicObject(18075, 1331.966796, 1577.373657, 3003.880126, 0.000000, 0.000000, 0.000000, object_world, object_int, -1, 300.00, 300.00);
            GlobalObject glavamappz24 = new GlobalObject(18075, new Vector3(1331.966796, 1577.373657, 3003.880126), new Vector3(0.000000, 0.000000, 0.000000), 30.0f);

            

        }
        private void GangZoneCreate(BasePlayer pl)
        {
            //Template:
            //  { "[name]", [minx], [miny], [maxx], [maxy], 0x[color - hex]FF },


            //  { "Zone 1", 87, 1789.5, 303, 1943.5, 0x7FFF00FF },
            //  { "Zone 2", -459, 2195.5, -334, 2273.5, 0x7FFF00FF },
            // { "Zone 3", 549, 88.5, 615, 149.5, 0x7FFF00FF },
            //  { "Zone 4", 808, -1387.5, 917, -1330.5, 0x00FF7FFF },
            
            GangZone gz1 = new GangZone(87.0f, 1789.5f,303f,1943.5f);
            gz1.Color = new SampSharp.GameMode.SAMP.Color(0x7FFF00FF);
            gz1.Show(pl);
            
            GangZone gz2 = new GangZone(-459f, 2195.5f, -334f, 2273.5f);
            gz2.Color = new SampSharp.GameMode.SAMP.Color(0x7FFF00FF);
            gz2.Show(pl);
            GangZone gz3 = new GangZone(549f, 88.5f, 615f, 149.5f);
            gz3.Color = new SampSharp.GameMode.SAMP.Color(0x7FFF00FF);
            gz3.Show(pl);
            GangZone gz4 = new GangZone(549f, 88.5f, 615f, 149.5f);
            gz4.Color = new SampSharp.GameMode.SAMP.Color(0x00FF7FFF);
            gz4.Show(pl);
        }
        protected override void OnPlayerText(BasePlayer player, TextEventArgs e)
        {
            
            e.SendToPlayers = false;
            
            if (e.Text.Length <= 113)
            {
                foreach(Player p in Player.All)
                {
                    if (p.IsInRangeOfPoint(25, player.Position))
                    {
                        p.SendClientMessage($"{{ffffff}}{player.Name}[{player.Id}]: {e.Text}");
                    }
                }
            }
            
            
            base.OnPlayerText(player, e);
        }


    }
}