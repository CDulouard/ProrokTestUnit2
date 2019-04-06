using UnityEngine;

namespace TestScripts
{
    public class ApplyForceToObject : MonoBehaviour
    {
        // Start is called before the first frame update
        public bool onX;
        public bool onY;
        public bool onZ;
        public float force;

        private Rigidbody _body;

        private void Start()
        {
            _body = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _body.AddForce(new Vector3(onX ? force * Time.deltaTime : 0, onY ? force * Time.deltaTime : 0,
                onZ ? force * Time.deltaTime : 0));
        }
    }
}
