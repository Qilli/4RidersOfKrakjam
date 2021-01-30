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
    Node currentNode;
    public Person p;



    public bool gameplayPerson = false;

    float waitTimeInNextPoint = 0f;
    float timer = 0f;

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
            setDestination(currentNode.getRandomConnectedNode());
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
                int rand = Random.Range(0, nodesLength);
                setDestination(currentNode.connections[rand]);
                timer = 0;
            }

        }


        if(currentNode == follower.lastNode)
        {
           // currentNode.type.onEnter(p);
        }
        //Debug.Log(currentNode.connections.Count);
    }

}
