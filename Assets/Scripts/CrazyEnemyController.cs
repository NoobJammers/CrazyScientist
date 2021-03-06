using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrazyEnemyController : MonoBehaviour
{
    public float speed=0.02f;
    public float probabilitytoattack=0.2f;
    public float HowFarPlayer = 2;
    public float forcemultiplier = 1;
    public float distFromFire = 0.2f;
    public int secondstocheckin=1;
    public bool OnFire;
    public float maxdistToCover=2;
    private bool patrolling = true;
    private Transform player;
    private string[] attackdontarray ;
    private Animator animator;
    public float smokeforce = 20;
    // Start is called before the first frame update
    void Start()
    {
       /* GetComponent<Rigidbody2D>().isKinematic = true;*/
        player = GameObject.FindGameObjectWithTag("player").transform;
        animator = GetComponent<Animator>();
        MakeProbabilityList();
        StartCoroutine(Patrol());
        StartCoroutine(CheckForAttack());
        FireBlobController.explodedHere += FireBlobExplodedNearby;
        StickyBlobController.explodedHere += StickyExploded;
    /*    StickyBlobController.smokehere += SmokeDetected;*/
    }

    public void FireBlobExplodedNearby(Vector3 pointofimpact)
    {
        if((new Vector2(pointofimpact.x,pointofimpact.y)-new Vector2(transform.position.x,transform.position.y)).magnitude<=distFromFire&&!OnFire)
        {
            GetComponent<Rigidbody2D>().AddForce((transform.up) * 10, ForceMode2D.Impulse);
            Destroy(gameObject, 0.2f);
            animator.SetBool("death", true);
        }
    }
    public void SmokeDetected(Vector3 point)
    {
        StartCoroutine(BlowSmoke(point));
    }

    IEnumerator BlowSmoke(Vector3 pointofimpact)
    {
        StopAllCoroutines();
        float timestart=Time.time;
        if ((new Vector2(pointofimpact.x, pointofimpact.y) - new Vector2(transform.position.x, transform.position.y)).magnitude <= distFromFire)
        {

            while (Time.time - timestart <= 20)
            { GetComponent<Rigidbody2D>().AddForce((-pointofimpact +transform.position)*smokeforce, ForceMode2D.Force);
                yield return null;
            }
           

            /*  Destroy(gameObject, 0.2f);
              animator.SetBool("death", true);*/
        }
    }
    public void StickyExploded(Vector3 pointofimpact)
    {
        if ((new Vector2(pointofimpact.x, pointofimpact.y) - new Vector2(transform.position.x, transform.position.y)).magnitude <= distFromFire )
        {
            GetComponent<Rigidbody2D>().AddForce((pointofimpact-transform.position) , ForceMode2D.Impulse);
            StopAllCoroutines();

          /*  Destroy(gameObject, 0.2f);
            animator.SetBool("death", true);*/
        }
    }

    private IEnumerator CheckForAttack()
    {   while(true)
        { 
        yield return new WaitForSecondsRealtime(secondstocheckin);
         patrolling = !possibilityOfAttack();
            if(!patrolling)
            {

                animator.SetTrigger("jump");
                AttackPlayer();
            }
            }
    }

    private void MakeProbabilityList()
    {
        attackdontarray = new string[10] { "D","D", "D", "D", "D", "D", "D", "D", "D", "D" };
        for(int i=0;i<probabilitytoattack*10;i++)
        {
            int index = Random.Range(0, 9);
            if (attackdontarray[index] != "A")
                attackdontarray[index] = "A";
            else
            {
                i--;
                
            }
        }

    }


    private IEnumerator Patrol()
    { Vector3 Travel = new Vector3(speed, 0, 0);
        Vector3 temptravel = Travel ;
        Vector3 finalPositiveX = transform.position + new Vector3(maxdistToCover, 0, 0);
        Vector3 finalnegativex= transform.position + new Vector3(-1*maxdistToCover, 0, 0);
        while (true)
        {
           
            while (!patrolling)
                yield return new WaitForSeconds(Time.fixedDeltaTime*1);
            
            transform.position += temptravel;
            if(transform.position.x>finalPositiveX.x )
            {
               temptravel = Travel * -1;
            }
            else if(transform.position.x<finalnegativex.x)
            {
                temptravel = Travel;
            }


            yield return new WaitForSeconds(Time.fixedDeltaTime * 1);
        }
    }

    private void AttackPlayer()
    {
      /*  GetComponent<Rigidbody2D>().isKinematic = false;*/
        GetComponent<Rigidbody2D>().AddForce((player.position - transform.position) * forcemultiplier,ForceMode2D.Impulse);

        StartCoroutine(delayPatrollingAnimation());

    }

    IEnumerator delayPatrollingAnimation()
    {
        yield return new WaitForSecondsRealtime(2.3f);
        animator.SetTrigger("patrol");
    }

    private bool possibilityOfAttack()
    {   if(player!=null && Mathf.Abs(player.position.x-transform.position.x)<HowFarPlayer)
        {
            int index = Random.Range(0, 9);
            return attackdontarray[index] == "A";
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="player" && player!=null)
        {   if (OnFire && player != null)
            {
                Destroy(player.gameObject,1);
                player.GetComponent<Animator>().SetBool("dead", true);
                Scene a = SceneManager.GetActiveScene();
                SceneManager.LoadScene(a.name);

            }

            else if (player != null && Vector3.Angle(transform.up, player.position - transform.position) < 60)
            {
                Destroy(gameObject, 0.2f);
                animator.SetBool("death", true);
            }
            else
            {
                Destroy(player.gameObject,1);
                player.GetComponent<Animator>().SetBool("dead",true);
                Scene a = SceneManager.GetActiveScene();
                SceneManager.LoadScene(a.name);
            }

        }
        if(collision.gameObject.GetComponent<FireBlobController>()!=null && !OnFire)

        {
            GetComponent<Rigidbody2D>().AddForce((transform.up) * 10, ForceMode2D.Impulse);
            Destroy(gameObject, 0.2f);
            animator.SetBool("death", true);
        }

        if (collision.gameObject.GetComponent<WaterBlobController>() != null && OnFire)

        {
            transform.GetChild(0).gameObject.SetActive(false);
            OnFire = false;
        }


        if (collision.gameObject.GetComponent<StickyBlobController>() != null)

        {
            GetComponent<Rigidbody2D>().AddForce((-collision.gameObject.transform.position + transform.position), ForceMode2D.Impulse);
            StopAllCoroutines();
        }
        /*   GetComponent<Rigidbody2D>().isKinematic = true;*/
        patrolling = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("particles hit");
    }
}
