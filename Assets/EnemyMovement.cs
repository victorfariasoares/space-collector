using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    public bool seguirJogador = true;
    public float minX = -6.5f;
    public float maxX = 6.5f;
    public float minY = -4f;
    public float maxY = 4f;

    private Vector2 direction;
    private Rigidbody2D rb;
    private Transform player;
    private float offsetAngulo;
    private float cooldownDano = 1f; // ← tempo mínimo entre danos
    private float ultimoDano = -999f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        transform.position = new Vector3(x, y, 0);

        offsetAngulo = Random.Range(-30f, 30f);

        float dx = Random.Range(0.5f, 1f) * (Random.value > 0.5f ? 1 : -1);
        float dy = Random.Range(0.5f, 1f) * (Random.value > 0.5f ? 1 : -1);
        direction = new Vector2(dx, dy).normalized;
    }

    void FixedUpdate()
    {
        if (GameController.gameOver) return;

        if (seguirJogador && player != null)
        {
            Vector2 direcaoBase = ((Vector2)player.position - rb.position).normalized;
            float angulo = Mathf.Atan2(direcaoBase.y, direcaoBase.x) * Mathf.Rad2Deg;
            angulo += offsetAngulo;
            float rad = angulo * Mathf.Deg2Rad;
            direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        }

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && !seguirJogador)
            direction = Vector2.Reflect(direction, collision.contacts[0].normal).normalized;
    }

    // Detecta quando entra em contato
    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameController.gameOver) return;
        if (other.CompareTag("Player")) TentarDanarJogador(other.gameObject);
    }

    // Detecta enquanto permanece em contato ← isso resolve o bug!
    void OnTriggerStay2D(Collider2D other)
    {
        if (GameController.gameOver) return;
        if (other.CompareTag("Player")) TentarDanarJogador(other.gameObject);
    }

    void TentarDanarJogador(GameObject playerObj)
    {
        // Só causa dano se passou o cooldown
        if (Time.time - ultimoDano < cooldownDano) return;

        ultimoDano = Time.time;
        GameController.LoseLife();
        playerObj.transform.position = Vector2.zero;
    }
}