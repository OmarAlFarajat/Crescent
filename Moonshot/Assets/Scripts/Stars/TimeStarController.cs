using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStarController : MonoBehaviour
{
    public GameObject _TimeStarPrefab;
    public Transform _TimeCoinSpawnPoint;

    //variables for timer of creating time star
    [SerializeField]
    float _CreationTimer;
    [SerializeField]
    float _MaxCreationTime;

    // Start is called before the first frame update
    void Start()
    {
        GenerateTimeStar();
    }

    // Update is called once per frame
    void Update()
    {
        _CreationTimer -= Time.deltaTime;
        if (_CreationTimer <= 0)
        {
            GenerateTimeStar();
            _CreationTimer = _MaxCreationTime;
        }
    }

    private void GenerateTimeStar()
    {
        Instantiate(_TimeStarPrefab, _TimeCoinSpawnPoint);
    }
}
