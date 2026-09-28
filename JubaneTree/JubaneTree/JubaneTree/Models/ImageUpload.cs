using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace JubaneTree.Models
{
    public class ImageUpload
    {
        // set default size here
        public int Width { get; set; }

        public int Height { get; set; }

        // folder for the upload, you can put this in the web.config
        public string UploadPath => "~/Images/Members/";

        public ImageResult RenameUploadFile(HttpPostedFileBase file, Int32 counter = 0)
        {
            var fileName = Path.GetFileName(file.FileName);

            string prepend = "item_";
            string finalFileName = prepend + ((counter).ToString()) + "_" + fileName;
            if (System.IO.File.Exists
                (HttpContext.Current.Request.MapPath(UploadPath + finalFileName)))
            {
                //file exists => add country try again
                return RenameUploadFile(file, ++counter);
            }
            //file doesn't exist, upload item but validate first
            return UploadFile(file, finalFileName);
        }

        private ImageResult UploadFile(HttpPostedFileBase file, string fileName)
        {
            var imageResult =
                new ImageResult
                {
                    Success = true,
                    ErrorMessage = null
                };

            var extension =
                Path.GetExtension(file.FileName)
                    .ToLowerInvariant();

            if (!ValidateExtension(extension))
            {
                imageResult.Success = false;
                imageResult.ErrorMessage =
                    "Invalid image type.";

                return imageResult;
            }

            try
            {
                var uploadDirectory =
                    HttpContext.Current.Server.MapPath(
                        UploadPath);

                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(
                        uploadDirectory);
                }

                var path =
                    Path.Combine(
                        uploadDirectory,
                        fileName);

                // Read directly from uploaded file
                using (var original =
                    Image.FromStream(file.InputStream))
                {
                    using (var resized =
                        Scale(original))
                    {
                        SaveImage(
                            resized,
                            path,
                            extension);
                    }
                }

                imageResult.ImageName =
                    fileName;

                return imageResult;
            }
            catch (Exception ex)
            {
                imageResult.Success = false;
                imageResult.ErrorMessage =
                    ex.Message;

                return imageResult;
            }
        }

        private static void SaveImage(Image image,string path,string extension)
        {
            switch (extension.ToLowerInvariant())
            {
                case ".jpg":
                case ".jpeg":

                    image.Save(
                        path,
                        ImageFormat.Jpeg);

                    break;

                case ".png":

                    image.Save(
                        path,
                        ImageFormat.Png);

                    break;

                case ".gif":

                    image.Save(
                        path,
                        ImageFormat.Gif);

                    break;

                default:

                    throw new InvalidOperationException(
                        "Unsupported image format.");
            }
        }
        private bool ValidateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".jpg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        private Image Scale(Image imgPhoto)
        {
            float sourceWidth = imgPhoto.Width;
            float sourceHeight = imgPhoto.Height;
            float destHeight = 0;
            float destWidth = 0;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            // force resize, might distort image
            if (Width != 0 && Height != 0)
            {
                destWidth = Width;
                destHeight = Height;
            }
            // change size proportially depending on width or height
            else if (Height != 0)
            {
                destWidth = (float)(Height * sourceWidth) / sourceHeight;
                destHeight = Height;
            }
            else
            {
                destWidth = Width;
                destHeight = (float)(sourceHeight * Width / sourceWidth);
            }

            Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
                                        PixelFormat.Format32bppPArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
                new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }
    }
}