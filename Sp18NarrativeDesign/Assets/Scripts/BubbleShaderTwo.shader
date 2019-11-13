Shader "Unlit/BubbleShaderTwo"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
	_ScrollX("scroll x", Float) = 1
	_ScrollY("scroll y", Float) = 1
	_Color ("Color", Color) = (1,1,1,1)
			  _Transparency("Transparency", Range(0.0,1)) = 0.25
	}
		SubShader
	{
		Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
		LOD 100
				ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _ScrollX;
			float _ScrollY;
			fixed4 _Color;
			float _Transparency;
            v2f vert (appdata v)
            {
	/*			o.screenuv = */
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				fixed2 scrolledUV = v.uv;
				fixed scrolledX = _Time * _ScrollX;
				fixed scrolledY = _Time * _ScrollY;
				scrolledUV += fixed2(scrolledX, scrolledY);
                o.uv = TRANSFORM_TEX(scrolledUV, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Color ;
			col.a = _Transparency;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
