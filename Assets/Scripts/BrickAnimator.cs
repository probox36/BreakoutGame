using System.Collections;
using UnityEngine;

public class BrickAnimator : MonoBehaviour
{   
    public GameObject brokenBrickPrefab;

    private IEnumerator AnimateBrokenBrickCoroutine(GameObject brokenBrick) {

        float timeElapsed = 0;
        
        while (timeElapsed < 0.75) {
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(brokenBrick);

    }
    public void animateBrokenBrick(GameObject brick) {

        GameObject brokenBrick = Instantiate(brokenBrickPrefab, transform);
        brokenBrick.transform.position = brick.transform.position;
        var targetColor = brick.GetComponent<SpriteRenderer>().color;
        
        var allChildren = brokenBrick.transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < allChildren.Length; i++) {
            var child = allChildren[i].gameObject;
            child.GetComponent<SpriteRenderer>().color = targetColor;
        }

        StartCoroutine(AnimateBrokenBrickCoroutine(brokenBrick));
    }    
    void Start() { }

    void Update() { }
}

