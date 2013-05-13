#version 150
#extension GL_ARB_explicit_attrib_location : enable

layout(location = 0) in vec3 position;

//uniform mat4 modelViewProjection;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main(){
	vec4 v =  vec4(position,1.0);
	gl_Position = (projection * view * model) * v;
}