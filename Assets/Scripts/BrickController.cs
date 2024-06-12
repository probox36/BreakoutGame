using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BrickController : MonoBehaviour
{

    public Vector2Int size;
    public Vector2 offset;
    public GameObject brickPrefab;
    public Gradient gradient;
    private List<GameObject> rows;

    public void notifyWhenBrickDestroyed() {

        for (int rowNum = 0; rowNum < size.y; rowNum++) {

            var row = rows[rowNum];
            if (row.GetComponentsInChildren<Brick>().Length < 1) {
                if (rowNum < size.y-1) { moveRowsDown(rowNum); }
                rows.RemoveAt(rowNum);
                rows.Add(row);
                placeRow(row, 0);
            }
        }
    }

    private void setBricksBreakable(bool value, GameObject row) {
        
        foreach (Brick brick in row.GetComponentsInChildren<Brick>()) {
            brick.breakable = value;
        }
    }

    private IEnumerator slideDownCoroutine(GameObject row, Vector3 target, float duration) {
        
        float timeElapsed = 0;
        Vector3 velocity = Vector3.zero;
        setBricksBreakable(false, row);
        
        while (timeElapsed < duration) {

            row.transform.position = Vector3.SmoothDamp(row.transform.position, target, ref velocity, duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        setBricksBreakable(true, row);
    }

    private void moveRowsDown(int removedRowNum) {

        for(int rowNum = size.y - 1; rowNum > removedRowNum; rowNum--) {
            
            var rowToMove = rows[rowNum];
            var target = rowToMove.transform.position;
            target.y -= offset.y + 0.5f;
            StartCoroutine(slideDownCoroutine(rowToMove, target, 0.15f));
        }
    }

    private void placeRow(GameObject row, float yPos) {

        for (int colNum = 0; colNum < size.x; colNum++)  {
            GameObject newBrick = Instantiate(brickPrefab, transform);
            newBrick.transform.position = transform.position + 
                new Vector3(offset.x*(colNum - 0.5f*(size.x - 1)), yPos, 0);
            var brickParams = newBrick.GetComponent<Brick>();
            var brickSprite = newBrick.GetComponent<SpriteRenderer>();

            brickParams.brickController = this;

            if (Random.Range(0f, 10f) < 9.5) {
                brickParams.type = BrickType.Regular;
                brickSprite.color = gradient.Evaluate((float)colNum / (size.y - 1));
            } else {
                brickParams.type = BrickType.Hard;
                brickSprite.color = Color.yellow;
            }

            newBrick.transform.SetParent(row.transform);
        }

    }

    public void Awake() {

        rows = new List<GameObject>();
        clear();
        for (int rowNum = 1; rowNum <= size.y; rowNum++)  {

            GameObject newRow = new GameObject { name = "Row " + rowNum };
            rows.Add(newRow);
            placeRow(newRow, offset.y*(rowNum - size.y));
        }
    }

    public void clear() {
        foreach (GameObject row in rows) {
            Destroy(row);
        }
    }

    void Start() { }

    void Update() { }

}
