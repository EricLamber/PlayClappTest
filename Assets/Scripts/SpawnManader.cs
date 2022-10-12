using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManader : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    private float _spawnCD = 1;
    private float _speed = 1;
    private float _distance = 1;
    public Queue<GameObject> _spawnQueue = new();

    void Start() => StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        while (true)
        {
            GameObject cube;
            if (_spawnQueue.Count == 0)
                cube = Instantiate(_cubePrefab, transform.position, transform.localRotation);
            else
            {
                cube = _spawnQueue.Dequeue();
                cube.transform.SetPositionAndRotation(transform.position, transform.localRotation);
                cube.SetActive(true);
            }
            StartCoroutine(Move(cube));
            yield return new WaitForSeconds(_spawnCD);
        }
    }

    private IEnumerator Move(GameObject cube)
    {
        var target = transform.position + new Vector3(0, 0, _distance);
        while (cube.transform.position != target)
        {
        var speed = _speed * Time.deltaTime;
            cube.transform. position = Vector3.MoveTowards(cube.transform.position, target, speed);
            yield return null;
        }
        cube.SetActive(false);
        _spawnQueue.Enqueue(cube);
    }

    public void CooldownChange(string newText)
    {
        var spawnCD = float.Parse(newText);
        _spawnCD = spawnCD;
    }

    public void SpeedChange(string newText)
    {
        var speed = float.Parse(newText);
        _speed = speed;
    }

    public void DistanceChange(string newText)
    {
        var distance = float.Parse(newText);
        _distance = distance;
    }
}
