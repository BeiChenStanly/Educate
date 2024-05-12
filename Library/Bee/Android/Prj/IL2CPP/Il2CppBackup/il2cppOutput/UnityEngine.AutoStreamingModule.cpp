#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>



struct String_t;

IL2CPP_EXTERN_C String_t* _stringLiteralE0194208F6A51814DFC69FDDB9361B5821168339;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_t510529AC60697D267638EF752B8A82590A8B12D3 
{
};
struct AutoStreaming_tC54FDB6B0999854C20A7E63F84CBECF3D2E21BCD  : public RuntimeObject
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif



IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AutoStreaming_GetAutoStreamingUrlRoot_mC627A627205DE89840EC1B1BE86DF61A68B07B46 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Format_mA8DBB4C2516B9723C5A41E6CB1E2FAF4BBE96DD8 (String_t* ___0_format, RuntimeObject* ___1_arg0, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AutoStreaming_get_autoStreaming_m003D15C5C06B6269B5B3E0FE516E0547B78ADE57 (const RuntimeMethod* method) 
{
	typedef bool (*AutoStreaming_get_autoStreaming_m003D15C5C06B6269B5B3E0FE516E0547B78ADE57_ftn) ();
	static AutoStreaming_get_autoStreaming_m003D15C5C06B6269B5B3E0FE516E0547B78ADE57_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AutoStreaming_get_autoStreaming_m003D15C5C06B6269B5B3E0FE516E0547B78ADE57_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AutoStreaming::get_autoStreaming()");
	bool icallRetVal = _il2cpp_icall_func();
	return icallRetVal;
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AutoStreaming_set_autoStreaming_m82D0D75384915D71E126166663DEFAFB8D04579D (bool ___0_value, const RuntimeMethod* method) 
{
	typedef void (*AutoStreaming_set_autoStreaming_m82D0D75384915D71E126166663DEFAFB8D04579D_ftn) (bool);
	static AutoStreaming_set_autoStreaming_m82D0D75384915D71E126166663DEFAFB8D04579D_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AutoStreaming_set_autoStreaming_m82D0D75384915D71E126166663DEFAFB8D04579D_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AutoStreaming::set_autoStreaming(System.Boolean)");
	_il2cpp_icall_func(___0_value);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AutoStreaming_get_CustomCloudAssetsRoot_mCAC48304EBA085351C989A1B2ADC7CC4D7E57798 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE0194208F6A51814DFC69FDDB9361B5821168339);
		s_Il2CppMethodInitialized = true;
	}
	String_t* V_0 = NULL;
	{
		String_t* L_0;
		L_0 = AutoStreaming_GetAutoStreamingUrlRoot_mC627A627205DE89840EC1B1BE86DF61A68B07B46(NULL);
		String_t* L_1;
		L_1 = String_Format_mA8DBB4C2516B9723C5A41E6CB1E2FAF4BBE96DD8(_stringLiteralE0194208F6A51814DFC69FDDB9361B5821168339, L_0, NULL);
		V_0 = L_1;
		goto IL_0013;
	}

IL_0013:
	{
		String_t* L_2 = V_0;
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AutoStreaming_GetAutoStreamingUrlRoot_mC627A627205DE89840EC1B1BE86DF61A68B07B46 (const RuntimeMethod* method) 
{
	typedef String_t* (*AutoStreaming_GetAutoStreamingUrlRoot_mC627A627205DE89840EC1B1BE86DF61A68B07B46_ftn) ();
	static AutoStreaming_GetAutoStreamingUrlRoot_mC627A627205DE89840EC1B1BE86DF61A68B07B46_ftn _il2cpp_icall_func;
	if (!_il2cpp_icall_func)
	_il2cpp_icall_func = (AutoStreaming_GetAutoStreamingUrlRoot_mC627A627205DE89840EC1B1BE86DF61A68B07B46_ftn)il2cpp_codegen_resolve_icall ("UnityEngine.AutoStreaming::GetAutoStreamingUrlRoot()");
	String_t* icallRetVal = _il2cpp_icall_func();
	return icallRetVal;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
