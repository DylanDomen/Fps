using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MonsterActions : MonoBehaviour
{
    public GameObject Player;
    public bool makeMonsterSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        if (makeMonsterSpawn)
        {
            makeMonsterSpawn = false;
            spawnMonster(10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);
        if (Vector3.Distance(this.gameObject.transform.position, Player.transform.position) <= 2.50f)
        {
            if(Player.GetComponent<PlayerInfo>().canBeDamaged)
            {
                Player.GetComponent<PlayerInfo>().takeDamage();
            }
        }
    }

    void spawnMonster(int numberOfMonsters)
    {
        for (int i = 1; i < numberOfMonsters; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10,21), 1, Random.Range(-1, 31));
            Instantiate(this.gameObject, randomSpawnPosition, Quaternion.identity);
        }
    }
}
