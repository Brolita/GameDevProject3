Shader "Custom/SaturationOpacity" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Saturation ("Satration", range(0,64)) = 1
		_Alpha ("Opacity", range(0,1.0)) = 1
		_NormalMap ("Normalmap", 2D) = "bump" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		
		Pass {
		LOD 300
		
		CGPROGRAM
		#pragma surface surf SimpleSpecular alpha
		#pragma target 3.0
	

		sampler2D _MainTex;
		sampler2D _NormalMap;
		float _Saturation;
		float _Alpha;
		
		half4 LightingSimpleSpecular (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
	        half3 h = normalize (lightDir + viewDir);

	        half diff = max (0, dot (s.Normal, lightDir));

	        float nh = max (0, dot (s.Normal, h));
	        float spec = pow (nh, 48.0);

	        half4 c;
	        c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * (atten * 2);
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
	} 
	
	FallBack "Diffuse"
	
}
