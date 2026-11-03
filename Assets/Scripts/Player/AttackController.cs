using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class AttackController : MonoBehaviour
    {
        private Animator animator;
        private InputAction attackAction;

        private void Awake() {
            animator = GetComponent<Animator>();
        }

         private void Start() {
            attackAction = InputSystem.actions.FindAction("Attack");
        }

        private void Update() {
            if (attackAction.WasPressedThisFrame()) {
                Attack();
            }
        }

        public void Attack() {
            animator.SetTrigger("Attack");
        }

    }
} 