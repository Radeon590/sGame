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

    [SerializeField] private Transform minBorder;
    [SerializeField] private Transform maxBorder;

    void Update()
    {
        #region Движение камеры с помощью клавиш
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // borders
        if ((horizontal < 0 && transform.position.x <= minBorder.position.x) || (horizontal > 0 && transform.position.x >= maxBorder.position.x))
        {
            horizontal = 0;
        }

        if (vertical < 0 && transform.position.y <= minBorder.position.y || (vertical > 0 && transform.position.y >= maxBorder.position.y))
        {
            vertical = 0;
        }
        
        // movement
        Vector3 movement = new Vector3(horizontal, vertical, 0);
        transform.position += movement * moveSpeed * Time.deltaTime;
        
        #endregion

        #region Перемещение камеры при зажатии колёсика
        
        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition; 
            Vector3 move = new Vector3(-delta.x, -delta.y, 0) * Time.deltaTime; 
            
            // borders
            if ((move.x < 0 && transform.position.x <= minBorder.position.x) || (move.x > 0 && transform.position.x >= maxBorder.position.x))
            {
                move = new Vector3(0, move.y, 0);
            }
            
            if ((move.y < 0 && transform.position.y <= minBorder.position.y) || (move.y > 0 && transform.position.y >= maxBorder.position.y))
            {
                move = new Vector3(move.x, 0, 0);
            }
            
            // movement
            transform.position += move * moveSpeed * Camera.main.orthographicSize / 10f; 
        }
        lastMousePosition = Input.mousePosition;
        
        #endregion

        #region Управление зумом с колесика мыши
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scroll * scrollSpeed, minZoom, maxZoom);

        #endregion
    }
}
