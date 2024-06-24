using UnityEngine;

public class DynamicCameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public SpriteRenderer backgroundRenderer;

    private Camera cam;
    private Vector2 minCameraPos;
    private Vector2 maxCameraPos;

    void Start()
    {
        cam = Camera.main;

        // Get the size of the background
        float backgroundWidth = backgroundRenderer.bounds.size.x;
        float backgroundHeight = backgroundRenderer.bounds.size.y;

        // Calculate the vertical extent of the camera's view in world units
        float vertExtent = cam.orthographicSize;

        // Calculate the horizontal extent of the camera's view in world units
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // Calculate the minimum and maximum camera positions
        minCameraPos = new Vector2(horzExtent, vertExtent);
        maxCameraPos = new Vector2(backgroundWidth - horzExtent, backgroundHeight - vertExtent);

        // Set the camera to the center of the background
        Vector3 startPos = new Vector3(backgroundWidth / 2, backgroundHeight / 2, transform.position.z);
        startPos.x = Mathf.Clamp(startPos.x, minCameraPos.x, maxCameraPos.x);
        startPos.y = Mathf.Clamp(startPos.y, minCameraPos.y, maxCameraPos.y);
        transform.position = startPos;
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.mousePosition.x >= Screen.width - 10) // Right edge
        {
            pos.x += moveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= 10) // Left edge
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y >= Screen.height - 10) // Top edge
        {
            pos.y += moveSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= 10) // Bottom edge
        {
            pos.y -= moveSpeed * Time.deltaTime;
        }

        // Clamp the camera position to the bounds of the background
        pos.x = Mathf.Clamp(pos.x, minCameraPos.x, maxCameraPos.x);
        pos.y = Mathf.Clamp(pos.y, minCameraPos.y, maxCameraPos.y);

        transform.position = pos;
    }
}
