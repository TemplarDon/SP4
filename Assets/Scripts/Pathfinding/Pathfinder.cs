using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Pathfinder : MonoBehaviour
{

    public GameObject MapObject;
    private LevelGenerate theLevel;
    private List<List<Node>> NodeList = new List<List<Node>>();

    List<Node> OpenList = new List<Node>();
    List<Node> ClosedList = new List<Node>();

    public Vector3 m_Destination;

    // Pathfinding Var
    private bool b_PathFound = false;
    private Node CurrentNode;

    // Translating Var
    private int currIdx = 0;

    // Use this for initialization
    void Start()
    {
        theLevel = MapObject.GetComponent<LevelGenerate>();

        // Fill up NodeList
        for (int y = 0; y < theLevel.ysize; ++y)
        {
            for (int x = 0; x < theLevel.xsize; ++x)
            {
                Node toAdd = new Node();
                toAdd.Init(theLevel.GetTileCost(x, y), x, y);
                NodeList[y][x] = toAdd;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FindAStarPath(Vector3 dest)
    {
        m_Destination = dest;
        b_PathFound = false;

        CurrentNode = GetNode(this.transform.position);

        List<Node> Neighbours = new List<Node>();

        while (!b_PathFound)
        {
            ClosedList.Add(CurrentNode);

            if (ValidateNode(GetNode(this.transform.position + new Vector3(0, 1, 0))))
            {
                OpenList.Add(GetNode(this.transform.position + new Vector3(0, 1, 0)));
            }

            if (ValidateNode(GetNode(this.transform.position + new Vector3(0, -1, 0))))
            {
                OpenList.Add(GetNode(this.transform.position + new Vector3(0, -1, 0)));
            }

            if (ValidateNode(GetNode(this.transform.position + new Vector3(1, 0, 0))))
            {
                OpenList.Add(GetNode(this.transform.position + new Vector3(1, 0, 0)));
            }

            if (ValidateNode(GetNode(this.transform.position + new Vector3(-1, 0, 0))))
            {
                OpenList.Add(GetNode(this.transform.position + new Vector3(-1, 0, 0)));
            }

            Node TempLowest = GetLowestF(OpenList);

            CurrentNode = TempLowest;
            Neighbours.Clear();

            if (TempLowest.m_pos == m_Destination)
                b_PathFound = true;
        }

        // Add current node to closed list

        // Get Neighbours of curr node, compute F-values and add to openlist

        // Get neighbour with lowest F value ()

        // Get that neighbour's neighbours, set that neighbour as the curr node

        // Repeat

        // Closed list will be the path to follow
    }

    void FollowPath()
    {
        if (b_PathFound)
        {
            Vector3 dir = (this.transform.position - ClosedList[currIdx].m_pos).normalized * Time.deltaTime * 10;
            this.transform.Translate(dir);

            if ((this.transform.position - ClosedList[currIdx].m_pos).sqrMagnitude < 10)
            {
                currIdx++;
            }

        }
    }

    void SetDestination(Vector3 dest)
    {

    }

    float GetManhattenDistance(Node aNode)
    {
        return (Mathf.Abs(m_Destination.x - aNode.m_pos.x) + Mathf.Abs(m_Destination.y - aNode.m_pos.y));
    }

    Node GetNode(Vector3 pos)
    {
        for (int y = 0; y < theLevel.ysize; ++y)
        {
            for (int x = 0; x < theLevel.xsize; ++x)
            {
                if (NodeList[y][x].m_pos.Equals(pos))
                {
                    return NodeList[y][x];
                }
            }
        }

        return null;
    }

    bool ValidateNode(Node checkNode)
    {
        if (checkNode == null)
            return false;

        if (checkNode.TileCost != -1 && !ClosedList.Contains(checkNode))
        {
            return true;
        }

        return false;
    }

    Node GetLowestF(List<Node> checkList)
    {
        int LowestF_Value = 99999;
        int LowestF_Idx = 0;
        for (int i = 0; i < checkList.Count; ++i)
        {
            Node check = checkList[i];
            if (check.CalculateAccCost() + GetManhattenDistance(check) < LowestF_Value)
            {
                LowestF_Value = check.AccCost + (int)GetManhattenDistance(check);
                LowestF_Idx = i;
            }
        }
        return checkList[LowestF_Idx];
    }
}
