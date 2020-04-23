using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        var musicPlayerInstances = FindObjectsOfType<MusicPlayer>().Length;
        if (musicPlayerInstances > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);    
    }
}
