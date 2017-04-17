using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject doors;
    public float speed;
    public AudioClip seDamage;

    private Rigidbody2D rb2d;
    private Animator anim;
    private Renderer rend;
    private DoorManager doorManager;

    private int direction;
    [HideInInspector]public bool active;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        doorManager = doors.GetComponent<DoorManager>();
        direction = 1;
        active = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (active && Time.timeScale != 0)
        {
            //気まぐれに向きかえる
            if (Random.value > 0.995)
            {
                direction *= -1;
                transform.localScale = new Vector3(transform.localScale.x * -1, 1.5f, 1);
            }
            //x方向に動いてればrun
            if (rb2d.velocity.x != 0) anim.SetBool("isRun", true);
            else anim.SetBool("isRun", false);

            //徐々にスピードを上げる
            speed += 0.07f;

            //スピードの上限と下限
            if (speed < 100) speed = 100;
            else if (speed > 300) speed = 300;
        }
	}

    void FixedUpdate()
    {
        if( active ) rb2d.velocity = new Vector2(speed*direction, rb2d.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Door")
        {
            transform.position = doorManager.PlayerMove(col.transform.name);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Oden")
        {
            //SoundManager.instance.SeSound(seOden, 0.3f);
            Destroy(col.gameObject);
        }
    }

    //ダメージ処理
    public IEnumerator EnemyDamage()
    {
        if (active == false) yield break;

        SoundManager.instance.SeSound(seDamage, 0.3f);
        active = false;
        anim.SetTrigger("isDamage");
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce( new Vector2( 0, 10000 ));
        speed -= 50;
        //点滅
        while(!active)
        {
            rend.material.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.05f);
            rend.material.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void OnActive()
    {
        active = true;
    }
}
