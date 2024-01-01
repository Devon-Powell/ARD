using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Runtime
{
    public class InputManager : MonoBehaviour
    {
        private CharacterController player;
        Vector2 moveAmount;

        public void Init(CharacterController player)
        {
            this.player = player;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            // read the value for the "move" action each event call
            moveAmount = context.ReadValue<Vector2>();
            DebugManager.Instance.Log("OnMove");

            RunMoveCommand(player, moveAmount);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            // your jump code goes here.
            DebugManager.Instance.Log("OnJump");

        }
        
        private void RunMoveCommand(CharacterController player, Vector2 moveAmount)
        {
            var stateMachine = player.StateMachine;
            if (stateMachine == null)
            {
                DebugManager.Instance.Log("StateMachine is Null");
                return;
            }
            if (stateMachine.Enter(stateMachine.walkState))
            {
                DebugManager.Instance.Log("Entered Walk State");
            }
            else
            {
                DebugManager.Instance.Log("Failed to Enter Walk State");
            }
        }
    }
}
