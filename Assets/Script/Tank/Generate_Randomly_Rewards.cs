using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class Generate_Randomly_Rewards : MonoBehaviour
{
    
    void Start()
    {
        System.Random random = new System.Random();
        int numOfBullets = random.Next(0,2);
        int numOfHealth = random.Next(0,2);
        if (numOfBullets == 1)
        {

        }
        if (numOfHealth == 1)
        {

        }

        
    }

    void Update()
    {
        
    }
}
