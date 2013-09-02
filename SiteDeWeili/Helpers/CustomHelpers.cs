using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MvcHtmlHelpers
{
    public static class CustomHelpers{
        public static IHtmlString GenerateCaptcha( this HtmlHelper helper )  
        {  
              
            var captchaControl = new Recaptcha.RecaptchaControl  
                    {  
                            ID = "recaptcha",  
                            Theme = "white",  
                        PublicKey = "6LfjpuYSAAAAANd5R5xdCChdOF2-k2BrmihtltFD",
                            PrivateKey = "6LfjpuYSAAAAALlkGasw_Sp37gdrLN4toyB-LmQO" 
                };  
  
            var htmlWriter = new HtmlTextWriter( new StringWriter() );  
  
            captchaControl.RenderControl(htmlWriter);  
  
            return new MvcHtmlString(htmlWriter.InnerWriter.ToString());  
        } 
    }
}