// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "CRT"
{
	Properties
	{
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_Scanline_Scale("Scanline_Scale", Float) = 1
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Emision_Value("Emision_Value", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _TextureSample2;
		uniform float _Scanline_Scale;
		uniform float _Emision_Value;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode2 = tex2D( _TextureSample0, uv_TextureSample0 );
			o.Albedo = tex2DNode2.rgb;
			float2 panner18 = ( ( i.uv_texcoord * _Scanline_Scale ) + 1 * _Time.y * float2( -0.6,0 ));
			float4 tex2DNode9 = tex2D( _TextureSample2, panner18 );
			o.Emission = ( tex2DNode2 + tex2DNode9 + ( tex2DNode2 * _Emision_Value ) ).rgb;
			o.Metallic = 1.0;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15101
2070;25;1441;1052;1998.033;237.7234;1.335346;True;False
Node;AmplifyShaderEditor.CommentaryNode;14;-1397.5,626;Float;False;652;313;Scanline Scale/Speed Control;4;18;17;16;15;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;16;-1355.5,668;Float;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;15;-1354.32,833.1765;Float;False;Property;_Scanline_Scale;Scanline_Scale;2;0;Create;True;0;0;False;0;1;3.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1130.5,742;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-878.4063,-229.308;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;False;0;95c5928b730c89446ae59890926d60db;95c5928b730c89446ae59890926d60db;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-434.3416,-280.3217;Float;False;Property;_Emision_Value;Emision_Value;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;18;-931.5,735;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.6,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;9;-596.618,395.0635;Float;True;Property;_TextureSample2;Texture Sample 2;1;0;Create;True;0;0;False;0;0031a3ec394053147b98b5d330583ec4;0031a3ec394053147b98b5d330583ec4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-216.6811,-172.2733;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;13;-1264.5,147;Float;False;517;340;Roughness Scale Control;3;10;12;11;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;1;-294.5,-29;Float;False;Constant;_Float0;Float 0;0;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-142.4202,247.6104;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;5;-208.7955,83.93016;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-902.3124,352.5135;Float;False;Property;_Roughness_power;Roughness_power;6;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1192.5,372;Float;False;Property;_Roughness_Scale;Roughness_Scale;5;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-413.3337,91.65257;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexCoordVertexDataNode;10;-1214.5,203;Float;False;0;2;0;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-980.5,253;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-641.3438,264.3316;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-561.3338,142.6526;Float;False;Constant;_5;5;0;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;-927.8019,163.275;Float;True;Property;_TextureSample1;Texture Sample 1;0;0;Create;True;0;0;False;0;b01259494da767c4aad609333e993562;b01259494da767c4aad609333e993562;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;CRT;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;0;16;0
WireConnection;17;1;15;0
WireConnection;18;0;17;0
WireConnection;9;1;18;0
WireConnection;22;0;2;0
WireConnection;22;1;20;0
WireConnection;19;0;2;0
WireConnection;19;1;9;0
WireConnection;19;2;22;0
WireConnection;5;0;4;0
WireConnection;5;1;9;0
WireConnection;4;0;2;0
WireConnection;4;1;3;0
WireConnection;11;0;10;0
WireConnection;11;1;12;0
WireConnection;8;0;7;0
WireConnection;8;1;6;0
WireConnection;7;1;11;0
WireConnection;0;0;2;0
WireConnection;0;2;19;0
WireConnection;0;3;1;0
ASEEND*/
//CHKSM=71786D215AC32444BF9DE7A6FFC421455B965766