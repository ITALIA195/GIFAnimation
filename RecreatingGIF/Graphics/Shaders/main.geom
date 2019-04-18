#version 330 core

const float d = 0.1;
const float minHeight = 0.2;
const float deltaHeight = 0.8;
const vec3 lightDirection = normalize(vec3(1, 1, 1));

uniform mat4 view;

layout (points) in;
layout (triangle_strip, max_vertices = 24) out;

out vec3 vColor;

void sendOffset(vec3 amount) {
    gl_Position = view * (gl_in[0].gl_Position + vec4(amount, 0));
    EmitVertex();
}

void calculateColor(vec3 color, vec3 faceNormal) {
    float brightness = max(dot(-lightDirection, faceNormal), 0.3);
    vColor = color * brightness;
}

void main() {
    // Top Face

    calculateColor(vec3(0.53f, 0.72f, 0.71f), vec3(0, 1, 0));
    sendOffset(vec3(+d, +d, +d));
    sendOffset(vec3(+d, +d, -d));
    sendOffset(vec3(-d, +d, +d));
    sendOffset(vec3(-d, +d, -d));
    
    EndPrimitive();
    
    // Bottom Face
    calculateColor(vec3(0.53f, 0.72f, 0.71f), vec3(0, 1, 0));
    sendOffset(vec3(-d, -d, +d));
    sendOffset(vec3(+d, -d, +d));
    sendOffset(vec3(-d, -d, -d));
    sendOffset(vec3(+d, -d, -d));

    EndPrimitive();

    // Left Face
    calculateColor(vec3(0.90f, 0.88f, 0.69f), vec3(-1, 0, 0));
    sendOffset(vec3(-d, +d, +d));
    sendOffset(vec3(-d, -d, +d));
    sendOffset(vec3(-d, +d, -d));
    sendOffset(vec3(-d, -d, -d));

    EndPrimitive();
    
    // Right Face
    calculateColor(vec3(0.90f, 0.88f, 0.69f), vec3(1, 0, 0));
    sendOffset(vec3(+d, +d, -d));
    sendOffset(vec3(+d, -d, -d));
    sendOffset(vec3(+d, +d, +d));
    sendOffset(vec3(+d, -d, +d));

    EndPrimitive();
    
    // Front Face
    calculateColor(vec3(0.25f, 0.32f, 0.51f), vec3(0, 0, 1));
    sendOffset(vec3(-d, +d, +d));
    sendOffset(vec3(-d, -d, +d));
    sendOffset(vec3(+d, +d, +d));
    sendOffset(vec3(+d, -d, +d));

    EndPrimitive();

    // Back Face
    calculateColor(vec3(0.25f, 0.32f, 0.51f), vec3(0, 0, -1));
    sendOffset(vec3(-d, +d, -d));
    sendOffset(vec3(-d, -d, -d));
    sendOffset(vec3(+d, +d, -d));
    sendOffset(vec3(+d, -d, -d));

    EndPrimitive();
}
