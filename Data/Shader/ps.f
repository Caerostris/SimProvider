﻿#version 150
#extension GL_ARB_explicit_attrib_location : enable

uniform sampler2DArray tex;
uniform int texZ;

in vec2 tc;
out vec4 color;
void main(){
	vec4 c = texture(tex,vec3(tc,texZ));
	color = c;
}