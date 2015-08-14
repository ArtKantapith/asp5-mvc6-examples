using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace ProgressBarComponent.TagHelpers
{
    // You may need to install the Microsoft.AspNet.Razor.Runtime package into your project
    [TargetElement("div", Attributes = ProgressValueAttributeName)]
    public class ProgressBarTagHelper : TagHelper
    {

        private const string ProgressValueAttributeName = "bs-progress-value";
        private const string ProgressMinAttributeName = "bs-progress-min";
        private const string ProgressMaxAttributeName = "bs-progress-max";

        [HtmlAttributeName(ProgressValueAttributeName)]
        public int ProgressValue { get; set; }
        [HtmlAttributeName(ProgressMinAttributeName)]
        public int ProgressMin { get; set; } = 0;
        [HtmlAttributeName(ProgressMaxAttributeName)]
        public int ProgressMax { get; set; } = 100;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ProgressMin >= ProgressMax)
            {
                throw new ArgumentException($"{ProgressMinAttributeName} must be less than {ProgressMaxAttributeName}");
            }
            if (ProgressValue > ProgressMax || ProgressValue < ProgressMin)
            {
                throw new ArgumentOutOfRangeException($"{ProgressValueAttributeName} must be within the range of {ProgressMinAttributeName} and {ProgressMaxAttributeName}");
            }
            var progressTotal = ProgressMax - ProgressMin;
            var progressPercentage = Math.Round(((decimal)(ProgressValue - ProgressMin) / (decimal)progressTotal) * 100, 4);
            // multiline string interpolation
            string markup = $@"<div class=""progress-bar"" role=""progressbar"" aria-valuenow=""{ProgressValue}"" aria-valuemin=""{ProgressMin}"" aria-valuemax=""{ProgressMax}"" style=""width: {progressPercentage}%;"">
                <span class=""sr-only"">{progressPercentage}% Complete</span>
              </div>";
            output.Content.Append(markup);
            string classValue;
            if (output.Attributes.ContainsName("class"))
            {
                classValue = $"{output.Attributes["class"]} progress";
            }
            else
            {
                classValue = "progress";
            }
            output.Attributes["class"] = classValue;
            base.Process(context, output);
        }
    }
}
