using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFx;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 20;
    [SerializeField] int hits = 10;

    ScoreBoard scoreBoard;
    bool isDying;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        AddBoxCollider();
    }
 
    private void AddBoxCollider()
    {
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hits <= 1)
            KillEnemy();
    }

    private void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits--;
    }

    private void KillEnemy()
    {
        if (isDying) return; //prevents multiple deathFX instances

        isDying = true;
        var instantiatedFx = Instantiate(deathFx, transform.position, Quaternion.identity);
        instantiatedFx.transform.parent = parent;
        Destroy(gameObject);
    }
}
