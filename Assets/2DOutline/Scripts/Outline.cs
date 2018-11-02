using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class Outline : MonoBehaviour
{
	//Always 		= will always have a outline around the object
	//MouseOver 	= will create a rigidbody + collider to make it work automatically
	//Click 		= will show it when clicking on it and remove it when clicking again. Also, create rigidbody + collider
	//OFF  			= you will have to enable/disable this component to show it.
	public enum OutlineType {Always, MouseOver, Click, OFF};

	//variables
	private OutlineManager vOutlineManager;
	private bool CanShow = false;
	public Outline MasterOutline;							//always keep who's the top Outline in all parts to ONLY call him once
	public int color;
	public OutlineType vOutlineType = OutlineType.OFF;		//by default, it will be OFF 
	public bool eraseRenderer;
	public bool ShowOutline = true;
	public bool OutlineAllChild = true;
	private bool IsChild = false;

	[HideInInspector]
	public int originalLayer;
	[HideInInspector]
	public Material originalMaterial;

	void Start()
    {

    }

	void StartOutline()
	{
		switch (vOutlineType) {
		case OutlineType.Always: 
			ShowHide_Outline (true);
			break;

			//create rigibody + use MouseEnter, MouseExit and MouseDown properly
		case OutlineType.Click: 
		case OutlineType.MouseOver: 

			//check if we have a RigidBody2D.
			Rigidbody2D vRigidBody2d = gameObject.GetComponent<Rigidbody2D> ();

			//if not, we create one
			if (vRigidBody2d == null)
				vRigidBody2d = gameObject.AddComponent<Rigidbody2D> ();

			//make him to not fall 
			vRigidBody2d.isKinematic = true;

			//check if we have a BoxCollider
			BoxCollider2D vBoxCollider2D = gameObject.GetComponent<BoxCollider2D> ();

			//if not, we create one
			if (vBoxCollider2D == null)
				vBoxCollider2D = gameObject.AddComponent<BoxCollider2D> ();

			//make him trigger 
			vBoxCollider2D.isTrigger = true;

			break;

			//disable itself on start
		case OutlineType.OFF:
			ShowHide_Outline (ShowOutline);
			break;
		}
	}

	public void Initialise()
	{
		//create the list which contain all the spriterenderer
		List<SpriteRenderer> sprites = new List<SpriteRenderer> ();

		//try to get the outlinemanager
		if (vOutlineManager == null) {
			vOutlineManager = Camera.main.GetComponent<OutlineManager> ();

			//if not create the outline manager on the main camera
			//by default, it will have red, blue and green color
			if (vOutlineManager == null)
				vOutlineManager = Camera.main.gameObject.AddComponent<OutlineManager> ();
		}

		if (vOutlineManager == null)
			Debug.Log ("Add a Camera to the game");

		//is his master
		SetMasterOutline (this);

		//get all the sprite renderer below
		foreach (SpriteRenderer vCurRenderer in GetComponentsInChildren<SpriteRenderer> ().OfType<SpriteRenderer> ().ToList ())
			sprites.Add (vCurRenderer);

		if (OutlineAllChild) {
			foreach (SpriteRenderer vRenderer in sprites) {
				//try to get this component
				Outline vOutline = vRenderer.GetComponent<Outline> (); 

				//if doesn't exist, create it
				if (vOutline == null) {
					vOutline = vRenderer.gameObject.AddComponent<Outline> ();
					vOutline.vOutlineManager = vOutlineManager;
					vOutline.color = color;
					vOutline.vOutlineType = vOutlineType;

					//save master for this once
					vOutline.SetMasterOutline (this);
				}

				//start them all
				vOutline.StartOutline ();
			}
		}
		else
			//start the outline
			StartOutline ();
	}

	//save the master outline here
	public void SetMasterOutline(Outline vMasterOutline)
	{
		MasterOutline = vMasterOutline;
	}

	public void ShowHide_Outline(bool vChoice)
	{
		//get all outlines child
		if (MasterOutline != null) {
			Outline[] Outlines = MasterOutline.GetComponentsInChildren<Outline> ();

			//if we want to ONlY have this sprite and not all the sub component to have a outline, we just get the main sprite.
			if (!OutlineAllChild)
				Outlines = new Outline[] {GetComponent<Outline>()};

			//add/remove them all
			foreach (Outline vCurOutline in Outlines) {
				if (vChoice)
					vOutlineManager.AddOutline (vCurOutline);
				else
					vOutlineManager.RemoveOutline (vCurOutline);
			}
		}

		//save the preference
		CanShow = vChoice;
	}

    void OnDisable()
    {
		vOutlineManager.RemoveOutline(this);
    }

	//ONLY work on mouseover type
	void OnMouseOver()
	{
		if (vOutlineType == OutlineType.MouseOver)
			ShowHide_Outline (true);
		else if (vOutlineType == OutlineType.Click && Input.GetMouseButtonDown(0))
		{
			//show/hide this outline
			ShowHide_Outline (!CanShow);
		}
	}

	void OnMouseExit()
	{
		if (vOutlineType == OutlineType.MouseOver)
			ShowHide_Outline (false);
	}
}
