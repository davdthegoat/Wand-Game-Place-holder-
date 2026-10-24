Shader "Custom/PSXShader"
{
    SubShader{
        Tags{"RenderPipeline" = "UniversalPipeline"}


    
        Pass{
            Name "ForwardLit" //fordebugging
            Tags{"LightMode" = "UniversalForward"}

            HLSLPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes {
                float3 positionLS : POSITION;

            }

            struct Varyings {
                float4 positionCS : SV_POSITION;
            }

            void Vertex() {
                Varyings output;
                output.positionCS = Transform
            }

            float4 Fragment(Attributes input) : SV_Target {
                return float4(1, 0, 0, 1);
            }
            ENDHLSL
        }
    }

      
}
