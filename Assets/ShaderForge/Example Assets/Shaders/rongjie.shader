// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32878,y:32713,varname:node_3138,prsc:2|normal-8996-RGB,custl-3172-OUT,clip-8433-OUT;n:type:ShaderForge.SFN_Tex2d,id:3776,x:32424,y:33085,ptovrint:False,ptlb:GradualaTex,ptin:_GradualaTex,varname:node_3776,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9649a9467bc4e884a8234f157f8185dd,ntxv:0,isnm:False|UVIN-4631-OUT;n:type:ShaderForge.SFN_Tex2d,id:3756,x:32454,y:32660,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_3756,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8b768351d6ca0e04da707ff6e68917b0,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:3140,x:32315,y:32814,ptovrint:False,ptlb:node_3140,ptin:_node_3140,varname:node_3140,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7573529,c2:0.7573529,c3:0.7573529,c4:1;n:type:ShaderForge.SFN_Multiply,id:3172,x:32659,y:33015,varname:node_3172,prsc:2|A-3756-RGB,B-3140-RGB,C-3776-RGB,D-2583-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2583,x:32424,y:33371,ptovrint:False,ptlb:Value,ptin:_Value,varname:node_2583,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Append,id:4631,x:32206,y:33061,varname:node_4631,prsc:2|A-2828-OUT,B-2828-OUT;n:type:ShaderForge.SFN_OneMinus,id:2828,x:32020,y:33090,varname:node_2828,prsc:2|IN-1267-OUT;n:type:ShaderForge.SFN_Clamp01,id:1267,x:31847,y:32983,varname:node_1267,prsc:2|IN-499-OUT;n:type:ShaderForge.SFN_RemapRange,id:499,x:31684,y:32983,varname:node_499,prsc:2,frmn:0,frmx:1,tomn:-4,tomx:4|IN-5349-OUT;n:type:ShaderForge.SFN_Slider,id:3121,x:31058,y:33377,ptovrint:False,ptlb:node_3121,ptin:_node_3121,varname:node_3121,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5555556,max:1;n:type:ShaderForge.SFN_RemapRange,id:3340,x:31105,y:33048,varname:node_3340,prsc:2,frmn:0,frmx:1,tomn:0.2,tomx:1.5|IN-3121-OUT;n:type:ShaderForge.SFN_OneMinus,id:2941,x:31332,y:33093,varname:node_2941,prsc:2|IN-3340-OUT;n:type:ShaderForge.SFN_Tex2d,id:8578,x:31391,y:33411,ptovrint:False,ptlb:NoiseTex,ptin:_NoiseTex,varname:node_8578,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:5349,x:31599,y:33306,varname:node_5349,prsc:2|A-2941-OUT,B-8578-R;n:type:ShaderForge.SFN_Set,id:3525,x:31899,y:33436,varname:OpacityCilp,prsc:2|IN-5349-OUT;n:type:ShaderForge.SFN_Get,id:8433,x:32696,y:33266,varname:node_8433,prsc:2|IN-3525-OUT;n:type:ShaderForge.SFN_Tex2d,id:8996,x:32673,y:32744,ptovrint:False,ptlb:node_8996,ptin:_node_8996,varname:node_8996,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1dd58eb7de3366f4cbd74334586986d8,ntxv:3,isnm:False;proporder:3776-3756-3140-2583-3121-8578-8996;pass:END;sub:END;*/

Shader "Shader Forge/rongjie" {
    Properties {
        _GradualaTex ("GradualaTex", 2D) = "white" {}
        _MainTex ("MainTex", 2D) = "white" {}
        _node_3140 ("node_3140", Color) = (0.7573529,0.7573529,0.7573529,1)
        _Value ("Value", Float ) = 4
        _node_3121 ("node_3121", Range(0, 1)) = 0.5555556
        _NoiseTex ("NoiseTex", 2D) = "white" {}
        _node_8996 ("node_8996", 2D) = "bump" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GradualaTex; uniform float4 _GradualaTex_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _node_3140;
            uniform float _Value;
            uniform float _node_3121;
            uniform sampler2D _NoiseTex; uniform float4 _NoiseTex_ST;
            uniform sampler2D _node_8996; uniform float4 _node_8996_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float3 tangentDir : TEXCOORD2;
                float3 bitangentDir : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float4 _node_8996_var = tex2D(_node_8996,TRANSFORM_TEX(i.uv0, _node_8996));
                float3 normalLocal = _node_8996_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _NoiseTex_var = tex2D(_NoiseTex,TRANSFORM_TEX(i.uv0, _NoiseTex));
                float node_5349 = ((1.0 - (_node_3121*1.3+0.2))+_NoiseTex_var.r);
                float OpacityCilp = node_5349;
                clip(OpacityCilp - 0.5);
////// Lighting:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_2828 = (1.0 - saturate((node_5349*8.0+-4.0)));
                float2 node_4631 = float2(node_2828,node_2828);
                float4 _GradualaTex_var = tex2D(_GradualaTex,TRANSFORM_TEX(node_4631, _GradualaTex));
                float3 finalColor = (_MainTex_var.rgb*_node_3140.rgb*_GradualaTex_var.rgb*_Value);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _node_3121;
            uniform sampler2D _NoiseTex; uniform float4 _NoiseTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _NoiseTex_var = tex2D(_NoiseTex,TRANSFORM_TEX(i.uv0, _NoiseTex));
                float node_5349 = ((1.0 - (_node_3121*1.3+0.2))+_NoiseTex_var.r);
                float OpacityCilp = node_5349;
                clip(OpacityCilp - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
