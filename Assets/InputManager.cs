using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager INSTANCE;

    public Vector2 MoveInput {get; private set;}
    public bool JumpPressed {get; private set;}
    public bool JumpHeld {get; private set;}
    public bool JumpReleased {get; private set;}
    public bool AttackInput {get; private set;}
    public bool AttackInputHeld {get; private set;}
    public bool Skill1Input {get; private set;}
    public bool Skill2Input {get; private set;}
    public bool SwitchInput {get; private set;}
    public bool DashInput {get; private set;}
    public bool OpenMenuInput {get; private set;}
    public bool OpenMenuHeld {get; private set;}
    public bool CloseMenuInput {get; private set;}
    public bool CloseMenuReleased {get; private set;}
    public bool SelectInput {get; private set;}

    [SerializeField] private PlayerInput _playerInput;

    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _skill1Action;
    private InputAction _skill2Action;
    private InputAction _moveAction;
    private InputAction _switchAction;
    private InputAction _dashAction;
    private InputAction _selectAction;
    private InputAction _menuOpenAction;
    private InputAction _menuCloseAction;
    
    
    void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }

        _playerInput = GetComponent<PlayerInput>();

        SetupInputActions();
    }

    void Update()
    {
        UpdateInputs();   
    }

    private void SetupInputActions()
    {
        _jumpAction = _playerInput.actions["Jump"];
        _attackAction = _playerInput.actions["Attack"];
        _skill1Action = _playerInput.actions["Skill1"];
        _skill2Action = _playerInput.actions["Skill2"];
        _moveAction = _playerInput.actions["Move"];
        _switchAction = _playerInput.actions["Switch"];
        _dashAction = _playerInput.actions["Dash"];
        _selectAction = _playerInput.actions["Select"];
        _menuOpenAction = _playerInput.actions["OpenMenu"];
        _menuCloseAction = _playerInput.actions["CloseMenu"];
    }

    private void UpdateInputs()
    {
        MoveInput = _moveAction.ReadValue<Vector2>();
        JumpPressed = _jumpAction.WasPressedThisFrame();
        JumpHeld = _jumpAction.IsPressed();
        JumpReleased = _jumpAction.WasReleasedThisFrame();
        AttackInput = _attackAction.WasPressedThisFrame();
        AttackInputHeld = _attackAction.IsPressed();
        Skill1Input = _skill1Action.WasPressedThisFrame();
        Skill2Input = _skill2Action.WasPressedThisFrame();
        SwitchInput = _switchAction.WasPressedThisFrame();
        DashInput = _dashAction.WasPressedThisFrame();
        SelectInput = _selectAction.WasPressedThisFrame();
        OpenMenuInput = _menuOpenAction.WasPressedThisFrame();
        OpenMenuHeld = _menuOpenAction.IsPressed();
        CloseMenuInput = _menuCloseAction.WasPressedThisFrame();
        CloseMenuReleased = _menuCloseAction.WasReleasedThisFrame();
    }

    public void UIInputs() //Cambiar a inputs de UI
    {
        
        _playerInput.SwitchCurrentActionMap("UI");
        Debug.Log("Cambio! a UI" + _playerInput.currentActionMap.name);
    }
    public void InGameInputs() //Cambiar a inputs del juego
    {
        Debug.Log("Cambio! a Gameplay");
        _playerInput.SwitchCurrentActionMap("InGame");
    }
}
