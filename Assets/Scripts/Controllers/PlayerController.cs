using UnityEngine;

namespace Shinjingi
{
    [CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
    public class PlayerController : InputController
    {
        public override bool RetrieveJumpInput()
        {
            return InputManager.INSTANCE.JumpInput;
        }

        public override float RetrieveMoveInput()
        {
            return InputManager.INSTANCE.MoveInput.x;
        }

        public override bool RetrieveJumpHoldInput()
        {
            return InputManager.INSTANCE.JumpHeld;
        }
    }
}

//quiero decir que esto es muy avanzado para mí
