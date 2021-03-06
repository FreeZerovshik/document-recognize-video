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

public class StringField : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal StringField(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(StringField obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~StringField() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          csSmartIdEnginePINVOKE.delete_StringField(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public StringField() : this(csSmartIdEnginePINVOKE.new_StringField__SWIG_0(), true) {
  }

  public StringField(string name, OcrString value, bool is_accepted, double confidence) : this(csSmartIdEnginePINVOKE.new_StringField__SWIG_1(name, OcrString.getCPtr(value), is_accepted, confidence), true) {
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
  }

  public StringField(string name, string value, bool is_accepted, double confidence) : this(csSmartIdEnginePINVOKE.new_StringField__SWIG_2(name, value, is_accepted, confidence), true) {
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
  }

  public StringField(string name, string value, string raw_value, bool is_accepted, double confidence) : this(csSmartIdEnginePINVOKE.new_StringField__SWIG_3(name, value, raw_value, is_accepted, confidence), true) {
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
  }

  public string GetName() {
    string ret = csSmartIdEnginePINVOKE.StringField_GetName(swigCPtr);
    return ret;
  }

  public OcrString GetValue() {
    OcrString ret = new OcrString(csSmartIdEnginePINVOKE.StringField_GetValue(swigCPtr), false);
    return ret;
  }

  public string GetUtf8Value() {
    string ret = csSmartIdEnginePINVOKE.StringField_GetUtf8Value(swigCPtr);
    return ret;
  }

  public OcrString GetRawValue() {
    OcrString ret = new OcrString(csSmartIdEnginePINVOKE.StringField_GetRawValue(swigCPtr), false);
    return ret;
  }

  public string GetUtf8RawValue() {
    string ret = csSmartIdEnginePINVOKE.StringField_GetUtf8RawValue(swigCPtr);
    return ret;
  }

  public bool IsAccepted() {
    bool ret = csSmartIdEnginePINVOKE.StringField_IsAccepted(swigCPtr);
    return ret;
  }

  public double GetConfidence() {
    double ret = csSmartIdEnginePINVOKE.StringField_GetConfidence(swigCPtr);
    return ret;
  }

}

}
