#version 330
uniform sampler2D tex;

in vec2 tc;
out vec4 color;
void main(){
	vec4 c = texture2D(tex,tc);
	color = c;
}