using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchPositionsDict {
    private class TouchPositions {
		Vector3 f;
		public Vector3 first { 
             get {
                  return f;
             }
             set {
                  f = value;
             }
        }
        float startTimePrivate;
        public float startTime
        {
            get {
                return startTimePrivate;
            }
            set {
                startTimePrivate = value;
            }
        }

        public bool onLeft;
        public bool onButton {get; set;}

        public TouchPositions(Vector3 first, float startTime, bool onButton) {
            this.first = first;
            this.startTime = startTime;
            this.onLeft = IsOnLeft(first);
            this.onButton = onButton;
        }

        bool IsOnLeft(Vector3 position) {
            return (position.x < (Screen.width / 2));
        }
	}

    private Dictionary<int, TouchPositions> touchPositionsDict;

    public TouchPositionsDict() {
        touchPositionsDict = new Dictionary<int, TouchPositions>();
    }

    public void AddFingerPosition(int fingerId, Vector3 first, float startTime, bool onButton) {
        TouchPositions touchPositions;
        bool containsKey = touchPositionsDict.TryGetValue(fingerId, out touchPositions);
        if (containsKey) {
            touchPositions.first = first;
            touchPositions.startTime = startTime;
            touchPositions.onLeft = (first.x < (Screen.width / 2));
        } else {
            touchPositions = new TouchPositions(first, startTime, onButton);
            touchPositionsDict.Add(fingerId, touchPositions);
        }
    }

    public Vector3 GetFirstForId(int fingerId) {
        return GetTouchPositionsForId(fingerId).first;
    }

    public float GetStartTimeForId(int fingerId) {
        return GetTouchPositionsForId(fingerId).startTime;
    }
    public bool GetOnLeftForId(int fingerId) {
        return GetTouchPositionsForId(fingerId).onLeft;
    }

    public bool IsOnButtonForId(int fingerId) {
        return GetTouchPositionsForId(fingerId).onButton;
    }

    private TouchPositions GetTouchPositionsForId(int fingerId) {
        TouchPositions touchPositions;
        bool containsKey = touchPositionsDict.TryGetValue(fingerId, out touchPositions);
        if (!containsKey) {
            Debug.LogError("no key matching");
        }
        return touchPositions;
    }
}