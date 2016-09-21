using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Tiles {
	Unset,
	Ground,
	Spike,
	Water,
	Rainbow
}

[Serializable]
public class TileSprite {
	public string tName;
	public Sprite tImage;
	public Tiles tType;

	public TileSprite() {
		tName = "Unset";
		tImage = new Sprite ();
		tType = Tiles.Unset;
	}

	public TileSprite(string name, Sprite image, Tiles tile)
	{
		this.tName = name;
		this.tImage = image;
		this.tType = tile;
	}
}

public class TileMapController : MonoBehaviour {

	public List<TileSprite> tileSprites;
	public Vector2 mapSize;
	public Sprite defaultImage;
	public GameObject tileContainerPrefab;
	public GameObject tilePrefab;
	public Vector2 currentPosition;
	public Vector2 viewPortSize;

	private TileSprite[,] _map;
	private GameObject controller;
	private GameObject _tileContainer;
	private List<GameObject> _tiles = new List<GameObject>();

	private TileSprite findTile(Tiles tile)
	{
		foreach (TileSprite tileSprite in tileSprites) {
			if (tileSprite.tType == tile)
				return tileSprite;
		}
		return null;
	}

	private void defaultTiles() 
	{
		for (int y = 0; y < mapSize.y; y++) {
			for (int x = 0; x < mapSize.x; x++) {
				_map [x, y] = new TileSprite ("Unset", defaultImage, Tiles.Unset);
			}
		}
	}

	private void setTiles()
	{
		int index = 0;
		for (int y = 0; y < mapSize.y; y++) {
			for (int x = 0; x < mapSize.x; x++) {
				_map [x, y] = new TileSprite (tileSprites[index].tName, tileSprites[index].tImage, tileSprites[index].tType);
				index++;
				if (index > tileSprites.Count - 1)
					index = 0;
			}
		}
	}

	private void addTilesToWorld()
	{
		foreach (GameObject o in _tiles) {
			Destroy (o);
		}
		_tiles.Clear ();
		// Maybe we could use Lean Pool for this
		Destroy (_tileContainer);
		_tileContainer = Instantiate (tileContainerPrefab);
		float tileSize = 0.64F;
		float viewOffsetX = viewPortSize.x / 2F;
		float viewOffsetY = viewPortSize.y / 2F;
		for (float y = -viewOffsetY; y < viewOffsetY; y++) {
			for (float x = -viewOffsetX; x < viewOffsetX; x++) {
				float tX = x*tileSize;
				float tY = y*tileSize;

				float iX = x + currentPosition.x;
				float iY = y + currentPosition.y;

				if (iX < 0)
					continue;
				if (iY < 0)
					continue;
				if (iX > mapSize.x - 2)
					continue;
				if (iY > mapSize.y - 2)
					continue;
				
				// Maybe we could use Lean Pool for this
				GameObject t = Instantiate (tilePrefab);
				t.transform.position = new Vector3 (tX, tY, -9);
				t.transform.SetParent (_tileContainer.transform);
				SpriteRenderer renderer = t.GetComponent<SpriteRenderer> ();
				renderer.sprite = _map [(int)x + (int)currentPosition.x, (int)y + (int)currentPosition.y].tImage;
				_tiles.Add (t);
			}
		}
	}

	public void Start()
	{
		controller = GameObject.Find ("Controller");
		_map = new TileSprite[(int) mapSize.x, (int) mapSize.y];

		defaultTiles ();
		setTiles ();
	}

	private void Update()
	{
		addTilesToWorld ();
	}
}
