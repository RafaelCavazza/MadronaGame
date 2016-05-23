using UnityEngine;

public class TextMeshScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var textMesh =  this.GetComponent<MeshRenderer>();
        textMesh.sortingLayerName = "Player";
        textMesh.sortingOrder = 1;
	} 
}

