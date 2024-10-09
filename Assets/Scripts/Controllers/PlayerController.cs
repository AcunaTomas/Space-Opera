using UnityEngine;

namespace Shinjingi
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
    public class PlayerController : InputController
    {
        public override bool RetrieveJumpInput()
        {
            return InputManager.INSTANCE.JumpPressed;
        }

        public override float RetrieveMoveInput()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public override bool RetrieveJumpHoldInput()
        {
            return InputManager.INSTANCE.JumpHeld;
        }
    }
}

//quiero decir que esto es muy avanzado para mí
