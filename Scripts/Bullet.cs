using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 trans;
    public float damage;
    public bool isShotgun;
    public bool isRpg;
    public GameObject explos;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(0, 0, 25);
        //rb.MovePosition(rb.position + moveVector * Time.deltaTime);
        trans = transform.position;
        trans.z += 1f;
        this.transform.position = trans;
        if (isShotgun)
        {
            Destroy(gameObject, 2f);
        }
        Destroy(gameObject, 15f);
    }
    
}
