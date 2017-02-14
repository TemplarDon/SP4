using UnityEngine;
using System.Collections;

public class Node {

    public int TileCost;
    public int AccCost;
    public Vector3 m_pos;

    public Node ParentNode = null;

    public void Init(int cost, int x, int y)
    {
        TileCost = cost;
        m_pos.x = x;
        m_pos.y = y;
    }

    public int CalculateAccCost()
    {
        int returnValue = 0;
        Node checkNode = ParentNode;

        while (checkNode != null)
        {
            returnValue += checkNode.TileCost;
            checkNode = checkNode.ParentNode;
        }

        AccCost = returnValue;
        return returnValue;
    }

}
