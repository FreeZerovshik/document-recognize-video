//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace se.smartid {

public class Image : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Image(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(Image obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~Image() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          csSmartIdEnginePINVOKE.delete_Image(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public Image() : this(csSmartIdEnginePINVOKE.new_Image__SWIG_0(), true) {
  }

  public Image(string image_filename) : this(csSmartIdEnginePINVOKE.new_Image__SWIG_1(image_filename), true) {
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
  }

  public Image(byte[] data, uint data_length, int width, int height, int stride, int channels) : this(csSmartIdEnginePINVOKE.new_Image__SWIG_2(data, data_length, width, height, stride, channels), true) {
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
  }

  public Image(byte[] yuv_data, uint yuv_data_length, int width, int height) : this(csSmartIdEnginePINVOKE.new_Image__SWIG_3(yuv_data, yuv_data_length, width, height), true) {
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
  }

  public Image(Image copy) : this(csSmartIdEnginePINVOKE.new_Image__SWIG_4(Image.getCPtr(copy)), true) {
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
  }

  public void Save(string image_filename) {
    csSmartIdEnginePINVOKE.Image_Save(swigCPtr, image_filename);
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
  }

  public int GetRequiredBufferLength() {
    int ret = csSmartIdEnginePINVOKE.Image_GetRequiredBufferLength(swigCPtr);
    return ret;
  }

  public int CopyToBuffer(byte[] out_buffer, int buffer_length) {
    int ret = csSmartIdEnginePINVOKE.Image_CopyToBuffer(swigCPtr, out_buffer, buffer_length);
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int GetRequiredBase64BufferLength() {
    int ret = csSmartIdEnginePINVOKE.Image_GetRequiredBase64BufferLength(swigCPtr);
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int CopyBase64ToBuffer(byte[] out_buffer, int buffer_length) {
    int ret = csSmartIdEnginePINVOKE.Image_CopyBase64ToBuffer(swigCPtr, out_buffer, buffer_length);
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Clear() {
    csSmartIdEnginePINVOKE.Image_Clear(swigCPtr);
  }

  public int GetWidth() {
    int ret = csSmartIdEnginePINVOKE.Image_GetWidth(swigCPtr);
    return ret;
  }

  public int GetHeight() {
    int ret = csSmartIdEnginePINVOKE.Image_GetHeight(swigCPtr);
    return ret;
  }

  public int GetChannels() {
    int ret = csSmartIdEnginePINVOKE.Image_GetChannels(swigCPtr);
    return ret;
  }

  public bool IsMemoryOwner() {
    bool ret = csSmartIdEnginePINVOKE.Image_IsMemoryOwner(swigCPtr);
    return ret;
  }

  public void ForceMemoryOwner() {
    csSmartIdEnginePINVOKE.Image_ForceMemoryOwner(swigCPtr);
  }

  public void Resize(int new_width, int new_height) {
    csSmartIdEnginePINVOKE.Image_Resize(swigCPtr, new_width, new_height);
  }

  public int width {
    set {
      csSmartIdEnginePINVOKE.Image_width_set(swigCPtr, value);
    } 
    get {
      int ret = csSmartIdEnginePINVOKE.Image_width_get(swigCPtr);
      return ret;
    } 
  }

  public int height {
    set {
      csSmartIdEnginePINVOKE.Image_height_set(swigCPtr, value);
    } 
    get {
      int ret = csSmartIdEnginePINVOKE.Image_height_get(swigCPtr);
      return ret;
    } 
  }

  public int stride {
    set {
      csSmartIdEnginePINVOKE.Image_stride_set(swigCPtr, value);
    } 
    get {
      int ret = csSmartIdEnginePINVOKE.Image_stride_get(swigCPtr);
      return ret;
    } 
  }

  public int channels {
    set {
      csSmartIdEnginePINVOKE.Image_channels_set(swigCPtr, value);
    } 
    get {
      int ret = csSmartIdEnginePINVOKE.Image_channels_get(swigCPtr);
      return ret;
    } 
  }

}

}
