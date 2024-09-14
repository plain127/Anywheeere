Shader "Unlit/Stencil_Obj"
{
	Properties {
		_Color("Main Color", Color) = (1,1,1,0)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

	SubShader {
		Tags {
			"RenderType" = "Opaque"
			"Queue" = "Geometry+1"
		}

		ColorMask RGB
		Cull Front
		ZTest Always

		Stencil {
			Ref 1
			Comp equal
			Pass keep
		}

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4 _Color;
			sampler2D _MainTex;

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			v2f vert(appdata v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target {
				fixed4 c = tex2D(_MainTex, i.uv) * _Color;
				return c;
			}

			ENDCG
		}
	}
}
