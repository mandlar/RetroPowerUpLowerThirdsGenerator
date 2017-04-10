using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LowerThirdsGenerator
{
    class LowerThird
    {
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string LineThree { get; set; }

        /// <summary>
        /// Converts LineOne of the lower third into a readable filename format
        /// e.g. "HELLO WORLD" becomes "lt_HelloWorld"
        /// </summary>
        /// <returns></returns>
        public string GetLowerThirdFileName()
        {
            // convert to Title Case
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string fileName = textInfo.ToTitleCase(LineOne.ToLower());

            // remove all characters except for alphanumerical
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            fileName = rgx.Replace(fileName, "");

            // add prefix
            fileName = "lt_" + fileName;

            return fileName;
        }
    }
}
