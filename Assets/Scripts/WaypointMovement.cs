using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Transform[] _points;
    private int _currentPoint;
    private bool _facingRight = false;
    private float _pointRadius = 1;

    private void Start()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
            _points[i] = _path.GetChild(i);
    }

    private void Update()
    {
        Transform target = _points[_currentPoint];
        transform.position = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (Vector2.Distance(target.position, transform.position) < _pointRadius)
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
                _currentPoint = 0;
        }

        var direction = (target.position - transform.position).normalized;

        if (direction.x > 0 && !_facingRight)
            Flip();
        else if (direction.x < 0 && _facingRight)
            Flip();
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}