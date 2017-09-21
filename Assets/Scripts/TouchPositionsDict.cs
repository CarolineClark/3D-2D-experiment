using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TouchPositionsDict {
    private EventSystem eventSystem;
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

    public TouchPositionsDict(EventSystem eventSystem) {
        this.eventSystem = eventSystem;
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

    public Vector3 GetFirstForId(Touch touch) {
        return GetTouchPositionsForId(touch).first;
    }

    public float GetStartTimeForId(Touch touch) {
        return GetTouchPositionsForId(touch).startTime;
    }
    public bool GetOnLeftForId(Touch touch) {
        return GetTouchPositionsForId(touch).onLeft;
    }

    public bool IsOnButtonForId(Touch touch) {
        return GetTouchPositionsForId(touch).onButton;
    }

    private TouchPositions GetTouchPositionsForId(Touch touch) {
        TouchPositions touchPositions;
        bool containsKey = touchPositionsDict.TryGetValue(touch.fingerId, out touchPositions);
        if (!containsKey) {
            touchPositions = new TouchPositions(touch.position, Time.time, OnButton(touch));
            touchPositionsDict.Add(touch.fingerId, touchPositions);
        }
        return touchPositions;
    }

    private bool OnButton(Touch touch) {
        return !eventSystem.IsPointerOverGameObject(touch.fingerId);
    }
}