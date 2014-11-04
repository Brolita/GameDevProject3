Shader "Custom/SaturationOpacity" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Saturation ("Saturation", range(0,64)) = 1
		_Alpha ("Opacity", range(0,1.0)) = 1
	}
	SubShader {
	
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		

		LOD 300
		
		CGPROGRAM
		#pragma surface surf Simple alpha
		#pragma target 3.0
	
		float _Alpha;
		float _Saturation;
		sampler2D _MainTex;
		sampler2D _NormalMap;

		
		
		half4 LightingSimple (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
	
	        half4 c;
	        c.rgb = (s.Albedo);
	        c.a = s.Alpha;
	        return c;
    	}


		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};
		

		void surf (Input IN, inout SurfaceOutput o) {
		
		
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			
			
			float greyScale = dot(c.rgb, fixed3(.222, .707, .071));  // Convert to greyscale
			c.rgb  = lerp(float3(greyScale, greyScale, greyScale), c.rgb, _Saturation);
			
			o.Normal = UnpackNormal (tex2D (_NormalMap, IN.uv_BumpMap));
			o.Albedo = c.rgb;
			o.Alpha = _Alpha;
		}
		ENDCG
	}
	
	
	FallBack "Diffuse"
	
}
