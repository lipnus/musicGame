Shader "3y3net/OutlineMask" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Color("Base (RGB) Trans (A)", Color) = (1, 1, 1, 1)
		_Cutoff("Alpha Cutoff", Float) = 0.5
		_SpriteMask("Mask (RGB)", 2D) = "black" {}
		_Threshold("Threshold", Float) = 1.0
	}

	CGINCLUDE
	uniform sampler2D _MainTex;
	uniform float4 _MainTex_ST;
	uniform sampler2D _SpriteMask;
	uniform float _Threshold;

	struct v2f_uv {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f_col {
		float4 pos : SV_POSITION;
		fixed4 color : COLOR;
		float2 uv : TEXCOORD0;
	};

	fixed4 LookForTexture(float2 coord) {
		return tex2D(_SpriteMask, coord);
	}
	ENDCG

		Category{
		LOD 100
		Lighting Off
		Fog{ Mode Off }

		SubShader{
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" "IgnoreProjector" = "True" }
		Pass{
			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag

			v2f_uv vert(appdata_base v) {
				v2f_uv o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag(v2f_uv i) : SV_Target {
				return LookForTexture(i.uv);
			}
			ENDCG
			}
		}



		SubShader{
		Tags{ "RenderType" = "TransparentCutout" "Queue" = "AlphaTest" "IgnoreProjector" = "True" }
		Pass{
		CGPROGRAM
#include "UnityCG.cginc"
#pragma vertex vert
#pragma fragment frag

		uniform fixed4 _Color;
	uniform fixed _Cutoff;

	v2f_uv vert(appdata_base v) {
		v2f_uv o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		return o;
	}

	fixed4 frag(v2f_uv i) : SV_Target{
		fixed4 col = tex2D(_MainTex, i.uv);
		clip(col.a * _Color.a - _Cutoff);
		return LookForTexture(i.uv);
	}
		ENDCG
	}
	}

		SubShader{
		Tags{ "RenderType" = "TreeBark" "Queue" = "Geometry" "IgnoreProjector" = "True" }
		Pass{
		CGPROGRAM
#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "UnityBuiltin3xTreeLibrary.cginc"
#pragma vertex vert
#pragma fragment frag

		v2f_uv vert(appdata_full v) {
		v2f_uv o;
		TreeVertBark(v);
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		return o;
	}

	fixed4 frag(v2f_uv i) : SV_Target{
		return LookForTexture(i.uv);
	}
		ENDCG
	}
	}

		SubShader{
		Tags{ "RenderType" = "TreeLeaf" "Queue" = "AlphaTest" "IgnoreProjector" = "True" }
		Pass{
		CGPROGRAM
#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "UnityBuiltin3xTreeLibrary.cginc"
#pragma vertex vert
#pragma fragment frag

		uniform fixed _Cutoff;

	v2f_uv vert(appdata_full v) {
		v2f_uv o;
		TreeVertBark(v);
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	fixed4 frag(v2f_uv i) : SV_Target{
		fixed4 col = tex2D(_MainTex, i.uv);
	clip(col.a - _Cutoff);
	return LookForTexture(i.uv);
	}
		ENDCG
	}
	}

		SubShader{
		Tags{ "RenderType" = "TreeOpaque" "Queue" = "Geometry" "IgnoreProjector" = "True" "DisableBatching" = "True" }
		Pass{
		CGPROGRAM
#include "UnityCG.cginc"
#include "TerrainEngine.cginc"
#pragma vertex vert
#pragma fragment frag

		struct a2f {
		float4 vertex : POSITION;
		float4 texcoord : TEXCOORD0;
		fixed4 color : COLOR;
	};

	v2f_uv vert(a2f v) {
		v2f_uv o;
		TerrainAnimateTree(v.vertex, v.color.w);
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		return o;
	}

	fixed4 frag(v2f_uv i) : SV_Target{
		return LookForTexture(i.uv);
	}
		ENDCG
	}
	}

		SubShader{
		Tags{ "RenderType" = "TreeTransparentCutout" "Queue" = "AlphaTest" "IgnoreProjector" = "True" "DisableBatching" = "True" }
		Pass{
		Cull Back

		CGPROGRAM
#include "UnityCG.cginc"
#include "TerrainEngine.cginc"
#pragma vertex vert
#pragma fragment frag

		uniform fixed _Cutoff;

	struct a2f {
		float4 vertex : POSITION;
		fixed4 color : COLOR;
		float4 texcoord : TEXCOORD0;
	};

	v2f_uv vert(a2f v) {
		v2f_uv o;
		TerrainAnimateTree(v.vertex, v.color.w);
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	fixed4 frag(v2f_uv i) : SV_Target{
		fixed4 col = tex2D(_MainTex, i.uv);
	clip(col.a - _Cutoff);
	return LookForTexture(i.uv);
	}
		ENDCG
	}
		Pass{
		Cull Front

		CGPROGRAM
#include "UnityCG.cginc"
#include "TerrainEngine.cginc"
#pragma vertex vert
#pragma fragment frag

		uniform fixed _Cutoff;

	struct a2f {
		float4 vertex : POSITION;
		fixed4 color : COLOR;
		float4 texcoord : TEXCOORD0;
	};

	v2f_uv vert(a2f v) {
		v2f_uv o;
		TerrainAnimateTree(v.vertex, v.color.w);
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	fixed4 frag(v2f_uv i) : SV_Target{
		fixed4 col = tex2D(_MainTex, i.uv);
	clip(col.a - _Cutoff);
	return LookForTexture(i.uv);
	}
		ENDCG
	}
	}

		SubShader{
		Tags{ "RenderType" = "TreeBillboard" "Queue" = "AlphaTest" "IgnoreProjector" = "True" }
		Pass{
		Cull Off

		CGPROGRAM
#include "UnityCG.cginc"
#include "TerrainEngine.cginc"
#pragma vertex vert
#pragma fragment frag

		v2f_uv vert(appdata_tree_billboard v) {
		v2f_uv o;
		TerrainBillboardTree(v.vertex, v.texcoord1.xy, v.texcoord.y);
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv.x = v.texcoord.x;
		o.uv.y = v.texcoord.y > 0;
		return o;
	}

	fixed4 frag(v2f_uv i) : SV_Target{
		fixed4 col = tex2D(_MainTex, i.uv);
	clip(col.a - 0.001);
	return LookForTexture(i.uv);
	}
		ENDCG
	}
	}

		SubShader{
		Tags{ "RenderType" = "GrassBillboard" "Queue" = "AlphaTest" "IgnoreProjector" = "True" }
		Pass{
		Cull Off

		CGPROGRAM
#include "UnityCG.cginc"
#include "TerrainEngine.cginc"
#pragma vertex vert
#pragma fragment frag

		uniform fixed _Cutoff;

	v2f_col vert(appdata_full v) {
		v2f_col o;
		WavingGrassBillboardVert(v);
		o.color = v.color;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	fixed4 frag(v2f_col i) : SV_Target{
		fixed4 col = tex2D(_MainTex, i.uv);
	clip(col.a * i.color.a - _Cutoff);
	return LookForTexture(i.uv);
	}
		ENDCG
	}
	}

		SubShader{
		Tags{ "RenderType" = "Grass" "Queue" = "AlphaTest" "IgnoreProjector" = "True" }
		Pass{
		CGPROGRAM
#include "UnityCG.cginc"
#include "TerrainEngine.cginc"
#pragma vertex vert
#pragma fragment frag

		uniform fixed _Cutoff;

	v2f_col vert(appdata_full v) {
		v2f_col o;
		WavingGrassVert(v);
		o.color = v.color;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	fixed4 frag(v2f_col i) : SV_Target{
		fixed4 col = tex2D(_MainTex, i.uv);
	clip(col.a * i.color.a - _Cutoff);
	return LookForTexture(i.uv);
	}
		ENDCG
	}
	}

		SubShader{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" "IgnoreProjector" = "True" }
		Pass{

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		//ColorMask RGB

		CGPROGRAM
#include "UnityCG.cginc"
#pragma vertex vert
#pragma fragment frag

		uniform fixed4 _Color;

	v2f_uv vert(appdata_base v) {
		v2f_uv o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		return o;
	}

	fixed4 frag(v2f_uv i) : SV_Target{
		fixed4 col = tex2D(_MainTex, i.uv);
		float alpha = col.a * _Color.a;
		alpha += (1.0 - _Color.a);
		fixed4 glo = LookForTexture(i.uv);
		//Thresholding... byas a bit or artifacts happens... 0.05 os enough
		if((alpha+0.05)>=_Threshold)
			return glo;
		return fixed4(0.0, 0.0, 0.0, 0.0);
	}
		ENDCG
	}
	}

	}

	Fallback Off
}
