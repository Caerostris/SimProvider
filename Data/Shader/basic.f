#version 330
uniform sampler2D texture;
uniform sampler2D shadowmap;
uniform vec3 lightsrc;
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

void main(){
	float b = 0.005;
	b = clamp(b, 0,0.01);
	float shadowfac = 1.0;
	for (int i=0;i<9;i++){
		if (texture2D(shadowmap, shadowcoord.xy + poissonDisk[i]/1000.0).z + b  < shadowcoord.z){
			shadowfac -= 0.07;
			}
	}
	vec3 lightdir = normalize(pos - lightsrc);
	float light = max(dot(-lightdir,n),0.0)*lightstr;
	FragColor = texture2D(texture,texcoord)*light*shadowfac;//+ambient*texture2D(texture,texcoord))/2;
}