/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * VHS.cs
 * 
 * 
 * Date:
 * 06-11-2019
 * 
 * 
 * Description:
 * Enables a VHS tape-style effect for the camera
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * MainCamera
 * 
 * 
 * Dependencies:
 * None
 * 
 * 
 * Changelog:
 * 06-11    Initial
 * 
 * =============================================================================
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VHS : MonoBehaviour
{
    private Material material;

    // ********************************************************************************************************

    void Awake()
    {
        material = new Material(Shader.Find("Custom/VHSeffect"));
        material.SetTexture("_SecondaryTex", Resources.Load("TVnoise") as Texture);
        material.SetFloat("_OffsetPosY", 0f);
        material.SetFloat("_OffsetColor", 0.01f);
        material.SetFloat("_OffsetDistortion", 480f);
        material.SetFloat("_Intensity", 0.64f);
    }

    // ========================================================================================================
    // ********************************************************************************************************
    // ========================================================================================================
    // ********************************************************************************************************
    // ========================================================================================================
    // ********************************************************************************************************
    // ========================================================================================================
    // ********************************************************************************************************
    // ========================================================================================================
    // ********************************************************************************************************
    // ========================================================================================================

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // TV noise
        material.SetFloat("_OffsetNoiseX", Random.Range(0f, 1f));
        float offsetNoise = material.GetFloat("_OffsetNoiseY");
        material.SetFloat("_OffsetNoiseY", offsetNoise + Random.Range(-0.03f, 0.01f));

        // Vertical shift
        float offsetPosY = material.GetFloat("_OffsetPosY");
        if (offsetPosY > 0.0f)
        {
            material.SetFloat("_OffsetPosY", offsetPosY - Random.Range(0f, offsetPosY));
        }
        else if (offsetPosY < 0.0f)
        {
            material.SetFloat("_OffsetPosY", offsetPosY + Random.Range(0f, -offsetPosY));
        }
        else if (Random.Range(0, 100) == 1)
        {
            material.SetFloat("_OffsetPosY", Random.Range(-0.2f, 0.2f));
        }

        // Channel color shift
        float offsetColor = material.GetFloat("_OffsetColor");
        if (offsetColor > 0.001f)
        {
            material.SetFloat("_OffsetColor", offsetColor - 0.001f);
        }
        else if (Random.Range(0, 1000) == 1)
        {
            material.SetFloat("_OffsetColor", Random.Range(0.003f, 0.12f));
        }

        // Distortion
        if (Random.Range(0, 15) == 1)
        {
            material.SetFloat("_OffsetDistortion", Random.Range(500f, 1500f));
        }
        else
        {
            material.SetFloat("_OffsetDistortion", 1500f);
        }

        Graphics.Blit(source, destination, material);
    }

    // ********************************************************************************************************
}