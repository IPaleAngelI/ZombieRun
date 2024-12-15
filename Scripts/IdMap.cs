using UnityEngine;

public class IdMap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int id;
    private GameObject gameManager;
    private void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var Manager = GameObject.Find("GameManager").GetComponent<SpawnMap>();
        if (collision.gameObject.name == "Player")
        {
            //print("spawn");
            Manager.SpawnPart();
            Manager.CheckDeleteParts();
        }
    }
    

   
}
