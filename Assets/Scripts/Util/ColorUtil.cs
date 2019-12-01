using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorUtil
{
    static public Color ryRed
    {
        get {
            Color newColor;
            ColorUtility.TryParseHtmlString("#FF5555", out newColor);
            return newColor;
        }
    }

    static public Color ryDarkRed
    {
        get
        {
            Color newColor;
            ColorUtility.TryParseHtmlString("#800A0A", out newColor);
            return newColor;
        }
    }

    static public Color ryBlue
    {
        get
        {
            Color newColor;
            ColorUtility.TryParseHtmlString("#47D2EE", out newColor);
            return newColor;
        }
    }

    static public Color ryGray
    {
        get {
            Color newColor;
            ColorUtility.TryParseHtmlString("#383838", out newColor);
            return newColor;
        }
    }

    static public Color ryThinGray
    {
        get
        {
            Color newColor;
            ColorUtility.TryParseHtmlString("#F0F0F0", out newColor);
            return newColor;
        }
    }

    static public Color ryGreen
    {
        get
        {
            Color newColor;
            ColorUtility.TryParseHtmlString("#86FF8E", out newColor);
            return newColor;
        }
    }

    static public Color ryGold
    {
        get
        {
            Color newColor;
            ColorUtility.TryParseHtmlString("#EFAF00", out newColor);
            return newColor;
        }
    }

    static public Color rySivler
    {
        get
        {
            Color newColor;
            ColorUtility.TryParseHtmlString("#AEB3B5", out newColor);
            return newColor;
        }
    }
    
    static public Color ryBronze
    {
        get
        {
            Color newColor;
            ColorUtility.TryParseHtmlString("#C47022", out newColor);
            return newColor;
        }
    }

    static public Color ryMine
    {
        get
        {
            Color newColor;
            ColorUtility.TryParseHtmlString("#FFF097", out newColor);
            return newColor;
        }
    }

    static public Color ryPYellow
    {
        get
        {
            Color newColor;
            ColorUtility.TryParseHtmlString("#FFF08B", out newColor);
            return newColor;            
        }
    }

    static public Color ryPMagenta
    {
        get
        {
            Color newColor;
            ColorUtility.TryParseHtmlString("#FBBFA3", out newColor);
            return newColor;
        }
    }

    static public Color ryPRed
    {
        get
        {
            Color newColor;
            ColorUtility.TryParseHtmlString("#E8A1AF", out newColor);
            return newColor;
        }
    }

    static public Color ryPPurple
    {
        get
        {
            Color newColor;
            ColorUtility.TryParseHtmlString("#CD9FC4", out newColor);
            return newColor;
        }
    }
}
