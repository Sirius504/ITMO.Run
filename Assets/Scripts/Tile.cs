using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    protected float tileSize = 6.25f;
 
    protected SpriteRenderer spRenderer;

    public void Start()
    {
         spRenderer = GetComponent<SpriteRenderer>();
    }


    public void PlaceAtStart(float startAt)
    {

        transform.position = new Vector3(startAt, transform.position.y, 0);

    }

    public void ChangeTexture2D(Sprite texture)
    {
        spRenderer.sprite = texture;
    }

    internal void PlaceOverLastTile(float lastTilePosition, float lastTileSize)
    {
        transform.position = new Vector3(lastTilePosition + lastTileSize - 0.001f, transform.position.y, 0);
    }
}
