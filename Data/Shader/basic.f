#version 330
uniform sampler2D texture;
uniform vec3 lightsrc;
uniform float lightstr;

in vec2 texcoord;
in vec3 n;
in vec3 pos;
out vec4 FragColor;
void main(){
	vec3 lightdir = normalize(pos - lightsrc);
	float light =max(dot(-lightdir,n),0.0)*lightstr;
	FragColor = texture2D(texture,texcoord)*light;
}