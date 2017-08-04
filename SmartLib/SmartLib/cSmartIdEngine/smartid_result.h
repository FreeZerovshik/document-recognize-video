#ifndef C_SMARTID_ENGINE_SMARTID_RESULT_H_INCLUDED
#define C_SMARTID_ENGINE_SMARTID_RESULT_H_INCLUDED

#include "smartid_common.h"

#ifdef __cplusplus
OPEN_EXTERN_C
#endif // __cplusplus

typedef struct {
  int character;
  double confidence;
} CSmartIdOcrCharVariant;

SMARTID_DLL_EXPORT int CSmartIdOcrCharVariantCreate(
    CSmartIdOcrCharVariant* out_ocr_char_variant,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdOcrCharVariantDestroy(
    CSmartIdOcrCharVariant* in_ocr_char_variant,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdOcrCharVariantCreateFromUtf8(
    char* in_utf8_char,
    double in_confidence,
    CSmartIdOcrCharVariant* out_ocr_char_variant,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdOcrCharVariantGetRequiredUtf8CharacterBufferSize(
    CSmartIdOcrCharVariant* in_ocr_char_variant,
    int* out_utf8_character_buffer_size,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdOcrCharVariantGetUtf8Character(
    CSmartIdOcrCharVariant* in_ocr_char_variant,
    char* out_utf8_character_buffer,
    int* out_bytes_copied,
    CSmartIdErrorMessage* error_message);

typedef struct {
  CSmartIdOcrCharVariant* ocr_char_variants;
  int ocr_char_variants_size;
  char is_highlighted;
  char is_corrected;
} CSmartIdOcrChar;

SMARTID_DLL_EXPORT int CSmartIdOcrCharCreate(
    CSmartIdOcrChar* out_ocr_char,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdOcrCharDestroy(
    CSmartIdOcrChar* in_ocr_char,
    CSmartIdErrorMessage* error_message);

typedef struct {
  CSmartIdOcrChar* ocr_chars;
  int ocr_chars_size;
} CSmartIdOcrString;

SMARTID_DLL_EXPORT int CSmartIdOcrStringCreate(
    CSmartIdOcrString* out_ocr_string,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdOcrStringDestroy(
    CSmartIdOcrString* in_ocr_string,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdOcrStringCreateFromUtf8String(
    char* in_utf8_string,
    CSmartIdOcrString* out_ocr_string,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdOcrStringGetRequiredUtf8StringBufferSize(
    CSmartIdOcrString* in_ocr_string,
    int* out_utf8_buffer_size,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdOcrStringGetUtf8String(
    CSmartIdOcrString* in_ocr_string,
    char* out_utf8_buffer,
    int* out_bytes_copied,
    CSmartIdErrorMessage* error_message);

typedef struct {
  char* name;
  CSmartIdOcrString value;
  char is_accepted;
  double confidence;
} CSmartIdStringField;

SMARTID_DLL_EXPORT int CSmartIdStringFieldCreate(
    CSmartIdStringField* out_string_field,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdStringFieldDestroy(
    CSmartIdStringField* in_string_field,
    CSmartIdErrorMessage* error_message);

typedef struct {
  char* name;
  CSmartIdImage value;
  char is_accepted;
  double confidence;
} CSmartIdImageField;

SMARTID_DLL_EXPORT int CSmartIdImageFieldCreate(
    CSmartIdImageField* out_image_field,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdImageFieldDestroy(
    CSmartIdImageField* in_image_field,
    CSmartIdErrorMessage* error_message);

typedef struct {
  char* template_type;
  CSmartIdQuadrangle quadrangle;
} CSmartIdMatchResult;

SMARTID_DLL_EXPORT int CSmartIdMatchResultCreate(
    CSmartIdMatchResult* out_match_result,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdMatchResultDestroy(
    CSmartIdMatchResult* in_match_result,
    CSmartIdErrorMessage* error_message);

typedef struct {
  char* document_type;
  CSmartIdMatchResult* match_results;
  int match_results_size;
  char is_terminal;
  void* recognition_result_internal;
} CSmartIdRecognitionResult;

SMARTID_DLL_EXPORT int CSmartIdRecognitionResultCreate(
    CSmartIdRecognitionResult* out_recog_result,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionResultDestroy(
    CSmartIdRecognitionResult* in_recog_result,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionResultGetStringFieldsCount(
    CSmartIdRecognitionResult* in_recog_result,
    int* out_string_fields_count,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionResultGetStringField(
    CSmartIdRecognitionResult* in_recog_result,
    int in_string_field_index,
    CSmartIdStringField* out_string_field,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionResultGetImageFieldsCount(
    CSmartIdRecognitionResult* in_recog_result,
    int* out_image_fields_count,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRecognitionResultGetImageField(
    CSmartIdRecognitionResult* in_recog_result,
    int in_image_field_index,
    CSmartIdImageField* out_image_field,
    CSmartIdErrorMessage* error_message);

typedef void (*CSmartIdSnapshotRejectedCallback)();
typedef void (*CSmartIdDocumentMatchedCallback)(
    CSmartIdMatchResult* out_match_results, int out_match_results_size);
typedef void (*CSmartIdSnapshotProcessedCallback)(
    CSmartIdRecognitionResult* out_recog_result);
typedef void (*CSmartIdSessionEndedCallback)();

typedef struct {
  CSmartIdSnapshotRejectedCallback snapshot_rejected;
  CSmartIdDocumentMatchedCallback document_matched;
  CSmartIdSnapshotProcessedCallback snapshot_processed;
  CSmartIdSessionEndedCallback session_ended;
} CSmartIdResultReporterInterface;

#ifdef __cplusplus
CLOSE_EXTERN_C
#endif // __cplusplus

#endif // C_SMARTID_ENGINE_SMARTID_RESULT_H_INCLUDED
