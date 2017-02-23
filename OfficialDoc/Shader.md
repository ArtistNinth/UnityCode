## Shader ##
### Writing Surface Shaders ###
#### 默认 ####
    Shader "Custom/NewSurfaceShader" {
        Properties {
            _Color ("Color", Color) = (1,1,1,1)
            _MainTex ("Albedo (RGB)", 2D) = "white" {}
            _Glossiness ("Smoothness", Range(0,1)) = 0.5
            _Metallic ("Metallic", Range(0,1)) = 0.0
        }
        SubShader {
            Tags { "RenderType"="Opaque" }
            LOD 200
            
            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;

            struct Input {
                float2 uv_MainTex;
            };

            half _Glossiness;
            half _Metallic;
            fixed4 _Color;

            void surf (Input IN, inout SurfaceOutputStandard o) {
                // Albedo comes from a texture tinted by color
                fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c.a;
            }
            ENDCG
        }
        FallBack "Diffuse"
    }


#### 输出 ####
    struct SurfaceOutput
    {
        fixed3 Albedo;  // diffuse color
        fixed3 Normal;  // tangent space normal, if written
        fixed3 Emission;
        half Specular;  // specular power in 0..1 range
        fixed Gloss;    // specular intensity
        fixed Alpha;    // alpha for transparencies
    };

    struct SurfaceOutputStandard
    {
        fixed3 Albedo;      // base (diffuse or specular) color
        fixed3 Normal;      // tangent space normal, if written
        half3 Emission;
        half Metallic;      // 0=non-metal, 1=metal
        half Smoothness;    // 0=rough, 1=smooth
        half Occlusion;     // occlusion (default 1)
        fixed Alpha;        // alpha for transparencies
    };

    struct SurfaceOutputStandardSpecular
    {
        fixed3 Albedo;      // diffuse color
        fixed3 Specular;    // specular color
        fixed3 Normal;      // tangent space normal, if written
        half3 Emission;
        half Smoothness;    // 0=rough, 1=smooth
        half Occlusion;     // occlusion (default 1)
        fixed Alpha;        // alpha for transparencies
    };

#### 例子 ####
##### Simple #####
    Shader "Example/Diffuse Simple" {
        SubShader {
            Tags { "RenderType" = "Opaque" }
            CGPROGRAM
            #pragma surface surf Lambert
            struct Input {
                float4 color : COLOR;
            };
            void surf (Input IN, inout SurfaceOutput o) {
                o.Albedo = 1;
            }
            ENDCG
        }
        Fallback "Diffuse"
    }
    
![](https://docs.unity3d.com/uploads/Main/SurfaceShaderSimple.png)

##### Texture #####
    Shader "Example/Diffuse Texture" {
        Properties {
            _MainTex ("Texture", 2D) = "white" {}
        }
        SubShader {
            Tags { "RenderType" = "Opaque" }
            CGPROGRAM
            #pragma surface surf Lambert
            struct Input {
                float2 uv_MainTex;
            };
            sampler2D _MainTex;
            void surf (Input IN, inout SurfaceOutput o) {
                o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
            }
            ENDCG
        } 
        Fallback "Diffuse"
    }
    
![](https://docs.unity3d.com/uploads/Main/SurfaceShaderDiffuseTex.png)
