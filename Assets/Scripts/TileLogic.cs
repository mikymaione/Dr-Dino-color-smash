/*
MIT License

Copyright (c) 2022 Michele Maione

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using UnityEngine;

public class TileLogic : MonoBehaviour
{
    public GameObject Player;

    private GameLogic _gameLogic;

    private void Start()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        _gameLogic = gameController.GetComponent<GameLogic>();
    }

    private void OnMouseDown()
    {
        var playerLogic = Player.GetComponent<PlayerLogic>();

        var (fromX, fromY) = GetIndexPosition(playerLogic.transform.position);
        var (toX, toY) = GetIndexPosition(transform.position);

        var isNearInX = fromX.Equals(toX) && Mathf.Abs(fromY - toY).Equals(1);
        var isNearInY = fromY.Equals(toY) && Mathf.Abs(fromX - toX).Equals(1);

        if (isNearInX || isNearInY)
        {
            _gameLogic.SetTileColor(fromX, fromY, toX, toY);
            playerLogic.MoveTo(transform.position);
            playerLogic.ChangeSprite(1);
        }

        /*
        if (_gameLogic.IsReacheable(fromX, fromY, toX, toY))
        {
            _gameLogic.SetTileColor(fromX, fromY, toX, toY);
            playerLogic.MoveTo(transform.position);
        }
        */
    }

    private static (int, int) GetIndexPosition(Vector3 aPosition)
    {
        var pX = aPosition.x / 0.5f;
        var pY = aPosition.y / 0.5f;

        return (Mathf.FloorToInt(pX), Mathf.FloorToInt(pY));
    }



}
