﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(SusanFOV))]
public class SusanEditor : Editor
{
    private void OnSceneGUI()
    {
        SusanFOV fov = (SusanFOV) target;

        //Draws view reach
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.forward, Vector3.up, 360, fov.viewRadius);

        //Draws cone of view
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);
    }
}
