using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.collider.name != "Ground")
        {
            Controller.SetIsColliding(1);
        }        
    }
    private void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.collider.name != "Ground")
        {
            Controller.SetIsColliding(0);
        }        
    }

}
