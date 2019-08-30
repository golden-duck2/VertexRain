Shader "Custom/VertexRain" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (0,0,0,1.0) // 雨の色
        _Speed ("Speed", Float) = 6.0 
        _Scale ("Scale", Float) = 4.0
        
        _X_Offset ("X Offset", Range(-0.5,0.5)) = 0
        _Z_Offset ("Z Offset", Range(-0.5,0.5)) = 0
    }
    SubShader {
        Tags {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }
        CGPROGRAM
        #pragma surface surf Lambert alpha
        #pragma vertex vert

        sampler2D _MainTex;

        fixed4 _Color;
        half _Speed;
        half _Scale;
        half _X_Offset;
        half _Z_Offset;
        
        struct Input {
            float2 uv_MainTex;
        };
        
        float rand(float3 co)
        {
            return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 53.539))) * 43758.5453);
        }

        float clampRange(float t, float a, float b){
            return clamp((t-a)/(b-a), 0, 1);
        }

        // 頂点シェーダー関数
        void vert(inout appdata_full v) {
            fixed rnd = rand(fmod(v.vertex.z, 512.0));
            float timer = _Time.w * _Speed * clampRange(0.7, 1.0, rnd);
            v.vertex.y -= fmod(-v.vertex.y + timer, _Scale) + v.vertex.y - _Scale * 0.5;
            v.vertex.x += _X_Offset;
            v.vertex.z += _Z_Offset;
        }

        void surf(Input IN, inout SurfaceOutput o) {
            half4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb * _Color.rgb;
            o.Alpha  = _Color.a;
        }
        ENDCG
    }
}