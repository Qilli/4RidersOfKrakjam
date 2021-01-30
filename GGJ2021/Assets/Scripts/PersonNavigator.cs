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

    public bool gameplayPerson = false;

    float timer = 0f;

    private void Awake()
    {
        follower = gameObject.GetComponent<Follower>();
        //follower.
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

        follower.destinationChanged(currentNode, destination);
    }

    public void setCurrentNode(Node node)
    {
        currentNode = node;
    }

    bool didOnce = false;

    private void Update()
    {
        if (!didOnce)
        {
            setDestination(currentNode[1]);
            didOnce = true;
        }

        if (follower.ended)
        {
            currentNode = follower.lastNode;
        }
        
        if(follower.ended && currentNode.waitTime > 0)
        {
            timer += Time.deltaTime;
            if(timer < currentNode.waitTime)
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
        //Debug.Log(currentNode.connections.Count);
    }

}
