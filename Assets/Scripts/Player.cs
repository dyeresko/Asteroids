
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    private bool _thrusting;
    private float _turnDirection;

    private Rigidbody2D _rigidbody;
    public float turningSpeed = 3.0f;
    public float speed = 10.0f;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _turnDirection = -1.0f;
        }
        else
        {
            _turnDirection = 0f;
        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (_thrusting)
        {
            _rigidbody.AddForce(this.transform.up * this.speed);
        }
        if (_turnDirection != 0)
        {
            _rigidbody.AddTorque(_turnDirection * this.turningSpeed);
        }
    }


    void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            this._rigidbody.velocity = Vector2.zero;
            this._rigidbody.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied();

        }
    }
}
