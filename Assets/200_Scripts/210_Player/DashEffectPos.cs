using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffectPos
{
    #region Front
    public static Vector3 FrontPos
    {
        get
        {
            return new Vector3(0, 0, 4);
        }
    }

    public static Vector3 FrontRot
    {
        get
        {
            return new Vector3(0, 180, 0);
        }
    }
    #endregion
    #region FrontRight
    public static Vector3 FrontRightPos
    {
        get
        {
            return new Vector3(3, 0, 3);
        }
    }

    public static Vector3 FrontRightRot
    {
        get
        {
            return new Vector3(0, 225, 0);
        }
    }
    #endregion
    #region FrontLeft
    public static Vector3 FrontLeftPos
    {
        get
        {
            return new Vector3(-3, 0, 3);
        }
    }

    public static Vector3 FrontLeftRot
    {
        get
        {
            return new Vector3(0, 135, 0);
        }
    }
    #endregion
    #region Back
    public static Vector3 BackPos
    {
        get
        {
            return new Vector3(0, 0, -2);
        }
    }

    public static Vector3 BackRot
    {
        get
        {
            return new Vector3(0, 0, 0);
        }
    }
    #endregion
    #region BackRight
    public static Vector3 BackRightPos
    {
        get
        {
            return new Vector3(1, 0, -1);
        }
    }

    public static Vector3 BackRightRot
    {
        get
        {
            return new Vector3(0, 315, 0);
        }
    }
    #endregion
    #region BackLeft
    public static Vector3 BackLeftPos
    {
        get
        {
            return new Vector3(-1, 0, -1);
        }
    }

    public static Vector3 BackLeftRot
    {
        get
        {
            return new Vector3(0, 45, 0);
        }
    }
    #endregion
    #region Right
    public static Vector3 RightPos
    {
        get
        {
            return new Vector3(2.5f, 0, 0);
        }
    }

    public static Vector3 RightRot
    {
        get
        {
            return new Vector3(0, 270, 0);
        }
    }
    #endregion
    #region Left
    public static Vector3 LeftPos
    {
        get
        {
            return new Vector3(-2.5f, 0, 0);
        }
    }

    public static Vector3 LeftRot
    {
        get
        {
            return new Vector3(0, 90, 0);
        }
    }
    #endregion
}