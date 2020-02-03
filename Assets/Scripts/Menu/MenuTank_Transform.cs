using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTank_Transform : MonoBehaviour
{
    //Put any tank transforms here
    //Eg rotation, colour, scaling, panning
    //TODO: Different camera motions eg panning?
 
    public int transformMode;
    private float rotateSpeed, translateSpeed;
    void Start() {

        transform.localScale = new Vector3(400, 400, 400);
        switch (transformMode)
        {
            case 0:
                transform.localPosition = new Vector3(0, 0, 0);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                break;
            case 1:
                transform.localPosition = new Vector3(0, 0, 0);
                transform.localRotation = Quaternion.Euler(0, 90, 0);
                break;
            case 2:
                float scale = Random.Range(200F, 500.0F);
                float xpos = Random.Range(-400.0F, 200.0F);
                translateSpeed = Random.Range(1.0F, 4.0F);
                rotateSpeed = Random.Range(-4.0F, 4.0F);
                transform.localScale = new Vector3(scale, scale, scale);
                transform.localPosition = new Vector3(xpos, 400, 0);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                break;
        }
    }

    void Update() {

        switch(transformMode)
        {
            case 0:
                transform.Rotate(0.0F, 1.0F, 0.0F, Space.World);
                break;
            case 1:
                if (transform.localPosition.x > 500) transform.localPosition = new Vector3(-600, 0, 0);
                transform.Translate(3.0F, 0.0F, 0.0F,Space.World);
                break;
            case 2:
                if (transform.localPosition.y < -300) transform.localPosition = new Vector3(Random.Range(-400.0F, 400.0F), 400, 0);
                transform.Translate(0.0F, -translateSpeed, 0.0F, Space.World);
                transform.Rotate(0.0f, rotateSpeed, 0.0f, Space.World);
                break;
        }
        
        
    }
}
