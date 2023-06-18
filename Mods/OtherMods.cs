using GorillaLocomotion;
using GorillaNetworking;
using ModMenuPatch.HarmonyPatches;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using UnityEngine;

namespace KosmosModMenu.Mods
{
    internal class OtherMods
    {
























        public static void NoTapCoolDown(bool enable)
        {
            if (enable)
            {
                GorillaTagger.Instance.tapCoolDown = 0f;
            }
            else
            {
                GorillaTagger.Instance.tapCoolDown = 0.33f;
            }
        }
        public static void instatag()
        {
            foreach (GorillaTagManager gorillaTagManager in UnityEngine.Object.FindObjectsOfType<GorillaTagManager>())
            {
                gorillaTagManager.checkCooldown = 0f;
                gorillaTagManager.tagCoolDown = 0f;
            }
        }

        // Dont Go Below This! -- kosmos
        public static void allCosmetics()
        {
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[1]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[2]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[3]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[4]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[5]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[6]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[7]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[8]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[9]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[10]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[11]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[12]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[13]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[14]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[15]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[16]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[17]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[18]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[19]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[20]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[21]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[22]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[23]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[24]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[25]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[26]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[27]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[28]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[29]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[30]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[31]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[32]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[33]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[34]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[35]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[36]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[37]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[38]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[39]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[40]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[41]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[42]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[43]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[44]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[45]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[46]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[47]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[48]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[49]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[50]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[51]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[52]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[53]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[54]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[55]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[56]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[57]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[58]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[59]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[60]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[61]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[62]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[63]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[64]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[65]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[66]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[67]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[68]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[69]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[70]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[81]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[82]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[83]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[84]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[85]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[86]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[87]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[88]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[89]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[90]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[91]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[92]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[93]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[94]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[95]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[96]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[97]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[98]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[99]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[100]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[101]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[102]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[103]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[104]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[105]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[106]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[107]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[108]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[109]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[110]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[111]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[112]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[113]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[114]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[115]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[116]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[117]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[118]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[119]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[120]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[121]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[122]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[123]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[124]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[125]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[126]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[127]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[128]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[129]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[130]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[131]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[132]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[133]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[134]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[135]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[136]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[137]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[138]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[139]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[140]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[141]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[142]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[143]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[144]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[145]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[146]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[147]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[148]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[149]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[150]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[151]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[152]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[153]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[154]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[155]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[156]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[157]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[158]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[159]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[160]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[161]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[162]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[163]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[164]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[165]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[166]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[167]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[168]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[169]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[170]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[181]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[182]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[183]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[184]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[185]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[186]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[187]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[188]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[189]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[190]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[191]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[192]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[193]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[194]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[195]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[196]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[197]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[198]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[199]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[200]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[201]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[202]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[203]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[204]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[205]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[206]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[207]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[208]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[209]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[210]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[211]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[212]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[213]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[214]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[215]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[216]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[217]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[218]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[219]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[220]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[221]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[222]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[223]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[224]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[225]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[226]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[227]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[228]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[229]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[230]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[231]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[232]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[233]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[234]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[235]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[236]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[237]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[238]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[239]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[240]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[241]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[242]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[243]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[244]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[245]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[246]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[247]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[248]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[249]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[250]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[251]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[252]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[253]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[254]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[255]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[256]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[257]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[258]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[259]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[260]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[261]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[262]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[263]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[264]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[265]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[266]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[267]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[268]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[269]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[270]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[281]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[282]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[283]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[284]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[285]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[286]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[287]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[288]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[289]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[290]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[291]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[292]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[293]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[294]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[295]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[296]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[297]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[298]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[299]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[300]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[301]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[302]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[303]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[304]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[305]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[306]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[307]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[308]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[309]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[310]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[311]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[312]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[313]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[314]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[315]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[316]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[317]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[318]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[319]);
            CosmeticsController.instance.unlockedHats.Add(CosmeticsController.instance.allCosmetics[320]);
            PlayerPrefs.Save();
            // End
        }
    }
}
