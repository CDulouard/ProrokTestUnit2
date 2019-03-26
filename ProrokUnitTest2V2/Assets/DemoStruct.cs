using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoStruct : MonoBehaviour
{
    public jointData[] jointData;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public struct jointData
{
    public string jointName;
    public int value;
[Range(0,1)]
public float valueAxis;}
