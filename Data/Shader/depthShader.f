#version 150
#extension GL_ARB_explicit_attrib_location : enable

uniform sampler2D modelTexture;
in vec2 texcoord;

out float depth;
void main(){
	if(texture(modelTexture,texcoord).w < 0.5)
		discard;
	else{
		depth = gl_FragCoord.z;
		}

}