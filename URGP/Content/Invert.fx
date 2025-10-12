// Invert.fx
sampler2D TextureSampler : register(s0);

float4 MainPS(float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR
{
    float4 tex = tex2D(TextureSampler, texCoord) * color;
    tex.rgb = 1.0 - tex.rgb;
    return tex;
}

technique Invert
{
    pass P0
    {
        PixelShader = compile ps_3_0 MainPS();
    }
}
