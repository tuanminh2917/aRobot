// ObstacleMover.cs
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Di chuyển obstacle mỗi frame
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }
}
