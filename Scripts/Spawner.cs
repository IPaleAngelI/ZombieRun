using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> carlist;
    public List<GameObject> weaponlist;
    public List<GameObject> enemylist;
    public Animator animator;
    Vector3 carposition;
    Vector3 weaponPos;
    Vector3 enemyPosition;
    List<GameObject> spawnedCars = new List<GameObject>();
    List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        SpawnObject();
        SpawnEnemies();
    }

    public void SpawnObject()
    {
        int carCount = Random.Range(1, 3);
        for (int i = 0; i < carCount; i++)
        {
            carposition = this.gameObject.transform.position;
            int carId = Random.Range(0, carlist.Count);
            int weaponId = Random.Range(0, weaponlist.Count);
            carposition.z = Random.Range(carposition.z - 30f, carposition.z + 30f);
            GameObject car = carlist[carId];
            GameObject weapon = weaponlist[weaponId];

            bool canSpawn = true;
            foreach (GameObject spawnedCar in spawnedCars)
            {
                if (Vector3.Distance(spawnedCar.transform.position, carposition) < 5f)
                {
                    canSpawn = false;
                    break;
                }
            }

            if (canSpawn)
            {
                GameObject spawnedCar = Instantiate(car, carposition, Quaternion.identity);
                spawnedCars.Add(spawnedCar);
                spawnedCar.transform.parent = this.transform;

                weaponPos = carposition;
                weaponPos.y += 3.5f;
                weapon.tag = "Weapon";
                GameObject spawnedWeapon = Instantiate(weapon, weaponPos, Quaternion.Euler(0f, 90f, 0f));
                spawnedWeapon.transform.localScale = new Vector3(3, 3, 3);
                spawnedWeapon.transform.parent = spawnedCar.transform;
            }
            else
            {
                i--;
            }
        }
    }

    public void SpawnEnemies()
    {
        int enemyCount = Random.Range(1, 3);
        for (int i = 0; i < enemyCount; i++)
        {
            enemyPosition = this.gameObject.transform.position;
            int enemyId = Random.Range(0, enemylist.Count);
            enemyPosition.z = Random.Range(enemyPosition.z - 30f, enemyPosition.z + 30f);
            GameObject enemy = enemylist[enemyId];

            bool canSpawn = true;
            foreach (GameObject spawnedEnemy in spawnedEnemies)
            {
                if (Vector3.Distance(spawnedEnemy.transform.position, enemyPosition) < 5f)
                {
                    canSpawn = false;
                    break;
                }
            }

            if (canSpawn)
            {
                foreach (GameObject spawnedCar in spawnedCars)
                {
                    if (Vector3.Distance(spawnedCar.transform.position, enemyPosition) < 5f)
                    {
                        canSpawn = false;
                        break;
                    }
                }
            }

            if (canSpawn)
            {
                GameObject spawnedEnemy = Instantiate(enemy, enemyPosition, Quaternion.identity);
                spawnedEnemies.Add(spawnedEnemy);
                spawnedEnemy.gameObject.tag = "Zombie";
                spawnedEnemy.transform.parent = this.transform;
            }
            else
            {
                i--;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var car in spawnedCars)
        {
            if (car != null)
            {
                Destroy(car);
            }
        }
        spawnedCars.Clear();

        foreach (var enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        spawnedEnemies.Clear();
    }
}