using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmisBlink : MonoBehaviour
{

    public Material mat;
    // Start is called before the first frame update
    void Start()
    {

        mat.DisableKeyword("_EMISSION");
         
    }

    // Update is called once per frame
    private float time = 0f;
    private bool emit = false;

    void Update()
    {
        if (time >= 0.1f)
        {
            emit = !emit;
            if (emit)
                mat.EnableKeyword("_EMISSION");
            else
                mat.DisableKeyword("_EMISSION");
            time = 0f;
        }

        time += Time.deltaTime;
    }
}
