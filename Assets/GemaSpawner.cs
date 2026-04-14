using UnityEngine;

public class GemaSpawner : MonoBehaviour
{
    public GameObject gemaPrefab;
    public int quantidadeGemas = 5;

    // Limites da área de jogo
    public float minX = -6f;
    public float maxX = 6f;
    public float minY = -3.5f;
    public float maxY = 3.5f;

    private int gemasAtivas;

    void Start()
    {
        gemasAtivas = GameObject.FindGameObjectsWithTag("Coletavel").Length;
    }

    void Update()
    {
        if (GameController.gameOver) return;

        // Conta gemas ativas na cena
        gemasAtivas = GameObject.FindGameObjectsWithTag("Coletavel").Length;

        // Se não há mais gemas, spawna novas
        if (gemasAtivas == 0)
            SpawnarGemas();
    }

    void SpawnarGemas()
    {
        for (int i = 0; i < quantidadeGemas; i++)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            Instantiate(gemaPrefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}