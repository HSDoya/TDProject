using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    public bool isSlow;
    private float slowspeed;
    private float normalSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    private void Start()
    {
        normalSpeed = moveSpeed;
        isSlow = false;
    }

    private void Update()
    {
       
        // 이동 처리
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        // 회전 처리
        RotateTowardsDirection(moveDirection);
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }

    private void RotateTowardsDirection(Vector3 direction)
    {
        // 방향을 기준으로 회전 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 회전 적용
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("IceBullet")) { return; }

        if (!isSlow)
        {
            IceBullet icebullet = collider.gameObject.GetComponent<IceBullet>();
            float slowdown = icebullet.GetSlowdown();

            slowspeed = Mathf.Max(moveSpeed - slowdown, 0);
            moveSpeed = slowspeed;
            isSlow = true;
            StartCoroutine(RestoreSpeedAfterDelay(3f));
        }  
    }

    private IEnumerator RestoreSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        moveSpeed = normalSpeed;
        isSlow = false;
    }



}
