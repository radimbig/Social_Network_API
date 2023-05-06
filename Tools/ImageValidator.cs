using ImageMagick;

namespace Social_Network_API.Tools
{
    public class ImageValidator
    {
        static public bool IsImageSizeValid(Stream imageStream)
        {
            
            if (!isImage(imageStream))
            {
                return false;
            }
            using (var tempImage = new MagickImage(imageStream))
            {
                if (tempImage.Width > 300 && tempImage.Height > 300)
                {
                    imageStream.Position = 0;
                    return true;
                }
            }
            return false;
        }

        static public bool IsFileSizeValid(Stream imageStream)
        {
            
            if (imageStream.Length < 5 * 1024 * 1024)
            {
                return true;
            }
            return false;
        }

        static public bool isImage(Stream file)
        {
            
            try
            {
                using (var image = new MagickImage(file)) { }
                file.Position = 0;
                return true;
            }
            catch
            {
                return false;
            }
        }
        static public bool IsImageFormatValid(Stream imageStream)
        {
            
            if (!isImage(imageStream))
            {
                return false;
            }
            using (var image = new MagickImage(imageStream))
            {
                imageStream.Position = 0;
                if (image.Format == MagickFormat.Png || image.Format == MagickFormat.Jpg || image.Format == MagickFormat.Jpeg) 
                {
                    return true;
                }
               
            }
            return false;
        }
        static public bool IsImageValid(Stream imageStream)
        {
            bool result = isImage(imageStream) && IsFileSizeValid(imageStream) && IsImageSizeValid(imageStream) && IsImageFormatValid(imageStream);
            return result;
        }
        static public bool IsImageValid(IFormFile file)
        {
            return IsImageValid(file.OpenReadStream());
        }
    }
}
