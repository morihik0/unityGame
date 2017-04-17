using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Vector3 startPos;
    private bool isHit;
    private GameObject player, enemy;
    private Player playerScript;
    private Enemy enemyScript;
    private LayerMask playerLayer, enemyLayer;

    void Awake()
    {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
    }

    // Use this for initialization
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        enemyScript = enemy.GetComponent<Enemy>();
        playerLayer = LayerMask.GetMask(new string[] { "Player" });
        enemyLayer = LayerMask.GetMask(new string[] { "Enemy" });
        rb2d = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit && transform.position.y < startPos.y)
        {
            rb2d.isKinematic = true;
            transform.position = startPos;
        }
        //傾く事があるので無理やりもどす
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void FixedUpdate()
    {
        //横方向の力は全部無視
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
    }

    public void PlayerHit()
    {
        isHit = true;
        rb2d.isKinematic = false;
        rb2d.AddForce(new Vector2(0, 10000));
        rb2d.gravityScale = 120;

        bool isplayer = Physics2D.Linecast(
            transform.position + transform.up * 25f - transform.right * 17.5f,
            transform.position + transform.up * 25f + transform.right * 17.5f,
            playerLayer);
        bool isEnemy = Physics2D.Linecast(
            transform.position + transform.up * 25f - transform.right * 17.5f,
            transform.position + transform.up * 25f + transform.right * 17.5f,
            enemyLayer);
        if (isplayer) StartCoroutine(playerScript.PlayerDamage());
        else if (isEnemy) StartCoroutine(enemyScript.EnemyDamage());
    }    
}
