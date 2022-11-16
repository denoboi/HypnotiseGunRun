using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HCB.CollectableSystem
{
    public class Collector : MonoBehaviour
    {
        private bool _canCollect = true;
        public bool CanCollect { get => _canCollect; protected set => _canCollect = value; }
        private void OnTriggerEnter(Collider other)
        {
            ICollectable collectable = other.GetComponentInParent<ICollectable>(); //burada hata veriyor dikkat parent olacak.
            
            if (collectable != null)
            {
                collectable.Collect(this);
                
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            ICollectable collectable = collision.collider.GetComponentInParent<ICollectable>();
            if (collectable != null)
            {
                collectable.Collect(this);
            }
        }
    }
}
