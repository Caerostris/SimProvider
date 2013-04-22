#version 110
attribute vec3 position;
attribute vec3 normal;

varying vec2 texcoord;

void main()
{
	gl_Position = vec4(position,1.0);
	texcoord = position.xy;
}