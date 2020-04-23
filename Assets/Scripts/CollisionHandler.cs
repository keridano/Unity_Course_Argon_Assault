using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] GameObject deathFx;

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
        deathFx.SetActive(true);        
        Invoke("HitDeadlyObject", levelLoadDelay);
    }

    /// <summary>
    /// Called by string reference
    /// </summary>
    private void HitDeadlyObject()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
