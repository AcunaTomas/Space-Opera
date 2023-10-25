using UnityEngine;

public class BrodyOval : MonoBehaviour
{

    [SerializeField]
    private Transform _player;
    [SerializeField]
    private GameObject _instantiate;
    [SerializeField]
    private GameObject[] _detectables;
    private int _objects = 0;
    [SerializeField]
    private float _xRadio = 0.2f;
    [SerializeField]
    private float _yRadio = 0.3f;

    private GameObject[] _instantiatedObjects;
    private Transform _ovalContainer;
    private Collider2D[] _detectableColliders;

    private void Start()
    {
        _detectableColliders = new Collider2D[_detectables.Length];
        for (int i = 0; i < _detectables.Length; i++)
        {
            _detectableColliders[i] = _detectables[i].GetComponent<Collider2D>();
            _objects++;
        }
        _instantiatedObjects = new GameObject[_objects];

        _ovalContainer = new GameObject("Oval").transform;
        _ovalContainer.parent = _player;

        CreateArrows();
    }

    private void CreateArrows()
    {
        float angleStep = 360f / _objects;

        for (int i = 0; i < _objects; i++)
        {
            float angle = i * angleStep;
            float radianAngle = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(radianAngle) * _xRadio;
            float y = Mathf.Sin(radianAngle) * _yRadio;
            Vector3 spawnPosition = new Vector3(x, y, 0);

            _instantiatedObjects[i] = Instantiate(_instantiate, _ovalContainer.position + spawnPosition, Quaternion.identity);
            _instantiatedObjects[i].transform.parent = _ovalContainer;
            _instantiatedObjects[i].SetActive(true);
        }

        for (int i = 0; i < _detectables.Length; i++)
        {
            if (!_detectables[i].activeSelf)
            {
                _player.transform.GetChild(_player.transform.childCount - 1).GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        _ovalContainer.position = _player.position;

        for (int i = 0; i < _objects; i++)
        {
            if (i < _detectables.Length)
            {
                Vector3 closestDirection = (_detectableColliders[i].bounds.center - _ovalContainer.position).normalized;
                Vector3 desiredPosition = _ovalContainer.position + (closestDirection * _xRadio);

                _instantiatedObjects[i].transform.position = desiredPosition;

                float angleToDetectable = Mathf.Atan2(closestDirection.y, closestDirection.x) * Mathf.Rad2Deg;
                _instantiatedObjects[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToDetectable - 90f));
            }
        }
    }
}