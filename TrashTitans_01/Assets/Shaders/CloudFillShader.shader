Shader "Unlit/CloudFillShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Phase1 ("Phase 1", Color) = (1,1,1,1) //wit
        _Phase2 ("Phase 2", Color) = (1,1,0,1) //geel
        _Phase3 ("Phase 3",Color) = (1,0,0,1) //rood
        _PhaseState ("Phase State", Range(0,2)) = 0 //start
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            uniform sampler2D _MainTex;
            uniform float4 _Phase1;
            uniform float4 _Phase2;
            uniform float4 _Phase3;
            uniform float _PhaseState;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                
                half4 col = tex2D(_MainTex, i.uv);
                
                if(col.a > 0.1)
                {
                    if(_PhaseState == 0)
                    {
                        col = _Phase1;
                        
                    }
                    else if(_PhaseState == 1)
                    {
                        col = _Phase2;
                    }
                    else if(_PhaseState == 2)
                    {
                        col = _Phase3;
                    }
                }
                return col; //return de waarde terug
            }
            ENDCG
        }
    }
}
