using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);    
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadFirstScene", 3f);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}
