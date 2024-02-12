using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCorridorBehavior : MonoBehaviour
{
    public GameObject player;

    [Serializable]
    public class Pattern {
        public GameObject pattern;
        public int luck;
    }

    public List<Pattern> availableCorridorsList;
    public GameObject startingPoint;
    public GameObject corridorsParent;
    public GameObject bigDoorsPattern;

    private int counterForBigDoors = 0;

    void ShuffleAvailablePatternList() {
        for (int i = 0; i < availableCorridorsList.Count; i++) {
            Pattern tmp = availableCorridorsList[i];
            int randomValue = UnityEngine.Random.Range(0, availableCorridorsList.Count);
            availableCorridorsList[i] = availableCorridorsList[randomValue];
            availableCorridorsList[randomValue] = tmp;
        }
    }

    Pattern ChoseRandomly() {
        ShuffleAvailablePatternList();
        
        while (true) {
            foreach (Pattern pattern in availableCorridorsList) {
                if (UnityEngine.Random.Range(1, 101) <= pattern.luck) {
                    return pattern;
                }
            }
        }
    }

    void AddPattern(GameObject pattern) {
        GameObject newPattern = Instantiate(pattern, startingPoint.transform.position, Quaternion.identity);
        newPattern.GetComponent<AutoDestroyWhenTooFar>().SetPlayer(player);
        if (newPattern.name == "Corridor Big Doors(Clone)") {
            newPattern.transform.Find("Pattern").Find("Left Door").GetComponent<BigDoorOpening>().SetPlayer(player);
            newPattern.transform.Find("Pattern").Find("Right Door").GetComponent<BigDoorOpening>().SetPlayer(player);
        } else {
            newPattern.transform.Find("Pattern").transform.Rotate(new Vector3(0f, UnityEngine.Random.Range(0, 2) * 180f, 0f));
        }
        newPattern.transform.SetParent(corridorsParent.transform);
        startingPoint = newPattern.transform.Find("Ending Point").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (corridorsParent.transform.childCount < 18) {
            AddPattern(ChoseRandomly().pattern);
            counterForBigDoors += 1;
        }

        if (counterForBigDoors >= 5) {
            counterForBigDoors = 0;
            AddPattern(bigDoorsPattern);
        }
    }
}
