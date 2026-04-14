using UnityEngine;

public class MusicaFundo : MonoBehaviour
{
    private static MusicaFundo instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}