#version 150
#extension GL_ARB_explicit_attrib_location : enable

uniform sampler2D modelTexture;

in vec2 tc;
out vec4 color;

void main(){
	color = texture(modelTexture,tc);
}