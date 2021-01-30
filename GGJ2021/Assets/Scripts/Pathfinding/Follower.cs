using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Follower.
/// </summary>
[ExecuteInEditMode]
public class Follower : MonoBehaviour
{

	[SerializeField]
	protected Graph m_Graph;
	[SerializeField]
	protected Node m_Start;
	[SerializeField]
	protected Node m_End;
	[SerializeField]
	protected float m_Speed = 0.01f;
	[SerializeField]
	protected Path m_Path = new Path ();
	protected Node m_Current;

	public Node lastNode = null;

	void Start ()
	{
		//m_Path = m_Graph.GetShortestPath ( m_Start, m_End );
		//Follow ( m_Path );

		
	}

    private void Awake()
    {
		m_Graph = FindObjectOfType<Graph>();
	}


    public void destinationChanged(Node start, Node end)
    {
		m_Start = start;
		m_End = end;
		m_Path = m_Graph.GetShortestPath(start, end);
		Follow(m_Path);
    }

	/// <summary>
	/// Follow the specified path.
	/// </summary>
	/// <param name="path">Path.</param>
	public void Follow ( Path path )
	{
		ended = false;
		StopCoroutine ( "FollowPath" );
		m_Path = path;
		transform.position = m_Path.nodes [ 0 ].transform.position;
		StartCoroutine ( "FollowPath" );
	}

	public bool ended = false;

    /// <summary>
    /// Following the path.
    /// </summary>
    /// <returns>The path.</returns>
    IEnumerator FollowPath ()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.update += Update;
		#endif
		var e = m_Path.nodes.GetEnumerator ();
		while ( e.MoveNext () )
		{
			m_Current = e.Current;
			lastNode = e.Current;

			// Wait until we reach the current target node and then go to next node
			yield return new WaitUntil ( () =>
			{
				return transform.position == m_Current.transform.position;
			} );
		}
		ended = true;
		m_Current = null;
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.update -= Update;
		#endif
	}

	void Update ()
	{
		if ( m_Current != null )
		{
			transform.position = Vector3.MoveTowards ( transform.position, m_Current.transform.position, m_Speed );
		}
	}

    private void OnDrawGizmos()
    {

		if(m_Start != null)
        {
			Gizmos.color = Color.black;
			Gizmos.DrawSphere(m_Start.gameObject.transform.position, 0.5f);
		}
		if(m_End != null)
        {
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(m_End.gameObject.transform.position, 0.5f);
		}

	}

}
