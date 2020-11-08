using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController : MonoBehaviour
{
    public GameObject player;

    private GameObject _camera;
    private Vector3 _cameraOffset;

    [SerializeField] private float _height;
    public float Height { get { return _height; } set { _height = value; } }
    [SerializeField] private float _distance;
    public float Distance { get { return _distance; } set { _distance = value; } }

    [Range(0, 360)]
    [SerializeField] private float _angle;
    public float Angle { get { return _angle; } set { _angle = value; } }

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Start()
    {
        _camera = Camera.main.gameObject;
        positionCamera();
    }

    // Update is called once per frame
    void Update()
    {

        if (!player)
        {
            return;
        }

        followPlayer();
    }

    public void positionCamera()
    {
        Vector3 cameraHeight = Vector3.up * _height;
        Vector3 cameraDistance = Quaternion.AngleAxis(_angle, Vector3.up) * -Vector3.forward * _distance;
        Vector3 cameraPosition = player.transform.position + cameraHeight + cameraDistance;

        _camera.transform.position = cameraPosition;

        _cameraOffset = player.transform.position - _camera.transform.position;
    }

    private void followPlayer()
    {
        _camera.transform.position = player.transform.position - _cameraOffset;
        transform.LookAt(player.transform, Vector3.up);
    }
}
