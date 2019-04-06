using UnityEngine;

namespace TestScripts
{
    public class moveObject : MonoBehaviour
    {
        public bool moveX;
        public bool moveY;
        public bool moveZ;
        public float speed;

        private void Update()
        {
            transform.position += new Vector3(moveX ? speed * Time.deltaTime : 0, moveY ? speed * Time.deltaTime : 0,
                moveZ ? speed * Time.deltaTime : 0);
        }
    }
}