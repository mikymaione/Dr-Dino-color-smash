/*
MIT License

Copyright (c) 2022 Michele Maione

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerLogic : MonoBehaviour
{
    [Range(0.1f, 100f)]
    public float Speed = 1f;

    public SpriteLibraryAsset[] Players;

    private Vector3 _targetPosition;

    private Animator _animator;
    private SpriteLibrary _spriteLibrary;

    private void Start()
    {
        _targetPosition = transform.position;
        _animator = GetComponent<Animator>();
        _spriteLibrary = GetComponent<SpriteLibrary>();
    }

    private void Update()
    {
        if (transform.position.Equals(_targetPosition))
        {
            // end of the walk
            _animator.SetBool("isMoving", false);
        }
        else
        {
            // walk to "_targetPosition"
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Speed * Time.deltaTime);
        }
    }

    internal void ChangeSprite(int i)
    {
        _spriteLibrary.spriteLibraryAsset = Players[i];
    }

    internal void MoveTo(Vector3 position)
    {
        var absV = transform.position.x > position.x ? -1 : 1;

        transform.localScale = new Vector3(absV * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        _targetPosition = position;
        _animator.SetBool("isMoving", true);
    }


}
