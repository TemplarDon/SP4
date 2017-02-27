using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class FugoSkill : BaseSkills
{
    // Damage all characters nearby
    public GameObject Animation;
    public int Range = 2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DoEffect(BaseCharacter user)
    {
        LevelGenerate Map = GameObject.Find("MapGeneration").GetComponent<LevelGenerate>();

        Vector3 mapPos = new Vector3(user.pos.x, user.pos.y, 0);

        List<BaseCharacter> AffectedCharacters = Map.GetCharactersInRange(mapPos, Range);

        foreach (BaseCharacter aCharacter in AffectedCharacters)
        {
            GameObject.Find(aCharacter.name).GetComponent<BaseCharacter>().TakeDamage(1);
            Debug.Log("Damage Taken!");
        }

        List<Vector3> CheckedPos = new List<Vector3>();
        List<Vector3> ExplosionTiles = RecursiveFindTiles(mapPos, Range, CheckedPos);

        foreach (Vector3 aPos in ExplosionTiles)
        {
            GameObject go = Instantiate(Animation, aPos, Quaternion.identity) as GameObject;
            go.GetComponent<CleanUp>().enabled = true;
        }
    }

    List<Vector3> RecursiveFindTiles(Vector3 checkPos, int tilesLeft, List<Vector3> checkedPos)
    {
        List<Vector3> returnList = new List<Vector3>();

        checkedPos.Add(checkPos);
        returnList.Add(checkPos);

        if (tilesLeft > 0)
        {
            Vector3 newPos = checkPos - new Vector3(0, 1, 0);
            List<Vector3> tempList1 = new List<Vector3>();
            if (!checkedPos.Contains(newPos))
            {
                tempList1 = RecursiveFindTiles(newPos, tilesLeft - 1, checkedPos);
            }

            newPos = checkPos - new Vector3(0, -1, 0);
            List<Vector3> tempList2 = new List<Vector3>();
            if (!checkedPos.Contains(newPos))
            {
                tempList2 = RecursiveFindTiles(newPos, tilesLeft - 1, checkedPos);
            }

            newPos = checkPos - new Vector3(1, 0, 0);
            List<Vector3> tempList3 = new List<Vector3>();
            if (!checkedPos.Contains(newPos))
            {
                tempList3 = RecursiveFindTiles(newPos, tilesLeft - 1, checkedPos);
            }

            newPos = checkPos - new Vector3(-1, 0, 0);
            List<Vector3> tempList4 = new List<Vector3>();
            if (!checkedPos.Contains(newPos))
            {
                tempList4 = RecursiveFindTiles(newPos, tilesLeft - 1, checkedPos);
            }

            foreach (Vector3 aPos in tempList1)
            {
                returnList.Add(aPos);
            }

            foreach (Vector3 aPos in tempList2)
            {
                returnList.Add(aPos);
            }

            foreach (Vector3 aPos in tempList3)
            {
                returnList.Add(aPos);
            }

            foreach (Vector3 aPos in tempList4)
            {
                returnList.Add(aPos);
            }
        }

        return returnList;
    }
}
