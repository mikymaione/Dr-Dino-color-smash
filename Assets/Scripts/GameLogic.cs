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
    public GameObject player;

    private GameColors[,] _fieldColors;
    private GameObject[,] _fieldTiles;

    private PlayerLogic _playerLogic;


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

    private void Start()
    {
        GenerateLevel();

        _playerLogic = player.GetComponent<PlayerLogic>();
    }

    private void GenerateLevel()
    {
        _fieldColors = new GameColors[tiles, tiles];
        _fieldTiles = new GameObject[tiles, tiles];

        for (var x = 0; x < tiles; x++)
            for (var y = 0; y < tiles; y++)
            {
                var t = Instantiate(tile);
                t.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                t.transform.position = new Vector3(0.5f * x, 0.5f * y, 0);

                var gc = x == 0 && y == 0 ? GameColors.CyanBlueAzure : (GameColors)Random.Range(0, 7);

                _fieldColors[x, y] = gc;
                _fieldTiles[x, y] = t;

                var spriteRenderer = t.GetComponent<SpriteRenderer>();
                spriteRenderer.color = Colors[gc];
            }
    }

    private static (int, int) GetIndexPosition(Vector3 aPosition)
    {
        var pX = aPosition.x / 0.5f;
        var pY = aPosition.y / 0.5f;

        return (Mathf.FloorToInt(pX), Mathf.FloorToInt(pY));
    }

    internal void MovePlayer(Vector3 dest)
    {
        var (toX, toY) = GetIndexPosition(dest);
        var (fromX, fromY) = GetIndexPosition(player.transform.position);

        if (IsReachable(fromX, fromY, toX, toY))
        {
            var toC = SetTileColor(fromX, fromY, toX, toY);

            _playerLogic.MoveTo(dest, toC);

            if (IsGameFinished())
            {

            }
        }
    }

    private bool IsGameFinished()
    {
        // check that tiles are the same color
        for (var x = 0; x < tiles; x++)
            for (var y = 0; y < tiles; y++)
                if (_fieldColors[0, 0] != _fieldColors[x, y])
                    return false;

        return true;
    }

    private GameColors SetTileColor(int fromX, int fromY, int toX, int toY)
    {
        var fromC = _fieldColors[fromX, fromY];
        var toC = _fieldColors[toX, toY];

        if (fromC.Equals(toC))
            return fromC;

        var surroundings = new List<(int, int)>
        {
            (fromX, fromY)
        };

        GetSurrounding(surroundings, fromC, fromX, fromY);

        foreach (var p in surroundings)
        {
            var t = _fieldTiles[p.Item1, p.Item2];
            var spriteRenderer = t.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Colors[toC];

            _fieldColors[p.Item1, p.Item2] = toC;
        }

        return toC;
    }

    private void GetSurrounding(List<(int, int)> surroundings, GameColors c, (int, int) p)
    {
        GetSurrounding(surroundings, c, p.Item1, p.Item2);
    }

    private void GetSurrounding(List<(int, int)> surroundings, GameColors c, int x, int y)
    {
        var up = (x, y - 1);
        var down = (x, y + 1);
        var left = (x - 1, y);
        var right = (x + 1, y);

        (int, int)[] toTest = { up, down, left, right };

        foreach (var p in toTest)
            if (PositionExists(p) &&
                !surroundings.Contains(p) &&
                    _fieldColors[p.Item1, p.Item2].Equals(c))
            {
                surroundings.Add(p);
                GetSurrounding(surroundings, c, p);
            }
    }

    private bool IsReachable(int fromX, int fromY, int toX, int toY)
    {
        var fromC = _fieldColors[fromX, fromY];
        var toC = _fieldColors[toX, toY];

        if (fromC.Equals(toC))
        {
            if (fromX == toX && fromY == toY)
                return true;

            if (fromX == toX)
            {
                var dir = toY >= fromY ? 1 : -1;

                return IsReachable(fromX, fromY + dir, toX, toY);
            }

            if (fromY == toY)
            {
                var dir = toX >= fromX ? 1 : -1;

                return IsReachable(fromX + dir, fromY, toX, toY);
            }
        }
        else
        {
            var isNearInX = fromX.Equals(toX) && Mathf.Abs(fromY - toY).Equals(1);
            var isNearInY = fromY.Equals(toY) && Mathf.Abs(fromX - toX).Equals(1);

            return isNearInX || isNearInY;
        }

        return false;
    }

    private bool PositionExists((int, int) p)
    {
        return p.Item1 > -1 && p.Item2 > -1 && p.Item1 < tiles && p.Item2 < tiles;
    }

}
