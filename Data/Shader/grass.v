#version 150
#extension GL_ARB_explicit_attrib_location : enable

uniform mat4 modelViewProjection;
uniform mat4 model;
uniform mat4 bias;
uniform mat4 shadowMat0;
uniform mat4 shadowMat1;
uniform mat4 shadowMat2;
layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 texCoord;

out vec2 texcoord;
out vec3 n;
out vec3 pos;
out vec3 shadowCoords[3];
void main()
{
	//Position
	vec4 v = vec4(position,1.0);
	gl_Position = (modelViewProjection) * v;

	//TexCoord
	texcoord = texCoord;
	
	//Per pixel lighting
	n =  mat3(model) * normal;

	//Shadow
	shadowCoords = vec3[]((shadowMat0 * v).xyz,(shadowMat1 * v).xyz,(shadowMat2 * v).xyz);
}