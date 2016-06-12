using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelMenu : MonoBehaviour
{

    public Camera _camera;
    public GameObject LevelSprite;
    public GameObject _lastBackgound;

    public static GameData GameData { get; set; }

    private float MaxHeigth = 0;
    private float MinHeigth = 0;
    private bool calculated = true;

    private double  lastTick;
    private double currentTick;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    // Use this for initialization
    void Start()
    {
        lastTick = DateTime.Now.Ticks;
        currentTick = DateTime.Now.Ticks;
        GameData = GetGameData();
        LoadLevels();
        if (_camera != null)
        {
            MinHeigth = _camera.transform.position.y;
        }
    }

    void Update()
    {
        MoveCamera();
        CreatNewBackGround();
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("Menu"); 
    }

    void MoveCamera()
    {
        //Logica de Mover a Tela
        //Link do Swipe http://forum.unity3d.com/threads/swipe-in-all-directions-touch-and-mouse.165416/
        currentTick = DateTime.Now.Ticks;
        if (_camera == null)
            return;
        var y = 0f;
        y += SwipeMouse();
        y += SwipeTouch();

        if (y == 0f)
            return;

        var deltaTime = (currentTick - lastTick)/2000000;
        Debug.Log(deltaTime.ToString());

        y = (float)((y * -1)/deltaTime);

        if (_camera.transform.position.y + y < MinHeigth)
        {
            _camera.transform.position = new Vector3(_camera.transform.position.x, MinHeigth, _camera.transform.position.z);
        }
        else if (_camera.transform.position.y + y > MaxHeigth)
        {
            _camera.transform.position = new Vector3(_camera.transform.position.x, MaxHeigth, _camera.transform.position.z);
        }
        else
        {
            _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y + y, _camera.transform.position.z);
        }
        lastTick = DateTime.Now.Ticks;
    }

    /// <summary>
    /// <para> Função para poupular os valores caso não existam </para>
    /// </summary>
    /// <returns></returns>
    private GameData SetLevelValue()
    {
        GameData = GetGameDataObj();
        SaveGameData();
        return GameData;
    }

    public GameData GetGameDataObj()
    {
        var levels = new List<LevelInfo>();
        levels.Add(new LevelInfo { SceneName = "Level1", SceneId = 1, SceneScore = 0 });
        levels.Add(new LevelInfo { SceneName = "Level2", SceneId = 2, SceneScore = 0 });
        levels.Add(new LevelInfo { SceneName = "Level3", SceneId = 3, SceneScore = 0 });
        levels.Add(new LevelInfo { SceneName = "Level4", SceneId = 4, SceneScore = 0 });
        levels.Add(new LevelInfo { SceneName = "Level5", SceneId = 5, SceneScore = 0 });
        levels.Add(new LevelInfo { SceneName = "Level6", SceneId = 6, SceneScore = 0 });
        levels.Add(new LevelInfo { SceneName = "Level7", SceneId = 7, SceneScore = 0 });
        levels.Add(new LevelInfo { SceneName = "Level8", SceneId = 8, SceneScore = 0 });
        levels.Add(new LevelInfo { SceneName = "Level9", SceneId = 9, SceneScore = 0 });
        levels.Add(new LevelInfo { SceneName = "Level10", SceneId = 10, SceneScore = 0 });
        return new GameData { LevelInfo = levels.ToArray() };
    }

    private GameData GetGameData()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("gameData")))
        {
            return SetLevelValue();
        }
        else
        {
            var byteArray = Convert.FromBase64String(PlayerPrefs.GetString("gameData"));
            var gameData = (GameData)ByteArrayToObject(byteArray);
            if (HasLevelInfoChanged(gameData))
            {
                PlayerPrefs.SetString("gameData", "");
                return GetGameData();
            }
            return gameData;
        }
    }

    public void LoadLevels()
    {
        var left = true;
        var y = -112f;
        var first = true;
        float x;
        foreach (var level in GameData.LevelInfo)
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
        SetMaxHeigth(y);
    }

    private void SetMaxHeigth(float y)
    {
        var camHeigth = (_camera.orthographicSize * 2f);
        MaxHeigth = y-(camHeigth/2);
    }

    public float SwipeMouse()
    {
        if (Input.GetMouseButtonUp(0)) //Quando solta o mause, resta os valores
        {
            firstPressPos = new Vector2(0, 0);
            secondPressPos = new Vector2(0, 0);
            calculated = true;
        }

        if (Input.GetMouseButton(0) && calculated)
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);  
            calculated = false;
        }

        if (Input.GetMouseButton(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (firstPressPos.y != secondPressPos.y)
                calculated = true;

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                return currentSwipe.y;
            }
            else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                return currentSwipe.y;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    void CreatNewBackGround()
    {
        if (_lastBackgound != null)
        {
            if (isMaxHeight())
            {
                var new_LastBackgound = GameObject.Instantiate(_lastBackgound);
                var transform = new_LastBackgound.GetComponent<Transform>();
                var sprite = new_LastBackgound.GetComponent<SpriteRenderer>();

                transform.position = new Vector2(transform.position.x, transform.position.y + (sprite.bounds.extents.y * 2));
                _lastBackgound = new_LastBackgound;
            }
        }
    }

    private bool isMaxHeight()
    {
        var currentHeight = _camera.transform.position.y;
        var lastBackgoundHeight = _lastBackgound.GetComponent<Transform>().position.y;
        return (currentHeight + 80) > lastBackgoundHeight;
    }

    public bool HasLevelInfoChanged(GameData memoryVal)
    {
        var obj = GetGameDataObj().LevelInfo;
        foreach (var item in obj)
        {
            if (!memoryVal.LevelInfo.Where(p => p.SceneName == item.SceneName).Any())
            {
                return true;
            }
        }
        return false;
    }

    public float SwipeTouch()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Moved && calculated)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Moved)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                if (firstPressPos.y != secondPressPos.y)
                    calculated = true;

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    return currentSwipe.y;
                }
                else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    return currentSwipe.y;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    static void SaveGameData()
    {
        var gameByte = ObjectToByteArray(GameData);
        PlayerPrefs.SetString("gameData", Convert.ToBase64String(gameByte));
    }

    static byte[] ObjectToByteArray(object obj)
    {
        if (obj == null)
            return null;
        var bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

    private object ByteArrayToObject(byte[] arrBytes)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        var obj = binForm.Deserialize(memStream);
        return obj;
    }
}


[Serializable()]
public class LevelInfo
{
    public string SceneName { get; set; }
    public int SceneId { get; set; }
    public int SceneScore { get; set; }
    public bool IsInLevel { get; set; }
}

[Serializable()]
public struct GameData
{
    public LevelInfo[] LevelInfo { get; set; }
}