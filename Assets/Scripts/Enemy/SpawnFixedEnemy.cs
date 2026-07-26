using UnityEngine;

public class SpawnFixedEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            enemyPrefab[i].SetActive(true);
        }
    }

}
