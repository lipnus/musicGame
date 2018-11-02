// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/OutlineEffect" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_LineColor ("Line Color", Color) = (1,1,1,.5)
		
	}
	SubShader 
	{
		Pass
		{
			Tags { "RenderType"="Opaque" }
			LOD 200
			ZTest Always
			ZWrite Off
			Cull Off
			
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _OutlineSource;

			struct v2f {
			   float4 position : SV_POSITION;
			   float2 uv : TEXCOORD0;
			};
			
			v2f vert(appdata_img v)
			{
			   	v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				
			   	return o;
			}

			float _LineThicknessX;
			float _LineThicknessY;
			float _LineIntensity;
			half4 _LineColor1;
			half4 _LineColor2;
			half4 _LineColor3;
			int _Dark;
			uniform float4 _MainTex_TexelSize;

			half4 frag (v2f input) : COLOR
			{	
				float2 uv = input.uv;

				half4 originalPixel = tex2D(_MainTex,input.uv);
				half4 outlineSource = tex2D(_OutlineSource, uv);
								
				float h = .95f;
				half4 outline = 0;
				bool hasOutline = false;

				half4 sample1 = tex2D(_OutlineSource, uv + float2(_LineThicknessX,0.0) * _MainTex_TexelSize.x * 1000.0f);
				half4 sample2 = tex2D(_OutlineSource, uv + float2(-_LineThicknessX,0.0) * _MainTex_TexelSize.x * 1000.0f);
				half4 sample3 = tex2D(_OutlineSource, uv + float2(.0,_LineThicknessY) * _MainTex_TexelSize.y * 1000.0f);
				half4 sample4 = tex2D(_OutlineSource, uv + float2(.0,-_LineThicknessY) * _MainTex_TexelSize.y * 1000.0f);
				
				if(outlineSource.a < h)
				{
					if (sample1.r > h || sample2.r > h || sample3.r > h || sample4.r > h)
					{
						outline = _LineColor1 * _LineIntensity;
						hasOutline = true;
					}
					else if (sample1.g > h || sample2.g > h || sample3.g > h || sample4.g > h)
					{
						outline = _LineColor2 * _LineIntensity;
						hasOutline = true;
					}
					else if (sample1.b > h || sample2.b > h || sample3.b > h || sample4.b > h)
					{
						outline = _LineColor3 * _LineIntensity;
						hasOutline = true;
					}
				}					
					
				//return outlineSource;		
				if (_Dark)
				{
					if (hasOutline)
						return originalPixel * (1 - _LineColor1.a) + outline;
					else
						return originalPixel;
				}
				else
					return originalPixel + outline;
			}
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}