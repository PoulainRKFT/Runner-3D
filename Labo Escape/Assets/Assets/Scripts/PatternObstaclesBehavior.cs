using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternObstaclesBehavior : MonoBehaviour
{
    [Serializable]
    public class Pattern {
        public GameObject pattern;
        public int luck;
    }

    public List<Pattern> availableObstaclesList;

    void ShuffleAvailablePatternList() {
        for (int i = 0; i < availableObstaclesList.Count; i++) {
            Pattern tmp = availableObstaclesList[i];
            int randomValue = UnityEngine.Random.Range(0, availableObstaclesList.Count);
            availableObstaclesList[i] = availableObstaclesList[randomValue];
            availableObstaclesList[randomValue] = tmp;
        }
    }

    Pattern ChoseRandomly() {
        ShuffleAvailablePatternList();
        
        while (true) {
            foreach (Pattern pattern in availableObstaclesList) {
                if (UnityEngine.Random.Range(1, 101) <= pattern.luck) {
                    return pattern;
                }
            }
        }
    }

    void AddPattern(GameObject pattern) {
            GameObject newPattern = Instantiate(pattern, this.transform.position, Quaternion.identity);
            newPattern.transform.SetParent(this.transform);
            newPattern.transform.parent.Rotate(new Vector3(0f, UnityEngine.Random.Range(0, 2) * 180f, 0f));
    }

    // Start is called before the first frame update
    void Start()
    {
        AddPattern(ChoseRandomly().pattern);
    }
}
