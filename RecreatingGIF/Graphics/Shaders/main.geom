#version 330 core

const vec3 TOP_COLOR = vec3(0.52f, 0.73f, 0.71f);
const vec3 LEFT_COLOR = vec3(0.25f, 0.33f, 0.52f);
const vec3 RIGHT_COLOR = vec3(0.90f, 0.89f, 0.69f);

const float maxDistance = 100;
const float minHeight = 2;
const float deltaHeight = 4;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform vec3 centerOfAnimation;
uniform float animation;

layout (points) in;
layout (triangle_strip, max_vertices = 24) out;

out vec3 vColor;


void sendOffset(vec3 amount) {
    float d = distance(centerOfAnimation, gl_in[0].gl_Position.xyz);
    float ratio = d / maxDistance;
    
    float multY = (1 - sin(animation)) * deltaHeight + minHeight; 
    
    vec4 vertex = gl_in[0].gl_Position;
    vertex += vec4(amount, 0);
    vertex *= vec4(1, multY, 1, 1);
    gl_Position = projection * view * model * vertex;
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
    vColor = RIGHT_COLOR;
    sendOffset(vec3(-1.0, +1.0, +1.0));
    sendOffset(vec3(-1.0, -1.0, +1.0));
    sendOffset(vec3(-1.0, +1.0, -1.0));
    sendOffset(vec3(-1.0, -1.0, -1.0));

    EndPrimitive();
    
    // Right Face
    vColor = RIGHT_COLOR;
    sendOffset(vec3(+1.0, +1.0, -1.0));
    sendOffset(vec3(+1.0, -1.0, -1.0));
    sendOffset(vec3(+1.0, +1.0, +1.0));
    sendOffset(vec3(+1.0, -1.0, +1.0));

    EndPrimitive();
    
    // Front Face
    vColor = LEFT_COLOR;
    sendOffset(vec3(-1.0, +1.0, +1.0));
    sendOffset(vec3(-1.0, -1.0, +1.0));
    sendOffset(vec3(+1.0, +1.0, +1.0));
    sendOffset(vec3(+1.0, -1.0, +1.0));

    EndPrimitive();

    // Back Face
    vColor = LEFT_COLOR;
    sendOffset(vec3(-1.0, +1.0, -1.0));
    sendOffset(vec3(-1.0, -1.0, -1.0));
    sendOffset(vec3(+1.0, +1.0, -1.0));
    sendOffset(vec3(+1.0, -1.0, -1.0));

    EndPrimitive();
}
