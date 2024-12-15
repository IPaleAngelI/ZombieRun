
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEditor.Timeline;


public class movePlayer : MonoBehaviour
{
    Camera cam;
    private Rigidbody rb;
    private float speed;
    public float horizontalSpeed = 5f;
    private Vector3 moveVector;
    private Vector3 camVector;
    GameObject player;
    public Collision collision1;
    public Vector3 cameraOffset = new Vector3(0, 5, -7);
    private bool restart = false;
    Animator animator;
    SpawnMap map;
    
    void Start()
    {
        map = GameObject.Find("GameManager").GetComponent<SpawnMap>();
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        player = this.gameObject;
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        
        if( collision.gameObject.tag == "Map")
        {
            map.levelComplication();
            speed = map.speedPlayer;
        }
        if (collision.gameObject.tag == "car")
        {
            speed = 0f;
            restart = true;
            StartCoroutine(RestartSceneWithDelay());
        }

        if (collision.gameObject.tag == "Zombie")
        {
            ZombieController zombie = collision.gameObject.gameObject.GetComponent<ZombieController>();
            if (zombie != null)
            {
                if (zombie.animator.GetBool("IsAttacking") == true)
                {
                    if (restart == false)
                    {

                        speed = 0f;
                        restart = true;
                        StartCoroutine(RestartSceneWithDelay());
                    }


                }

            }
        }
    }

    private IEnumerator RestartSceneWithDelay()
    {
        
        
        print("u died");
        // Ждем немного, чтобы все процессы успели завершиться
        yield return new WaitForSeconds(1.5f);

        // Перезагружаем сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        restart = false;
    }

    void FixedUpdate()
    {
        
        moveVector = Vector3.zero;
        moveVector.z = speed;
        moveVector.z = speed;
        if (restart == false)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            moveVector.x = horizontalInput * horizontalSpeed;
            print(moveVector);
            rb.MovePosition(rb.position + moveVector * Time.deltaTime);
        }
        
        
        camVector = player.transform.position;
        camVector.y = -4f;
        camVector.z -= 4f;
        cam.transform.position = camVector;
    }
}