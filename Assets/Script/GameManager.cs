using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private float mFadeDeltaTime;
    [SerializeField]
    private float mFadeInTime;
    [SerializeField]
    private float mFadeOutTime;
    [SerializeField]
    private float mStayFadeInTime;

    [Header("Boolean Data")]
    [SerializeField]
    private bool bOnFadeIn;
    [SerializeField]
    private bool bOnFadeOut;
    [SerializeField]
    private bool bOnStayFade;

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
    [SerializeField]
    private Image mFadeIn_OutImage;

    // Use this for initialization
    void Start() {

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bOnFadeIn = true;
        }

        StartFadeIn_Out();

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
    #endregion

    #region Fade In/Out
    void StartFadeIn_Out()
    {
        if (bOnFadeIn)
        {
            StartFadeIn(mFadeInTime);
        }
        else if (bOnStayFade)
        {
            StayFade(mStayFadeInTime);
        }
        else if (bOnFadeOut)
        {
            StartFadeOut(mFadeOutTime);
        }
    }

    void StartFadeIn(float fadeInTime)
    {
        Color tempColor = mFadeIn_OutImage.color;

        mFadeDeltaTime += Time.deltaTime;
        tempColor.a = Mathf.Lerp(0, 1, mFadeDeltaTime / fadeInTime);
        mFadeIn_OutImage.color = tempColor;

        if (mFadeDeltaTime >= fadeInTime)
        {
            bOnFadeIn = false;
            bOnStayFade = true;
            mFadeDeltaTime = 0.0f;
        }
    }

    void StartFadeOut(float fadeOutTime)
    {
        Color tempColor = mFadeIn_OutImage.color;

        mFadeDeltaTime += Time.deltaTime;
        tempColor.a = Mathf.Lerp(1, 0, mFadeDeltaTime / fadeOutTime);
        mFadeIn_OutImage.color = tempColor;

        if (mFadeDeltaTime >= fadeOutTime)
        {
            mFadeDeltaTime = 0.0f;
            bOnFadeOut = false;
        }
    }

    void StayFade(float stayFadeTime)
    {
        mFadeDeltaTime += Time.deltaTime;

        if (mFadeDeltaTime >= stayFadeTime)
        {
            mFadeDeltaTime = 0.0f;
            bOnStayFade = false;
            bOnFadeOut = true;
        }
    }

    #endregion
}
