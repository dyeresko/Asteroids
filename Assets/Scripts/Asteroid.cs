using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float size = 1.0f;
    public float speed = 5.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float maxLifetime = 30.0f;
    public Sprite[] sprites;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;
        _rigidbody.mass = this.size * 5.0f;

    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);
        Destroy(this.gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (this.size * 0.5 >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            Destroy(this.gameObject);
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
        }

    }
    private void CreateSplit()
    {

        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle.normalized * 0.5f;
        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed / 4);
    }
}
