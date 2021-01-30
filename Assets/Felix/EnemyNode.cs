using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNode : MonoBehaviour
{
    public List<EnemyNode> nodes;

    public EnemyTrackingSystem system { set; get; }

    public int index { set; get; }

    public bool visited = false;

    
    // Start is called before the first frame update
    void Start()
    {
        BiDirectionalCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BiDirectionalCheck()
    {
        foreach (EnemyNode node in nodes)
        {
            foreach (EnemyNode adyacentNode in node.nodes)
            {
                if (adyacentNode == this) return;
            }
            node.nodes.Add(this);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            system.newNodeTouchedbyPlayerRegistered(index);

            this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            system.newNodeTouchedbyEnemyRegistered(index);
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }


}
