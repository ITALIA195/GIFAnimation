#version 330 core

const vec3 TOP_COLOR = vec3(0.53f, 0.72f, 0.71f);
const vec3 LEFT_COLOR = vec3(0.90f, 0.88f, 0.69f);
const vec3 RIGHT_COLOR = vec3(0.25f, 0.32f, 0.51f);

const float minHeight = 0.2;
const float deltaHeight = 0.8;

uniform mat4 view;

layout (points) in;
layout (triangle_strip, max_vertices = 24) out;

out vec3 vColor;

void sendOffset(vec3 amount) {
    amount *= 0.5;
    gl_Position = view * (gl_in[0].gl_Position + vec4(amount, 0));
    EmitVertex();
}

void main() {
    // Top Face
    vColor = TOP_COLOR;
    sendOffset(vec3(+1.0, +1.0, +1.0));
    sendOffset(vec3(+1.0, +1.0, -1.0));
    sendOffset(vec3(-1.0, +1.0, +1.0));
    sendOffset(vec3(-1.0, +1.0, -1.0));
    
    EndPrimitive();
    
    // Bottom Face
    vColor = TOP_COLOR;
    sendOffset(vec3(-1.0, -1.0, +1.0));
    sendOffset(vec3(+1.0, -1.0, +1.0));
    sendOffset(vec3(-1.0, -1.0, -1.0));
    sendOffset(vec3(+1.0, -1.0, -1.0));

    EndPrimitive();

    // Left Face
    vColor = LEFT_COLOR;
    sendOffset(vec3(-1.0, +1.0, +1.0));
    sendOffset(vec3(-1.0, -1.0, +1.0));
    sendOffset(vec3(-1.0, +1.0, -1.0));
    sendOffset(vec3(-1.0, -1.0, -1.0));

    EndPrimitive();
    
    // Right Face
    vColor = LEFT_COLOR;
    sendOffset(vec3(+1.0, +1.0, -1.0));
    sendOffset(vec3(+1.0, -1.0, -1.0));
    sendOffset(vec3(+1.0, +1.0, +1.0));
    sendOffset(vec3(+1.0, -1.0, +1.0));

    EndPrimitive();
    
    // Front Face
    vColor = RIGHT_COLOR;
    sendOffset(vec3(-1.0, +1.0, +1.0));
    sendOffset(vec3(-1.0, -1.0, +1.0));
    sendOffset(vec3(+1.0, +1.0, +1.0));
    sendOffset(vec3(+1.0, -1.0, +1.0));

    EndPrimitive();

    // Back Face
    vColor = RIGHT_COLOR;
    sendOffset(vec3(-1.0, +1.0, -1.0));
    sendOffset(vec3(-1.0, -1.0, -1.0));
    sendOffset(vec3(+1.0, +1.0, -1.0));
    sendOffset(vec3(+1.0, -1.0, -1.0));

    EndPrimitive();
}
