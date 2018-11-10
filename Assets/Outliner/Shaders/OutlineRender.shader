Shader "3y3net/OutlineRender" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

		CGINCLUDE
		uniform sampler2D _MainTex;
	uniform float4 _MainTex_TexelSize;
	uniform int _Thickness;
	uniform float _Blending;
	uniform sampler2D _OutlineTextureMask;

	struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};

	fixed IsMask(float2 coord) {
		return tex2D(_OutlineTextureMask, coord).a;
	}
	ENDCG

		SubShader{
		Pass{
		Name "BASE"
		Tags{ "LightMode" = "Always" }

		ZTest Always Cull Off ZWrite Off Fog{ Mode off }

		CGPROGRAM
#pragma target 2.5
#include "UnityCG.cginc"
#pragma vertex vert
#pragma fragment frag

		v2f vert(appdata_img v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord.xy;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target{

		v2f maskcoord = i;
	// flip the y if a render texture is being used
	if (_MainTex_TexelSize.y < 0)
	{
		maskcoord.uv.y = 1 - maskcoord.uv.y;
	}

	fixed4 main = tex2D(_MainTex, i.uv);


	//Debug... see mask
	//return tex2D(_OutlineTextureMask, i.uv);

	//Debug... see mask alpha
	//fixed4 mask = tex2D(_OutlineTextureMask, i.uv);
	//return fixed4(mask.a, mask.a, mask.a, 1.0);

	fixed4 mask = main;// tex2D(_OutlineTextureMask, i.uv);

	float dist = 5000.0;

	float2 xx = float2(_MainTex_TexelSize.x, 0);
	float2 yy = float2(0, _MainTex_TexelSize.y);

	int byass = _Thickness + 1.0;

	float maxd = 2 * byass*byass;
	float alpha = 1.0;

	//[unroll(120)]

	for (uint p = 0; p < 144; p++) {
		int k = (p / 12) - 6;
		int j = (p % 12) - 6;
		float val = IsMask(maskcoord.uv + (xx * k) + (yy*j));

		if (IsMask(maskcoord.uv + (xx * k) + (yy*j)) == 1.0) {
			float dd = (k*k) + (j*j);
			if (dd < dist) {
				alpha = max(0, ((maxd - dd) / maxd));
				alpha = alpha*alpha*_Blending;
				mask = tex2D(_OutlineTextureMask, maskcoord.uv + (xx * k) + (yy*j));
				mask = main*(1 - (alpha*0.7)) + mask * alpha;
			}
			dist = min(dd, dist);
		}
	}


	if (IsMask(maskcoord.uv) == 1.0) {
		return main;
	}

	return mask;
	return fixed4(main + mask);

	}
		ENDCG
	}
	}

		Fallback off
}
