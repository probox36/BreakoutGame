using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class SpawnBricks : MonoBehaviour
{

    public Vector2Int size;
    public Vector2 offset;
    public GameObject brickPrefab;
    public Gradient gradient;
    private GameObject[][] bricks;

    void Awake() {

        for (int i = 0; i < size.x; i++)  {
            for (int j = 0; j < size.y; j++)  {
                GameObject newBrick = Instantiate(brickPrefab, transform);
                newBrick.transform.position = transform.position + 
                    new Vector3((float)((size.x - 1)*0.5f - i) * offset.x, j*offset.y, 0);
                
                var brickParams = newBrick.GetComponent<Brick>();
                var brickSprite = newBrick.GetComponent<SpriteRenderer>();

                if (Random.Range(0f, 10f) < 9) {
                    brickParams.type = Brick.BrickType.Regular;
                    gradient.Evaluate((float)j / (size.y - 1));
                } else {
                    brickParams.type = Brick.BrickType.Hard;
                    brickSprite.color = Color.yellow;
                }
            }
        }
    }

    void Start() { }

    void Update() { }
}
