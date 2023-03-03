using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    float speed = 500.0f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    public void Project(Vector2 direction)
    {
        _rigidbody.AddForce(direction * speed);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
