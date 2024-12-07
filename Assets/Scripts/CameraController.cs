using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f; 
    public float scrollSpeed = 10f; 
    public float minZoom = 5f; 
    public float maxZoom = 20f; 
    private Vector3 lastMousePosition; // Последняя позиция мыши

    void Update()
    {
        // Движение камеры с помощью клавиш
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0);
        transform.position += movement * moveSpeed * Time.deltaTime;

        // Перемещение камеры при зажатии колёсика
        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition; 
            Vector3 move = new Vector3(-delta.x, -delta.y, 0) * Time.deltaTime; 
            transform.position += move * moveSpeed * Camera.main.orthographicSize / 10f; 
        }
        lastMousePosition = Input.mousePosition;

        // Управление зумом с колесика мыши
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scroll * scrollSpeed, minZoom, maxZoom);

    }
}
