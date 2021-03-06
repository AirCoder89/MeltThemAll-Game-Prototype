// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32901,y:32697,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32235,y:32601,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1cb215b55ba8f414fa6be5602f01b288,ntxv:0,isnm:False|UVIN-1342-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32657,y:32768,varname:node_2393,prsc:2|A-6166-OUT,B-2053-RGB,C-797-RGB,D-9248-OUT,E-84-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:84,x:32507,y:32949,varname:node_84,prsc:2|A-6074-A,B-797-A;n:type:ShaderForge.SFN_Time,id:3682,x:31055,y:32616,varname:node_3682,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:6053,x:31011,y:32850,ptovrint:False,ptlb:x speed,ptin:_xspeed,varname:node_6053,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:2235,x:31011,y:32951,ptovrint:False,ptlb:y speed,ptin:_yspeed,varname:node_2235,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:1507,x:31185,y:32769,varname:node_1507,prsc:2|A-6053-OUT,B-2235-OUT;n:type:ShaderForge.SFN_Multiply,id:7702,x:31403,y:32703,varname:node_7702,prsc:2|A-3682-T,B-1507-OUT;n:type:ShaderForge.SFN_TexCoord,id:6826,x:31403,y:32505,varname:node_6826,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:1342,x:31622,y:32608,varname:node_1342,prsc:2|A-6826-UVOUT,B-7702-OUT;n:type:ShaderForge.SFN_Tex2d,id:6625,x:32235,y:32216,ptovrint:False,ptlb:node_6625,ptin:_node_6625,varname:node_6625,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4258,x:32235,y:32398,ptovrint:False,ptlb:node_4258,ptin:_node_4258,varname:node_4258,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-1342-OUT;n:type:ShaderForge.SFN_Multiply,id:6166,x:32499,y:32483,varname:node_6166,prsc:2|A-4258-RGB,B-6074-RGB;proporder:6074-797-6053-2235-4258;pass:END;sub:END;*/

Shader "Shader Forge/LazerBeam" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _xspeed ("x speed", Float ) = 0
        _yspeed ("y speed", Float ) = 0
        _node_4258 ("node_4258", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _node_4258; uniform float4 _node_4258_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _TintColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _xspeed)
                UNITY_DEFINE_INSTANCED_PROP( float, _yspeed)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
////// Lighting:
////// Emissive:
                float4 node_3682 = _Time;
                float _xspeed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _xspeed );
                float _yspeed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _yspeed );
                float2 node_1342 = (i.uv0+(node_3682.g*float2(_xspeed_var,_yspeed_var)));
                float4 _node_4258_var = tex2D(_node_4258,TRANSFORM_TEX(node_1342, _node_4258));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_1342, _MainTex));
                float4 _TintColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _TintColor );
                float3 emissive = ((_node_4258_var.rgb*_MainTex_var.rgb)*i.vertexColor.rgb*_TintColor_var.rgb*2.0*(_MainTex_var.a*_TintColor_var.a));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5,0.5,0.5,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
