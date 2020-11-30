// https://www.youtube.com/watch?v=IhRl3fLgTak
// https://drive.google.com/file/d/1yahA3QyOLTuNGQVR6JVr9aqQzRGJVJbk/view

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision_1 : MonoBehaviour
{

	public float minDistance = 1.0f;
	public float maxDistance = 4.0f;
	public float smooth = 10.0f;
	Vector3 dollyDir;
	public Vector3 dollyDirAdjusted;
	public float distance;

	public float dis_ray;

	// Use this for initialization
	void Awake()
	{
		dollyDir = transform.localPosition.normalized;
		distance = transform.localPosition.magnitude;
	}

	// Update is called once per frame
	void Update()
	{

		Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
		RaycastHit hit;

		if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
		{
			distance = Mathf.Clamp((hit.distance * dis_ray), minDistance, maxDistance);

		}
		else
		{
			distance = maxDistance;
		}

		transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
	}
}