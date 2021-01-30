﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrackingSystem : MonoBehaviour
{
    public List<EnemyNode> nodes { set; get; }

    private EnemyNode lastNodeTouchedbyPlayer;
    private EnemyNode lastNodeTouchedbyEnemy;

    public EnemyBehaviour enemy { set; get; }

    public List<EnemyNode> visitingNodes { set; get; }

    // Start is called before the first frame update
    void Start()
    {
        this.enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>();
        lastNodeTouchedbyEnemy = GameObject.FindGameObjectWithTag("Waypoint").GetComponent<EnemyNode>();
        nodes = new List<EnemyNode>();

        for (int i = 0; i < transform.childCount; i++)
        {
            var thisNode = transform.GetChild(i).GetComponent<EnemyNode>();
            thisNode.system = this;
            thisNode.index = i;
            thisNode.gameObject.name = "NODE - " + i;
            nodes.Add(thisNode);
        }
    }

    public void newNodeTouchedbyPlayerRegistered(int index)
    {
        StopAllCoroutines();
        clearVisited();
        visitingNodes = new List<EnemyNode>();
        lastNodeTouchedbyPlayer = nodes[index];
        createWay(lastNodeTouchedbyEnemy, lastNodeTouchedbyPlayer);

        print("Best path to get to the player at "+lastNodeTouchedbyPlayer.name);
        foreach (EnemyNode node in visitingNodes)
        {
            print(node.name+" -> ");
        }

        visitingNodes.Reverse();
        StartCoroutine(BeginChase());
    }

    public IEnumerator BeginChase()
    {
        while (visitingNodes.Count >= 1)
        {
            iTween.MoveTo(enemy.gameObject, iTween.Hash("position", visitingNodes[0].transform.position, "time", 2f, "easeType", iTween.EaseType.linear));
            yield return new WaitForSeconds(2f);

            visitingNodes.RemoveAt(0);
        }

    }

    public void newNodeTouchedbyEnemyRegistered(int index)
    {
        lastNodeTouchedbyEnemy = nodes[index];
    }

    public bool createWay(EnemyNode current, EnemyNode target)
    {
        current.visited = true;
        bool found = false;
        if (current == target)
        {
            visitingNodes.Add(current);
            return true;
        }

        if (current.nodes.Count == 0)
        {
            return false;
        }

        for (int i = 0; i < current.nodes.Count; i++)
        {
            if(!current.nodes[i].visited)
                found = createWay(current.nodes[i], target);

            if (found) break;
        }

        if (found)
        {
            visitingNodes.Add(current);
        }

        return found;
    }

    public void clearVisited()
    {
        foreach (EnemyNode node in nodes)
        {
            node.visited = false;
        }
    }

}