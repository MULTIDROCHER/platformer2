using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private Npc _npc;
    private Transform _path;

    private Transform[] _pathPoints;
    private int _currentPoint;

    public IdleState(Npc npc, Transform path)
    {
        _npc = npc;
        _path = path;
    }

    public override void Enter()
    {
        if (_pathPoints == null || _pathPoints.Length == 0)
            SetPath();
    }

    public override void Update()
    {
        if (Vector3.Distance(_npc.transform.position, _pathPoints[_currentPoint].position) > 100)
        {
            int nearestPointIndex = FindNearestPointIndex();
            _currentPoint = nearestPointIndex;
        }

        Transform _targetPoint = _pathPoints[_currentPoint];
        _npc.transform.position = Vector3.MoveTowards(_npc.transform.position, _targetPoint.position, _npc.Speed * Time.deltaTime);

        if (_npc.transform.position == _targetPoint.position)
        {
            _currentPoint++;

            if (_currentPoint >= _pathPoints.Length)
                _currentPoint = 0;
        }
    }

    private void SetPath()
    {
        _pathPoints = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _pathPoints[i] = _path.GetChild(i);
        }
    }

    private int FindNearestPointIndex()
    {
        int nearestIndex = 0;
        float nearestDistance = Mathf.Infinity;

        for (int i = 0; i < _pathPoints.Length; i++)
        {
            float distance = Vector3.Distance(_npc.transform.position, _pathPoints[i].position);
            
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestIndex = i;
            }
        }

        return nearestIndex;
    }
}