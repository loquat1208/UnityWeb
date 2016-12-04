Shader "Custom/RingShader" {
	Properties {
	    // properties for water shader
	    _MainTex ("Texture", 2D) = "white" { }
	    _InnerRadius ("Inner Radius", Range (0.0, 1.0)) = 0.3 // sliders
	    _OuterRadius ("Outer Radius", Range (0.0, 1.0)) = 0.2 // sliders
	    _Angle ("Angle (Float)", Range ( -3.15, 3.15 )) = 0
		_Color ("Tint", Color) = (1.0, 0.6, 0.6, 1.0)
	} 
	
	SubShader {
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _InnerRadius;
			uniform float _OuterRadius;
			uniform float _Angle;
			uniform fixed4 _Color;
		
			struct v2f {
			    float4  pos : SV_POSITION;
			    float2  uv : TEXCOORD0;
			};
			
			float4 _MainTex_ST;
			
			v2f vert (appdata_base v)
			{
			    v2f o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			    o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
			    return o;
			}
			
			half4 frag (v2f i) : COLOR
			{
		    	    half4 texcol = float4( 1, 1, 1, 1);
		    	    float dist = distance(i.uv, float2(0.5,0.5));
		    	    float4 result = tex2D(_MainTex, i.uv);
					float angle = atan2(i.uv.x-0.5 , i.uv.y-0.5);

		    	    if(dist < _InnerRadius) {
		    		clip(-1);
		    	    } else if(dist < _OuterRadius) {
		    	    if ( angle > _Angle ) {
		    	    	texcol.a = 0;
		    	    }
		    	    } else {
		    		clip(-1.0);
		    	    }
		    	
		    	    return texcol * _Color;
			}

			ENDCG
		}
	}
}