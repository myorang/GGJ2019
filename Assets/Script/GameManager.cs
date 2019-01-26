using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [System.Serializable]
    class WallMaterial
    {
        public Material ForntMaterial;
        public Material BehindMaterial;
        public Material LeftMaterial;
        public Material RigthMaterial;
        public Material DownMaterial;
        public Material UpMaterial;
    }

    [Header("Material")]
    [SerializeField]
    private WallMaterial mCurrentWallMaterial;
    [SerializeField]
    private WallMaterial[] mAllWallMaterialGroup;

    enum Wall
    {
        Front, Behind, Left, Right, Down, Up
    }

    [Header("Index Data")]
    [SerializeField]
    private int mCurrentWallIndex;
    [SerializeField]
    private const int WallLength = 6;

    [System.Serializable]
    class WallMesh
    {
        public MeshRenderer Front;
        public MeshRenderer Behind;
        public MeshRenderer Left;
        public MeshRenderer Right;
        public MeshRenderer Down;
        public MeshRenderer Up;
    }

    [Header("Reference Obejct")]
    [SerializeField]
    private WallMesh mWallMesh; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
            case (int)Wall.Front:
                mWallMesh.Front.material = mCurrentWallMaterial.ForntMaterial;
                break;

            case (int)Wall.Behind:
                mWallMesh.Behind.material = mCurrentWallMaterial.BehindMaterial;
                break;

            case (int)Wall.Left:
                mWallMesh.Left.material = mCurrentWallMaterial.LeftMaterial;
                break;

            case (int)Wall.Right:
                mWallMesh.Right.material = mCurrentWallMaterial.RigthMaterial;
                break;

            case (int)Wall.Down:
                mWallMesh.Down.material = mCurrentWallMaterial.DownMaterial;
                break;

            case (int)Wall.Up:
                mWallMesh.Up.material = mCurrentWallMaterial.UpMaterial;
                break;
        }
    } 

}
