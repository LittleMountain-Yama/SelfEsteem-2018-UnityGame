using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerLvl3 : MonoBehaviour
{
    public Transform[] positions;    
    public Flier flier;
    public Jumper jumper;
    public AirStrike striker;
    public GameObject spawnA;
    public GameObject spawnB;
    public GameObject spawnC;
    bool spawnAUsed;
    bool spawnBUsed;
    bool spawnCUsed;

    void Update()
    {
        if (spawnA == null && spawnAUsed == false)
        {
            Flier enemyTempA = Instantiate(flier);
            enemyTempA.transform.position = positions[0].position;
            Jumper enemyTempB = Instantiate(jumper);
            enemyTempA.transform.position = positions[1].position;
            spawnAUsed = true;
        }
        if (spawnB == null && spawnBUsed == false)
        {
            Flier enemyTempC = Instantiate(flier);
            enemyTempC.transform.position = positions[2].position;
            AirStrike enemyTempD = Instantiate(striker);
            enemyTempD.transform.position = positions[3].position;
            spawnBUsed = true;
        }
        if (spawnC == null && spawnCUsed == false)
        {
            AirStrike enemyTempE = Instantiate(striker);
            enemyTempE.transform.position = positions[4].position;
            Flier enemyTempF = Instantiate(flier);
            enemyTempF.transform.position = positions[5].position;
            spawnCUsed = true;
        }
    }
}
