using UnityEngine;

public class PortalBoundary : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        mainCamera = Camera.main;
        // Get the world position of the screen corners
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        // Half width and height of the sprite/renderer
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            objectWidth = renderer.bounds.extents.x;
            objectHeight = renderer.bounds.extents.y;
        }
        else
        {
            objectWidth = 0.5f;
            objectHeight = 0.5f;
        }
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        // Clamp X and Y positions to keep the object inside the camera
        viewPos.x = Mathf.Clamp(viewPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        transform.position = viewPos;
    }
}
