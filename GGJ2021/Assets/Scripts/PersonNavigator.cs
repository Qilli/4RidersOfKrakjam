using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for navigating between POIs or going to a certain destination we give
/// </summary>

public class PersonNavigator : MonoBehaviour
{
    public Follower follower;
    [SerializeField]
   public Node currentNode;
    public Person p;



    public bool gameplayPerson = false;

    float waitTimeInNextPoint = 0f;
    float timer = 0f;

    /// <summary>
    /// Absolutny mode do ktorego udadza sie postacie po skonczeniu aktualnej sciezki
    /// </summary>
    Node importantNode = null;
    bool finalNodeComing = false;


    void setImportantNode(Node n)
    {
        Debug.Log("Important node changed");
        importantNode = n;
    }


    private void Awake()
    {
        follower = gameObject.GetComponent<Follower>();
    }


    public void setDestination(Node destination)
    {
        if(currentNode == null)
        {
            Debug.Log("current node error");
            return;
        }
        if(destination == null)
        {
            Debug.Log("Destination null");
            return;
        }

        //currentNode.type.onExit(p);

        destination.personsQueued.Add(p);
        waitTimeInNextPoint = destination.type.GetTimeWait();
        follower.destinationChanged(currentNode, destination);
    }

    public void setCurrentNode(Node node)
    {
        currentNode = node;
    }

    bool didOnce = false;

    private void Update()
    {
        /*
        if (!didOnce)
        {
            setDestination(currentNode[1]);
            didOnce = true;
        }

        */

        if(currentNode != null && !didOnce)
        {
            setDestination(currentNode.getRandomConnectedNodeWithoutFullPOI());
            didOnce = true;
        }

        if (follower.ended)
        {
            currentNode = follower.lastNode;
            //p.setWalking(false);
        }
        
        if(follower.ended)
        {
           // p.setWalking(false);
            timer += Time.deltaTime;
            if(timer < waitTimeInNextPoint)
                return;
        }

        if(currentNode != null && currentNode == follower.lastNode && follower.ended)
        {
            int nodesLength = currentNode.connections.Count;
            if(nodesLength > 0)
            {
                Debug.Log("randomize new node");
                if(importantNode != null && !finalNodeComing)
                {
                    setDestination(importantNode);
                    finalNodeComing = true;
                }
                else
                {
                    int rand = Random.Range(0, nodesLength);
                    setDestination(currentNode.connections[rand]);
                }

                timer = 0;
            }

        }


        if(currentNode == follower.lastNode)
        {
           // currentNode.type.onEnter(p);
        }
        //Debug.Log(currentNode.connections.Count);
    }

    //debug
    [SerializeField]
    Node endNode;
    [ContextMenu("go to end node")]
    public void goToEndNode()
    {
        setImportantNode(endNode);
    }
    //end debug

}
