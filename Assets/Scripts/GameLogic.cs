/*
MIT License

Copyright (c) 2022 Michele Maione

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{

    [Range(10, 1000)]
    public int tiles = 10;

    public GameObject tile;

    private GameColors[,] field;

    private static readonly Dictionary<GameColors, Color32> Colors = new()
    {
        { GameColors.CyanBlueAzure, new Color32(77, 146, 188, 255) },
        { GameColors.LightFuchsiaPink, new Color32(244, 137, 246, 255) },
        { GameColors.InterdimensionalBlue, new Color32(54, 0, 221, 255) },
        { GameColors.BittersweetShimmer, new Color32(188, 77, 79, 255) },
        { GameColors.MustardBrown, new Color32(202, 133, 0, 255) },
        { GameColors.HarlequinGreen, new Color32(49, 217, 0, 255) },
        { GameColors.OrangeYellow, new Color32(253, 199, 96, 255) },
        { GameColors.AndroidGreen, new Color32(159, 188, 77, 255) }
    };

    void Start()
    {
        GenerateLevel();
    }

    void Update()
    {

    }

    private void GenerateLevel()
    {
        field = new GameColors[tiles, tiles];

        for (var x = 0; x < tiles; x++)
            for (var y = 0; y < tiles; y++)
            {
                var t = Instantiate(tile);
                t.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                t.transform.position = new Vector3(0.5f * x, 0.5f * y, 0);

                var gc = x == 0 && y == 0 ? GameColors.CyanBlueAzure : (GameColors)Random.Range(0, 7);

                field[x, y] = gc;

                var spriteRenderer = t.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Colors[gc];
            }
    }

    internal bool IsAdjacent()
    {
        return true;
    }

}
