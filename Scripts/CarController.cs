using UnityEngine;
using UnityEngine.UI;
public class CarController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float maxHP;
    private float minHP;
    private float health;
    private float maxHealth;
    private GameObject player;
    private GameObject loot;
    public GameObject explosion;
    [SerializeField] Slider hpSlider;
    Canvas canvas;
    SpawnMap map;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpSlider = GetComponentInChildren<Slider>();
        canvas = GetComponentInChildren<Canvas>();
        player = GameObject.Find("Player");
        map = GameObject.Find("GameManager").GetComponent<SpawnMap>();
        maxHP = map.maxHP;
        minHP = map.minHP;
        health = Random.Range(minHP, maxHP);
        maxHealth = health;
        for (int i = 0; i < transform.childCount+1; i++)
        {
            
            GameObject child = this.gameObject.transform.GetChild(i).gameObject;
            
            
            if (child != null)
            {
                if (child.tag == "Weapon") 
                { 
                
                    
                    loot = child;
                    break;
                }
            }
        }
            
        

    }
    public void UpdateSlider(float currHP, float maxhealth)
    {
        hpSlider.value = currHP / maxhealth;
    }
    private void OnCollisionEnter(Collision collision)
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
    void OnTriggerEnter(Collider other)
    {

        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            
            player.GetComponent<WeaponAndShoot>().SwitchWeapon(loot);

            die();
            
        }

    }
    void die()
    {
        canvas.enabled = false;
        Instantiate(explosion, this.transform);
        Destroy(gameObject, 0.5f);
    }
}
