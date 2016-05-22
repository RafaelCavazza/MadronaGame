using UnityEngine;
using System.Collections;

public class TextMesh : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var textMesh =  this.GetComponent<MeshRenderer>();
        textMesh.sortingLayerName = "Player";
        textMesh.sortingOrder = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

