using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFx;

    void Start()
    {
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        Instantiate(deathFx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
