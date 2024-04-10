using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool boughtSuppressor;
    public bool boughtExtendedMag;
    public bool boughtLaserAttach;
    public PlayerData (Player player)
    {
        boughtSuppressor = player.boughtSuppressor1;
        boughtExtendedMag = player.boughtExtendedMag1;
        boughtLaserAttach = player.boughtLaserAttach1;
    }
}
