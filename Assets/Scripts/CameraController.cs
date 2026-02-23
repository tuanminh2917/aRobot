using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The target the camera will follow
    private Vector3 offset; // Offset from the target

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = gameObject.transform.position - target.position; // Calculate the initial offset   
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = target.position + offset;
    }
}
