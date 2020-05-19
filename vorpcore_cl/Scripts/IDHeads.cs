﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace vorpcore_cl.Scripts
{
    public class IDHeads : BaseScript
    {
        public static Dictionary<int, int> PlayerTags = new Dictionary<int, int>();

        public IDHeads() 
        {
            Tick += SetPlayerIdOnHead;
        }

        [Tick]
        private async Task SetPlayerIdOnHead()
        {

            for (int i = 0; i < 255; i++)
            {

                if (NetworkIsPlayerActive(i))
                {
                    if (GetPlayerPed(i) != PlayerPedId())
                    {

                        if (PlayerTags.ContainsKey(i))
                        {
                            if (Function.Call<bool>((Hash)0x6E1C31E14C7A5F97, PlayerTags[i]))
                            {
                                float distanceConfig = float.Parse(Utils.GetConfig.Config["HeadIdDistance"].ToString());
                                if (GetDistanceFromPlayer(i) < distanceConfig)
                                {
                                    // Feature 2.0
                                    //if () //NetworkIsPlayerTalking
                                    //{
                                    //    Function.Call((Hash)0x84BD27DDF9575816, PlayerTags[i], 0x333FC632);
                                    //}
                                    //else
                                    //{ 
                                    //    Function.Call((Hash)0x84BD27DDF9575816, PlayerTags[i], 0x42C33427);
                                    //}

                                    Function.Call((Hash)0x93171DDDAB274EB8, PlayerTags[i], 2);

                                }
                                else
                                {
                                    Function.Call((Hash)0x93171DDDAB274EB8, PlayerTags[i], 0);
                                }
                            }
                            else
                            {
                               
                                int tagId = Function.Call<int>((Hash)0xD877AF112AD2B41B, i, GetPlayerServerId(i).ToString(), false, false, "", 0);
                                PlayerTags[i] = tagId;
                            }
                        }
                        else
                        {
                            int tagId = Function.Call<int>((Hash)0xD877AF112AD2B41B, i, GetPlayerServerId(i).ToString(), false, false, "", 0);
                            PlayerTags.Add(i, tagId);
                        }
                    }
                    
                }
                    
            }
           
        }

        public static float GetDistanceFromPlayer(int p)
        {
            int playerPedId = GetPlayerPed(p);
            Vector3 playerCoords = GetEntityCoords(playerPedId, true, true);
            Vector3 myCoords = GetEntityCoords(PlayerPedId(), true, true);
            return GetDistanceBetweenCoords(myCoords.X, myCoords.Y, myCoords.Z, playerCoords.X, playerCoords.Y, playerCoords.Z, true);
        }
    }
}
