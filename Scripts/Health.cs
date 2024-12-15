
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float maxHP;
    private float minHP;
    private float health;
    private float maxHealth;
    private GameObject player;
    private ZombieController zombieController;
    [SerializeField] Slider hpSlider;
    Canvas canvas;
    SpawnMap map;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zombieController = GetComponent<ZombieController>();
        player = GameObject.Find("Player");
        hpSlider = GetComponentInChildren<Slider>();
        canvas = GetComponentInChildren<Canvas>();
        map = GameObject.Find("GameManager").GetComponent<SpawnMap>();
        maxHP = map.maxHP;
        minHP = map.minHP;
        health = Random.Range(minHP, maxHP);
        maxHealth = health;
        
    }
    public void UpdateSlider(float currHP, float maxhealth)
    {
        hpSlider.value = currHP/ maxhealth;
    }
    void OnCollisionEnter (Collision collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            if (collision.gameObject.GetComponent<Bullet>().isRpg == true)
            {
                Instantiate(collision.gameObject.GetComponent<Bullet>().explos, gameObject.transform);
            }
            Destroy(collision.gameObject.gameObject);
            
                float damage = collision.gameObject.GetComponent<Bullet>().damage;
                
                health -= damage;
            UpdateSlider(health, maxHealth);
                
            

        }
    }
    //void OnCollisionEnter(Collision collision)
    //{
        
    //    if (collision.gameObject.tag == "Bullet")
    //    {
            
    //        Destroy(collision.gameObject.gameObject);
    //        var weapon = player.GetComponent<WeaponAndShoot>();
    //        if (weapon != null)
    //        {
    //            if (weapon.weapon.name == "AK74(Clone)" || weapon.weapon.name == "AK74")
    //            {

    //                health -= 2;
    //            }
    //        }

    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
        
        if (health <= 0) 
        {

            canvas.enabled = false;
            zombieController.Die();
        }
        
    }
}
