#ifndef C_SMARTID_ENGINE_SMARTID_COMMON_H_INCLUDED
#define C_SMARTID_ENGINE_SMARTID_COMMON_H_INCLUDED

#define OPEN_EXTERN_C extern "C" {
#define CLOSE_EXTERN_C }

#ifdef __cplusplus
OPEN_EXTERN_C
#endif // __cplusplus

#if defined _WIN32 && SMARTID_ENGINE_EXPORTS
#  define SMARTID_DLL_EXPORT   __declspec( dllexport )
#else
#  define SMARTID_DLL_EXPORT
#endif // SMARTID_ENGINE_EXPORTS defined

typedef struct {
  char* buffer;
  unsigned long buffer_size;
} CSmartIdErrorMessage;

enum CSmartIdImageOrientation {
  CSMARTID_IMAGE_ORIENTATION_LANDSCAPE,
  CSMARTID_IMAGE_ORIENTATION_PORTRAIT,
  CSMARTID_IMAGE_ORIENTATION_INVERTED_LANDSCAPE,
  CSMARTID_IMAGE_ORIENTATION_INVERTED_PORTRAIT
};

typedef struct {
  int x;
  int y;
  int width;
  int height;
} CSmartIdRectangle;

SMARTID_DLL_EXPORT int CSmartIdRectangleCreate(
    CSmartIdRectangle* out_rectangle,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdRectangleDestroy(
    CSmartIdRectangle* in_rectangle,
    CSmartIdErrorMessage* error_message);

typedef struct {
  double x;
  double y;
} CSmartIdPoint;

SMARTID_DLL_EXPORT int CSmartIdPointCreate(
    CSmartIdPoint* out_point,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdPointDestroy(
    CSmartIdPoint* in_point,
    CSmartIdErrorMessage* error_message);

typedef struct {
  CSmartIdPoint points[4];
} CSmartIdQuadrangle;

SMARTID_DLL_EXPORT int CSmartIdQuadrangleCreate(
    CSmartIdQuadrangle* out_quad,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdQuadrangleDestroy(
    CSmartIdQuadrangle* in_quad,
    CSmartIdErrorMessage* error_message);

typedef struct {
  int width;
  int height;
  int stride;
  int channels;
  void* image_internal;
} CSmartIdImage;

SMARTID_DLL_EXPORT int CSmartIdImageCreate(
    CSmartIdImage* out_image,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdImageDestroy(
    CSmartIdImage* in_image,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdImageCopy(
    CSmartIdImage* in_image,
    CSmartIdImage* out_image,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdImageGetRequiredBufferLength(
    CSmartIdImage* in_image,
    int* out_buffer_length,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdImageCopyToBuffer(
    CSmartIdImage* in_image,
    char* out_buffer,
    int out_buffer_length,
    int* out_bytes_copied,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdImageGetRequiredBase64BufferLength(
    CSmartIdImage* in_image,
    int* out_buffer_length,
    CSmartIdErrorMessage* error_message);

SMARTID_DLL_EXPORT int CSmartIdImageCopyBase64ToBuffer(
    CSmartIdImage* in_image,
    char* out_buffer,
    int out_buffer_length,
    int* out_bytes_copied,
    CSmartIdErrorMessage* error_message);

// string vector
typedef struct {
  char** data;
  unsigned long size;
} CSmartIdStringVector;

SMARTID_DLL_EXPORT int CSmartIdStringVectorDestroy(
    CSmartIdStringVector *in_vector,
    CSmartIdErrorMessage* error_message);

// string vector 2D
typedef struct {
  CSmartIdStringVector* data;
  unsigned long size;
} CSmartIdStringVector2d;

SMARTID_DLL_EXPORT int CSmartIdStringVector2dDestroy(
    CSmartIdStringVector2d *in_vector2d,
    CSmartIdErrorMessage* error_message);

#ifdef __cplusplus
CLOSE_EXTERN_C
#endif // __cplusplus

#endif // C_SMARTID_ENGINE_SMARTID_COMMON_H_INCLUDED
