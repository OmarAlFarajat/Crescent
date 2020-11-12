using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStar : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 60.0f); //TODO: play with time
    }
}
