using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private Tilemap tilemap;
    private BlockManager blockManager;
    private GameObject previewBlock;
    private bool isPlacing = false;
    private void Awake()
    {
        blockManager = GetComponent<BlockManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isPlacing = !isPlacing;
            if (isPlacing)
                ShowPreviewBlock();
            else
                HidePreviewBlock();

                
        }
        if (isPlacing && previewBlock != null)
        {
            Vector3Int gridPosition = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            previewBlock.transform.position = tilemap.GetCellCenterWorld(gridPosition);

            if (Input.GetMouseButtonDown(0) && blockManager.woodAmount >= blockManager.woodPerBlock)
            {
                PlaceBlock(gridPosition);
            }
        }
    }

    void ShowPreviewBlock()
    {
        previewBlock = Instantiate(blockPrefab);
        Color color = previewBlock.GetComponent<SpriteRenderer>().color;
        color.a = 0.7f;
        previewBlock.GetComponent<SpriteRenderer>().color = color;
    }

    void HidePreviewBlock()
    {
        if (previewBlock != null)
            Destroy(previewBlock);
    }

    void PlaceBlock(Vector3Int gridPosition)
    {
        blockManager.UseWood(10);

        GameObject block = Instantiate(blockPrefab, tilemap.GetCellCenterWorld(gridPosition), Quaternion.identity);
        SpriteRenderer sr = block.GetComponent<SpriteRenderer>();
        Color color = sr.color;
        color.a = 1f;                                          
        sr.color = color;
    }
}
