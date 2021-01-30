using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public float speed = 1f;
    public float ghostingSpeed = 2f;


    private float frameCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frameCounter += 0.01f;
        var newYPosFactor = Mathf.Sin(frameCounter)/(100f * ghostingSpeed);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + newYPosFactor, this.transform.position.z);
    }

    private void beginTracking()
    {

    }
}
