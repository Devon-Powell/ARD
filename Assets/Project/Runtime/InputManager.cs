using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Runtime
{
    public class InputManager : MonoBehaviour
    {
        Vector2 moveAmount;

        public void OnMove(InputAction.CallbackContext context)
        {
            // read the value for the "move" action each event call
            moveAmount = context.ReadValue<Vector2>();
            Debug.Log("OnMove");
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            // your jump code goes here.
            Debug.Log("OnJump");

        }

        public void Update()
        {
            // to use the Vector2 value from the "move" action each
            // frame, use the "moveAmount" variable here.
        }
    }
}

/*public interface ICommand
{
    void Execute();
    void Undo();
}

public class MoveCommand : ICommand
{
    PlayerMover playerMover;
    Vector3 movement;
    public MoveCommand(PlayerMover player, Vector3 moveVector)
    {
        this.playerMover = player;
        this.movement = moveVector;
    }
    public void Execute()
    {
        playerMover.Move(movement);
    }
    public void Undo()
    {
        playerMover.Move(-movement);
    }
}*/