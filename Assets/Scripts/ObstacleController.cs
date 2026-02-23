using UnityEngine;
using TMPro;
using System.Collections;

public class ObstacleController : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2.0f;
    public float destroyTime = 5.0f;
    private float timer;

    private ObstacleMover obstacleMover;
    private int count = 0;

    public TMPro.TextMeshProUGUI speedUpText;

    private void Start()
    {
        obstacleMover = obstaclePrefab.GetComponent<ObstacleMover>();
        SpawnObstacle();
        speedUpText.gameObject.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
            count++;
            if (count == 10)
            {
                obstacleMover.moveSpeed *= 1.05f; // Tăng tốc độ di chuyển obstacle lên 5%
                count = 0;
                StartCoroutine(ShowSpeedUpMessage());
            }
        }
    }

    private void SpawnObstacle()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-4f, 4f), 0, 10f);

        GameObject wall = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        Destroy(wall, destroyTime); // Sau destroyTime thì tự xóa
    }

    IEnumerator ShowSpeedUpMessage()
    {
        speedUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        speedUpText.gameObject.SetActive(false);
    }
}
