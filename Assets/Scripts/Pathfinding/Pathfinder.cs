using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Pathfinder : MonoBehaviour
{
    public LevelGenerate theLevel;
    private List<List<Node>> NodeList;

    List<Node> OpenList = new List<Node>();
    List<Node> ClosedList = new List<Node>();

    public Vector3 m_Destination;

    // Pathfinding Var
    private bool b_PathFound = false;
    private Node CurrentNode = null;

    private int CurrentItr = 0;
    private int ForceStop = 5;

    // Translating Var
    private int currIdx = 0;
    //private Vector3 OFFSET = new Vector3(0.5f, -0.5f, 0f);
    private Vector3 OFFSET = new Vector3(0, -0, 0);

    // Use this for initialization
    void Start()
    {
        //Debug.Log(theLevel.xsize + " " + theLevel.ysize);

        NodeList = new List<List<Node>>();

        // Fill up NodeList
        for (int row = 0; row < theLevel.ysize; ++row)
        {
            NodeList.Add(new List<Node>());
            for (int col = 0; col < theLevel.xsize; ++col)
            {
                //Debug.Log("Row: " + row + " Col: " + col);

                Node toAdd = new Node();
                toAdd.Init(theLevel.GetTileCost(col, row), theLevel.GetTilePos(col, row));
                NodeList[row].Add(toAdd);
                //NodeList[y][x] = toAdd;

                //Debug.Log("Creating");
            }
        }
        //Debug.Log("TestPathfinder");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FindPath(Vector3 dest)
    {
        m_Destination.x = Mathf.Round(dest.x);
        m_Destination.y = Mathf.Round(dest.y);

        if (!ValidateNode(CurrentNode = GetNode(this.transform.position)))
            return;

        List<Node> NeighbourList = new List<Node>();

        OpenList.Clear();
        ClosedList.Clear();

        while (!b_PathFound)
        {
            Debug.Log("Adding to closed list: " + CurrentNode.m_pos.ToString());
            ClosedList.Add(CurrentNode);

            Debug.Log("Dist to destination: " + (CurrentNode.m_pos - m_Destination).magnitude.ToString());
            if ((CurrentNode.m_pos - m_Destination).magnitude < 1.05)
            {
                b_PathFound = true;
                Debug.Log("Path to destination: " + m_Destination.ToString() + " found.");
            }


            if (ValidateNode(GetNode(CurrentNode.m_pos + new Vector3(0, 1, 0))))
            {
                OpenList.Add(GetNode(CurrentNode.m_pos + new Vector3(0, 1, 0)));
                NeighbourList.Add(GetNode(CurrentNode.m_pos + new Vector3(0, 1, 0)));
            }

            if (ValidateNode(GetNode(CurrentNode.m_pos + new Vector3(0, -1, 0))))
            {
                OpenList.Add(GetNode(CurrentNode.m_pos + new Vector3(0, -1, 0)));
                NeighbourList.Add(GetNode(CurrentNode.m_pos + new Vector3(0, -1, 0)));
            }

            if (ValidateNode(GetNode(CurrentNode.m_pos + new Vector3(1, 0, 0))))
            {
                OpenList.Add(GetNode(CurrentNode.m_pos + new Vector3(1, 0, 0)));
                NeighbourList.Add(GetNode(CurrentNode.m_pos + new Vector3(1, 0, 0)));
            }

            if (ValidateNode(GetNode(CurrentNode.m_pos + new Vector3(-1, 0, 0))))
            {
                OpenList.Add(GetNode(CurrentNode.m_pos + new Vector3(-1, 0, 0)));
                NeighbourList.Add(GetNode(CurrentNode.m_pos + new Vector3(-1, 0, 0)));
            }

            if (NeighbourList.Count <= 0)
            {
                return;
            }

            // Set all neghbours parent to current node
            foreach (Node aNode in NeighbourList)
            {
                aNode.ParentNode = CurrentNode;
            }

            //Debug.Log("OpenList Size: " + OpenList.Count.ToString());
            Node TempLowest = GetLowestF(OpenList);
            OpenList.Remove(TempLowest);
            CurrentNode = TempLowest;

            ++CurrentItr;

            NeighbourList.Clear();

        }

        //for (int i = 0; i < 3; ++i)
        //{
        //    Debug.Log("Adding to closed list: " + CurrentNode.m_pos.ToString());
        //    ClosedList.Add(CurrentNode);

        //    if (ValidateNode(GetNode(this.transform.position + new Vector3(0, 1, 0))))
        //    {
        //        OpenList.Add(GetNode(this.transform.position + new Vector3(0, 1, 0)));
        //    }

        //    if (ValidateNode(GetNode(this.transform.position + new Vector3(0, -1, 0))))
        //    {
        //        OpenList.Add(GetNode(this.transform.position + new Vector3(0, -1, 0)));
        //    }

        //    if (ValidateNode(GetNode(this.transform.position + new Vector3(1, 0, 0))))
        //    {
        //        OpenList.Add(GetNode(this.transform.position + new Vector3(1, 0, 0)));
        //    }

        //    if (ValidateNode(GetNode(this.transform.position + new Vector3(-1, 0, 0))))
        //    {
        //        OpenList.Add(GetNode(this.transform.position + new Vector3(-1, 0, 0)));
        //    }

        //    Debug.Log("OpenList Size: " + OpenList.Count.ToString());
        //    Node TempLowest = GetLowestF(OpenList);
        //    TempLowest.ParentNode = CurrentNode;

        //    Debug.Log("Dist to destination: " + (CurrentNode.m_pos - m_Destination).magnitude.ToString());
        //    if ((CurrentNode.m_pos - m_Destination).magnitude < 1.1f)
        //    {
        //        b_PathFound = true;
        //        Debug.Log("Path to destination: " + m_Destination.ToString() + " found.");

        //        OpenList.Clear();
        //        ClosedList.Clear();
        //        break;
        //    }

        //    CurrentNode = TempLowest;

        //    ++CurrentItr;
        //}

        // Add current node to closed list

        // Get Neighbours of curr node, compute F-values and add to openlist

        // Get neighbour with lowest F value ()

        // Get that neighbour's neighbours, set that neighbour as the curr node

        // Repeat

        // Closed list will be the path to follow
    }

    public void FollowPath()
    {
        if (b_PathFound)
        {
            ////Debug.Log("Idx: " + currIdx.ToString() + " ClosedList Size: " + ClosedList.Count.ToString());

            ////Vector3 dir = (this.transform.position - (ClosedList[currIdx].m_pos + OFFSET)).normalized * Time.deltaTime * 5;
            //Vector3 dir = (ClosedList[currIdx].m_pos + OFFSET - this.transform.position).normalized * Time.deltaTime * 5;
            //Debug.Log("Dir: " + dir.ToString() + " Idx: " + currIdx);

            //this.GetComponent<BaseCharacter>().pos.x += dir.x;
            //this.GetComponent<BaseCharacter>().pos.y += dir.y;

            //if ((this.transform.position - ClosedList[currIdx].m_pos + OFFSET).magnitude < 0.1)
            //{
            //    ++currIdx;

            //    if (currIdx >= ClosedList.Count)
            //    {
            //        currIdx = ClosedList.Count - 1;
            //        b_PathFound = false;
            //    }

            //    Mathf.Round(this.GetComponent<BaseCharacter>().pos.x);
            //    Mathf.Round(this.GetComponent<BaseCharacter>().pos.y);
            //}

            ////Debug.Log("Following path.");

            // Use the last node to get the path
            Node endNode = ClosedList[ClosedList.Count - 1];

            List<Node> Path = new List<Node>();

            Path.Add(endNode);

            while (endNode.ParentNode != null)
            {
                endNode = endNode.ParentNode;
                Path.Add(endNode);
            }

            Path.Reverse();

            Vector3 dir = (Path[currIdx].m_pos + OFFSET - this.transform.position).normalized * Time.deltaTime * 5;
            Debug.Log("Dir: " + dir.ToString() + " Idx: " + currIdx);

            this.GetComponent<BaseCharacter>().pos.x += dir.x;
            this.GetComponent<BaseCharacter>().pos.y += dir.y;

            if ((this.transform.position - Path[currIdx].m_pos + OFFSET).magnitude < 0.1)
            {
                ++currIdx;

                if (currIdx >= Path.Count)
                {
                    currIdx = Path.Count - 1;
                    b_PathFound = false;
                }

                Mathf.Round(this.GetComponent<BaseCharacter>().pos.x);
                Mathf.Round(this.GetComponent<BaseCharacter>().pos.y);
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
        for (int row = 0; row < theLevel.ysize; ++row)
        {
            for (int col = 0; col < theLevel.xsize; ++col)
            {
                //Debug.Log("Pos: " + pos.ToString() + " XIndex: " + x.ToString() + " YIndex: " + y.ToString());
                //Debug.Log(NodeList[y][x].m_pos.ToString());

                //Debug.Log(" XIndex: " + col.ToString() + " YIndex: " + row.ToString());
                //Debug.Log(NodeList[row][col].m_pos.ToString() + " compare to: " + pos.ToString());

                if (NodeList[row][col].m_pos.ToString().Equals(pos.ToString()))
                //if (NodeList[row][col].m_pos == (pos))
                {
                    //Debug.Log("Node found.");
                    return NodeList[row][col];
                }
            }
        }

        //Debug.Log("Can't find node. Returning NULL.");
        return null;
    }

    bool ValidateNode(Node checkNode)
    {
        if (checkNode == null)
        {
            //Debug.Log("Node Rejected. (NULL)");
            return false;
        }
         
        if (checkNode.TileCost != -1 && !CheckIfInClosedList(checkNode) && !CheckIfInOpenList(checkNode))
        {
            //Debug.Log("Node Accepted.");
            return true;
        }

        //Debug.Log("Node Rejected.");
        return false;
    }

    Node GetLowestF(List<Node> checkList)
    {
        if (checkList.Count <= 0)
            return null;

        int LowestF_Value = 99999;
        int LowestF_Idx = 0;
        for (int i = 0; i < checkList.Count; ++i)
        {
            if (checkList[i].CalculateAccCost() + GetManhattenDistance(checkList[i]) < LowestF_Value)
            {
                LowestF_Value = checkList[i].AccCost + (int)GetManhattenDistance(checkList[i]);
                LowestF_Idx = i;
            }
        }

        //Debug.Log("CheckList Size: " + checkList.Count.ToString());
        //Debug.Log("LowestIdx: " + LowestF_Idx.ToString());
        return checkList[LowestF_Idx];
    }

    bool CheckIfInClosedList(Node checkNode)
    {
        for (int i = 0; i < ClosedList.Count; ++i)
        {
            if (ClosedList[i].Equals(checkNode))
            {
                return true;
            }

            //if (ClosedList[i].m_pos.ToString().Equals(checkNode.m_pos.ToString()))
            //{
            //    return true;
            //}
        }

        return false;
    }

    bool CheckIfInOpenList(Node checkNode)
    {
        for (int i = 0; i < OpenList.Count; ++i)
        {
            if (OpenList[i].Equals(checkNode))
            {
                return true;
            }

            //if (OpenList[i].m_pos.ToString().Equals(checkNode.m_pos.ToString()))
            //{
            //    return true;
            //}
        }

        return false;
    }
}
