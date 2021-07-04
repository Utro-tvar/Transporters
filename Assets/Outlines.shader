Shader "Unlit/Outlines"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //[HDR]_OutlineColor ("Outline Color", Color) = (1, 1, 1, 1)
        _OutlineWidth ("Outline Width", Range(0, 0.05)) = 1
    }
        SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

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
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;

            fixed4 _OutlineColor;
            float _OutlineWidth;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            float GetOffsetAlpha(float2 uv) 
            {
                float y = (_MainTex_TexelSize.z / _MainTex_TexelSize.w) * _OutlineWidth;
                float result = 0;

                float2 sUV = uv + float2(_OutlineWidth, y);
                result = max(result, tex2D(_MainTex, sUV).a);
                sUV = uv + float2(_OutlineWidth, -y);
                result = max(result, tex2D(_MainTex, sUV).a);
                sUV = uv + float2(-_OutlineWidth, y);
                result = max(result, tex2D(_MainTex, sUV).a);
                sUV = uv + float2(-_OutlineWidth, -y);
                result = max(result, tex2D(_MainTex, sUV).a);

                return result;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                //col *= i.color;

                col.rgb = lerp(i.color, col.rgb, col.a);
                //col.rgb = lerp(_OutlineColor, col.rgb, col.a);
                col.a = GetOffsetAlpha(i.uv);

                return col;
            }
            ENDCG
        }
    }
}
