using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class GameManager : MonoBehaviour {

    [Header("PostProcessing Control")]
    [SerializeField]
    private PostProcessingProfile mPostProcessing;

    [System.Serializable]
    class WallMaterial
    {
        public Material PlaneMaterial;
        public Material BackMaterial;
        public Material LeftMaterial;
        public Material RigthMaterial;
        public Material FrontMaterial;
        public Material RoofMaterial;
    }

    [Header("Material")]
    [SerializeField]
    private WallMaterial mCurrentWallMaterial;
    [SerializeField]
    private WallMaterial[] mAllWallMaterialGroup;

    enum Wall
    {
        Plane, Back, Left, Right, Front, Roof
    }

    [Header("Index Data")]
    [SerializeField]
    private int mCurrentWallIndex;
    [SerializeField]
    private const int WallLength = 6;

    [System.Serializable]
    class WallMesh
    {
        public MeshRenderer Plane;
        public MeshRenderer Back;
        public MeshRenderer Left;
        public MeshRenderer Right;
        public MeshRenderer Front;
        public MeshRenderer Roof;
    }

    [Header("Reference Obejct")]
    [SerializeField]
    private WallMesh mWallMesh;

    [Header("Start Event Object")]
    [SerializeField]
    private EventableObject mStartEventObject;

    // Use this for initialization
    void Start() {
        mStartEventObject.Activate();
        SetPostProcessing();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mCurrentWallIndex++;

            if (mCurrentWallIndex >= mAllWallMaterialGroup.Length)
            {
                mCurrentWallIndex = 0;
            }

            ChangeCurrentMaterial(mCurrentWallIndex);
        }
    }

    #region Change Wall Material
    void ChangeCurrentMaterial(int arrayIndex)
    {
        mCurrentWallMaterial = mAllWallMaterialGroup[arrayIndex];

        for (int index = 0; index < WallLength; index++)
        {
            ChangeWallMaterial(index);
        }
    }

    void ChangeWallMaterial(int wallIndex)
    {
        switch (wallIndex)
        {
            case (int)Wall.Plane:
                mWallMesh.Plane.material = mCurrentWallMaterial.PlaneMaterial;
                break;

            case (int)Wall.Back:
                mWallMesh.Back.material = mCurrentWallMaterial.BackMaterial;
                break;

            case (int)Wall.Left:
                mWallMesh.Left.material = mCurrentWallMaterial.LeftMaterial;
                break;

            case (int)Wall.Right:
                mWallMesh.Right.material = mCurrentWallMaterial.RigthMaterial;
                break;

            case (int)Wall.Front:
                mWallMesh.Front.material = mCurrentWallMaterial.FrontMaterial;
                break;

            case (int)Wall.Roof:
                mWallMesh.Roof.material = mCurrentWallMaterial.RoofMaterial;
                break;
        }
    }
    #endregion

    void SetPostProcessing()
    {
        DepthOfFieldModel.Settings temp = new DepthOfFieldModel.Settings();
        temp = mPostProcessing.depthOfField.settings;
        temp.focalLength = 35;

        mPostProcessing.depthOfField.enabled = true;
        mPostProcessing.depthOfField.settings = temp;
        mPostProcessing.grain.enabled = false;
    }

    
}
