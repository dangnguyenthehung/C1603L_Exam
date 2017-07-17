using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;

namespace C1603L_Exam
{
    public class Helper
    {
        public static string GetImageName()
        {
            return 
                $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}_{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}{DateTime.Now.Millisecond}";
        }

        public static async Task<SoftwareBitmapSource> ConvertToBitMap(IRandomAccessStream stream)
        {
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();

            SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap,
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Premultiplied);

            SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
            await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);

            return bitmapSource;
        }

        public static async Task<StorageFile> ApplyBlurEffect(IRandomAccessStream stream)
        {
            var device = new CanvasDevice();
            var bitmap = await CanvasBitmap.LoadAsync(device, stream);
            var renderer = new CanvasRenderTarget(device, bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height, bitmap.Dpi);

            using (var ds = renderer.CreateDrawingSession())
            {
                var blur = new GaussianBlurEffect();
                blur.BlurAmount = 9.0f;
                blur.BorderMode = EffectBorderMode.Hard;
                blur.Optimization = EffectOptimization.Quality;
                blur.Source = bitmap;

                ds.DrawImage(blur);
            }

            var saveFile = await SaveTempoaryFile(renderer);

            return saveFile;
        }

        public static async Task<StorageFile> ApplyHueRotationEffect(IRandomAccessStream stream)
        {
            var device = new CanvasDevice();
            var bitmap = await CanvasBitmap.LoadAsync(device, stream);
            var renderer = new CanvasRenderTarget(device, bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height, bitmap.Dpi);

            using (var ds = renderer.CreateDrawingSession())
            {
                var hue = new HueRotationEffect
                {
                    Angle = (float)Math.PI / 2,
                    Source = bitmap
                };
                ds.DrawImage(hue);
            }

            var saveFile = await SaveTempoaryFile(renderer);

            return saveFile;
        }

        public static async Task<StorageFile> ApplySaturationEffect(IRandomAccessStream stream)
        {
            var device = new CanvasDevice();
            var bitmap = await CanvasBitmap.LoadAsync(device, stream);
            var renderer = new CanvasRenderTarget(device, bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height, bitmap.Dpi);

            using (var ds = renderer.CreateDrawingSession())
            {
                var saturation = new SaturationEffect()
                {
                    Saturation = (float) 0.3,
                    Source = bitmap
                };
                ds.DrawImage(saturation);
            }

            var saveFile = await SaveTempoaryFile(renderer);

            return saveFile;
        }

        public static async Task<StorageFile> ApplyLinearTransferEffect(IRandomAccessStream stream)
        {
            var device = new CanvasDevice();
            var bitmap = await CanvasBitmap.LoadAsync(device, stream);
            var renderer = new CanvasRenderTarget(device, bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height, bitmap.Dpi);

            using (var ds = renderer.CreateDrawingSession())
            {
                var saturation = new LinearTransferEffect()
                {
                    AlphaOffset = (float)0.5,
                    BlueOffset = (float)0.2,
                    GreenSlope = (float)0.3,
                    RedOffset = (float)0.3,
                    Source = bitmap
                };
                ds.DrawImage(saturation);
            }

            var saveFile = await SaveTempoaryFile(renderer);

            return saveFile;
        }

        public static async Task<StorageFile> ApplyContrastEffect(IRandomAccessStream stream)
        {
            var device = new CanvasDevice();
            var bitmap = await CanvasBitmap.LoadAsync(device, stream);
            var renderer = new CanvasRenderTarget(device, bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height, bitmap.Dpi);

            using (var ds = renderer.CreateDrawingSession())
            {
                var saturation = new ContrastEffect()
                {
                    Contrast = (float)-0.8,
                    Source = bitmap
                };
                ds.DrawImage(saturation);
            }

            var saveFile = await SaveTempoaryFile(renderer);

            return saveFile;
        }

        public static async Task<StorageFile> ApplyExposureEffect(IRandomAccessStream stream)
        {
            var device = new CanvasDevice();
            var bitmap = await CanvasBitmap.LoadAsync(device, stream);
            var renderer = new CanvasRenderTarget(device, bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height, bitmap.Dpi);

            using (var ds = renderer.CreateDrawingSession())
            {
                var saturation = new ExposureEffect()
                {
                    Exposure = (float)1.2,
                    Source = bitmap
                };
                ds.DrawImage(saturation);
            }

            var saveFile = await SaveTempoaryFile(renderer);

            return saveFile;
        }

        public static async Task<StorageFile> ApplyGammaEffect(IRandomAccessStream stream)
        {
            var device = new CanvasDevice();
            var bitmap = await CanvasBitmap.LoadAsync(device, stream);
            var renderer = new CanvasRenderTarget(device, bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height, bitmap.Dpi);

            using (var ds = renderer.CreateDrawingSession())
            {
                var saturation = new GammaTransferEffect()
                {
                    AlphaAmplitude = (float)0.9,
                    BlueAmplitude = (float)0.8,
                    GreenAmplitude = (float)0.9,
                    RedAmplitude = (float)0.8,
                    Source = bitmap
                };
                ds.DrawImage(saturation);
            }

            var saveFile = await SaveTempoaryFile(renderer);

            return saveFile;
        }

        public static async Task<StorageFile> ApplyGrayscaleEffect(IRandomAccessStream stream)
        {
            var device = new CanvasDevice();
            var bitmap = await CanvasBitmap.LoadAsync(device, stream);
            var renderer = new CanvasRenderTarget(device, bitmap.SizeInPixels.Width, bitmap.SizeInPixels.Height, bitmap.Dpi);

            using (var ds = renderer.CreateDrawingSession())
            {
                var saturation = new GrayscaleEffect()
                {
                    Source = bitmap
                };
                ds.DrawImage(saturation);
            }

            var saveFile = await SaveTempoaryFile(renderer);

            return saveFile;
        }

        public static async Task<StorageFile> SaveTempoaryFile(CanvasRenderTarget renderer)
        {
            StorageFolder destinationFolder =
                await ApplicationData.Current.LocalFolder.CreateFolderAsync("CapturePhoto",
                    CreationCollisionOption.OpenIfExists);

            var saveFile = await destinationFolder.CreateFileAsync("temp.jpg", CreationCollisionOption.ReplaceExisting);

            using (var outStream = await saveFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                await renderer.SaveAsync(outStream, CanvasBitmapFileFormat.Jpeg);
            }

            return saveFile;
        }
    }
}
