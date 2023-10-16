using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritetest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("puto");
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       transform.position += new Vector3(0.01f,0,0);
    }
}
