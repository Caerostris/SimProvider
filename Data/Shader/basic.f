#version 330
uniform sampler2D texture;
uniform vec4 lightsrc;
uniform float lightstr;

in vec2 texcoord;
in vec3 n;
out vec4 FragColor;
void main(){
	FragColor = texture2D(texture,texcoord)*max(dot(lightsrc.xyz,n),0.0)*lightstr/2;
}