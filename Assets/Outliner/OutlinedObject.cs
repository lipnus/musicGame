using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutlinedObject : MonoBehaviour {

    public Color outlineColor = Color.red;

    public enum OutlineMode { MouseOver, UserCall, AllwaysOn } 
    public OutlineMode outlineMode = OutlineMode.MouseOver;      
    public bool flashing = false;    
    [Range(0.5f,5)]                               
    public float flashSpeed = 1.5f;                 
    public float threshold = 1.0f;

    float timeBlink, deltaRate;

    Texture2D maskTexture, maskNone;
    private List<Material> materials = new List<Material>();

    bool ApplyEffect = true;
    bool currentMode = false;
    bool blinkMode = false;

    float andThr;
    Color outAnt;

    void Awake() {
        materials.Clear();
        maskTexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        outlineColor.a = 1.0f;
        maskTexture.SetPixel(0, 0, outlineColor);
        maskTexture.SetPixel(1, 0, outlineColor);
        maskTexture.SetPixel(0, 1, outlineColor);
        maskTexture.SetPixel(1, 1, outlineColor);
        maskTexture.Apply();

        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            materials.Add(rend.material);

		andThr = threshold;
        outAnt = outlineColor;

        maskNone = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        maskNone.SetPixel(0, 0, new Color(0,0,0,0));
        maskNone.SetPixel(1, 0, new Color(0, 0, 0, 0));
        maskNone.SetPixel(0, 1, new Color(0, 0, 0, 0));
        maskNone.SetPixel(1, 1, new Color(0, 0, 0, 0));
        maskNone.Apply();

        if (outlineMode == OutlineMode.AllwaysOn)
            ApplyEffect = true;
        else
            ApplyEffect = false;

    }
	
	void OnEnable() {
        if (ApplyEffect)
        {
            foreach (Material material in materials)
                material.SetTexture("_SpriteMask", maskTexture);
        }
        else
        {
            foreach (Material material in materials)
                material.SetTexture("_SpriteMask", maskNone);
        }
        foreach (Material material in materials)
            material.SetFloat("_Threshold", threshold);
    }

    void OnMouseEnter()
    {
        if (outlineMode == OutlineMode.MouseOver)
            ApplyEffect = true;
    }

    public void UserCall(bool isOn)
    {
        ApplyEffect = isOn;
    }

    void OnMouseExit()
    {
        if (outlineMode == OutlineMode.MouseOver)
            ApplyEffect = false;
    }

    void LightOn()
    {
        if (currentMode)
            return;
        currentMode = true;
        foreach (Material material in materials)
            material.SetTexture("_SpriteMask", maskTexture);
    }

    void LightOff()
    {
        if (!currentMode)
            return;
        currentMode = false;
        foreach (Material material in materials)
            material.SetTexture("_SpriteMask", maskNone);
    }

    void Update()
    {
        if (flashing && ApplyEffect)
        {
            deltaRate = 1f / (2f * flashSpeed);

            if (Time.time - timeBlink > deltaRate)
            {
                timeBlink = Time.time;
                blinkMode = !blinkMode;
                if (!blinkMode)
                    LightOff();
                else
                    LightOn();
            }
        }
        else
        {
            if (ApplyEffect)
                LightOn();
            else
                LightOff();
        }

        if (andThr!=threshold)
        {
            andThr = threshold;
            foreach (Material material in materials)
                material.SetFloat("_Threshold", threshold);
        }

        if(!outAnt.Equals(outlineColor))
        {
            outlineColor.a = 1.0f;
            outAnt = outlineColor;
            maskTexture.SetPixel(0, 0, outlineColor);
            maskTexture.SetPixel(1, 0, outlineColor);
            maskTexture.SetPixel(0, 1, outlineColor);
            maskTexture.SetPixel(1, 1, outlineColor);
            maskTexture.Apply();
            foreach (Material material in materials)
                material.SetTexture("_SpriteMask", maskTexture);
        }

    }

       

    void OnDisable() {
        foreach (Material material in materials)
            material.SetTexture("_SpriteMask", null);
	}
}
