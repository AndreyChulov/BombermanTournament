using System.Reflection;
using Vortice.Direct2D1;
using Vortice.WIC;

namespace Engine.Shared.GraphicEngine
{
    public static class ID2D1RenderTargetExtension
    {
        public static ID2D1Bitmap LoadBitmapFromEmbeddedResource(
            this ID2D1RenderTarget renderTarget, 
            Assembly embeddedResourceAssembly, string embeddedResourceName, 
            int frameIndex = 0)
        {
            using IWICImagingFactory wicImagingFactory = new();

            using Stream stream = embeddedResourceAssembly.GetManifestResourceStream(embeddedResourceName) ?? new MemoryStream();
            using IWICBitmapDecoder bitmapDecoder = wicImagingFactory.CreateDecoderFromStream(stream);
            using IWICBitmapFrameDecode bitmapFrameDecode = bitmapDecoder.GetFrame(frameIndex);
            using IWICFormatConverter formatConverter = wicImagingFactory.CreateFormatConverter();
            
            formatConverter.Initialize(bitmapFrameDecode, PixelFormat.Format64bppRGB);

            return renderTarget.CreateBitmapFromWicBitmap(formatConverter);
        }
        
        public static ID2D1Bitmap LoadBitmapWithAlphaChannelFromEmbeddedResource(
            this ID2D1RenderTarget renderTarget, 
            Assembly embeddedResourceAssembly, string embeddedResourceName, 
            int frameIndex = 0)
        {
            using IWICImagingFactory wicImagingFactory = new();

            using Stream stream = embeddedResourceAssembly.GetManifestResourceStream(embeddedResourceName) ?? new MemoryStream();
            using IWICBitmapDecoder bitmapDecoder = wicImagingFactory.CreateDecoderFromStream(stream);
            using IWICBitmapFrameDecode bitmapFrameDecode = bitmapDecoder.GetFrame(frameIndex);
            using IWICFormatConverter formatConverter = wicImagingFactory.CreateFormatConverter();

            formatConverter.Initialize(
                bitmapFrameDecode, PixelFormat.Format64bppRGBA,
                BitmapDitherType.None, null, 
                0f, BitmapPaletteType.MedianCut); 
            
            return renderTarget.CreateBitmapFromWicBitmap(formatConverter, 
                new BitmapProperties(Vortice.DCommon.PixelFormat.Premultiplied));
        }
    }
}