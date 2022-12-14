using UnityEngine;

namespace CodeBase.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private Transform minLimit;
        [SerializeField] private Transform maxLimit;
        
        private Rigidbody rb;
        
        private void Start()
        {
            rb ??= GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (joystick.Direction != Vector2.zero)
            {
                Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
                Move(direction);
                Rotate(direction);
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }

        private void Move(Vector3 dir)
        {
            rb.MovePosition(transform.position + dir * (moveSpeed * Time.fixedDeltaTime));
            ClampMove();
        }

        private void Rotate(Vector3 dir)
        {
            Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(lookRotation, transform.rotation, rotateSpeed * Time.fixedDeltaTime);
        }

        private void ClampMove()
        {
            var clampX = Mathf.Clamp(rb.position.x, minLimit.position.x, maxLimit.position.x);
            var clampZ = Mathf.Clamp(rb.position.z, minLimit.position.z, maxLimit.position.z);
            rb.position = new Vector3(clampX, rb.position.y, clampZ);
        }
    }
}