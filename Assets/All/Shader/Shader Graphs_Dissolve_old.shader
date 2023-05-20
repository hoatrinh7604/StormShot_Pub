// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

Shader "Shader Graphs/Dissolve_old"
{
  Properties
  {
    [NoScaleOffset] Texture2D_4972a32b9fed4a2b8180e52f9e0028d3 ("Albedo", 2D) = "white" {}
    _BaseColor ("AlbedoColor", Color) = (0.3176471,0.3921569,1,0)
    Vector1_1d89e84864d54f5189c0732a74238096 ("Metallic", float) = 0
    Vector1_afb8382c98f844d580b89da5ae4dfa01 ("Smoothness", float) = 0
    _NoiseScale ("Noise Scale", float) = 30
    _DissolveThreshhold ("AlphaClipThreshold", float) = 0
    _Thickness ("Thickness", float) = 0.05
    _EdgeColor ("EdgeColor", Color) = (0.4339623,0,0,0)
    unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
    unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
    unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
  }
  SubShader
  {
    Tags
    { 
      "QUEUE" = "AlphaTest"
      "RenderPipeline" = "UniversalPipeline"
      "RenderType" = "Opaque"
      "UniversalMaterialType" = "Unlit"
    }
    Pass // ind: 1, name: Pass
    {
      Name "Pass"
      Tags
      { 
        "QUEUE" = "AlphaTest"
        "RenderPipeline" = "UniversalPipeline"
        "RenderType" = "Opaque"
        "UniversalMaterialType" = "Unlit"
      }
      // m_ProgramMask = 6
      Program "vp"
      {
        SubProgram "vulkan"
        {
          
      }
      Program "fp"
      {
      }
      Program "gp"
      {
      }
      Program "hp"
      {
      }
      Program "dp"
      {
      }
      Program "surface"
      {
      }
      Program "rtp"
      {
      }
      
    } // end phase
    Pass // ind: 2, name: ShadowCaster
    {
      Name "ShadowCaster"
      Tags
      { 
        "LIGHTMODE" = "SHADOWCASTER"
        "QUEUE" = "AlphaTest"
        "RenderPipeline" = "UniversalPipeline"
        "RenderType" = "Opaque"
        "UniversalMaterialType" = "Unlit"
      }
      ColorMask 0
      // m_ProgramMask = 6
      Program "vp"
      {
        SubProgram "vulkan"
        {
          
      }
      Program "fp"
      {
      }
      Program "gp"
      {
      }
      Program "hp"
      {
      }
      Program "dp"
      {
      }
      Program "surface"
      {
      }
      Program "rtp"
      {
      }
      
    } // end phase
    Pass // ind: 3, name: DepthOnly
    {
      Name "DepthOnly"
      Tags
      { 
        "LIGHTMODE" = "DepthOnly"
        "QUEUE" = "AlphaTest"
        "RenderPipeline" = "UniversalPipeline"
        "RenderType" = "Opaque"
        "UniversalMaterialType" = "Unlit"
      }
      ColorMask 0
      // m_ProgramMask = 6
      Program "vp"
      {
        SubProgram "vulkan"
        {
          
      }
      Program "fp"
      {
      }
      Program "gp"
      {
      }
      Program "hp"
      {
      }
      Program "dp"
      {
      }
      Program "surface"
      {
      }
      Program "rtp"
      {
      }
      
    } // end phase
    Pass // ind: 4, name: DepthNormals
    {
      Name "DepthNormals"
      Tags
      { 
        "LIGHTMODE" = "DepthNormals"
        "QUEUE" = "AlphaTest"
        "RenderPipeline" = "UniversalPipeline"
        "RenderType" = "Opaque"
        "UniversalMaterialType" = "Unlit"
      }
      // m_ProgramMask = 6
      Program "vp"
      {
        SubProgram "vulkan"
        {
          
      }
      Program "fp"
      {
      }
      Program "gp"
      {
      }
      Program "hp"
      {
      }
      Program "dp"
      {
      }
      Program "surface"
      {
      }
      Program "rtp"
      {
      }
      
    } // end phase
  }
  SubShader
  {
    Tags
    { 
      "QUEUE" = "AlphaTest"
      "RenderPipeline" = "UniversalPipeline"
      "RenderType" = "Opaque"
      "UniversalMaterialType" = "Unlit"
    }
    Pass // ind: 1, name: Pass
    {
      Name "Pass"
      Tags
      { 
        "QUEUE" = "AlphaTest"
        "RenderPipeline" = "UniversalPipeline"
        "RenderType" = "Opaque"
        "UniversalMaterialType" = "Unlit"
      }
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      
      
      uniform float4 unity_MatrixVP[4];
      
      uniform sampler2D Texture2D_4972a32b9fed4a2b8180e52f9e0028d3;
      
      
      
      struct appdata_t
      {
          
          float3 vertex : POSITION0;
          
          float4 texcoord : TEXCOORD0;
      
      };
      
      
      struct OUT_Data_Vert
      {
          
          float4 texcoord : TEXCOORD0;
          
          float4 vertex : SV_POSITION;
      
      };
      
      
      struct v2f
      {
          
          float4 texcoord : TEXCOORD0;
      
      };
      
      
      struct OUT_Data_Frag
      {
          
          float4 SV_TARGET0 : SV_TARGET0;
      
      };
      
      
      uniform UnityPerDraw 
          {
          
          #endif
          uniform float4 unity_ObjectToWorld[4];
          
          uniform float4 unity_WorldToObject[4];
          
          uniform float4 unity_LODFade;
          
          uniform float4 unity_WorldTransformParams;
          
          uniform float4 unity_LightData;
          
          uniform float4 unity_LightIndices[2];
          
          uniform float4 unity_ProbesOcclusion;
          
          uniform float4 unity_SpecCube0_HDR;
          
          // uniform float4 unity_LightmapST;
          
          // uniform float4 unity_DynamicLightmapST;
          
          uniform float4 unity_SHAr;
          
          uniform float4 unity_SHAg;
          
          uniform float4 unity_SHAb;
          
          uniform float4 unity_SHBr;
          
          uniform float4 unity_SHBg;
          
          uniform float4 unity_SHBb;
          
          uniform float4 unity_SHC;
          
          #if HLSLCC_ENABLE_UNIFORM_BUFFERS
      };
      
      float4 u_xlat0;
      
      float4 u_xlat1;
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          
          u_xlat0.xyz = in_v.vertex.yyy * unity_ObjectToWorld[1].xyz;
          
          u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_v.vertex.xxx + u_xlat0.xyz;
          
          u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_v.vertex.zzz + u_xlat0.xyz;
          
          u_xlat0.xyz = u_xlat0.xyz + unity_ObjectToWorld[3].xyz;
          
          u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
          
          u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
          
          u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
          
          out_v.vertex = u_xlat0 + unity_MatrixVP[3];
          
          out_v.texcoord = in_v.texcoord;
          
          return;
      
      }
      
      
      #define CODE_BLOCK_FRAGMENT
      
      
      
      uniform UnityPerMaterial 
          {
          
          #endif
          uniform float4 Texture2D_4972a32b9fed4a2b8180e52f9e0028d3_TexelSize;
          
          uniform float4 _BaseColor;
          
          uniform float Vector1_1d89e84864d54f5189c0732a74238096;
          
          uniform float Vector1_afb8382c98f844d580b89da5ae4dfa01;
          
          uniform float _NoiseScale;
          
          uniform float _DissolveThreshhold;
          
          uniform float _Thickness;
          
          uniform float4 _EdgeColor;
          
          #if HLSLCC_ENABLE_UNIFORM_BUFFERS
      };
      
      float4 u_xlat0_d;
      
      float4 u_xlat1_d;
      
      float3 u_xlat16_1;
      
      int u_xlatb1;
      
      float4 u_xlat2;
      
      float4 u_xlat3;
      
      float4 u_xlat4;
      
      float4 u_xlat5;
      
      float u_xlat6;
      
      float3 u_xlat7;
      
      float2 u_xlat13;
      
      float2 u_xlat14;
      
      float u_xlat19;
      
      OUT_Data_Frag frag(v2f in_f)
      {
          
          u_xlat0_d = in_f.texcoord.xyxy * float4(float4(_NoiseScale, _NoiseScale, _NoiseScale, _NoiseScale));
          
          u_xlat1_d.xy = floor(u_xlat0_d.zw);
          
          u_xlat13.xy = u_xlat1_d.xy + float2(1.0, 1.0);
          
          u_xlat13.x = dot(u_xlat13.xy, float2(12.9898005, 78.2330017));
          
          u_xlat13.x = sin(u_xlat13.x);
          
          u_xlat13.x = u_xlat13.x * 43758.5469;
          
          u_xlat1_d.z = fract(u_xlat13.x);
          
          u_xlat2.xy = fract(u_xlat0_d.zw);
          
          u_xlat0_d = u_xlat0_d * float4(0.5, 0.5, 0.25, 0.25);
          
          u_xlat14.xy = u_xlat2.xy * u_xlat2.xy;
          
          u_xlat2.xy = (-u_xlat2.xy) * float2(2.0, 2.0) + float2(3.0, 3.0);
          
          u_xlat3.xy = u_xlat2.xy * u_xlat14.xy;
          
          u_xlat2.xy = (-u_xlat14.xy) * u_xlat2.xy + float2(1.0, 1.0);
          
          u_xlat4 = u_xlat1_d.xyxy + float4(1.0, 0.0, 0.0, 1.0);
          
          u_xlat1_d.x = dot(u_xlat1_d.xy, float2(12.9898005, 78.2330017));
          
          u_xlat1_d.x = sin(u_xlat1_d.x);
          
          u_xlat1_d.x = u_xlat1_d.x * 43758.5469;
          
          u_xlat7.x = dot(u_xlat4.zw, float2(12.9898005, 78.2330017));
          
          u_xlat7.z = dot(u_xlat4.xy, float2(12.9898005, 78.2330017));
          
          u_xlat7.xz = sin(u_xlat7.xz);
          
          u_xlat1_d.yw = u_xlat7.xz * float2(43758.5469, 43758.5469);
          
          u_xlat1_d.xyw = fract(u_xlat1_d.xyw);
          
          u_xlat13.xy = u_xlat1_d.zw * u_xlat3.xx;
          
          u_xlat1_d.x = u_xlat2.x * u_xlat1_d.x + u_xlat13.y;
          
          u_xlat7.x = u_xlat2.x * u_xlat1_d.y + u_xlat13.x;
          
          u_xlat7.x = u_xlat7.x * u_xlat3.y;
          
          u_xlat1_d.x = u_xlat2.y * u_xlat1_d.x + u_xlat7.x;
          
          u_xlat2 = floor(u_xlat0_d);
          
          u_xlat0_d = fract(u_xlat0_d);
          
          u_xlat3 = u_xlat2.xyxy + float4(1.0, 0.0, 0.0, 1.0);
          
          u_xlat7.x = dot(u_xlat3.zw, float2(12.9898005, 78.2330017));
          
          u_xlat7.y = dot(u_xlat3.xy, float2(12.9898005, 78.2330017));
          
          u_xlat7.xy = sin(u_xlat7.xy);
          
          u_xlat7.xy = u_xlat7.xy * float2(43758.5469, 43758.5469);
          
          u_xlat3 = u_xlat2 + float4(1.0, 1.0, 1.0, 0.0);
          
          u_xlat19 = dot(u_xlat3.xy, float2(12.9898005, 78.2330017));
          
          u_xlat3.x = dot(u_xlat3.zw, float2(12.9898005, 78.2330017));
          
          u_xlat3.x = sin(u_xlat3.x);
          
          u_xlat3.x = u_xlat3.x * 43758.5469;
          
          u_xlat3.x = fract(u_xlat3.x);
          
          u_xlat19 = sin(u_xlat19);
          
          u_xlat7.z = u_xlat19 * 43758.5469;
          
          u_xlat7.xyz = fract(u_xlat7.xyz);
          
          u_xlat4 = u_xlat0_d * u_xlat0_d;
          
          u_xlat0_d = (-u_xlat0_d) * float4(2.0, 2.0, 2.0, 2.0) + float4(3.0, 3.0, 3.0, 3.0);
          
          u_xlat5 = u_xlat0_d * u_xlat4;
          
          u_xlat0_d = (-u_xlat4) * u_xlat0_d + float4(1.0, 1.0, 1.0, 1.0);
          
          u_xlat19 = u_xlat7.z * u_xlat5.x;
          
          u_xlat7.x = u_xlat0_d.x * u_xlat7.x + u_xlat19;
          
          u_xlat19 = dot(u_xlat2.xy, float2(12.9898005, 78.2330017));
          
          u_xlat19 = sin(u_xlat19);
          
          u_xlat19 = u_xlat19 * 43758.5469;
          
          u_xlat19 = fract(u_xlat19);
          
          u_xlat7.xy = u_xlat7.xy * u_xlat5.yx;
          
          u_xlat0_d.x = u_xlat0_d.x * u_xlat19 + u_xlat7.y;
          
          u_xlat0_d.x = u_xlat0_d.y * u_xlat0_d.x + u_xlat7.x;
          
          u_xlat0_d.x = u_xlat0_d.x * 0.25;
          
          u_xlat0_d.x = u_xlat1_d.x * 0.125 + u_xlat0_d.x;
          
          u_xlat1_d = u_xlat2.zwzw + float4(0.0, 1.0, 1.0, 1.0);
          
          u_xlat6 = dot(u_xlat2.zw, float2(12.9898005, 78.2330017));
          
          u_xlat6 = sin(u_xlat6);
          
          u_xlat6 = u_xlat6 * 43758.5469;
          
          u_xlat6 = fract(u_xlat6);
          
          u_xlat1_d.z = dot(u_xlat1_d.zw, float2(12.9898005, 78.2330017));
          
          u_xlat1_d.x = dot(u_xlat1_d.xy, float2(12.9898005, 78.2330017));
          
          u_xlat1_d.xy = sin(u_xlat1_d.xz);
          
          u_xlat1_d.xy = u_xlat1_d.xy * float2(43758.5469, 43758.5469);
          
          u_xlat1_d.xy = fract(u_xlat1_d.xy);
          
          u_xlat7.x = u_xlat1_d.y * u_xlat5.z;
          
          u_xlat1_d.x = u_xlat0_d.z * u_xlat1_d.x + u_xlat7.x;
          
          u_xlat1_d.x = u_xlat1_d.x * u_xlat5.w;
          
          u_xlat7.x = u_xlat3.x * u_xlat5.z;
          
          u_xlat6 = u_xlat0_d.z * u_xlat6 + u_xlat7.x;
          
          u_xlat6 = u_xlat0_d.w * u_xlat6 + u_xlat1_d.x;
          
          u_xlat0_d.w = u_xlat6 * 0.5 + u_xlat0_d.x;
          
          u_xlat1_d.x = u_xlat0_d.w + (-_DissolveThreshhold);
          
          #ifdef UNITY_ADRENO_ES3
          u_xlatb1 = (u_xlat1_d.x<0.0);
          
          #else
          u_xlatb1 = u_xlat1_d.x<0.0;
          
          #endif
          if(u_xlatb1)
      {
              discard;
      }
          
          u_xlat16_1.xyz = texture(Texture2D_4972a32b9fed4a2b8180e52f9e0028d3, in_f.texcoord.xy).xyz;
          
          u_xlat0_d.xyz = u_xlat16_1.xyz * _BaseColor.xyz;
          
          out_f.SV_TARGET0 = u_xlat0_d;
          
          return;
      
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: ShadowCaster
    {
      Name "ShadowCaster"
      Tags
      { 
        "LIGHTMODE" = "SHADOWCASTER"
        "QUEUE" = "AlphaTest"
        "RenderPipeline" = "UniversalPipeline"
        "RenderType" = "Opaque"
        "UniversalMaterialType" = "Unlit"
      }
      ColorMask 0
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      
      
      uniform float4 unity_MatrixVP[4];
      
      uniform float4 _ShadowBias;
      
      uniform float3 _LightDirection;
      
      
      
      struct appdata_t
      {
          
          float3 vertex : POSITION0;
          
          float3 normal : NORMAL0;
          
          float4 texcoord : TEXCOORD0;
      
      };
      
      
      struct OUT_Data_Vert
      {
          
          float3 texcoord : TEXCOORD0;
          
          float4 texcoord1 : TEXCOORD1;
          
          float4 vertex : SV_POSITION;
      
      };
      
      
      struct v2f
      {
          
          float4 texcoord1 : TEXCOORD1;
      
      };
      
      
      struct OUT_Data_Frag
      {
          
          float4 SV_TARGET0 : SV_TARGET0;
      
      };
      
      
      uniform UnityPerDraw 
          {
          
          #endif
          uniform float4 unity_ObjectToWorld[4];
          
          uniform float4 unity_WorldToObject[4];
          
          uniform float4 unity_LODFade;
          
          uniform float4 unity_WorldTransformParams;
          
          uniform float4 unity_LightData;
          
          uniform float4 unity_LightIndices[2];
          
          uniform float4 unity_ProbesOcclusion;
          
          uniform float4 unity_SpecCube0_HDR;
          
          // uniform float4 unity_LightmapST;
          
          // uniform float4 unity_DynamicLightmapST;
          
          uniform float4 unity_SHAr;
          
          uniform float4 unity_SHAg;
          
          uniform float4 unity_SHAb;
          
          uniform float4 unity_SHBr;
          
          uniform float4 unity_SHBg;
          
          uniform float4 unity_SHBb;
          
          uniform float4 unity_SHC;
          
          #if HLSLCC_ENABLE_UNIFORM_BUFFERS
      };
      
      float4 u_xlat0;
      
      float4 u_xlat1;
      
      float u_xlat16_2;
      
      float u_xlat9;
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          
          u_xlat0.xyz = in_v.vertex.yyy * unity_ObjectToWorld[1].xyz;
          
          u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_v.vertex.xxx + u_xlat0.xyz;
          
          u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_v.vertex.zzz + u_xlat0.xyz;
          
          u_xlat0.xyz = u_xlat0.xyz + unity_ObjectToWorld[3].xyz;
          
          u_xlat0.xyz = _LightDirection.xyz * _ShadowBias.xxx + u_xlat0.xyz;
          
          u_xlat1.x = dot(in_v.normal.xyz, unity_WorldToObject[0].xyz);
          
          u_xlat1.y = dot(in_v.normal.xyz, unity_WorldToObject[1].xyz);
          
          u_xlat1.z = dot(in_v.normal.xyz, unity_WorldToObject[2].xyz);
          
          u_xlat9 = dot(u_xlat1.xyz, u_xlat1.xyz);
          
          u_xlat9 = max(u_xlat9, 1.17549435e-38);
          
          u_xlat16_2 = inversesqrt(u_xlat9);
          
          u_xlat1.xyz = u_xlat1.xyz * float3(u_xlat16_2);
          
          u_xlat9 = dot(_LightDirection.xyz, u_xlat1.xyz);
          
          #ifdef UNITY_ADRENO_ES3
          u_xlat9 = min(max(u_xlat9, 0.0), 1.0);
          
          #else
          u_xlat9 = clamp(u_xlat9, 0.0, 1.0);
          
          #endif
          u_xlat9 = (-u_xlat9) + 1.0;
          
          u_xlat9 = u_xlat9 * _ShadowBias.y;
          
          u_xlat0.xyz = u_xlat1.xyz * float3(u_xlat9) + u_xlat0.xyz;
          
          out_v.texcoord.xyz = u_xlat1.xyz;
          
          u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
          
          u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
          
          u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
          
          u_xlat0 = u_xlat0 + unity_MatrixVP[3];
          
          out_v.vertex.z = max((-u_xlat0.w), u_xlat0.z);
          
          out_v.vertex.xyw = u_xlat0.xyw;
          
          out_v.texcoord1 = in_v.texcoord;
          
          return;
      
      }
      
      
      #define CODE_BLOCK_FRAGMENT
      
      
      
      uniform UnityPerMaterial 
          {
          
          #endif
          uniform float4 Texture2D_4972a32b9fed4a2b8180e52f9e0028d3_TexelSize;
          
          uniform float4 _BaseColor;
          
          uniform float Vector1_1d89e84864d54f5189c0732a74238096;
          
          uniform float Vector1_afb8382c98f844d580b89da5ae4dfa01;
          
          uniform float _NoiseScale;
          
          uniform float _DissolveThreshhold;
          
          uniform float _Thickness;
          
          uniform float4 _EdgeColor;
          
          #if HLSLCC_ENABLE_UNIFORM_BUFFERS
      };
      
      float4 u_xlat0_d;
      
      int u_xlatb0;
      
      float4 u_xlat1_d;
      
      float4 u_xlat2;
      
      float4 u_xlat3;
      
      float4 u_xlat4;
      
      float4 u_xlat5;
      
      float u_xlat6;
      
      float3 u_xlat7;
      
      float2 u_xlat13;
      
      float2 u_xlat14;
      
      float u_xlat19;
      
      OUT_Data_Frag frag(v2f in_f)
      {
          
          u_xlat0_d = in_f.texcoord1.xyxy * float4(float4(_NoiseScale, _NoiseScale, _NoiseScale, _NoiseScale));
          
          u_xlat1_d.xy = floor(u_xlat0_d.zw);
          
          u_xlat13.xy = u_xlat1_d.xy + float2(1.0, 1.0);
          
          u_xlat13.x = dot(u_xlat13.xy, float2(12.9898005, 78.2330017));
          
          u_xlat13.x = sin(u_xlat13.x);
          
          u_xlat13.x = u_xlat13.x * 43758.5469;
          
          u_xlat1_d.z = fract(u_xlat13.x);
          
          u_xlat2.xy = fract(u_xlat0_d.zw);
          
          u_xlat0_d = u_xlat0_d * float4(0.5, 0.5, 0.25, 0.25);
          
          u_xlat14.xy = u_xlat2.xy * u_xlat2.xy;
          
          u_xlat2.xy = (-u_xlat2.xy) * float2(2.0, 2.0) + float2(3.0, 3.0);
          
          u_xlat3.xy = u_xlat2.xy * u_xlat14.xy;
          
          u_xlat2.xy = (-u_xlat14.xy) * u_xlat2.xy + float2(1.0, 1.0);
          
          u_xlat4 = u_xlat1_d.xyxy + float4(1.0, 0.0, 0.0, 1.0);
          
          u_xlat1_d.x = dot(u_xlat1_d.xy, float2(12.9898005, 78.2330017));
          
          u_xlat1_d.x = sin(u_xlat1_d.x);
          
          u_xlat1_d.x = u_xlat1_d.x * 43758.5469;
          
          u_xlat7.x = dot(u_xlat4.zw, float2(12.9898005, 78.2330017));
          
          u_xlat7.z = dot(u_xlat4.xy, float2(12.9898005, 78.2330017));
          
          u_xlat7.xz = sin(u_xlat7.xz);
          
          u_xlat1_d.yw = u_xlat7.xz * float2(43758.5469, 43758.5469);
          
          u_xlat1_d.xyw = fract(u_xlat1_d.xyw);
          
          u_xlat13.xy = u_xlat1_d.zw * u_xlat3.xx;
          
          u_xlat1_d.x = u_xlat2.x * u_xlat1_d.x + u_xlat13.y;
          
          u_xlat7.x = u_xlat2.x * u_xlat1_d.y + u_xlat13.x;
          
          u_xlat7.x = u_xlat7.x * u_xlat3.y;
          
          u_xlat1_d.x = u_xlat2.y * u_xlat1_d.x + u_xlat7.x;
          
          u_xlat2 = floor(u_xlat0_d);
          
          u_xlat0_d = fract(u_xlat0_d);
          
          u_xlat3 = u_xlat2.xyxy + float4(1.0, 0.0, 0.0, 1.0);
          
          u_xlat7.x = dot(u_xlat3.zw, float2(12.9898005, 78.2330017));
          
          u_xlat7.y = dot(u_xlat3.xy, float2(12.9898005, 78.2330017));
          
          u_xlat7.xy = sin(u_xlat7.xy);
          
          u_xlat7.xy = u_xlat7.xy * float2(43758.5469, 43758.5469);
          
          u_xlat3 = u_xlat2 + float4(1.0, 1.0, 1.0, 0.0);
          
          u_xlat19 = dot(u_xlat3.xy, float2(12.9898005, 78.2330017));
          
          u_xlat3.x = dot(u_xlat3.zw, float2(12.9898005, 78.2330017));
          
          u_xlat3.x = sin(u_xlat3.x);
          
          u_xlat3.x = u_xlat3.x * 43758.5469;
          
          u_xlat3.x = fract(u_xlat3.x);
          
          u_xlat19 = sin(u_xlat19);
          
          u_xlat7.z = u_xlat19 * 43758.5469;
          
          u_xlat7.xyz = fract(u_xlat7.xyz);
          
          u_xlat4 = u_xlat0_d * u_xlat0_d;
          
          u_xlat0_d = (-u_xlat0_d) * float4(2.0, 2.0, 2.0, 2.0) + float4(3.0, 3.0, 3.0, 3.0);
          
          u_xlat5 = u_xlat0_d * u_xlat4;
          
          u_xlat0_d = (-u_xlat4) * u_xlat0_d + float4(1.0, 1.0, 1.0, 1.0);
          
          u_xlat19 = u_xlat7.z * u_xlat5.x;
          
          u_xlat7.x = u_xlat0_d.x * u_xlat7.x + u_xlat19;
          
          u_xlat19 = dot(u_xlat2.xy, float2(12.9898005, 78.2330017));
          
          u_xlat19 = sin(u_xlat19);
          
          u_xlat19 = u_xlat19 * 43758.5469;
          
          u_xlat19 = fract(u_xlat19);
          
          u_xlat7.xy = u_xlat7.xy * u_xlat5.yx;
          
          u_xlat0_d.x = u_xlat0_d.x * u_xlat19 + u_xlat7.y;
          
          u_xlat0_d.x = u_xlat0_d.y * u_xlat0_d.x + u_xlat7.x;
          
          u_xlat0_d.x = u_xlat0_d.x * 0.25;
          
          u_xlat0_d.x = u_xlat1_d.x * 0.125 + u_xlat0_d.x;
          
          u_xlat1_d = u_xlat2.zwzw + float4(0.0, 1.0, 1.0, 1.0);
          
          u_xlat6 = dot(u_xlat2.zw, float2(12.9898005, 78.2330017));
          
          u_xlat6 = sin(u_xlat6);
          
          u_xlat6 = u_xlat6 * 43758.5469;
          
          u_xlat6 = fract(u_xlat6);
          
          u_xlat1_d.z = dot(u_xlat1_d.zw, float2(12.9898005, 78.2330017));
          
          u_xlat1_d.x = dot(u_xlat1_d.xy, float2(12.9898005, 78.2330017));
          
          u_xlat1_d.xy = sin(u_xlat1_d.xz);
          
          u_xlat1_d.xy = u_xlat1_d.xy * float2(43758.5469, 43758.5469);
          
          u_xlat1_d.xy = fract(u_xlat1_d.xy);
          
          u_xlat7.x = u_xlat1_d.y * u_xlat5.z;
          
          u_xlat1_d.x = u_xlat0_d.z * u_xlat1_d.x + u_xlat7.x;
          
          u_xlat1_d.x = u_xlat1_d.x * u_xlat5.w;
          
          u_xlat7.x = u_xlat3.x * u_xlat5.z;
          
          u_xlat6 = u_xlat0_d.z * u_xlat6 + u_xlat7.x;
          
          u_xlat6 = u_xlat0_d.w * u_xlat6 + u_xlat1_d.x;
          
          u_xlat0_d.x = u_xlat6 * 0.5 + u_xlat0_d.x;
          
          u_xlat0_d.x = u_xlat0_d.x + (-_DissolveThreshhold);
          
          #ifdef UNITY_ADRENO_ES3
          u_xlatb0 = (u_xlat0_d.x<0.0);
          
          #else
          u_xlatb0 = u_xlat0_d.x<0.0;
          
          #endif
          if(u_xlatb0)
      {
              discard;
      }
          
          out_f.SV_TARGET0 = float4(0.0, 0.0, 0.0, 0.0);
          
          return;
      
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 3, name: DepthOnly
    {
      Name "DepthOnly"
      Tags
      { 
        "LIGHTMODE" = "DepthOnly"
        "QUEUE" = "AlphaTest"
        "RenderPipeline" = "UniversalPipeline"
        "RenderType" = "Opaque"
        "UniversalMaterialType" = "Unlit"
      }
      ColorMask 0
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      
      
      uniform float4 unity_MatrixVP[4];
      
      
      
      struct appdata_t
      {
          
          float3 vertex : POSITION0;
          
          float4 texcoord : TEXCOORD0;
      
      };
      
      
      struct OUT_Data_Vert
      {
          
          float4 texcoord : TEXCOORD0;
          
          float4 vertex : SV_POSITION;
      
      };
      
      
      struct v2f
      {
          
          float4 texcoord : TEXCOORD0;
      
      };
      
      
      struct OUT_Data_Frag
      {
          
          float4 SV_TARGET0 : SV_TARGET0;
      
      };
      
      
      uniform UnityPerDraw 
          {
          
          #endif
          uniform float4 unity_ObjectToWorld[4];
          
          uniform float4 unity_WorldToObject[4];
          
          uniform float4 unity_LODFade;
          
          uniform float4 unity_WorldTransformParams;
          
          uniform float4 unity_LightData;
          
          uniform float4 unity_LightIndices[2];
          
          uniform float4 unity_ProbesOcclusion;
          
          uniform float4 unity_SpecCube0_HDR;
          
          // uniform float4 unity_LightmapST;
          
          // uniform float4 unity_DynamicLightmapST;
          
          uniform float4 unity_SHAr;
          
          uniform float4 unity_SHAg;
          
          uniform float4 unity_SHAb;
          
          uniform float4 unity_SHBr;
          
          uniform float4 unity_SHBg;
          
          uniform float4 unity_SHBb;
          
          uniform float4 unity_SHC;
          
          #if HLSLCC_ENABLE_UNIFORM_BUFFERS
      };
      
      float4 u_xlat0;
      
      float4 u_xlat1;
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          
          u_xlat0.xyz = in_v.vertex.yyy * unity_ObjectToWorld[1].xyz;
          
          u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_v.vertex.xxx + u_xlat0.xyz;
          
          u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_v.vertex.zzz + u_xlat0.xyz;
          
          u_xlat0.xyz = u_xlat0.xyz + unity_ObjectToWorld[3].xyz;
          
          u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
          
          u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
          
          u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
          
          out_v.vertex = u_xlat0 + unity_MatrixVP[3];
          
          out_v.texcoord = in_v.texcoord;
          
          return;
      
      }
      
      
      #define CODE_BLOCK_FRAGMENT
      
      
      
      uniform UnityPerMaterial 
          {
          
          #endif
          uniform float4 Texture2D_4972a32b9fed4a2b8180e52f9e0028d3_TexelSize;
          
          uniform float4 _BaseColor;
          
          uniform float Vector1_1d89e84864d54f5189c0732a74238096;
          
          uniform float Vector1_afb8382c98f844d580b89da5ae4dfa01;
          
          uniform float _NoiseScale;
          
          uniform float _DissolveThreshhold;
          
          uniform float _Thickness;
          
          uniform float4 _EdgeColor;
          
          #if HLSLCC_ENABLE_UNIFORM_BUFFERS
      };
      
      float4 u_xlat0_d;
      
      int u_xlatb0;
      
      float4 u_xlat1_d;
      
      float4 u_xlat2;
      
      float4 u_xlat3;
      
      float4 u_xlat4;
      
      float4 u_xlat5;
      
      float u_xlat6;
      
      float3 u_xlat7;
      
      float2 u_xlat13;
      
      float2 u_xlat14;
      
      float u_xlat19;
      
      OUT_Data_Frag frag(v2f in_f)
      {
          
          u_xlat0_d = in_f.texcoord.xyxy * float4(float4(_NoiseScale, _NoiseScale, _NoiseScale, _NoiseScale));
          
          u_xlat1_d.xy = floor(u_xlat0_d.zw);
          
          u_xlat13.xy = u_xlat1_d.xy + float2(1.0, 1.0);
          
          u_xlat13.x = dot(u_xlat13.xy, float2(12.9898005, 78.2330017));
          
          u_xlat13.x = sin(u_xlat13.x);
          
          u_xlat13.x = u_xlat13.x * 43758.5469;
          
          u_xlat1_d.z = fract(u_xlat13.x);
          
          u_xlat2.xy = fract(u_xlat0_d.zw);
          
          u_xlat0_d = u_xlat0_d * float4(0.5, 0.5, 0.25, 0.25);
          
          u_xlat14.xy = u_xlat2.xy * u_xlat2.xy;
          
          u_xlat2.xy = (-u_xlat2.xy) * float2(2.0, 2.0) + float2(3.0, 3.0);
          
          u_xlat3.xy = u_xlat2.xy * u_xlat14.xy;
          
          u_xlat2.xy = (-u_xlat14.xy) * u_xlat2.xy + float2(1.0, 1.0);
          
          u_xlat4 = u_xlat1_d.xyxy + float4(1.0, 0.0, 0.0, 1.0);
          
          u_xlat1_d.x = dot(u_xlat1_d.xy, float2(12.9898005, 78.2330017));
          
          u_xlat1_d.x = sin(u_xlat1_d.x);
          
          u_xlat1_d.x = u_xlat1_d.x * 43758.5469;
          
          u_xlat7.x = dot(u_xlat4.zw, float2(12.9898005, 78.2330017));
          
          u_xlat7.z = dot(u_xlat4.xy, float2(12.9898005, 78.2330017));
          
          u_xlat7.xz = sin(u_xlat7.xz);
          
          u_xlat1_d.yw = u_xlat7.xz * float2(43758.5469, 43758.5469);
          
          u_xlat1_d.xyw = fract(u_xlat1_d.xyw);
          
          u_xlat13.xy = u_xlat1_d.zw * u_xlat3.xx;
          
          u_xlat1_d.x = u_xlat2.x * u_xlat1_d.x + u_xlat13.y;
          
          u_xlat7.x = u_xlat2.x * u_xlat1_d.y + u_xlat13.x;
          
          u_xlat7.x = u_xlat7.x * u_xlat3.y;
          
          u_xlat1_d.x = u_xlat2.y * u_xlat1_d.x + u_xlat7.x;
          
          u_xlat2 = floor(u_xlat0_d);
          
          u_xlat0_d = fract(u_xlat0_d);
          
          u_xlat3 = u_xlat2.xyxy + float4(1.0, 0.0, 0.0, 1.0);
          
          u_xlat7.x = dot(u_xlat3.zw, float2(12.9898005, 78.2330017));
          
          u_xlat7.y = dot(u_xlat3.xy, float2(12.9898005, 78.2330017));
          
          u_xlat7.xy = sin(u_xlat7.xy);
          
          u_xlat7.xy = u_xlat7.xy * float2(43758.5469, 43758.5469);
          
          u_xlat3 = u_xlat2 + float4(1.0, 1.0, 1.0, 0.0);
          
          u_xlat19 = dot(u_xlat3.xy, float2(12.9898005, 78.2330017));
          
          u_xlat3.x = dot(u_xlat3.zw, float2(12.9898005, 78.2330017));
          
          u_xlat3.x = sin(u_xlat3.x);
          
          u_xlat3.x = u_xlat3.x * 43758.5469;
          
          u_xlat3.x = fract(u_xlat3.x);
          
          u_xlat19 = sin(u_xlat19);
          
          u_xlat7.z = u_xlat19 * 43758.5469;
          
          u_xlat7.xyz = fract(u_xlat7.xyz);
          
          u_xlat4 = u_xlat0_d * u_xlat0_d;
          
          u_xlat0_d = (-u_xlat0_d) * float4(2.0, 2.0, 2.0, 2.0) + float4(3.0, 3.0, 3.0, 3.0);
          
          u_xlat5 = u_xlat0_d * u_xlat4;
          
          u_xlat0_d = (-u_xlat4) * u_xlat0_d + float4(1.0, 1.0, 1.0, 1.0);
          
          u_xlat19 = u_xlat7.z * u_xlat5.x;
          
          u_xlat7.x = u_xlat0_d.x * u_xlat7.x + u_xlat19;
          
          u_xlat19 = dot(u_xlat2.xy, float2(12.9898005, 78.2330017));
          
          u_xlat19 = sin(u_xlat19);
          
          u_xlat19 = u_xlat19 * 43758.5469;
          
          u_xlat19 = fract(u_xlat19);
          
          u_xlat7.xy = u_xlat7.xy * u_xlat5.yx;
          
          u_xlat0_d.x = u_xlat0_d.x * u_xlat19 + u_xlat7.y;
          
          u_xlat0_d.x = u_xlat0_d.y * u_xlat0_d.x + u_xlat7.x;
          
          u_xlat0_d.x = u_xlat0_d.x * 0.25;
          
          u_xlat0_d.x = u_xlat1_d.x * 0.125 + u_xlat0_d.x;
          
          u_xlat1_d = u_xlat2.zwzw + float4(0.0, 1.0, 1.0, 1.0);
          
          u_xlat6 = dot(u_xlat2.zw, float2(12.9898005, 78.2330017));
          
          u_xlat6 = sin(u_xlat6);
          
          u_xlat6 = u_xlat6 * 43758.5469;
          
          u_xlat6 = fract(u_xlat6);
          
          u_xlat1_d.z = dot(u_xlat1_d.zw, float2(12.9898005, 78.2330017));
          
          u_xlat1_d.x = dot(u_xlat1_d.xy, float2(12.9898005, 78.2330017));
          
          u_xlat1_d.xy = sin(u_xlat1_d.xz);
          
          u_xlat1_d.xy = u_xlat1_d.xy * float2(43758.5469, 43758.5469);
          
          u_xlat1_d.xy = fract(u_xlat1_d.xy);
          
          u_xlat7.x = u_xlat1_d.y * u_xlat5.z;
          
          u_xlat1_d.x = u_xlat0_d.z * u_xlat1_d.x + u_xlat7.x;
          
          u_xlat1_d.x = u_xlat1_d.x * u_xlat5.w;
          
          u_xlat7.x = u_xlat3.x * u_xlat5.z;
          
          u_xlat6 = u_xlat0_d.z * u_xlat6 + u_xlat7.x;
          
          u_xlat6 = u_xlat0_d.w * u_xlat6 + u_xlat1_d.x;
          
          u_xlat0_d.x = u_xlat6 * 0.5 + u_xlat0_d.x;
          
          u_xlat0_d.x = u_xlat0_d.x + (-_DissolveThreshhold);
          
          #ifdef UNITY_ADRENO_ES3
          u_xlatb0 = (u_xlat0_d.x<0.0);
          
          #else
          u_xlatb0 = u_xlat0_d.x<0.0;
          
          #endif
          if(u_xlatb0)
      {
              discard;
      }
          
          out_f.SV_TARGET0 = float4(0.0, 0.0, 0.0, 0.0);
          
          return;
      
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 4, name: DepthNormals
    {
      Name "DepthNormals"
      Tags
      { 
        "LIGHTMODE" = "DepthNormals"
        "QUEUE" = "AlphaTest"
        "RenderPipeline" = "UniversalPipeline"
        "RenderType" = "Opaque"
        "UniversalMaterialType" = "Unlit"
      }
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      
      
      uniform float4 unity_MatrixVP[4];
      
      
      
      struct appdata_t
      {
          
          float3 vertex : POSITION0;
          
          float3 normal : NORMAL0;
          
          float4 tangent : TANGENT0;
          
          float4 texcoord : TEXCOORD0;
      
      };
      
      
      struct OUT_Data_Vert
      {
          
          float3 texcoord : TEXCOORD0;
          
          float4 texcoord1 : TEXCOORD1;
          
          float4 texcoord2 : TEXCOORD2;
          
          float4 vertex : SV_POSITION;
      
      };
      
      
      struct v2f
      {
          
          float3 texcoord : TEXCOORD0;
          
          float4 texcoord2 : TEXCOORD2;
      
      };
      
      
      struct OUT_Data_Frag
      {
          
          float4 SV_TARGET0 : SV_TARGET0;
      
      };
      
      
      uniform UnityPerDraw 
          {
          
          #endif
          uniform float4 unity_ObjectToWorld[4];
          
          uniform float4 unity_WorldToObject[4];
          
          uniform float4 unity_LODFade;
          
          uniform float4 unity_WorldTransformParams;
          
          uniform float4 unity_LightData;
          
          uniform float4 unity_LightIndices[2];
          
          uniform float4 unity_ProbesOcclusion;
          
          uniform float4 unity_SpecCube0_HDR;
          
          // uniform float4 unity_LightmapST;
          
          // uniform float4 unity_DynamicLightmapST;
          
          uniform float4 unity_SHAr;
          
          uniform float4 unity_SHAg;
          
          uniform float4 unity_SHAb;
          
          uniform float4 unity_SHBr;
          
          uniform float4 unity_SHBg;
          
          uniform float4 unity_SHBb;
          
          uniform float4 unity_SHC;
          
          #if HLSLCC_ENABLE_UNIFORM_BUFFERS
      };
      
      float4 u_xlat0;
      
      float4 u_xlat1;
      
      float u_xlat16_2;
      
      float u_xlat9;
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          
          u_xlat0.xyz = in_v.vertex.yyy * unity_ObjectToWorld[1].xyz;
          
          u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_v.vertex.xxx + u_xlat0.xyz;
          
          u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_v.vertex.zzz + u_xlat0.xyz;
          
          u_xlat0.xyz = u_xlat0.xyz + unity_ObjectToWorld[3].xyz;
          
          u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
          
          u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
          
          u_xlat0 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
          
          out_v.vertex = u_xlat0 + unity_MatrixVP[3];
          
          u_xlat0.x = dot(in_v.normal.xyz, unity_WorldToObject[0].xyz);
          
          u_xlat0.y = dot(in_v.normal.xyz, unity_WorldToObject[1].xyz);
          
          u_xlat0.z = dot(in_v.normal.xyz, unity_WorldToObject[2].xyz);
          
          u_xlat9 = dot(u_xlat0.xyz, u_xlat0.xyz);
          
          u_xlat9 = max(u_xlat9, 1.17549435e-38);
          
          u_xlat16_2 = inversesqrt(u_xlat9);
          
          out_v.texcoord.xyz = u_xlat0.xyz * float3(u_xlat16_2);
          
          u_xlat0.xyz = in_v.tangent.yyy * unity_ObjectToWorld[1].xyz;
          
          u_xlat0.xyz = unity_ObjectToWorld[0].xyz * in_v.tangent.xxx + u_xlat0.xyz;
          
          u_xlat0.xyz = unity_ObjectToWorld[2].xyz * in_v.tangent.zzz + u_xlat0.xyz;
          
          u_xlat9 = dot(u_xlat0.xyz, u_xlat0.xyz);
          
          u_xlat9 = max(u_xlat9, 1.17549435e-38);
          
          u_xlat16_2 = inversesqrt(u_xlat9);
          
          out_v.texcoord1.xyz = u_xlat0.xyz * float3(u_xlat16_2);
          
          out_v.texcoord1.w = in_v.tangent.w;
          
          out_v.texcoord2 = in_v.texcoord;
          
          return;
      
      }
      
      
      #define CODE_BLOCK_FRAGMENT
      
      
      
      uniform UnityPerMaterial 
          {
          
          #endif
          uniform float4 Texture2D_4972a32b9fed4a2b8180e52f9e0028d3_TexelSize;
          
          uniform float4 _BaseColor;
          
          uniform float Vector1_1d89e84864d54f5189c0732a74238096;
          
          uniform float Vector1_afb8382c98f844d580b89da5ae4dfa01;
          
          uniform float _NoiseScale;
          
          uniform float _DissolveThreshhold;
          
          uniform float _Thickness;
          
          uniform float4 _EdgeColor;
          
          #if HLSLCC_ENABLE_UNIFORM_BUFFERS
      };
      
      float4 u_xlat0_d;
      
      int u_xlatb0;
      
      float4 u_xlat1_d;
      
      float4 u_xlat2;
      
      float4 u_xlat3;
      
      float4 u_xlat4;
      
      float4 u_xlat5;
      
      float3 u_xlat16_6;
      
      float u_xlat7;
      
      float3 u_xlat8;
      
      float2 u_xlat15;
      
      float2 u_xlat16;
      
      float u_xlat22;
      
      float u_xlat16_27;
      
      OUT_Data_Frag frag(v2f in_f)
      {
          
          u_xlat0_d = in_f.texcoord2.xyxy * float4(float4(_NoiseScale, _NoiseScale, _NoiseScale, _NoiseScale));
          
          u_xlat1_d.xy = floor(u_xlat0_d.zw);
          
          u_xlat15.xy = u_xlat1_d.xy + float2(1.0, 1.0);
          
          u_xlat15.x = dot(u_xlat15.xy, float2(12.9898005, 78.2330017));
          
          u_xlat15.x = sin(u_xlat15.x);
          
          u_xlat15.x = u_xlat15.x * 43758.5469;
          
          u_xlat1_d.z = fract(u_xlat15.x);
          
          u_xlat2.xy = fract(u_xlat0_d.zw);
          
          u_xlat0_d = u_xlat0_d * float4(0.5, 0.5, 0.25, 0.25);
          
          u_xlat16.xy = u_xlat2.xy * u_xlat2.xy;
          
          u_xlat2.xy = (-u_xlat2.xy) * float2(2.0, 2.0) + float2(3.0, 3.0);
          
          u_xlat3.xy = u_xlat2.xy * u_xlat16.xy;
          
          u_xlat2.xy = (-u_xlat16.xy) * u_xlat2.xy + float2(1.0, 1.0);
          
          u_xlat4 = u_xlat1_d.xyxy + float4(1.0, 0.0, 0.0, 1.0);
          
          u_xlat1_d.x = dot(u_xlat1_d.xy, float2(12.9898005, 78.2330017));
          
          u_xlat1_d.x = sin(u_xlat1_d.x);
          
          u_xlat1_d.x = u_xlat1_d.x * 43758.5469;
          
          u_xlat8.x = dot(u_xlat4.zw, float2(12.9898005, 78.2330017));
          
          u_xlat8.z = dot(u_xlat4.xy, float2(12.9898005, 78.2330017));
          
          u_xlat8.xz = sin(u_xlat8.xz);
          
          u_xlat1_d.yw = u_xlat8.xz * float2(43758.5469, 43758.5469);
          
          u_xlat1_d.xyw = fract(u_xlat1_d.xyw);
          
          u_xlat15.xy = u_xlat1_d.zw * u_xlat3.xx;
          
          u_xlat1_d.x = u_xlat2.x * u_xlat1_d.x + u_xlat15.y;
          
          u_xlat8.x = u_xlat2.x * u_xlat1_d.y + u_xlat15.x;
          
          u_xlat8.x = u_xlat8.x * u_xlat3.y;
          
          u_xlat1_d.x = u_xlat2.y * u_xlat1_d.x + u_xlat8.x;
          
          u_xlat2 = floor(u_xlat0_d);
          
          u_xlat0_d = fract(u_xlat0_d);
          
          u_xlat3 = u_xlat2.xyxy + float4(1.0, 0.0, 0.0, 1.0);
          
          u_xlat8.x = dot(u_xlat3.zw, float2(12.9898005, 78.2330017));
          
          u_xlat8.y = dot(u_xlat3.xy, float2(12.9898005, 78.2330017));
          
          u_xlat8.xy = sin(u_xlat8.xy);
          
          u_xlat8.xy = u_xlat8.xy * float2(43758.5469, 43758.5469);
          
          u_xlat3 = u_xlat2 + float4(1.0, 1.0, 1.0, 0.0);
          
          u_xlat22 = dot(u_xlat3.xy, float2(12.9898005, 78.2330017));
          
          u_xlat3.x = dot(u_xlat3.zw, float2(12.9898005, 78.2330017));
          
          u_xlat3.x = sin(u_xlat3.x);
          
          u_xlat3.x = u_xlat3.x * 43758.5469;
          
          u_xlat3.x = fract(u_xlat3.x);
          
          u_xlat22 = sin(u_xlat22);
          
          u_xlat8.z = u_xlat22 * 43758.5469;
          
          u_xlat8.xyz = fract(u_xlat8.xyz);
          
          u_xlat4 = u_xlat0_d * u_xlat0_d;
          
          u_xlat0_d = (-u_xlat0_d) * float4(2.0, 2.0, 2.0, 2.0) + float4(3.0, 3.0, 3.0, 3.0);
          
          u_xlat5 = u_xlat0_d * u_xlat4;
          
          u_xlat0_d = (-u_xlat4) * u_xlat0_d + float4(1.0, 1.0, 1.0, 1.0);
          
          u_xlat22 = u_xlat8.z * u_xlat5.x;
          
          u_xlat8.x = u_xlat0_d.x * u_xlat8.x + u_xlat22;
          
          u_xlat22 = dot(u_xlat2.xy, float2(12.9898005, 78.2330017));
          
          u_xlat22 = sin(u_xlat22);
          
          u_xlat22 = u_xlat22 * 43758.5469;
          
          u_xlat22 = fract(u_xlat22);
          
          u_xlat8.xy = u_xlat8.xy * u_xlat5.yx;
          
          u_xlat0_d.x = u_xlat0_d.x * u_xlat22 + u_xlat8.y;
          
          u_xlat0_d.x = u_xlat0_d.y * u_xlat0_d.x + u_xlat8.x;
          
          u_xlat0_d.x = u_xlat0_d.x * 0.25;
          
          u_xlat0_d.x = u_xlat1_d.x * 0.125 + u_xlat0_d.x;
          
          u_xlat1_d = u_xlat2.zwzw + float4(0.0, 1.0, 1.0, 1.0);
          
          u_xlat7 = dot(u_xlat2.zw, float2(12.9898005, 78.2330017));
          
          u_xlat7 = sin(u_xlat7);
          
          u_xlat7 = u_xlat7 * 43758.5469;
          
          u_xlat7 = fract(u_xlat7);
          
          u_xlat1_d.z = dot(u_xlat1_d.zw, float2(12.9898005, 78.2330017));
          
          u_xlat1_d.x = dot(u_xlat1_d.xy, float2(12.9898005, 78.2330017));
          
          u_xlat1_d.xy = sin(u_xlat1_d.xz);
          
          u_xlat1_d.xy = u_xlat1_d.xy * float2(43758.5469, 43758.5469);
          
          u_xlat1_d.xy = fract(u_xlat1_d.xy);
          
          u_xlat8.x = u_xlat1_d.y * u_xlat5.z;
          
          u_xlat1_d.x = u_xlat0_d.z * u_xlat1_d.x + u_xlat8.x;
          
          u_xlat1_d.x = u_xlat1_d.x * u_xlat5.w;
          
          u_xlat8.x = u_xlat3.x * u_xlat5.z;
          
          u_xlat7 = u_xlat0_d.z * u_xlat7 + u_xlat8.x;
          
          u_xlat7 = u_xlat0_d.w * u_xlat7 + u_xlat1_d.x;
          
          u_xlat0_d.x = u_xlat7 * 0.5 + u_xlat0_d.x;
          
          u_xlat0_d.x = u_xlat0_d.x + (-_DissolveThreshhold);
          
          #ifdef UNITY_ADRENO_ES3
          u_xlatb0 = (u_xlat0_d.x<0.0);
          
          #else
          u_xlatb0 = u_xlat0_d.x<0.0;
          
          #endif
          if(u_xlatb0)
      {
              discard;
      }
          
          u_xlat0_d.x = dot(in_f.texcoord.xyz, in_f.texcoord.xyz);
          
          u_xlat0_d.x = inversesqrt(u_xlat0_d.x);
          
          u_xlat0_d.xyz = u_xlat0_d.xxx * in_f.texcoord.xyz;
          
          u_xlat16_6.x = dot(abs(u_xlat0_d.xyz), float3(1.0, 1.0, 1.0));
          
          u_xlat16_6.x = float(1.0) / float(u_xlat16_6.x);
          
          u_xlat16_6.xyz = u_xlat0_d.xyz * u_xlat16_6.xxx;
          
          u_xlat16_27 = (-u_xlat16_6.x) * 0.5 + 0.5;
          
          u_xlat16_27 = u_xlat16_6.y * 0.5 + u_xlat16_27;
          
          #ifdef UNITY_ADRENO_ES3
          u_xlat16_27 = min(max(u_xlat16_27, 0.0), 1.0);
          
          #else
          u_xlat16_27 = clamp(u_xlat16_27, 0.0, 1.0);
          
          #endif
          #ifdef UNITY_ADRENO_ES3
          u_xlatb0 = (u_xlat16_6.z>=0.0);
          
          #else
          u_xlatb0 = u_xlat16_6.z>=0.0;
          
          #endif
          out_f.SV_TARGET0.y = u_xlat16_6.y + u_xlat16_6.x;
          
          u_xlat0_d.x = (u_xlatb0) ? u_xlat16_27 : (-u_xlat16_27);
          
          out_f.SV_TARGET0.x = u_xlat0_d.x;
          
          out_f.SV_TARGET0.zw = float2(0.0, 0.0);
          
          return;
      
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "Hidden/Shader Graph/FallbackError"
}
