using UnityEngine;

public class MoverByWaypoints : MonoBehaviour
{
    [SerializeField] private Transform _waypointsContainer;
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _minimalDistanceForChangeWaypoint = 0.05f;

    private bool _isMoving;
    private int _currentWaypointIndex = 0;
    private Vector3 _currentWaypointPosition;
    private Transform[] _waypoints;

    private void Awake()
    {
        int _waypointsCount = transform.childCount;

        if (_waypointsCount > 0)
        {
            _waypoints = new Transform[_waypointsCount];

            for (int i = 0; i < _waypointsCount; i++)
            {
                _waypoints[i] = _waypointsContainer.GetChild(i);
            }
        }
    }

    private void OnEnable()
    {
        if (_waypoints.Length > 0)
        {
            _isMoving = true;
        }
    }

    private void OnDisable()
    {
        _isMoving = false;
    }

    private void Update()
    {
        while (_isMoving)
        {
            MoveForward();

            if (IsWaypointReached())
            {
                ChangeWaypoint();
            }
        }
    }

    private void MoveForward()
    {
        transform.rotation = Quaternion.LookRotation(_currentWaypointPosition - transform.position);
        transform.position = Vector3.MoveTowards(transform.position, _currentWaypointPosition, _speed * Time.deltaTime);
    }
    
    private bool IsWaypointReached()
    {
        Vector3 delta = _currentWaypointPosition - transform.position;
        float squaredDelta = delta.sqrMagnitude;
        
        return squaredDelta < _minimalDistanceForChangeWaypoint * _minimalDistanceForChangeWaypoint;
    }
    
    private void ChangeWaypoint()
    {
        _currentWaypointIndex = GetNextWaypointIndex();

        _currentWaypointPosition = GetNextWaypointPosition();
    }
    
    private int GetNextWaypointIndex()
    {
        return (_currentWaypointIndex + 1) % _waypoints.Length;
    }

    private Vector3 GetNextWaypointPosition()
    {
        return _waypoints[_currentWaypointIndex].position;
    }
}
