#version 330

uniform mat4 modelViewProjection;
layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 texCoord;

out vec2 texcoord;
out vec3 n;
void main()
{
	vec4 v =  vec4(position,1.0);
	gl_Position = modelViewProjection * v;
	texcoord = texCoord;
	n = normal;
}