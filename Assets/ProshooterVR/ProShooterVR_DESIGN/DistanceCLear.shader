Shader "Proshooter/DistanceCLear"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _DistanceThreshold("Distance Threshold", Range(0.1, 2000)) = 1.0
        _SharpenAmount("Sharpen Amount", Range(0.1, 2000)) = 1.0
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }

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
                    float3 worldPos : TEXCOORD1;
                };

                sampler2D _MainTex;
                float _DistanceThreshold;
                float _SharpenAmount;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float dist = length(i.worldPos);
                    float sharpenFactor = saturate((_DistanceThreshold - dist) * _SharpenAmount);

                    fixed4 col = tex2D(_MainTex, i.uv);
                    col.rgb += (col.rgb - 0.5) * sharpenFactor;
                    return col;
                }

                ENDCG
            }
        }
}
