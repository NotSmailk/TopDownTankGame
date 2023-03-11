using UnityEngine;

public class EnemyDetection
{
    private DetectionParametres _parametres;
    private GameObject _target;
    private GameObject _tower;
    private Transform _transform;
    private bool _isChasingTarget = false;
    private float _shootDistanceOffset = 3f;

    public bool IsChasingTarget { get => _isChasingTarget; }
    public GameObject Target { get => _target; }

    public void Initialize(DetectionParametres parametres, GameObject tower, Transform transform)
    {
        _tower = tower;
        _transform = transform;
        _parametres = parametres;
    }

    public void GameUpdate()
    {
        GetTarget();
    }

    #region DetectionMethods

    public void DetectObjectsInRadius()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(_transform.position, _parametres.SphereRadius, _parametres.DetectionLayer);

        foreach (var detectedObject in detectedObjects)
        {
            if (_target == null)
                _target = detectedObject.gameObject;

            return;
        }

        if (_target != null)
            _target = null;
    }

    public void DetectObjectAtFieldOfView()
    {
        Collider[] detectedObjects = Physics.OverlapSphere(_transform.position, _parametres.ViewRadius, _parametres.DetectionLayer);

        foreach (var detectedObject in detectedObjects)
        {
            Vector3 directionToTarget = (detectedObject.transform.position - _transform.position).normalized;
            if (Vector3.Angle(_tower.transform.right, directionToTarget) < _parametres.ViewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(detectedObject.transform.position, _transform.position);
                if (Physics.Raycast(_tower.transform.position, directionToTarget, distanceToTarget, _parametres.ObstructionLayer))
                {
                    _isChasingTarget = false;
                    _target = null;

                    return;
                }

                if (_target == null)
                    _target = detectedObject.gameObject;

                _isChasingTarget = distanceToTarget > _parametres.RadiusAtObservation;

                return;
            }
        }

        _isChasingTarget = false;
        _target = null;
        DetectObjectsInRadius();
    }

    public bool TargetInShootDistance()
    {
        if (_target == null)
            return false;

        Vector3 directionToTarget = (_target.transform.position - _transform.position).normalized;
        if (Vector3.Angle(_tower.transform.right, directionToTarget) < _parametres.ShootViewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(_target.transform.position, _transform.position);
            return distanceToTarget <= _parametres.RadiusAtObservation + _shootDistanceOffset;
        }

        return false;
    }

    #endregion DetectionMethods

    #region GetMethods

    public Vector3 GetTargetPosition()
    {
        if (_target == null)
            return _transform.position;

        return _target.transform.position;
    }

    public GameObject GetTarget()
    {
        DetectObjectAtFieldOfView();

        return _target;
    }

    #endregion GetMethods
}
