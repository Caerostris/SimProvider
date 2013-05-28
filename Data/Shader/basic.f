#version 150
#extension GL_ARB_explicit_attrib_location : enable

uniform sampler2D modelTexture;
uniform sampler2DArray shadowmap;
uniform vec3 lightdir;
uniform float lightstr;
uniform vec4 ambient;
uniform vec3 slices;

in vec2 texcoord;
in vec3 n;
in vec3 pos;
in vec3 shadowCoords[3];
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

float getShadowFac(){
	if (gl_FragCoord.z < slices.z){
		return 1.0;
	}
	int shadowmapIndex = 0;

	if(gl_FragCoord.z > slices.x){
		shadowmapIndex = 0;
		}
	else if(gl_FragCoord.z > slices.y){
		shadowmapIndex = 1;
		}
	else {
		shadowmapIndex = 2;
		}
	int index = shadowmapIndex;
	vec4 sc2;
	sc2.xyw = shadowCoords[index];
	sc2.z = index;
	float shadowfac = 1.0;
	if (sc2.x >= 0 && sc2.x <= 1 && sc2.y >= 0 && sc2.y <= 1){
			for (int i=0;i<9;i++){
				if (texture(shadowmap, vec3((sc2.xy + poissonDisk[i]/1000.0),sc2.z)).z + b  < sc2.w){
					shadowfac -= 0.07;
					}
			}
	}
	return shadowfac;
}

void main(){
	//float shadowfac = 1.0;
	//  if (shadowcoord.x >= 0 && shadowcoord.x <= 1 && shadowcoord.y >= 0 && shadowcoord.y <= 1){
	//  	for (int i=0;i<9;i++){
	//  		if (texture2D(shadowmap, shadowcoord.xy + poissonDisk[i]/1000.0).z + b  < shadowcoord.z){
	//  			shadowfac -= 0.07;
	//  			}
	//  	}
	//}
	float shadowfac = getShadowFac();
	float light = max(dot(-lightdir,n),0.0)*lightstr;
	vec4 color = texture(modelTexture,texcoord);
	FragColor = (color*light*shadowfac)+(color*ambient);
}