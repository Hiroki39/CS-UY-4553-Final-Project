using UnityEngine;

public class PointInTime
{

    public Vector3 position;
    public Quaternion rotation;

    public float timeScale;

    public PointInTime(Vector3 _position, Quaternion _rotation, float _timeScale)
    {
        position = _position;
        rotation = _rotation;
        timeScale = _timeScale;
    }

}