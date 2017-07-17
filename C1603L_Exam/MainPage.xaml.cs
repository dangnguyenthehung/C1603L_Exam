using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace C1603L_Exam
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string ImagePath = "";

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btn_CaptureImage_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }

            var fileName = $"{Helper.GetImageName()}.jpg";
            //var saveFolder = "D://CapturePhoto";
            //if (!Directory.Exists(saveFolder))
            //{
            //    Directory.CreateDirectory(saveFolder);
            //}
            //StorageFolder destinationFolder = await StorageFolder.GetFolderFromPathAsync(saveFolder);


            StorageFolder destinationFolder =
                await ApplicationData.Current.LocalFolder.CreateFolderAsync("CapturePhoto",
                    CreationCollisionOption.OpenIfExists);

            await photo.CopyAsync(destinationFolder, fileName, NameCollisionOption.ReplaceExisting);

            ImagePath = $"{destinationFolder.Path}\\{fileName}";

            IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
            
            ImageFrame.Source = await Helper.ConvertToBitMap(stream);
        }

        private async void btn_ApplyFilter_Blur_Click(object sender, RoutedEventArgs e)
        {
            var file = await StorageFile.GetFileFromPathAsync(ImagePath);

            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var saveFile = await Helper.ApplyBlurEffect(stream);
                
                // Display Image
                using (var inputStream = await saveFile.OpenAsync(FileAccessMode.Read))
                {
                    ImageFrame_Filter.Source = await Helper.ConvertToBitMap(inputStream);
                }
            }
        }

        private async void btn_ApplyFilter_Hue_Click(object sender, RoutedEventArgs e)
        {
            var file = await StorageFile.GetFileFromPathAsync(ImagePath);

            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var saveFile = await Helper.ApplyHueRotationEffect(stream);

                // Display Image
                using (var inputStream = await saveFile.OpenAsync(FileAccessMode.Read))
                {
                    ImageFrame_Filter.Source = await Helper.ConvertToBitMap(inputStream);
                }
            }
        }

        private async void btn_ApplyFilter_Saturation_Click(object sender, RoutedEventArgs e)
        {
            var file = await StorageFile.GetFileFromPathAsync(ImagePath);

            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var saveFile = await Helper.ApplySaturationEffect(stream);

                // Display Image
                using (var inputStream = await saveFile.OpenAsync(FileAccessMode.Read))
                {
                    ImageFrame_Filter.Source = await Helper.ConvertToBitMap(inputStream);
                }
            }
        }

        private async void btn_ApplyFilter_LinearTransfer_Click(object sender, RoutedEventArgs e)
        {
            var file = await StorageFile.GetFileFromPathAsync(ImagePath);

            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var saveFile = await Helper.ApplyLinearTransferEffect(stream);

                // Display Image
                using (var inputStream = await saveFile.OpenAsync(FileAccessMode.Read))
                {
                    ImageFrame_Filter.Source = await Helper.ConvertToBitMap(inputStream);
                }
            }
        }

        private async void btn_ApplyFilter_Contrast_Click(object sender, RoutedEventArgs e)
        {
            var file = await StorageFile.GetFileFromPathAsync(ImagePath);

            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var saveFile = await Helper.ApplyContrastEffect(stream);

                // Display Image
                using (var inputStream = await saveFile.OpenAsync(FileAccessMode.Read))
                {
                    ImageFrame_Filter.Source = await Helper.ConvertToBitMap(inputStream);
                }
            }
        }

        private async void btn_ApplyFilter_Exposure_Click(object sender, RoutedEventArgs e)
        {
            var file = await StorageFile.GetFileFromPathAsync(ImagePath);

            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var saveFile = await Helper.ApplyExposureEffect(stream);

                // Display Image
                using (var inputStream = await saveFile.OpenAsync(FileAccessMode.Read))
                {
                    ImageFrame_Filter.Source = await Helper.ConvertToBitMap(inputStream);
                }
            }
        }

        private async void btn_ApplyFilter_Gamma_Click(object sender, RoutedEventArgs e)
        {
            var file = await StorageFile.GetFileFromPathAsync(ImagePath);

            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var saveFile = await Helper.ApplyGammaEffect(stream);

                // Display Image
                using (var inputStream = await saveFile.OpenAsync(FileAccessMode.Read))
                {
                    ImageFrame_Filter.Source = await Helper.ConvertToBitMap(inputStream);
                }
            }
        }

        private async void btn_ApplyFilter_Grayscale_Click(object sender, RoutedEventArgs e)
        {
            var file = await StorageFile.GetFileFromPathAsync(ImagePath);

            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var saveFile = await Helper.ApplyGrayscaleEffect(stream);

                // Display Image
                using (var inputStream = await saveFile.OpenAsync(FileAccessMode.Read))
                {
                    ImageFrame_Filter.Source = await Helper.ConvertToBitMap(inputStream);
                }
            }
        }
    }
}
