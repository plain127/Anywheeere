Shader "Custom/Stencil"
{
 
    SubShader
    {
        Tags { 
               "RenderType" = "Opaque"
               "RenderPipeline" = "UniversalPipeline"
            }

        Zwrite off
        ColorMask 0

        Stencil {
            Ref 1
            Comp always
            Pass replace
            }

        Pass {
            Zwrite off
            Cull off
            }
      


    }
}
