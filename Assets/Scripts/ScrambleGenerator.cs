// Source for scrambling ideas
// https://www.speedsolving.com/threads/scramble-generating-scripts-algorithms.34779/?scrlybrkr
// https://www.jaapsch.net/puzzles/cube3.htm

/*
 * Generate a sequence of L, R, U, D, F, B moves, such that (a) the same move doesn't occur twice in a row, and (b) there are no sequences such as "U D U" where the same move gets repeated without changing "axis".
 * For each move, add a suffix to specify a random amount of that move. So, for R, we randomly choose between R, R', and R2.
 * 
 * 
 * 
 * 
 * 
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class Scramble
{
    private List<string> scramble = new List<string>();

    public string finalScramble { get; set; }

    private string possibleMoves = "LRUDFB";
    private string possibleModifiers = " '2";

    public Scramble(int length)
    {
        CreateScramble(length);
        //Debug.Log(this.scramble);
        this.finalScramble = GetScramble();
    }
    public Scramble(string scramble)
    {
        for (int i = 0; i < scramble.Length-1; i+=2)
        {
            this.scramble.Add($"{scramble[i]}{scramble[i+1]}");
        }
        //Debug.Log(this.scramble);
        this.finalScramble = GetScramble();
    }

    public List<string> CreateScramble(int length)
    {
        System.Random rnd = new System.Random();

        for (int i = 0; i < length; i++)
        {
            string cache = $"{possibleMoves[rnd.Next(6)]}{possibleModifiers[rnd.Next(3)]}"; // not the most scientific method, but it works.

            this.scramble.Add(cache);
        }

        return this.scramble;
    }

    public string GetScramble()
    {
        string res = "";

        foreach(var scram in scramble)
        {
            res += scram + " ";
        }

        return res;
    }

    public (List<SIDE> sides, List<int> measures) InterpretScramble()
    {
        (List<SIDE> sides, List<int> measures) res = (new List<SIDE>(), new List<int>());

        foreach(var scram in scramble)
        {
            switch (scram[0])
            {
                case 'R':
                    res.sides.Add(SIDE.RIGHT);
                    res.measures.Add(InterpretCharacter(scram[1]));
                    break;

                case 'L':
                    res.sides.Add(SIDE.LEFT);
                    res.measures.Add(InterpretCharacter(scram[1]));
                    break;

                case 'U':
                    res.sides.Add(SIDE.UP);
                    res.measures.Add(InterpretCharacter(scram[1]));
                    break;

                case 'D':
                    res.sides.Add(SIDE.DOWN);
                    res.measures.Add(InterpretCharacter(scram[1]));
                    break;

                case 'F':
                    res.sides.Add(SIDE.FRONT);
                    res.measures.Add(InterpretCharacter(scram[1]));
                    break;

                case 'B':
                    res.sides.Add(SIDE.BACK);
                    res.measures.Add(InterpretCharacter(scram[1]));
                    break;

            }
        }

        return res;
    }

    private int InterpretCharacter(char mode)
    {
        int res;

        switch (mode)
        {
            case ' ':
                res = 90;
                break;

            case '\'':
                res = -90;
                break;

            case '2':
                res = 180;
                break;

            default:
                res = 0;
                break;
        }
        //Debug.Log($"Char: {mode} and res {res}");
        return res;
    }

}
