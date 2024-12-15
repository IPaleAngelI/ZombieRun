using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public Animator animator;
    private GameObject player;
    private float attackDistance = 4f;
    private float minDistance = 6f;
    private bool isDead = false;
    private float moveSpeed = 2f;

    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        player = GameObject.Find("Player");

        if (animator == null)
        {
            Debug.LogError("Animator не найден на объекте " + gameObject.name);
            enabled = false;
            return;
        }

        if (player == null)
        {
            Debug.LogError("Игрок не найден. Убедитесь, что он имеет тег 'Player'");
            enabled = false;
            return;
        }

        var originalController = animator.runtimeAnimatorController;
        var newController = Instantiate(originalController);
        animator.runtimeAnimatorController = newController;

        // Отключаем Root Motion
        animator.applyRootMotion = false;

        animator.SetBool("IsRunning", true);
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Vector3 direction = (player.transform.position - transform.position).normalized;

        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if(animator.applyRootMotion == true){
            animator.applyRootMotion = false;
        }
        if (minDistance > distanceToPlayer)
        {
            if (animator.GetBool("IsAttacking") == false)
            {
                this.animator.SetBool("IsAttacking", false);
                this.animator.SetBool("IsRunning", true);
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
           
            
            
        }
        if (distanceToPlayer <= attackDistance)
        {
            this.animator.SetBool("IsRunning", false);
            this.animator.SetBool("IsAttacking", true);
        }
        
    }

    // Добавляем метод для контроля Root Motion
    void OnAnimatorMove()
    {
        // Этот метод отменяет движение от анимации
        if (animator != null)
        {
            //animator.ApplyBuiltinRootMotion();
           
        }
    }

    public void Die()
    {
        
        if (isDead) return;
        isDead = true;
        animator.enabled = false;
        this.animator.SetBool("IsRunning", false);
        this.animator.SetBool("IsAttacking", false);
        this.animator.SetInteger("DeathType", Random.Range(0, 2));
        this.animator.SetBool("Death", true);

        animator.enabled = true;
        
        
        


        if (GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = false;
        }
        
        Destroy(animator.runtimeAnimatorController, 6f);
        Destroy(gameObject, 6f);
    }

    private void OnDestroy()
    {
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            var controller = animator.runtimeAnimatorController;
            animator.runtimeAnimatorController = null;
            Destroy(controller);
        }
    }
}