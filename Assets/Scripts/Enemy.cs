
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    
    public float health = 100;
    public int value  = 50;

    public GameObject deathEffect;


    void Start() {
        speed = startSpeed;
    }

    public void TakeDamage(float amount) {
        health -= amount;
        //Debug.Log("ENEMY HIT HEALTH ::" + health.ToString());
        if(health <= 0 ) {
            Die();
        }
    }
    void Die() {
        //Debug.Log("DIE HEALTH :: " + health.ToString());
        PlayerStats.Money += value;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(effect,3f);
    }

    public void Slow(float pct) {
        speed = startSpeed * (1f - pct);
    }
    

}
