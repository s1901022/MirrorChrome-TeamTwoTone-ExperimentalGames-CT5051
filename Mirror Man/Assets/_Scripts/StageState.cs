﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageState : MonoBehaviour {
    //Controls the flipping of the tileset and is used to check for other flippable states too
    [SerializeField]
    private GameObject normalMap;
    [SerializeField]
    private GameObject invertedMap;

    private bool m_mirrored = false;

    public bool GetMirrorState() { return m_mirrored; }
    public float GetMirrorStateFloat() {
        if (m_mirrored == true) {
            return -1;
        }
        else {
            return 1;
        }
    }
    public void SetMirrorState(bool a_mirrorState) {
        m_mirrored = a_mirrorState;
        normalMap.GetComponent<scrTileAlphaFlip>().AlphaFlip(normalMap.GetComponent<scrTileAlphaFlip>().GetInitialAlpha() * -1);
        invertedMap.GetComponent<scrTileAlphaFlip>().AlphaFlip(normalMap.GetComponent<scrTileAlphaFlip>().GetInitialAlpha() * -1);
    }

    public void ResetTileset() {
        normalMap.GetComponent<scrTileAlphaFlip>().AlphaFlip(normalMap.GetComponent<scrTileAlphaFlip>().GetInitialAlpha() * -1);
        invertedMap.GetComponent<scrTileAlphaFlip>().AlphaFlip(normalMap.GetComponent<scrTileAlphaFlip>().GetInitialAlpha() * -1);
    }



    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
