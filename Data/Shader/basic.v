#version 330

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform mat4 dview;
uniform mat4 dprojection;
uniform mat4 bias;
layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 texCoord;

out vec2 texcoord;
out vec3 n;
out vec3 pos;
out vec4 shadowcoord;
void main()
{
	//Position
	vec4 v =  vec4(position,1.0);
	gl_Position = (projection * view * model) * v;

	//TexCoord
	texcoord = texCoord;

	//Shadow
	shadowcoord = (bias*(dprojection * dview * model)) * v;
	
	//Per pixel lighting
	n = normal * mat3(model);
	pos = position * mat3(model);
}