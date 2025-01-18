Shader "Custom/EdgeJitter"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _JitterStrength ("Jitter Strength", Float) = 0.1
        _JitterSpeed ("Jitter Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _JitterStrength;
            float _JitterSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                float jitter = sin(_Time.y * _JitterSpeed) * _JitterStrength;
                v.vertex.xy += jitter * normalize(v.vertex.xy);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}