using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(CustomerLOS))]
public class CustomerEditor : Editor
{
    void OnSceneGUI()
    {
        CustomerLOS los = (CustomerLOS)target;

        //Draws view reach
        Handles.color = Color.red;
        Handles.DrawWireArc(los.transform.position, Vector3.forward, Vector3.up, 360, los.viewRadius);

        //Draws cone of view
        //Vector3 viewAngleA = los.DirFromAngle(-los.viewAngle / 2, false);
        //Vector3 viewAngleB = los.DirFromAngle(los.viewAngle / 2, false);
        //Handles.DrawLine(los.transform.position, los.transform.position + viewAngleA * los.viewRadius);
        //Handles.DrawLine(los.transform.position, los.transform.position + viewAngleB * los.viewRadius);
    }
}
