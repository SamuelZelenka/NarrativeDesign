// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Portal"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_BorderInner("BorderInner", Range( 0 , 20)) = 0.7829127
		_BorderOuter("BorderOuter", Range( 0 , 1)) = 0.130512
		_BorderNoiseScroll2("Border Noise Scroll 2", Vector) = (0,-0.6,0,0)
		[HDR]_BorderEmission("BorderEmission", Color) = (1.267016,2,0,0)
		_BorderColor("BorderColor", Color) = (1,0.9943777,0,0)
		_BorderNoiseScroll1("Border Noise Scroll 1", Vector) = (0,1,0,0)
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_DistortionMap("Distortion Map", 2D) = "white" {}
		_DistortionScale("Distortion Scale", Range( 0 , 1)) = 0
		_DistortionScrolling("Distortion Scrolling", Vector) = (0.12,0,0,0)
		_Noise2Scale("Noise 2 Scale", Float) = 1
		_Noise1Scale("Noise 1 Scale", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform float2 _BorderNoiseScroll1;
		uniform float _Noise1Scale;
		uniform float2 _BorderNoiseScroll2;
		uniform float _Noise2Scale;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _BorderOuter;
		uniform float _BorderInner;
		uniform float4 _BorderColor;
		uniform sampler2D _TextureSample1;
		uniform sampler2D _DistortionMap;
		uniform float2 _DistortionScrolling;
		uniform float _DistortionScale;
		uniform float4 _BorderEmission;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TexCoord6 = i.uv_texcoord * float2( 5,5 ) + ( _Time.y * _BorderNoiseScroll1 );
			float simplePerlin2D4 = snoise( uv_TexCoord6*_Noise1Scale );
			simplePerlin2D4 = simplePerlin2D4*0.5 + 0.5;
			float2 uv_TexCoord42 = i.uv_texcoord * float2( 5,5 ) + ( _Time.y * _BorderNoiseScroll2 );
			float simplePerlin2D43 = snoise( uv_TexCoord42*_Noise2Scale );
			simplePerlin2D43 = simplePerlin2D43*0.5 + 0.5;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode1 = tex2D( _TextureSample0, uv_TextureSample0 );
			float temp_output_58_0 = ( step( ( pow( simplePerlin2D4 , 5.49 ) * simplePerlin2D43 ) , 0.02909878 ) * pow( ( ( 1.0 - tex2DNode1.r ) - ( 1.0 - step( _BorderOuter , tex2DNode1.r ) ) ) , _BorderInner ) );
			float2 uv_TexCoord116 = i.uv_texcoord + ( _Time.y * _DistortionScrolling );
			float4 temp_output_104_0 = ( tex2D( _DistortionMap, uv_TexCoord116 ) * _DistortionScale );
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			o.Albedo = ( ( temp_output_58_0 * _BorderColor ) + float4( 0,0,0,0 ) + tex2D( _TextureSample1, ( temp_output_104_0 + ase_grabScreenPosNorm ).rg ) ).rgb;
			o.Emission = ( _BorderEmission * temp_output_58_0 ).rgb;
			float4 clampResult135 = clamp( ( ( 1.0 - temp_output_104_0 ) * ( ( 1.0 - step( tex2DNode1.r , 0.04 ) ) - ( ( 1.0 - step( ( pow( simplePerlin2D4 , 5.49 ) * simplePerlin2D43 ) , 0.02909878 ) ) * pow( ( ( 1.0 - tex2DNode1.r ) - ( 1.0 - step( _BorderOuter , tex2DNode1.r ) ) ) , _BorderInner ) ) ) ) , float4( 0,0,0,0 ) , float4( 1,0,0,0 ) );
			o.Alpha = clampResult135.r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17400
937;105;530;654;2568.056;518.709;1.3;False;False
Node;AmplifyShaderEditor.SimpleTimeNode;9;-4908.636,-719.0526;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;10;-4965.855,-633.4343;Inherit;False;Property;_BorderNoiseScroll1;Border Noise Scroll 1;6;0;Create;True;0;0;False;0;0,1;0,0.15;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;40;-4983.988,-157.45;Inherit;False;Property;_BorderNoiseScroll2;Border Noise Scroll 2;3;0;Create;True;0;0;False;0;0,-0.6;0,-2.37;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;39;-4961.812,-292.5287;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-4726.296,-699.3402;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;121;-4475.247,-541.9023;Inherit;False;Property;_Noise1Scale;Noise 1 Scale;12;0;Create;True;0;0;False;0;1;-4.23;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-4536.567,-743.6929;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;5,5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-5651.031,198.3371;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;afe8003060456404abf3a64b6e270db8;afe8003060456404abf3a64b6e270db8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;127;-5332.704,642.3384;Inherit;False;1344.443;635.1416;Border;8;54;63;49;122;124;123;126;64;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-4709.911,-256.2544;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;120;-4403.277,-181.4083;Inherit;False;Property;_Noise2Scale;Noise 2 Scale;11;0;Create;True;0;0;False;0;1;-0.65;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;4;-4237.477,-776.0449;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1.87;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;63;-5095.183,861.6537;Inherit;False;Property;_BorderOuter;BorderOuter;2;0;Create;True;0;0;False;0;0.130512;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;42;-4533.192,-368.607;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;5,5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RelayNode;54;-5282.704,960.9785;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;38;-3985.217,-607.7517;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;5.49;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;49;-4859.672,979.0573;Inherit;True;2;0;FLOAT;0.3;False;1;FLOAT;0.2;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;43;-4221.091,-328.3591;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;2.74;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;114;-4666.031,-1191.338;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;115;-4688.031,-1083.338;Inherit;False;Property;_DistortionScrolling;Distortion Scrolling;10;0;Create;True;0;0;False;0;0.12,0;0.12,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.OneMinusNode;122;-5037.551,760.7969;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;124;-4638.24,892.7708;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;99;-3829.178,-243.1545;Inherit;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;0.02909878;0;0;0.4;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-3707.404,-510.7894;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;112;-4408.031,-1161.338;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;123;-4458.498,692.3384;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;98;-3495.396,-465.6762;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;64;-4645.275,1061.304;Inherit;False;Property;_BorderInner;BorderInner;1;0;Create;True;0;0;False;0;0.7829127;10.7;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;126;-4249.26,1024.48;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;4.08;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;116;-4225.074,-1382.753;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RelayNode;96;-3511.442,43.80259;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;100;-3949.672,-1258.554;Inherit;True;Property;_DistortionMap;Distortion Map;8;0;Create;True;0;0;False;0;-1;77fdad851e93f394c9f8a1b1a63b56f3;61c0b9c0523734e0e91bc6043c72a490;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;105;-3983.376,-985.9013;Inherit;False;Property;_DistortionScale;Distortion Scale;9;0;Create;True;0;0;False;0;0;0.029;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;59;-3250.706,-135.7028;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.04;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;94;-3297.239,86.4528;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RelayNode;125;-3960.336,1006.898;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;61;-2906.348,-136.3644;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;101;-3740.927,-857.1907;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;128;-3558.178,832.997;Inherit;False;759.865;768.1993;Border Color and emission;5;58;78;67;77;66;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;104;-3581.767,-1032.759;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;-3079.167,111.9625;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;119;-3002.708,-494.6741;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;93;-2702.917,-93.66156;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;78;-3383.881,1351.621;Inherit;False;Property;_BorderColor;BorderColor;5;0;Create;True;0;0;False;0;1,0.9943777,0,0;0.2363467,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-3508.178,1049.497;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;102;-3416.101,-869.8842;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;-3033.313,1348.196;Inherit;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;85;-3128.287,-998.6401;Inherit;True;Property;_TextureSample1;Texture Sample 1;7;0;Create;True;0;0;False;0;-1;4a271b87fecf13b4781d76d7527b5461;ed81631baec6810488a6e34114e81554;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;67;-3300.696,882.997;Inherit;False;Property;_BorderEmission;BorderEmission;4;1;[HDR];Create;True;0;0;False;0;1.267016,2,0,0;0,2.611879,2.670157,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;118;-2423.259,-157.4805;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;88;-2670.19,-452.5335;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;135;-2188.819,-144.3416;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;133;-2471.665,63.00294;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.001;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;33;-3983.524,-855.4437;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.69;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-3066.166,1064.303;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-2019.164,-353.8164;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Portal;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;0;9;0
WireConnection;8;1;10;0
WireConnection;6;1;8;0
WireConnection;41;0;39;0
WireConnection;41;1;40;0
WireConnection;4;0;6;0
WireConnection;4;1;121;0
WireConnection;42;1;41;0
WireConnection;54;0;1;1
WireConnection;38;0;4;0
WireConnection;49;0;63;0
WireConnection;49;1;54;0
WireConnection;43;0;42;0
WireConnection;43;1;120;0
WireConnection;122;0;54;0
WireConnection;124;0;49;0
WireConnection;44;0;38;0
WireConnection;44;1;43;0
WireConnection;112;0;114;0
WireConnection;112;1;115;0
WireConnection;123;0;122;0
WireConnection;123;1;124;0
WireConnection;98;0;44;0
WireConnection;98;1;99;0
WireConnection;126;0;123;0
WireConnection;126;1;64;0
WireConnection;116;1;112;0
WireConnection;96;0;98;0
WireConnection;100;1;116;0
WireConnection;59;0;1;1
WireConnection;94;0;96;0
WireConnection;125;0;126;0
WireConnection;61;0;59;0
WireConnection;104;0;100;0
WireConnection;104;1;105;0
WireConnection;95;0;94;0
WireConnection;95;1;125;0
WireConnection;119;0;104;0
WireConnection;93;0;61;0
WireConnection;93;1;95;0
WireConnection;58;0;96;0
WireConnection;58;1;125;0
WireConnection;102;0;104;0
WireConnection;102;1;101;0
WireConnection;77;0;58;0
WireConnection;77;1;78;0
WireConnection;85;1;102;0
WireConnection;118;0;119;0
WireConnection;118;1;93;0
WireConnection;88;0;77;0
WireConnection;88;2;85;0
WireConnection;135;0;118;0
WireConnection;133;0;1;1
WireConnection;33;0;4;0
WireConnection;66;0;67;0
WireConnection;66;1;58;0
WireConnection;0;0;88;0
WireConnection;0;2;66;0
WireConnection;0;9;135;0
ASEEND*/
//CHKSM=752BC331B631CA7DAF793D065D96AABB7580CE02