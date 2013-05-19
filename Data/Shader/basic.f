#version 150
#extension GL_ARB_explicit_attrib_location : enable

uniform sampler2D texture;
uniform sampler2D shadowmap;
uniform vec3 lightdir;
uniform float lightstr;
uniform vec4 ambient;

in vec2 texcoord;
in vec3 n;
in vec3 pos;
in vec4 shadowcoord;
out vec4 FragColor;

vec2 poissonDisk[9] = vec2[](
  vec2( -1, -1 ),
  vec2( 0, -1 ),
  vec2( 1, -1 ),
  vec2( -1, 0 ),
  vec2( 0, 0 ),
  vec2( 1, 0 ),
  vec2( -1, 1 ),
  vec2( 0, 1 ),
  vec2( 1, 1 )
);
	float b = 0.001;

void main(){
	float shadowfac = 1.0;
	if (shadowcoord.x >= 0 && shadowcoord.x <= 1 && shadowcoord.y >= 0 && shadowcoord.y <= 1){
	for (int i=0;i<9;i++){
		if (texture2D(shadowmap, shadowcoord.xy + poissonDisk[i]/1000.0).z + b  < shadowcoord.z){
			shadowfac -= 0.07;
			}
	}
	}
	float light = max(dot(-lightdir,n),0.0)*lightstr;

	FragColor = (texture2D(texture,texcoord)*light*shadowfac)+(ambient*texture2D(texture,texcoord));
}