using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Brody")]
    [field: SerializeField] public EventReference FootstepsBrody { get; private set; }
    [field: SerializeField] public EventReference ShootBrody { get; private set; }
    [field: SerializeField] public EventReference JumpBrody { get; private set; }
    [field: SerializeField] public EventReference DoubleJump { get; private set; }
    [field: SerializeField] public EventReference GetDamageBrody { get; private set; }

    [field: Header("Bito")]
    [field: SerializeField] public EventReference LaserAttack { get; private set; }
    [field: SerializeField] public EventReference JumpSound { get; private set; }
    [field: SerializeField] public EventReference FallingSound { get; private set; }
    [field: SerializeField] public EventReference FallSound { get; private set; }
    [field: SerializeField] public EventReference DashSound { get; private set; }
    [field: SerializeField] public EventReference GetDamageBito { get; private set; }
    [field: SerializeField] public EventReference MoveBox { get; private set; }

    [field: Header("UI")]
    [field: SerializeField] public EventReference sfx_ui_navigate { get; private set; }
    [field: SerializeField] public EventReference sfx_ui_select { get; private set; }
    [field: SerializeField] public EventReference sfx_ui_open { get; private set; }
    [field: SerializeField] public EventReference sfx_ui_close { get; private set; }
    [field: SerializeField] public EventReference sfx_ui_save { get; private set; }
    [field: SerializeField] public EventReference sfx_ui_error { get; private set; }


    [field: Header("Enemies")]
    [field: SerializeField] public EventReference EnemiesDead { get; private set; }
    [field: SerializeField] public EventReference EnemiesAttack { get; private set; }

    [field: Header("General")]
    [field: SerializeField] public EventReference GameOverMelody { get; private set; }
    [field: SerializeField] public EventReference SaveLevel { get; private set; }
    
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
