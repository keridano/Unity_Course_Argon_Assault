using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFx;
    [SerializeField] Transform parent;

    bool isDying;

    void Start()
    {
        isDying = false;
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(!isDying)
        {
            isDying = true;
            var instantiatedFx = Instantiate(deathFx, transform.position, Quaternion.identity);
            instantiatedFx.transform.parent = parent;
            Destroy(gameObject);
        }
    }
}
