using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;

namespace MyCVonline.Core.ViewModels.ValidationAttributes
{
    public class ValidatePhotoAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            bool isValid = false;
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                isValid = true;
                return isValid;
            }
            if (file.ContentLength > 4 * 1024 * 1024)
            {
                return isValid;
            }

            if (IsFileTypeValid(file))
            {
                isValid = true;
            }

            return isValid;
        }

        private bool IsFileTypeValid(HttpPostedFileBase file)
        {
            bool isValid = false;

            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    if (IsOneOfValidFormats(img.RawFormat))
                    {
                        isValid = true;
                    }
                }
            }
            catch
            {
                //Image is invalid
            }
            return isValid;
        }

        private bool IsOneOfValidFormats(ImageFormat rawFormat)
        {
            List<ImageFormat> formats = getValidFormats();

            foreach (ImageFormat format in formats)
            {
                if (rawFormat.Equals(format))
                {
                    return true;
                }
            }
            return false;
        }

        private List<ImageFormat> getValidFormats()
        {
            List<ImageFormat> formats = new List<ImageFormat>();
            formats.Add(ImageFormat.Png);
            formats.Add(ImageFormat.Jpeg);
            //add types here
            return formats;
        }
    }
}