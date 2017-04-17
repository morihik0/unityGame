using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Animator anim;
    private Renderer rend;
    private float inputx;
    private bool inputJump , isGround;
    private DoorManager doorManager;
    private GuiManager guiManager;
    [HideInInspector]public bool active;
    [HideInInspector]public int odenCount;

    public GameObject doors , gui;
    public LayerMask groundlayer;

    public AudioClip seJump , seOden , seDamage;

    public float speed; //moveSpeed

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        doorManager = doors.GetComponent<DoorManager>();
        guiManager = gui.GetComponent<GuiManager>();
        active = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (active && Time.timeScale != 0)
        {
            //左右入力取得
            inputx = Input.GetAxisRaw("Horizontal");
            //ジャンプ
            //足元に地面があるか判定
            isGround = Physics2D.Linecast(
                transform.position - transform.up * 38.5f - transform.right * 8,
                transform.position - transform.up * 38.5f + transform.right * 8,
                groundlayer);
            if (Input.GetButtonDown("Jump") && isGround)
            {
                SoundManager.instance.SeSound(seJump, 0.3f);
                inputJump = true;
            }

            //アニメ（走り）セット
            if (inputx != 0) anim.SetBool("isRun", true);
            else anim.SetBool("isRun", false);
            //アニメ（落下）
            if (rb2d.velocity.y < 0) anim.SetBool("isFalling", true);
            else anim.SetBool("isFalling", false);

            //左右向き変更
            if (inputx > 0) transform.localScale = new Vector3(1.5f, 1.5f, 1);
            else if (inputx < 0) transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        }
        if (GameManager.instance.isGameOver) GameOver();
    }

    void FixedUpdate()
    {
        if (active)
        {
            //左右移動
            rb2d.velocity = new Vector2(inputx * speed, rb2d.velocity.y);
            //ジャンプ
            if (inputJump)
            {
                anim.SetTrigger("isJumping");
                inputJump = false;
                rb2d.AddForce(new Vector2(rb2d.velocity.x, 15000));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Door")
        {
             transform.position =  doorManager.PlayerMove(col.transform.name);
        }
        if (col.transform.tag == "Ground" && col.transform.position.y > transform.position.y)
        {
            col.gameObject.GetComponent<Floor>().PlayerHit();
        }
        if(col.transform.tag == "Enemy")
        {
            StartCoroutine(PlayerDamage());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Oden")
        {
            odenCount++;
            SoundManager.instance.SeSound(seOden, 0.3f);
            guiManager.TimeCountPlus(2f);
            Destroy(col.gameObject);
        }
    }

    public IEnumerator PlayerDamage()
    {
        if (active == false) yield break;

        gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        SoundManager.instance.SeSound(seDamage, 0.3f);
        active = false;
        anim.SetTrigger("isDamage");
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(new Vector2(0, 10000));
        //点滅
        while (!active)
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
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        rend.material.color = new Color(1, 1, 1, 1);
        anim.SetBool("isGameOver", true);
    }
}
