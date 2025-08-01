using UnityEngine;

public static class MouseUtil
{
    private static Camera _camera;

    public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
    {
        _camera = GetCamera();
        if( _camera == null )
        {
            return Vector3.zero;
        }

        Plane dragPlane = new(_camera.transform.forward, new Vector3(0, 0, zValue));
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if(dragPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }

        return Vector3.zero;
    }

    private static Camera GetCamera()
    {
        if(_camera != null && _camera.gameObject != null)
        {
            return _camera;
        }

        _camera = Camera.main;
        return _camera;
    }
}
