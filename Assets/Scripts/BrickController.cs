using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BrickController : MonoBehaviour
{

    public Vector2Int size;
    public Vector2 offset;
    public GameObject brickPrefab;
    public Gradient blueGradient;
    public Gradient greenGradient;
    public Gradient yellowGradient;
    public BrickAnimator brickAnimator;
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
            brickParams.brickAnimator = brickAnimator;

            var random = Random.Range(0f, 10f);

            if (random < 9) {
                brickParams.type = BrickType.Regular;
                brickSprite.color = blueGradient.Evaluate((float)colNum / (size.y - 1));
                newBrick.transform.SetParent(row.transform);
            } else if (random > 9.1 && random < 9.5) {
                brickParams.type = BrickType.Bonus;
                brickSprite.color = greenGradient.Evaluate((float)colNum / (size.y - 1));
                newBrick.transform.SetParent(row.transform);
            } else {
                GameObject brickParent = new GameObject();
                brickParent.transform.position = newBrick.transform.position;
                brickParent.transform.SetParent(row.transform);
                brickParams.type = BrickType.Hard;
                brickSprite.color = yellowGradient.Evaluate((float)colNum / (size.y - 1));
                newBrick.transform.SetParent(brickParent.transform);
            }
        }
    }

    public void Awake() {

        rows = new List<GameObject>();
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
