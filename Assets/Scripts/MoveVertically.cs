using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveVertically : MonoBehaviour
{
    [SerializeField] private float verticalMovementPerSecond;
    [SerializeField] private RectTransform rt;

    private void Update()
    {
        rt.position += new Vector3(0, verticalMovementPerSecond * Time.deltaTime, 0);
    }

}
