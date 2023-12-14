using Microsoft.Identity.Client;
using MySql.Data.MySqlClient;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using System;

namespace SampSharpGameMode1
{
    public class GameMode : BaseMode
    {
        MySqlConnection sqlCon = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=Samp");
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Console.WriteLine("\n----------------------------------");
            Console.WriteLine(" Blank game mode by your name here");
            Console.WriteLine("----------------------------------\n");
            Server.SetWeather(31);
            SetGameModeText("Blank game mode");
            


            // TODO: Put logic to initialize your game mode here
        }
        protected override void OnPlayerSpawned(BasePlayer player, SpawnEventArgs e)
        {
            base.OnPlayerSpawned(player, e);
            //fso_map = CreateDynamicObject(16151, 242.750000, 1855.770020, 8.090000, 0.000000, 0.000000, 180.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 7, 5267, "lashops91_las2", "laspowrec2", 0);
            // SetDynamicObjectMaterial(fso_map, 5, 18642, "taser1", "metalshinydented1", 0);
            // SetDynamicObjectMaterial(fso_map, 3, 14533, "pleas_dome", "club_zeb_SFW2", 0)
            GlobalObject gb = new GlobalObject(16151, new Vector3(242.75f, 1855.770020f, 8.09000f) , new Vector3(0.00000f, 0.00000f , 180.0f), 300.0f);
            gb.SetMaterial(7, 5267, "lashops911_las2", "laspowrec2", 0);
            gb.SetMaterial(5, 18642, "taser1", "metalshinydented1", 0);
            gb.SetMaterial(3, 14533, "pleas_dome", "club_zeb_SFW2", 0);
            //fso_map = CreateDynamicObject(19411, 242.940002, 1861.199951, 9.430000, 0.000000, 0.000000, 0.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0)
            GlobalObject gbw1 = new GlobalObject(19411, new Vector3(242.940002f, 1861.199951f, 9.430000f), new Vector3(0.00000f, 0.00000f, 0.0f), 300.0f);
            gbw1.SetMaterial(0, 5267, "lashops91_las2" , "laspowrec2", 0);

            // fso_map = CreateDynamicObject(19438, 242.229996, 1859.530029, 9.430000, 0.000000, 0.000000, 90.000000, -1, -1);
            // SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0);
            GlobalObject gbw2 = new GlobalObject(19438, new Vector3(242.229996f, 1859.530029f, 9.43f), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            gbw2.SetMaterial(0, 5267, "lashops91_las2", "laspowrec2", 0);
            //fso_map = CreateDynamicObject(19438, 242.210007, 1861.229980, 11.090000, 0.000000, 90.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0);
            GlobalObject gbw3 = new GlobalObject(19438, new Vector3(242.210007f, 1861.229980f, 11.090000f), new Vector3(0.00000f, 90.00000f, 90.0f), 300.0f);
            gbw3.SetMaterial(0, 5267, "lashops91_las2", "laspowrec2", 0);
            //fso_map = CreateDynamicObject(19365, 241.440002, 1862.890015, 9.430000, 0.000000, 0.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0);
            GlobalObject gbw4 = new GlobalObject(19365, new Vector3(241.440002, 1862.890015, 9.430000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            gbw4.SetMaterial(0, 5267, "lashops91_las2", "laspowrec2", 0);
            //fso_map = CreateDynamicObject(19394, 241.619995, 1857.839966, 9.430000, 0.000000, 0.000000, 0.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0);
            GlobalObject gbw5 = new GlobalObject(19394, new Vector3(241.619995, 1857.839966, 9.430000), new Vector3(0.00000f, 0.00000f, 0.0f), 300.0f);
            gbw5.SetMaterial(0, 5267, "lashops91_las2", "laspowrec2", 0);
            //fso_map = CreateDynamicObject(19365, 241.619995, 1854.630005, 9.430000, 0.000000, 0.000000, 0.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0);
            GlobalObject gbw6 = new GlobalObject(19365, new Vector3(241.619995, 1854.630005, 9.430000), new Vector3(0.00000f, 0.00000f, 0.0f), 300.0f);
            gbw6.SetMaterial(0, 5267, "lashops91_las2", "laspowrec2", 0);

            //fso_map = CreateDynamicObject(19365, 241.619995, 1851.420044, 9.430000, 0.000000, 0.000000, 0.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0);
            GlobalObject gbw7 = new GlobalObject(19365, new Vector3(241.619995, 1851.420044, 9.430000), new Vector3(0.00000f, 0.00000f, 0.0f), 300.0f);
            gbw7.SetMaterial(0, 5267, "lashops91_las2", "laspowrec2", 0);
            //fso_map = CreateDynamicObject(19365, 240.169998, 1861.229980, 11.100000, 0.000000, 90.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0);
            GlobalObject gbw8 = new GlobalObject(19365, new Vector3(240.169998, 1861.229980, 11.100000), new Vector3(0.00000f, 90.00000f, 90.0f), 300.0f);
            gbw8.SetMaterial(0, 5267, "lashops91_las2", "laspowrec2", 0);
            //fso_map = CreateDynamicObject(19365, 240.080002, 1858.560059, 11.109999, 0.000000, 90.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0);
            GlobalObject gbw9 = new GlobalObject(19365, new Vector3(240.080002, 1858.560059, 11.109999), new Vector3(0.00000f, 90.00000f, 90.0f), 300.0f);
            gbw9.SetMaterial(0, 5267, "lashops91_las2", "laspowrec2", 0);
            //fso_map = CreateDynamicObject(19365, 240.080002, 1855.060059, 11.109999, 0.000000, 90.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0);
            GlobalObject gbw10 = new GlobalObject(19365, new Vector3(240.080002, 1855.060059, 11.109999), new Vector3(0.00000f, 90.00000f, 90.0f), 300.0f);
            gbw10.SetMaterial(0, 5267, "lashops91_las2", "laspowrec2", 0);
            //fso_map = CreateDynamicObject(19365, 240.080002, 1851.569946, 11.109999, 0.000000, 90.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0);
            GlobalObject gbw11 = new GlobalObject(19365, new Vector3(240.080002, 1851.569946, 11.109999), new Vector3(0.00000f, 90.00000f, 90.0f), 300.0f);
            gbw11.SetMaterial(0, 5267, "lashops91_las2", "laspowrec2", 0);
            //fso_map = CreateDynamicObject(19394, 240.110001, 1849.900024, 9.430000, 0.000000, 0.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0);
            GlobalObject gbw12 = new GlobalObject(19394, new Vector3(240.110001, 1849.900024, 9.430000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            gbw12.SetMaterial(0, 5267, "lashops91_las2", "laspowrec2", 0);
            //fso_map = CreateDynamicObject(19365, 238.229996, 1862.890015, 9.430000, 0.000000, 0.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 5267, "lashops91_las2", "laspowrec2", 0);
            GlobalObject gbw13 = new GlobalObject(19365, new Vector3(238.229996, 1862.890015, 9.430000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            gbw13.SetMaterial(0, 5267, "lashops91_las2", "laspowrec2", 0);
            //CreateDynamicObject(1829, 239.100006, 1857.819946, 9.170000, 0.000000, 0.000000, 90.000000, -1, -1);
            GlobalObject gbsafe = new GlobalObject(1829, new Vector3(239.100006, 1857.819946, 9.170000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            //CreateDynamicObject(2185, 239.029999, 1857.219971, 7.760000, 0.000000, 0.000000, -90.000000, -1, -1)
            GlobalObject gbtable = new GlobalObject(2185, new Vector3(239.029999, 1857.219971, 7.760000), new Vector3(0.00000f, 0.00000f, -90.0f), 300.0f);
            //fso_map = CreateDynamicObject(2653, 301.188934, 1832.943726, 6.392000, 0.000000, 0.000000, 0.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 0, 16640, "a51", "airvent_gz", 0);
            GlobalObject gbvent = new GlobalObject(2653, new Vector3(301.188934, 1832.943726, 6.392000), new Vector3(0.00000f, 0.00000f, 0.0f), 300.0f);
            gbvent.SetMaterial(0, 16640, "a51", "airvent_gz", 0);
            //fso_map = CreateDynamicObject(1495, 241.660004, 1857.109985, 7.760000, 0.000000, 0.000000, 90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 2, 18757, "vcinteriors", "dt_officewall3", 0);
            GlobalObject gbdoor1 = new GlobalObject(1495, new Vector3(241.660004, 1857.109985, 7.760000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            gbdoor1.SetMaterial(2, 18757, "vcinteriors", "dt_officewall3", 0);
            //fso_map = CreateDynamicObject(1495, 239.330002, 1849.880005, 7.719999, 0.000000, 0.000000, 0.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 2, 18757, "vcinteriors", "dt_officewall3", 0);
            GlobalObject gbdoor2 = new GlobalObject(1495, new Vector3(239.330002, 1849.880005, 7.719999), new Vector3(0.00000f, 0.00000f, 00.0f), 300.0f);
            gbdoor2.SetMaterial(2, 18757, "vcinteriors", "dt_officewall3", 0);
            //CreateDynamicObject(2007, 241.500000, 1862.300049, 9.140000, 0.000000, 0.000000, 0.000000, -1, -1);
            //CreateDynamicObject(2007, 241.500000, 1862.300049, 7.740000, 0.000000, 0.00000, 0.000000, -1, -1);
            GlobalObject gbshelf1 = new GlobalObject(2007, new Vector3(241.500000, 1862.300049, 9.140000), new Vector3(0.00000f, 0.00000f, 00.0f), 300.0f);
            GlobalObject gbshelf2 = new GlobalObject(2007, new Vector3(241.500000, 1862.300049, 7.740000), new Vector3(0.00000f, 0.00000f, 00.0f), 300.0f);
            //CreateDynamicObject(2007, 240.509995, 1862.300049, 9.140000, 0.000000, 0.000000, 0.000000, -1, -1);
            //CreateDynamicObject(2007, 240.509995, 1862.300049, 7.740000, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject gbshelf3 = new GlobalObject(2007, new Vector3(240.509995, 1862.300049, 9.140000), new Vector3(0.00000f, 0.00000f, 00.0f), 300.0f);
            GlobalObject gbshelf4 = new GlobalObject(2007, new Vector3(240.509995, 1862.300049, 7.740000), new Vector3(0.00000f, 0.00000f, 00.0f), 300.0f);
            //CreateDynamicObject(2007, 239.500000, 1862.300049, 9.130000, 0.000000, 0.000000, 0.000000, -1, -1);
            //CreateDynamicObject(2007, 239.500000, 1862.300049, 7.740000, 0.000000, 0.000000, 0.000000, -1, -1);
            GlobalObject gbshelf5 = new GlobalObject(2007, new Vector3(239.500000, 1862.300049, 9.130000), new Vector3(0.00000f, 0.00000f, 00.0f), 300.0f);
            GlobalObject gbshelf6 = new GlobalObject(2007, new Vector3(239.500000, 1862.300049, 7.740000), new Vector3(0.00000f, 0.00000f, 00.0f), 300.0f);
            //CreateDynamicObject(2007, 239.029999, 1861.800049, 9.130000, 0.000000, 0.000000, 90.000000, -1, -1);
            //CreateDynamicObject(2007, 239.029999, 1860.800049, 9.130000, 0.000000, 0.000000, 90.000000, -1, -1);
            GlobalObject gbshelf7 = new GlobalObject(2007, new Vector3(239.029999, 1861.800049, 9.130000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            GlobalObject gbshelf8 = new GlobalObject(2007, new Vector3(239.029999, 1860.800049, 9.130000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            //CreateDynamicObject(2007, 239.039993, 1861.800049, 7.740000, 0.000000, 0.000000, 90.000000, -1, -1);
            //CreateDynamicObject(2007, 239.039993, 1860.800049, 7.740000, 0.000000, 0.000000, 90.000000, -1, -1);
            GlobalObject gbshelf9 = new GlobalObject(2007, new Vector3(239.039993, 1861.800049, 7.740000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            GlobalObject gbshelf10 = new GlobalObject(2007, new Vector3(239.039993, 1860.800049, 7.740000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            //CreateDynamicObject(2007, 239.029999, 1859.800049, 9.130000, 0.000000, 0.000000, 90.000000, -1, -1);
            //CreateDynamicObject(2007, 239.029999, 1859.800049, 7.740000, 0.000000, 0.000000, 90.000000, -1, -1);
            GlobalObject gbshelf11 = new GlobalObject(2007, new Vector3(239.029999, 1859.800049, 9.130000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            GlobalObject gbshelf12 = new GlobalObject(2007, new Vector3(239.029999, 1859.800049, 7.740000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            //fso_map = CreateDynamicObject(936, 288.740509, 1833.488770, 7.456299, 0.000000, 0.000000, -90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 1, 3979, "civic01_lan", "sl_laglasswall1", 0);
            //SetDynamicObjectMaterial(fso_map, 0, 4828, "airport3_las", "brwall_128", 0);
            GlobalObject gbtable1 = new GlobalObject(936, new Vector3(288.740509, 1833.488770, 7.456299), new Vector3(0.00000f, 0.00000f, -90.0f), 300.0f);
            gbtable1.SetMaterial(1, 3979, "civic01_lan", "sl_laglasswall1", 0);
            gbtable1.SetMaterial(0, 4828, "airport3_las", "brwall_128", 0);
            //fso_map = CreateDynamicObject(936, 288.740204, 1831.707642, 7.456200, 0.000000, 0.000000, -90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 1, 3979, "civic01_lan", "sl_laglasswall1", 0);
            //SetDynamicObjectMaterial(fso_map, 0, 4828, "airport3_las", "brwall_128", 0);
            GlobalObject gbtable2 = new GlobalObject(936, new Vector3(288.740204, 1831.707642, 7.456200), new Vector3(0.00000f, 0.00000f, -90.0f), 300.0f);
            gbtable2.SetMaterial(1, 3979, "civic01_lan", "sl_laglasswall1", 0);
            gbtable2.SetMaterial(0, 4828, "airport3_las", "brwall_128", 0);
            //fso_map = CreateDynamicObject(936, 288.740509, 1829.917603, 7.456399, 0.000000, 0.000000, -90.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 1, 3979, "civic01_lan", "sl_laglasswall1", 0);
            //SetDynamicObjectMaterial(fso_map, 0, 4828, "airport3_las", "brwall_128", 0);
            GlobalObject gbtable3 = new GlobalObject(936, new Vector3(288.740509, 1829.917603, 7.456399), new Vector3(0.00000f, 0.00000f, -90.0f), 300.0f);
            gbtable3.SetMaterial(1, 3979, "civic01_lan", "sl_laglasswall1", 0);
            gbtable3.SetMaterial(0, 4828, "airport3_las", "brwall_128", 0);

            //CreateDynamicObject(936, 239.100006, 1858.280029, 8.220000, 0.000000, 0.000000, 90.000000, -1, -1);
            GlobalObject gbtable4 = new GlobalObject(936, new Vector3(239.100006, 1858.280029, 8.220000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);
            //fso_map = CreateDynamicObject(936, 306.483032, 1836.241577, 6.380169, 0.000000, 0.000000, 0.000000, -1, -1);
            //SetDynamicObjectMaterial(fso_map, 1, 16322, "a51_stores", "dish_panel_a", 0);
            //SetDynamicObjectMaterial(fso_map, 0, 14652, "ab_trukstpa", "CJ_WOOD6", 0);
            GlobalObject gbtable5 = new GlobalObject(936, new Vector3(306.483032, 1836.241577, 6.380169), new Vector3(0.00000f, 0.00000f, 0.0f), 300.0f);
            gbtable5.SetMaterial(1, 3979, "a51_stores", "dish_panel_a", 0);
            gbtable5.SetMaterial(0, 4828, "ab_trukstpa", "CJ_WOOD6", 0);
            //CreateDynamicObject(1310, 238.550003, 1857.829956, 9.750000, 0.000000, 0.000000, 90.000000, -1, -1);
            GlobalObject gbpara = new GlobalObject(1310, new Vector3(238.550003, 1857.829956, 9.750000), new Vector3(0.00000f, 0.00000f, 90.0f), 300.0f);


            GlobalObject.Remove(player,16641, new Vector3(251.445297f, 1866.304688f, 9.289100f), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16322, 248.726593, 1869.984375, 11.851600, 0.250000);
            GlobalObject.Remove(player, 16322, new Vector3(248.726593f, 1869.984375f, 11.851600f), 0.25f);
            //RemoveBuildingForPlayer(playerid, 16666, 246.328094, 1827.648438, 3.804700, 0.250000);
            GlobalObject.Remove(player, 16666, new Vector3(246.328094f, 1827.648438f, 3.804700f), 0.25f);
            //RemoveBuildingForPlayer(playerid, 3388, 271.226593, 1870.929688, 7.757800, 0.250000);
            GlobalObject.Remove(player, 3388, new Vector3(271.226593f, 1870.929688f, 7.757800f), 0.25f);
            //RemoveBuildingForPlayer(playerid, 3387, 271.250000, 1869.695313, 7.757800, 0.250000);
            GlobalObject.Remove(player, 3387, new Vector3(271.25f, 1869.695313f, 7.757800f), 0.25f);
            //RemoveBuildingForPlayer(playerid, 3386, 271.265594, 1868.187500, 7.757800, 0.250000);
            GlobalObject.Remove(player, 3388, new Vector3(271.265594f, 1868.187500f, 7.757800f), 0.25f);
            //RemoveBuildingForPlayer(playerid, 3395, 280.328094, 1874.234375, 7.750000, 0.250000);
            GlobalObject.Remove(player, 3395, new Vector3(280.328094f, 1874.234375f, 7.750000f), 0.25f);
            //RemoveBuildingForPlayer(playerid, 3384, 276.593811, 1868.960938, 9.179700, 0.250000);
            GlobalObject.Remove(player, 3384, new Vector3(276.593811f, 1868.960938f, 9.179700f), 0.25f);
            //RemoveBuildingForPlayer(playerid, 3384, 276.593811, 1870.195313, 9.179700, 0.250000);
            GlobalObject.Remove(player, 3384, new Vector3(276.593811f, 1870.195313f, 9.179700f), 0.25f);
            //RemoveBuildingForPlayer(playerid, 3394, 271.789093, 1852.953125, 7.750000, 0.250000);
            GlobalObject.Remove(player, 3394, new Vector3(271.789093f, 1852.953125f, 7.750000f), 0.25f);
            // RemoveBuildingForPlayer(playerid, 3395, 276.273407, 1852.937500, 7.750000, 0.250000);
            GlobalObject.Remove(player, 3395, new Vector3(276.273407f, 1852.937500f, 7.750000f), 0.25f);//
            // RemoveBuildingForPlayer(playerid, 3397, 280.656311, 1855.523438, 7.750000, 0.250000);
            GlobalObject.Remove(player, 3397, new Vector3(271.789093f, 1852.953125f, 7.750000f), 0.25f);//
            //  RemoveBuildingForPlayer(playerid, 3383, 275.375000, 1859.148438, 7.757800, 0.250000);
            GlobalObject.Remove(player, 3383, new Vector3(275.375000f, 1859.148438f, 7.757800f), 0.25f);//
            // RemoveBuildingForPlayer(playerid, 3383, 269.093811, 1858.320313, 7.757800, 0.250000);
            // GlobalObject.Remove(player, 3383, new Vector3(269.093811f, 1858.320313f, 7.757800f), 0.25f);//
            //   RemoveBuildingForPlayer(playerid, 3389, 266.867188, 1852.906250, 7.757800, 0.250000);
            GlobalObject.Remove(player, 3389, new Vector3(266.867188f, 1852.906250f, 7.757800f), 0.25f);//
            //  RemoveBuildingForPlayer(playerid, 3396, 275.312500, 1874.242188, 7.750000, 0.250000);
            GlobalObject.Remove(player, 3396, new Vector3(275.312500f, 1874.242188f, 7.750000f), 0.25f);//
            //  RemoveBuildingForPlayer(playerid, 3384, 278.093811, 1869.101563, 9.179700, 0.250000);
            GlobalObject.Remove(player, 3384, new Vector3(278.093811f, 1869.101563f, 9.179700f), 0.250000f);//
            //   RemoveBuildingForPlayer(playerid, 3384, 278.093811, 1870.335938, 9.179700, 0.250000);
            GlobalObject.Remove(player, 3384, new Vector3(278.093811f, 1870.335938f, 9.179700f), 0.25f); //
           // RemoveBuildingForPlayer(playerid, 3260, 259.023407, 1861.906250, 8.757800, 0.250000);
           GlobalObject.Remove(player, 3260, new Vector3(259.023407f, 1861.906250f, 8.757800f), 0.25f); //
            //  RemoveBuildingForPlayer(playerid, 3260, 259.968811, 1864.937500, 8.757800, 0.250000);
            GlobalObject.Remove(player, 3260, new Vector3(259.968811f, 1864.937500f, 8.757800f), 0.25f);//
            //   RemoveBuildingForPlayer(playerid, 3260, 258.078094, 1862.953125, 8.757800, 0.250000);
            GlobalObject.Remove(player, 3260, new Vector3(258.078094f, 1862.953125f, 8.757800f), 0.25f); //
            //   RemoveBuildingForPlayer(playerid, 3260, 259.968811, 1866.921875, 8.757800, 0.250000);
            GlobalObject.Remove(player, 3260, new Vector3(259.968811f, 1866.921875f, 8.757800f), 0.25f);//
            //   RemoveBuildingForPlayer(playerid, 3260, 258.078094, 1866.921875, 8.757800, 0.250000);
            GlobalObject.Remove(player, 3260, new Vector3(258.078094f, 1866.921875f, 8.757800f), 0.25f);//
            

        }

    }
}