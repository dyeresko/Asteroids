using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public int lives = 3;
    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public int score;



    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();
        if (asteroid.size < 0.75)
        {
            score += 100;
        }
        else if (asteroid.size < 1.2)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }

    }
    public void PlayerDied()
    {
        explosion.transform.position = player.transform.position;
        explosion.Play();
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            lives--;
            Invoke(nameof(Respawn), respawnTime);
        }

    }


    void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnInvulnerabilityTime);
    }


    void TurnOnCollisions()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void GameOver()
    {
        this.lives = 3;
        this.score = 0;

        Invoke(nameof(Respawn), respawnTime);
    }

}
