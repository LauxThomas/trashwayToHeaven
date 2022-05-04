using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;

	public static GameManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

	

	public float BackgroundVelocity = 1f;
	public float GridSize = 1f;
	public GameObject SolidGroundObject;
	public GameObject Player;
    
    public float placementOffset;
    
    public float behindPlayerSpawnPoint;

	private GameObject tmpBlock;

	private GameObject block;

    public static GameManager getInz()
	{
		return GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	public void buildBlock(Vector2 position, GameObject go)
	{
		block = Instantiate(go, position, new Quaternion(0, 0, 0, 0));
        block.layer = LayerMask.NameToLayer("Objects");
        block.GetComponent<PolygonCollider2D>().enabled = true;
		block.GetComponent<ObjectController>().enabled = true;
	}
	
	public void OnParticleCollision(GameObject other)
    {
        block.GetComponentInChildren<SpriteRenderer>().color = new Color(1,1,1,1);
    }

	public float getSolidGroundStart()
	{
        //Debug.Log("getSolidGroundStart: " + (SolidGroundObject.transform.position.x - ((getSolidGroundTextureSize().x * SolidGroundObject.transform.localScale.x) / 2f)));
        //return SolidGroundObject.transform.position.x - ((getSolidGroundTextureSize().x*SolidGroundObject.transform.localScale.x) / 2f);
        return behindPlayerSpawnPoint;
    }

	public float getSolidGroundEnd()
	{
        return SolidGroundObject.transform.position.x + ((getSolidGroundTextureSize().x*SolidGroundObject.transform.localScale.x) / 2f);
    }
	
	public Vector2 getSolidGroundTextureSize()
	{
		Texture2D tex = SolidGroundObject.GetComponent<SpriteRenderer>().sprite.texture;
		return new Vector2(tex.width / 100f, tex.height / 100f);
	}

	public Vector2 getSolidGroundWorldSize()
	{
		Vector2 texSize = getSolidGroundTextureSize();
		return new Vector2(texSize.x * SolidGroundObject.transform.localScale.x, texSize.y * SolidGroundObject.transform.localScale.y);
	}

	public static float getPlayerBottomPositionY()
	{
		float playerTextureSizeY = (getInz().Player.GetComponent<SpriteRenderer>().sprite.texture.height *
		                           getInz().Player.transform.localScale.y) / 100f;
		return getInz().Player.transform.position.y - (playerTextureSizeY / 2f);
	}

	public static float toGridPosition(float x)
	{
		return Mathf.Floor(x / getInz().GridSize) * getInz().GridSize;
	}
	
	public static Vector2 getCameraBounds()
	{

		float vertExtent = Camera.main.orthographicSize;
		float horzExtent = vertExtent * Camera.main.aspect;
		float minX = Camera.main.transform.position.x - horzExtent;
		float minY = Camera.main.transform.position.y - vertExtent;
		float maxX = Camera.main.transform.position.x + horzExtent;
		float maxY = Camera.main.transform.position.y + vertExtent;
		return new Vector2(maxX - minX, maxY - minY);
	}
}
