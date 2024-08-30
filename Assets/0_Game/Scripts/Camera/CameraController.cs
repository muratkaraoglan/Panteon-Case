using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _normalSpeed;
    [SerializeField] private float _movementTime;

    private Vector3 _newPosition;

    #region Mouse
    private Vector3 _dragStartPosition;
    private Vector3 _dragCurrentPosition;
    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        transform.position = new Vector3(GridManager.Instance.MapCollider.size.x / 2, GridManager.Instance.MapCollider.size.y / 2, transform.position.z);
        _newPosition = transform.position;
    }

    private void Update()
    {
        HandleMouseInput();
    }

    private void LateUpdate()
    {
        var verticalExtent = _mainCamera.orthographicSize;// calculates the vertical extent (half-height) of what the camera can see in world units
        var horizontalExtent = verticalExtent * Screen.width / Screen.height; //calculates the horizontal extent (half-width) of the camera's view by multiplying the vertical extent by the aspect ratio
        var areaBounds = GridManager.Instance.MapCollider.bounds;

        _newPosition = new Vector3(
            Mathf.Clamp(_newPosition.x, areaBounds.min.x + horizontalExtent, areaBounds.max.x - horizontalExtent),
            Mathf.Clamp(_newPosition.y, areaBounds.min.y + verticalExtent, areaBounds.max.y - verticalExtent),
            _newPosition.z
            );
        transform.position = Vector3.Lerp(transform.position, _newPosition, _movementTime * Time.deltaTime);
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);

            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry))
            {
                _dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.forward, Vector3.zero);

            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry))
            {
                _dragCurrentPosition = ray.GetPoint(entry);

                _newPosition = transform.position + _dragStartPosition - _dragCurrentPosition;
            }
        }
    }

    public Camera MainCamera => _mainCamera;
}
