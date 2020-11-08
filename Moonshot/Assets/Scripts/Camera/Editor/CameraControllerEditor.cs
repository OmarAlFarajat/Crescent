using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraController))]
public class CameraControllerEditor : Editor
{

    private GameObject _player;
    private GameObject _camera;

    private void OnEnable()
    {
        _player = Camera.main.GetComponent<CameraController>().player;
        _camera = Camera.main.gameObject;
    }


    private void OnSceneGUI()
    {

        if (!_player)
        {
            return;
        }

        CameraController cameraPositionDetails = _camera.GetComponent<CameraController>();

        float radius = Vector3.Magnitude(_player.transform.forward * cameraPositionDetails.Distance);

        Handles.color = new Color(1, 0, 0, 0.15f);
        Handles.DrawSolidDisc(_player.transform.position, Vector3.up, radius);

        Handles.color = new Color(0, 1, 0, 1.0f);
        Handles.DrawWireDisc(_player.transform.position, Vector3.up, radius);

        Handles.color = new Color(0, 1, 0, 1.0f);
        cameraPositionDetails.Height = Handles.ScaleSlider(cameraPositionDetails.Height, _player.transform.position, Vector3.up, Quaternion.identity, cameraPositionDetails.Height, 0.5f);

        Handles.color = new Color(0, 0, 1, 1.0f);
        Vector3 distanceDirection = Quaternion.AngleAxis(cameraPositionDetails.Angle, Vector3.up) * -Vector3.forward * cameraPositionDetails.Distance;
        distanceDirection /= Vector3.Magnitude(distanceDirection);
        cameraPositionDetails.Distance = Handles.ScaleSlider(cameraPositionDetails.Distance, _player.transform.position, distanceDirection, Quaternion.identity, cameraPositionDetails.Distance, 0.5f);

        Handles.color = new Color(0.16f, 0.021f, 0.24f, 1.0f);
        Vector3 playerToCamera = _camera.transform.position - _player.transform.position;
        float playerToCameraDistance = Vector3.Magnitude(playerToCamera);
        float newVal = Handles.ScaleSlider(playerToCameraDistance, _player.transform.position, playerToCamera / playerToCameraDistance, Quaternion.identity, playerToCameraDistance, 0.5f);
        float diff = newVal - playerToCameraDistance;

        cameraPositionDetails.Height += diff;
        cameraPositionDetails.Distance += diff;

        cameraPositionDetails.Height = Mathf.Clamp(cameraPositionDetails.Height, 5f, float.MaxValue);
        cameraPositionDetails.Distance = Mathf.Clamp(cameraPositionDetails.Distance, 5f, float.MaxValue);

        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 15;
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.LowerLeft;
        labelStyle.fontStyle = FontStyle.Bold;

        Handles.Label(new Vector3(_camera.transform.position.x, _player.transform.position.y, _camera.transform.position.z), "Distance", labelStyle);
        Handles.Label(new Vector3(_player.transform.position.x, _camera.transform.position.y, _player.transform.position.z), "Height", labelStyle);
        Handles.Label(_camera.transform.position, "Camera", labelStyle);

        cameraPositionDetails.positionCamera();
    }

}
