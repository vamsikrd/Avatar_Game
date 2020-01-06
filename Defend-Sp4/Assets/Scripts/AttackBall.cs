using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBall : MonoBehaviour
{
    public ParticleSystem explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            GameObject explo = Instantiate(explosion.gameObject);
            explo.transform.position = this.transform.position;
            Destroy(explo, 1f);
            FindObjectOfType<LevelManager>().MainHealth();
            Destroy(this.gameObject);
        }
    }
}
