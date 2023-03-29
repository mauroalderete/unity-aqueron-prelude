using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Opening.MisionScene
{
    public class PowerupTurn : MonoBehaviour
    {
        [SerializeField] float initialAngle;
        [SerializeField] Transform center;

        private void Start()
        {
            transform.RotateAround(center.position, Vector3.forward, initialAngle);
            transform.rotation = Quaternion.identity;
        }

        private void FixedUpdate()
        {
            transform.RotateAround(center.position, Vector3.forward, 15 * Time.deltaTime);
            transform.rotation = Quaternion.identity;
        }
    }

}
