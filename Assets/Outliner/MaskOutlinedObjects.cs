using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MaskOutlinedObjects : MonoBehaviour {

    //Post process data
    [Range(1, 5)]
    public int thickness = 5;

    [Range(0, 1)]
    public float blending = 0.5f;
    protected Shader shaderRender;
    private Material m_Material;

    //Outline data
    private Shader shaderMask;
	private GameObject maskCameraObj;
	private Camera maskCamera;
	private RenderTexture maskTexture;
	private Camera mainCamera;
    Texture2D defaultTexture;

    //Material controller
    protected Material material
    {
        get
        {
            if (m_Material == null)
            {
                m_Material = new Material(shaderRender);
                m_Material.hideFlags = HideFlags.HideAndDontSave;
            }
            return m_Material;
        }
    }

    //Common awake
    void Awake() {
        shaderMask = Shader.Find("3y3net/OutlineMask");
        Color mmc = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        defaultTexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        defaultTexture.SetPixel(0, 0, mmc);
        defaultTexture.SetPixel(1, 0, mmc);
        defaultTexture.SetPixel(0, 1, mmc);
        defaultTexture.SetPixel(1, 1, mmc);
        defaultTexture.Apply();
        Shader.SetGlobalTexture("_SpriteMask", defaultTexture);

        shaderRender = Shader.Find("3y3net/OutlineRender");
    }

	void OnEnable() {
		if(!shaderMask || !shaderMask.isSupported) {
			enabled = false;
			return;
		}

        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
        
        // Disable the image effect if the shader can't run on the users graphics card
        if (!shaderRender || !shaderRender.isSupported)
        {
            enabled = false;
            return;
        }

        //Get start_page camera_page
        mainCamera = GetComponent<Camera>();
		//Create render camera_page
		if(!maskCameraObj) {
			maskCameraObj = new GameObject("OutlineMaskCamera", typeof(Camera));
			maskCameraObj.hideFlags = HideFlags.HideAndDontSave;
			maskCamera = maskCameraObj.GetComponent<Camera>();
			maskCamera.enabled = false;
		}
	}

	void OnDisable() {
		DestroyImmediate(maskCameraObj);    
        if (m_Material)
        {
            DestroyImmediate(m_Material);
        }
    }
	
	void OnPreCull() {
		if(!enabled || !gameObject.activeSelf)
			return;
		//Prepare RT
		maskTexture = RenderTexture.GetTemporary(mainCamera.pixelWidth, mainCamera.pixelHeight, 24, RenderTextureFormat.ARGB32);
		//Prepare the camera_page for rendering
		maskCamera.CopyFrom(mainCamera);
		maskCamera.clearFlags = CameraClearFlags.SolidColor;
        maskCamera.backgroundColor = new Color(0, 0, 0, 0);
		maskCamera.renderingPath = RenderingPath.Forward;
		maskCamera.targetTexture = maskTexture;
		
		//Render
		maskCamera.RenderWithShader(shaderMask, "RenderType");
		//Set the RT available globally
		Shader.SetGlobalTexture("_OutlineTextureMask", maskTexture);
	}
	
	void OnPostRender() {
		if(!enabled || !gameObject.activeSelf)
			return;
		RenderTexture.ReleaseTemporary(maskTexture);
	}

    //Post process methods
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        material.SetFloat("_Thickness", thickness);
        material.SetFloat("_Blending", blending + 0.5f);
        Graphics.Blit(src, dest, material);
    }
}
