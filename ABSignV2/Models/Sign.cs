using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABSignV2
{
    public partial class Sign
    {
        public string GetImageDataUri() {
            var image = this.Images.First();
            var base64 = Convert.ToBase64String(image.ImageBin);
            var imgsrc = string.Format("data:image/jpeg; base64,{0}", base64);
            return imgsrc;
        }
    }
}