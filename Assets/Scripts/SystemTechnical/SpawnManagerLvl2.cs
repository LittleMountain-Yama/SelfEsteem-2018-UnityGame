using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerLvl2 : MonoBehaviour
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
            AirStrike enemyTempA = Instantiate(striker);
            enemyTempA.transform.position = positions[0].position;
            Jumper enemyTempB = Instantiate(jumper);
            enemyTempB.transform.position = positions[1].position;
            Jumper enemyTempC = Instantiate(jumper);
            enemyTempC.transform.position = positions[2].position;
            spawnAUsed = true;
        }
        if (spawnB == null && spawnBUsed == false)
        {
            Flier enemyTempD = Instantiate(flier);
            enemyTempD.transform.position = positions[3].position;
            Jumper enemyTempE = Instantiate(jumper);
            enemyTempE.transform.position = positions[4].position;            
            spawnBUsed = true;
        }
        if (spawnC == null && spawnCUsed == false)
        {
            Flier enemyTempG = Instantiate(flier);
            enemyTempG.transform.position = positions[5].position;
            Jumper enemyTempH = Instantiate(jumper);
            enemyTempH.transform.position = positions[6].position;
            spawnCUsed = true;
        }
    }
}
