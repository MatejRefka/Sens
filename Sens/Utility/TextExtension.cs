using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Sens
{
    /// <summary>
    /// Custom MarkupExtension that can access converter directly through xaml without referencing this file (eg.like binding)
    /// Source: https://stackoverflow.com/questions/41062844/bind-to-resource-text-file, Petter Hesselberg
    /// </summary>

    public class TextExtension : MarkupExtension
    {
        private readonly string fileName;

        public TextExtension(string fileName)
        {
            this.fileName = fileName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            //error thrown in XAML can be ignored, file not found handled here
            try
            {
                var uri = new Uri("pack://application:,,,/" + fileName);
                using (var stream = Application.GetResourceStream(uri).Stream)
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }

            catch (FileNotFoundException e)
            {

                throw e;
            }
                }
            }
        }
    

