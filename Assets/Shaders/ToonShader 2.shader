Shader "Unlit/ToonShader2"
{
    Properties
    {
        _MainTex("Texture",2D)="white"{}
        _PeelNoise("PeelNoise",2D)="white"{}
        _PeelTex("PeelTexture",2D)="white"{}
        _PeelAmount ("PeelAmount", Range(0, 1)) = 0.05
        _BaseColor ("BaseColor", Color) = (1,1,1,1)
        _PeelColor ("PeelColor", Color) = (1,1,1,1)
        _RangeVal ("ShadowRange1", Range(-1, 1)) = 0.05
        _ShadowPower1("ShadowStrenght1", Range(0, 1)) = 0.5
        _RangeVal2 ("ShadowRange2", Range(-1, 1)) = 0.05
        _ShadowPower2("ShadowStrenght2", Range(0., 1)) = 0.5
        _BorderColor ("BorderColor", Color) = (1,1,1,1)
        _BorderWidth ("BorderWidth", Range(0., .01)) = 0.005
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
                float2 uv: TEXCOORD0;
            };

            struct v2f
            {
             float4 vertex : SV_POSITION;
             float3 worldNormal : TEXCOORD1;
             float2 uv : TEXCOORD0;
            };

            
            float4 _BaseColor;
            float _RangeVal;
            float _RangeVal2;
            float _ShadowPower1;
            float _ShadowPower2;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _PeelTex;
            float4 _PeelTex_ST;
            float _PeelAmount;
            sampler2D _PeelNoise;
            float4 _PeelNoise_ST;
            float4 _PeelColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal=UnityObjectToWorldNormal(v.normal);
                 o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float AngleLight=dot(normalize(i.worldNormal),normalize(_WorldSpaceLightPos0.xyz));


                fixed4 col = _BaseColor;

                col *= tex2D(_MainTex, i.uv);

                if(tex2D(_PeelNoise, i.uv).r<=_PeelAmount)
                col = tex2D(_PeelTex, i.uv)*_PeelColor;

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
