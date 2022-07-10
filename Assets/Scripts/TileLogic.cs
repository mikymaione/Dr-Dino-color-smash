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

    void Start()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        _gameLogic = gameController.GetComponent<GameLogic>();
    }

    void OnMouseDown()
    {
        var pl = Player.GetComponent<PlayerLogic>();

        var (pX, pY) = GetIndexPosition(pl.transform.position);
        var (mX, mY) = GetIndexPosition(transform.position);

        if (
            (pY.Equals(mY) && Mathf.Abs(pX - mX).Equals(1))
            || (pX.Equals(mX) && Mathf.Abs(pY - mY).Equals(1))
        )
        {
            pl.MoveTo(transform.position);

            _gameLogic.SetTileColor(mX, mY, pX, pY);
        }
    }

    private (int, int) GetIndexPosition(Vector3 aPosition)
    {
        var pX = aPosition.x / 0.5f;
        var pY = aPosition.y / 0.5f;

        return (Mathf.FloorToInt(pX), Mathf.FloorToInt(pY));
    }



}
