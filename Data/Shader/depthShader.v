#version 150
#extension GL_ARB_explicit_attrib_location : enable

layout(location = 0) in vec3 position;
layout(location = 2) in vec2 texCoord;

//uniform mat4 modelViewProjection;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec2 texcoord;

void main(){
	texcoord = texCoord;
	gl_Position = (projection * view * model) * vec4(position,1.0);
}