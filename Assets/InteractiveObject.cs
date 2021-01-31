using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    private CanvasController canvas;
    private ObjectType myType = ObjectType.Key;

    private bool touched = false;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Player").GetComponent<CanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (touched) return;

        if (collision.gameObject.tag == "Player")
        {
            switch (this.gameObject.name)
            {
                case "BoxLight":
                    myType = ObjectType.Light;
                    break;
                case "BoxWeapon":
                    myType = ObjectType.Weapon;
                    break;
                case "BoxDrug":
                    myType = ObjectType.Drug;
                    break;
                case "Key":
                    myType = ObjectType.Key;
                    this.GetComponent<SpriteRenderer>().enabled = false;
                    break;
                case "LockedDoor":
                    myType = ObjectType.LockedDoor;
                    break;
            }

            if (transform.childCount > 0)
            {
                touched = true;
                transform.GetChild(0).gameObject.SetActive(false);
            }
            
            canvas.ObjectAcquired(myType);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.name == "LockedDoor")
            {
                myType = ObjectType.LockedDoor;


                if (canvas.personaje.hasKey)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }

        canvas.ObjectAcquired(myType);
    }
}

public enum ObjectType
{
    Light,
    Weapon,
    Drug,
    Key,
    LockedDoor
}

