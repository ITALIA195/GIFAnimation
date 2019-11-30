#version 330 core

layout(location = 0) in vec2 position; 

void main() {
    gl_Position = vec4(position.x, 0.0, position.y, 1.0);
}
