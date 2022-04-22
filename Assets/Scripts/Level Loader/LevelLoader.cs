using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static int loadedLevel = 1;
    public static int maxLevel { get; private set; }
    [SerializeField] private float sideIndentHorizontal = 0f;
    [SerializeField] private float sideIndentVehrical = 2f;

    private Levels levels;
    private GameObject blockPrefab;
    private BlockType typeList;
    void Start()
    {

        LoadResources();
        LoadLevel(loadedLevel - 1);
    }

    private void LoadLevel(int levelNumber)
    {
        int bocksInLine = levels.level[levelNumber].line[0].block.Length;
        float blockFieldWidth = GetFieldWidth() - 2 * sideIndentHorizontal;
        float blockSize = blockFieldWidth / bocksInLine;
        BlockFieldCreator(levelNumber, blockSize);



    }
    private void BlockFieldCreator(int levelNumber, float blockSize)
    {
        NumberBlock[] lines = levels.level[levelNumber].line;
        Vector3 blockPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMin, Screen.safeArea.yMax, -Camera.main.transform.position.z));
        blockPoint += new Vector3(sideIndentHorizontal + (blockSize / 2), - sideIndentVehrical - (blockSize / 2));
        float startPointX = blockPoint.x;

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0 ; j < lines[i].block.Length; j++)
            {
                if (lines[i].block[j] != 0)
                {
                    GameObject block = Instantiate(blockPrefab, blockPoint, Quaternion.identity, transform);
                    Block blockData = block.GetComponent<Block>();
                    blockData.SetData(lines[i].block[j], typeList.sprites, out Sprite sprite);
                    float blockScale = GetBlockScale(sprite, blockSize);
                    block.transform.localScale = new Vector3(blockScale, blockScale);
                }
                blockPoint.x += blockSize;
            }
            blockPoint.x = startPointX;
            blockPoint.y -= blockSize;
        }
    }

    private float GetBlockScale(Sprite sprite, float blockSize)
    {
        return blockSize / (sprite.rect.width / sprite.pixelsPerUnit);
    }
    private float GetFieldWidth()
    {
        float _xMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.safeArea.width, 0f, 0f)).x;
        float _xMin = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        return _xMax - _xMin;
    }
    private void LoadResources()
    {
        TextAsset json = Resources.Load<TextAsset>("Text/LevelsList");
        levels = JsonUtility.FromJson<Levels>(json.text);
        maxLevel = levels.level.Length;
        blockPrefab = Resources.Load<GameObject>("Prefabs/Block");
        typeList = Resources.Load<BlockType>("ScriptableObject/BlockType");
    }
}