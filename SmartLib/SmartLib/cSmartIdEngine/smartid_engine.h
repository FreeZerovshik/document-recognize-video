#ifndef C_SMARTID_ENGINE_SMARTID_ENGINE_H_INCLUDED
#define C_SMARTID_ENGINE_SMARTID_ENGINE_H_INCLUDED

#include "smartid_common.h"
#include "smartid_result.h"

#ifdef __cplusplus
OPEN_EXTERN_C
#endif // __cplusplus

typedef struct {
  void *smartid_settings_internal;
} CSmartIdSessionSettings;

SMARTID_DLL_EXPORT int CSmartIdSessionSettingsDestroy(
    CSmartIdSessionSettings* in_session_settings,
    CSmartIdErrorMessage* error_message);

// CSmartIdSessionSettings - document types
SMARTID_DLL_EXPORT int CSmartIdSessionSettingsGetSupportedDocumentTypes(
    CSmartIdSessionSettings* in_session_settings,
    CSmartIdStringVector2d* out_supported_document_types,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdSessionSettingsGetEnabledDocumentTypes(
    CSmartIdSessionSettings* in_session_settings,
    CSmartIdStringVector* out_enabled_document_types,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdSessionSettingsSetEnabledDocumentTypes(
    CSmartIdSessionSettings* in_session_settings,
    CSmartIdStringVector* in_document_types,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdSessionSettingsAddEnabledDocumentTypes(
    CSmartIdSessionSettings* in_session_settings,
    char* in_document_types_mask,
    CSmartIdErrorMessage* error_message);

// CSmartIdSessionSettings - options
SMARTID_DLL_EXPORT int CSmartIdSessionSettingsGetOptionNames(
    CSmartIdSessionSettings* in_session_settings,
    CSmartIdStringVector* out_option_names,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdSessionSettingsGetOptionValues(
    CSmartIdSessionSettings* in_session_settings,
    CSmartIdStringVector* out_option_values,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdSessionSettingsSetOption(
    CSmartIdSessionSettings* in_session_settings,
    char* in_option_name,
    char* in_option_value,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdSessionSettingsRemoveOption(
    CSmartIdSessionSettings* in_session_settings,
    char* in_option_name,
    CSmartIdErrorMessage* error_message);

// recognition session
typedef struct {
  void* session_internal;
  void* reporter_internal;
} CSmartIdRecognitionSession;

SMARTID_DLL_EXPORT int CSmartIdRecognitionSessionDestroy(
    CSmartIdRecognitionSession* in_session,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionSessionProcessSnapshotWithRoi(
    CSmartIdRecognitionSession* in_session,
    void* in_data,
    int in_data_length,
    int in_width,
    int in_height,
    int in_stride,
    int in_channels,
    CSmartIdRectangle* in_roi,
    int in_image_orientation,
    CSmartIdRecognitionResult* out_recog_result,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionSessionProcessSnapshot(
    CSmartIdRecognitionSession* in_session,
    void* in_data,
    int in_data_length,
    int in_width,
    int in_height,
    int in_stride,
    int in_channels,
    int in_image_orientation,
    CSmartIdRecognitionResult* out_recog_result,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionSessionProcessYUVSnapshotWithRoi(
    CSmartIdRecognitionSession* in_session,
    void* in_yuv_data,
    int in_yuv_data_length,
    int in_width,
    int in_height,
    CSmartIdRectangle* in_roi,
    int in_image_orientation,
    CSmartIdRecognitionResult* out_recog_result,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionSessionProcessYUVSnapshot(
    CSmartIdRecognitionSession* in_session,
    void* in_yuv_data,
    int in_yuv_data_length,
    int in_width,
    int in_height,
    int in_image_orientation,
    CSmartIdRecognitionResult* out_recog_result,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionSessionProcessImageFileWithRoi(
    CSmartIdRecognitionSession* in_session,
    char* in_image_filename,
    CSmartIdRectangle* in_roi,
    int in_image_orientation,
    CSmartIdRecognitionResult* out_recog_result,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionSessionProcessImageFile(
    CSmartIdRecognitionSession* in_session,
    char* in_image_filename,
    int in_image_orientation,
    CSmartIdRecognitionResult* out_recog_result,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionSessionReset(
    CSmartIdRecognitionSession* in_session,
    CSmartIdErrorMessage* error_message);

// recognition engine
typedef void* CSmartIdRecognitionEngine;

SMARTID_DLL_EXPORT int CSmartIdRecognitionEngineCreateFromConfigPath(
    CSmartIdRecognitionEngine* out_engine,
    char* config_path,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionEngineCreateFromZipArchive(
    CSmartIdRecognitionEngine* out_engine,
    void* zip_config_data,
    int zip_config_data_length,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionEngineDestroy(
    CSmartIdRecognitionEngine* in_engine,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionEngineCreateSessionSettings(
    CSmartIdRecognitionEngine* in_engine,
    CSmartIdSessionSettings* out_session_settings,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionEngineSpawnSession(
    CSmartIdRecognitionEngine* in_engine,
    CSmartIdSessionSettings* in_session_settings,
    CSmartIdResultReporterInterface* in_result_reporter,
    CSmartIdRecognitionSession* out_session,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIDRecognitionEngineGetVersion(char* out_version);

#ifdef __cplusplus
CLOSE_EXTERN_C
#endif // __cplusplus

#endif // C_SMARTID_ENGINE_SMARTID_ENGINE_H_INCLUDED
