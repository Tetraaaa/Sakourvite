using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoader : MonoBehaviour {

    public Sprite backgroundSprite;
    public int layerOrder;
    public float speed;
    public Vector3 initialPosition;
    public float maxXPosition;
    public float minXPosition;
    public Vector3 respawnPosition;

    private List<GameObject> instantiated = new List<GameObject>();

	// Use this for initialization
	void Start () {       
        GameObject instance = InstantiateBackgroundPrefabWithParameters(backgroundSprite, layerOrder, initialPosition,Quaternion.identity, gameObject.transform);
        SetInitialVelocity(instance);
        instantiated.Add(instance);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (instantiated.Count < 2)
        {
            if (instantiated[0].transform.position.x < maxXPosition)
            {
                GameObject instance = InstantiateBackgroundPrefabWithParameters(backgroundSprite, layerOrder, respawnPosition, Quaternion.identity, gameObject.transform);
                SetInitialVelocity(instance);
                instantiated.Add(instance);
            }
        }
        else
        {
            if (instantiated[0].transform.position.x < minXPosition)
            {
                GameObject toDelete = instantiated[0];
                instantiated.RemoveAt(0);
                Destroy(toDelete);
            }
        }
        foreach(GameObject o in instantiated)
        {
            SetInitialVelocity(o);
        }
	}

    void SetInitialVelocity(GameObject o)
    {
        o.GetComponent<Rigidbody2D>().velocity = new Vector2(-(GameController.Instance.levelOneSpeedRatio * speed), 0);
    }

    GameObject InstantiateBackgroundPrefabWithParameters(Sprite s, int orderInLayer, Vector3 screenPos, Quaternion rotation, Transform transform)
    {
        GameObject background = Instantiate(Resources.Load("Background_Parallax")) as GameObject;
        background.transform.SetParent(transform);
        background.GetComponent<SpriteRenderer>().sprite = s;
        background.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        background.transform.position = screenPos;
        background.transform.rotation = rotation;
        return background;
    }
}
