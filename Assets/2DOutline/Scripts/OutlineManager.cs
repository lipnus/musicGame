using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class OutlineManager : MonoBehaviour
{
	List<Outline> outlines = new List<Outline>();
	List<Color> colorused = new List<Color> ();

    private Camera MCamera;
	private Camera OCamera;

    public float Thickness = 4f;
    private float Intensity = 1f;

	//initialize all the color here
	private Color lineColor0 = Color.red;
	private Color lineColor1 = Color.blue;
	private Color lineColor2 = Color.green;
	private bool darkOutlines = false;
	private float alphaCutoff = .5f;
	private float vWidth = 0f;
	private float vHeight = 0f;

	//materials list
   	Material outline0Material;
    Material outline1Material;
    Material outline2Material;
    Material outlineEraseMaterial;

	//shader list
    Shader outlineShader;
    Shader outlineBufferShader;
    Material outlineShaderMaterial;
    RenderTexture renderTexture;

	//get the right material with the associated number
    Material GetMaterialByNbr(int nbr)
    {
		if (nbr == 0)
            return outline0Material;
		else if (nbr == 1)
            return outline1Material;
		else
            return outline2Material;
    }

	//create the material
    Material CreateMaterial(Color emissionColor)
    {
        Material m = new Material(outlineBufferShader);
        m.SetColor("_Color", emissionColor);
        m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        m.SetInt("_ZWrite", 0);
        m.DisableKeyword("_ALPHATEST_ON");
        m.EnableKeyword("_ALPHABLEND_ON");
        m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        m.renderQueue = 3000;
        return m;
    }

    void Start()
    {
		//get the original screen orientation when game start, if it change, we will reload the second camera to fit the original
		vWidth = Screen.width;
		vHeight = Screen.height;

        CreateMaterialsIfNeeded();
        UpdateMaterialsPublicProperties();

		//get the Main Camera
		if (MCamera == null)
        {
			MCamera = GetComponent<Camera>();

			if (MCamera == null)
				MCamera = Camera.main;
        }

		//create a hidden cam
		if (OCamera == null)
        {
            GameObject HiddenCam = new GameObject("Outline Camera");
			HiddenCam.transform.parent = MCamera.transform;
			OCamera = HiddenCam.AddComponent<Camera>();
			HiddenCam.hideFlags = HideFlags.HideInHierarchy;
        }

        UpdateOutlineCameraFromSource();

		renderTexture = new RenderTexture(MCamera.pixelWidth, MCamera.pixelHeight, 16, RenderTextureFormat.Default);
		OCamera.targetTexture = renderTexture;

		//activate all Outline scripts in game
		foreach (Outline vCurOutline in (Outline[])FindObjectsOfType(typeof(Outline))) {
			vCurOutline.Initialise ();
		}
    }

    void OnDestroy()
    {
		if (renderTexture != null)
        	renderTexture.Release();
        DestroyMaterials();
    }

	void Update ()
	{
		//check if Screen has been resized (Potrait, Landscrap)
		if (vWidth != Screen.width || vHeight != Screen.height) {
			vWidth = Screen.width;
			vHeight = Screen.height;

			UpdateOutlineCameraFromSource ();
			renderTexture = new RenderTexture(MCamera.pixelWidth, MCamera.pixelHeight, 16, RenderTextureFormat.Default);
			OCamera.targetTexture = renderTexture;
		}
	}

    void OnPreCull()
    {
        if (outlines != null)
        {
            for (int i = 0; i < outlines.Count; i++)
            {
                if (outlines[i] != null)
                {
					outlines[i].originalMaterial = outlines[i].GetComponent<Renderer>().sharedMaterial;
					outlines[i].originalLayer = outlines[i].gameObject.layer;

					if (outlines [i].eraseRenderer)
						outlines [i].GetComponent<Renderer> ().sharedMaterial = outlineEraseMaterial;
					else
						outlines [i].GetComponent<Renderer> ().sharedMaterial = GetMaterialByNbr(outlines[i].color);

					//change the name layer
                    outlines[i].gameObject.layer = LayerMask.NameToLayer("Outline");
                }
            }
        }

		OCamera.Render();

        if (outlines != null)
        {
            for (int i = 0; i < outlines.Count; i++)
            {
                if (outlines[i] != null)
                {
					outlines[i].GetComponent<Renderer>().sharedMaterial = outlines[i].originalMaterial;
					outlines[i].gameObject.layer = outlines[i].originalLayer;
                }
            }
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        outlineShaderMaterial.SetTexture("_OutlineSource", renderTexture);
        Graphics.Blit(source, destination, outlineShaderMaterial);
    }

    private void CreateMaterialsIfNeeded()
    {
        if (outlineShader == null)
            outlineShader = Resources.Load<Shader>("Mats/OutlineShader");
        if (outlineBufferShader == null)
			outlineBufferShader = Resources.Load<Shader>("Mats/OutlineBufferShader");
        if (outlineShaderMaterial == null)
        {
            outlineShaderMaterial = new Material(outlineShader);
            outlineShaderMaterial.hideFlags = HideFlags.HideAndDontSave;
            UpdateMaterialsPublicProperties();
        }

		//create all the materials by default
        if (outlineEraseMaterial == null)
            outlineEraseMaterial = CreateMaterial(new Color(0, 0, 0, 0));
        if (outline0Material == null)
			outline0Material = CreateMaterial(lineColor0);
        if (outline1Material == null)
			outline1Material = CreateMaterial(lineColor1);
        if (outline2Material == null)
			outline2Material = CreateMaterial(lineColor2);

		outline0Material.SetFloat("_AlphaCutoff", alphaCutoff);
		outline1Material.SetFloat("_AlphaCutoff", alphaCutoff);
		outline2Material.SetFloat("_AlphaCutoff", alphaCutoff);
    }

    private void DestroyMaterials()
    {
        DestroyImmediate(outlineShaderMaterial);
        DestroyImmediate(outlineEraseMaterial);
        DestroyImmediate(outline1Material);
        DestroyImmediate(outline2Material);
        outlineShader = null;
        outlineBufferShader = null;
        outlineShaderMaterial = null;
        outlineEraseMaterial = null;
		outline0Material = null;
		outline1Material = null;
		outline2Material = null;
    }

    private void UpdateMaterialsPublicProperties()
    {
        if (outlineShaderMaterial)
        {				
			outlineShaderMaterial.SetFloat("_LineThicknessX", Thickness / 1000);
			outlineShaderMaterial.SetFloat("_LineThicknessY", Thickness / 1000);
			outlineShaderMaterial.SetFloat("_LineIntensity", Intensity);
            outlineShaderMaterial.SetColor("_LineColor1", lineColor0);
            outlineShaderMaterial.SetColor("_LineColor2", lineColor1);
            outlineShaderMaterial.SetColor("_LineColor3", lineColor2);

            if (darkOutlines)
                outlineShaderMaterial.SetInt("_Dark", 1);
            else
                outlineShaderMaterial.SetInt("_Dark", 0);
        }
    }

    // Call this when source camera has been changed.
    public void UpdateFromSource()
    {
		renderTexture.width = MCamera.pixelWidth;
		renderTexture.height = MCamera.pixelHeight;
        UpdateOutlineCameraFromSource();
    }

    void UpdateOutlineCameraFromSource()
    {
		OCamera.CopyFrom(MCamera);
		OCamera.renderingPath = RenderingPath.Forward;
		OCamera.enabled = false;
		OCamera.backgroundColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		OCamera.clearFlags = CameraClearFlags.SolidColor;
		OCamera.cullingMask = LayerMask.GetMask("Outline");
		OCamera.rect = new Rect(0, 0, 1, 1);
    }

    public void AddOutline(Outline outline)
    {
        if (!outlines.Contains(outline))
        {
			outlines.Add(outline);
        }
    }
	public void RemoveOutline(Outline outline)
	{
		outlines.Remove(outline);
    }

}
