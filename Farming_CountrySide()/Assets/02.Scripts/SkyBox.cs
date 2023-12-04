using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox : MonoBehaviour
{
    private SkyboxBlender skybox;

    void Start()
    {
        skybox = GetComponent<SkyboxBlender>();
        //skybox.Blend();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
