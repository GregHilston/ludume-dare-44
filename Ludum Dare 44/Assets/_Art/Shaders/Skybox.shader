// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TheCosmicWhale/Skybox"
{
    Properties
    {
        _Color1 ("Color 1", Color) = (1, 1, 1, 0)
        _Color2 ("Color 2", Color) = (1, 1, 1, 0)
        _TargetVector ("Target Vector", Vector) = (0, 1, 0, 0)
        _Power ("Power", Float) = 1.0
    }

    CGINCLUDE



    ENDCG

    SubShader
    {
        Tags { "RenderType"="Background" "Queue"="Background" }
        Pass
        {
            ZWrite Off
            Cull Off
            Fog { Mode Off }
            
            CGPROGRAM
            
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma fragment frag
            #pragma vertex vert

            struct appdata
            {
                float4 position : POSITION;
                float3 texcoord : TEXCOORD0;
            };
            
            struct v2f
            {
                float4 position : SV_POSITION;
                float3 texcoord : TEXCOORD0;
            };
            
            half4 _Color1;
            half4 _Color2;
            half4 _TargetVector;
            half _Power;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos (v.position);
                o.texcoord = v.texcoord;
                return o;
            }
            
            fixed4 frag (v2f i) : COLOR
            {
                half d = dot (normalize (i.texcoord), _TargetVector) * 0.5f + 0.5f;
                d= saturate(d);
                return lerp (_Color1, _Color2, saturate(pow (d, _Power)));
            }
            
            ENDCG
        }
    }
}