using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class UFOManager : MonoBehaviour
{
    [SerializeField] private UFO _ufoPrefab;

    public float minWaitDuration;
    public float maxWaitDuration;

    public IEnumerator UFOClock()
    {
        while (true)
        {
            float delay = Random.Range(minWaitDuration, maxWaitDuration);
            yield return new WaitForSeconds(delay);
            Vector2 startPos = new Vector2(ResourceManager.instance.leftWall.transform.position.x + 3f, transform.position.y);
            UFO ufoInstance = Instantiate(_ufoPrefab, startPos, Quaternion.identity, transform);
            ufoInstance.SetImpulse(Vector2.right);
        }
    }
}
