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

public class RecognitionEngine : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal RecognitionEngine(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(RecognitionEngine obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~RecognitionEngine() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          csSmartIdEnginePINVOKE.delete_RecognitionEngine(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public RecognitionEngine(string config_path) : this(csSmartIdEnginePINVOKE.new_RecognitionEngine__SWIG_0(config_path), true) {
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
  }

  public RecognitionEngine(byte[] config_data, uint data_length) : this(csSmartIdEnginePINVOKE.new_RecognitionEngine__SWIG_1(config_data, data_length), true) {
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
  }

  public SessionSettings CreateSessionSettings() {
    global::System.IntPtr cPtr = csSmartIdEnginePINVOKE.RecognitionEngine_CreateSessionSettings(swigCPtr);
    SessionSettings ret = (cPtr == global::System.IntPtr.Zero) ? null : new SessionSettings(cPtr, true);
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public RecognitionSession SpawnSession(SessionSettings session_settings, ResultReporterInterface result_reporter) {
    global::System.IntPtr cPtr = csSmartIdEnginePINVOKE.RecognitionEngine_SpawnSession__SWIG_0(swigCPtr, SessionSettings.getCPtr(session_settings), ResultReporterInterface.getCPtr(result_reporter));
    RecognitionSession ret = (cPtr == global::System.IntPtr.Zero) ? null : new RecognitionSession(cPtr, true);
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public RecognitionSession SpawnSession(SessionSettings session_settings) {
    global::System.IntPtr cPtr = csSmartIdEnginePINVOKE.RecognitionEngine_SpawnSession__SWIG_1(swigCPtr, SessionSettings.getCPtr(session_settings));
    RecognitionSession ret = (cPtr == global::System.IntPtr.Zero) ? null : new RecognitionSession(cPtr, true);
    if (csSmartIdEnginePINVOKE.SWIGPendingException.Pending) throw csSmartIdEnginePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public static string GetVersion() {
    string ret = csSmartIdEnginePINVOKE.RecognitionEngine_GetVersion();
    return ret;
  }

}

}
