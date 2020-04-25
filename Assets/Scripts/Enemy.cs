using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFx;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 20;

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
        if(!isDying)
        {
            isDying = true;

            //Update score
            scoreBoard.ScoreHit(scorePerHit);

            var instantiatedFx = Instantiate(deathFx, transform.position, Quaternion.identity);
            instantiatedFx.transform.parent = parent;
            Destroy(gameObject);
        }
    }
}
