using UnityEngine;

namespace ChameleonJump
{
    public class ChameleonCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            ChameleonJump.Instance.LoseGame();
        }
    }
}
