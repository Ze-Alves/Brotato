Shader "Unlit/ToonShader"
{
    Properties
    {
        _BaseColor ("BaseColor", Color) = (1,1,1,1)
        _RangeVal ("ShadowRange1", Range(-1, 1)) = 0.05
        _ShadowPower1("ShadowStrenght1", Range(0, 1)) = 0.5
        _RangeVal2 ("ShadowRange2", Range(-1, 1)) = 0.05
        _ShadowPower2("ShadowStrenght2", Range(0., 1)) = 0.5
        _BorderColor ("BorderColor", Color) = (1,1,1,1)
        _BorderWidth ("BorderWidth", Range(0., .02)) = 0.005
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        Pass
        {
        Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
             float4 vertex : SV_POSITION;
            };

            float4 _BorderColor;
            float _BorderWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex+_BorderWidth*normalize(v.normal));
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _BorderColor;
            }
            ENDCG
        }

        Pass
        {
        Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
             float4 vertex : SV_POSITION;
             float3 worldNormal : TEXCOORD0;
            };

            
            float4 _BaseColor;
            float _RangeVal;
            float _RangeVal2;
            float _ShadowPower1;
            float _ShadowPower2;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal=UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float AngleLight=dot(normalize(i.worldNormal),normalize(_WorldSpaceLightPos0.xyz));
                fixed4 col = _BaseColor;

                if(AngleLight<_RangeVal )
                col*= 1-_ShadowPower1;
                if(AngleLight<_RangeVal2)
                col*= 1-_ShadowPower2;

                return col;
            }
            ENDCG
        }
    }
}
