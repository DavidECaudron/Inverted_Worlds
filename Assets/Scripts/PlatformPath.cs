using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class PlatformPath : MonoBehaviour
{
    List<Transform> _points = new List<Transform>();
    int _currentIndex;
    bool indent = true;

    void Start()
    {
        LevelRotation lr = FindObjectOfType<LevelRotation>();
        lr.OnRotationEnd += RefreshPointList;

        InitPoints();
    }

    void InitPoints()
    {
        _points = gameObject.transform.GetComponentsInChildren<Transform>().ToList();
    }

    void RefreshPointList()
    {
        _points.Clear();
        InitPoints();
    }

    public Vector3 GetActualPoint()
    {
        return _points[_currentIndex].transform.position;
    }


    public Vector3 GetNextPoint()
    {
        if (_points.Count == 0)
        {
            InitPoints();
        }


        if (_currentIndex == _points.Count - 1 && indent)
        {
            indent = false;
        }
        else if (_currentIndex == 0 && !indent)
        {
            indent = true;
        }

        _currentIndex = indent ? _currentIndex += 1 : _currentIndex -= 1;

        return _points[_currentIndex].transform.position;
    }

    public List<Transform> GetAllPoints()
    {
        if (_points.Count == 0)
        {
            InitPoints();
        }
        return _points;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Vector3 lastPoint = transform.position;

        foreach (Transform point in _points)
        {
            Gizmos.DrawLine(lastPoint, point.transform.position);

            lastPoint = point.transform.position;
        }
    }


}
