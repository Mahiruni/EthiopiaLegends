using UnityEngine;

namespace EthiopiaLegends.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 4.2f;
        [SerializeField] private float gravity = -20f;
        [SerializeField] private Transform cameraTransform;
        private CharacterController controller;
        private Vector2 moveInput;
        private float verticalVelocity;

        private void Awake() => controller = GetComponent<CharacterController>();
        public void SetMoveInput(Vector2 value) => moveInput = Vector2.ClampMagnitude(value, 1f);

        private void Update()
        {
            Vector3 forward = cameraTransform ? cameraTransform.forward : Vector3.forward;
            Vector3 right = cameraTransform ? cameraTransform.right : Vector3.right;
            forward.y = 0f; right.y = 0f;
            forward.Normalize(); right.Normalize();
            Vector3 direction = Vector3.ClampMagnitude(forward * moveInput.y + right * moveInput.x, 1f);
            if (direction.sqrMagnitude > 0.01f) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 12f);
            if (controller.isGrounded && verticalVelocity < 0f) verticalVelocity = -2f;
            verticalVelocity += gravity * Time.deltaTime;
            controller.Move((direction * moveSpeed + Vector3.up * verticalVelocity) * Time.deltaTime);
        }
    }
}
