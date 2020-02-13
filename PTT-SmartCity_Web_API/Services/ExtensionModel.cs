using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace PTT_SmartCity_Web_API.Services
{
    public static class ExtensionModel
    {
        //ปรับแต่งค่า Error ของ ModelState ใหม่
        public static string GetErrorModelState(this ModelStateDictionary modelState)
        {
            string ErrorMessage = null;

            var modelValues = modelState.Values.Select(value => value.Errors).FirstOrDefault();
            if (modelValues.FirstOrDefault().Exception != null)
            {
                string message = modelValues[0].Exception.Message;
                var modelMsg = message.Split(new char[] { '\'' }).Where(x => x != null && x.Trim().Length > 0).Select(x => x.Trim()).ToList();
                ErrorMessage = $"The {modelMsg[3].ToString()} Field is Required";
            }
            else
            {
                modelValues = modelState.Values.Select(value => value.Errors).FirstOrDefault();
                ErrorMessage = modelValues[0].ErrorMessage;
            }
            return ErrorMessage;
        }
    }
}