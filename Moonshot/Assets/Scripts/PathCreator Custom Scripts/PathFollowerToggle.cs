using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowerToggle : MonoBehaviour
{
    PathCreation.Examples.PathFollower pf;  //make public for testing
    // Start is called before the first frame update
    void Start()
    {
        pf = GetComponentInParent<PathCreation.Examples.PathFollower>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            pf.endOfPathInstruction = PathCreation.EndOfPathInstruction.Stop;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            pf.endOfPathInstruction = PathCreation.EndOfPathInstruction.Reverse;
    }
}
