using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{

    public static int w = 10; //width
    public static int h = 13; //height
    public static DefaultElement[,] elements = new DefaultElement[w, h];

    //uncover all mines after hitting 1
    public static void uncoverMines()
    {
        foreach (DefaultElement elem in elements)
            if (elem.mine)
            {
                elem.LoadTexture(0);
            }
    }

    // find out mine location
    public static bool mineAt(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < w && y < h)
            return elements[x, y].mine;
        return false;
    }

    //count adjacent mines for an element
    public static int adjacentMines(int x, int y)
    {
        int count = 0;

        if (mineAt(x, y + 1)) ++count; // top
        if (mineAt(x + 1, y + 1)) ++count; // top-right
        if (mineAt(x + 1, y)) ++count; // right
        if (mineAt(x + 1, y - 1)) ++count; // bottom-right
        if (mineAt(x, y - 1)) ++count; // bottom
        if (mineAt(x - 1, y - 1)) ++count; // bottom-left
        if (mineAt(x - 1, y)) ++count; // left
        if (mineAt(x - 1, y + 1)) ++count; // top-left

        return count;
    }

    //flood fill empty elements
    public static void FFuncover(int x, int y, bool[,] visited)
    {    //coordinates in set range
        if (x >= 0 && y >= 0 && x < w && y < h)
        {   // visited already
            if (visited[x, y])
                return;
            //uncover element
            elements[x, y].LoadTexture(adjacentMines(x, y));

            // Dont go on when close to mine
            if (adjacentMines(x, y) > 0)
                return;

            // set flag
            visited[x, y] = true;

            FFuncover(x - 1, y, visited);
            FFuncover(x + 1, y, visited);
            FFuncover(x, y - 1, visited);
            FFuncover(x, y + 1, visited);
        }
    }

    public static bool isFinished()
    {
        // try to find a covered element that is not a mine
        foreach (DefaultElement elem in elements)
            if (elem.Iscovered() && !elem.mine)
                return false;

        // there are none so game won
        return true;
    }
}
