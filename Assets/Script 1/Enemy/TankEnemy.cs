using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }
}
