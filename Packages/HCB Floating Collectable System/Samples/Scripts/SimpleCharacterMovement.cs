using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.FloatingCollectableSystem.Examples
{
    public class SimpleCharacterMovement : MonoBehaviour
    {
        public float MovementSpeed;
        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 movementDirection = new Vector3(horizontal, 0, vertical);          

            transform.Translate(Time.deltaTime * MovementSpeed * movementDirection);
        }
    }
}
