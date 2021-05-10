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
using Java.Lang;

namespace VideoTranslator.Droid.Services
{
    class CameraDevice : ICameraDevice
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private string cameraId;
        private CameraCharacteristics cameraCharacteristics;
        private CameraCaptureSession cameraCaptureSession;
        private CaptureRequest captureRequest;
        private CaptureRequest.Builder builder;
    }
}