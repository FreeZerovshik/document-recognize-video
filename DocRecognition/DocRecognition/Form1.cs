using System;
using System.Drawing;
using System.Windows.Forms;
using DShowNET;
using DShowNET.Device;
using System.Runtime.InteropServices;

using System.Collections;
using System.Drawing.Imaging;
using System.IO;
using se.smartid;
using System.Text;

namespace Recognition
{

    public partial class Form1 : Form, ISampleGrabberCB
    {
        
        public DsDevice SelectedDevice;
        
        /// <summary> base filter of the actually used video devices. </summary>
        private IBaseFilter capFilter;

        /// <summary> graph builder interface. </summary>
        private IGraphBuilder graphBuilder;

        /// <summary> capture graph builder interface. </summary>
        private ICaptureGraphBuilder2 capGraph;

        /// <summary> video window interface. </summary>
        private IVideoWindow videoWin;

        /// <summary> control interface. </summary>
        private IMediaControl mediaCtrl;

        /// <summary> event interface. </summary>
        private IMediaEventEx mediaEvt;

        private ISampleGrabberCB sampGrabberCB = null;

        private IBasicVideo2 iVideo = null;

        protected ISampleGrabber sampGrabber = null;
      

        private IBaseFilter baseGrabFlt;
        //private IBaseFilter CaptureF = null;

        private const int WM_GRAPHNOTIFY = 0x00008001;  // message from graph

        private const int WS_CHILD = 0x40000000;    // attributes for video window
        private const int WS_CLIPCHILDREN = 0x02000000;
        private const int WS_CLIPSIBLINGS = 0x04000000;


        public Form1()
        {
           InitializeComponent();
        }

        /// <summary> list of installed video devices. </summary>
        private ArrayList capDevices;

     
        //private IBaseFilter SampleGrabberF = null;
        private bool fCaptured = true;
        private int bufferedSize;
        private byte[] savedArray;
        private delegate void CaptureDone();
        private VideoInfoHeader videoInfoHeader;
        private string curr_dir = System.Environment.CurrentDirectory;
        // SaveFileDialog sd;



        public void getDevices()
        {
            DsDev.GetDevicesOfCat(FilterCategory.VideoInputDevice, out capDevices);

            if(capDevices.Count >= 1)
            {
                ListViewItem item = null;
                foreach (DsDevice dev in capDevices)
                {   
                    item = new ListViewItem(dev.Name);
                    item.Tag = dev;
                    lvDev.Items.Add(item);
                }
            }

            if (capDevices.Count == 1) {

                ListViewItem selItem = lvDev.Items[0];
                SelectedDevice = selItem.Tag as DsDevice;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getDevices();
            if (SelectedDevice is null)
            {
                MessageBox.Show(this, "No video capture devices found!", "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close(); return;
            }


            StartVideo(SelectedDevice.Mon);
        }



        /// <summary> create the user selected capture device. </summary>
        bool CreateCaptureDevice(UCOMIMoniker mon)
        {
            object capObj;
            capObj = null;
            try
            {
                Guid gbf = typeof(IBaseFilter).GUID;
                mon.BindToObject(null, null, ref gbf, out capObj);
                capFilter = (IBaseFilter)capObj; capObj = null;
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not create capture device\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            finally
            {
                if (capObj != null)
                    Marshal.ReleaseComObject(capObj); capObj = null;
            }

        }

        /// <summary> create the used COM components and get the interfaces. </summary>
        bool GetInterfaces()
        {
            Type comType = null;
            object comObj = null;
            try
            {
                comType = Type.GetTypeFromCLSID(Clsid.FilterGraph);
                if (comType == null)
                    throw new NotImplementedException(@"DirectShow FilterGraph not installed/registered!");
                comObj = Activator.CreateInstance(comType);
                graphBuilder = (IGraphBuilder)comObj; comObj = null;

                Guid clsid = Clsid.CaptureGraphBuilder2;
                Guid riid = typeof(ICaptureGraphBuilder2).GUID;
                comObj = DsBugWO.CreateDsInstance(ref clsid, ref riid);
                capGraph = (ICaptureGraphBuilder2)comObj; comObj = null;

                comType = Type.GetTypeFromCLSID(Clsid.SampleGrabber);
                if (comType == null)
                    throw new NotImplementedException(@"DirectShow SampleGrabber not installed/registered!");
                comObj = Activator.CreateInstance(comType);
                sampGrabber = (ISampleGrabber)comObj; comObj = null;
             
                baseGrabFlt = (IBaseFilter)sampGrabber;

                mediaCtrl = (IMediaControl)graphBuilder;
                videoWin = (IVideoWindow)graphBuilder;
                mediaEvt = (IMediaEventEx)graphBuilder;

                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not get interfaces\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            finally
            {
                if (comObj != null)
                    Marshal.ReleaseComObject(comObj); comObj = null;
            }
        }

        /// <summary> start all the interfaces, graphs and preview window. </summary>
        bool StartVideo(UCOMIMoniker mon)
        {
            int hr;
            try
            {
                if (!CreateCaptureDevice(mon))
                    return false;

                if (!GetInterfaces())
                    return false;

                if (!SetupGraph())
                    return false;

                if (!SetupVideoWindow())
                    return false;

                hr = mediaCtrl.Run();
                if (hr < 0)
                   Marshal.ThrowExceptionForHR(hr);
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not start video stream\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }


        /// <summary> make the video preview window to show in videoPanel. </summary>
        bool SetupVideoWindow()
        {
            int hr;
            try
            {
                // Set the video window to be a child of the main window
                // установка видеопанели, как дочернее от главного окна
                hr = videoWin.put_Owner(videoPanel.Handle);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                // Set video window style
                hr = videoWin.put_WindowStyle(WS_CHILD | WS_CLIPCHILDREN);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                // Use helper function to position video window in client rect of owner window
                ResizeVideoWindow();

                //---------------------------------------------------

                // Make the video window visible, now that it is properly positioned
                hr = videoWin.put_Visible(DsHlp.OATRUE);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                hr = mediaEvt.SetNotifyWindow(this.Handle, WM_GRAPHNOTIFY, IntPtr.Zero);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not setup video window\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }

        /// <summary> the videoPanel is resized. </summary>
        private void videoPanel_Resize(object sender, System.EventArgs e)
        {
            ResizeVideoWindow();
        }

        /// <summary> if the videoPanel is resized, adjust video preview window. </summary>
        void ResizeVideoWindow()
        {
            if (videoWin != null)
            {
                System.Drawing.Rectangle rc = videoPanel.ClientRectangle;
                videoWin.SetWindowPosition(0, 0, rc.Right, rc.Bottom);
            }
        }

        /// <summary> build the capture graph. </summary>
        bool SetupGraph()
        {
            int hr;
            IBaseFilter mux = null;
            IFileSinkFilter sink = null;


            try
            {
                hr = capGraph.SetFiltergraph(graphBuilder);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                hr = graphBuilder.AddFilter(capFilter, "Ds.NET Video Capture Device");
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                AMMediaType media = new AMMediaType();
                media.majorType = MediaType.Video;
                media.subType = MediaSubType.RGB24;
                media.formatType = FormatType.VideoInfo;        // ???
                hr = sampGrabber.SetMediaType(media);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                hr = graphBuilder.AddFilter(baseGrabFlt, "Ds.NET Grabber");
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);


                Guid cat = PinCategory.Preview;
                Guid med = MediaType.Video;
                hr = capGraph.RenderStream(ref cat, ref med, capFilter, null, null); // preview 
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                cat = PinCategory.Capture;
                med = MediaType.Video;
                hr = capGraph.RenderStream(ref cat, ref med, capFilter, null, baseGrabFlt); // capture 
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                media = new AMMediaType();
                hr = sampGrabber.GetConnectedMediaType(media);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);
                if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero))
                    throw new NotSupportedException("Unknown Grabber Media Format");

                videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
                Marshal.FreeCoTaskMem(media.formatPtr); media.formatPtr = IntPtr.Zero;

                hr = sampGrabber.SetBufferSamples(false);
                if (hr == 0)
                    hr = sampGrabber.SetOneShot(false);
                if (hr == 0)
                    hr = sampGrabber.SetCallback(null, 0);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not setup graph\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            finally
            {
                if (mux != null)
                    Marshal.ReleaseComObject(mux); mux = null;
                if (sink != null)
                    Marshal.ReleaseComObject(sink); sink = null;

            }
        }

        bool StopVideo(UCOMIMoniker mon)
        {
            int hr;
            try
            {
                Guid cat = PinCategory.Capture;
                Guid med = MediaType.Video;

                hr = mediaCtrl.Stop();
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not start video stream\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }


        int ISampleGrabberCB.SampleCB(double SampleTime, IMediaSample pSample)
        {
            return 0;
        }

        int ISampleGrabberCB.BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            if (fCaptured || (savedArray == null))
            {
                return 1;
            }
            fCaptured = true;
            bufferedSize = BufferLen;
            if ((pBuffer != IntPtr.Zero) && (BufferLen > 1000) && (BufferLen <= savedArray.Length))
            {
                Marshal.Copy(pBuffer, savedArray, 0, BufferLen);
            }
            else
            {
                return 1;
            }
            this.BeginInvoke(new CaptureDone(this.OnCaptureDone));
            return 0;
        }

        void OnCaptureDone()
        {
            if (sampGrabber == null) return;
            try
            {
                int hr;
                if (sampGrabber == null)
                    return;
                hr = sampGrabber.SetCallback(null, 0);

                int w = videoInfoHeader.BmiHeader.Width;
                int h = videoInfoHeader.BmiHeader.Height;
                if (((w & 0x03) != 0) || (w < 32) || (w > 4096) || (h < 32) || (h > 4096))
                    return;
                int stride = w * 3;

                GCHandle handle = GCHandle.Alloc(savedArray, GCHandleType.Pinned);
                int scan0 = (int)handle.AddrOfPinnedObject();
                scan0 += (h - 1) * stride;
                Bitmap b = new Bitmap(w, h, -stride, PixelFormat.Format24bppRgb, (IntPtr)scan0);

                //String curr_dir = System.Environment.CurrentDirectory;

                //b.Save(curr_dir + @"\recog.jpg", ImageFormat.Jpeg);

                handle.Free();
                savedArray = null;

                get_recog(b);

                if (b != null)
                    b.Dispose();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Capture()
        {
            int hr = 0;
            if (sampGrabber == null) return;
            if (savedArray == null)
            {
                int size = videoInfoHeader.BmiHeader.ImageSize;
                if ((size < 1000) || (size > 16000000))
                    return;
                savedArray = new byte[size + 64000];
            }

            fCaptured = false;
            hr = sampGrabber.SetCallback(this, 1);
        }


        private void BtnRecog_Click(object sender, EventArgs e)
        {
            Capture();           
        }

        private int cnt = 0;
        private RecognitionResult recog_result;
        private RecognitionSession session;
        private SessionSettings settings;
        private RecognitionEngine engine;
        private CsReporter reporter;

        public void get_recog(byte[] p_bt, int p_len, int w, int h, int p_stride)
        {
            try
            {
                se.smartid.Image sm_img = new se.smartid.Image(p_bt, (uint)Math.Abs(p_stride) * 3, w, h, Math.Abs(p_stride), 3);
                recog_result = session.ProcessImage(sm_img);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error " + e);
            }

            if (recog_result.IsTerminal() || cnt >= RecogCnt.Value)
            {
                if (File.Exists(curr_dir + @"\recognize.log"))
                {
                    File.Delete(curr_dir + @"\recognize.log");
                }

                System.IO.FileStream ostrm = new FileStream(curr_dir + @"\recognize.log", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter writer;
                TextWriter oldOut = Console.Out;

                writer = new StreamWriter(ostrm);
                Console.SetOut(writer);

                Console.WriteLine("Start recognize application");
                Console.WriteLine("--------------------------------");

                reporter = new CsReporter();

                // this is important: GC works differently with native-heap objects
                OutputRecognitionResult(recog_result);

                recog_result.Dispose();
                session.Dispose();
                engine.Dispose();
                reporter.Dispose();

                Console.WriteLine("End application!");
                Console.SetOut(oldOut);

                writer.Close();
                ostrm.Close();
                destroy();
            }
            else
            {
                cnt++;
                pBar.PerformStep();
                Capture();
            }

        }

        private void destroy()
        {

            mediaCtrl.Stop();

            if (recog_result != null)
                recog_result.Dispose();

            if (session != null)
                session.Dispose();

            if (settings != null)
                settings.Dispose();

            if (engine != null)
                engine.Dispose();

            if (reporter != null)
                reporter.Dispose();

            capDevices = null;
            savedArray = null;

            videoPanel.Dispose();
            //pBar.Dispose();
            //Form1.Dispose();
            
            Application.Exit();
        }

        private void init()
        {
            getDevices();
            if (SelectedDevice is null)
                return;

            StartVideo(SelectedDevice.Mon);
        }


        public void get_recog(Bitmap image)
        {
            Bitmap d_image = ImageToByte(image);

            int w = d_image.Width;
            int h = d_image.Height;

            BitmapData bitmapData = d_image.LockBits(new System.Drawing.Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, d_image.PixelFormat);
            int ByteCount = Math.Abs(bitmapData.Stride) * h;
            byte[] Pixels = new byte[ByteCount];

            int bitsPerPixel = ((int)bitmapData.PixelFormat & 0xff00) >> 8;
            int bytesPerPixel = (bitsPerPixel + 7) / 8;
            int stride = 4 * ((w * bytesPerPixel + 3) / 4);

            //указатель на первый пиксель
            IntPtr ptrFirstPixel = bitmapData.Scan0;
            //копируем данные из указателя в массив байтов
            Marshal.Copy(ptrFirstPixel, Pixels, 0, Pixels.Length);


            get_recog(Pixels, Pixels.Length, w, h, stride);
        }

        public byte[] FileToByteArray(string fileName)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(fileName,
                                           FileMode.Open,
                                           FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(fileName).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }

        public static Bitmap ImageToByte(System.Drawing.Image img)
        {
            Bitmap bm;
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                bm = new Bitmap(stream);
                return bm;
            }
        }

         
        private void videoPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.C)
            {
                string image_path = curr_dir + @"\recog.jpg";
                string config_path = curr_dir + @"\smartid.json";
                string document_types = "rus.passport.*";
                engine = new RecognitionEngine(config_path);

                settings = engine.CreateSessionSettings();
                settings.AddEnabledDocumentTypes(document_types);

                session = engine.SpawnSession(settings, reporter);

                Capture();
              
            }else if (e.KeyCode == Keys.F)
            {
                string image_path = curr_dir + @"\recog.jpg";
                byte[] im_bt = FileToByteArray(image_path);

                Bitmap d_image = (Bitmap)System.Drawing.Image.FromFile(image_path);

                int w = d_image.Width;
                int h = d_image.Height;

                BitmapData bitmapData = d_image.LockBits(new System.Drawing.Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, d_image.PixelFormat);
                int ByteCount = bitmapData.Stride * h;
                byte[] Pixels = new byte[ByteCount];

                int bitsPerPixel = ((int)bitmapData.PixelFormat & 0xff00) >> 8;
                int bytesPerPixel = (bitsPerPixel + 7) / 8;
                int stride = 4 * ((w * bytesPerPixel + 3) / 4);

                //указатель на первый пиксель
                IntPtr ptrFirstPixel = bitmapData.Scan0;
                //копируем данные из указателя в массив байтов
                Marshal.Copy(ptrFirstPixel, Pixels, 0, Pixels.Length);

                get_recog(Pixels, Pixels.Length, w, h, stride); 

            }

             //Application.Exit();
        }


        public static void OutputRecognitionResult(se.smartid.RecognitionResult recog_result)
        {
            Console.WriteLine("Document type: {0}", recog_result.GetDocumentType());
            Console.WriteLine("Match results:");

            MatchResultVector match_results = recog_result.GetMatchResults();
            for (int i = 0; i < match_results.Count; i++)
            {
                Console.WriteLine("    Template Type = {0}", match_results[i].GetTemplateType());
                Console.WriteLine("    Zone = {{({0:0.0}, {1:0.0}), ({2:0.0}, {3:0.0}), ({4:0.0}, {5:0.0}), ({6:0.0}, {7:0.0})}}",
                    match_results[i].GetQuadrangle().GetPoint(0).x, match_results[i].GetQuadrangle().GetPoint(0).y,
                    match_results[i].GetQuadrangle().GetPoint(1).x, match_results[i].GetQuadrangle().GetPoint(1).y,
                    match_results[i].GetQuadrangle().GetPoint(2).x, match_results[i].GetQuadrangle().GetPoint(2).y,
                    match_results[i].GetQuadrangle().GetPoint(3).x, match_results[i].GetQuadrangle().GetPoint(3).y);
            }
            Console.WriteLine("String fields:");
            StringVector string_field_names = recog_result.GetStringFieldNames();
            for (int i = 0; i < string_field_names.Count; i++)
            {
                StringField field = recog_result.GetStringField(string_field_names[i]);
                String is_accepted = field.IsAccepted() ? "[+]" : "[-]";
                Utf16CharVector chars = field.GetValue().GetUtf16String();
                StringBuilder value = new StringBuilder();
                foreach (Char ch in chars)
                {
                    value.Append(ch);
                }
                Console.WriteLine("    {0,-15} {1} {2}",
                    field.GetName(), is_accepted, value);
            }
            Console.WriteLine("Image fields:");
            StringVector image_field_names = recog_result.GetImageFieldNames();
            for (int i = 0; i < image_field_names.Count; i++)
            {
                ImageField field = recog_result.GetImageField(image_field_names[i]);
                String is_accepted = field.IsAccepted() ? "[+]" : "[-]";
                Console.WriteLine("    {0, -15} {1} W: {2} H: {3}",
                    field.GetName(), is_accepted,
                    field.GetValue().width, field.GetValue().height);

            }

            Console.WriteLine("Result terminal:    {0}", recog_result.IsTerminal() ? "[+]" : "[-]");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            destroy();
            Application.Exit();
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C)
            {
                // string image_path = curr_dir + @"\recog.jpg";
                string config_path = curr_dir + @"\smartid.json";
                string document_types = "rus.passport.*";
                engine = new RecognitionEngine(config_path);

                settings = engine.CreateSessionSettings();
                settings.AddEnabledDocumentTypes(document_types);

                session = engine.SpawnSession(settings, reporter);

                Capture();

            }
            else if (e.KeyCode == Keys.F)
            {
                string image_path = curr_dir + @"\recog.jpg";
                byte[] im_bt = FileToByteArray(image_path);

                Bitmap d_image = (Bitmap)System.Drawing.Image.FromFile(image_path);

                int w = d_image.Width;
                int h = d_image.Height;

                BitmapData bitmapData = d_image.LockBits(new System.Drawing.Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, d_image.PixelFormat);
                int ByteCount = bitmapData.Stride * h;
                byte[] Pixels = new byte[ByteCount];

                int bitsPerPixel = ((int)bitmapData.PixelFormat & 0xff00) >> 8;
                int bytesPerPixel = (bitsPerPixel + 7) / 8;
                int stride = 4 * ((w * bytesPerPixel + 3) / 4);

                //указатель на первый пиксель
                IntPtr ptrFirstPixel = bitmapData.Scan0;
                //копируем данные из указателя в массив байтов
                Marshal.Copy(ptrFirstPixel, Pixels, 0, Pixels.Length);

                get_recog(Pixels, Pixels.Length, w, h, stride);

            }
            // throw new NotImplementedException();
        }

    }

   
}
