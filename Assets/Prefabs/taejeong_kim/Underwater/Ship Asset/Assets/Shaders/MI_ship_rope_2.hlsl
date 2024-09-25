#define NUM_TEX_COORD_INTERPOLATORS 1
#define NUM_MATERIAL_TEXCOORDS_VERTEX 1
#define NUM_CUSTOM_VERTEX_INTERPOLATORS 0

struct Input
{
	//float3 Normal;
	float2 uv_MainTex : TEXCOORD0;
	float2 uv2_Material_Texture2D_0 : TEXCOORD1;
	float4 color : COLOR;
	float4 tangent;
	//float4 normal;
	float3 viewDir;
	float4 screenPos;
	float3 worldPos;
	//float3 worldNormal;
	float3 normal2;
};
struct SurfaceOutputStandard
{
	float3 Albedo;		// base (diffuse or specular) color
	float3 Normal;		// tangent space normal, if written
	half3 Emission;
	half Metallic;		// 0=non-metal, 1=metal
	// Smoothness is the user facing name, it should be perceptual smoothness but user should not have to deal with it.
	// Everywhere in the code you meet smoothness it is perceptual smoothness
	half Smoothness;	// 0=rough, 1=smooth
	half Occlusion;		// occlusion (default 1)
	float Alpha;		// alpha for transparencies
};

//#define HDRP 1
#define URP 1
#define UE5
//#define HAS_CUSTOMIZED_UVS 1
#define MATERIAL_TANGENTSPACENORMAL 1
//struct Material
//{
	//samplers start
SAMPLER( SamplerState_Linear_Repeat );
SAMPLER( SamplerState_Linear_Clamp );
TEXTURE2D(       Material_Texture2D_0 );
SAMPLER(  samplerMaterial_Texture2D_0 );
float4 Material_Texture2D_0_TexelSize;
float4 Material_Texture2D_0_ST;
TEXTURE2D(       Material_Texture2D_1 );
SAMPLER(  samplerMaterial_Texture2D_1 );
float4 Material_Texture2D_1_TexelSize;
float4 Material_Texture2D_1_ST;
TEXTURE2D(       Material_Texture2D_2 );
SAMPLER(  samplerMaterial_Texture2D_2 );
float4 Material_Texture2D_2_TexelSize;
float4 Material_Texture2D_2_ST;
TEXTURE2D(       Material_Texture2D_3 );
SAMPLER(  samplerMaterial_Texture2D_3 );
float4 Material_Texture2D_3_TexelSize;
float4 Material_Texture2D_3_ST;
TEXTURE2D(       Material_Texture2D_4 );
SAMPLER(  samplerMaterial_Texture2D_4 );
float4 Material_Texture2D_4_TexelSize;
float4 Material_Texture2D_4_ST;
TEXTURE2D(       Material_Texture2D_5 );
SAMPLER(  samplerMaterial_Texture2D_5 );
float4 Material_Texture2D_5_TexelSize;
float4 Material_Texture2D_5_ST;
TEXTURE2D(       Material_Texture2D_6 );
SAMPLER(  samplerMaterial_Texture2D_6 );
float4 Material_Texture2D_6_TexelSize;
float4 Material_Texture2D_6_ST;
TEXTURE2D(       Material_Texture2D_7 );
SAMPLER(  samplerMaterial_Texture2D_7 );
float4 Material_Texture2D_7_TexelSize;
float4 Material_Texture2D_7_ST;
TEXTURE2D(       Material_Texture2D_8 );
SAMPLER(  samplerMaterial_Texture2D_8 );
float4 Material_Texture2D_8_TexelSize;
float4 Material_Texture2D_8_ST;
TEXTURE2D(       Material_Texture2D_9 );
SAMPLER(  samplerMaterial_Texture2D_9 );
float4 Material_Texture2D_9_TexelSize;
float4 Material_Texture2D_9_ST;
TEXTURE2D(       Material_Texture2D_10 );
SAMPLER(  samplerMaterial_Texture2D_10 );
float4 Material_Texture2D_10_TexelSize;
float4 Material_Texture2D_10_ST;
TEXTURE2D(       Material_Texture2D_11 );
SAMPLER(  samplerMaterial_Texture2D_11 );
float4 Material_Texture2D_11_TexelSize;
float4 Material_Texture2D_11_ST;
TEXTURE2D(       Material_Texture2D_12 );
SAMPLER(  samplerMaterial_Texture2D_12 );
float4 Material_Texture2D_12_TexelSize;
float4 Material_Texture2D_12_ST;
TEXTURE2D(       Material_Texture2D_13 );
SAMPLER(  samplerMaterial_Texture2D_13 );
float4 Material_Texture2D_13_TexelSize;
float4 Material_Texture2D_13_ST;

//};

#ifdef UE5
	#define UE_LWC_RENDER_TILE_SIZE			2097152.0
	#define UE_LWC_RENDER_TILE_SIZE_SQRT	1448.15466
	#define UE_LWC_RENDER_TILE_SIZE_RSQRT	0.000690533954
	#define UE_LWC_RENDER_TILE_SIZE_RCP		4.76837158e-07
	#define UE_LWC_RENDER_TILE_SIZE_FMOD_PI		0.673652053
	#define UE_LWC_RENDER_TILE_SIZE_FMOD_2PI	0.673652053
	#define INVARIANT(X) X
	#define PI 					(3.1415926535897932)

	#include "LargeWorldCoordinates.hlsl"
#endif
struct MaterialStruct
{
	float4 PreshaderBuffer[25];
	float4 ScalarExpressions[1];
	float VTPackedPageTableUniform[2];
	float VTPackedUniform[1];
};
static SamplerState View_MaterialTextureBilinearWrapedSampler;
static SamplerState View_MaterialTextureBilinearClampedSampler;
struct ViewStruct
{
	float GameTime;
	float RealTime;
	float DeltaTime;
	float PrevFrameGameTime;
	float PrevFrameRealTime;
	float MaterialTextureMipBias;	
	float4 PrimitiveSceneData[ 40 ];
	float4 TemporalAAParams;
	float2 ViewRectMin;
	float4 ViewSizeAndInvSize;
	float MaterialTextureDerivativeMultiply;
	uint StateFrameIndexMod8;
	float FrameNumber;
	float2 FieldOfViewWideAngles;
	float4 RuntimeVirtualTextureMipLevel;
	float PreExposure;
	float4 BufferBilinearUVMinMax;
};
struct ResolvedViewStruct
{
	#ifdef UE5
		FLWCVector3 WorldCameraOrigin;
		FLWCVector3 PrevWorldCameraOrigin;
		FLWCVector3 PreViewTranslation;
		FLWCVector3 WorldViewOrigin;
	#else
		float3 WorldCameraOrigin;
		float3 PrevWorldCameraOrigin;
		float3 PreViewTranslation;
		float3 WorldViewOrigin;
	#endif
	float4 ScreenPositionScaleBias;
	float4x4 TranslatedWorldToView;
	float4x4 TranslatedWorldToCameraView;
	float4x4 TranslatedWorldToClip;
	float4x4 ViewToTranslatedWorld;
	float4x4 PrevViewToTranslatedWorld;
	float4x4 CameraViewToTranslatedWorld;
	float4 BufferBilinearUVMinMax;
	float4 XRPassthroughCameraUVs[ 2 ];
};
struct PrimitiveStruct
{
	float4x4 WorldToLocal;
	float4x4 LocalToWorld;
};

static ViewStruct View;
static ResolvedViewStruct ResolvedView;
static PrimitiveStruct Primitive;
uniform float4 View_BufferSizeAndInvSize;
uniform float4 LocalObjectBoundsMin;
uniform float4 LocalObjectBoundsMax;
static SamplerState Material_Wrap_WorldGroupSettings;
static SamplerState Material_Clamp_WorldGroupSettings;

#include "UnrealCommon.cginc"

static MaterialStruct Material;
void InitializeExpressions()
{
	Material.PreshaderBuffer[0] = float4(1.000000,-0.000000,0.000000,1.000000);//(Unknown)
	Material.PreshaderBuffer[1] = float4(0.000000,0.000000,30.000000,30.000000);//(Unknown)
	Material.PreshaderBuffer[2] = float4(1.000000,1.000000,-0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[3] = float4(0.000000,1.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[4] = float4(1.000000,1.000000,30.000000,1.000000);//(Unknown)
	Material.PreshaderBuffer[5] = float4(1.000000,1.000000,1.000000,2.000000);//(Unknown)
	Material.PreshaderBuffer[6] = float4(-1.000000,30.000000,1.000000,2.000000);//(Unknown)
	Material.PreshaderBuffer[7] = float4(-1.000000,1.000000,30.000000,1.000000);//(Unknown)
	Material.PreshaderBuffer[8] = float4(2.000000,-1.000000,1.000000,2.000000);//(Unknown)
	Material.PreshaderBuffer[9] = float4(-1.000000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[10] = float4(0.000000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[11] = float4(0.000000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[12] = float4(0.332000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[13] = float4(1.000000,1.000000,1.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[14] = float4(0.685514,-0.984276,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[15] = float4(1.000000,1.000000,1.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[16] = float4(0.368000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[17] = float4(1.000000,1.000000,1.000000,-6.250117);//(Unknown)
	Material.PreshaderBuffer[18] = float4(0.232000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[19] = float4(1.000000,1.000000,1.000000,1.000000);//(Unknown)
	Material.PreshaderBuffer[20] = float4(1.000000,1.000000,1.000000,0.150000);//(Unknown)
	Material.PreshaderBuffer[21] = float4(0.200000,0.000000,1.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[22] = float4(1.000000,0.000000,1.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[23] = float4(1.000000,0.000000,0.000000,1.000000);//(Unknown)
	Material.PreshaderBuffer[24] = float4(1.000000,1.000000,1.000000,0.000000);//(Unknown)
}float3 GetMaterialWorldPositionOffset(FMaterialVertexParameters Parameters)
{
	return 0.00000000.rrr;;
}
void CalcPixelMaterialInputs(in out FMaterialPixelParameters Parameters, in out FPixelMaterialInputs PixelMaterialInputs)
{
	//WorldAligned texturing & others use normals & stuff that think Z is up
	Parameters.TangentToWorld[0] = Parameters.TangentToWorld[0].xzy;
	Parameters.TangentToWorld[1] = Parameters.TangentToWorld[1].xzy;
	Parameters.TangentToWorld[2] = Parameters.TangentToWorld[2].xzy;

	float3 WorldNormalCopy = Parameters.WorldNormal;

	// Initial calculations (required for Normal)
	MaterialFloat2 Local0 = Parameters.TexCoords[0].xy;
	MaterialFloat2 Local1 = (MaterialFloat2(-0.50000000,-0.50000000) + DERIV_BASE_VALUE(Local0));
	MaterialFloat Local2 = dot(DERIV_BASE_VALUE(Local1),Material.PreshaderBuffer[0].xy);
	MaterialFloat Local3 = dot(DERIV_BASE_VALUE(Local1),Material.PreshaderBuffer[0].zw);
	MaterialFloat2 Local4 = MaterialFloat2(DERIV_BASE_VALUE(Local2),DERIV_BASE_VALUE(Local3));
	MaterialFloat2 Local5 = (MaterialFloat2(0.50000000,0.50000000) + DERIV_BASE_VALUE(Local4));
	MaterialFloat2 Local6 = (DERIV_BASE_VALUE(Local5) + Material.PreshaderBuffer[1].xy);
	MaterialFloat2 Local7 = (DERIV_BASE_VALUE(Local6) * Material.PreshaderBuffer[1].zw);
	MaterialFloat2 Local8 = ddy((float2)DERIV_BASE_VALUE(Local7));
	MaterialFloat2 Local9 = ddx((float2)DERIV_BASE_VALUE(Local7));
	MaterialFloat2 Local10 = (DERIV_BASE_VALUE(Local9) * ((MaterialFloat2)View.MaterialTextureDerivativeMultiply));
	MaterialFloat2 Local11 = (DERIV_BASE_VALUE(Local8) * ((MaterialFloat2)View.MaterialTextureDerivativeMultiply));
	MaterialFloat Local12 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local7), 2);
	MaterialFloat4 Local13 = UnpackNormalMap(Texture2DSampleGrad(Material_Texture2D_0,samplerMaterial_Texture2D_0,DERIV_BASE_VALUE(Local7),DERIV_BASE_VALUE(Local10),DERIV_BASE_VALUE(Local11)));
	MaterialFloat Local14 = MaterialStoreTexSample(Parameters, Local13, 2);
	MaterialFloat Local15 = (Local13.r * Material.PreshaderBuffer[2].x);
	MaterialFloat Local16 = (Local13.g * Material.PreshaderBuffer[2].x);
	MaterialFloat Local17 = dot(DERIV_BASE_VALUE(Local1),Material.PreshaderBuffer[2].yz);
	MaterialFloat Local18 = dot(DERIV_BASE_VALUE(Local1),Material.PreshaderBuffer[3].xy);
	MaterialFloat2 Local19 = MaterialFloat2(DERIV_BASE_VALUE(Local17),DERIV_BASE_VALUE(Local18));
	MaterialFloat2 Local20 = (MaterialFloat2(0.50000000,0.50000000) + DERIV_BASE_VALUE(Local19));
	MaterialFloat2 Local21 = (DERIV_BASE_VALUE(Local20) + Material.PreshaderBuffer[3].zw);
	MaterialFloat2 Local22 = (DERIV_BASE_VALUE(Local21) * Material.PreshaderBuffer[4].xy);
	MaterialFloat2 Local23 = ddy((float2)DERIV_BASE_VALUE(Local22));
	MaterialFloat2 Local24 = ddx((float2)DERIV_BASE_VALUE(Local22));
	MaterialFloat2 Local25 = (DERIV_BASE_VALUE(Local22) * ((MaterialFloat2)Material.PreshaderBuffer[4].z));
	MaterialFloat2 Local26 = (DERIV_BASE_VALUE(Local24) * ((MaterialFloat2)View.MaterialTextureDerivativeMultiply));
	MaterialFloat2 Local27 = (DERIV_BASE_VALUE(Local23) * ((MaterialFloat2)View.MaterialTextureDerivativeMultiply));
	MaterialFloat Local28 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local25), 2);
	MaterialFloat4 Local29 = UnpackNormalMap(Texture2DSampleGrad(Material_Texture2D_1,samplerMaterial_Texture2D_1,DERIV_BASE_VALUE(Local25),DERIV_BASE_VALUE(Local26),DERIV_BASE_VALUE(Local27)));
	MaterialFloat Local30 = MaterialStoreTexSample(Parameters, Local29, 2);
	MaterialFloat Local31 = (Local29.r * Material.PreshaderBuffer[4].w);
	MaterialFloat Local32 = (Local29.g * Material.PreshaderBuffer[4].w);
	MaterialFloat Local33 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local0), 1);
	MaterialFloat4 Local34 = Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,DERIV_BASE_VALUE(Local0),View.MaterialTextureMipBias);
	MaterialFloat Local35 = MaterialStoreTexSample(Parameters, Local34, 1);
	MaterialFloat Local36 = (Local34.r * Material.PreshaderBuffer[5].x);
	MaterialFloat2 Local37 = (((MaterialFloat2)Material.PreshaderBuffer[5].y) * DERIV_BASE_VALUE(Local0));
	MaterialFloat Local38 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local37), 4);
	MaterialFloat4 Local39 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,DERIV_BASE_VALUE(Local37),View.MaterialTextureMipBias));
	MaterialFloat Local40 = MaterialStoreTexSample(Parameters, Local39, 4);
	MaterialFloat3 Local41 = (Local39.rgb * ((MaterialFloat3)Material.PreshaderBuffer[5].z));
	MaterialFloat3 Local42 = (((MaterialFloat3)Local34.r) + Local41);
	MaterialFloat3 Local43 = (((MaterialFloat3)Local36) * Local42);
	MaterialFloat Local44 = lerp(Material.PreshaderBuffer[6].x,Material.PreshaderBuffer[5].w,Local43.x);
	MaterialFloat Local45 = saturate(Local44);
	MaterialFloat Local46 = saturate(Local45.r);
	MaterialFloat3 Local47 = lerp(MaterialFloat3(MaterialFloat2(Local15,Local16),Local13.b),MaterialFloat3(MaterialFloat2(Local31,Local32),Local29.b),Local46);
	MaterialFloat2 Local48 = (DERIV_BASE_VALUE(Local22) * ((MaterialFloat2)Material.PreshaderBuffer[6].y));
	MaterialFloat Local49 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local48), 2);
	MaterialFloat4 Local50 = UnpackNormalMap(Texture2DSampleGrad(Material_Texture2D_4,samplerMaterial_Texture2D_4,DERIV_BASE_VALUE(Local48),DERIV_BASE_VALUE(Local26),DERIV_BASE_VALUE(Local27)));
	MaterialFloat Local51 = MaterialStoreTexSample(Parameters, Local50, 2);
	MaterialFloat Local52 = (Local50.r * Material.PreshaderBuffer[6].z);
	MaterialFloat Local53 = (Local50.g * Material.PreshaderBuffer[6].z);
	MaterialFloat Local54 = lerp(Material.PreshaderBuffer[7].x,Material.PreshaderBuffer[6].w,Local34.g);
	MaterialFloat Local55 = saturate(Local54);
	MaterialFloat Local56 = (Local55.r * Material.PreshaderBuffer[7].y);
	MaterialFloat Local57 = saturate(Local56);
	MaterialFloat3 Local58 = lerp(Local47,MaterialFloat3(MaterialFloat2(Local52,Local53),Local50.b),Local57);
	MaterialFloat2 Local59 = (DERIV_BASE_VALUE(Local22) * ((MaterialFloat2)Material.PreshaderBuffer[7].z));
	MaterialFloat Local60 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local59), 2);
	MaterialFloat4 Local61 = UnpackNormalMap(Texture2DSampleGrad(Material_Texture2D_5,samplerMaterial_Texture2D_5,DERIV_BASE_VALUE(Local59),DERIV_BASE_VALUE(Local26),DERIV_BASE_VALUE(Local27)));
	MaterialFloat Local62 = MaterialStoreTexSample(Parameters, Local61, 2);
	MaterialFloat Local63 = (Local61.r * Material.PreshaderBuffer[7].w);
	MaterialFloat Local64 = (Local61.g * Material.PreshaderBuffer[7].w);
	MaterialFloat Local65 = lerp(Material.PreshaderBuffer[8].y,Material.PreshaderBuffer[8].x,Local34.b);
	MaterialFloat Local66 = saturate(Local65);
	MaterialFloat Local67 = (Local66.r * Material.PreshaderBuffer[8].z);
	MaterialFloat Local68 = saturate(Local67);
	MaterialFloat3 Local69 = lerp(Local58,MaterialFloat3(MaterialFloat2(Local63,Local64),Local61.b),Local68);
	MaterialFloat Local70 = lerp(Material.PreshaderBuffer[9].x,Material.PreshaderBuffer[8].w,Local34.a);
	MaterialFloat Local71 = saturate(Local70);
	MaterialFloat Local72 = (Local71.r * Material.PreshaderBuffer[9].y);
	MaterialFloat Local73 = saturate(Local72);
	MaterialFloat3 Local74 = lerp(Local69,((MaterialFloat3)1.00000000),Local73);

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = Local74;


#if TEMPLATE_USES_SUBSTRATE
	Parameters.SubstratePixelFootprint = SubstrateGetPixelFootprint(Parameters.WorldPosition_CamRelative, GetRoughnessFromNormalCurvature(Parameters));
	Parameters.SharedLocalBases = SubstrateInitialiseSharedLocalBases();
	Parameters.SubstrateTree = GetInitialisedSubstrateTree();
#if SUBSTRATE_USE_FULLYSIMPLIFIED_MATERIAL == 1
	Parameters.SharedLocalBasesFullySimplified = SubstrateInitialiseSharedLocalBases();
	Parameters.SubstrateTreeFullySimplified = GetInitialisedSubstrateTree();
#endif
#endif

	// Note that here MaterialNormal can be in world space or tangent space
	float3 MaterialNormal = GetMaterialNormal(Parameters, PixelMaterialInputs);

#if MATERIAL_TANGENTSPACENORMAL

#if FEATURE_LEVEL >= FEATURE_LEVEL_SM4
	// Mobile will rely on only the final normalize for performance
	MaterialNormal = normalize(MaterialNormal);
#endif

	// normalizing after the tangent space to world space conversion improves quality with sheared bases (UV layout to WS causes shrearing)
	// use full precision normalize to avoid overflows
	Parameters.WorldNormal = TransformTangentNormalToWorld(Parameters.TangentToWorld, MaterialNormal);

#else //MATERIAL_TANGENTSPACENORMAL

	Parameters.WorldNormal = normalize(MaterialNormal);

#endif //MATERIAL_TANGENTSPACENORMAL

#if MATERIAL_TANGENTSPACENORMAL || TWO_SIDED_WORLD_SPACE_SINGLELAYERWATER_NORMAL
	// flip the normal for backfaces being rendered with a two-sided material
	Parameters.WorldNormal *= Parameters.TwoSidedSign;
#endif

	Parameters.ReflectionVector = ReflectionAboutCustomWorldNormal(Parameters, Parameters.WorldNormal, false);

#if !PARTICLE_SPRITE_FACTORY
	Parameters.Particle.MotionBlurFade = 1.0f;
#endif // !PARTICLE_SPRITE_FACTORY

	// Now the rest of the inputs
	MaterialFloat3 Local75 = lerp(Material.PreshaderBuffer[10].xyz,Material.PreshaderBuffer[11].xyz,Material.PreshaderBuffer[10].w);
	MaterialFloat Local76 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local7), 0);
	MaterialFloat4 Local77 = ProcessMaterialColorTextureLookup(Texture2DSample(Material_Texture2D_6,GetMaterialSharedSampler(samplerMaterial_Texture2D_6,View_MaterialTextureBilinearWrapedSampler),DERIV_BASE_VALUE(Local7)));
	MaterialFloat Local78 = MaterialStoreTexSample(Parameters, Local77, 0);
	MaterialFloat3 Local79 = (Local77.rgb * ((MaterialFloat3)Material.PreshaderBuffer[12].x));
	MaterialFloat Local80 = dot(Local79,MaterialFloat3(0.30000001,0.58999997,0.11000000));
	MaterialFloat3 Local81 = lerp(Local79,((MaterialFloat3)Local80),Material.PreshaderBuffer[12].y);
	MaterialFloat3 Local82 = RotateAboutAxis(MaterialFloat4(normalize(MaterialFloat3(1.00000000,1.00000000,1.00000000).rgb),Material.PreshaderBuffer[11].w),((MaterialFloat3)0.00000000),Local81);
	MaterialFloat3 Local83 = (Local82 + Local81);
	MaterialFloat3 Local84 = (Material.PreshaderBuffer[13].xyz * Local83);
	MaterialFloat Local85 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local25), 0);
	MaterialFloat4 Local86 = ProcessMaterialColorTextureLookup(Texture2DSample(Material_Texture2D_7,GetMaterialSharedSampler(samplerMaterial_Texture2D_7,View_MaterialTextureBilinearWrapedSampler),DERIV_BASE_VALUE(Local25)));
	MaterialFloat Local87 = MaterialStoreTexSample(Parameters, Local86, 0);
	MaterialFloat3 Local88 = (Local86.rgb * ((MaterialFloat3)Material.PreshaderBuffer[14].x));
	MaterialFloat Local89 = dot(Local88,MaterialFloat3(0.30000001,0.58999997,0.11000000));
	MaterialFloat3 Local90 = lerp(Local88,((MaterialFloat3)Local89),Material.PreshaderBuffer[14].y);
	MaterialFloat3 Local91 = RotateAboutAxis(MaterialFloat4(normalize(MaterialFloat3(1.00000000,1.00000000,1.00000000).rgb),Material.PreshaderBuffer[13].w),((MaterialFloat3)0.00000000),Local90);
	MaterialFloat3 Local92 = (Local91 + Local90);
	MaterialFloat3 Local93 = (Material.PreshaderBuffer[15].xyz * Local92);
	MaterialFloat3 Local94 = lerp(Local84,Local93,Local46);
	MaterialFloat Local95 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local48), 0);
	MaterialFloat4 Local96 = ProcessMaterialColorTextureLookup(Texture2DSample(Material_Texture2D_8,GetMaterialSharedSampler(samplerMaterial_Texture2D_8,View_MaterialTextureBilinearWrapedSampler),DERIV_BASE_VALUE(Local48)));
	MaterialFloat Local97 = MaterialStoreTexSample(Parameters, Local96, 0);
	MaterialFloat3 Local98 = (Local96.rgb * ((MaterialFloat3)Material.PreshaderBuffer[16].x));
	MaterialFloat Local99 = dot(Local98,MaterialFloat3(0.30000001,0.58999997,0.11000000));
	MaterialFloat3 Local100 = lerp(Local98,((MaterialFloat3)Local99),Material.PreshaderBuffer[16].y);
	MaterialFloat3 Local101 = RotateAboutAxis(MaterialFloat4(normalize(MaterialFloat3(1.00000000,1.00000000,1.00000000).rgb),Material.PreshaderBuffer[15].w),((MaterialFloat3)0.00000000),Local100);
	MaterialFloat3 Local102 = (Local101 + Local100);
	MaterialFloat3 Local103 = (Material.PreshaderBuffer[17].xyz * Local102);
	MaterialFloat3 Local104 = lerp(Local94,Local103,Local57);
	MaterialFloat Local105 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local59), 0);
	MaterialFloat4 Local106 = ProcessMaterialColorTextureLookup(Texture2DSample(Material_Texture2D_9,GetMaterialSharedSampler(samplerMaterial_Texture2D_9,View_MaterialTextureBilinearWrapedSampler),DERIV_BASE_VALUE(Local59)));
	MaterialFloat Local107 = MaterialStoreTexSample(Parameters, Local106, 0);
	MaterialFloat3 Local108 = (Local106.rgb * ((MaterialFloat3)Material.PreshaderBuffer[18].x));
	MaterialFloat Local109 = dot(Local108,MaterialFloat3(0.30000001,0.58999997,0.11000000));
	MaterialFloat3 Local110 = lerp(Local108,((MaterialFloat3)Local109),Material.PreshaderBuffer[18].y);
	MaterialFloat3 Local111 = RotateAboutAxis(MaterialFloat4(normalize(MaterialFloat3(1.00000000,1.00000000,1.00000000).rgb),Material.PreshaderBuffer[17].w),((MaterialFloat3)0.00000000),Local110);
	MaterialFloat3 Local112 = (Local111 + Local110);
	MaterialFloat3 Local113 = (Material.PreshaderBuffer[19].xyz * Local112);
	MaterialFloat3 Local114 = lerp(Local104,Local113,Local68);
	MaterialFloat3 Local115 = lerp(Local114,((MaterialFloat3)1.00000000),Local73);
	MaterialFloat Local116 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local7), 1);
	MaterialFloat4 Local117 = Texture2DSample(Material_Texture2D_10,GetMaterialSharedSampler(samplerMaterial_Texture2D_10,View_MaterialTextureBilinearWrapedSampler),DERIV_BASE_VALUE(Local7));
	MaterialFloat Local118 = MaterialStoreTexSample(Parameters, Local117, 1);
	MaterialFloat Local119 = (Local117.b * Material.PreshaderBuffer[19].w);
	MaterialFloat Local120 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local25), 1);
	MaterialFloat4 Local121 = Texture2DSample(Material_Texture2D_11,GetMaterialSharedSampler(samplerMaterial_Texture2D_11,View_MaterialTextureBilinearWrapedSampler),DERIV_BASE_VALUE(Local25));
	MaterialFloat Local122 = MaterialStoreTexSample(Parameters, Local121, 1);
	MaterialFloat Local123 = (Local121.b * Material.PreshaderBuffer[20].x);
	MaterialFloat Local124 = lerp(Local119,Local123,Local46);
	MaterialFloat Local125 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local48), 1);
	MaterialFloat4 Local126 = Texture2DSample(Material_Texture2D_12,GetMaterialSharedSampler(samplerMaterial_Texture2D_12,View_MaterialTextureBilinearWrapedSampler),DERIV_BASE_VALUE(Local48));
	MaterialFloat Local127 = MaterialStoreTexSample(Parameters, Local126, 1);
	MaterialFloat Local128 = PositiveClampedPow(Local126.b,Material.PreshaderBuffer[20].y);
	MaterialFloat Local129 = lerp(Local124,Local128,Local57);
	MaterialFloat Local130 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local59), 1);
	MaterialFloat4 Local131 = Texture2DSample(Material_Texture2D_13,GetMaterialSharedSampler(samplerMaterial_Texture2D_13,View_MaterialTextureBilinearWrapedSampler),DERIV_BASE_VALUE(Local59));
	MaterialFloat Local132 = MaterialStoreTexSample(Parameters, Local131, 1);
	MaterialFloat Local133 = PositiveClampedPow(Local131.b,Material.PreshaderBuffer[20].z);
	MaterialFloat Local134 = lerp(Local129,Local133,Local68);
	MaterialFloat Local135 = lerp(Local134,1.00000000,Local73);
	MaterialFloat Local136 = lerp(Material.PreshaderBuffer[21].y,Material.PreshaderBuffer[21].x,Local117.g);
	MaterialFloat Local137 = lerp(Material.PreshaderBuffer[21].w,Material.PreshaderBuffer[21].z,Local121.g);
	MaterialFloat Local138 = lerp(Local136,Local137,Local46);
	MaterialFloat Local139 = lerp(Material.PreshaderBuffer[22].y,Material.PreshaderBuffer[22].x,Local126.g);
	MaterialFloat Local140 = lerp(Local138,Local139,Local57);
	MaterialFloat Local141 = lerp(Material.PreshaderBuffer[22].w,Material.PreshaderBuffer[22].z,Local131.g);
	MaterialFloat Local142 = lerp(Local140,Local141,Local68);
	MaterialFloat Local143 = lerp(Local142,1.00000000,Local73);
	MaterialFloat Local144 = lerp(Local77.a,1.00000000,0.50000000);
	MaterialFloat Local145 = (Local144 * Material.PreshaderBuffer[23].x);
	MaterialFloat3 Local146 = (Local115 * ((MaterialFloat3)Material.PreshaderBuffer[23].y));
	MaterialFloat Local147 = PositiveClampedPow(Local117.r,Material.PreshaderBuffer[23].w);
	MaterialFloat Local148 = PositiveClampedPow(Local121.r,Material.PreshaderBuffer[24].x);
	MaterialFloat Local149 = lerp(Local147,Local148,Local46);
	MaterialFloat Local150 = PositiveClampedPow(Local126.r,Material.PreshaderBuffer[24].y);
	MaterialFloat Local151 = lerp(Local149,Local150,Local57);
	MaterialFloat Local152 = PositiveClampedPow(Local131.r,Material.PreshaderBuffer[24].z);
	MaterialFloat Local153 = lerp(Local151,Local152,Local68);
	MaterialFloat Local154 = lerp(Local153,1.00000000,Local73);

	PixelMaterialInputs.EmissiveColor = Local75;
	PixelMaterialInputs.Opacity = 1.00000000;
	PixelMaterialInputs.OpacityMask = Local145;
	PixelMaterialInputs.BaseColor = Local115;
	PixelMaterialInputs.Metallic = Local135;
	PixelMaterialInputs.Specular = Material.PreshaderBuffer[20].w;
	PixelMaterialInputs.Roughness = Local143;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Normal = Local74;
	PixelMaterialInputs.Tangent = MaterialFloat3(1.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Subsurface = MaterialFloat4(Local146,Material.PreshaderBuffer[23].z);
	PixelMaterialInputs.AmbientOcclusion = Local154;
	PixelMaterialInputs.Refraction = 0;
	PixelMaterialInputs.PixelDepthOffset = 0.00000000;
	PixelMaterialInputs.ShadingModel = 6;
	PixelMaterialInputs.FrontMaterial = GetInitialisedSubstrateData();
	PixelMaterialInputs.SurfaceThickness = 0.01000000;
	PixelMaterialInputs.Displacement = 0.50000000;


#if MATERIAL_USES_ANISOTROPY
	Parameters.WorldTangent = CalculateAnisotropyTangent(Parameters, PixelMaterialInputs);
#else
	Parameters.WorldTangent = 0;
#endif
}

#define UnityObjectToWorldDir TransformObjectToWorld

void SetupCommonData( int Parameters_PrimitiveId )
{
	View_MaterialTextureBilinearWrapedSampler = SamplerState_Linear_Repeat;
	View_MaterialTextureBilinearClampedSampler = SamplerState_Linear_Clamp;

	Material_Wrap_WorldGroupSettings = SamplerState_Linear_Repeat;
	Material_Clamp_WorldGroupSettings = SamplerState_Linear_Clamp;

	View.GameTime = View.RealTime = _Time.y;// _Time is (t/20, t, t*2, t*3)
	View.PrevFrameGameTime = View.GameTime - unity_DeltaTime.x;//(dt, 1/dt, smoothDt, 1/smoothDt)
	View.PrevFrameRealTime = View.RealTime;
	View.DeltaTime = unity_DeltaTime.x;
	View.MaterialTextureMipBias = 0.0;
	View.TemporalAAParams = float4( 0, 0, 0, 0 );
	View.ViewRectMin = float2( 0, 0 );
	View.ViewSizeAndInvSize = View_BufferSizeAndInvSize;
	View.MaterialTextureDerivativeMultiply = 1.0f;
	View.StateFrameIndexMod8 = 0;
	View.FrameNumber = (int)_Time.y;
	View.FieldOfViewWideAngles = float2( PI * 0.42f, PI * 0.42f );//75degrees, default unity
	View.RuntimeVirtualTextureMipLevel = float4( 0, 0, 0, 0 );
	View.PreExposure = 0;
	View.BufferBilinearUVMinMax = float4(
		View_BufferSizeAndInvSize.z * ( 0 + 0.5 ),//EffectiveViewRect.Min.X
		View_BufferSizeAndInvSize.w * ( 0 + 0.5 ),//EffectiveViewRect.Min.Y
		View_BufferSizeAndInvSize.z * ( View_BufferSizeAndInvSize.x - 0.5 ),//EffectiveViewRect.Max.X
		View_BufferSizeAndInvSize.w * ( View_BufferSizeAndInvSize.y - 0.5 ) );//EffectiveViewRect.Max.Y

	for( int i2 = 0; i2 < 40; i2++ )
		View.PrimitiveSceneData[ i2 ] = float4( 0, 0, 0, 0 );

	float4x4 LocalToWorld = transpose( UNITY_MATRIX_M );
    LocalToWorld[3] = float4(ToUnrealPos(LocalToWorld[3]), LocalToWorld[3].w);
	float4x4 WorldToLocal = transpose( UNITY_MATRIX_I_M );
	float4x4 ViewMatrix = transpose( UNITY_MATRIX_V );
	float4x4 InverseViewMatrix = transpose( UNITY_MATRIX_I_V );
	float4x4 ViewProjectionMatrix = transpose( UNITY_MATRIX_VP );
	uint PrimitiveBaseOffset = Parameters_PrimitiveId * PRIMITIVE_SCENE_DATA_STRIDE;
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 0 ] = LocalToWorld[ 0 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 1 ] = LocalToWorld[ 1 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 2 ] = LocalToWorld[ 2 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 3 ] = LocalToWorld[ 3 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 5 ] = float4( ToUnrealPos( SHADERGRAPH_OBJECT_POSITION ), 100.0 );//ObjectWorldPosition
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 6 ] = WorldToLocal[ 0 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 7 ] = WorldToLocal[ 1 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 8 ] = WorldToLocal[ 2 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 9 ] = WorldToLocal[ 3 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 10 ] = LocalToWorld[ 0 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 11 ] = LocalToWorld[ 1 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 12 ] = LocalToWorld[ 2 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 13 ] = LocalToWorld[ 3 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 18 ] = float4( ToUnrealPos( SHADERGRAPH_OBJECT_POSITION ), 0 );//ActorWorldPosition
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 19 ] = LocalObjectBoundsMax - LocalObjectBoundsMin;//ObjectBounds
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 21 ] = mul( LocalToWorld, float3( 1, 0, 0 ) );
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 23 ] = LocalObjectBoundsMin;//LocalObjectBoundsMin 
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 24 ] = LocalObjectBoundsMax;//LocalObjectBoundsMax

#ifdef UE5
	ResolvedView.WorldCameraOrigin = LWCPromote( ToUnrealPos( _WorldSpaceCameraPos.xyz ) );
	ResolvedView.PreViewTranslation = LWCPromote( float3( 0, 0, 0 ) );
	ResolvedView.WorldViewOrigin = LWCPromote( float3( 0, 0, 0 ) );
#else
	ResolvedView.WorldCameraOrigin = ToUnrealPos( _WorldSpaceCameraPos.xyz );
	ResolvedView.PreViewTranslation = float3( 0, 0, 0 );
	ResolvedView.WorldViewOrigin = float3( 0, 0, 0 );
#endif
	ResolvedView.PrevWorldCameraOrigin = ResolvedView.WorldCameraOrigin;
	ResolvedView.ScreenPositionScaleBias = float4( 1, 1, 0, 0 );
	ResolvedView.TranslatedWorldToView		 = ViewMatrix;
	ResolvedView.TranslatedWorldToCameraView = ViewMatrix;
	ResolvedView.TranslatedWorldToClip		 = ViewProjectionMatrix;
	ResolvedView.ViewToTranslatedWorld		 = InverseViewMatrix;
	ResolvedView.PrevViewToTranslatedWorld = ResolvedView.ViewToTranslatedWorld;
	ResolvedView.CameraViewToTranslatedWorld = InverseViewMatrix;
	ResolvedView.BufferBilinearUVMinMax = View.BufferBilinearUVMinMax;
	Primitive.WorldToLocal = WorldToLocal;
	Primitive.LocalToWorld = LocalToWorld;
}
#define VS_USES_UNREAL_SPACE 1
float3 PrepareAndGetWPO( float4 VertexColor, float3 UnrealWorldPos, float3 UnrealNormal, float4 InTangent,
						 float4 UV0, float4 UV1 )
{
	InitializeExpressions();
	FMaterialVertexParameters Parameters = (FMaterialVertexParameters)0;

	float3 InWorldNormal = UnrealNormal;
	float4 tangentWorld = InTangent;
	tangentWorld.xyz = normalize( tangentWorld.xyz );
	//float3x3 tangentToWorld = CreateTangentToWorldPerVertex( InWorldNormal, tangentWorld.xyz, tangentWorld.w );
	Parameters.TangentToWorld = float3x3( normalize( cross( InWorldNormal, tangentWorld.xyz ) * tangentWorld.w ), tangentWorld.xyz, InWorldNormal );

	
	#ifdef VS_USES_UNREAL_SPACE
		UnrealWorldPos = ToUnrealPos( UnrealWorldPos );
	#endif
	Parameters.WorldPosition = UnrealWorldPos;
	#ifdef VS_USES_UNREAL_SPACE
		Parameters.TangentToWorld[ 0 ] = Parameters.TangentToWorld[ 0 ].xzy;
		Parameters.TangentToWorld[ 1 ] = Parameters.TangentToWorld[ 1 ].xzy;
		Parameters.TangentToWorld[ 2 ] = Parameters.TangentToWorld[ 2 ].xzy;//WorldAligned texturing uses normals that think Z is up
	#endif

	Parameters.VertexColor = VertexColor;

#if NUM_MATERIAL_TEXCOORDS_VERTEX > 0			
	Parameters.TexCoords[ 0 ] = float2( UV0.x, UV0.y );
#endif
#if NUM_MATERIAL_TEXCOORDS_VERTEX > 1
	Parameters.TexCoords[ 1 ] = float2( UV1.x, UV1.y );
#endif
#if NUM_MATERIAL_TEXCOORDS_VERTEX > 2
	for( int i = 2; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
	{
		Parameters.TexCoords[ i ] = float2( UV0.x, UV0.y );
	}
#endif

	Parameters.PrimitiveId = 0;

	SetupCommonData( Parameters.PrimitiveId );

#ifdef UE5
	Parameters.PrevFrameLocalToWorld = MakeLWCMatrix( float3( 0, 0, 0 ), Primitive.LocalToWorld );
#else
	Parameters.PrevFrameLocalToWorld = Primitive.LocalToWorld;
#endif
	
	float3 Offset = float3( 0, 0, 0 );
	Offset = GetMaterialWorldPositionOffset( Parameters );
	#ifdef VS_USES_UNREAL_SPACE
		//Convert from unreal units to unity
		Offset /= float3( 100, 100, 100 );
		Offset = Offset.xzy;
	#endif
	return Offset;
}

void SurfaceReplacement( Input In, out SurfaceOutputStandard o )
{
	InitializeExpressions();

	float3 Z3 = float3( 0, 0, 0 );
	float4 Z4 = float4( 0, 0, 0, 0 );

	float3 UnrealWorldPos = float3( In.worldPos.x, In.worldPos.y, In.worldPos.z );

	float3 UnrealNormal = In.normal2;	

	FMaterialPixelParameters Parameters = (FMaterialPixelParameters)0;
#if NUM_TEX_COORD_INTERPOLATORS > 0			
	Parameters.TexCoords[ 0 ] = float2( In.uv_MainTex.x, 1.0 - In.uv_MainTex.y );
#endif
#if NUM_TEX_COORD_INTERPOLATORS > 1
	Parameters.TexCoords[ 1 ] = float2( In.uv2_Material_Texture2D_0.x, 1.0 - In.uv2_Material_Texture2D_0.y );
#endif
#if NUM_TEX_COORD_INTERPOLATORS > 2
	for( int i = 2; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
	{
		Parameters.TexCoords[ i ] = float2( In.uv_MainTex.x, 1.0 - In.uv_MainTex.y );
	}
#endif
	Parameters.VertexColor = In.color;
	Parameters.WorldNormal = UnrealNormal;
	Parameters.ReflectionVector = half3( 0, 0, 1 );
	Parameters.CameraVector = normalize( _WorldSpaceCameraPos.xyz - UnrealWorldPos.xyz );
	//Parameters.CameraVector = mul( ( float3x3 )unity_CameraToWorld, float3( 0, 0, 1 ) ) * -1;
	Parameters.LightVector = half3( 0, 0, 0 );
	//float4 screenpos = In.screenPos;
	//screenpos /= screenpos.w;
	Parameters.SvPosition = In.screenPos;
	Parameters.ScreenPosition = Parameters.SvPosition;

	Parameters.UnMirrored = 1;

	Parameters.TwoSidedSign = 1;


	float3 InWorldNormal = UnrealNormal;	
	float4 tangentWorld = In.tangent;
	tangentWorld.xyz = normalize( tangentWorld.xyz );
	//float3x3 tangentToWorld = CreateTangentToWorldPerVertex( InWorldNormal, tangentWorld.xyz, tangentWorld.w );
	Parameters.TangentToWorld = float3x3( normalize( cross( InWorldNormal, tangentWorld.xyz ) * tangentWorld.w ), tangentWorld.xyz, InWorldNormal );

	//WorldAlignedTexturing in UE relies on the fact that coords there are 100x larger, prepare values for that
	//but watch out for any computation that might get skewed as a side effect
	UnrealWorldPos = ToUnrealPos( UnrealWorldPos );
	
	Parameters.AbsoluteWorldPosition = UnrealWorldPos;
	Parameters.WorldPosition_CamRelative = UnrealWorldPos;
	Parameters.WorldPosition_NoOffsets = UnrealWorldPos;

	Parameters.WorldPosition_NoOffsets_CamRelative = Parameters.WorldPosition_CamRelative;
	Parameters.LightingPositionOffset = float3( 0, 0, 0 );

	Parameters.AOMaterialMask = 0;

	Parameters.Particle.RelativeTime = 0;
	Parameters.Particle.MotionBlurFade;
	Parameters.Particle.Random = 0;
	Parameters.Particle.Velocity = half4( 1, 1, 1, 1 );
	Parameters.Particle.Color = half4( 1, 1, 1, 1 );
	Parameters.Particle.TranslatedWorldPositionAndSize = float4( UnrealWorldPos, 0 );
	Parameters.Particle.MacroUV = half4( 0, 0, 1, 1 );
	Parameters.Particle.DynamicParameter = half4( 0, 0, 0, 0 );
	Parameters.Particle.LocalToWorld = float4x4( Z4, Z4, Z4, Z4 );
	Parameters.Particle.Size = float2( 1, 1 );
	Parameters.Particle.SubUVCoords[ 0 ] = Parameters.Particle.SubUVCoords[ 1 ] = float2( 0, 0 );
	Parameters.Particle.SubUVLerp = 0.0;
	Parameters.TexCoordScalesParams = float2( 0, 0 );
	Parameters.PrimitiveId = 0;
	Parameters.VirtualTextureFeedback = 0;

	FPixelMaterialInputs PixelMaterialInputs = (FPixelMaterialInputs)0;
	PixelMaterialInputs.Normal = float3( 0, 0, 1 );
	PixelMaterialInputs.ShadingModel = 0;
	PixelMaterialInputs.FrontMaterial = 0;

	SetupCommonData( Parameters.PrimitiveId );
	//CustomizedUVs
	#if NUM_TEX_COORD_INTERPOLATORS > 0 && HAS_CUSTOMIZED_UVS
		float2 OutTexCoords[ NUM_TEX_COORD_INTERPOLATORS ];
		//Prevent uninitialized reads
		for( int i = 0; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
		{
			OutTexCoords[ i ] = float2( 0, 0 );
		}
		GetMaterialCustomizedUVs( Parameters, OutTexCoords );
		for( int i = 0; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
		{
			Parameters.TexCoords[ i ] = OutTexCoords[ i ];
		}
	#endif
	//<-
	CalcPixelMaterialInputs( Parameters, PixelMaterialInputs );

	#define HAS_WORLDSPACE_NORMAL 0
	#if HAS_WORLDSPACE_NORMAL
		PixelMaterialInputs.Normal = mul( PixelMaterialInputs.Normal, (MaterialFloat3x3)( transpose( Parameters.TangentToWorld ) ) );
	#endif

	o.Albedo = PixelMaterialInputs.BaseColor.rgb;
	o.Alpha = PixelMaterialInputs.Opacity;
	if( PixelMaterialInputs.OpacityMask < 0.333 ) discard;

	o.Metallic = PixelMaterialInputs.Metallic;
	o.Smoothness = 1.0 - PixelMaterialInputs.Roughness;
	o.Normal = normalize( PixelMaterialInputs.Normal );
	o.Emission = PixelMaterialInputs.EmissiveColor.rgb;
	o.Occlusion = PixelMaterialInputs.AmbientOcclusion;

	//BLEND_ADDITIVE o.Alpha = ( o.Emission.r + o.Emission.g + o.Emission.b ) / 3;
}