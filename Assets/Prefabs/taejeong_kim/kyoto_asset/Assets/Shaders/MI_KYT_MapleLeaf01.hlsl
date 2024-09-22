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
	float4 PreshaderBuffer[7];
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
	Material.PreshaderBuffer[0] = float4(1.000000,1.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[1] = float4(0.000000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[2] = float4(1.000000,1.000000,1.000000,1.000000);//(Unknown)
	Material.PreshaderBuffer[3] = float4(0.500000,1.000000,0.000000,1.000000);//(Unknown)
	Material.PreshaderBuffer[4] = float4(0.400000,0.000000,0.500000,0.000000);//(Unknown)
	Material.PreshaderBuffer[5] = float4(1.000000,10.000000,3.000000,0.050000);//(Unknown)
	Material.PreshaderBuffer[6] = float4(2.000000,0.000000,1.000000,0.000000);//(Unknown)
}
{
	float4 Vectors[1];
};
//MPC_KYT_Rain
MaterialCollection0Type MaterialCollection0;
void Initialize_MaterialCollection0()
{
	MaterialCollection0.Vectors[0] = float4(0.000000,0.000000,0.000000,0.000000);//Rain,,,
}
struct MaterialCollection1Type
{
	float4 Vectors[1];
};
//MPC_KYT_Emissive
MaterialCollection1Type MaterialCollection1;
void Initialize_MaterialCollection1()
{
	MaterialCollection1.Vectors[0] = float4(0.000000,0.000000,0.000000,0.000000);//Emissive,,,
}
float3 GetMaterialWorldPositionOffset(FMaterialVertexParameters Parameters)
{
	MaterialFloat Local35 = (View.GameTime * Material.PreshaderBuffer[4].x);
	MaterialFloat Local36 = (Local35 * -0.50000000);
	MaterialFloat3 Local37 = (normalize(MaterialFloat4(0.00000000,1.00000000,0.00000000,1.00000000).rgba.rgb) * ((MaterialFloat3)Local36));
	FWSVector3 Local38 = GetWorldPosition(Parameters);
	FWSVector3 Local39 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local38)), WSGetY(DERIV_BASE_VALUE(Local38)), WSGetZ(DERIV_BASE_VALUE(Local38)));
	FWSVector3 Local40 = WSDivideByPow2(DERIV_BASE_VALUE(Local39), ((MaterialFloat3)1024.00000000));
	FWSVector3 Local41 = WSAdd(Local37, DERIV_BASE_VALUE(Local40));
	FWSVector3 Local42 = WSAdd(DERIV_BASE_VALUE(Local41), ((MaterialFloat3)0.50000000));
	MaterialFloat3 Local43 = WSFracDemote(DERIV_BASE_VALUE(Local42));
	MaterialFloat3 Local44 = (DERIV_BASE_VALUE(Local43) * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local45 = (DERIV_BASE_VALUE(Local44) + ((MaterialFloat3)-1.00000000));
	MaterialFloat3 Local46 = abs(DERIV_BASE_VALUE(Local45));
	MaterialFloat3 Local47 = (DERIV_BASE_VALUE(Local46) * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local48 = (((MaterialFloat3)3.00000000) - DERIV_BASE_VALUE(Local47));
	MaterialFloat3 Local49 = (DERIV_BASE_VALUE(Local48) * DERIV_BASE_VALUE(Local46));
	MaterialFloat3 Local50 = (DERIV_BASE_VALUE(Local49) * DERIV_BASE_VALUE(Local46));
	MaterialFloat Local51 = dot(normalize(MaterialFloat4(0.00000000,1.00000000,0.00000000,1.00000000).rgba.rgb),DERIV_BASE_VALUE(Local50));
	FWSVector3 Local52 = WSDivide(DERIV_BASE_VALUE(Local39), ((MaterialFloat3)200.00000000));
	FWSVector3 Local53 = WSAdd(((MaterialFloat3)Local36), DERIV_BASE_VALUE(Local52));
	FWSVector3 Local54 = WSAdd(DERIV_BASE_VALUE(Local53), ((MaterialFloat3)0.50000000));
	MaterialFloat3 Local55 = WSFracDemote(DERIV_BASE_VALUE(Local54));
	MaterialFloat3 Local56 = (DERIV_BASE_VALUE(Local55) * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local57 = (DERIV_BASE_VALUE(Local56) + ((MaterialFloat3)-1.00000000));
	MaterialFloat3 Local58 = abs(DERIV_BASE_VALUE(Local57));
	MaterialFloat3 Local59 = (DERIV_BASE_VALUE(Local58) * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local60 = (((MaterialFloat3)3.00000000) - DERIV_BASE_VALUE(Local59));
	MaterialFloat3 Local61 = (DERIV_BASE_VALUE(Local60) * DERIV_BASE_VALUE(Local58));
	MaterialFloat3 Local62 = (DERIV_BASE_VALUE(Local61) * DERIV_BASE_VALUE(Local58));
	MaterialFloat3 Local63 = (DERIV_BASE_VALUE(Local62) - ((MaterialFloat3)0.00000000));
	MaterialFloat Local64 = length(DERIV_BASE_VALUE(Local63));
	MaterialFloat Local65 = (DERIV_BASE_VALUE(Local51) + DERIV_BASE_VALUE(Local64));
	MaterialFloat Local66 = (DERIV_BASE_VALUE(Local65) * 6.28318548);
	MaterialFloat4 Local67 = MaterialFloat4(cross(normalize(MaterialFloat4(0.00000000,1.00000000,0.00000000,1.00000000).rgba.rgb),MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb),DERIV_BASE_VALUE(Local66));
	FWSVector3 Local68 = TransformLocalPositionToWorld(Parameters, MaterialFloat3(0.00000000,0.00000000,0.00000000).rgb);
	FWSVector3 Local69 = WSAdd(Local68, Material.PreshaderBuffer[4].yzw);
	FWSVector3 Local70 = WSSubtract(DERIV_BASE_VALUE(Local39), Local69);
	MaterialFloat3 Local71 = WSDemote(DERIV_BASE_VALUE(Local70));
	MaterialFloat Local72 = length(DERIV_BASE_VALUE(Local71));
	MaterialFloat Local73 = (DERIV_BASE_VALUE(Local72) / 256.00000000);
	FWSVector3 Local74 = WSAdd(((MaterialFloat3)View.GameTime), GetObjectWorldPosition(Parameters));
	FWSVector3 Local75 = WSMultiply(Local74, ((MaterialFloat3)Material.PreshaderBuffer[5].x));
	FWSVector3 Local76 = WSMultiply(Local75, ((MaterialFloat3)0.62831855));
	MaterialFloat3 Local77 = WSSin(Local76);
	MaterialFloat3 Local78 = (((MaterialFloat3)1.00000000) + DERIV_BASE_VALUE(Local77));
	MaterialFloat3 Local79 = (DERIV_BASE_VALUE(Local78) * ((MaterialFloat3)0.50000000));
	MaterialFloat3 Local80 = (MaterialFloat3(1.00000000,1.00000000,0.00000000).rgb * DERIV_BASE_VALUE(Local79));
	MaterialFloat3 Local81 = (((MaterialFloat3)DERIV_BASE_VALUE(Local73)) * DERIV_BASE_VALUE(Local80));
	MaterialFloat3 Local82 = (((MaterialFloat3)Material.PreshaderBuffer[5].y) * DERIV_BASE_VALUE(Local81));
	MaterialFloat4 Local83 = Parameters.VertexColor;
	MaterialFloat Local84 = DERIV_BASE_VALUE(Local83).r;
	MaterialFloat3 Local85 = (DERIV_BASE_VALUE(Local82) * ((MaterialFloat3)DERIV_BASE_VALUE(Local84)));
	MaterialFloat3 Local86 = (DERIV_BASE_VALUE(Local85) + MaterialFloat3(0.00000000,0.00000000,-10.00000000).rgb);
	MaterialFloat3 Local87 = RotateAboutAxis(DERIV_BASE_VALUE(Local67),DERIV_BASE_VALUE(Local86),DERIV_BASE_VALUE(Local85));
	MaterialFloat3 Local88 = (Local87 * ((MaterialFloat3)Material.PreshaderBuffer[5].z));
	MaterialFloat3 Local89 = (Local88 * ((MaterialFloat3)Material.PreshaderBuffer[5].w));
	MaterialFloat3 Local90 = (Local89 + DERIV_BASE_VALUE(Local85));
	MaterialFloat2 Local91 = Parameters.TexCoords[0].xy;
	MaterialFloat4 Local92 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleLevel(Material_Texture2D_4,samplerMaterial_Texture2D_4,DERIV_BASE_VALUE(Local91),-1.00000000));
	MaterialFloat3 Local93 = (Local90 * Local92.rgb);
	MaterialFloat Local96 = (View.PrevFrameGameTime * Material.PreshaderBuffer[4].x);
	MaterialFloat Local97 = (Local96 * -0.50000000);
	MaterialFloat3 Local98 = (normalize(MaterialFloat4(0.00000000,1.00000000,0.00000000,1.00000000).rgba.rgb) * ((MaterialFloat3)Local97));
	FWSVector3 Local99 = GetPrevWorldPosition(Parameters);
	FWSVector3 Local100 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local99)), WSGetY(DERIV_BASE_VALUE(Local99)), WSGetZ(DERIV_BASE_VALUE(Local99)));
	FWSVector3 Local101 = WSDivideByPow2(DERIV_BASE_VALUE(Local100), ((MaterialFloat3)1024.00000000));
	FWSVector3 Local102 = WSAdd(Local98, DERIV_BASE_VALUE(Local101));
	FWSVector3 Local103 = WSAdd(DERIV_BASE_VALUE(Local102), ((MaterialFloat3)0.50000000));
	MaterialFloat3 Local104 = WSFracDemote(DERIV_BASE_VALUE(Local103));
	MaterialFloat3 Local105 = (DERIV_BASE_VALUE(Local104) * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local106 = (DERIV_BASE_VALUE(Local105) + ((MaterialFloat3)-1.00000000));
	MaterialFloat3 Local107 = abs(DERIV_BASE_VALUE(Local106));
	MaterialFloat3 Local108 = (DERIV_BASE_VALUE(Local107) * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local109 = (((MaterialFloat3)3.00000000) - DERIV_BASE_VALUE(Local108));
	MaterialFloat3 Local110 = (DERIV_BASE_VALUE(Local109) * DERIV_BASE_VALUE(Local107));
	MaterialFloat3 Local111 = (DERIV_BASE_VALUE(Local110) * DERIV_BASE_VALUE(Local107));
	MaterialFloat Local112 = dot(normalize(MaterialFloat4(0.00000000,1.00000000,0.00000000,1.00000000).rgba.rgb),DERIV_BASE_VALUE(Local111));
	FWSVector3 Local113 = WSDivide(DERIV_BASE_VALUE(Local100), ((MaterialFloat3)200.00000000));
	FWSVector3 Local114 = WSAdd(((MaterialFloat3)Local97), DERIV_BASE_VALUE(Local113));
	FWSVector3 Local115 = WSAdd(DERIV_BASE_VALUE(Local114), ((MaterialFloat3)0.50000000));
	MaterialFloat3 Local116 = WSFracDemote(DERIV_BASE_VALUE(Local115));
	MaterialFloat3 Local117 = (DERIV_BASE_VALUE(Local116) * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local118 = (DERIV_BASE_VALUE(Local117) + ((MaterialFloat3)-1.00000000));
	MaterialFloat3 Local119 = abs(DERIV_BASE_VALUE(Local118));
	MaterialFloat3 Local120 = (DERIV_BASE_VALUE(Local119) * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local121 = (((MaterialFloat3)3.00000000) - DERIV_BASE_VALUE(Local120));
	MaterialFloat3 Local122 = (DERIV_BASE_VALUE(Local121) * DERIV_BASE_VALUE(Local119));
	MaterialFloat3 Local123 = (DERIV_BASE_VALUE(Local122) * DERIV_BASE_VALUE(Local119));
	MaterialFloat3 Local124 = (DERIV_BASE_VALUE(Local123) - ((MaterialFloat3)0.00000000));
	MaterialFloat Local125 = length(DERIV_BASE_VALUE(Local124));
	MaterialFloat Local126 = (DERIV_BASE_VALUE(Local112) + DERIV_BASE_VALUE(Local125));
	MaterialFloat Local127 = (DERIV_BASE_VALUE(Local126) * 6.28318548);
	MaterialFloat4 Local128 = MaterialFloat4(cross(normalize(MaterialFloat4(0.00000000,1.00000000,0.00000000,1.00000000).rgba.rgb),MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb),DERIV_BASE_VALUE(Local127));
	FWSVector3 Local129 = TransformLocalPositionToPrevWorld(Parameters, MaterialFloat3(0.00000000,0.00000000,0.00000000).rgb);
	FWSVector3 Local130 = WSAdd(Local129, Material.PreshaderBuffer[4].yzw);
	FWSVector3 Local131 = WSSubtract(DERIV_BASE_VALUE(Local100), Local130);
	MaterialFloat3 Local132 = WSDemote(DERIV_BASE_VALUE(Local131));
	MaterialFloat Local133 = length(DERIV_BASE_VALUE(Local132));
	MaterialFloat Local134 = (DERIV_BASE_VALUE(Local133) / 256.00000000);
	FWSVector3 Local135 = WSAdd(((MaterialFloat3)View.PrevFrameGameTime), GetObjectWorldPosition(Parameters));
	FWSVector3 Local136 = WSMultiply(Local135, ((MaterialFloat3)Material.PreshaderBuffer[5].x));
	FWSVector3 Local137 = WSMultiply(Local136, ((MaterialFloat3)0.62831855));
	MaterialFloat3 Local138 = WSSin(Local137);
	MaterialFloat3 Local139 = (((MaterialFloat3)1.00000000) + DERIV_BASE_VALUE(Local138));
	MaterialFloat3 Local140 = (DERIV_BASE_VALUE(Local139) * ((MaterialFloat3)0.50000000));
	MaterialFloat3 Local141 = (MaterialFloat3(1.00000000,1.00000000,0.00000000).rgb * DERIV_BASE_VALUE(Local140));
	MaterialFloat3 Local142 = (((MaterialFloat3)DERIV_BASE_VALUE(Local134)) * DERIV_BASE_VALUE(Local141));
	MaterialFloat3 Local143 = (((MaterialFloat3)Material.PreshaderBuffer[5].y) * DERIV_BASE_VALUE(Local142));
	MaterialFloat3 Local144 = (DERIV_BASE_VALUE(Local143) * ((MaterialFloat3)DERIV_BASE_VALUE(Local84)));
	MaterialFloat3 Local145 = (DERIV_BASE_VALUE(Local144) + MaterialFloat3(0.00000000,0.00000000,-10.00000000).rgb);
	MaterialFloat3 Local146 = RotateAboutAxis(DERIV_BASE_VALUE(Local128),DERIV_BASE_VALUE(Local145),DERIV_BASE_VALUE(Local144));
	MaterialFloat3 Local147 = (Local146 * ((MaterialFloat3)Material.PreshaderBuffer[5].z));
	MaterialFloat3 Local148 = (Local147 * ((MaterialFloat3)Material.PreshaderBuffer[5].w));
	MaterialFloat3 Local149 = (Local148 + DERIV_BASE_VALUE(Local144));
	MaterialFloat3 Local150 = (Local149 * Local92.rgb);
	return Local93.rgb;;
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
	MaterialFloat Local1 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local0), 2);
	MaterialFloat4 Local2 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,DERIV_BASE_VALUE(Local0),View.MaterialTextureMipBias));
	MaterialFloat Local3 = MaterialStoreTexSample(Parameters, Local2, 2);
	MaterialFloat2 Local4 = (DERIV_BASE_VALUE(Local0) * ((MaterialFloat2)Material.PreshaderBuffer[0].x));
	MaterialFloat Local5 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local4), 2);
	MaterialFloat4 Local6 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,DERIV_BASE_VALUE(Local4),View.MaterialTextureMipBias));
	MaterialFloat Local7 = MaterialStoreTexSample(Parameters, Local6, 2);
	MaterialFloat3 Local8 = lerp(Local2.rgb,Local6.rgb,Material.PreshaderBuffer[0].y);

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = Local8.rgb;


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
	MaterialFloat3 Local9 = lerp(0.00000000.rrr,Material.PreshaderBuffer[1].xyz,Material.PreshaderBuffer[0].z);
	MaterialFloat Local10 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local4), 4);
	MaterialFloat4 Local11 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,DERIV_BASE_VALUE(Local4),View.MaterialTextureMipBias));
	MaterialFloat Local12 = MaterialStoreTexSample(Parameters, Local11, 4);
	MaterialFloat3 Local13 = (Local11.rgb * Material.PreshaderBuffer[2].xyz);
	MaterialFloat3 Local14 = (Local13 * ((MaterialFloat3)0.50000000));
	MaterialFloat Local15 = dot(WorldNormalCopy,normalize(MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb));
	MaterialFloat Local16 = (Local15 * 0.50000000);
	MaterialFloat Local17 = (Local16 + 0.50000000);
	MaterialFloat Local18 = (Local17 * 21.08651161);
	MaterialFloat Local19 = (Local18 + (-5.28591824 - 10.54325581));
	MaterialFloat Local20 = saturate(Local19);
	MaterialFloat3 Local21 = lerp(Local13,Local14,Local20);
	MaterialFloat4 Local22 = MaterialCollection0.Vectors[0];
	MaterialFloat3 Local23 = lerp(Local13,Local21,Local22.r);
	MaterialFloat Local24 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local4), 3);
	MaterialFloat4 Local25 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,DERIV_BASE_VALUE(Local4),View.MaterialTextureMipBias));
	MaterialFloat Local26 = MaterialStoreTexSample(Parameters, Local25, 3);
	MaterialFloat Local27 = (Local25.b * Material.PreshaderBuffer[2].w);
	MaterialFloat Local28 = (1.00000000 - Local25.g);
	MaterialFloat Local29 = (Material.PreshaderBuffer[3].x * Local28);
	MaterialFloat Local30 = lerp(Material.PreshaderBuffer[3].z,Material.PreshaderBuffer[3].y,Local29);
	MaterialFloat Local31 = saturate(Local30);
	MaterialFloat Local32 = (Local25.g * Material.PreshaderBuffer[3].w);
	MaterialFloat Local33 = lerp(Local32,0.20000000,Local20);
	MaterialFloat Local34 = lerp(Local32,Local33,Local22.r);
	MaterialFloat3 Local94 = (Local13 * ((MaterialFloat3)Material.PreshaderBuffer[6].x));
	MaterialFloat Local95 = (Local25.r * Material.PreshaderBuffer[6].z);

	PixelMaterialInputs.EmissiveColor = Local9;
	PixelMaterialInputs.Opacity = 1.00000000;
	PixelMaterialInputs.OpacityMask = Local11.a.r;
	PixelMaterialInputs.BaseColor = Local23.rgb;
	PixelMaterialInputs.Metallic = Local27.r;
	PixelMaterialInputs.Specular = Local31.r.r;
	PixelMaterialInputs.Roughness = Local34.r;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Normal = Local8.rgb;
	PixelMaterialInputs.Tangent = MaterialFloat3(1.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Subsurface = MaterialFloat4(Local94.rgb,Material.PreshaderBuffer[6].y);
	PixelMaterialInputs.AmbientOcclusion = Local95.r;
	PixelMaterialInputs.Refraction = 0;
	PixelMaterialInputs.PixelDepthOffset = 0.00000000.r;
	PixelMaterialInputs.ShadingModel = 2;
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
	Initialize_MaterialCollection0();

	Initialize_MaterialCollection1();

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
	Initialize_MaterialCollection0();

	Initialize_MaterialCollection1();


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