using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerLvl1 : MonoBehaviour
{
    public Transform[] positions;   
    public Flier flier;
    public Jumper jumper;
    public AirStrike striker;
    public GameObject spawnA;
    public GameObject spawnB;    
    bool spawnAUsed;
    bool spawnBUsed;   

    void Update()
    {
        if (spawnA == null && spawnAUsed == false)
        {
            AirStrike enemyTempA = Instantiate(striker);
            enemyTempA.transform.position = positions[0].position;            
            spawnAUsed = true;
            Flier enemyTempB = Instantiate(flier);
            enemyTempB.transform.position = positions[1].position;
            spawnAUsed = true;            
        }

        if (spawnB == null && spawnBUsed == false)
        {
            AirStrike enemyTempC = Instantiate(striker);
            enemyTempC.transform.position = positions[2].position;
            spawnBUsed = true;
            Jumper enemyTempD = Instantiate(jumper);
            enemyTempD.transform.position = positions[3].position;
            spawnBUsed = true;
            Flier enemyTempE = Instantiate(flier);
            enemyTempE.transform.position = positions[4].position;
            spawnBUsed = true;
        }        
    }
}
