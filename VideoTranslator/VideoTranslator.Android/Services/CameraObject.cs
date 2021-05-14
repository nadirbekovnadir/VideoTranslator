using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoTranslator.Interfaces;
using Android.Hardware.Camera2;
using Android.Hardware.Camera2.Params;
using Android.Util;
using Android.Media;
using Android.Graphics;

using JObject = Java.Lang.Object;

namespace VideoTranslator.Droid.Services
{
    public class CameraObject : ICameraObject
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private string cameraId;
        public string CameraId => cameraId;

        private readonly CameraManager cameraManager;

        private readonly CameraCharacteristics cameraCharacteristics;
        private readonly StreamConfigurationMap configurationMap;
        private readonly List<ImageFormatType> outputFormats;
        private ImageFormatType selectedFormat;
        private readonly Size[] outputSizes;

        private CameraDevice cameraDevice;

        private CameraCaptureSession cameraCaptureSession;
        private CaptureRequest captureRequest;
        private CaptureRequest.Builder builder;

        private ImageReader imageReader;
        class CameraStateCallback : CameraDevice.StateCallback
        {
            private readonly CameraObject parent;
            public CameraStateCallback(CameraObject parent)
            {

            }
            public override void OnOpened(CameraDevice camera)
            {
                parent.OnOpened(camera);
            }

            public override void OnDisconnected(CameraDevice camera)
            {
                throw new NotImplementedException();
            }

            public override void OnError(CameraDevice camera, [GeneratedEnum] CameraError error)
            {
                throw new NotImplementedException();
            }

            public override void OnClosed(CameraDevice camera)
            {
                base.OnClosed(camera);
                parent.OnClosed(camera);
            }
        }

        internal CameraObject(string cameraId, CameraManager cameraManager)
        {
            this.cameraId = cameraId;
            this.cameraManager = cameraManager;

            this.cameraCharacteristics = cameraManager.GetCameraCharacteristics(cameraId); ;

            this.configurationMap = cameraCharacteristics
                .Get(CameraCharacteristics.ScalerStreamConfigurationMap) as StreamConfigurationMap;

            this.outputFormats = configurationMap.GetOutputFormats().Select(f => (ImageFormatType)f).ToList();

            // Add Permissions
        }

        public List<string> GetAllFormats() =>
            Enum.GetNames(typeof(ImageFormatType)).ToList();

        public void SetFormat(string format)
        {
            selectedFormat = Enum.Parse<ImageFormatType>(format);
        }

        //public void GetAllSizesForFormat()

        public void Open()
        {
            cameraManager.OpenCamera(cameraId, new CameraStateCallback(this), null);
        }

        protected void OnOpened(CameraDevice camera)
        {
            cameraDevice = camera;
        }

        protected void OnClosed(CameraDevice camera)
        {
            cameraDevice = null;
            camera.Close();
        }
    }

}