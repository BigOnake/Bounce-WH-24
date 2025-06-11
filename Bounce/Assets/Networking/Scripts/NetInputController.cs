using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NetInputController : MonoBehaviour
{
    public NetInputs netInputs;
    #region InputActions
    private InputAction i_move;
    private InputAction i_look;
    private InputAction i_jump;
    private InputAction i_attack;
    #endregion

    #region Events
    public static event Action<Vector2> onPlayerMove;
    public static event Action<Vector2> onPlayerLook;
    public static event Action onPlayerJump;
    public static event Action onPlayerAttack;
    #endregion

    private Vector2 movementInputs;
    private Vector2 lookingInputs;

    private void OnEnable()
    {
        i_EnableInputs();
    }

    private void OnDisable()
    {
        i_DisableInputs();
    }
   
    private void Awake()
    {
        netInputs = new NetInputs();
    }

    private void Update()
    {
        ReadMovementInputs();
        ReadLookingInputs();
    }

    private void i_EnableInputs()
    {
        i_move = netInputs.Player.Move;
        i_look = netInputs.Player.Look;
        i_jump = netInputs.Player.Jump;
        i_attack = netInputs.Player.Attack;

        i_jump.performed += Jump;
        i_attack.performed += Attack;

        i_move.Enable();
        i_look.Enable();
        netInputs.Player.Jump.Enable();
        netInputs.Player.Attack.Enable();
    }

    private void i_DisableInputs()
    {
        i_move.Disable();
        i_look.Disable();
        netInputs.Player.Jump.Disable();
        netInputs.Player.Attack.Disable();
    }

    #region Invokes
    private void Attack(InputAction.CallbackContext context)
    {
        onPlayerAttack?.Invoke();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        onPlayerJump?.Invoke();
    }

    private void ReadMovementInputs()
    {
        movementInputs = i_move.IsPressed() ? i_move.ReadValue<Vector2>() : Vector2.zero;
        onPlayerMove?.Invoke(movementInputs);
    }

    private void ReadLookingInputs()
    {
        lookingInputs = i_look.ReadValue<Vector2>();
        onPlayerLook?.Invoke(lookingInputs);
    }
    #endregion
}
