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
    class CameraDeviceFactory : ICameraDeviceFactory
    {
        private readonly CameraManager cameraManager;

        public CameraDeviceFactory()
        {
            cameraManager = Application.Context.GetSystemService(Context.CameraService) as CameraManager;
            if (cameraManager is null)
            {
                // Надо бы организовать что-то вроде окна лога
                AndroidEnvironment.RaiseThrowable(new Throwable("CameraManager is null"));
            }
        }
    }
}