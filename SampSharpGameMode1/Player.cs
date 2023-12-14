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

namespace SampSharpGameMode1
{

    [PooledType]
    public class Player : BasePlayer
    {
        int PlayerBooms = 0;
        MySqlConnection sqlCon = new MySqlConnection("server=localhost;port=3306;username=root;password=;database=Samp");// CONNECTION VAR

        //TEXTDRAWS!!!!
            TextDraw booms = new TextDraw(new SampSharp.GameMode.Vector2(513.0f, 9.0f), " ");
            

            TextDraw immunity = new TextDraw( new SampSharp.GameMode.Vector2(476.0f, 100.0f), "HUD:radar_girlfriend");
           
            //ЗНАЧЕНИЕ ИММУНИТЕТА
            
            TextDraw immunityNum = new TextDraw( new SampSharp.GameMode.Vector2(478.0f, 116.0f), "");
            
            TextDraw voda = new TextDraw( new SampSharp.GameMode.Vector2(516.0f, 100.0f), "HUD:radar_diner");
            
            //ЗНАЧЕНИЕ ЖАЖДЫ
            
            TextDraw vodaNum = new TextDraw( new SampSharp.GameMode.Vector2(518.0f, 116.0f), "");
           
            //______________________________________________________________________________________________________________________
            TextDraw hungry = new TextDraw( new SampSharp.GameMode.Vector2(496.0f, 100.0f), "HUD:radar_datefood");
            
            //EDA NUMBEROF
            
            TextDraw hungryNum = new TextDraw( new SampSharp.GameMode.Vector2(499.0f, 116.0f), " ");
            
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            TextDraw holod = new TextDraw( new SampSharp.GameMode.Vector2(536.0f, 100.0f), "HUD:radar_tshirt");
            
            //HOLOD NUMBER
            
            TextDraw holodNum = new TextDraw( new SampSharp.GameMode.Vector2(539.0f, 116.0f), "");
           



            TextDraw sleep = new TextDraw( new SampSharp.GameMode.Vector2(556.0f, 100.0f), "HUD:radar_propertyg");
            
            //SLEEPNUMBER
            TextDraw sleepNum = new TextDraw( new SampSharp.GameMode.Vector2(558.0f, 116.0f), "");
            

            TextDraw piss = new TextDraw( new SampSharp.GameMode.Vector2(575.0f, 100.0f), "HUD:radar_centre");
            
            //piss num
            TextDraw pissNum = new TextDraw( new SampSharp.GameMode.Vector2(577.0f, 116.0f), "");
            



            TextDraw nastroy = new TextDraw( new SampSharp.GameMode.Vector2(593.0f, 100.0f), "HUD:radar_mafiacasino");
            
            //nastroyNUM
            TextDraw nastroyNum = new TextDraw( new SampSharp.GameMode.Vector2(595.0f, 116.0f), "");
           

            TextDraw gigiena = new TextDraw( new SampSharp.GameMode.Vector2(613.0f, 100.0f), "ld_grav:flwr");
            
            //GIGIENA NUMBER
            TextDraw gigienaNum = new TextDraw( new SampSharp.GameMode.Vector2(615.0f, 116.0f), "");
            


            TextDraw bleed = new TextDraw( new SampSharp.GameMode.Vector2(476.0f, 80.0f), "particle:bloodpool_64");
            
            
            

            TextDraw bolezn = new TextDraw( new SampSharp.GameMode.Vector2(476.0f, 60.0f), "HUD:radar_hostpital");
            

            TextDraw dangerPNG = new TextDraw( new SampSharp.GameMode.Vector2(476.0f, 40.0f), "HUD:radar_locosyndicate");
            

            TextDraw vivihPNG = new TextDraw( new SampSharp.GameMode.Vector2(476.0f, 20.0f), "particle:handman");
            

            TextDraw BackPlate = new TextDraw( new SampSharp.GameMode.Vector2(75.0f, 318.0f), "_");
            

            TextDraw TimePNG = new TextDraw( new SampSharp.GameMode.Vector2(88.0f, 319.0f), "00:00");
            

            TextDraw temperaturePNG = new TextDraw( new SampSharp.GameMode.Vector2(10.0f, 321.0f), "t : 22");
            

            TextDraw virusPNG = new TextDraw( new SampSharp.GameMode.Vector2(10.0f, 427.0f), "virus : 30%");
            
        public override void OnConnected(EventArgs e)
        {
            base.OnConnected(e);

            
            // base.CameraPosition = new SampSharp.GameMode.Vector3(189.96f, 1918.68, 29.92);

            Autorization();

        }
        [Command("MakeGun")]
        private void MakeGun(Weapon WeaponId, int Bullets)
        {
            PlayerBooms += Bullets;
            GiveWeapon(WeaponId, Bullets);
            booms.Text = PlayerBooms.ToString();
        }
        public override void OnRequestSpawn(RequestSpawnEventArgs e)
        {
            base.OnRequestSpawn(e);

            
            Kick();

        }

        public override void OnWeaponShot(WeaponShotEventArgs e)
        {
            base.OnWeaponShot(e);

            SampSharp.GameMode.Vector3 shotVec = e.Position;
            PlayerBooms--;
            booms.Text = PlayerBooms.ToString();
            CreateExplosionForAll(shotVec, ExplosionType.LargeVisibleDamage, 30);

        }
        public override void OnSpawned(SpawnEventArgs e)
        {
            
            base.OnSpawned(e);
            sqlCon.Open();
            var getSkin = new MySqlCommand($"SELECT `SkinId` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
            SetSpawnInfo(0, Convert.ToInt32(getSkin.ExecuteScalar()), new SampSharp.GameMode.Vector3(x: 275.11f, y: 1861.51f, z: 8.75), 355.68f);
            this.Skin = Convert.ToInt32(getSkin.ExecuteScalar());

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
            booms.Show(this);

            
            immunity.Font = TextDrawFont.DrawSprite;
            immunity.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            immunity.Outline = 1;
            immunity.Width = 16.0f;
            immunity.Height = 15.5f;
            immunity.Shadow = 0;
            immunity.Alignment = TextDrawAlignment.Left;
            immunity.ForeColor = -1;
            immunity.BackColor = 255;
            immunity.BoxColor = 50;
            immunity.UseBox = true;
            immunity.Proportional = true;
            immunity.Selectable = false;
            immunity.Show(this);
            //ЗНАЧЕНИЕ ИММУНИТЕТА
            
            immunityNum.Text = (getImmunity.ExecuteScalar().ToString());
            immunityNum.Font = TextDrawFont.Normal;
            immunityNum.LetterSize = new SampSharp.GameMode.Vector2(0.179167f, 1.0f);
            immunityNum.Outline = 1;
            immunityNum.Width = 16.0f;
            immunityNum.Height = 15.5f;
            immunityNum.Shadow = 0;
            immunityNum.Alignment = TextDrawAlignment.Left;
            immunityNum.ForeColor = 1433087999;
            immunityNum.BackColor = 255;
            immunityNum.BoxColor = 50;
            immunityNum.UseBox = false;
            immunityNum.Proportional = true;
            immunityNum.Selectable = false;
            immunityNum.Show(this);

            
            voda.Font = TextDrawFont.DrawSprite;
            voda.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            voda.Outline = 1;
            voda.Width = 16.0f;
            voda.Height = 15.5f;
            voda.Shadow = 0;
            voda.Alignment = TextDrawAlignment.Left;
            voda.ForeColor = -1;
            voda.BackColor = 255;
            voda.BoxColor = 50;
            voda.UseBox = true;
            voda.Proportional = true;
            voda.Selectable = false;
            voda.Show(this);
            //ЗНАЧЕНИЕ ЖАЖДЫ
            
            vodaNum.Text = (getVoda.ExecuteScalar().ToString());
            vodaNum.Font = TextDrawFont.Normal;
            vodaNum.LetterSize = new SampSharp.GameMode.Vector2(0.179167f, 1.0f);
            vodaNum.Outline = 1;
            vodaNum.Width = 16.0f;
            vodaNum.Height = 15.5f;
            vodaNum.Shadow = 0;
            vodaNum.Alignment = TextDrawAlignment.Left;
            vodaNum.ForeColor = 1433087999;
            vodaNum.BackColor = 255;
            vodaNum.BoxColor = 50;
            vodaNum.UseBox = false;
            vodaNum.Proportional = true;
            vodaNum.Selectable = false;
            vodaNum.Show(this);
            //______________________________________________________________________________________________________________________
            
            hungry.Font = TextDrawFont.DrawSprite;
            hungry.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            hungry.Outline = 1;
            hungry.Width = 16.0f;
            hungry.Height = 15.5f;
            hungry.Shadow = 0;
            hungry.Alignment = TextDrawAlignment.Left;
            hungry.ForeColor = -1;
            hungry.BackColor = 255;
            hungry.BoxColor = 50;
            hungry.UseBox = true;
            hungry.Proportional = true;
            hungry.Selectable = false;
            hungry.Show(this);
            //EDA NUMBEROF
            
            hungryNum.Text = (getFood.ExecuteScalar().ToString());
            hungryNum.Font = TextDrawFont.Normal;
            hungryNum.LetterSize = new SampSharp.GameMode.Vector2(0.179167f, 1.0f);
            hungryNum.Outline = 1;
            hungryNum.Width = 16.0f;
            hungryNum.Height = 15.5f;
            hungryNum.Shadow = 0;
            hungryNum.Alignment = TextDrawAlignment.Left;
            hungryNum.ForeColor = 1433087999;
            hungryNum.BackColor = 255;
            hungryNum.BoxColor = 50;
            hungryNum.UseBox = false;
            hungryNum.Proportional = true;
            hungryNum.Selectable = false;
            hungryNum.Show(this);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            
            holod.Font = TextDrawFont.DrawSprite;
            holod.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            holod.Outline = 1;
            holod.Width = 16.0f;
            holod.Height = 15.5f;
            holod.Shadow = 0;
            holod.Alignment = TextDrawAlignment.Left;
            holod.ForeColor = -1;
            holod.BackColor = 255;
            holod.BoxColor = 50;
            holod.UseBox = true;
            holod.Proportional = true;
            holod.Selectable = false;
            holod.Show(this);
            //HOLOD NUMBER
            
            holodNum.Text = (getHolod.ExecuteScalar().ToString());
            holodNum.Font = TextDrawFont.Normal;
            holodNum.LetterSize = new SampSharp.GameMode.Vector2(0.179167f, 1.0f);
            holodNum.Outline = 1;
            holodNum.Width = 16.0f;
            holodNum.Height = 15.5f;
            holodNum.Shadow = 0;
            holodNum.Alignment = TextDrawAlignment.Left;
            holodNum.ForeColor = 1433087999;
            holodNum.BackColor = 255;
            holodNum.BoxColor = 50;
            holodNum.UseBox = false;
            holodNum.Proportional = true;
            holodNum.Selectable = false;
            holodNum.Show(this);



            
            sleep.Font = TextDrawFont.DrawSprite;
            sleep.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            sleep.Outline = 1;
            sleep.Width = 16.0f;
            sleep.Height = 15.5f;
            sleep.Shadow = 0;
            sleep.Alignment = TextDrawAlignment.Left;
            sleep.ForeColor = -1;
            sleep.BackColor = 255;
            sleep.BoxColor = 50;
            sleep.UseBox = true;
            sleep.Proportional = true;
            sleep.Selectable = false;
            sleep.Show(this);
            //SLEEPNUMBER
            sleepNum.Text = (getSleep.ExecuteScalar().ToString());
            sleepNum.Font = TextDrawFont.Normal;
            sleepNum.LetterSize = new SampSharp.GameMode.Vector2(0.179167f, 1.0f);
            sleepNum.Outline = 1;
            sleepNum.Width = 16.0f;
            sleepNum.Height = 15.5f;
            sleepNum.Shadow = 0;
            sleepNum.Alignment = TextDrawAlignment.Left;
            sleepNum.ForeColor = 1433087999;
            sleepNum.BackColor = 255;
            sleepNum.BoxColor = 50;
            sleepNum.UseBox = false;
            sleepNum.Proportional = true;
            sleepNum.Selectable = false;
            sleepNum.Show(this);

            
            piss.Font = TextDrawFont.DrawSprite;
            piss.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            piss.Outline = 1;
            piss.Width = 15.0f;
            piss.Height = 14.5f;
            piss.Shadow = 0;
            piss.Alignment = TextDrawAlignment.Left;
            piss.ForeColor = -1;
            piss.BackColor = 255;
            piss.BoxColor = 50;
            piss.UseBox = true;
            piss.Proportional = true;
            piss.Selectable = false;
            piss.Show(this);
            //piss num
            pissNum.Text = (getPiss.ExecuteScalar().ToString());
            pissNum.Font = TextDrawFont.Normal;
            pissNum.LetterSize = new SampSharp.GameMode.Vector2(0.179167f, 1.0f);
            pissNum.Outline = 1;
            pissNum.Width = 16.0f;
            pissNum.Height = 15.5f;
            pissNum.Shadow = 0;
            pissNum.Alignment = TextDrawAlignment.Left;
            pissNum.ForeColor = 1433087999;
            pissNum.BackColor = 255;
            pissNum.BoxColor = 50;
            pissNum.UseBox = false;
            pissNum.Proportional = true;
            pissNum.Selectable = false;
            pissNum.Show(this);



            
            nastroy.Font = TextDrawFont.DrawSprite;
            nastroy.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            nastroy.Outline = 1;
            nastroy.Width = 16.0f;
            nastroy.Height = 15.5f;
            nastroy.Shadow = 0;
            nastroy.Alignment = TextDrawAlignment.Left;
            nastroy.ForeColor = -1;
            nastroy.BackColor = 255;
            nastroy.BoxColor = 50;
            nastroy.UseBox = true;
            nastroy.Proportional = true;
            nastroy.Selectable = false;
            nastroy.Show(this);
            //nastroyNUM
            nastroyNum.Text = (getNastroy.ExecuteScalar().ToString());
            nastroyNum.Font = TextDrawFont.Normal;
            nastroyNum.LetterSize = new SampSharp.GameMode.Vector2(0.179167f, 1.0f);
            nastroyNum.Outline = 1;
            nastroyNum.Width = 16.0f;
            nastroyNum.Height = 15.5f;
            nastroyNum.Shadow = 0;
            nastroyNum.Alignment = TextDrawAlignment.Left;
            nastroyNum.ForeColor = 1433087999;
            nastroyNum.BackColor = 255;
            nastroyNum.BoxColor = 50;
            nastroyNum.UseBox = false;
            nastroyNum.Proportional = true;
            nastroyNum.Selectable = false;
            nastroyNum.Show(this);

            
            gigiena.Font = TextDrawFont.DrawSprite;
            gigiena.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            gigiena.Outline = 1;
            gigiena.Width = 16.0f;
            gigiena.Height = 15.5f;
            gigiena.Shadow = 0;
            gigiena.Alignment = TextDrawAlignment.Left;
            gigiena.ForeColor = -1;
            gigiena.BackColor = 255;
            gigiena.BoxColor = 50;
            gigiena.UseBox = true;
            gigiena.Proportional = true;
            gigiena.Selectable = false;
            gigiena.Show(this);
            //GIGIENA NUMBER
            gigienaNum.Text = (getGigiena.ExecuteScalar().ToString());
            gigienaNum.Font = TextDrawFont.Normal;
            gigienaNum.LetterSize = new SampSharp.GameMode.Vector2(0.179167f, 1.0f);
            gigienaNum.Outline = 1;
            gigienaNum.Width = 16.0f;
            gigienaNum.Height = 15.5f;
            gigienaNum.Shadow = 0;
            gigienaNum.Alignment = TextDrawAlignment.Left;
            gigienaNum.ForeColor = 1433087999;
            gigienaNum.BackColor = 255;
            gigienaNum.BoxColor = 50;
            gigienaNum.UseBox = false;
            gigienaNum.Proportional = true;
            gigienaNum.Selectable = false;
            gigienaNum.Show(this);


            
            bleed.Font = TextDrawFont.DrawSprite;
            bleed.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            bleed.Outline = 1;
            bleed.Width = 16.0f;
            bleed.Height = 15.5f;
            bleed.Shadow = 0;
            bleed.Alignment = TextDrawAlignment.Left;
            bleed.ForeColor = -1;
            bleed.BackColor = 255;
            bleed.BoxColor = 50;
            bleed.UseBox = true;
            bleed.Proportional = true;
            bleed.Selectable = false;
            bleed.Show(this);
            if (Convert.ToInt32(getBleed.ExecuteScalar()) == 0)
            {
                bleed.ForeColor = new SampSharp.GameMode.SAMP.Color(0 , 0 , 0);
            }
            else
            {
                bleed.ForeColor = -1;
            }
            

            
            bolezn.Font = TextDrawFont.DrawSprite;
            bolezn.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            bolezn.Outline = 1;
            bolezn.Width = 16.0f;
            bolezn.Height = 15.5f;
            bolezn.Shadow = 0;
            bolezn.Alignment = TextDrawAlignment.Left;
            bolezn.ForeColor = -1;
            bolezn.BackColor = 255;
            bolezn.BoxColor = 50;
            bolezn.UseBox = true;
            bolezn.Proportional = true;
            bolezn.Selectable = false;
            bolezn.Show(this);

            if (Convert.ToInt32(getBolezn.ExecuteScalar()) == 0)
            {
                bolezn.ForeColor = new SampSharp.GameMode.SAMP.Color(0, 0, 0);
            }
            else
            {
                bolezn.ForeColor = -1;
            }

            
            dangerPNG.Font = TextDrawFont.DrawSprite;
            dangerPNG.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            dangerPNG.Outline = 1;
            dangerPNG.Width = 16.0f;
            dangerPNG.Height = 15.5f;
            dangerPNG.Shadow = 0;
            dangerPNG.Alignment = TextDrawAlignment.Left;
            dangerPNG.ForeColor = -1;
            dangerPNG.BackColor = 255;
            dangerPNG.BoxColor = 50;
            dangerPNG.UseBox = true;
            dangerPNG.Proportional = true;
            dangerPNG.Selectable = false;
            dangerPNG.Show(this);

            if (Convert.ToInt32(getDanger.ExecuteScalar()) == 0)
            {
                dangerPNG.ForeColor = new SampSharp.GameMode.SAMP.Color(0, 0, 0);
            }
            else
            {
                dangerPNG.ForeColor = -1;
            }


            
            vivihPNG.Font = TextDrawFont.DrawSprite;
            vivihPNG.LetterSize = new SampSharp.GameMode.Vector2(0.6f, 2.0f);
            vivihPNG.Outline = 1;
            vivihPNG.Width = 16.0f;
            vivihPNG.Height = 15.5f;
            vivihPNG.Shadow = 0;
            vivihPNG.Alignment = TextDrawAlignment.Left;
            vivihPNG.ForeColor = -1;
            vivihPNG.BackColor = 255;
            vivihPNG.BoxColor = 50;
            vivihPNG.UseBox = true;
            vivihPNG.Proportional = true;
            vivihPNG.Selectable = false;
            vivihPNG.Show(this);
            if (Convert.ToInt32(getVivih.ExecuteScalar()) == 0)
            {
                vivihPNG.ForeColor = new SampSharp.GameMode.SAMP.Color(0, 0, 0);
            }
            else
            {
                vivihPNG.ForeColor = -1;
            }

            
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
            BackPlate.Show(this);

            
            TimePNG.Font = TextDrawFont.Pricedown;
            TimePNG.LetterSize = new SampSharp.GameMode.Vector2(0.279166f, 1.299998f);
            TimePNG.Outline = 2;
            TimePNG.Width = 405.5f;
            TimePNG.Height = 28.0f;
            TimePNG.Shadow = 0;
            TimePNG.Alignment = TextDrawAlignment.Center;
            TimePNG.ForeColor = -1;
            TimePNG.BackColor = 255;
            TimePNG.BoxColor = 50;
            TimePNG.UseBox = true;
            TimePNG.Proportional = true;
            TimePNG.Selectable = false;
            TimePNG.Show(this);

            
            temperaturePNG.Font = TextDrawFont.Normal;
            temperaturePNG.LetterSize = new SampSharp.GameMode.Vector2(0.187500f, 1.2f);
            temperaturePNG.Outline = 1;
            temperaturePNG.Width = 400.0f;
            temperaturePNG.Height = 17.0f;
            temperaturePNG.Shadow = 0;
            temperaturePNG.Alignment = TextDrawAlignment.Left;
            temperaturePNG.ForeColor = -1;
            temperaturePNG.BackColor = 255;
            temperaturePNG.BoxColor = 50;
            temperaturePNG.UseBox = false;
            temperaturePNG.Proportional = true;
            temperaturePNG.Selectable = false;
            temperaturePNG.Show(this);

            
            virusPNG.Font = TextDrawFont.Normal;
            virusPNG.LetterSize = new SampSharp.GameMode.Vector2(0.187500f, 1.2f);
            virusPNG.Outline = 1;
            virusPNG.Width = 400.0f;
            virusPNG.Height = 17.0f;
            virusPNG.Shadow = 0;
            virusPNG.Alignment = TextDrawAlignment.Left;
            virusPNG.ForeColor = -1;
            virusPNG.BackColor = 255;
            virusPNG.BoxColor = 50;
            virusPNG.UseBox = false;
            virusPNG.Proportional = true;
            virusPNG.Selectable = false;
            virusPNG.Show(this);


        }

        public void Autorization()
        {

            int attemps = 4;


            sqlCon.Open();
            MySqlCommand initPlayer = new MySqlCommand($"SELECT COUNT(*) FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
            if (Convert.ToInt32(initPlayer.ExecuteScalar()) == 1) {
                var authDialog = new InputDialog("Авторизация", "{FF0000}Данный аккаунт зарегистрирован\n Пожалуйста введите пароль для продолжения", true, "Продолжить", "Выйти");
                
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
                            this.SetSpawnInfo(0, Convert.ToInt32(getSkin.ExecuteScalar()), new SampSharp.GameMode.Vector3(x: 275.11f, y: 1861.51f, z: 8.75), 355.68f);
                            sqlCon.Close();
                            this.Spawn();
                            

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
                var registerDialog = new InputDialog("Регистрация", "Данный аккаунт не зарегистрирован\n Введите пароль который хотите использовать для входа", true, "Подтвердить", "Выйти");
                
                registerDialog.Response += registerDialog_Response;
                registerDialog.Show(this);
                void registerDialog_Response(object sender, DialogResponseEventArgs e)
                {
                    if (e.DialogButton == DialogButton.Left)
                    {
                        var AddUser = new MySqlCommand($"INSERT INTO `Players` (`NickName` , `Password`) VALUES ('{Name}', '{e.InputText}')", sqlCon);
                        AddUser.ExecuteNonQuery();
                        var getSkin = new MySqlCommand($"SELECT `SkinId` FROM `Players` WHERE `NickName` = '{Name}'", sqlCon);
                        this.SetSpawnInfo(0, Convert.ToInt32(getSkin.ExecuteScalar()), new SampSharp.GameMode.Vector3(x: 275.11f, y: 1861.51f, z: 8.75), 355.68f);
                        sqlCon.Close();
                        this.Spawn();

                    }
                    else
                    {
                        SendClientMessage("Вы отменили регистрацию!");
                        Kick();
                    }
                }
                SendClientMessage(initPlayer.ExecuteScalar().ToString());
                SendClientMessage("аккаунт отсутствует в базе данных non reg");
            }



        }
        
        [Command("Skin")]
        private void ChangeSkin(int skin)
        {
            Skin = skin;
        }
        

    }
}