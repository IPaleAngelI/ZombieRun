using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WeaponAndShoot : MonoBehaviour
{
    public GameObject weapon;
    public GameObject bullet;
    public GameObject shoot;
    private Vector3 spawnPos;
    GameObject prefab;
    public float shootInterval = 0.5f; // Интервал между выстрелами
    private float lastShootTime = 0f; // Время последнего выстрела

    public List<GameObject> weaponlist;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        prefab = bullet;
        SwitchWeapon(weapon);
    }
    void Update()
    {
        spawnPos = shoot.transform.position;

        prefab.tag = "Bullet";
        if (weapon != null)
        {
            if ( Time.time - lastShootTime >= shootInterval) // Проверка времени
            {

                GameObject bull = Instantiate(prefab, spawnPos, new quaternion(0, 0, 0, 0));
                lastShootTime = Time.time; // Обновление времени последнего выстрела
            }
        }
    }
    public void SwitchWeapon(GameObject wp)
    {
        prefab.GetComponent<Bullet>().isShotgun = false;
        prefab.GetComponent<Bullet>().isRpg = false;
        for (int i = 0; i < weaponlist.Count; i++)
        {
            if (weaponlist[i].name + "(Clone)" == wp.name || weaponlist[i].name == wp.name)
            {
                weapon = weaponlist[i];
            }
            if (weapon != null)
            {
                switch (weapon.name)
                {
                    case "AK74":
                        prefab.GetComponent<Bullet>().damage = 2;
                        shootInterval = 0.5f;
                        break;
                    
                    case "Bennelli_M4":
                        prefab.GetComponent<Bullet>().damage = 10;
                        shootInterval = 1f;
                        prefab.GetComponent<Bullet>().isShotgun = true;
                        break;
                    case "M4_8":
                        prefab.GetComponent<Bullet>().damage = 3;
                        shootInterval = 0.5f;
                        break;
                    case "M107":
                        prefab.GetComponent<Bullet>().damage = 8;
                        shootInterval = 1.5f;
                        break;
                    case "M249":
                        prefab.GetComponent<Bullet>().damage = 4;
                        shootInterval = 0.5f;
                        break;
                    case "M1911":
                        prefab.GetComponent<Bullet>().damage = 1;
                        shootInterval = 0.5f;
                        break;
                    case "RPG7":
                        prefab.GetComponent<Bullet>().damage = 50;
                        shootInterval = 5f;
                        prefab.GetComponent<Bullet>().isRpg = true;
                        break;
                    case "Uzi":
                        prefab.GetComponent<Bullet>().damage = 0.5f;
                        shootInterval = 0.2f;
                        break;

                }


            }
        }
        
        
        
    }
}