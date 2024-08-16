using System;
using UnityEngine;

public class PlayerStatus: MonoBehaviour
{
    public int Energy = 0;
    public int Resources = 0;

    public void ChangeEnergy(int energyGain)
    {
        Energy += energyGain;
    }

    public void ChangeResource(int resourceGain)
    {
        Resources += resourceGain;
    }

}

