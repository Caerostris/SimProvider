#version 330

layout(location = 0) out float depth;
float linearize(float depth) {
    float zNear = 0.1;
    float zFar = 100.0;

    return (2.0 * zNear) / (zFar + zNear - depth * (zFar - zNear));
}
void main(){
	depth =  gl_FragCoord.z * 888;
}