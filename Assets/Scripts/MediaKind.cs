using UnityEngine;
using System.Collections;

public class MediaKind : MonoBehaviour
{
	private ConditionActionK myKind;
    private bool isInitialMedia;

    public string MediaId;
    public GameObject[] prefabs;
    public GameObject initialIconPrefab;
	private Object currentIcon;
    private Object initialIcon;
 
	// Use this for initialization
	void Start ()
	{
		MyKind = ConditionActionK.none;
        isInitialMedia = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public ConditionActionK MyKind
    {
		get{ 
			return myKind;
		}
		set{ 
			if (MyKind != value) {
				myKind = value;
				this.InstantiateCurrentKind ();
			}
		}
	}

    public bool IsInitialMedia
    {
        get
        {
            return isInitialMedia;
        }
        set
        {
            isInitialMedia = value;
            if (isInitialMedia)
            {
                InstantiateSymbol(); 
            }
            else
            {
                RemoveInitialSymbol();
            }
            
        }
    }

	private void InstantiateCurrentKind()
    {
		Destroy (currentIcon);
		if(this.MyKind!=ConditionActionK.none)currentIcon = Instantiate(prefabs [(int)this.MyKind], transform, false);
	}

    private void RemoveInitialSymbol()
    {
        Destroy(initialIcon);
    }

    private void InstantiateSymbol()
    {
        initialIcon = Instantiate(initialIconPrefab, transform, false);
    }
}

