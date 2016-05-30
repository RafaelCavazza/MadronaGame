using System;
using UnityEngine;
using System.Collections.Generic;
using TextMesh = UnityEngine.TextMesh;

public class LevelMenu : MonoBehaviour
{

    public List<LevelInfo> LevelSelect { get; internal set; }
    public GameObject LevelSprite;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    // Use this for initialization
    void Start ()
	{
	    SetLevelValue();
	    LoadLevels();

	}

    void Update()
    {
        Swipe();
    }

    private void SetLevelValue()
    {
        LevelSelect = new List<LevelInfo>();
        LevelSelect.Add(new LevelInfo { SceneName =  "Main",   SceneId = 1, SceneScore = 0});
        LevelSelect.Add(new LevelInfo { SceneName = "Level2",  SceneId = 2, SceneScore = 0 });
        LevelSelect.Add(new LevelInfo { SceneName = "Level3",  SceneId = 3, SceneScore = 0 });
        LevelSelect.Add(new LevelInfo { SceneName = "Level4",  SceneId = 4, SceneScore = 0 });
        LevelSelect.Add(new LevelInfo { SceneName = "Level5",  SceneId = 5, SceneScore = 0 });
        LevelSelect.Add(new LevelInfo { SceneName = "Level6",  SceneId = 6, SceneScore = 0 });
        LevelSelect.Add(new LevelInfo { SceneName = "Level7",  SceneId = 7, SceneScore = 0 });
        LevelSelect.Add(new LevelInfo { SceneName = "Level8",  SceneId = 8, SceneScore = 0 });
        LevelSelect.Add(new LevelInfo { SceneName = "Level9",  SceneId = 9, SceneScore = 0 });
        LevelSelect.Add(new LevelInfo { SceneName = "Level10", SceneId = 10, SceneScore = 0 });
    }

    public void LoadLevels()
    {
        var left = true;
        var y = -112f;
        var first = true;
        float x;
        foreach (var level in LevelSelect)
        {
            var newLevel = Instantiate(LevelSprite);
            if (first)
            {
                Destroy(LevelSprite);
                LevelSprite = newLevel;
                first = false;
            }

            x = left ? -20 : 30;
            newLevel.transform.position = new Vector3(x, y, 0);
            var levelLoader = newLevel.GetComponent<LevelLoader>();
            levelLoader.SceneToLoad = level.SceneName;
            var textMesh = newLevel.GetComponentInChildren<UnityEngine.TextMesh>();
            textMesh.text = level.SceneId.ToString();
            left = !left;
            y += 30; 
        }
    }

    private void SetCameraSwipe()
    {
        throw  new NotImplementedException("Não Implementado");
    }

    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f &&  currentSwipe.x < 0.5f)
            {
                Debug.Log("up swipe");
            }
            //swipe down
            if (currentSwipe.y < 0 &&  currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("down swipe");
            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("left swipe");
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("right swipe");
            }
        }
    }
}

public class LevelInfo
{
    public string SceneName { get; set; }
    public int SceneId { get; set; }
    public int SceneScore { get; set; }
}
